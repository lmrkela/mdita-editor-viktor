using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Lams.Forms
{
    public partial class AssessmentNumericalForm : Form
    {
        public bool isEdit = false;
        public AssessmentForm ParentControl;
        public List<AssessmentNumericalControl> _answers;
        public LamsAssessment.AssessmentQuestion AssessmentQuestion
        {
            get; set;
        }
        public LamsAssessment AssessmentAss;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
        public LamsAssessment.AssessmentQuestionOption AssessmentQuestionOption;
        public LamsAssessment.QuestionReference QuestionReference;
        WebClient webClient = new WebClient();
        public bool isSaveClose = false;
        public string url;
        public string oldValue = null;


        public AssessmentNumericalForm()
        {
            InitializeComponent();
        }

        public void InitAssNumContent()
        {
            AssessmentQuestion = new LamsAssessment.AssessmentQuestion();
            AssessmentQuestionOption = new LamsAssessment.AssessmentQuestionOption();
            QuestionReference = new LamsAssessment.QuestionReference();
            AssessmentQuestion.Type = "4";
            AssessmentQuestion.Question = "Pitanje";
            AssessmentQuestion.MultipleAnswersAllowed = "false";
            AssessmentAss.AttemptsAllowed = "1";
            AssessmentAss.Shuffled = "false";
            AssessmentAss.AllowQuestionFeedback = "false";
            AssessmentAss.AllowRightAnswersAfterQuestion = "false";
            AssessmentAss.AllowWrongAnswersAfterQuestion = "false";
            AssessmentAss.AllowGradesAfterAttempt = "false";
            AssessmentAss.DisplaySummary = "false";
            AssessmentQuestionOption.Grade = 1.0;
            QuestionReference.RandomQuestion = "false";               
        }
        public AssessmentNumericalForm(LamsAssessment ass, AssessmentForm form, LamsAssessment.AssessmentQuestion assessmentQuestion = null) : this()
        {
            this.AssessmentAss = ass;
            this.ParentControl = form;
            _answers = new List<AssessmentNumericalControl>();
            if (assessmentQuestion == null)
            {
                InitAssNumContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                AssessmentQuestion = assessmentQuestion;
                isEdit = true;
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


            if (isEdit)
            {
                foreach (LamsAssessment.AssessmentQuestionOption questions in AssessmentQuestion.Options.AssessmentQuestionOption)
                {
                    var answerControl = new AssessmentNumericalControl(questions, this);
                    _answers.Add(answerControl);


                    answerControl.RelocateControls();

                }
            }
            RelocateControls();
            webClient.UploadFileCompleted += UploadFileCompletedCallback;
            webClient.UploadProgressChanged += UploadProgressCallback;
        }



        private void txtPitanjeNaziv_TextChanged(object sender, EventArgs e)
        {
            AssessmentQuestion.Title = txtPitanjeNaziv.Text;

        }

        private void txtPitanjeText_TextChanged(object sender, EventArgs e)
        {
            AssessmentQuestion.Question = txtPitanjeText.Text;
        }

        private void OnDisposed(object sender, EventArgs e)
        {
            if (txtPitanjeNaziv.Text != null)
            {
                txtPitanjeNaziv.Dispose();
            }
            if (txtPitanjeText.Text != null)
            {
                txtPitanjeText.Dispose();
            }


        }

        public void RelocateControls(bool suspendLayout = true)
        {
            if (suspendLayout)
            {
                SuspendLayout();
            }

            var w = panelOdgovori.Width - 20;
            panelOdgovori.Controls.Clear();
            for (int i = 0; i < _answers.Count; i++)
            {
                var question = _answers[i];
                question.Width = w;
                panelOdgovori.Controls.Add(question);
                question.TabIndex = i;
            }

            ResumeLayout();
            vScrollBar.Visible = !panelOdgovori.VerticalScroll.Visible;
        }

        /// <summary>
        /// Metoda koja vrsi dodavanje novog odgovora
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {

            LamsAssessment.AssessmentQuestionOption noviOdgovor = new LamsAssessment.AssessmentQuestionOption();
            noviOdgovor.Grade = 1.0;
            if (newQ)
            {
                noviOdgovor.OptionFloat = "0.0";
                //   labelPitanje.Text = "Pitanje 1:";

            }
            if (_answers.Count > 0)
            {
                noviOdgovor.SequenceId = (int.Parse(_answers[_answers.Count - 1].AssessmentQuestionOption.SequenceId) + 1) + "";
                //  labelPitanje.Text = "Pitanje " + AssessmentQuestion.SequenceId + ":";
                
            }
            else
            {
                noviOdgovor.SequenceId = "0";
                //  labelPitanje.Text = "Pitanje " + AssessmentQuestion.SequenceId + ":";

            }
            var question = new AssessmentNumericalControl(noviOdgovor, this);
            AssessmentQuestion.Options.AssessmentQuestionOption.Add(noviOdgovor);
            _answers.Add(question);
            RelocateControls();
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
        /// Event na button delete za brisanje kontrole koji poziva metodu Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {

            ParentControl.LamsAssessment.QuestionsAss.AssessmentQuestion.Remove(AssessmentQuestion);
            ParentControl.RelocateControls();
            Dispose();
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

            if (AssessmentQuestion.Options.AssessmentQuestionOption.Count == 0)
            {
                MessageBox.Show("Niste definisali odgovore za pitanje" + AssessmentQuestion.SequenceId);
                isError = true;

            }
            //if (AssessmentQuestion.Options.AssessmentQuestionOption.Count < 3)
            //{
            //    MessageBox.Show("Niste definisali bar tri odgovora za pitanje" + AssessmentQuestion.SequenceId);
            //    isError = true;
            //}



            foreach (LamsAssessment.AssessmentQuestionOption odgovor2 in AssessmentQuestion.Options.AssessmentQuestionOption)
            {
                //if (odgovor2.OptionFloat == "" || odgovor2.OptionFloat == null)
                //{
                //    MessageBox.Show("Morate definisati broj za svaki odgovor" + odgovor2.SequenceId);
                //    isError = true;
                //}

                if (!Regex.IsMatch(odgovor2.OptionFloat, @"\d"))
                {
                    MessageBox.Show("Niste ispravno uneli broj za odgovor" + AssessmentQuestion.SequenceId);
                    isError = true;
                }

            }

            if (!isError)
            {
                if (url != "")
                {
                    AssessmentQuestion.Question = AssessmentQuestion.Question + "<br/><img src='" + url + "' style='max-width:600px;max-height:300px;'/>";
                }
                this.Close();
                DialogResult = DialogResult.OK;
                if (!isEdit)
                {
                    AssessmentAss.QuestionsAss.AssessmentQuestion.Add(this.AssessmentQuestion);
                }
                ParentControl.RefreshSequenceIds();
                ParentControl.RefreshList();
            }


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

        private void AssessmentNumericalControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isSaveClose)
            {
                AssessmentQuestion.Question = oldValue;
            }
        }

        private void AssessmentNumericalControlForm_Resize(object sender, EventArgs e)
        {
            var w = panelOdgovori.Width - 20;
            foreach (var control in _answers)
            {
                control.Width = w;
            }
        }
    }
}
