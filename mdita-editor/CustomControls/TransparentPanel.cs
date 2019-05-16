using System.Windows.Forms;

namespace mDitaEditor.CustomControls
{
    /// <summary>
    /// A transparent control.
    /// </summary>
    public class TransparentPanel : Panel
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x0020; // WS_EX_TRANSPARENT
                return createParams;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {}
    }
}
