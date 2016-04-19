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

public class ClientSocket : MonoBehaviour
{
    public delegate void CB(bool ok, object ud, byte[] subid, byte[] secret);

    public delegate void RespAction(uint session, SprotoTypeBase responseObj);
    public delegate void ReqAction(uint session, SprotoTypeBase requestObj, SprotoRpc.ResponseFunction Response);

    private class ReqPg
    {
        public uint Session { get; set; }
        public string Protocol { get; set; }
        public byte[] Buffer { get; set; }
        public int Version { get; set; }
        public int Index { get; set; }
    }

    private class RespPg
    {
        public uint Session { get; set; }
        public string Protocol { get; set; }
        public byte[] Buffer { get; set; }
        public int Version { get; set; }
        public int Index { get; set; }
    }

    private PackageSocket sock = new PackageSocket();
    private string ip = "192.168.1.239";
    private int port = 8888;
    private User user = null;
    private int step = 0;
    private bool begain = false;
    private object ud = null;
    private CB callback = null;

    private uint index = 1;
    private uint session = 0;
    private SprotoRpc host = null;
    private SprotoRpc.RpcRequest send_request = null;
    //private SprotoStream stream = new SprotoStream();
    private bool handshake = false;
    private const int c2s_req_tag = (7 & (1 << 2) & (1 << 4));
    private const int c2s_resp_tag = (7 & (1 << 2) & (0 << 4));
    private const int s2c_req_tag = (7 & (0 << 2) & (1 << 4));
    private const int s2c_resp_tag = (7 & (0 << 2) & (0 << 4));
    private const int auth_tag = 1;

    private Dictionary<string, RespAction> response = new Dictionary<string, RespAction>();
    private Dictionary<string, ReqAction> request = new Dictionary<string, ReqAction>();
    private Dictionary<string, ReqPg> request_pg = new Dictionary<string, ReqPg>();      /*protocal -> pg */
    private Dictionary<string, RespPg> response_pg = new Dictionary<string, RespPg>();   /*protocal -> pg */


    // Use this for initialization
    public void Start()
    {
        host = new SprotoRpc(S2cProtocol.Instance);
        send_request = host.Attach(C2sProtocol.Instance);
        sock.OnConnect = OnConnect;
        sock.OnRecvive = OnRecvive;
        sock.OnDisconnect = OnDisconnect;
        sock.SetEnabledPing(true);

    }

    // Update is called once per frame
    public void Update()
    {
        sock.Update();
        sock.ProcessPackage();
    }

    public void OnConnect(bool connected)
    {
        if (connected)
        {
            if (begain)
            {
                step++;
            }
        }
        else
        {
            Auth(ip, port, user, ud, null);
        }
    }

    void OnRecvive(byte[] data, int start, int length)
    {
        uint session = 0;
        byte tag = 1;
        byte[] buffer = new byte[length - 5];
        Array.Copy(data, start, buffer, 0, length - 5);
        if (auth_tag == tag)
        {
            Debug.Assert(begain);
            if (step == 1)
            {
                byte[] handshake = WriteToke();
                byte[] hmac = Crypt.hmac64(Crypt.hashkey(handshake), user.Secret);
                byte[] tmp = new byte[handshake.Length + hmac.Length + 1];
                Array.Copy(handshake, 0, tmp, 0, handshake.Length);
                tmp[handshake.Length] = Encoding.ASCII.GetBytes(":")[0];
                Array.Copy(hmac, 0, tmp, handshake.Length+1, hmac.Length);
                sock.Send(tmp, 0, tmp.Length);
                //Wirte(, session, auth_tag);
                step++;
            }
            else if (step == 2)
            {
                begain = false;
                handshake = true;
            }
        }
        else
        {
            if (tag == c2s_resp_tag)
            {
                SprotoRpc.RpcInfo sinfo = host.Dispatch(buffer);
                Debug.Assert(sinfo.type == SprotoRpc.RpcType.RESPONSE);
                Debug.Assert(sinfo.session != null);
                Debug.Assert(sinfo.session == session);
                string key = id_to_hex(session);
                var rpc = response[key];
                rpc.Action(rpc.Ud, session, sinfo.responseObj);
            }
            else if (tag == s2c_req_tag)
            {
                SprotoRpc.RpcInfo sinfo = host.Dispatch(buffer);
                Debug.Assert(sinfo.type == SprotoRpc.RpcType.REQUEST);
                Debug.Assert(sinfo.session != null);
                Debug.Assert(sinfo.session == session);
                Debug.Assert(sinfo.tag != null);
                string key = id_to_hex((uint)sinfo.tag);
                var rpc = request[key];
                Debug.Assert(rpc != null);
                rpc.Session = session;
                byte[] resp = rpc.Action(rpc.Ud, session, sinfo.requestObj);
                byte[] tmp = new byte[resp.Length + 5];
                sock.Send(tmp, 0, tmp.Length);
            }
        }
    }

