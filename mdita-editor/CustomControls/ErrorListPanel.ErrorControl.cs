using System;
using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.CustomControls
{
    public partial class ErrorControl : Label
    {
        private SavingError _error;

        /// <summary>
        /// Na set errora postavljamo Text errora na prosleđeni error.TExt
        /// </summary>
        public SavingError Error
        {
            get { return _error; }
            set
            {
                _error = value;
                if (_error != null)
                {
                    Text = _error.Text;
                }
                else
                {
                    Text = "";
                }
            }
        }

        /// <summary>
        /// Konstruktor koji prima Error
        /// </summary>
        /// <param name="error"></param>
        public ErrorControl(SavingError error)
        {
            InitializeComponent();
            Error = error;
        }

        /// <summary>
        /// Metoda koja na klik error-a otvara slajd sa problemom i fokusira
        /// kontrolu koja je problematična
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ErrorControl_Click(object sender, EventArgs e)
        {
            Error?.FocusRelevantItem();
        }
    }
}
