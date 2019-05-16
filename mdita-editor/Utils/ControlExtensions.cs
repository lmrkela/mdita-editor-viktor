using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mDitaEditor.Utils
{
    public static class ControlExtensions
    {
        [DllImport("gdi32.dll")]

        [return: MarshalAs(UnmanagedType.Bool)]

        static extern bool BitBlt(IntPtr hdc, int nXDest, int nYDest, int nWidth,

            int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, uint dwRop);
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(
   IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc,
          int nXSrc, int nYSrc, long dwRop);


        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

        static extern IntPtr CreateCompatibleDC(IntPtr hdc);



        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

        static extern bool DeleteDC(IntPtr hdc);



        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);



        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]

        static extern bool DeleteObject(IntPtr hObject);



        public enum TernaryRasterOperations

        {

            SRCCOPY = 0x00CC0020, /* dest = source*/

            SRCPAINT = 0x00EE0086, /* dest = source OR dest*/

            SRCAND = 0x008800C6, /* dest = source AND dest*/

            SRCINVERT = 0x00660046, /* dest = source XOR dest*/

            SRCERASE = 0x00440328, /* dest = source AND (NOT dest )*/

            NOTSRCCOPY = 0x00330008, /* dest = (NOT source)*/

            NOTSRCERASE = 0x001100A6, /* dest = (NOT src) AND (NOT dest) */

            MERGECOPY = 0x00C000CA, /* dest = (source AND pattern)*/

            MERGEPAINT = 0x00BB0226, /* dest = (NOT source) OR dest*/

            PATCOPY = 0x00F00021, /* dest = pattern*/

            PATPAINT = 0x00FB0A09, /* dest = DPSnoo*/

            PATINVERT = 0x005A0049, /* dest = pattern XOR dest*/

            DSTINVERT = 0x00550009, /* dest = (NOT dest)*/

            BLACKNESS = 0x00000042, /* dest = BLACK*/

            WHITENESS = 0x00FF0062, /* dest = WHITE*/

        };
        public static void CaptureControl(this Control ctrl, Bitmap bitmap, Rectangle targetBounds)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            if (targetBounds.Width <= 0 || targetBounds.Height <= 0
                || targetBounds.X < 0 || targetBounds.Y < 0)
            {
                throw new ArgumentException("targetBounds");
            }

            int width = Math.Min(ctrl.Width, targetBounds.Width);
            int height = Math.Min(ctrl.Height, targetBounds.Height);

            Bitmap image = new Bitmap(width, height, bitmap.PixelFormat);
            using (Graphics g = Graphics.FromImage(image))
            {
                IntPtr hDc = g.GetHdc();
                User32Custom.SendMessage(ctrl.Handle, User32Custom.WM_PRINT, hDc, User32Custom.COMBINED_PRINTFLAGS);
                using (Graphics destGraphics = Graphics.FromImage(bitmap))
                {
                    IntPtr desthDC = destGraphics.GetHdc();
                    BitBlt(desthDC, targetBounds.X, targetBounds.Y, width, height, hDc, 0, 0, 0xcc0020);
                    destGraphics.ReleaseHdcInternal(desthDC);
                }
                g.ReleaseHdcInternal(hDc);
            }
        }
    }
}
