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
    private byte[] Last = null;
    private byte[] RecvBuffer = new byte[1024];
    private int RecvBufferOffset = 0;
    private int RecvBufferSz = 0;
    private byte[] challenge = null;
    private byte[] clientkey = null;
    private byte[] secret = null;
    private byte[] subid = null;
    private int step = 0;
    private bool Connected = false;

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
        if (!Connected)
        {
            return;
        }
        if (Sock.Poll(0, SelectMode.SelectRead))
        {
            int ret = Sock.Receive(RecvBuffer, RecvBufferSz, 1024 - RecvBufferSz, SocketFlags.None);
            RecvBufferSz += ret;
        }
        if (RecvBufferOffset < RecvBufferSz && RecvBufferSz < 1024)
        {
            if (step == 1)
            {

                byte[] buffer = ReadLine();
                challenge = Crypt.base64decode(buffer);
                clientkey = Crypt.randomkey();
                WriteLine(Crypt.base64encode(Crypt.dhexchange(clientkey)));
                step = 2;
                return;
            }
            else if (step == 2)
            {
                byte[] buffer = ReadLine();

                byte[] key = Crypt.base64decode(buffer);
                secret = Crypt.dhsecret(key, clientkey);
                Debug.Log("sceret is " + Encoding.ASCII.GetString(Crypt.hexencode(secret)));
                byte[] hmac = Crypt.hmac64(challenge, secret);
                WriteLine(Crypt.base64encode(hmac));
                WriteToke();
                step = 3;
                return;
            }
            else if (step == 3)
            {
                byte[] buffer = ReadLine();
                string str = Encoding.ASCII.GetString(buffer);
                int code = Int32.Parse(str.Substring(0, 3));
                Debug.Assert(code == 200);
                Sock.Close();
                string en = str.Substring(4, 4);
                subid = Crypt.base64decode(Encoding.ASCII.GetBytes(en));
                step = 4;
            }
            else if (step == 4)
            {
                var com = GameObject.Find("Agent").GetComponent<ClientAgent>();
                com.StartConnServer(secret, subid);
                step = 0;
            }
        }
        else
        {
            if (RecvBufferSz > 0)
            {
                Array.Copy(RecvBuffer, RecvBufferOffset, RecvBuffer, 0, RecvBufferSz);
            }
        }
    }

    void ConnectAC(IAsyncResult ar)
    {
        Sock.EndConnect(ar);
        Connected = true;
    }

    public void WriteToke()
    {
        string str = String.Format("{0}@{1}:{2}", Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("hello"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("sample"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("password"))));
        Debug.Log(str);
        byte[] etoken = Crypt.desencode(secret, Encoding.ASCII.GetBytes(str));
        byte[] b = Crypt.base64encode(etoken);
        WriteLine(b);
    }

    private byte[] ReadLine()
    {
        int i = 0;
        for (i = RecvBufferOffset; i < RecvBufferSz; i++)
        {
            if (RecvBuffer[i] == 10)
            {
                break;
            }
        }
        byte[] buffer = new byte[i - RecvBufferOffset];
        Array.Copy(RecvBuffer, RecvBufferOffset, buffer, 0, i - RecvBufferOffset);
        RecvBufferOffset += (i - RecvBufferOffset);
        return buffer;
    }

    private void WriteLine(byte[] text)
    {
        string t = Encoding.ASCII.GetString(text);
        t += "\n";
        byte[] buffer = Encoding.ASCII.GetBytes(t);
        Sock.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendAR, this);
    }

    private void SendAR(IAsyncResult ar)
    {
        Sock.EndSend(ar);
    }

}
