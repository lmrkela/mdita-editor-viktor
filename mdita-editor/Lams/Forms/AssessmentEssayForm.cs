using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Controls;

namespace mDitaEditor.Lams.Forms
{
    public partial class AssessmentEssayForm : Form
    {

        public bool isEdit = false;

        public AssessmentForm ParentControl;
        public LamsAssessment.AssessmentQuestion AssessmentQuestion
        {
            get; set;
        }
        

        public LamsAssessment LamsAssessment;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
     



        public AssessmentEssayForm()
        {
            InitializeComponent();
        }

        public void InitAssEssayContent()
        {

         
           AssessmentQuestion = new LamsAssessment.AssessmentQuestion
            {
                Type = "6",
                Question = "",
                AnswerRequired = "true",
                

           };

             

        }

        public AssessmentEssayForm( LamsAssessment ass, AssessmentForm form, bool edit,
            LamsAssessment.AssessmentQuestion assessmentQuestion = null,
            LamsAssessment.AssessmentQuestionOption assessmentQuestionOption = null ) : this()
        {
            isEdit = edit;

            this.LamsAssessment = ass;
            this.ParentControl = form;

            

            if (assessmentQuestion == null && assessmentQuestionOption == null)
            {
                InitAssEssayContent();
              
            }
            else
            {
                AssessmentQuestion = assessmentQuestion;
             
            }


            if ( edit )
            {

                if ( AssessmentQuestion != null )
                {
                    TitleText.Text = AssessmentQuestion.Title;
                    textBox1.Text = AssessmentQuestion.Question;

                }
            }



        }

        private void TitleText_TextChanged(object sender, EventArgs e)
        {
           
            AssessmentQuestion.Title = TitleText.Text;
           
        }

        public void RelocateControls(bool suspendLayout = true)
        {
           
        }




        public void Add(bool newQ = false)
        {

        }

       

        private void button1_Click(object sender, EventArgs e)
        {
            bool isError = false;
      


            //AssessmentQuestion.Title = TitleText.Text;


            if (string.IsNullOrEmpty( AssessmentQuestion.Title ))
            {
                
                MessageBox.Show("Niste definisali naslov!");
                isError = true;
            }

            if (string.IsNullOrEmpty(AssessmentQuestion.Question))
            {

                MessageBox.Show("Niste definisali pitanje!");
                isError = true;
            }

            if (!isError)
            {
                this.Close();
                DialogResult = DialogResult.OK;
                if (!isEdit)
                {
                    LamsAssessment.QuestionsAss.AssessmentQuestion.Add(this.AssessmentQuestion);
                }
                ParentControl.RefreshSequenceIds();
                ParentControl.RefreshList();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            AssessmentQuestion.Question = textBox1.Text;
        }
    }
}
