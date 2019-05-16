using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class NotebookForm : Form
    {
        public bool isEdit = false;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
        public LamsNotebook LamsNotebook;
        public NotebookForm()
        {
            InitializeComponent();
        }
        public void InitNotebookContent()
        {
            LamsNotebook = new LamsNotebook();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsNotebook.Title == "" || LamsNotebook.Title == null)
            {
                MessageBox.Show("Niste definisali naslov za chat");
                isError = true;
            }
            if (LamsNotebook.Instructions == "" || LamsNotebook.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije za chat");
                isError = true;
            }

            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsNotebook);
                }
                this.Close();
                DialogResult = DialogResult.OK;
            }
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
        public NotebookForm(LearningBase selectedObject, LamsNotebook chatchat = null)
        {
            Icon = Icon.FromHandle(Resources.notebook.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();

            if (chatchat == null)
            {
                InitNotebookContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsNotebook = chatchat;
                isEdit = true;
            }

            naslovTextBox.Text = LamsNotebook.Title;
            instrukcijeTextBox.Text = LamsNotebook.Instructions;
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
            LamsNotebook.Instructions = instrukcijeTextBox.Text;
        }
        /// <summary>
        /// Event na promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsNotebook.Title = naslovTextBox.Text;
        }

        /// <summary>
        /// Metoda koja vrsi dodavanje
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            LamsNotebook chat = new LamsNotebook();
            if (newQ)
            {
                LamsNotebook.Title = "Naslov";
                LamsNotebook.Instructions = "Instrukcije";
            }

        }
    }
}
