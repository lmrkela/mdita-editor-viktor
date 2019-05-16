using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Lams.Forms
{
    public partial class AssessmentTfForm : Form
    {


        public bool isEdit = false;
        public AssessmentForm ParentControl;
        public List<AssessmentMcControl> _questions;
        public LamsAssessment.AssessmentQuestion AssessmentQuestion;
        public LamsAssessment LamsAssessment;
        WebClient webClient = new WebClient();
        public bool isSaveClose = false;
        public string url;
        public string oldValue = null;

        public AssessmentTfForm()
        {
            InitializeComponent();

           
        }

        public AssessmentTfForm(AssessmentForm parent, LamsAssessment assessmentass, LamsAssessment.AssessmentQuestion asq = null) : this()
        {
            
            LamsAssessment = assessmentass;
            AssessmentQuestion = asq;
            this.ParentControl = parent;
            _questions = new List<AssessmentMcControl>();

            if (AssessmentQuestion == null)
            {
                AssessmentQuestion = new LamsAssessment.AssessmentQuestion();
                AssessmentQuestion.Type = "5";
                AssessmentQuestion.FeedbackOnCorrect = "Tačan odgovor";
                AssessmentQuestion.FeedbackOnIncorrect = "Netačan odgovor";
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                isEdit = true;
                comboBox1.SelectedIndex = (AssessmentQuestion.CorrectAnswer == "true") ? 0 : 1;
            }

            txtPitanjeNaziv.Text = AssessmentQuestion.Title;
            txtPitanjeText.Text = AssessmentQuestion.Question;


            try
            {
                string matchString = Regex.Match(AssessmentQuestion.Question, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                url = matchString;
                Regex regex = new Regex("<br\\/><img src='(.*?)'\\/>");
                oldValue = txtPitanjeText.Text;
                txtPitanjeText.Text = regex.Replace(txtPitanjeText.Text, "");
                pictureBoxPitanje.ImageLocation = url;

            }
            catch { }

            RelocateControls();
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


        private void txtPitanjeNaziv_TextChanged(object sender, EventArgs e)
        {
            AssessmentQuestion.Title = txtPitanjeNaziv.Text;

        }

        private void txtPitanjeText_TextChanged(object sender, EventArgs e)
        {

            AssessmentQuestion.Question = txtPitanjeText.Text;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent != null)
            {
                Width = Parent.Width - 20;
            }
        }

        public void RelocateControls(bool suspendLayout = true)
        {
            if (suspendLayout)
            {
                SuspendLayout();
            }

            ResumeLayout();
        }

        /// <summary>
        /// Metoda koja vrsi dodavanje novog odgovora
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {

            LamsAssessment.AssessmentQuestionOption noviOdgovor = new LamsAssessment.AssessmentQuestionOption();

            if (newQ)
            {
                noviOdgovor.OptionString = "Prvi odgovor";
                //   labelPitanje.Text = "Pitanje 1:";

            }
            if (_questions.Count > 0)
            {
                noviOdgovor.SequenceId = (int.Parse(_questions[_questions.Count - 1].AssessmentQuestionOption.SequenceId) + 1) + "";
                //  labelPitanje.Text = "Pitanje " + AssessmentQuestion.SequenceId + ":";
            }
            else
            {
                noviOdgovor.SequenceId = "1";
                //  labelPitanje.Text = "Pitanje " + AssessmentQuestion.SequenceId + ":";

            }
            //var question = new AssessmentMCAnswerAddControl(noviOdgovor, this);
            //AssessmentQuestion.Options.AssessmentQuestionOption.Add(noviOdgovor);
            //_questions.Add(question);
            //RelocateControls();
        }
        /// <summary>
        /// Event na button Answer koji poziva metodu Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddAnswer_Click(object sender, EventArgs e)
        {
            Add();
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

        public new void Move(bool up)
        {
            //var list = ParentControl._questions;
            //int index = list.IndexOf(this);
            //int newIndex = index + (up ? -1 : 1);

            //if (newIndex < 0 || newIndex >= list.Count)
            //{
            //    return;
            //}
            //list[index] = list[newIndex];
            //list[newIndex] = this;

            var list2 = ParentControl.LamsAssessment.QuestionsAss.AssessmentQuestion;
            int index2 = list2.IndexOf(this.AssessmentQuestion);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].SequenceId;
            list2[index2].SequenceId = this.AssessmentQuestion.SequenceId;
            list2[newIndex2] = this.AssessmentQuestion;
            list2[newIndex2].SequenceId = dipslayTemp;
            ParentControl.RelocateControls();

        }
        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {


            //  ParentControl._questions.Remove(this);
            ParentControl.LamsAssessment.QuestionsAss.AssessmentQuestion.Remove(AssessmentQuestion);
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

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            AssessmentQuestion.MultipleAnswersAllowed = comboBox1.Text;

        }



        /// <summary>
        /// Event koja vrsi validaciju unetog naslova, instrukcije, broja pitanja i odgovora, teksta za pitanje i odgovor, checkbox-a i tacnog odgovora, 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            isSaveClose = true;
            if (AssessmentQuestion.Title == "" || AssessmentQuestion.Title == null)
            {
                MessageBox.Show("Niste definisali pitanje" + AssessmentQuestion.SequenceId);
                isError = true;
            }
            if (AssessmentQuestion.Question == "" || AssessmentQuestion.Question == null)
            {
                MessageBox.Show("Niste definisali tekst za pitanje" + AssessmentQuestion.SequenceId);
                isError = true;
            }
           

            if (!isError)
            {
                if (url != "")
                {
                    AssessmentQuestion.Question = AssessmentQuestion.Question + "<br/><img src='" + url + "' style='max-width:600px;max-height:300px;'/>" ;
                }
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


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                AssessmentQuestion.CorrectAnswer = "true";
            }
            else
            {
                AssessmentQuestion.CorrectAnswer = "false";
            }
        }

        private void AssessmentTFQuestionControlForm_Load(object sender, EventArgs e)
        {

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
                        pictureBoxPitanje.Image = Image.FromFile(fileName);
                        NameValueCollection query = new NameValueCollection();
                        query.Add("pass", "da8s4ada1s8d4sadas484ds89a4d8a");
                        query.Add("lesson", ProjectSingleton.Project.LessonNumber);
                        query.Add("subject", ProjectSingleton.Project.CourseCode);
                        webClient.QueryString = query;
                        //webClient.Headers["Content-type"] = "multipart/form-data; boundary=---------------------------" + _boundaryNo;
                        new Thread(() => webClient.UploadFileAsync(new Uri("http://mdita.metropolitan.ac.rs/uploader.php"), "POST", fileName)).Start();

                        //AssessmentMCQuestionControlForm ass = new AssessmentMCQuestionControlForm();

                        //ass.pictureBoxPitanje.Image = new Bitmap(dialog.FileName);
                        //this.Controls.Add(ass.pictureBoxPitanje);
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

        private void AssessmentTFQuestionControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaveClose)
            {
                AssessmentQuestion.Question = oldValue;
            }
        }

        private void labelPitanje_Click(object sender, EventArgs e)
        {

        }
    }
}
