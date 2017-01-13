using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Maria.Play
{
    class Play_CSharp
    {
        [DllImport("play", EntryPoint = "play_alloc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr play_alloc();

        [DllImport("play", EntryPoint = "play_free", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_free(IntPtr self);

        [DllImport("play", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_start(IntPtr self);

        [DllImport("play", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_close(IntPtr self);

        [DllImport("play", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_kill(IntPtr self);

        [DllImport("play", EntryPoint = "play_update", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_update(IntPtr self, float delta);

        [DllImport("play", EntryPoint = "play_join", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int  play_join(IntPtr self, int uid, int sid);

        [DllImport("play", EntryPoint = "play_leave", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_leave(IntPtr self, int uid, int sid);

        [DllImport("play", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void play_fetch(IntPtr self, IntPtr ptr);

    }
}
