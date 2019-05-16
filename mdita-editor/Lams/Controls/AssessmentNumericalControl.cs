using System;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Forms;

namespace mDitaEditor.Lams.Controls
{
    public partial class AssessmentNumericalControl : UserControl
    {
        public bool isEdit = false;
        public AssessmentNumericalForm ParentControl;
        public LearningBase LearningObject;
        public LamsAssessment.AssessmentQuestionOption AssessmentQuestionOption;

        //    public AssessmentQuestionOption AssessmentQuestionOp
        //    {

        //        get; set;

        //}

        public AssessmentNumericalControl()
        {
            AssessmentQuestionOption = new LamsAssessment.AssessmentQuestionOption();

        }



        public AssessmentNumericalControl(LamsAssessment.AssessmentQuestionOption assqp, AssessmentNumericalForm parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            AssessmentQuestionOption = assqp;
            this.ParentControl = parent;
            odgovorTextBox.Text += AssessmentQuestionOption.OptionFloat;
            odgovorTextBox.TextChanged += odgovorTextBox_TextChanged;
           

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
            AssessmentQuestionOption.OptionFloat = odgovorTextBox.Text;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent != null)
            {
                Width = Parent.Width - 20;
            }
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (odgovorTextBox.Text != null)
            {
                odgovorTextBox.Dispose();
            }
        }

        /// <summary>
        /// Event na button Up za pomeranje kontrole na gore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            Move(true);
        }
        /// <summary>
        /// Event na button delete za brisanje kontrole koji poziva metodu Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }
        /// <summary>
        /// Metoda koja vrsi zamenu mesta kontrola
        /// </summary>
        /// <param name="up"></param>
        public new void Move(bool up)
        {
            var list = ParentControl._answers;
            int index = list.IndexOf(this);
            int newIndex = index + (up ? -1 : 1);

            if (newIndex < 0 || newIndex >= list.Count)
            {
                return;
            }
            list[index] = list[newIndex];
            list[newIndex] = this;

            var list2 = ParentControl.AssessmentQuestion.Options.AssessmentQuestionOption;
            int index2 = list2.IndexOf(this.AssessmentQuestionOption);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].SequenceId;
            list2[index2].SequenceId = this.AssessmentQuestionOption.SequenceId;
            list2[newIndex2] = this.AssessmentQuestionOption;
            list2[newIndex2].SequenceId = dipslayTemp;
            ParentControl.RelocateControls();

        }
        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {


            ParentControl._answers.Remove(this);
            ParentControl.AssessmentQuestion.Options.AssessmentQuestionOption.Remove(AssessmentQuestionOption);
            ParentControl.RelocateControls();
            Dispose();
        }
        /// <summary>
        /// Event na button Down za pomeranje kontrole na dole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            Move(false);

        }

        private void AssessmentNumericalAnswerAddControl_Load(object sender, EventArgs e)
        {

        }
    }
}
