using System;
using System.Windows.Forms;
using mDitaEditor.Dita;

namespace mDitaEditor.Lams.Controls
{
    public partial class McAnswerControl : UserControl
    {
        public bool isEdit = false;
        public McQuestionAnswerControl ParentControl;
        public LearningBase LearningObject;
        public LamsMultipleChoice.McOptsContent McOptsContent;

        public LamsMultipleChoice.McOptsContent McOpts { get; set; }

        /// <summary>
        /// Konstruktor koji prima parametre mcopts i parent 
        /// </summary>
        /// <param name="mcopts"></param>
        /// <param name="parent"></param>
        public McAnswerControl(LamsMultipleChoice.McOptsContent mcopts, McQuestionAnswerControl parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            McOpts = mcopts;
            this.ParentControl = parent;
            odgovorTextBox.Text += McOpts.McQueOptionText;
            odgovorTextBox.TextChanged += OdgovorTextBox_TextChanged;
            btnCorrect.Checked = (McOpts.CorrectOption == "true") ? true : false;
        }
        /// <summary>
        /// Konstruktor kome se prosledjuju parametri selectedObject i mcopts, 
        /// proverava se da li je mcopts null, ukoliko jeste, vrsi se inicijalizacija 
        /// kontenta McOptsContent, u suprotnom kontent McOptsContent-a postaje ono sto se prosledjuje
        /// kroz sam parametar mcopts. 
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <param name="mcopts"></param>
        public McAnswerControl(LearningBase selectedObject, LamsMultipleChoice.McOptsContent mcopts = null)
        {
            LearningObject = selectedObject;
            InitializeComponent();
            Disposed += OnDisposed;

            //     McOptsContent = new McOptsContent();

            if (mcopts == null)
            {
                McOptsContent = new LamsMultipleChoice.McOptsContent();
                isEdit = false;

            }
            else
            {
                McOptsContent = mcopts;
                isEdit = true;
            }

            odgovorTextBox.Text += McOptsContent.McQueOptionText;
            odgovorTextBox.TextChanged += OdgovorTextBox_TextChanged;


            //RelocateControls();



        }

        /// <summary>
        /// Event na promenu textbox-a odgovora
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OdgovorTextBox_TextChanged(object sender, EventArgs e)
        {
            McOpts.McQueOptionText = odgovorTextBox.Text;
        }

        /// <summary>
        /// Metoda koja vrsi dispose textbox-a odgovora ukoliko nije null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            var list = ParentControl._questions;
            int index = list.IndexOf(this);
            int newIndex = index + (up ? -1 : 1);

            if (newIndex < 0 || newIndex >= list.Count)
            {
                return;
            }
            list[index] = list[newIndex];
            list[newIndex] = this;

            var list2 = ParentControl.McQueContentMc.McOptionsContents.McOptsContent;
            int index2 = list2.IndexOf(this.McOpts);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].DisplayOrder;
            list2[index2].DisplayOrder = this.McOpts.DisplayOrder;
            list2[newIndex2] = this.McOpts;
            list2[newIndex2].DisplayOrder = dipslayTemp;
            ParentControl.RelocateControls();

        }
        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {


            ParentControl._questions.Remove(this);
            ParentControl.McQueContentMc.McOptionsContents.McOptsContent.Remove(McOpts);
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
        /// Event na radio button Correct koji vraca true ukoliko je cekiran, a false ukoliko nije 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnCorrect_CheckedChanged_1(object sender, EventArgs e)
        {
            McOpts.CorrectOption = (btnCorrect.Checked) ? "true" : "false";
        }
        /// <summary>
        /// Event na radio button Correct koji poziva metodu ClearQuestionTrue
        /// kojoj se prosledjuje parametar btnCorrect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCorrect_Click(object sender, EventArgs e)
        {
            ParentControl.ClearQuestionTrue(btnCorrect);
        }

        private void MCAnswerControl_Load(object sender, EventArgs e)
        {

        }
    }
}
