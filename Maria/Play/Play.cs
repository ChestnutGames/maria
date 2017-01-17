using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Maria.Play {
    public class Play : DisposeObject {

        private SharpC _sharpc = null;
        private IntPtr _play = IntPtr.Zero;

        public Play(SharpC sharpc ) {
            _sharpc = sharpc;
            SharpC.CSObject[] args = new SharpC.CSObject[2];
            args[0] = _sharpc.CacheObj(this);
            args[1] = _sharpc.CacheFunc(fetch);
            _play = Play_CSharp.play_alloc(args[0], args[1]);
        }

        protected override void Dispose(bool disposing) {
            if (_disposed) return;
            if (disposing) {
                // release managed
            }
            // release unmanaged
            Play_CSharp.play_free(_play);
            _disposed = true;
        }

        public void update(float delta) {
            SharpC.CSObject cso = new SharpC.CSObject();
            cso.type = SharpC.CSType.REAL;
            cso.f = delta;
            Play_CSharp.play_update(_play, cso);
        }

        public int join(int uid, int sid) {
            //GCHandle handle = GCHandle.Alloc(ud);
            //IntPtr ptr = GCHandle.ToIntPtr(handle);

            SharpC.CSObject[] args = new SharpC.CSObject[2];
            args[0].type = SharpC.CSType.INT32;
            args[0].v32 = uid;

            args[1].type = SharpC.CSType.INT32;
            args[1].v32 = sid;

            return Play_CSharp.play_join(_play, args[0], args[1]);
        }

        public void leave(int uid, int sid) {
            SharpC.CSObject[] args = new SharpC.CSObject[2];
            args[0].type = SharpC.CSType.INT32;
            args[0].v32 = uid;

            args[1].type = SharpC.CSType.INT32;
            args[1].v32 = sid;
            Play_CSharp.play_leave(_play, args[0], args[1]);
        }

        public static int fetch(int argc, [In, Out, MarshalAs(UnmanagedType.LPArray, SizeConst = 8)] SharpC.CSObject[] argv, int args, int res) {
            return 0;
        }
    }
}