    void OnDisconnect(SocketError socketError, PackageSocketError packageSocketError)
    {
    }

    private byte[] WriteToke()
    {
        string u = Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes(user.Account)));
        string s = Encoding.ASCII.GetString(Crypt.base64encode(Encoding.ASCII.GetBytes(user.Server)));
        string sid = Encoding.ASCII.GetString(Crypt.base64encode(user.Subid));
        string handshake = string.Format("%s@%s#%s:%d", u, s, sid, index);
        return Encoding.ASCII.GetBytes(handshake);
    }

    private uint B2L(byte[] buffer, int start, int length)
    {
        uint r = 0;
        for (int i = 0; i < length; i++)
        {
            int idx = start + i;
            int b = buffer[idx];
        }
        return r;
    }

    private void Wirte(byte[] buffer, uint session, byte tag)
    {
        int l = buffer.Length + 5;
        byte[] tmp = new byte[l];
        Array.Copy(buffer, tmp, buffer.Length);
        byte[] s = new byte[4];
        for (int i = 0; i < 4; i++)
        {
            s[i] = (byte)(session >> (8 * (3 - i)) & 0xff);
        }
        Array.Copy(s, 0, tmp, buffer.Length, 4);
        tmp[l - 1] = tag;
        sock.Send(tmp, 0, l);
    }

    private void Send(object ud, uint id, byte[] buffer, RespAction cb)
    {
        RespPg pg = new RespPg();
        pg.Action = cb;
        pg.Buffer = buffer;
        pg.Session = id;
        string key = id_to_hex(id);
        response[key] = pg;
        sock.Send(pg.Buffer, 0, pg.Buffer.Length);
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

    public void Auth(string ipstr, int pt, User u, object d, CB cb)
    {
        ip = ipstr;
        port = pt;
        user = u;
        ud = u;
        callback = cb;
        begain = true;
        sock.Connect(ip, port);
    }

    public void Reset()
    {
        ip = null;
        port = 0;
        User user = null;
        step = 0;
        begain = false;
        ud = null;
        callback = null;
    }

    private void RegisterProtocol()
    {
        request["handshake"] = HandshakeReq;
    }

    public void Handshake()
    {
        SprotoTypeBase requestObj = null;
        byte[] req = null;
        uint id = genSession();
        if (requestObj != null)
        {
            req = send_request.Invoke<C2sProtocol.handshake>(requestObj, id);
            Debug.Assert(req != null);
        }
        Send(ud, id, req, );
    }

    public void HandshakeResp(object ud, SprotoTypeBase responseObj)
    {
        var resp = (C2sSprotoType.handshake.response)responseObj;
        Debug.Log(resp.msg);
    }

    public void HandshakeDispatch(object ud, ReqAction cb)
    {
        long id = (long)S2cProtocol.heartbeat.Tag;
        ReqPg pg = new ReqPg();
        pg.Ud = ud;
        pg.Session = 0;
        pg.Action = cb;
        string key = id_to_hex((uint)id);
        request[key] = pg;
    }

    public void HandshakeReq(uint session, SprotoTypeBase requestObj, SprotoRpc.ResponseFunction Response)
    {

        //send_request.Invoke<S2cProtocol.heartbeat>()

        //return null;
    }
}
