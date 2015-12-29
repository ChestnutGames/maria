using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System;
using System.Threading;
using Sproto;
using S2cSprotoType;
using C2sSprotoType;

public class SNSocket
{
    
    private enum STATE
    {
        NO_STATE,
        CONNECTING,
        CONNECTED,
        SHUTDOWN_WRITE,
        SHUTDOWN_READ,
        CLOSE,
    }

    private STATE mState;
    private Socket mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

    /// <summary>
    /// 这两个变量都是用来作为消息传递的，为了更好的应用Sproto，所以才加了这个handler。
    /// </summary>
    private ISNDelegate mDelegate = null;
    
    private const int mRecvBufferCapacity = 1024;
    private byte[] mRecvBuffer = new byte[mRecvBufferCapacity];
    private int mRecvOffset = 0;
    private int mRecvSize = 0;

    private Thread mSentT = null;
    private Thread mRecvT = null;

    private Queue<byte[]> mSendQ = new Queue<byte[]>();
    private Queue<byte[]> mRecvQ = new Queue<byte[]>();

    private Dictionary<string, string> mConf = null;

    public SNSocket(Dictionary<string, string> conf)
    {
        mConf = conf;
    }

    /// <summary>
    /// @param 
    /// </summary>
    public void Start()
    {
        Connect();
    }

    /// <summary>
    /// 只是关闭Send，Close会在recv后close。
    /// </summary>
    public void Stop()
    {
        if (mState == STATE.CONNECTED)
        {
            mSocket.Shutdown(SocketShutdown.Send);
        }
    }

    /// <summary>
    /// 设置委托，当有什么消息的时候由委托传递
    /// </summary>
    /// <param name="d"></param>
    public void setDelegate(ISNDelegate d)
    {
        if (mDelegate != d)
        {
            mDelegate = d;
        }
    }

