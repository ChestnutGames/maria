﻿using UnityEngine;
using System.Collections.Generic;
using System.Threading;
using Maria.Network;
using System;
using Sproto;
using System.Text;

namespace Maria {
    public class Context {
        private class Timer {
            public string Name { get; set; }
            public float CD { get; set; }
            public CountdownCb CB { get; set; }
        }

        public delegate void CountdownCb();

        protected global::App _app;
        protected Application _application;

        protected EventDispatcher _dispatcher = null;
        protected Dictionary<string, Controller> _hash = new Dictionary<string, Controller>();
        protected Stack<Controller> _stack = new Stack<Controller>();
        protected ClientLogin _login = null;
        protected ClientSocket _client = null;
        protected Gate _gate = null;
        protected User _user = new User();
        private ClientSocket.CB _authcb;
        private Dictionary<string, Timer> _timer = new Dictionary<string, Timer>();
        protected bool _authtcp = false;
        protected bool _authudp = false;
        protected Config _config = null;
        protected TimeSync _ts = null;

        public Context(global::App app, Maria.Application application) {
            _app = app;
            _application = application;

            //var go = GameObject.Find("/Assets");
            //Assets = go;

            _dispatcher = new EventDispatcher(this);

            _gate = new Gate(this);

            _login = new ClientLogin(this);
            _client = new ClientSocket(this);

            _hash["start"] = new StartController(this);
            _hash["login"] = new LoginController(this);

            //_hash["start"].Run();

            _config = new Config();
            _ts = new TimeSync();

        }

        // Use this for initialization
        public void Start() {
        }

        // Update is called once per frame
        public virtual void Update(float delta) {
            _login.Update();
            _client.Update();

            foreach (var item in _timer) {
                Timer tm = item.Value as Timer;
                if (tm != null) {
                    if (tm.CD > 0) {
                        //Debug.Log(tm.CD);
                        tm.CD -= delta;
                        if (tm.CD < 0) {
                            tm.CB();

                            //_timer.Remove()
                            //_timer.Remove(tm.Name);
                        }
                    } else {
                        //_timer.Remove(tm.Name);
                    }
                }
            }

            if (_stack.Count > 0) {
                Controller controller = Top();
                if (controller != null) {
                    controller.Update(delta);
                }
            }
        }

        public EventDispatcher EventDispatcher { get { return _dispatcher; } set { _dispatcher = value; } }

        public Config Config { get { return _config; } set { _config = value; } }

        public TimeSync TiSync { get { return _ts; } set { _ts = value; } }

        public GameObject Assets { get; set; }

        public User U { get { return _user; } }

        public long Session { get; set; }

        public T GetController<T>(string name) where T : Controller {
            try {
                if (_hash.ContainsKey(name)) {
                    Controller controller = _hash[name];
                    return controller as T;
                } else {
                    Debug.LogError(string.Format("{0} no't exitstence", name));
                    return null;
                }
            } catch (KeyNotFoundException ex) {
                Debug.LogError(ex.Message);
                return null;
            }
        }

        public void SendReq<T>(String callback, SprotoTypeBase obj) {
            _client.SendReq<T>(callback, obj);
        }

        public void AuthLogin(string s, string u, string pwd, ClientSocket.CB cb) {
            _authtcp = false;
            _authcb = cb;

            _user.Server = s;
            _user.Username = u;
            _user.Password = pwd;
            string ip = Config.LoginIp;
            int port = Config.LoginPort;
            _login.Auth(ip, port, s, u, pwd, AuthLoginCb);
        }

        public void AuthLoginCb(bool ok, byte[] secret, string dummy) {
            if (ok) {
                int _1 = dummy.IndexOf('#');
                int _2 = dummy.IndexOf('@', _1);
                int _3 = dummy.IndexOf(':', _2);

                byte[] uid = Encoding.ASCII.GetBytes(dummy.Substring(0, _1));
                byte[] sid = Encoding.ASCII.GetBytes(dummy.Substring(_1 + 1, _2 - _1 - 1));
                string gip = dummy.Substring(_2 + 1, _3 - _2 - 1);
                int gpt = Int32.Parse(dummy.Substring(_3 + 1));

                Debug.Log(string.Format("uid: {0}, sid: {1}", uid, sid));
                Debug.Log("login");

                _user.Secret = secret;
                _user.Uid = uid;
                _user.Subid = sid;

                _client.Auth(Config.GateIp, Config.GatePort, _user, AuthGateCB);
            } else {
            }
        }

        public void AuthGate(ClientSocket.CB cb) {
            _authcb = cb;
            _client.Auth(Config.GateIp, Config.GatePort, _user, AuthGateCB);
        }

        public void AuthGateCB(int ok) {
            if (ok == 200) {
                _authtcp = true;
                string dummy = string.Empty;
                foreach (var item in _hash) {
                    item.Value.AuthGateCB(ok);
                }
            } else if (ok == 403) {
                AuthLogin(_user.Server, _user.Username, _user.Password, _authcb);
            }
        }

        public void AuthUdp(ClientSocket.CB cb) {
            if (!_authudp) {
                _client.AuthUdp(cb);
            }
        }

        public void SendUdp(byte[] data) {
            if (_authudp) {
                _client.SendUdp(data);
            }
        }

        public void AuthUdpCb(long session, string ip, int port) {
            _authudp = true;
            this.Session = session;
            _client.AuthUdpCb(session, ip, port);
            foreach (var item in _hash) {
                item.Value.AuthUdpCb(true);
            }
        }

        public Controller Top() {
            return _stack.Peek();
        }

        public void Push(string name) {
            var ctr = _hash[name];
            Debug.Assert(ctr != null);
            _stack.Push(ctr);
            ctr.Enter();
        }

        public void Pop() {
            _stack.Pop();
        }

        public void Countdown(string name, float cd, CountdownCb cb) {
            var tm = new Timer();
            tm.Name = name;
            tm.CD = cd;
            tm.CB = cb;
            _timer[name] = tm;
        }

        public void EnqueueRenderQueue(Actor.RenderHandler handler) {
            _app.EnqueueRenderQueue(handler);
        }
    }
}
