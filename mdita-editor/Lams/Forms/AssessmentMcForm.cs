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
    public partial class AssessmentMcForm : Form
    {

        public bool isEdit = false;
        public AssessmentForm ParentControl;
        public List<AssessmentMcControl> _answers;
        public LamsAssessment.AssessmentQuestion AssessmentQuestion
        {
            get; set;
        }
        public LamsAssessment.AssessmentQuestionOption AssessmentQuestionOption
        {
            get; set;
        }
        public LamsAssessment LamsAssessment;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
        WebClient webClient = new WebClient();

        public string url;
        public bool isSaveClose = false;
        public string oldValue = null;


        public AssessmentMcForm()
        {
            InitializeComponent();

        }


        public void InitAssMCContent()
        {
            AssessmentQuestion = new LamsAssessment.AssessmentQuestion();
            AssessmentQuestionOption = new LamsAssessment.AssessmentQuestionOption();
            AssessmentQuestion.Type = "1";
            AssessmentQuestion.Question = "Pitanje";

        }
        public AssessmentMcForm(LamsAssessment ass, AssessmentForm form, LamsAssessment.AssessmentQuestion assessmentQuestion = null, LamsAssessment.AssessmentQuestionOption assessmentQuestionOption = null ) : this()
        {

            this.LamsAssessment = ass;
            this.ParentControl = form;
            _answers = new List<AssessmentMcControl>();
            if (assessmentQuestion == null && assessmentQuestionOption == null)
            {
                InitAssMCContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                AssessmentQuestion = assessmentQuestion;
                AssessmentQuestionOption = assessmentQuestionOption;

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

            boxShuffle.Checked = (AssessmentQuestion.Shuffle == "true") ? true : false;

            if (isEdit)
            {
                foreach (LamsAssessment.AssessmentQuestionOption questions in AssessmentQuestion.Options.AssessmentQuestionOption)
                {
                    var answerControl = new AssessmentMcControl(questions, this);
                    string matchString = Regex.Match(questions.OptionString, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
                    string newurl = matchString;
                    Regex regex = new Regex("<br\\/><img src='(.*?)'\\/>");
                    answerControl.oldValue = questions.OptionString;
                  
                        questions.OptionString = regex.Replace(questions.OptionString, "");
                    
                    answerControl.odgovorTextBox.Text = questions.OptionString;
                    answerControl.pictureBoxOdgovor.ImageLocation  = newurl;
                    _answers.Add(answerControl);
                    answerControl.RelocateControls();

                }
            }
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
                question.btnUp.Enabled = i != 0;
                question.btnDown.Enabled = i != _answers.Count - 1;
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

            if (newQ)
            {
                noviOdgovor.OptionString = "Prvi odgovor";
                //   labelPitanje.Text = "Pitanje 1:";

            }
            if (_answers.Count > 0)
            {
                noviOdgovor.SequenceId = (int.Parse(_answers[_answers.Count - 1].AssessmentQuestionOption.SequenceId) + 1) + "";
                //  labelPitanje.Text = "Pitanje " + AssessmentQuestion.SequenceId + ":";
            }
            else
            {
                noviOdgovor.SequenceId = "1";
                //  labelPitanje.Text = "Pitanje " + AssessmentQuestion.SequenceId + ":";

            }
            var question = new AssessmentMcControl(noviOdgovor, this);
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


        private void boxShuffle_CheckedChanged(object sender, EventArgs e)
        {
            AssessmentQuestion.Shuffle = (boxShuffle.Checked) ? "true" : "false";

            //if (AssessmentQuestion.Shuffle == "true")
            //{
            //    Random rnd = new Random();
            //    int r = rnd.Next(_questions.Count);

            //}
        }

        public void setMultipleAllowed()
        {

            int countAnswers = 0;
            foreach (AssessmentMcControl control in _answers)
            {
                if (control.AssessmentQuestionOption.Grade > 0)
                {
                    countAnswers++;
                }
            }
            if (countAnswers == 1)
            {
                AssessmentQuestion.MultipleAnswersAllowed = "false";
            }
            else
            {
                AssessmentQuestion.MultipleAnswersAllowed = "true";
            }

            if (countAnswers > 1)
            {
                double grade = 1.0 / countAnswers;
                foreach (AssessmentMcControl control in _answers)
                {
                    if (control.AssessmentQuestionOption.Grade > 0)
                    {
                        control.AssessmentQuestionOption.Grade = grade;
                    }
                    else
                    {
                        control.AssessmentQuestionOption.Grade = -1.0;
                    }
                }
            }
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

            setMultipleAllowed();


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
            if (AssessmentQuestion.Options.AssessmentQuestionOption.Count < 2)
            {
                MessageBox.Show("Niste definisali bar dva odgovora za pitanje" + AssessmentQuestion.SequenceId);
                isError = true;
            }



            foreach (LamsAssessment.AssessmentQuestionOption odgovor2 in AssessmentQuestion.Options.AssessmentQuestionOption)
            {
               AssessmentMcControl a = new AssessmentMcControl();

                if (odgovor2.OptionString == "" || odgovor2.OptionString == null)
                {
                    MessageBox.Show("Morate definisati tekst za svaki odgovor" + odgovor2.SequenceId);
                    isError = true;
                }

            }

            //foreach (AssessmentMCAnswerAddControl odgovor2 in _answers)
            //{

            //    if (!odgovor2.box.Checked)
            //    {
            //        MessageBox.Show("Niste definisali ni jedan tačan odgovor");
            //        isError = true;
            //    }

            //}

          

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
                    LamsAssessment.QuestionsAss.AssessmentQuestion.Add(this.AssessmentQuestion);
                }
                ParentControl.RefreshSequenceIds();
                ParentControl.RefreshList();
            }


        }

        private void AssessmentMCQuestionControlForm_Load(object sender, EventArgs e)
        {


        }

        private void AssessmentMCQuestionControlForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            setMultipleAllowed();
            if (!isSaveClose)
            {
                AssessmentQuestion.Question = oldValue;
                //    AssessmentQuestionOption.OptionString = AssessmentMCAnswerAddControl.oldValue;
                foreach (AssessmentMcControl odgovor2 in _answers)
                {
                    odgovor2.AssessmentQuestionOption.OptionString = odgovor2.oldValue;
                }




            }
            else
            {
                foreach (AssessmentMcControl odgovor2 in _answers)
                {
                    if (url != "")
                    {
                        odgovor2.AssessmentQuestionOption.OptionString = odgovor2.AssessmentQuestionOption.OptionString + "<br/><img src='" + odgovor2.pictureBoxOdgovor.ImageLocation + "' style='max-width:600px;max-height:300px;'/>";
                    }
                }
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

        private void AssessmentMCQuestionControlForm_Resize(object sender, EventArgs e)
        {
            var w = panelOdgovori.Width - 20;
            foreach (var control in _answers)
            {
                control.Width = w;
            }
        }
    }
}
