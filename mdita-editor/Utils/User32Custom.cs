using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace mDitaEditor.Utils
{
    public class User32Custom
    {
        public const UInt32 WM_PRINT = 0x0317;//The WM_PRINT message is sent to a window to request that it draw itself in the specified device context, most commonly in a printer device context. 
        private const UInt32 WM_PRINTCLIENT = 0x0318;// The WM_PRINTCLIENT message is sent to a window to request that it draw  
        public const int PRF_ERASEBKGND = 0x00000008,PRF_CLIENT = 4, PRF_CHILDREN = 0x10, PRF_NON_CLIENT = 2, COMBINED_PRINTFLAGS = PRF_CLIENT | PRF_CHILDREN | PRF_NON_CLIENT | PRF_ERASEBKGND;
        public const UInt32 WM_PAINT = 0x000F;
        public delegate bool EnumWinProc(IntPtr hwnd, int lParam);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }



        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, int lParam);
        [DllImport("user32.Dll")]
        public static extern int EnumDesktopWindows(IntPtr DeskTopHandle, EnumWinProc x, int y);
        [DllImport("User32.Dll")]
        public static extern void GetWindowText(IntPtr h, StringBuilder s, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, ref IntPtr pid);
        [DllImport("user32.dll")]
        public static extern int GetWindowModuleFileName(int hWnd, StringBuilder title, int size);
        [DllImport("user32")]
        public static extern int EnumChildWindows(IntPtr hwndParent, EnumWinProc x, int lParam);

    }


}

