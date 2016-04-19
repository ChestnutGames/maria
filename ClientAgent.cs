using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;
using Sproto;
using SprotoType;
using S2cSprotoType;
using C2sSprotoType;

public class ClientAgent : MonoBehaviour
{
    private string ip = "192.168.1.239";
    private int port = 8888;
    private ClientSocket sock;
    private byte[] secret = null;
    private byte[] subid = null;

    private int step = 0;

    // Use this for initialization
    void Start()
    {
        sock = new ClientSocket(ip, port);
        sock.Start();
    }

    // Update is called once per frame
    void Update()
    {
        sock.Update();
    }

    private void Handshake()
    {
        int index = 1;
        string str = String.Format("{0}@{1}#{2}:{3}", Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("hello"))),
            Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes("sample"))),
            Encoding.ASCII.GetString(Crypt.base64encode(subid)), index);
        byte[] hmac = Crypt.hmac64(Crypt.hashkey(Encoding.ASCII.GetBytes(str)), secret);
        C2sSprotoType.handshake.request requestObj = new handshake.request();
        //requestObj.secret = secret;
        sock.Handshake(this, requestObj, null);
    }

    public void StartConnServer(byte[] s, byte[] si)
    {
        secret = s;
        subid = si;
        step = 1;
    }

}
