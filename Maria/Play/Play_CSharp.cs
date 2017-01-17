using System;
using System.Runtime.InteropServices;

namespace Maria.Play {
    class Play_CSharp
    {
        private const string DLL = "play";

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr play_alloc();

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_free(IntPtr self);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_update(IntPtr self);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern int  play_join(IntPtr self, IntPtr ud);

        [DllImport(DLL, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_leave(IntPtr self, int id);
    }
}
