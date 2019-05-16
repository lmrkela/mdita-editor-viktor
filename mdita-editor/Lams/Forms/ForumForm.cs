using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class ForumForm : Form
    {
        public bool isEdit = false;
        public List<ForumTopicControl> _questions;
        public LamsForum Forum;
        public LearningContent LearningContent;
        //  public Sectiondiv _sectiondiv;
        public LearningBase LearningObject;
        public ForumForm()
        {
            InitializeComponent();
        }


        public void InitForumContent()
        {
            Forum = new LamsForum();
        }
        /// <summary>
        /// Konstruktor kome se prosledjuju parametri selectedObject i forum, 
        /// proverava se da li je forum null, ukoliko jeste, vrsi se inicijalizacija 
        /// kontenta foruma, u suprotnom kontent LamsForum-a postaje ono sto se prosledjuje
        /// kroz parametar forum. 
        /// Takodje, vrsi se provera da li je forma editovana, kroz globalnu promenljivu isEdit, 
        /// i vrsi se dodavanje pitanja na listu pitanja List<ForumTopicControl>.
        /// Konstruktor se poziva kroz event na Edit button ManageAdditionalActivities klase, 
        /// kao i na event AddForum button-a.
        /// <param name="selectedObject"></param>
        /// <param name="forum"></param>
        public ForumForm(LearningBase selectedObject, LamsForum forum = null)
        {
            Icon = Icon.FromHandle(Resources.lms_forum24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
            _questions = new List<ForumTopicControl>();
            if (forum == null)
            {
                InitForumContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                Forum = forum;
                isEdit = true;
            }
            naslovTextBox.TextChanged += NaslovTextBox_TextChanged;
            instrukcijeTextBox.TextChanged += InstrukcijeTextBox_TextChanged;
            naslovTextBox.Text = Forum.Title;
            instrukcijeTextBox.Text = Forum.Instructions;
            if (isEdit)
            {
                foreach (var question in Forum.Messages.Message)
                {
                    _questions.Add(new ForumTopicControl(question, this));
                }
            }
            RelocateControls();
        }
        /// <summary>
        /// Metoda koja hvata promenu teksta u okviru instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstrukcijeTextBox_TextChanged(object sender, EventArgs e)
        {
            Forum.Instructions = instrukcijeTextBox.Text;
        }
        /// <summary>
        /// Metoda koja hvata promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            Forum.Title = naslovTextBox.Text;
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje novog pitanja
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            var novoPitanje = new LamsForum.Message();
            if (newQ)
            {
                novoPitanje.Subject = "Prva tema foruma";
                novoPitanje.Body = "Prva opis foruma";
            }
            if (_questions.Count > 0)
            {
                novoPitanje.SequenceId = (int.Parse(_questions[_questions.Count - 1].Pitanje.SequenceId) + 1) + "";
            }
            else
            {
                novoPitanje.SequenceId = "1";
            }
            var question = new ForumTopicControl(novoPitanje, this);
            Forum.Messages.Message.Add(novoPitanje);
            _questions.Add(question);
        }
        /// <summary>
        /// Event na button dodaj, koja poziva metodu za dodavanje novog pitanja i vrsi relokaciju kontrola
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dodajButton_Click(object sender, EventArgs e)
        {
            Add();
            RelocateControls();
        }
        /// <summary>
        /// Metoda koja layout prilagodjava parentu - Form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            SuspendLayout();

            if (Parent != null)
            {
                Width = Parent.Width;
                Height = Parent.Height;
            }
            RelocateControls(false);

            ResumeLayout();
        }
        /// <summary>
        /// Metoda koja vrsi relokaciju kontrola
        /// </summary>
        /// <param name="suspendLayout"></param>
        public void RelocateControls(bool suspendLayout = true)
        {
            if (suspendLayout)
            {
                SuspendLayout();
            }

            var w = panelListQA.Width - 20;
            panelListQA.Controls.Clear();
            for (int i = 0; i < _questions.Count; i++)
            {
                var question = _questions[i];
                panelListQA.Controls.Add(question);
                question.Width = w;
                question.TabIndex = i;
                question.btnUp.Enabled = i != 0;
                question.btnDown.Enabled = i != _questions.Count - 1;
            }

            ResumeLayout();
            vScrollBar.Visible = !panelListQA.VerticalScroll.Visible;
        }
        /// <summary>
        /// Event koja vrsi validaciju unetog naslova, instrukcije, teme i teksta teme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (Forum.Title == "" || Forum.Title == null)
            {
                MessageBox.Show("Niste definisali naslov za Forum");
                isError = true;
            }
            if (Forum.Instructions == "" || Forum.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije za Forum");
                isError = true;
            }
            if (Forum.Messages.Message.Count == 0)
            {
                MessageBox.Show("Niste definisali barem jednu temu");
                isError = true;
            }
            foreach (var que in Forum.Messages.Message)
            {
                if (que.Body == "" || que.Subject == null)
                {
                    MessageBox.Show("Morate definisati tekst za temu broj " + que.SequenceId);
                    isError = true;
                }
            }
            if (!isError)
            {   
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.Forum);
                }
                this.Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void ForumControlForm_Load(object sender, EventArgs e)
        {

        }

        private void ForumControlForm_Resize(object sender, EventArgs e)
        {
            var w = panelListQA.Width - 20;
            foreach (var control in _questions)
            {
                control.Width = w;
            }
        }
    }
}
