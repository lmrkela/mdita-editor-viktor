using System;
using System.Windows.Forms;
using mDitaEditor.Lams.Forms;

namespace mDitaEditor.Lams.Controls
{
    public partial class ForumTopicControl : UserControl
    {
        private ForumForm ParentControl;

        public LamsForum.Message Pitanje
        {
            get; set;
        }
        /// <summary>
        /// Konstruktor koji prima parametre pitanje i parent 
        /// </summary>
        /// <param name="pitanje"></param>
        /// <param name="parent"></param>
        public ForumTopicControl(LamsForum.Message pitanje, ForumForm parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            Pitanje = pitanje;
            this.ParentControl = parent;
            txtSadrzaj.Text = Pitanje.Body;
            txtTema.Text = Pitanje.Subject;
            txtSadrzaj.TextChanged += TxtSadrzaj_TextChanged;
            txtTema.TextChanged += TxtTema_TextChanged;
        }
        /// <summary>
        /// Event na promenu textbox-a sadrzaja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSadrzaj_TextChanged(object sender, EventArgs e)
        {
            Pitanje.Body = txtSadrzaj.Text;
        }
        /// <summary>
        /// Event na promenu textbox-a teme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTema_TextChanged(object sender, EventArgs e)
        {
            Pitanje.Subject = txtTema.Text;
        }

        /// <summary>
        /// Metoda koja vrsi dispose textbox-a sadrzaj ukoliko nije null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            if (txtSadrzaj.Text != null)
            {
                txtSadrzaj.Dispose();
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

            var list2 = ParentControl.Forum.Messages.Message;
            int index2 = list2.IndexOf(this.Pitanje);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].SequenceId;
            list2[index2].SequenceId = this.Pitanje.SequenceId;
            list2[newIndex2] = this.Pitanje;
            list2[newIndex2].SequenceId = dipslayTemp;
            ParentControl.RelocateControls();
        }
        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {
            ParentControl._questions.Remove(this);
            ParentControl.Forum.Messages.Message.Remove(Pitanje);
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

        private void ForumTopicControl_Load(object sender, EventArgs e)
        {

        }
    }
}
