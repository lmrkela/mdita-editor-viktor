using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace mDitaEditor.CustomControls
{
    public class CueTextBox :TextBox
    {
        private bool _waterMarkTextEnabled;

        private string _cue;
        [Localizable(true)]
        public string Cue
        {
            get { return _cue; }
            set { _cue = value; Invalidate(); }
        }

        private Color _cueColor = Color.Gray;
        public Color CueColor
        {
            get { return _cueColor; }
            set
            { _cueColor = value; Invalidate(); }
        }

        public string OldText
        {
            get
            {
                return _oldText;
            }

            set
            {
                _oldText = value;
            }
        }

        public void saveCurrentText()
        {
            OldText = Text;
        }

        private string _oldText;
        
        /// <summary>
        /// Definiše metode za promenu teksta, fonta i gubljenje fokusa
        /// </summary>
        public CueTextBox()
        {
            TextChanged += CueTextBox_TextChanged;
            LostFocus += CueTextBox_TextChanged;
            FontChanged += CueTextBox_FontChanged;
            this.BorderStyle = BorderStyle.FixedSingle;
        }
        
        /// <summary>
        /// Kada se kontrola kreira pozvati TextChanged
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            CueTextBox_TextChanged(null, null);
        }
        /// <summary>
        /// Na paint nacrtati Watermark string
        /// </summary>
        /// <param name="args"></param>
        protected override void OnPaint(PaintEventArgs args)
        {
            base.OnPaint(args);
            if (_waterMarkTextEnabled)
            {
                SolidBrush drawBrush = new SolidBrush(CueColor);
                args.Graphics.DrawString(Cue, Font, drawBrush, new PointF(1, 3));
            }
            ControlPaint.DrawBorder(args.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }
        /// <summary>
        /// Ugasiti watermark ako ne postoji tekst
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CueTextBox_TextChanged(object sender, EventArgs args)
        {
            _waterMarkTextEnabled = Text.Length == 0;
            this.Text = Text.Replace("\v", "").Replace("\t", "");
            SetStyle(ControlStyles.UserPaint, _waterMarkTextEnabled);
            Invalidate();
        }
        /// <summary>
        /// Uraditi Invalidate pri promeni fonta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void CueTextBox_FontChanged(object sender, EventArgs args)
        {
            if (_waterMarkTextEnabled)
            {
                Invalidate();
            }
        }
    }
}
