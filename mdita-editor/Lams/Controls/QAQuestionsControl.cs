using System;
using System.Windows.Forms;
using mDitaEditor.Lams.Forms;

namespace mDitaEditor.Lams.Controls
{
    public partial class QaQuestionsControl : UserControl
    {
        private QaForm ParentControl;
      
        public QaQueContent Pitanje {
            get;
            set;
        }
        /// <summary>
        /// Konstruktor koji prima parametre pitanje i parent 
        /// </summary>
        /// <param name="pitanje"></param>
        /// <param name="parent"></param>
        public QaQuestionsControl(QaQueContent pitanje, QaForm parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            Pitanje = pitanje;
            this.ParentControl = parent;
            txtPitanje.Text = Pitanje.Question;
            chbRequired.Checked = (Pitanje.Required == "true") ? true : false;
            txtPitanje.TextChanged += txtPitanje_TextChanged;
            chbRequired.CheckedChanged += chbRequired_CheckedChanged;
        }

        /// <summary>
        /// Metoda koja vrsi dispose textbox-a pitanja ukoliko nije null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            if (txtPitanje.Text != null)
            {
                txtPitanje.Dispose();
            }
        }

        /// <summary>
        /// Event na checkbox koji ako je cekiran vraca true, u suprotnom false
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbRequired_CheckedChanged(object sender, EventArgs e)
        {
            Pitanje.Required = (chbRequired.Checked) ? "true" : "false";
        }

        /// <summary>
        ///  Event na button Up za pomeranje kontrole na gore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            Move(true);
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

            var list2 = ParentControl.LamsQa.QaQueContents.QaQueContent;
            int index2 = list2.IndexOf(this.Pitanje);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].DisplayOrder;
            list2[index2].DisplayOrder = this.Pitanje.DisplayOrder;
            list2[newIndex2] = this.Pitanje;
            list2[newIndex2].DisplayOrder = dipslayTemp;
            ParentControl.RelocateControls();
        }

        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {
            ParentControl._questions.Remove(this);
            ParentControl.LamsQa.QaQueContents.QaQueContent.Remove(Pitanje);
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
        /// Event na promenu textbox-a pitanja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPitanje_TextChanged(object sender, EventArgs e)
        {

            Pitanje.Question = txtPitanje.Text;
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
    }
}
