using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class ShareResourcesForm : Form
    {

        public bool isEdit = false;
        public List<ShareResourcesAddUrlControl> _urls;
        public LamsShareResource LamsShareResource;
        public LearningContent LearningContent;
        public LearningBase LearningObject;
        public ShareResourcesForm()
        {
            InitializeComponent();
        }

        public void InitSRContent()
        {
            LamsShareResource = new LamsShareResource();
        }
        /// <summary>
        /// Konstruktor kome se prosledjuju parametri selectedObject i LamsShareResource, 
        /// proverava se da li je LamsShareResource null, ukoliko jeste, vrsi se inicijalizacija 
        /// kontenta share resources-a, u suprotnom kontent SR-a postaje ono sto se prosledjuje
        /// kroz parametar LamsShareResource. 
        /// Takodje, vrsi se provera da li je forma editovana, kroz globalnu promenljivu isEdit, 
        /// i vrsi se dodavanje pitanja na listu pitanja List<ShareResourcesAddUrlControl>.
        /// Konstruktor se poziva kroz event na Edit button ManageAdditionalActivities klase, 
        /// kao i na event AddForum button-a.
        /// <param name="selectedObject"></param>
        /// <param name="lamsShareResource"></param>
        public ShareResourcesForm(LearningBase selectedObject, LamsShareResource lamsShareResource = null)
        {
            Icon = Icon.FromHandle(Resources.lms_share_resources24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
            _urls = new List<ShareResourcesAddUrlControl>();
            if (lamsShareResource == null)
            {
                InitSRContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsShareResource = lamsShareResource;
                isEdit = true;
            }
            naslovTextBox.TextChanged += NaslovTextBox_TextChanged;
            instrukcijeTextBox.TextChanged += InstrukcijeTextBox_TextChanged;
            naslovTextBox.Text = LamsShareResource.Title;
            instrukcijeTextBox.Text = LamsShareResource.Instructions;
            if (isEdit)
            {
                foreach (var url in LamsShareResource.ResourceItems.ResourceItem)
                {
                    _urls.Add(new ShareResourcesAddUrlControl(url, this));
                }
            }
            RelocateControls();
        }

        /// <summary>
        /// Metoda koja hvata promenu teksta u okviru instrukcije
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstrukcijeTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsShareResource.Instructions = instrukcijeTextBox.Text;  
        }
        /// <summary>
        /// Metoda koja hvata promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsShareResource.Title = naslovTextBox.Text;
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje novog pitanja
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            var ri = new LamsShareResource.ResourceItem();
            if (_urls.Count > 0)
            {
                ri.OrderId = (int.Parse(_urls[_urls.Count - 1].ResourceItem.OrderId) + 1) + "";
            }
            else
            {
                ri.OrderId = "1";
            }
            var url = new ShareResourcesAddUrlControl(ri, this);
            LamsShareResource.ResourceItems.ResourceItem.Add(ri);
            _urls.Add(url);
        }

        /// <summary>
        /// Event na button dodaj, koja poziva metodu za dodavanje novog url-a i vrsi relokaciju kontrola
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dodajButton_Click(object sender, EventArgs e)
        {
            Add();
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
            for (int i = 0; i < _urls.Count; i++)
            {
                var question = _urls[i];
                panelListQA.Controls.Add(question);
                question.Width = w;
                question.TabIndex = i;
                question.btnUp.Enabled = i != 0;
                question.btnDown.Enabled = i != _urls.Count - 1;
            }

            ResumeLayout();
            vScrollBar.Visible = !panelListQA.VerticalScroll.Visible;
        }

        /// <summary>
        /// Event koja vrsi validaciju unetog naslova, instrukcije i url-a
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsShareResource.Title == "" || LamsShareResource.Title == null)
            {
                MessageBox.Show("Niste definisali naslov");
                isError = true;
            }
            if (LamsShareResource.Instructions == "" || LamsShareResource.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije");
                isError = true;
            }
            if (LamsShareResource.ResourceItems.ResourceItem.Count == 0)
            {
                MessageBox.Show("Niste definisali bar jedan URL");
                isError = true;
            }
            foreach (LamsShareResource.ResourceItem que in LamsShareResource.ResourceItems.ResourceItem)
            {
                if (que.Title == "" || que.Title == null)
                {
                    MessageBox.Show("Morate definisati naslov za URL broj " + que.OrderId);
                    isError = true;
                }

                if (que.Url == "" || que.Url == null)
                {
                    MessageBox.Show("Morate definisati URL broj " + que.OrderId);
                    isError = true;
                }
            }
            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsShareResource);
                }
                this.Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void ShareResourcesControlForm_Load(object sender, EventArgs e)
        {

        }

        private void ShareResourcesControlForm_Resize(object sender, EventArgs e)
        {
            var w = panelListQA.Width - 20;
            foreach (var control in _urls)
            {
                control.Width = w;
            }
        }
    }
}
