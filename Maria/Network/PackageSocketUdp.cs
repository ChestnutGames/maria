﻿using System;
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
            public uint Eventtime { get; set; }
            public long Session { get; set; }
            public byte[] Data { get; set; }
        }

        public delegate void RecviveCB(R r);

        private Socket _so = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private string _host = String.Empty;
        private int _port = 0;
        private IPEndPoint _ep = null;

        private byte[] _buffer = new byte[3072];
        private int _head = 0;
        private int _tail = 0;
        private int _cap = 3072;

        private byte[] _secret;
        private long _session;
        private TimeSync _timeSync;

        private bool _connected = false;
        private List<byte[]> _sendBuffer = new List<byte[]>();

        public PackageSocketUdp(byte[] secret, long session, TimeSync ts) {
            Debug.Assert(ts != null);
            _secret = secret;
            _session = session;
            _timeSync = ts;

        }

        public RecviveCB OnRecviveUdp { get; set; }

        public void Change(byte[] secret, long session) {
            _secret = secret;
            _session = session;
        }

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
                byte[] sz = new byte[4];
                byte[] buffer = new byte[8 + 12 + 4 + data.Length];

                int local = _timeSync.LocalTime();
                int[] global = _timeSync.GlobalTime();
                NetPack.PacklI(head, 0, (uint)local);
                NetPack.PacklI(head, 4, (uint)global[0]);
                NetPack.PacklI(head, 8, (uint)_session);
                byte[] crypt_head = Crypt.hmac_hash(_secret, head);
                NetPack.PackbI(sz, 0, (uint)data.Length);

                Array.Copy(crypt_head, buffer, 8);
                Array.Copy(head, 0, buffer, 8, 12);
                Array.Copy(sz, 0, buffer, 20, 4);
                Array.Copy(data, 0, buffer, 24, data.Length);

                Debug.Log(string.Format("localtime:{0}, eventtime:{1}, session:{2}", local, global[0], _session));
                _so.SendTo(buffer, _ep);
            } else {
                _sendBuffer.Add(data);
            }
        }

        public void Update() {
            if (!_connected) {
                return;
            }
            if (!_so.Poll(0, SelectMode.SelectRead)) {
            } else {
                int size = Rebase();
                EndPoint ep = _ep as EndPoint;
                int sz = _so.ReceiveFrom(_buffer, _tail, size, SocketFlags.None, ref ep);
                _tail += sz;

                int remaining = 0;
                do {
                    int globaltime = NetUnpack.Unpackli(_buffer, _head);
                    int localtime = NetUnpack.Unpackli(_buffer, _head + 4);
                    uint eventtime = NetUnpack.UnpacklI(_buffer, _head + 8);
                    int session = NetUnpack.Unpackli(_buffer, _head + 12);
                    Debug.Log(string.Format("localtime:{0}, eventtime:{1}, session:{2}", localtime, eventtime, session));
                    if (eventtime == 0xffffffff) {
                        if (session == _session) {
                            _timeSync.Sync(localtime, globaltime);
                        }
                        _head += 16;
                        remaining = _tail - _head;
                    } else {
                        if (session == _session) {
                            _timeSync.Sync(localtime, globaltime);
                        }
                        int datalen = NetUnpack.Unpackli(_buffer, _head + 16);
                        if (datalen > 0) {
                            R r = new R();
                            r.Eventtime = eventtime;
                            r.Session = session;
                            byte[] buffer = new byte[datalen];
                            Array.Copy(_buffer, _head + 20, buffer, 0, datalen);
                            r.Data = buffer;
                            OnRecviveUdp(r);
                            _head += 16 + 4 + datalen;
                        } else {
                            _head += 16 + 4;
                        }
                        remaining = _tail - _head;
                    }
                    Debug.Assert(remaining >= 0);
                } while (remaining > 0);
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