    /// <summary>
    /// 开始连接
    /// </summary>
    public void Connect()
    {
        try
        {
            string IP = mConf["IP"];
            string PORT = mConf["PORT"];
            IPAddress ip = IPAddress.Parse(IP);
            int port = Int32.Parse(PORT);
            //ip = IPAddress.Parse("192.168.1.239");
            //port = 8888;
            mSocket.BeginConnect(new IPEndPoint(ip, port), ConnectAC, this);
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void ConnectAC(IAsyncResult ar)
    {
        mSocket.EndConnect(ar);
        mState = STATE.CONNECTED;
        if (mDelegate != null)
        {
            mDelegate.OnConnected(this);
        }

        mSentT = new Thread(SendRun);
        mSentT.Start();
        mRecvT = new Thread(RecvRun);
        mRecvT.Start();
    }

    /// <summary>
    /// 发送线程，发送之所开一个线程。这里，当初开始直接使用异步并发，可是发送发送不能作为缓存。所以还是单开线程模拟，是不需要用协程呢！
    /// </summary>
    /// <param name="o"></param>
    public void SendRun(object o)
    {
        while (mState != STATE.CLOSE)
        {
            if (mState == STATE.CONNECTED)
            {
                //Debug.Log("SendRun ...");
                byte[] buf = null;
                lock (mSendQ)
                {
                    if (mSendQ.Count > 0)
                    {
                        buf = mSendQ.Dequeue();
                    }
                }
                if (buf != null)
                {
                    SendAsync(buf);
                }
            }
        }
    }

    /// <summary>
    /// 如果用异步并发接受，将会是解析一点接收一点
    /// </summary>
    /// <param name="o"></param>
    public void RecvRun(object o)
    {
        while (mState != STATE.CLOSE)
        {
            if (mState == STATE.CONNECTED)
            {
                IList checkRead = new List<Socket>();
                IList checkError = new List<Socket>();
                checkRead.Add(mSocket);
                checkError.Add(mSocket);
                Socket.Select(checkRead, null, checkError, 1000000); //microsecond
                if (checkError.Contains(mSocket))
                {
                    // 错误状态
                }
                if (checkRead.Contains(mSocket))
                {
                    Debug.Assert(mRecvSize < mRecvBufferCapacity);
                    SocketError errorCode;
                    int size = mSocket.Receive(mRecvBuffer, mRecvSize, mRecvBufferCapacity - mRecvSize, SocketFlags.None, out errorCode);
                    if (errorCode == SocketError.Success)
                    {
                        mRecvSize += size;
                        Debug.Assert(mRecvSize <= mRecvBufferCapacity);
                    }
                    else if (errorCode == SocketError.Disconnecting)
                    {
                        mState = STATE.CONNECTING;
                        if (mDelegate != null)
                        {
                            mDelegate.OnError(this, SocketError.Disconnecting, "disconnecting.");
                        }
                        Connect();
                        break;
                    }
                    else
                    {
                        mState = STATE.CLOSE;
                        Close();
                        break;
                    }
                }
                else
                {
                    Thread.Sleep(1000); // 睡1s
                    break;
                }
                while (mRecvOffset < mRecvSize && (mRecvSize - mRecvOffset > 2))
                {
                    int a = mRecvOffset, b = mRecvOffset + 1;
                    int size = (mRecvBuffer[a] << 8) & 0xffff;
                    size |= mRecvBuffer[b] & 0xffff;
                    mRecvOffset += 2;
                    if (mRecvOffset + size <= mRecvSize)
                    {
                        byte[] buffer = new byte[size];
                        Array.Copy(mRecvBuffer, mRecvOffset, buffer, 0, size);
                        lock (mRecvQ)
                        {
                            mRecvQ.Enqueue(buffer);
                        }
                    }
                }
                System.Buffer.BlockCopy(mRecvBuffer, mRecvOffset, mRecvBuffer, 0, mRecvSize - mRecvOffset);
                mRecvOffset = 0;
                mRecvSize = mRecvSize - mRecvOffset;
            }
            else if (mState == STATE.SHUTDOWN_WRITE)
            {
                IList checkRead = new List<Socket>();
                checkRead.Add(mSocket);
                Socket.Select(checkRead, null, null, 100);
                if (checkRead.Contains(mSocket))
                {
                    Debug.Assert(mRecvSize < mRecvBufferCapacity);
                    SocketError errorCode;
                    int size = mSocket.Receive(mRecvBuffer, mRecvSize, mRecvBufferCapacity - mRecvSize, SocketFlags.None, out errorCode);
                    switch (errorCode)
                    {
                        case SocketError.Success:
                            {
                                mRecvSize += size;
                                Debug.Assert(mRecvSize <= mRecvBufferCapacity);
                            }
                            break;
                        case SocketError.Disconnecting:
                            {
                                mState = STATE.CONNECTING;
                                Connect();
                            }
                            break;
                        default:
                            break;
                    }
                }
                while (mRecvOffset < mRecvSize && (mRecvSize - mRecvOffset > 2))
                {
                    int a = mRecvOffset, b = mRecvOffset + 1;
                    int size = (mRecvBuffer[a] << 8) & 0xffff;
                    size |= mRecvBuffer[b] & 0xffff;
                    mRecvOffset += 2;
                    if (mRecvOffset + size <= mRecvSize)
                    {
                        byte[] buffer = new byte[size];
                        Array.Copy(mRecvBuffer, mRecvOffset, buffer, 0, size);
                        lock (mRecvQ)
                        {
                            mRecvQ.Enqueue(buffer);
                        }
                    }
                }
                System.Buffer.BlockCopy(mRecvBuffer, mRecvOffset, mRecvBuffer, 0, mRecvSize - mRecvOffset);
                mRecvOffset = 0;
                mRecvSize = mRecvSize - mRecvOffset;
                Close();
            }
        }
    }

    /// <summary>
    /// 此函数在主线程运行，分发结果
    /// </summary>
    public void update()
    {
        switch (mState)
        {
            case STATE.CLOSE:
                {
                    mSentT.Join();
                    mRecvT.Join();
                }
                break;
            case STATE.CONNECTED:
                {
                    lock (mRecvQ)
                    {
                        if (mRecvQ.Count > 0)
                        {
                            if (mDelegate != null)
                            {
                                mDelegate.OnMessage(this, mRecvQ.Dequeue());
                            }
                        }
                    }
                }
                break;
            default:
                break;
        }
    }

    public void Send(byte[] buffer)
    {
        lock (mSendQ)
        {
            mSendQ.Enqueue(buffer);
        }
    }

    private void SendAsync(byte[] buffer)
    {
        Debug.Log("SendAsync ......");
        byte[] buf = new byte[buffer.Length + 2];
        short len = (short)buffer.Length;
        buf[0] = (byte)((len >> 8) & 0xFF);
        buf[1] = (byte)((len & 0xff));
        Array.Copy(buffer, 0, buf, 2, buffer.Length);
        SocketError errorCode;
        IAsyncResult ar = mSocket.BeginSend(buf, 0, buf.Length, SocketFlags.None, out errorCode, SendAC, this);
        switch (errorCode)
        {
            case SocketError.Shutdown:
                {
                    // 关闭发送，那么状态会在Recv后关闭
                    mState = STATE.SHUTDOWN_WRITE;
                    mSocket.EndSend(ar);
                    // 是否销毁mSendQ;
                }
                break;
            case SocketError.Disconnecting:
                {
                    mState = STATE.CONNECTING;
                    Connect();
                }
                break;
            default:
                {
                    mSocket.EndSend(ar);
                    if (mDelegate != null)
                    {
                        mDelegate.OnError(this, errorCode, "occor error.");
                    }
                }
                break;
        }
    }

    public void SendAC(IAsyncResult ar)
    {
        mSocket.EndSend(ar);
    }

    public void Close()
    {
        mState = STATE.CLOSE;
        mSocket.Close();
    }
}
