using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Sproto;
using SprotoType;
using S2cSprotoType;
using C2sSprotoType;

public class ClientSockt : MonoBehaviour
{
    public delegate void RespAction(SprotoRpc.RpcInfo sinfo);

    private long Session = 0;

    private SprotoRpc Host = null;
    private SprotoRpc.RpcRequest Request = null;
    private SprotoStream mSendStream = new SprotoStream();

    private PackageSocket PSocket = new PackageSocket();
    private Dictionary<long, RespAction> Handler = new Dictionary<long, RespAction>();

    // Use this for initialization
    void Start()
    {

        Host = new SprotoRpc(S2cProtocol.Instance);
        Request = Host.Attach(C2sProtocol.Instance);

        AddressBook address = new AddressBook();
        address.person = new System.Collections.Generic.List<Person>();
        Person person = new Person();
        person.name = "Alice";
        person.id = 10000;
        person.phone = new System.Collections.Generic.List<Person.PhoneNumber>();
        Person.PhoneNumber num1 = new Person.PhoneNumber();
        num1.number = "1234567899";
        num1.type = 1;
        person.phone.Add(num1);
        address.person.Add(person);

        byte[] data = address.encode();  // encode to bytes
        Sproto.SprotoStream stream = new SprotoStream();
        address.encode(stream);          // encode t0 stream

        Sproto.SprotoPack spack = new SprotoPack();
        byte[] pack_data = spack.pack(data);

        byte[] unpack_data = spack.unpack(pack_data);
        AddressBook obj = new AddressBook(unpack_data);

        //var t = GameObject.Find("Text");
        //var text = t.GetComponent<Text>();
        //text.text = address.person[0].name;

        PSocket.Connect("192.168.1.116", 8888);
        PSocket.OnConnect = OnConnect;
        PSocket.OnRecvive = OnRecvive;
        PSocket.OnDisconnect = OnDisconnect;

    }

    // Update is called once per frame
    void Update()
    {
        PSocket.Update();
    }

    void OnConnect(bool connected)
    {
        if (!connected)
        {
            PSocket.Connect("192.168.1.239", 8888);
        }
    }

    void OnRecvive(byte[] data, int start, int length)
    {
        byte[] buffer = new byte[length];
        Array.Copy(data, start, buffer, 0, length);
        SprotoRpc.RpcInfo sinfo = Host.Dispatch(buffer);
        if (sinfo.type == SprotoRpc.RpcType.REQUEST)
        {
            Debug.Assert(sinfo.requestObj == null);
            Debug.Assert(sinfo.tag == S2cProtocol.heartbeat.Tag);
            Debug.Assert(sinfo.Response == null);
            Debug.Log("heartbeat");
        }
        else
        {
            Debug.Assert(sinfo.type == SprotoRpc.RpcType.RESPONSE);
            if (sinfo.session != null)
            {
                Handler[(long)sinfo.session](sinfo);
                Unregister((long)sinfo.session);
            }
        }
    }

    void OnDisconnect(SocketError socketError, PackageSocketError packageSocketError)
    {

    }

    /// <summary>
    /// 注册回调，发出请求的同时，同session的响应
    /// </summary>
    /// <param name="session"></param>
    /// <param name="action"></param>
    public void Register(long session, RespAction action)
    {
        Handler.Add(session, action);
    }

    public void Unregister(long session)
    {
        Handler.Remove(session);
    }

    void GetCallback(SprotoRpc.RpcInfo rinfo)
    {
        var resp = (C2sSprotoType.get.response)rinfo.responseObj;
        Debug.Log(resp.result);
    }

    void SendGet(List<String> keys)
    {
        foreach (var item in keys)
        {
            Session++;
            C2sSprotoType.get.request obj = new get.request();
            obj.what = item;
            byte[] req = this.Request.Invoke<C2sProtocol.get>(obj, Session);
            PSocket.Send(req, 0, req.Length);
            Register(Session, GetCallback);
        }
    }

    void SetCallback(SprotoRpc.RpcInfo rinfo)
    {
    }

    void SendSet(Dictionary<string, string> dict)
    {
        Debug.Assert(dict != null);
        foreach (var item in dict)
        {
            Session++;
            C2sSprotoType.set.request obj = new set.request();
            obj.what = item.Key;
            obj.value = item.Value;
            byte[] req = this.Request.Invoke<C2sProtocol.set>(obj, Session);
            Debug.Assert(req != null);
            PSocket.Send(req, 0, req.Length);
            Register(Session, SetCallback);
        }
    }

    void HandshakeCallback(SprotoRpc.RpcInfo rinfo)
    {
        var resp = (C2sSprotoType.handshake.response)rinfo.responseObj;
        Debug.Log(resp.msg);
    }

    void SendHandshake()
    {
        Session++;
        var req = Request.Invoke<C2sProtocol.handshake>(null, Session);
        Debug.Assert(req != null);
        PSocket.Send(req, 0, req.Length);
        Register(Session, HandshakeCallback);
    }

    public void OnClickHandshake()
    {
        SendHandshake();
    }

    public void OnClickSet()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string key = "hello";
        string value = "world";
        dict.Add(key, value);
        SendSet(dict);
    }

    public void OnClickGet()
    {
        List<string> l = new List<string>();
        l.Add("hu");
        SendGet(l);
    }

}
