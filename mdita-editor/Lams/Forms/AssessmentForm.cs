using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Project;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class AssessmentForm : Form
    {
        public bool isEdit = false;
        public LamsAssessment LamsAssessment;
        public LearningContent LearningContent;
        public LearningBase LearningObject;

        public string tipPitanja;
        public LamsAssessment.AssessmentQuestion AssessmentQuestion;

        public void InitAssContent()
        {
            LamsAssessment = new LamsAssessment();
        }

        public AssessmentForm(LearningBase selectedObject, LamsAssessment assessmentass = null)
        {
            Icon = Icon.FromHandle(Resources.lms_assesment24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
            int index;
            string result = null;

            if (assessmentass == null)
            {
                InitAssContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsAssessment = assessmentass;
                isEdit = true;
                if (assessmentass.QuestionsAss.AssessmentQuestion.Count > 0)
                {
                    numericUpDown2.Value = assessmentass.QuestionsAss.AssessmentQuestion[0].DefaultGrade;
                }
            }
            naslovTextBox.TextChanged += NaslovTextBox_TextChanged;
            instrukcijeTextBox.TextChanged += InstrukcijeTextBox_TextChanged;
            naslovTextBox.Text = LamsAssessment.Title;
            naslovTextBox.Text = "Test provere znanja";
            instrukcijeTextBox.Text = LamsAssessment.Instructions;

            if(instrukcijeTextBox.Text.Contains( "<div><iframe" ))
            {
                index = instrukcijeTextBox.Text.IndexOf( "<div><iframe" );
                result = instrukcijeTextBox.Text.Substring( 0, index );
                instrukcijeTextBox.Text = result;
                checkBox2.Checked = true;
            }

            comboBox1.SelectedValueChanged += comboBox1_SelectedValueChanged;
            if (LamsAssessment.QuestionReferences != null && LamsAssessment.QuestionReferences.QuestionReference != null)
            {
                numericUpDown1.Value = LamsAssessment.QuestionReferences.QuestionReference.Count;
            }
            RelocateControls();
            RefreshList();
            comboBox1.SelectedIndex = 0;
        }

        /// <summary>
        /// Event na promenu teksta u okviru instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstrukcijeTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsAssessment.Instructions = instrukcijeTextBox.Text;
        }
        /// <summary>
        /// Event na promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsAssessment.Title = naslovTextBox.Text;
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje novog pitanja
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            int brojPitanja = LamsAssessment.QuestionsAss.AssessmentQuestion.Count;

            if (comboBox1.SelectedIndex == 0)
            {

                brojPitanja++;
                var question = new AssessmentMcForm(LamsAssessment, this, null);
                question.Show();
                question.AssessmentQuestion.DefaultGrade = (int) numericUpDown2.Value;
                RelocateControls();
            }

            if (comboBox1.SelectedIndex == 1)
            {
                brojPitanja++;
                var question2 = new AssessmentTfForm(this, LamsAssessment);
                question2.AssessmentQuestion.DefaultGrade = (int) numericUpDown2.Value;
                question2.Show();
                RelocateControls();
            }
            
            if (comboBox1.SelectedIndex == 2)
            {

                brojPitanja++;
                var question = new AssessmentNumericalForm(LamsAssessment, this, null);
                question.Show();
                question.AssessmentQuestion.DefaultGrade = (int) numericUpDown2.Value;
                RelocateControls();
            }
            if (comboBox1.SelectedIndex == 3)
            {
                brojPitanja++;
                var question = new AssessmentEssayForm(LamsAssessment,this,false,null);
                question.Show();
                question.AssessmentQuestion.DefaultGrade = (int)numericUpDown2.Value;
                RelocateControls();

            }
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


            ResumeLayout();
        }
        /// <summary>
        /// Event koja vrsi validaciju unetog naslova, instrukcije, broja pitanja i odgovora, teksta za pitanje i odgovor, checkbox-a i tacnog odgovora, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsAssessment.Title == "" || LamsAssessment.Title == null)
            {
                MessageBox.Show("Niste definisali naslov");
                isError = true;
            }
            if (LamsAssessment.Instructions == "" || LamsAssessment.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije");
                isError = true;
            }
            if (LamsAssessment.QuestionsAss.AssessmentQuestion.Count == 0)
            {
                MessageBox.Show("Niste definisali bar jedno pitanje");
                isError = true;
            }
            if (numericUpDown1.Value > listQuestionBank.Items.Count)
            {
                MessageBox.Show("Nemate dovoljno pitanja u banci za navedeni broj pitanja");
                isError = true;
            }
            if (numericUpDown1.Value == 0)
            {
                MessageBox.Show("Niste izabrali nijedno pitanje iz banke. Unesite broj pitanje koje želite da povučete iz banke");
                isError = true;
            }
            if (!isError)
            {
                if (!isEdit)
                {
                    if ( checkBox2.Checked )
                    {
                        string mindmap =
                            "<div><iframe allowfullscreen=\"no\" frameborder=\"0\" height=\"700\" scrolling=\"no\" src=\"http://mdita.metropolitan.ac.rs/mindmap/mindblow/mindmap.html?predmet=" +
                            ProjectSingleton.Project.CourseCode + "&amp;lekcija=" +
                            ProjectSingleton.Project.LessonNumber + "\" width=\"900\"></iframe></div>";
                            LamsAssessment.Instructions = LamsAssessment.Instructions + mindmap;

                        instrukcijeTextBox.Text = LamsAssessment.Instructions + mindmap;
                        LearningObject.ToolList.Add( this.LamsAssessment );
                    }
                    else
                    {
                        LearningObject.ToolList.Add(this.LamsAssessment);
                    }
                }



                UpdateReference();

                this.Close();
                DialogResult = DialogResult.OK;
            }

        }

        public void UpdateReference()
        {
            LamsAssessment.QuestionReferences.QuestionReference.Clear();
            for (int i = 0; i < numericUpDown1.Value; i++)
            {
                LamsAssessment.QuestionReferences.QuestionReference.Add(new LamsAssessment.QuestionReference(i + 1, (int)numericUpDown2.Value));
            }
        }


        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            //String combo = comboBox1.SelectedItem.ToString();
            //if (combo == "Multiple choice") {
            //    AssessmentMCQuestionControl ass = new AssessmentMCQuestionControl();
            //}
        }

        public void RefreshList()
        {
            listQuestionBank.Items.Clear();
            foreach (LamsAssessment.AssessmentQuestion question in LamsAssessment.QuestionsAss.AssessmentQuestion)
            {
                listQuestionBank.Items.Add(question);
            }
        }

        private void AssessmentControlForm_Load(object sender, EventArgs e)
        {

        }

        public void RefreshSequenceIds()
        {
            int i = 1;
            foreach (LamsAssessment.AssessmentQuestion question in LamsAssessment.QuestionsAss.AssessmentQuestion)
            {
                question.SequenceId = i + "";
                i++;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if ( listQuestionBank.SelectedItem != null &&
                 listQuestionBank.SelectedItem is LamsAssessment.AssessmentQuestion )
            {

                LamsAssessment.AssessmentQuestion quesiton =
                    (LamsAssessment.AssessmentQuestion) listQuestionBank.SelectedItem;
                if ( quesiton.Type == "1" )
                {
                    AssessmentMcForm addQAForm = new AssessmentMcForm( LamsAssessment, this, quesiton );
                    addQAForm.ShowDialog();
                }

                if ( quesiton.Type == "5" )
                {
                    AssessmentTfForm addQAForm = new AssessmentTfForm( this, LamsAssessment, quesiton );
                    addQAForm.ShowDialog();
                }
                if ( quesiton.Type == "4" )
                {
                    AssessmentNumericalForm addQAForm = new AssessmentNumericalForm( LamsAssessment, this, quesiton );
                    addQAForm.ShowDialog();
                }
                if ( quesiton.Type == "6" )
                {
                    var question = new AssessmentEssayForm(LamsAssessment, this, true, quesiton);
                    question.ShowDialog();
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            foreach (LamsAssessment.AssessmentQuestion question in LamsAssessment.QuestionsAss.AssessmentQuestion)
            {
                question.DefaultGrade = (int) numericUpDown2.Value;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listQuestionBank.SelectedItem != null)
            {
                LamsAssessment.AssessmentQuestion question = (LamsAssessment.AssessmentQuestion)listQuestionBank.SelectedItem;
                listQuestionBank.Items.Remove(question);
                LamsAssessment.QuestionsAss.AssessmentQuestion.Remove(question);
                RefreshSequenceIds();
            }
        }

        private void AssessmentControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(numericUpDown1.Value > listQuestionBank.Items.Count)
            {
                numericUpDown1.Value = LamsAssessment.QuestionReferences.QuestionReference.Count;
            }
            UpdateReference();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
