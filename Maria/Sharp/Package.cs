using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Maria.Sharp {
    public class Package {

        public const string DLL = "mariac";

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr package_alloc(IntPtr src, int size);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr package_alloci(int size);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int package_size(IntPtr self);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr package_buffer(IntPtr self);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void package_memcpy(IntPtr self, IntPtr dst, int len);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void package_free(IntPtr self);

        public static IntPtr package_packarray(byte[] buffer) {
            IntPtr pg = package_alloci(buffer.Length);
            IntPtr pgbuffer = package_buffer(pg);
            Marshal.Copy(buffer, 0, pgbuffer, buffer.Length);
            return pg;
        }

        public static byte[] package_unpackarray(IntPtr package) {
            int size = package_size(package);
            IntPtr pgbuffer = package_buffer(package);
            byte[] buffer = new byte[size];
            Marshal.Copy(pgbuffer, buffer, 0, size);
            return buffer;
        }

        public static int ReadUInt8(IntPtr ptr, int ofs, out byte val, int n) {
            UnityEngine.Debug.Assert((ofs + 1) < n);
            val = Marshal.ReadByte(ptr, ofs);
            return ofs + 1;
        }

        public static int ReadInt16(IntPtr ptr, int ofs, out short val) {
            val = Marshal.ReadInt16(ptr, ofs);
            return ofs + 2;
        }

        public static int ReadInt32(IntPtr ptr, int ofs, out int val) {
            val = Marshal.ReadInt32(ptr, ofs);
            return ofs + 4;
        }

        public static int ReadInt64(IntPtr ptr, int ofs, out long val) {
            val = Marshal.ReadInt64(ptr, ofs);
            return ofs + 8;
        }

        public static int ReadFnt32(IntPtr ptr, int ofs, out float val) {
            int i = Marshal.ReadInt32(ptr, ofs);
            val = BitConverter.ToSingle(BitConverter.GetBytes(i), 0);
            return ofs + 4;
        }

        public static int ReadFnt64(IntPtr ptr, int ofs, out double val) {
            long i = Marshal.ReadInt64(ptr, ofs);
            val = BitConverter.ToDouble(BitConverter.GetBytes(i), 0);
            return ofs + 4;
        }

        public static int ReadString(IntPtr ptr, int ofs, out string val) {
            int len;
            ofs = ReadInt32(ptr, ofs, out len);
            if (len > 0) {
                byte[] buffer = new byte[len];
                Marshal.Copy(ptr, buffer, ofs, len);
                val = Encoding.ASCII.GetString(buffer);
                return (ofs + len);
            } else {
                val = string.Empty;
                return ofs;
            }
        }
    }
}
