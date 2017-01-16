using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Maria {
    class SharpC : DisposeObject {
        public enum CSType {
            NIL = 0,
            INT32 = 1,
            INT64 = 2,
            REAL = 3,
            BOOLEAN = 4,
            STRING = 5,
            INTPTR = 6,
            SHARPOBJECT = 7,
            SHARPFUNCTION = 8,
            SHARPSTRING = 9,
        }

        public struct CSObject {
            //public CSObject() {
            //}

            //public CSType Type { get; set; }
            //public WeakReference obj { get; set; }
            //public object obj { get; set; }
            public CSType type;
            public IntPtr ptr;
            public Int32 v32; // len or key or d
            public Int64 v64;
            public Double f;
        }

        public delegate int pfunc(int argc, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 32)] CSObject[] argv);

        const string DLL = "sharpc.dll";

        IntPtr _sharpc = IntPtr.Zero;

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr sharpc_alloc(pfunc func);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void sharpc_free(IntPtr self);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int sharpc_call(IntPtr self, int argc, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst =256)] CSObject[] argv);

        private static SharpObject _cache = new SharpObject();

        public SharpC(pfunc func) {
            _sharpc = sharpc_alloc(func);
        }

        protected virtual void Dispose(bool disposing) {
            if (_disposed) {
                return;
            }
            if (disposing) {
                // 清理托管资源，调用自己管理的对象的Dispose方法
            }
            // 清理非托管资源
            sharpc_free(_sharpc);

            _disposed = true;
        }

        public static int CallCSharp(int argc, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 32)] CSObject[] argv) {
            UnityEngine.Debug.Assert(argc > 0);
            CSObject o = argv[argc];
            if (o.type == CSType.SHARPFUNCTION) {
                pfunc f = (pfunc)_cache.Get(o.v32);
                f(argc, argv);
            }
            return 0;
        }

        void CacheFunc(pfunc func) {
            CSObject o = new CSObject();
            o.type = CSType.SHARPFUNCTION;
            o.v32 = _cache.AddKey(func);
        }
    }
}
