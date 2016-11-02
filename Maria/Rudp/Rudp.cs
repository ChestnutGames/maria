using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Maria.Rudp {
    class Rudp : IDisposable {
        private IntPtr _u;

        public Rudp(int send_delay, int expired_time) {
            _u = Rudp_CSharp.aux_new(send_delay, expired_time);
        }

        public void Dispose() {
            Rudp_CSharp.aux_delete(_u);
        }

        public byte[] Recv(int cap) {
            Rudp_CSharp.package pack = new Rudp_CSharp.package();
            IntPtr buffer = Marshal.AllocHGlobal(cap);
            pack.buffer = buffer;
            pack.cap = cap;
            pack.sz = 0;

            int res = Rudp_CSharp.aux_recv(_u, pack);
            if (res == 0) {
                return null;
            } else if (res == 1) {
                byte[] r = new byte[pack.sz];
                Marshal.Copy(r, 0, pack.buffer, pack.sz);
                Marshal.FreeHGlobal(buffer);
                return r;
            } else if (res == 2) {
                return null;
            }
            return null;
        }

        public void Send(byte[] buf) {
            IntPtr buffer = Marshal.AllocHGlobal(buf.Length);
            Marshal.Copy(buf, 0, buffer, buf.Length);
            Rudp_CSharp.package pack = new Rudp_CSharp.package();
            pack.buffer = buffer;
            pack.cap = buf.Length;
            pack.sz = buf.Length;

            Rudp_CSharp.aux_send(_u, pack);
        }

        public byte[] Update(byte[] buf, int tick) {
            IntPtr buffer = Marshal.AllocHGlobal(buf.Length);
            Marshal.Copy(buf, 0, buffer, buf.Length);
            Rudp_CSharp.package pack = new Rudp_CSharp.package();
            pack.buffer = buffer;
            pack.cap = buf.Length;
            pack.sz = buf.Length;

            Rudp_CSharp.package res = Rudp_CSharp.aux_update(_u, pack, tick);
            byte[] r = new byte[res.sz];
            Marshal.Copy(r, 0, res.buffer, res.sz);
            Rudp_CSharp.aux_free_package(res);
            Marshal.FreeHGlobal(buffer);
            return r;
        }
    }
}
