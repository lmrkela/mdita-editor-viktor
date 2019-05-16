using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Forms;

namespace mDitaEditor.Lams.Controls
{
    public partial class McQuestionAnswerControl : UserControl
    {
        public bool isEdit = false;
        public MultipleChoiceForm ParentControl;
        public List<McAnswerControl> _questions;
        public LamsMultipleChoice.McQueContentMc McQueContentMc;
        public LamsMultipleChoice LamsMultipleChoice;
        public LearningContent LearningContent;
        public LearningBase LearningObject;



        //public McQuestionAnswerControl()
        //{
        //    InitializeComponent();
        //}

        /// <summary>
        /// Konstruktor koji prima parametre selectedObject, mc, parent i  mccontentmc
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <param name="mc"></param>
        /// <param name="parent"></param>
        /// <param name="mccontentmc"></param>
        public McQuestionAnswerControl(LearningBase selectedObject, LamsMultipleChoice.McQueContentMc mc, MultipleChoiceForm parent, LamsMultipleChoice mccontentmc)
        {
            LearningObject = selectedObject;
            InitializeComponent();
            Disposed += OnDisposed;
            McQueContentMc = mc;
            _questions = new List<McAnswerControl>();
            this.ParentControl = parent;
            pitanjeTextBox.Text += McQueContentMc.Question;
            pitanjeTextBox.TextChanged += PitanjeTextBox_TextChanged;
            RelocateControls();


        }
        /// <summary>
        /// Metoda koja setuje sve radio button-e koji nisu cekirani na false
        /// </summary>
        /// <param name="btn"></param>
        public void ClearQuestionTrue(RadioButton btn)
        {
            foreach(McAnswerControl control in _questions)
            {
                if (control.btnCorrect != btn)
                {
                    control.btnCorrect.Checked = false;
                }
            }
        }
        /// <summary>
        /// Prazan konstruktor
        /// </summary>
        public McQuestionAnswerControl()
        {


        }

        /// <summary>
        ///  Event na promenu textbox-a pitanja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PitanjeTextBox_TextChanged(object sender, EventArgs e)
        {
            McQueContentMc.Question = pitanjeTextBox.Text;
        }
        
        /// <summary>
        /// Metoda koja vrsi dispose textbox-a pitanja ukoliko nije null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            if (pitanjeTextBox.Text != null)
            {
                pitanjeTextBox.Dispose();
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
            var list = ParentControl._questions;
            int index = list.IndexOf(this);
            int newIndex = index + (up ? -1 : 1);

            if (newIndex < 0 || newIndex >= list.Count)
            {
                return;
            }
            list[index] = list[newIndex];
            list[newIndex] = this;

            var list2 = ParentControl.LamsMultipleChoice.McQueContents.McQueContentMc;
            int index2 = list2.IndexOf(this.McQueContentMc);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].DisplayOrder;
            list2[index2].DisplayOrder = this.McQueContentMc.DisplayOrder;
            list2[newIndex2] = this.McQueContentMc;
            list2[newIndex2].DisplayOrder = dipslayTemp;
            ParentControl.RelocateControls();
        }

        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {


            ParentControl._questions.Remove(this);
            ParentControl.LamsMultipleChoice.McQueContents.McQueContentMc.Remove(McQueContentMc);
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

            var w = pitanjeTextBox.Width - 4;
            panelOdgovori.Controls.Clear();
            for (int i = 0; i < _questions.Count; i++)
            {
                var question = _questions[i];
                question.Width = w;
                panelOdgovori.Controls.Add(question);
                question.TabIndex = i;
                question.btnUp.Enabled = i != 0;
                question.btnDown.Enabled = i != _questions.Count - 1;
            }

            ResumeLayout();
        }

        /// <summary>
        /// Metoda koja vrsi dodavanje novog odgovora
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {

            var noviOdgovor = new LamsMultipleChoice.McOptsContent();

            if (newQ)
            {
                noviOdgovor.McQueOptionText = "Prvi odgovor";
            }
            if (_questions.Count > 0)
            {
                noviOdgovor.DisplayOrder = (int.Parse(_questions[_questions.Count - 1].McOpts.DisplayOrder) + 1) + "";
            }
            else
            {
                noviOdgovor.DisplayOrder = "1";
            }
            var question = new McAnswerControl(noviOdgovor, this);
            McQueContentMc.McOptionsContents.McOptsContent.Add(noviOdgovor);
            _questions.Add(question);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MCQuestionAnswerControl_SizeChanged(object sender, EventArgs e)
        {
            var w = pitanjeTextBox.Width - 4;
            foreach (var control in _questions)
            {
                control.Width = w;
            }
        }



        //private void btnSave_Click(object sender, EventArgs e)
        //{
        //    bool isError = false;
        //    if (LamsMultipleChoice.Title == "" || LamsMultipleChoice.Title == null)
        //    {
        //        MessageBox.Show("Niste definisali naslov");
        //        isError = true;
        //    }
        //    if (LamsMultipleChoice.Instructions == "" || LamsMultipleChoice.Instructions == null)
        //    {
        //        MessageBox.Show("Niste definisali instrukcije");
        //        isError = true;
        //    }
        //    if (LamsMultipleChoice.McQueContentsClass.McQueContentMc.Count == 0)
        //    {
        //        MessageBox.Show("Niste definisali barem jedno pitanje");
        //        isError = true;
        //    }
        //    foreach (McQueContentMc que in LamsMultipleChoice.McQueContentsClass.McQueContentMc)
        //    {
        //        if (que.Question == "" || que.Question == null)
        //        {
        //            MessageBox.Show("Morate definisati tekst za pitanje broj " + que.DisplayOrder);
        //            isError = true;
        //        }
        //    }
        //    if (!isError)
        //    {
        //        if (!isEdit)
        //        {
        //            LearningObject.ToolList.Add(this.LamsMultipleChoice);
        //        }

        //      //  this.Close();
        //        ((Form)this.TopLevelControl).Close();
        //      //  DialogResult = DialogResult.OK;
        //       this.ParentForm.DialogResult = DialogResult.OK;


        //    }
        //}
    }
}