using System;
using System.Runtime.InteropServices;
using System.Threading;
using static SidebarApp.NativeMethods;

namespace SidebarApp
{
    class Program
    {
        private const string MutexName = "Global\\SidebarApp_Mutex";
        private const string ExitEventName = "Global\\SidebarApp_Exit";

        static void Main(string[] args)
        {
            int width = ParseWidth(args);

            bool isFirstInstance;
            using (Mutex mutex = new Mutex(true, MutexName, out isFirstInstance))
            {
                if (!isFirstInstance)
                {
                    // Only allowed action from second instance: close
                    if (width == 0)
                        SignalExit();

                    return;
                }

                if (width <= 0)
                    return;

                Run(width);
            }
        }

        static void Run(int width)
        {
            IntPtr hwnd = NativeMethods.CreateMessageWindow();
            if (hwnd == IntPtr.Zero)
                return;

            using (EventWaitHandle exitEvent =
                new EventWaitHandle(false, EventResetMode.AutoReset, ExitEventName))
            {
                AppBar.SetRightSidebar(hwnd, width);

                NativeMethods.MSG msg;
                while (true)
                {
                    if (exitEvent.WaitOne(0))
                        break;

                    if (!NativeMethods.PeekMessage(out msg, IntPtr.Zero, 0, 0, 1))
                    {
                        Thread.Sleep(50);
                        continue;
                    }

                    NativeMethods.TranslateMessage(ref msg);
                    NativeMethods.DispatchMessage(ref msg);
                }
            }

            AppBar.SetRightSidebar(hwnd, 0);
            NativeMethods.DestroyWindow(hwnd);
        }

        static void SignalExit()
        {
            try
            {
                EventWaitHandle.OpenExisting(ExitEventName).Set();
            }
            catch { }
        }

        static int ParseWidth(string[] args)
        {
            if (args.Length != 1)
                return -1;

            int value;
            return int.TryParse(args[0], out value) ? value : -1;
        }
    }

    // ---------------- APPBAR ----------------

    internal static class AppBar
    {
        private const int ABM_NEW = 0;
        private const int ABM_REMOVE = 1;
        private const int ABM_QUERYPOS = 2;
        private const int ABM_SETPOS = 3;
        private const int ABE_RIGHT = 2;

        [StructLayout(LayoutKind.Sequential)]
        private struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public int lParam;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left, top, right, bottom;
        }

        [DllImport("shell32.dll")]
        private static extern uint SHAppBarMessage(uint msg, ref APPBARDATA data);

        public static void SetRightSidebar(IntPtr hwnd, int width)
        {
            APPBARDATA abd = new APPBARDATA
            {
                cbSize = Marshal.SizeOf(typeof(APPBARDATA)),
                hWnd = hwnd
            };

            if (width <= 0)
            {
                SHAppBarMessage(ABM_REMOVE, ref abd);
                return;
            }

            SHAppBarMessage(ABM_NEW, ref abd);

            int w = NativeMethods.GetSystemMetrics(0);
            int h = NativeMethods.GetSystemMetrics(1);

            abd.uEdge = ABE_RIGHT;
            abd.rc.top = 0;
            abd.rc.bottom = h;
            abd.rc.right = w;
            abd.rc.left = w - width;

            SHAppBarMessage(ABM_QUERYPOS, ref abd);
            abd.rc.left = abd.rc.right - width;
            SHAppBarMessage(ABM_SETPOS, ref abd);
        }
    }

    // ---------------- TRAY ICON ----------------



    // ---------------- NATIVE ----------------

    internal static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        internal struct MSG
        {
            public IntPtr hwnd;
            public uint message;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct NOTIFYICONDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public int uID;
            public int uFlags;
            public int uCallbackMessage;
            public IntPtr hIcon;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szTip;
        }

        [DllImport("user32.dll")]
        internal static extern int GetSystemMetrics(int index);

        [DllImport("user32.dll")]
        internal static extern bool PeekMessage(out MSG msg, IntPtr hWnd, uint min, uint max, uint remove);

        [DllImport("user32.dll")]
        internal static extern bool TranslateMessage(ref MSG msg);

        [DllImport("user32.dll")]
        internal static extern IntPtr DispatchMessage(ref MSG msg);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr CreateWindowEx(
            int ex, string cls, string name, int style,
            int x, int y, int w, int h,
            IntPtr parent, IntPtr menu, IntPtr inst, IntPtr param);

        [DllImport("user32.dll")]
        internal static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("shell32.dll")]
        internal static extern bool Shell_NotifyIcon(int msg, ref NOTIFYICONDATA data);

        [DllImport("user32.dll")]
        internal static extern IntPtr LoadIcon(IntPtr hInst, IntPtr name);

        internal static IntPtr CreateMessageWindow()
        {
            return CreateWindowEx(
                0, "STATIC", "", 0,
                0, 0, 0, 0,
                IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
