using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace mDitaEditor.CustomControls
{
    public class FlowWithHackedScroll : FlowLayoutPanel
    {
        /// <summary>
        /// Radimo Inject na user32 dll kako bi zvali Cpp funkciju SHowScrollBar i promenili ScrollBar na paneli
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="wBar"></param>
        /// <param name="bShow"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }
        /// <summary>
        /// Definišemo da samo postoji Scroll na dole
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            ShowScrollBar(this.Handle, (int)ScrollBarDirection.SB_HORZ, false);
            base.WndProc(ref m);
        }
    }
}
