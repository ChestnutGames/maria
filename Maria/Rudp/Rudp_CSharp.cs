﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Maria.Rudp {
    class Rudp_CSharp {
        public delegate void On

        [StructLayout(LayoutKind.Sequential)]
        public struct rudp_package {
            public IntPtr next;
            public IntPtr buffer;
            public int sz;
        }

        public static void rudp_package_foreach()





        [DllImport("rudp", EntryPoint = "aux_new", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr aux_new(int send_delay, int expired_time);

        [DllImport("rudp", EntryPoint = "aux_delete", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void aux_delete(IntPtr U);

        [DllImport("rudp", EntryPoint = "aux_recv", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int aux_recv(IntPtr U, IntPtr buffer, int sz);

        [DllImport("rudp", EntryPoint = "aux_send", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void aux_send(IntPtr U, IntPtr buffer, int sz);

        [DllImport("rudp", EntryPoint = "aux_update", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr aux_update(IntPtr U, IntPtr buffer, int sz, int tick);

        //[DllImport("rudp", EntryPoint = "aux_free_package", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Winapi)]
        //public static extern void aux_free_package(package pack);

        public static bool unpack_rudp_package(IntPtr ptr) {
            rudp_package pack = (rudp_package)Marshal.PtrToStructure(ptr, typeof(rudp_package));
            return true;
        }
    }
}
