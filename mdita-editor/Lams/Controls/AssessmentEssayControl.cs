using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Forms;

namespace mDitaEditor.Lams.Controls
{
    public partial class AssessmentEssayControl : UserControl
    {

        public bool isEdit = false;
        public AssessmentEssayForm ParentControl;
        public LearningBase LearningObject;
        public LamsAssessment.AssessmentQuestionOption AssessmentQuestionOption;
        public LamsAssessment.AssessmentQuestion AssessmentQuestion
        {
            get; set;
        }
        WebClient webClient = new WebClient();

        public string url;

        public string urlValue
        {
            get { return url; }
        }

        public bool isSaveClose = false;
        public string oldValue = null;

        public AssessmentEssayControl(LamsAssessment.AssessmentQuestionOption assqp, AssessmentEssayForm parent)
        {
            InitializeComponent();
            
            AssessmentQuestionOption = assqp;
            this.ParentControl = parent;
            odgovorTextBox.Text += AssessmentQuestionOption.OptionString;
            odgovorTextBox.TextChanged += odgovorTextBox_TextChanged;
            
            
        }

        public AssessmentEssayControl()
        {
            AssessmentQuestionOption = new LamsAssessment.AssessmentQuestionOption();
            AssessmentQuestion = new LamsAssessment.AssessmentQuestion();


        }

        public void RelocateControls(bool suspendLayout = true)
        {
            if (suspendLayout)
            {
                SuspendLayout();
            }



            ResumeLayout();
        }


        private void odgovorTextBox_TextChanged(object sender, EventArgs e)
        {

            AssessmentQuestionOption.OptionString = odgovorTextBox.Text;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (odgovorTextBox.Text != null)
            {
                odgovorTextBox.Dispose();
            }
        }
    }
}
