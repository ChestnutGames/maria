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

public class ClientSockt : MonoBehaviour, ISNDelegate
{
    public delegate void RpcAction(SprotoRpc.RpcInfo rinfo);

    private SNSocket SS = null;
    private float delta = 0;
    private long Session = 0;
    private List<ISNDelegate> Delegates = new List<ISNDelegate>();
    private SprotoRpc Host = null;
    private SprotoRpc.RpcRequest Request = null;
    private SprotoStream mSendStream = new SprotoStream();
    private Dictionary<long, RpcAction> mHandler = new Dictionary<long, RpcAction>();

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

        var t = GameObject.Find("Text");
        var text = t.GetComponent<Text>();
        text.text = address.person[0].name;

        Dictionary<string, string> conf = new Dictionary<string, string>();
        conf["IP"] = "192.168.1.239";
        conf["PORT"] = "8888";
        SS = new SNSocket(conf);
        SS.setDelegate(this);
        //SS.Start();
    }

    // Update is called once per frame
    void Update()
    {
        //delta += Time.deltaTime;
        //if (delta > 5)
        //{
        //    delta = 0;
        //    SS.update();
        //}
    }

    public void Handshake()
    {
        Dictionary<string, string> token = new Dictionary<string, string>();
        token["server"] = "sample";
        token["user"] = "hello";
        token["password"] = "password";
        //string hk = String.Format("{0}@{1}#{2}:{3}", Crypt.base64encode(token["user"]), Crypt.base64encode(token["server"]), Crypt.base64encode(s))
    }

    /// <summary>
    /// 注册回调，发出请求的同时，同session的响应
    /// </summary>
    /// <param name="session"></param>
    /// <param name="action"></param>
    public void Register(long session, RpcAction action)
    {
        mHandler.Add(session, action);
    }

    public void Unregister(long session)
    {
        mHandler.Remove(session);
    }

    public void GetCallback(SprotoRpc.RpcInfo rinfo)
    {
        var resp = (C2sSprotoType.get.response)rinfo.responseObj;
        Debug.Log(resp.result);
    }

    public void SendGet(List<String> keys)
    {
        foreach (var item in keys)
        {
            Session++;
            C2sSprotoType.get.request obj = new get.request();
            obj.what = item;
            byte[] req = this.Request.Invoke<C2sProtocol.get>(obj, Session);
            Send(req);
            Register(Session, GetCallback);
        }
    }

    public void SetCallback(SprotoRpc.RpcInfo rinfo)
    {
    }

    public void SendSet(Dictionary<string, string> dict)
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
            Send(req);
            Register(Session, SetCallback);
        }
    }

    public void HandshakeCallback(SprotoRpc.RpcInfo rinfo)
    {
        var resp = (C2sSprotoType.handshake.response)rinfo.responseObj;
        Debug.Log(resp.msg);
    }

    public void SendHandshake()
    {
        Session++;
        var req = Request.Invoke<C2sProtocol.handshake>(null, Session);
        Debug.Assert(req != null);
        Send(req);
        Register(Session, HandshakeCallback);
    }

    public void Send(byte[] buffer)
    {
        SS.Send(buffer);
    }

    public void OnClickSet()
    {
        SendHandshake();
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string key = "hello";
        string value = "world";
        dict.Add(key, value);
        SendSet(dict);
        Debug.Log("onclickset");
    }

    public void OnClickGet()
    {
        List<string> l = new List<string>();
        l.Add("hu");
        SendGet(l);
        Debug.Log("onclickget");
    }

    public void AddDelegate(ISNDelegate d)
    {
        Delegates.Add(d);
    }

    public void RemoveDelegate(ISNDelegate d)
    {
        Delegates.Remove(d);
    }

    public void OnConnected(SNSocket s)
    {
        foreach (var item in Delegates)
        {
            item.OnConnected(s);
        }
    }

    public void OnMessage(SNSocket s, byte[] buffer)
    {
        SprotoRpc.RpcInfo sinfo = Host.Dispatch(buffer);
        if (sinfo.type == SprotoRpc.RpcType.REQUEST)
        {
            Debug.Log("handshake");
            if (sinfo.session != null)
            {
                mHandler[(long)sinfo.session](sinfo);
                //mHandler.Remove((long)sinfo.session);
            }
        }
        else
        {
            Debug.Assert(sinfo.type == SprotoRpc.RpcType.RESPONSE);
            if (sinfo.session != null)
            {
                mHandler[(long)sinfo.session](sinfo);
                Unregister((long)sinfo.session);
            }
        }
    }

    public void OnError(SNSocket s, SocketError errorCode, string msg)
    {
        foreach (var item in Delegates)
        {
            item.OnError(s, errorCode, msg);
        }
    }

    public void OnClose(SNSocket s)
    {
        foreach (var item in Delegates)
        {
            item.OnClose(s);
        }
    }
}
