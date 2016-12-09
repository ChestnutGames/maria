using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Maria.Play 
{
    class Play : IDisposable
    {
        protected bool _disposed;

        private IntPtr _play = IntPtr.Zero;

        public Play()
        {
            _play = Play_CSharp.play_alloc();
        }

        public ~Play()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                // release managed
            }
            // release unmanaged
            Play_CSharp.play_free(_play);
            _disposed = true;
        }

        public void update()
        {
            Play_CSharp.play_update(_play);
        }

        public int join(object ud)
        {
            GCHandle handle = GCHandle.Alloc(ud);
            IntPtr ptr = GCHandle.ToIntPtr(handle);
            return Play_CSharp.play_join(_play, ptr);
        }

        public void leave(int id)
        {
            Play_CSharp.play_leave(_play, id);
        }
    }
}
