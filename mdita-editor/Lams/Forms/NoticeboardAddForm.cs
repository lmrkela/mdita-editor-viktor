using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class NoticeboardAddForm : Form
    {
        public bool isEdit = false;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
        public LamsNoticeboard LamsNoticeboard;

        public NoticeboardAddForm()
        {
            InitializeComponent();
        }
        public void InitNoticeboardContent()
        {
            LamsNoticeboard = new LamsNoticeboard();
        }
        /// <summary>
        /// Konstruktor kome se prosledjuju parametri selectedObject i sf, 
        /// proverava se da li je sf null, ukoliko jeste, vrsi se inicijalizacija 
        /// kontenta submit forme, u suprotnom kontent SF-a postaje ono sto se prosledjuje
        /// kroz parametar sf. 
        /// Konstruktor se poziva kroz event na Edit button ManageAdditionalActivities klase, 
        /// kao i na event AddSF button-a.
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <param name="sf"></param>
        public NoticeboardAddForm(LearningBase selectedObject, LamsNoticeboard noticeboard = null)
        {
            Icon = Icon.FromHandle(Resources.noticeboard.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();

            if (noticeboard == null)
            {
                InitNoticeboardContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsNoticeboard = noticeboard;
                isEdit = true;
            }

            naslovTextBox.Text = LamsNoticeboard.Title;
            instrukcijeTextBox.DocumentText = LamsNoticeboard.Content;
            naslovTextBox.TextChanged += NaslovTextBox_TextChanged;
            instrukcijeTextBox.TextChanged += InstrukcijeTextBox_TextChanged;
        }
        /// <summary>
        /// Event na promenu teksta u okviru instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstrukcijeTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsNoticeboard.Content = instrukcijeTextBox.BodyHtml;
        }
        /// <summary>
        /// Event na promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsNoticeboard.Title = naslovTextBox.Text;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            LamsNoticeboard.Content = instrukcijeTextBox.DocumentText;
            bool isError = false;
            if (LamsNoticeboard.Title == "" || LamsNoticeboard.Title == null)
            {
                MessageBox.Show("Niste definisali naslov za noticeboard");
                isError = true;
            }
            if (LamsNoticeboard.Content == "" || LamsNoticeboard.Content == null)
            {
                MessageBox.Show("Niste definisali sadrzaj za noticeboard");
                isError = true;
            }

            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsNoticeboard);
                }
                this.Close();
                DialogResult = DialogResult.OK;
            }
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            if (newQ)
            {
                LamsNoticeboard.Title = "Naslov";
                LamsNoticeboard.Content = "Tekst oglasne table";
            }

        }
    }
}
