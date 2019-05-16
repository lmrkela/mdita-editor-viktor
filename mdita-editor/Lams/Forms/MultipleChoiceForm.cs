using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class MultipleChoiceForm : Form
    {
        public bool isEdit = false;
        public List<McQuestionAnswerControl> _questions;

        public LamsMultipleChoice LamsMultipleChoice;
        
       

        public LearningContent LearningContent;
        //  public Sectiondiv _sectiondiv;
        public LearningBase LearningObject;
        public MultipleChoiceForm()
        {
            InitializeComponent();
        }


        public void InitForumContent()
        {
            LamsMultipleChoice = new LamsMultipleChoice();
          

        }
        /// <summary>
        /// Konstruktor kome se prosledjuju parametri selectedObject i mccontentmc, 
        /// proverava se da li je mccontentmc null, ukoliko jeste, vrsi se inicijalizacija 
        /// kontenta LamsMultipleChoice, u suprotnom kontent LamsMultipleChoice-a postaje ono sto se prosledjuje
        /// kroz sam parametar mccontentmc. 
        /// Takodje, vrsi se provera da li je forma editovana, kroz globalnu promenljivu isEdit, 
        /// i vrsi se dodavanje pitanja na listu pitanja List<McQuestionAnswerControl> i 
        /// dodavanje odgovora na listu odgovora List<McAnswerControl>.
        /// Konstruktor se poziva kroz event na Edit button ManageAdditionalActivities klase, 
        /// kao i na event AddMC button-a.
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <param name="mccontentmc"></param>
        public MultipleChoiceForm(LearningBase selectedObject, LamsMultipleChoice mccontentmc = null)
        {
            Icon = Icon.FromHandle(Resources.lms_multiple_choice24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
            _questions = new List<McQuestionAnswerControl>();

            if (mccontentmc == null)
            {
                InitForumContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsMultipleChoice = mccontentmc;
                isEdit = true;
            }
            naslovTextBox.TextChanged += NaslovTextBox_TextChanged;
            instrukcijeTextBox.TextChanged += InstrukcijeTextBox_TextChanged;
            naslovTextBox.Text = LamsMultipleChoice.Title;
            instrukcijeTextBox.Text = LamsMultipleChoice.Instructions;
            if (isEdit)
            {
                foreach (LamsMultipleChoice.McQueContentMc questions in LamsMultipleChoice.McQueContents.McQueContentMc)
                {
                    var questionControl = new McQuestionAnswerControl(LearningObject, questions, this, LamsMultipleChoice);
                    _questions.Add(questionControl);
                    
                    foreach(LamsMultipleChoice.McOptsContent content in questions.McOptionsContents.McOptsContent)
                    {
                        questionControl._questions.Add(new McAnswerControl(content, questionControl));
                    }
                    questionControl.RelocateControls();

                }
            }
            RelocateControls();
        }
        /// <summary>
        /// Event na promenu teksta u okviru instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstrukcijeTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsMultipleChoice.Instructions = instrukcijeTextBox.Text;
        }
        /// <summary>
        /// Event na promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsMultipleChoice.Title = naslovTextBox.Text;
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje novog pitanja
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            LamsMultipleChoice.McQueContentMc novoPitanje = new LamsMultipleChoice.McQueContentMc();
            if (newQ)
            {
                novoPitanje.Question = "Prvo pitanje";
            }
            if (_questions.Count > 0)
            {
                novoPitanje.DisplayOrder = (int.Parse(_questions[_questions.Count - 1].McQueContentMc.DisplayOrder) + 1) + "";
            }
            else
            {
                novoPitanje.DisplayOrder = "1";
            }
                var question = new McQuestionAnswerControl(LearningObject, novoPitanje, this, LamsMultipleChoice);
                LamsMultipleChoice.McQueContents.McQueContentMc.Add(novoPitanje);
                _questions.Add(question);
                RelocateControls();
        }
        /// <summary>
        /// Event na button dodaj, koja poziva metodu za dodavanje novog pitanja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dodajButton_Click(object sender, EventArgs e)
        {
            Add();
        }
        /// <summary>
        ///  Metoda koja layout prilagodjava parentu - Form
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
        ///  Metoda koja vrsi relokaciju kontrola
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
                question.Width = w;
                panelListQA.Controls.Add(question);
                question.TabIndex = i;
                question.btnUp.Enabled = i != 0;
                question.btnDown.Enabled = i != _questions.Count - 1;
            }

            ResumeLayout();
            vScrollBar.Visible = !panelListQA.VerticalScroll.Visible;
        }

        /// <summary>
        /// Event koja vrsi validaciju unetog naslova, instrukcije, broja pitanja i odgovora, teksta za pitanje i odgovor, checkbox-a i tacnog odgovora, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsMultipleChoice.Title == "" || LamsMultipleChoice.Title == null)
            {
                MessageBox.Show("Niste definisali naslov");
                isError = true;
            }
            if (LamsMultipleChoice.Instructions == "" || LamsMultipleChoice.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije");
                isError = true;
            }
            if (LamsMultipleChoice.McQueContents.McQueContentMc.Count == 0)
            {
                MessageBox.Show("Niste definisali bar jedno pitanje");
                isError = true;
            }
            foreach (LamsMultipleChoice.McQueContentMc pitanje in LamsMultipleChoice.McQueContents.McQueContentMc)
            {

                if (pitanje.McOptionsContents.McOptsContent.Count < 2)
                {
                    MessageBox.Show("Niste definisali bar dva odgovora za pitanje broj " + pitanje.DisplayOrder);
                    isError = true;
                }
            }
            foreach (LamsMultipleChoice.McQueContentMc pitanje in LamsMultipleChoice.McQueContents.McQueContentMc)
            {
              
                foreach(LamsMultipleChoice.McOptsContent odgovor2 in pitanje.McOptionsContents.McOptsContent)

                    if (odgovor2.McQueOptionText == "")
                    {
                        MessageBox.Show("Morate definisati tekst za odgovor broj " + odgovor2.DisplayOrder);
                        isError = true;
                    }
                
            }
            foreach (LamsMultipleChoice.McQueContentMc pitanje in LamsMultipleChoice.McQueContents.McQueContentMc)
            {

                int countFalseOdgovora = 0;
                foreach (LamsMultipleChoice.McOptsContent odgovor2 in pitanje.McOptionsContents.McOptsContent)
                {
                    if (odgovor2.CorrectOption == "false")
                    {
                        countFalseOdgovora++;
                    }
                }
                if(countFalseOdgovora == pitanje.McOptionsContents.McOptsContent.Count)
                {
                    MessageBox.Show("Morate definisati tačan odgovor za pitanje broj " +pitanje.DisplayOrder);
                    isError = true;
                }
            }
            foreach (LamsMultipleChoice.McQueContentMc que in LamsMultipleChoice.McQueContents.McQueContentMc)
            {
                if (que.Question == "")
                {
                    MessageBox.Show("Morate definisati tekst za pitanje broj " + que.DisplayOrder);
                    isError = true;
                }
            }

            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsMultipleChoice);
                }
                this.Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void MCControlForm_Load(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panelListQA_Paint(object sender, PaintEventArgs e)
        {

        }

        private void MCControlForm_SizeChanged(object sender, EventArgs e)
        {
            var w = panelListQA.Width - 20;
            foreach (var control in _questions)
            {
                control.Width = w;
            }
        }
    }
}
