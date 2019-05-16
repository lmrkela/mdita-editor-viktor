using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class ChatForm : Form
    {
        public bool isEdit = false;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
        public LamsChat LamsChat;

        public ChatForm()
        {
            InitializeComponent();

        }

        public void InitChatContent()
        {
            LamsChat = new LamsChat();
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
        public ChatForm(LearningBase selectedObject, LamsChat chatchat = null)
        {
            Icon = Icon.FromHandle(Resources.lms_chat24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();

            if (chatchat == null)
            {
                InitChatContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsChat = chatchat;
                isEdit = true;
            }

            naslovTextBox.Text = LamsChat.Title;
            instrukcijeTextBox.Text = LamsChat.Instructions;
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
            LamsChat.Instructions = instrukcijeTextBox.Text;
        }
        /// <summary>
        /// Event na promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsChat.Title = naslovTextBox.Text;
        }

        /// <summary>
        ///  Event na button Save koja vrsi validaciju unetog naslova i instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsChat.Title == "" || LamsChat.Title == null)
            {
                MessageBox.Show("Niste definisali naslov za chat");
                isError = true;
            }
            if (LamsChat.Instructions == "" || LamsChat.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije za chat");
                isError = true;
            }

            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsChat);
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
            LamsChat chat = new LamsChat();
            if (newQ)
            {
                LamsChat.Title = "Naslov";
                LamsChat.Instructions = "Instrukcije";
            }

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
