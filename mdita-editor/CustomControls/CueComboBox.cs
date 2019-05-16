using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace mDitaEditor.CustomControls
{
    public class CueComboBox : ComboBox
    {
        private string _cue;

        /// <summary>
        /// Getter i setter za Watermark na Comboboxu
        /// </summary>
        public string Cue
        {
            get { return _cue; }
            set
            {
                _cue = value;
                updateCue();
            }
        }

        public int OldIndex
        {
            get
            {
                return _oldIndex;
            }

            set
            {
                _oldIndex = value;
            }
        }
        int _oldIndex;


        public bool UndoRedoEvent
        {
            get
            {
                return _undoRedo;
            }

            set
            {
                _undoRedo = value;
            }
        }
        bool _undoRedo;



        public void saveCurrentIndex()
        {
            _oldIndex = SelectedIndex;
        }

   

        /// <summary>
        /// Metoda koja updejtuje Watermark
        /// </summary>
        private void updateCue()
        {
            if (this.IsHandleCreated)
                SendMessageCue(this.Handle, CB_SETCUEBANNER, IntPtr.Zero, _cue ?? "");
        }
        /// <summary>
        /// Kada se kontrola kreira potrebno je ažurirati Watermark
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            updateCue();
        }

        // P/Invoke
        private const int CB_SETCUEBANNER = 0x1703;
        [DllImport("user32.dll", EntryPoint = "SendMessageW", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessageCue(IntPtr hWnd, int msg, IntPtr wp, string lp);
    }
}
