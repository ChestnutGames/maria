using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using Maria.Encrypt;

namespace Maria.Network
{
    public class PackageSocketUdp
    {
        public class R
        {
            public int Eventtime { get; set; }
            public long Session { get; set; }
            public uint Protocol { get; set; }
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

        public PackageSocketUdp(byte[] secret, long session, TimeSync ts)
        {
            Debug.Assert(ts != null);
            _secret = secret;
            _session = session;
            _timeSync = ts;

        }

        public RecviveCB OnRecviveUdp { get; set; }

        public void Change(byte[] secret, long session)
        {
            _secret = secret;
            _session = session;
        }

        public void Connect(string host, int port)
        {
            _host = host;
            _host = "192.168.199.239";
            _port = port;
            IPAddress ipadd = IPAddress.Parse(_host);
            _ep = new IPEndPoint(ipadd, _port);
            //_so.Connect(host, port);
            _connected = false;
        }

        public void Sync()
        {
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

        public void Send(byte[] data)
        {
            if (_connected)
            {
                int local = _timeSync.LocalTime();
                int[] global = _timeSync.GlobalTime();
                byte[] buffer = new byte[12 + data.Length];
                NetPack.PacklI(buffer, 0, (uint)local);
                NetPack.PacklI(buffer, 4, (uint)global[0]);
                NetPack.PacklI(buffer, 8, (uint)_session);
                Array.Copy(data, 0, buffer, 12, data.Length);
                byte[] head = Crypt.hmac_hash(_secret, buffer);
                byte[] send = new byte[8 + buffer.Length];
                Array.Copy(head, send, 8);
                Array.Copy(buffer, 0, send, 8, buffer.Length);
                Debug.Log(string.Format("localtime:{0}, eventtime:{1}, session:{2}", local, global[0], _session));
                _so.SendTo(send, _ep);
            }
            else
            {
                _sendBuffer.Add(data);
            }
        }

        public void Update()
        {
            if (!_connected)
            {
                return;
            }
            if (!_so.Poll(0, SelectMode.SelectRead))
            {
            }
            else
            {
                int remaining = 0;
                do
                {
                    int size = Rebase();
                    EndPoint ep = _ep as EndPoint;
                    int sz = _so.ReceiveFrom(_buffer, _tail, size, SocketFlags.None, ref ep);
                    _tail += sz;

                    int globaltime = NetUnpack.Unpackli(_buffer, _head);
                    int localtime = NetUnpack.Unpackli(_buffer, _head + 4);
                    int eventtime = NetUnpack.Unpackli(_buffer, _head + 8);
                    int session = NetUnpack.Unpackli(_buffer, _head + 12);
                    Debug.Log(string.Format("localtime:{0}, eventtime:{1}, session:{2}", localtime, eventtime, session));
                    if (session == _session)
                    {
                        _timeSync.Sync(localtime, globaltime);
                    }

                    remaining = _tail - _head - 16;
                    if (remaining > 0)
                    {
                        R r = new R();
                        r.Eventtime = eventtime;
                        r.Session = session;
                        r.Protocol = NetUnpack.UnpacklI(_buffer, _head + 16);
                        if (r.Protocol == 1)
                        {
                            int datalen = 28;
                            Debug.Assert(remaining >= 28 + 4);
                            byte[] buffer = new byte[datalen];
                            Array.Copy(_buffer, _head + 16, buffer, 0, datalen);
                            r.Data = buffer;
                            OnRecviveUdp(r);
                            _head += 16 + 4 + datalen;
                        }
                        else
                        {
                            Debug.Assert(false);
                        }
                    }
                    else
                    {
                        _head += 16;
                        break;
                    }
                    remaining = _tail - _head;
                } while (remaining > 0);
            }
        }

        private int Rebase()
        {
            if (_head == _tail)
            {
                _head = 0;
                _tail = 0;
            }
            else
            {
                Array.Copy(_buffer, 0, _buffer, _head, _tail - _head);
                _head = 0;
                _tail = _tail - _head;
            }
            return _cap - _tail;
        }
    }
}
