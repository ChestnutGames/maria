using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System;
using Sproto;
using S2cSprotoType;
using C2sSprotoType;

public class Client
{
    public delegate void AT();

    private enum STATE
    {
        NO_STATE,

    }
    
    private Socket mSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    SprotoStream mSendStream = new SprotoStream();
    private byte[] mRecvBuffer = new byte[1024];
    private int mRecvOffset = 0;
    private int mSession = 0;
    private SprotoRpc mClient = null;
    private SprotoRpc.RpcRequest mClientRequest = null;
    private SprotoRpc mService = null;
    private long session = 0;
    private AT mHandler;

    public void Register(AT del)
    {
        mHandler += del;
    }

    public void Start()
    {

    }

    public void Run(object o)
    {
        IPAddress ip = IPAddress.Parse("192.168.1.239");
        mSocket.Connect(new IPEndPoint(ip, 8888));
        mClient = new SprotoRpc();
        mClientRequest = mClient.Attach(C2sProtocol.Instance);
        mService = new SprotoRpc(S2cProtocol.Instance);

        session++;
        C2sSprotoType.set.request obj = new set.request();
        obj.what = "foo";
        byte[] req = mClientRequest.Invoke<C2sProtocol.set>(obj, session);

        //mcl = new SprotoRpc();
        //mRpc.Attach(s2c);

        //mRpc = SprotoRpc( SpRpc.Create (s2c, "package");
        //mRpc.Attach (c2s);

        //Send ("handshake", null);
        //Send ("set", new SpObject (SpObject.ArgType.Table, 
        //                           "what", "hello", 
        //                           "value", "world"));

        //mSocket.BeginReceive(mRecvBuffer, 0, mRecvBuffer.Length, SocketFlags.None, new AsyncCallback(ReadCallback), this);
    }

    public void Recv(IAsyncResult ar)
    {
        int ret = mSocket.EndReceive(ar);

        if (ret > 0)
        {
            mRecvOffset += ret;

            int read = 0;
            while (mRecvOffset > read)
            {
                int size = (mRecvBuffer[read + 1] | (mRecvBuffer[read + 0] << 8));

                read += 2;
                if (mRecvOffset >= size + read)
                {

                    SprotoStream stream = new SprotoStream();
                    //stream.Read()
                    //new SprotoStream( (mRecvBuffer, read, size, size);
                    //SpRpcResult result = mRpc.Dispatch (stream);
                    //switch (result.Op) {
                    //case SpRpcOp.Request:
                    //    Util.Log ("Recv Request : " + result.Protocol.Name + ", session : " + result.Session);
                    //    if (result.Arg != null)
                    //        Util.DumpObject (result.Arg);
                    //    break;
                    //case SpRpcOp.Response:
                    //    Util.Log ("Recv Response : " + result.Protocol.Name + ", session : " + result.Session);
                    //    if (result.Arg != null)
                    //        Util.DumpObject (result.Arg);
                    //    break;
                    //}

                    //read += size;
                }
            }
            //Util.Assert (mRecvOffset == read);
            mRecvOffset = 0;
        }

        mSocket.BeginReceive(mRecvBuffer, 0, mRecvBuffer.Length, SocketFlags.None, new System.AsyncCallback(ReadCallback), this);
    }

    public void ReadCallback(IAsyncResult ar)
    {
        mHandler();
        Client client = (Client)ar.AsyncState;
        client.Recv(ar);
    }

    private void Send(byte[] buf)
    {
        byte[] buffer = new byte[buf.Length + 2];
        short len = (short)buf.Length;
        buffer[0] = (byte)((len >> 8) & 0xFF);
        buffer[1] = (byte)((len & 0xff));
        Array.Copy(buf, 0, buffer, 2, buf.Length);
        mSocket.Send(buffer);
        IAsyncResult ar = mSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendAsyncCallback, null);
    }
 
    void SendAsyncCallback(IAsyncResult ar)
    {
    }
}
