using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

public class ClientLogin : MonoBehaviour
{
    private PackageSocket sock = new PackageSocket();
    private Dictionary<string, string> Token = new Dictionary<string, string>();
    private byte[] challenge = null;
    private byte[] clientkey = null;
    private byte[] secret = null;
    private byte[] subid = null;
    private int step = 0;
    private bool Connected = false;

    // Use this for initialization
    void Start()
    {
        sock.OnConnect = OnConnect;
        sock.OnDisconnect = OnDisconnect;
        sock.OnRecvive = OnRecvive;
        sock.SetEnabledPing(false);
        string ip = "192.168.1.239";
        int port = 8001;
        sock.Connect(ip, port);
    }

    // Update is called once per frame
    void Update()
    {
        sock.Update();
        sock.ProcessLine();
    }

    void OnConnect(bool connected)
    {
    }

    void OnRecvive(byte[] data, int start, int length)
    {
        byte[] buffer = new byte[length];
        Array.Copy(data, start, buffer, 0, length);
        if (step == 1)
        {
            challenge = Crypt.base64decode(buffer);
            clientkey = Crypt.randomkey();
            var buf = Crypt.base64encode(Crypt.dhexchange(clientkey));
            sock.SendLine(buf, 0, buf.Length);
            step = 2;
            return;
        }
        else if (step == 2)
        {
            byte[] key = Crypt.base64decode(buffer);
            secret = Crypt.dhsecret(key, clientkey);
            Debug.Log("sceret is " + Encoding.ASCII.GetString(Crypt.hexencode(secret)));
            byte[] hmac = Crypt.hmac64(challenge, secret);
            var buf = Crypt.base64encode(hmac);
            sock.SendLine(buf, 0, buf.Length);
            WriteToke();
            step = 3;
            return;
        }
        else if (step == 3)
        {
            string str = Encoding.ASCII.GetString(buffer);
            int code = Int32.Parse(str.Substring(0, 3));
            Debug.Assert(code == 200);
            sock.Close();
            string en = str.Substring(4, 4);
            subid = Crypt.base64decode(Encoding.ASCII.GetBytes(en));
            step = 4;
        }
        else if (step == 4)
        {
            var agent = GameObject.Find("Agent").GetComponent<ClientAgent>();
            agent.StartConnServer(secret, subid);
            step = 0;
        }
    }

    void OnDisconnect(SocketError socketError, PackageSocketError packageSocketError)
    {
    }

    public void WriteToke()
    {
        string str = String.Format("{0}@{1}:{2}", Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("hello"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("sample"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("password"))));
        Debug.Log(str);
        byte[] etoken = Crypt.desencode(secret, Encoding.ASCII.GetBytes(str));
        byte[] b = Crypt.base64encode(etoken);
        sock.SendLine(b, 0, b.Length);
    }
}
