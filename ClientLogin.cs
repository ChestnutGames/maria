using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

public class ClientLogin : MonoBehaviour
{

    private Socket Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    private Dictionary<string, string> Token = new Dictionary<string, string>();
    private string last = "";
    private Queue<string> Lines = new Queue<string>();
    private byte[] challenge = null;
    private byte[] clientkey = null;
    private byte[] secret = null;
    private byte[] subid = null;
    private int step = 0;
    private byte[] recvBuffer = new byte[1024];

    // Use this for initialization
    void Start()
    {
        Dictionary<string, string> conf = new Dictionary<string, string>();
        string IP = "192.168.1.239";
        IPAddress ip = IPAddress.Parse(IP);
        Sock.BeginConnect(new IPEndPoint(ip, 8001), ConnectAC, this);
        Token["server"] = "sample";
        Token["user"] = "hello";
        Token["pass"] = "password";
    }

    // Update is called once per frame
    void Update()
    {

        if (step == 1)
        {
            Sock.BeginReceive(recvBuffer, 0, 1024, SocketFlags.None, RecvChanlenge, this);
            step = 0;
        }
        if (step == 2)
        {
            Sock.BeginReceive(recvBuffer, 0, 1024, SocketFlags.None, RecvSecret, this);
            step = 0;
        }
        else if (step == 3)
        {
            Sock.BeginReceive(recvBuffer, 0, 1024, SocketFlags.None, RecvResult, this);
            step = 0;
        }
        else if (step == 4)
        {
            var ca = GameObject.Find("Agent").GetComponent<ClientAgent>();
            ca.StartConnServer(secret, subid);
            step = 0;
        }
    }

    private void ConnectAC(IAsyncResult ar)
    {
        Sock.EndConnect(ar);
        step = 1;
    }

    private void RecvChanlenge(IAsyncResult ar)
    {
        Sock.EndReceive(ar);
        int i = 0;
        for (; i < recvBuffer.Length; i++)
        {
            if (recvBuffer[i] == 10)
            {
                break;
            }
        }
        byte[] buffer = new byte[i];
        Array.Copy(recvBuffer, buffer, i);
        recvBuffer = new byte[1024];
        challenge = Crypt.base64decode(buffer);
        clientkey = Crypt.randomkey();
        string rt = Encoding.ASCII.GetString(Crypt.base64encode(Crypt.dhexchange(clientkey)));
        WriteLine(rt);
        step = 2;
    }

    private void RecvSecret(IAsyncResult ar)
    {
        Sock.EndReceive(ar);
        int i = 0;
        for (; i < recvBuffer.Length; i++)
        {
            if (recvBuffer[i] == 10)
            {
                break;
            }
        }
        byte[] buffer = new byte[i];
        Array.Copy(recvBuffer, buffer, i);
        recvBuffer = new byte[1024];

        byte[] key = Crypt.base64decode(buffer);
        secret = Crypt.dhsecret(key, clientkey);
        Debug.Log("sceret is " + Encoding.ASCII.GetString(Crypt.hexencode(secret)));
        byte[] hmac = Crypt.hmac64(challenge, secret);
        WriteLine(Encoding.ASCII.GetString(Crypt.base64encode(hmac)));
        Dictionary<string, string> token = new Dictionary<string, string>();
        token["server"] = "sample";
        token["hello"] = "hello";
        token["pass"] = "password";
        WriteToke(token);
        step = 3;
    }

    private void RecvResult(IAsyncResult ar)
    {
        Sock.EndReceive(ar);
        string str = Encoding.ASCII.GetString(recvBuffer);
        recvBuffer = new byte[1024];
        int code = Int32.Parse(str.Substring(0, 3));
        Debug.Assert(code == 200);
        Sock.Close();
        string en = str.Substring(4, 4);
        subid = Crypt.base64decode(Encoding.ASCII.GetBytes(en));
        step = 4;
    }

    public void WriteToke(Dictionary<string, string> token)
    {
        string str = String.Format("{0}@{1}:{2}", Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("hello"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("sample"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("password"))));
        Debug.Log(str);
        byte[] etoken = Crypt.desencode(secret, Encoding.ASCII.GetBytes(str));
        byte[] b = Crypt.base64encode(etoken);
        WriteLine(Encoding.ASCII.GetString(b));
    }

    private void WriteLine(string text)
    {
        text += "\n";
        byte[] buffer = Encoding.ASCII.GetBytes(text);
        Send(buffer);
    }

    private void WriteLineB(byte[] text)
    {
        byte[] buffer = new byte[text.Length + 1];
        Array.Copy(text, buffer, text.Length);
        buffer.SetValue(10, text.Length);
        Send(buffer);
    }

    private void Send(byte[] buffer)
    {
        Sock.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendAR, this);
    }

    private void SendAR(IAsyncResult ar)
    {
        Sock.EndSend(ar);
    }

}
