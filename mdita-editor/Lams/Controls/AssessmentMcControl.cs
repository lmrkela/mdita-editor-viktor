using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Lams.Controls
{
    public partial class AssessmentMcControl : UserControl
    {

        public bool isEdit = false;
        public AssessmentMcForm ParentControl;
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


        //    public AssessmentQuestionOption AssessmentQuestionOp
        //    {

        //        get; set;

        //}

        public AssessmentMcControl()
        {
            AssessmentQuestionOption = new LamsAssessment.AssessmentQuestionOption();
            AssessmentQuestion = new LamsAssessment.AssessmentQuestion();


        }



        public AssessmentMcControl(LamsAssessment.AssessmentQuestionOption assqp, AssessmentMcForm parent)
        {

            InitializeComponent();
            Disposed += OnDisposed;
            AssessmentQuestionOption = assqp;
            this.ParentControl = parent;
            odgovorTextBox.Text += AssessmentQuestionOption.OptionString;
            odgovorTextBox.TextChanged += odgovorTextBox_TextChanged;
            box.Checked = (AssessmentQuestionOption.Grade>0) ? true : false;
            box.CheckedChanged += box_CheckedChanged;
            webClient.UploadFileCompleted += UploadFileCompletedCallback;
            webClient.UploadProgressChanged += UploadProgressCallback;


        }



        public void UploadFileCompletedCallback(object sender, UploadFileCompletedEventArgs e)
        {
            BeginInvoke(
                new MethodInvoker(() =>
                {
                    url = Encoding.UTF8.GetString(e.Result);
                    progressBarUpload.Value = 100;

                    if (url != "")
                    {
                        pictureBoxOdgovor.ImageLocation = url;
                    }



                }));

           
        }

        private void UploadProgressCallback(object sender, UploadProgressChangedEventArgs e)
        {

            BeginInvoke(
                new MethodInvoker(() =>
                {
                    progressBarUpload.Value = e.ProgressPercentage;
                }));
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



        private void box_CheckedChanged(object sender, EventArgs e)
        {
            AssessmentQuestionOption.Grade = (box.Checked) ? 1.0 : 0;
        }


        private void dodajSlikuPitanje_Click(object sender, EventArgs e)
        {
            if (Util.checkIfHasInternetConnection())
            {

                string _boundaryNo = DateTime.Now.Ticks.ToString("x");
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "JPG | *.jpg; *.jpeg | PNG | *.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Util.checkIfHasInternetConnection())
                    {
                        string fileName = dialog.FileName;
                        pictureBoxOdgovor.Image = Image.FromFile(fileName);
                        NameValueCollection query = new NameValueCollection();
                        query.Add("pass", "da8s4ada1s8d4sadas484ds89a4d8a");
                        query.Add("lesson", ProjectSingleton.Project.LessonNumber);
                        query.Add("subject", ProjectSingleton.Project.CourseCode);
                        webClient.QueryString = query;
                        //webClient.Headers["Content-type"] = "multipart/form-data; boundary=---------------------------" + _boundaryNo;
                        new Thread(() => webClient.UploadFileAsync(new Uri("http://mdita.metropolitan.ac.rs/uploader.php"), "POST", fileName)).Start();

                    }
                    else {
                        MessageBox.Show("Potrebna Vam je internet konekcija");
                    }

                }
            }
            else {
                MessageBox.Show("Potrebna Vam je internet konekcija");
            }


        }

        private void AssessmentMCAnswerAddControl_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
