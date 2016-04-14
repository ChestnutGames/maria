using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using Sproto;
using SprotoType;
using S2cSprotoType;
using C2sSprotoType;
using System.Net.Sockets;

public class ClientSocket
{

    public delegate void RespAction(object ud, SprotoTypeBase responseObj);
    public delegate SprotoTypeBase ReqAction(object ud, SprotoTypeBase requestObj);

    private struct RespCB
    {
        public object ud;
        public RespAction action;
        public uint version;
        public uint index;
    }

    private struct ReqCB
    {
        public object ud;
        public ReqAction action;
        public uint version;
        public uint index;
    }

    private string ip = "192.168.1.239";
    private int port = 8888;
    private uint session = 0;
    private SprotoRpc host = null;
    private SprotoRpc.RpcRequest send_request = null;
    private SprotoStream stream = new SprotoStream();
    private PackageSocket sock = new PackageSocket();
    private Dictionary<string, ReqCB> request = new Dictionary<string, ReqCB>();
    private Dictionary<string, RespCB> response = new Dictionary<string, RespCB>();
    private bool handshake = true;

    //public User user;

    private byte[] secret = null;
    private byte[] subid = null;

    public ClientSocket(string ipstr, int p)
    {
        ip = ipstr;
        port = p;
        host = new SprotoRpc(S2cProtocol.Instance);
        send_request = host.Attach(C2sProtocol.Instance);
        sock.OnConnect = OnConnect;
        sock.OnRecvive = OnRecvive;
        sock.OnDisconnect = OnDisconnect;
    }

    // Use this for initialization
    public void Start()
    {
        sock.Connect("192.168.1.116", 8888);
    }

    // Update is called once per frame
    public void Update()
    {
        if (sock != null)
        {
            sock.Update();
        }
    }

    public void OnConnect(bool connected)
    {
        if (!connected)
        {
            sock.Connect(ip, port);
        }
    }

    void OnRecvive(byte[] data, int start, int length)
    {
        byte[] buffer = new byte[length];
        Array.Copy(data, start, buffer, 0, length);
        SprotoRpc.RpcInfo sinfo = host.Dispatch(buffer);
        if (sinfo.type == SprotoRpc.RpcType.REQUEST)
        {
            //Debug.Assert(sinfo.tag != null);
            //Debug.Assert(sinfo.tag != null);
            long id = (long)sinfo.tag;
            string key = id_to_hex(id);
            //Debug.Assert(request[key]);
            var rpc = request[key];
            var resp =  rpc.action(rpc.ud, sinfo.requestObj);
            Debug.Assert(resp != null);
            byte[] buf = sinfo.Response(resp);
            sock.Send(buffer, 0, buffer.Length);
        }
        else
        {
            Debug.Assert(sinfo.type == SprotoRpc.RpcType.RESPONSE);
            Debug.Assert(sinfo.session != null);
            string key = id_to_hex((long)sinfo.session);
            var rpc = response[key];
            rpc.action(rpc.ud, sinfo.responseObj);
            Debug.Assert(response.Remove(key));
        }
    }

    void OnDisconnect(SocketError socketError, PackageSocketError packageSocketError)
    {

    }

    protected void Dispatch(object ud, long id, ReqAction cb)
    {
        string key = id_to_hex(id);
        var rpc = new ReqCB();
        rpc.ud = ud;
        rpc.action = cb;
        request[key] = rpc;
    }

    private void send_request(byte[] v, uint session)
    {

    }

    protected void Send(object ud, uint id, byte[] buf, RespAction cb)
    {
        string key = id_to_hex(id);
        var rpc = new RespCB();
        rpc.ud = ud;
        rpc.action = cb;

        response[key] = rpc;
        sock.Send(buf, 0, buf.Length);
    }

    private uint genSession()
    {
        ++session;
        if (session == 0)
        {
            session = 1;
            return session;
        }
        return session;
    }

    private string id_to_hex(uint id)
    {
        byte[] tmp = new byte[9];
        byte[] hex = new byte[16] { 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 65, 66, 67, 68, 69, 70 };
        tmp[0] = 58;
        for (int i = 0; i < 16; i++)
        {
            tmp[i + 1] = hex[(id >> ((7 - i) * 4)) & 0xf];
        }
        return Encoding.ASCII.GetString(tmp);
    }

    public void Handshake(object ud, SprotoTypeBase requestObj, RespAction cb)
    {
        long id = genSession();
        var req = send_request.Invoke<C2sProtocol.handshake>(requestObj, id);
        Debug.Assert(req != null);
        Send(ud, id, req, HandshakeResp);
    }

    public void HandshakeResp(object ud, SprotoTypeBase responseObj)
    {
        var resp = (C2sSprotoType.handshake.response)responseObj;
        Debug.Log(resp.msg);
    }

    public void HandshakeDispatch(object ud, ReqAction cb)
    {
        long id = (long)S2cProtocol.heartbeat.Tag;
        Dispatch(ud, id, HandshakeReq);
    }

    public SprotoTypeBase HandshakeReq(object ud, SprotoTypeBase requestObj)
    {
        return null;
    }

}
