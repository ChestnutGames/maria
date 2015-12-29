using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;

public class ClientAgent : MonoBehaviour
{

    private Socket Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private byte[] recvBuffer = new byte[1024];
    private byte[] secret = null;
    private byte[] subid = null;

    private int step = 0;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (step == 1)
        {
            string IP = "192.168.1.239";
            IPAddress ip = IPAddress.Parse(IP);
            Sock.BeginConnect(new IPEndPoint(ip, 8888), ConnectAC, this);
            step = 0;
        }
        else if (step == 2)
        {
            Handshake();
            step = 3;
        }
        else if (step == 3)
        {
            Sock.BeginReceive(recvBuffer, 0, 1024, SocketFlags.None, RecvAC, this);
            string text = "echo";
            int session = 0;
            SendReq(Encoding.ASCII.GetBytes(text), session);
            step = 4;
        }
    }

    private void ConnectAC(IAsyncResult ar)
    {
        Sock.EndConnect(ar);
        step = 2;
    }

    private void RecvAC(IAsyncResult ar)
    {
        Sock.EndReceive(ar);
        if (step == 4)
        {
            Debug.Log(UnpackPackage(recvBuffer));
        }
    }

    private void SendReq(byte[] buffer, int session)
    {
        ushort size = (ushort)(buffer.Length + 4);
        byte[] b = new byte[size];
        b[0] = (byte)(size >> 8 & 0xff);
        b[1] = (byte)(size & 0xff);
        Array.Copy(buffer, 0, b, 2, buffer.Length);
        int idx = buffer.Length;
        b[idx++] = (byte)(session >> 24 & 0xff);
        b[idx++] = (byte)(session >> 16 & 0xff);
        b[idx++] = (byte)(session >> 8 & 0xff);
        b[idx++] = (byte)(session & 0xff);
        Sock.BeginSend(b, 0, size, SocketFlags.None, SendAC, this);
    }

    private void SendPackage(string str)
    {
        ushort size = (ushort)(str.Length);
        byte[] b = new byte[size + 2];
        b[0] = (byte)(size >> 8 & 0xff);
        b[1] = (byte)(size & 0xff);
        Array.Copy(Encoding.ASCII.GetBytes(str), 0, b, 2, str.Length);
        Sock.BeginSend(b, 0, size, SocketFlags.None, SendAC, this);
    }

    private void SendAC(IAsyncResult ar)
    {
        Sock.EndSend(ar);
    }

    private void RecvResp(byte[] buffer)
    {
        int size = buffer.Length - 5;
    }

    private string UnpackPackage(byte[] buffer)
    {
        if (buffer.Length < 2)
        {
            throw new Exception("");
        }
        ushort size = 0;
        size |= (ushort)(buffer[0] << 8 & 0xffff);
        size |= (ushort)(buffer[1] & 0xffff);
        if (buffer.Length < size + 2)
        {
            return "";
        }
        return Encoding.ASCII.GetString(buffer, 3, size);
    }

    private void Handshake()
    {
        int index = 1;
        string str = String.Format("{0}@{1}#{2}:{3}", Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("hello"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("sample"))),
            Encoding.ASCII.GetString(Crypt.base64encode(subid)), index);
        byte[] hmac = Crypt.hmac64(Crypt.hashkey(Encoding.ASCII.GetBytes(str)), secret);

        SendPackage(str + Encoding.ASCII.GetString(Crypt.base64encode(hmac)));
        step = 3;
    }

    public void StartConnServer(byte[] s, byte[] si)
    {
        secret = s;
        subid = si;
        step = 1;
    }
}
