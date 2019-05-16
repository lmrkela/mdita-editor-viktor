using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class QaForm : Form
    {
        public bool isEdit = false;
        public List<QaQuestionsControl> _questions;
        public LamsQa LamsQa;
        public LearningContent LearningContent;
        //  public Sectiondiv _sectiondiv;
        public LearningBase LearningObject;

        /// <summary>
        /// Metoda koja kreira instance klasa i setuje njihove vrednosti
        /// </summary>
        public void InitQAContent()
        {
            LamsQa = new LamsQa();
            QaQueContents questionContents = new QaQueContents("tree-set");
            LamsQa.QaQueContents = questionContents;
            questionContents.QaQueContent = new List<QaQueContent>();
            LamsQa.Conditions conditions = new LamsQa.Conditions("tree-set");
            LamsQa.ConditionList = conditions;
            LamsQa.Comparator comparator = new LamsQa.Comparator("org.lamsfoundation.lams.learningdesign.TextSearchConditionComparator");
            conditions.Comparator = comparator;
            QaCondition qAcond = new QaCondition();
            Questions questionsCond = new Questions("tree-set");
            questionsCond.Comparator = new LamsQa.Comparator("org.lamsfoundation.lams.tool.qa.util.QaQueContentComparator");
            qAcond.Questions = questionsCond;
            TemporaryQuestionDTOSet temp = new TemporaryQuestionDTOSet("tree-set", new LamsQa.Comparator("org.lamsfoundation.lams.tool.qa.util.QaQuestionContentDTOComparator"));
            qAcond.TemporaryQuestionDTOSet = temp;
            conditions.QaCondition = qAcond;
        }
        /// <summary>
        /// Metoda koja konvertuje datum u string
        /// </summary>
        /// <returns></returns>
        public static string GiveMeTimestamp()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
        }
        /// <summary>
        /// Konstruktor kome se prosledjuju parametri selectedObject i QA, 
        /// proverava se da li je QA null, ukoliko jeste, vrsi se inicijalizacija 
        /// kontenta QA, u suprotnom kontent lamsQa-a postaje ono sto se prosledjuje
        /// kroz sam parametar. 
        /// Takodje, vrsi se provera da li je forma editovana, kroz globalnu promenljivu isEdit, 
        /// i vrsi se dodavanje pitanja na listu pitanja List<QaQuestionsControl>.
        /// Konstruktor se poziva kroz event na Edit button ManageAdditionalActivities klase, 
        /// kao i na event AddQA button-a.
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <param name="lamsQa"></param>
        public QaForm(LearningBase selectedObject, LamsQa lamsQa = null)
        {
            Icon = Icon.FromHandle(Resources.lms_qa24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
            _questions = new List<QaQuestionsControl>();
            if (lamsQa == null)
            {
                InitQAContent();
                Add(true);
                isEdit = false;
            }
            else
            {
                LamsQa = lamsQa;
                isEdit = true;
            }
            naslovTextBox.Text = LamsQa.Title;
            instrukcijeTextBox.Text = LamsQa.Instructions;
            if (isEdit)
            {
                foreach (QaQueContent question in LamsQa.QaQueContents.QaQueContent)
                {
                    _questions.Add(new QaQuestionsControl(question, this));
                }
            }
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
        /// Event na promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void naslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsQa.Title = naslovTextBox.Text;
        }
        /// <summary>
        ///  Event na promenu teksta u okviru instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void instrukcijeTextBox_TextChanged(object sender, EventArgs e)
        {

            LamsQa.Instructions = instrukcijeTextBox.Text;
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje novog pitanja
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            QaQueContent novoPitanje = new QaQueContent();
            if (newQ)
            {
                novoPitanje.Question = "Novo pitanje";
            }
            novoPitanje.Feedback = "";
            if (_questions.Count > 0)
            {
                novoPitanje.DisplayOrder = (int.Parse(_questions[_questions.Count - 1].Pitanje.DisplayOrder) + 1) + "";
            }
            else
            {
                novoPitanje.DisplayOrder = "1";
            }
            novoPitanje.Required = "false";
            var question = new QaQuestionsControl(novoPitanje, this);
            LamsQa.QaQueContents.QaQueContent.Add(novoPitanje);
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
        /// Event koja vrsi validaciju unetog naslova, instrukcije, pitanja i teksta pitanja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsQa.Title == "" || LamsQa.Title == null)
            {
                MessageBox.Show("Niste definisali naslov za Q/A");
                isError = true;
            }
            if (LamsQa.Instructions == "" || LamsQa.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije za Q/A");
                isError = true;
            }
            if (LamsQa.QaQueContents.QaQueContent.Count == 0)
            {
                MessageBox.Show("Niste definisali barem jedno pitanje");
                isError = true;
            }
            foreach (QaQueContent que in LamsQa.QaQueContents.QaQueContent)
            {
                if (que.Question == "" || que.Question == null)
                {
                    MessageBox.Show("Morate definisati tekst za pitanje broj " + que.DisplayOrder);
                    isError = true;
                }
            }
            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsQa);
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void panelListQA_SizeChanged(object sender, EventArgs e)
        {
            var w = panelListQA.Width - 20;
            foreach (var control in _questions)
            {
                control.Width = w;
            }
        }

        private void QaForm_Load(object sender, EventArgs e)
        {

        }
    }
}
