using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using Maria.Encrypt;

namespace Maria.Network {
    public class PackageSocketUdp {
        public class R {
            
            public uint Globaltime { get; set; }
            public uint Localtime { get; set; }
            public uint Eventtime { get; set; }
            public uint Session { get; set; }
            public byte[] Data { get; set; }
        }

        public delegate void RecvCB(R r);
        public delegate void SyncCB();

        private Socket _so = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private string _host = String.Empty;
        private int _port = 0;
        private IPEndPoint _ep = null;

        private byte[] _buffer = new byte[3072];
        private int _head = 0;
        private int _tail = 0;
        private int _cap = 3072;

        private byte[] _secret;
        private uint _session;
        private TimeSync _timeSync;

        private List<byte[]> _sendBuffer = new List<byte[]>();
        private bool _connected = false;
        private RecvCB _recvCb = null;
        private SyncCB _syncCb = null;
        
        public PackageSocketUdp(byte[] secret, uint session, TimeSync ts) {
            Debug.Assert(ts != null);
            _secret = secret;
            _session = session;
            _timeSync = ts;
        }

        public RecvCB OnRecv { get { return _recvCb; } set { _recvCb = value; } }
        public SyncCB OnSync { get { return _syncCb; } set { _syncCb = value; } }

        public void Connect(string host, int port) {
            _host = host;
            _host = "192.168.199.239";
            _port = port;
            IPAddress ipadd = IPAddress.Parse(_host);
            _ep = new IPEndPoint(ipadd, _port);
            //_so.Connect(host, port);
            _connected = false;
        }

        public void Sync() {
            int now = _timeSync.LocalTime();
            byte[] buffer = new byte[12];
            NetPack.PacklI(buffer, 0, (uint)now);
            NetPack.PacklI(buffer, 4, 0xffffffff);
            NetPack.PacklI(buffer, 8, (uint)_session);
            byte[] head = Crypt.hmac_hash(_secret, buffer);
            byte[] data = new byte[8 + buffer.Length];
            Array.Copy(head, data, 8);
            Array.Copy(buffer, 0, data, 8, buffer.Length);
            _so.SendTo(data, _ep);
            _connected = true;
        }

        public void Send(byte[] data) {
            if (_connected) {
                // 8 + 12 + 4 + data
                //byte[] crypt_head = new byte[8];
                byte[] head = new byte[12];
                byte[] buffer = new byte[8 + 12 + data.Length];

                int local = _timeSync.LocalTime();
                int[] global = _timeSync.GlobalTime();
                NetPack.PacklI(head, 0, (uint)local);
                NetPack.PacklI(head, 4, (uint)global[0]);
                NetPack.PacklI(head, 8, (uint)_session);
                byte[] crypt_head = Crypt.hmac_hash(_secret, head);

                Array.Copy(crypt_head, buffer, 8);
                Array.Copy(head, 0, buffer, 8, 12);
                Array.Copy(data, 0, buffer, 20, data.Length);

                Debug.Log(string.Format("localtime:{0}, eventtime:{1}, session:{2}", local, global[0], _session));
                _so.SendTo(buffer, _ep);
            } else {
                Debug.Assert(false);
                //_sendBuffer.Add(data);
            }
        }

        public void Update() {
            if (!_connected) {
                return;
            }
            while (_so.Poll(0, SelectMode.SelectRead)) {
                EndPoint ep = _ep as EndPoint;
                int sz = _so.ReceiveFrom(_buffer, _head, _cap, SocketFlags.None, ref ep);
                _tail += sz;

                int remaining = 0;
                do {
                    uint globaltime = NetUnpack.UnpacklI(_buffer, _head);
                    uint localtime = NetUnpack.UnpacklI(_buffer, _head + 4);
                    uint eventtime = NetUnpack.UnpacklI(_buffer, _head + 8);
                    uint session = NetUnpack.UnpacklI(_buffer, _head + 12);
                    Debug.Log(string.Format("localtime:{0}, eventtime:{1}, session:{2}", localtime, eventtime, session));
                    if (eventtime == 0xffffffff) {
                        if (session == _session) {
                            _timeSync.Sync((int)localtime, (int)globaltime);
                            if (_syncCb != null) {
                                _syncCb();
                            }
                        }
                        Debug.Assert(_tail - _head == 16);
                    } else {
                        if (session == _session) {
                            //_timeSync.Sync((int)localtime, (int)globaltime);
                        }

                        int datalen = _tail - 16;
                        if (datalen > 0) {
                            R r = new R();
                            r.Globaltime = globaltime;
                            r.Localtime = localtime;
                            r.Eventtime = eventtime;
                            r.Session = session;
                            byte[] buffer = new byte[datalen];
                            Array.Copy(_buffer, _head + 16, buffer, 0, datalen);
                            r.Data = buffer;
                            if (_recvCb != null) {
                                _recvCb(r);
                            }
                        }
                    }
                } while (remaining > 0);
                _head = 0;
                _tail = 0;
            }
        }

        private int Rebase() {
            if (_head == _tail) {
                _head = 0;
                _tail = 0;
            } else {
                Array.Copy(_buffer, 0, _buffer, _head, _tail - _head);
                _head = 0;
                _tail = _tail - _head;
            }
            return _cap - _tail;
        }
    }
}
