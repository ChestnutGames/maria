﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;

namespace Maria.Rudp {
    public class Rudp : DisposeObject {
        public delegate void Callback(byte[] buffer, int start, int len);

        private Context _ctx;
        private IntPtr _u;
        private IntPtr _buffer;
        private static byte[] _sendBuffer;
        private static byte[] _recvBuffer;

        public Rudp(Context ctx, int send_delay, int expired_time) {
            _ctx = ctx;
            SharpC sharpc = _ctx.SharpC;
            SharpC.CSObject cso1 = sharpc.CacheFunc(RSend);
            SharpC.CSObject cso2 = sharpc.CacheFunc(RRecv);

            _u = Rudp_CSharp.aux_new(send_delay, expired_time, cso1, cso2);

            _buffer = Marshal.AllocHGlobal(3072);

            _sendBuffer = new byte[3072];
            _recvBuffer = new byte[3072];
        }

        protected override void Dispose(bool disposing) {
            if (_disposed) {
                return;
            }
            if (disposing) {
                // 清理托管资源，调用自己管理的对象的Dispose方法
            }
            // 清理非托管资源
            Marshal.FreeHGlobal(_buffer);
            Rudp_CSharp.aux_delete(_u);
            _disposed = true;
        }

        public Callback OnSend { get; set; }
        public Callback OnRecv { get; set; }

        public void Send(byte[] buf) {
            Debug.Assert(buf.Length != 0);
            IntPtr buffer = Marshal.AllocHGlobal(buf.Length);
            Marshal.Copy(buf, 0, buffer, buf.Length);
            Rudp_CSharp.aux_send(_u, buffer, buf.Length);
            Marshal.FreeHGlobal(buffer);
        }

        public void Update(byte[] buf, int start, int len, int tick) {
            Marshal.Copy(buf, start, _buffer, len);
            Rudp_CSharp.aux_update(_u, _buffer, len, tick);
        }

        public static int RSend(int argc, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 32)] SharpC.CSObject[] argv) {
            Debug.Assert(argc >= 3);
            Debug.Assert(argv[0].type == SharpC.CSType.SHARPOBJECT);
            Rudp u = (Rudp)SharpC.cache.Get(argv[0].v32);
            IntPtr buffer = argv[1].ptr;
            int len = argv[2].v32;
            if (u.OnSend != null) {
                Marshal.Copy(buffer, _sendBuffer, 0, len);
                u.OnSend(_sendBuffer, 0, len);
            }
            return 0;
        }

        public static int RRecv(int argc, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 32)] SharpC.CSObject[] argv) {
            Debug.Assert(argc >= 3);
            Debug.Assert(argv[0].type == SharpC.CSType.SHARPOBJECT);
            Rudp u = (Rudp)SharpC.cache.Get(argv[0].v32);
            IntPtr buffer = argv[1].ptr;
            int len = argv[2].v32;
            if (u.OnSend != null) {
                Marshal.Copy(buffer, _recvBuffer, 0, len);
                u.OnSend(_recvBuffer, 0, len);
            }
            return 0;
        }


            //[MonoPInvokeCallback(typeof(Rudp_CSharp.Callback))]
            //public void RSend(IntPtr buffer, int len) {
            //    Marshal.Copy(buffer, _sendBuffer, 0, len);
            //    if (OnSend != null) {
            //        OnSend(_sendBuffer, 0, len);
            //    }
            //}

            //[MonoPInvokeCallback(typeof(Rudp_CSharp.Callback))]
            //public void RRecv(IntPtr buffer, int len) {
            //    Marshal.Copy(buffer, _recvBuffer, 0, len);
            //    if (OnRecv != null) {
            //        OnRecv(_recvBuffer, 0, len);
            //    }
            //}
        }
}
