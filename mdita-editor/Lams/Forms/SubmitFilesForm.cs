using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class SubmitFilesForm : Form
    {
        public bool isEdit = false;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
        public LamsSubmitFiles LamsSubmitFiles;

        public SubmitFilesForm() {
            InitializeComponent();
           
        }

        public void InitSFContent()
        {
            LamsSubmitFiles = new LamsSubmitFiles();
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
        public SubmitFilesForm(LearningBase selectedObject, LamsSubmitFiles sf = null)
        {
            Icon = Icon.FromHandle(Resources.lms_submit_files24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
           
            if (sf == null)
            {
                InitSFContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsSubmitFiles = sf;
                isEdit = true;
            }

            naslovTextBox.Text = LamsSubmitFiles.Title;
            instrukcijeTextBox.Text = LamsSubmitFiles.Instruction;
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
            LamsSubmitFiles.Instruction = instrukcijeTextBox.Text;
        }
        /// <summary>
        /// Event na promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsSubmitFiles.Title = naslovTextBox.Text;
        }

        private void SFControlForm_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        ///  Event na button Save koja vrsi validaciju unetog naslova i instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsSubmitFiles.Title == "" || LamsSubmitFiles.Title == null)
            {
                MessageBox.Show("Niste definisali naslov za slanje datoteke");
                isError = true;
            }
            if (LamsSubmitFiles.Instruction == "" || LamsSubmitFiles.Instruction == null)
            {
                MessageBox.Show("Niste definisali instrukcije za slanje datoteke");
                isError = true;
            }
           
            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsSubmitFiles);
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            LamsSubmitFiles sf = new LamsSubmitFiles();
            if (newQ)
            {
                sf.Title = "Naslov";
                sf.Instruction = "Instrukcije";
            }
            
        }
        /// <summary>
        /// Event na button Cancel koji zatvara formu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
