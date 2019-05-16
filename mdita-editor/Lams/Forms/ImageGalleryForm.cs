using mDitaEditor.Dita;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Project;
using mDitaEditor.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mDitaEditor.Lams.Forms
{
    public partial class ImageGalleryForm : Form
    {

        public bool isEdit = false;
        public List<ImageGalleryControl> _urls;
        public LamsImageGallery LamsImageGallery;
        public LearningContent LearningContent;
        public LearningBase LearningObject;

        public ImageGalleryForm()
        {
            InitializeComponent();
        }

        public void InitIGContent()
        {
            LamsImageGallery = new LamsImageGallery();
        }
        public ImageGalleryForm(LearningBase selectedObject, LamsImageGallery lamsImageGallery = null)
        {//PROMENI
            Icon = Icon.FromHandle(Resources.lms_share_resources24.GetHicon());
            LearningObject = selectedObject;
            InitializeComponent();
            _urls = new List<ImageGalleryControl>();


            if (lamsImageGallery == null)
            {
                InitIGContent();
                isEdit = false;
                Add(true);
            }
            else
            {
                LamsImageGallery = lamsImageGallery;
                string folderPath = "";
                if (ProjectFile.folderLekcije.Contains("project.mdita"))
                {

                    folderPath = ProjectFile.folderLekcije.Substring(0, ProjectFile.folderLekcije.LastIndexOf('\\'));
                }
                else
                {

                    folderPath = ProjectFile.folderLekcije;
                }
                //Console.WriteLine("Folder path " + ProjectFile.folderLekcije);
                string galleryPath = folderPath + "\\resources\\imagegallery";
                foreach (LamsImageGallery.ImageGalleryItem item in LamsImageGallery.ImageGalleryItems.ImageGalleryItem)
                {
                    string filePath = item.imagePath;
                    int pos = filePath.LastIndexOf("\\") + 1;
                    string fileName = filePath.Substring(pos, filePath.Length - pos);
                    item.imagePath = galleryPath + "\\" + fileName;
                }
                isEdit = true;
            }
            naslovTB.TextChanged += NaslovTextBox_TextChanged;
            instrukcijeTB.TextChanged += InstrukcijeTextBox_TextChanged;
            naslovTB.Text = LamsImageGallery.Title;
            instrukcijeTB.Text = LamsImageGallery.Instructions;
            if (isEdit)
            {
                foreach (var url in LamsImageGallery.ImageGalleryItems.ImageGalleryItem)
                {
                    _urls.Add(new ImageGalleryControl(url, this));
                }
            }
            RelocateControls();
        }
        private void InstrukcijeTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsImageGallery.Instructions = instrukcijeTB.Text;
        }
        /// <summary>
        /// Metoda koja hvata promenu teksta u okviru naslova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaslovTextBox_TextChanged(object sender, EventArgs e)
        {
            LamsImageGallery.Title = naslovTB.Text;
        }
        /// <summary>
        /// Metoda koja vrsi dodavanje novog pitanja
        /// </summary>
        /// <param name="newQ"></param>
        public void Add(bool newQ = false)
        {
            var ri = new LamsImageGallery.ImageGalleryItem();
            if (_urls.Count > 0)
            {
                ri.SequenceId = (int.Parse(_urls[_urls.Count - 1].ImageGalleryItem.SequenceId) + 1) + "";             
            }
            else
            {
                ri.SequenceId = "1";                
            }
            
            var url = new ImageGalleryControl(ri, this);
            LamsImageGallery.ImageGalleryItems.ImageGalleryItem.Add(ri);
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

            var w = flowWithHackedScroll1.Width - 5;
            flowWithHackedScroll1.Controls.Clear();
            for (int i = 0; i < _urls.Count; i++)
            {
                var question = _urls[i];
                flowWithHackedScroll1.Controls.Add(question);
                question.Width = w;
                question.TabIndex = i;
                question.btnUp.Enabled = i != 0;
                question.btnDown.Enabled = i != _urls.Count - 1;
            }

            ResumeLayout();
            vScrollBar.Visible = !flowWithHackedScroll1.VerticalScroll.Visible;
        }

        /// <summary>
        /// Event koja vrsi validaciju unetog naslova, instrukcije i url-a
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isError = false;
            if (LamsImageGallery.Title == "" || LamsImageGallery.Title == null)
            {
                MessageBox.Show("Niste definisali naslov");
                isError = true;
            }
            if (LamsImageGallery.Instructions == "" || LamsImageGallery.Instructions == null)
            {
                MessageBox.Show("Niste definisali instrukcije");
                isError = true;
            }
            if (LamsImageGallery.ImageGalleryItems.ImageGalleryItem.Count == 0)
            {
                MessageBox.Show("Niste definisali bar jednu sliku");
                isError = true;
            }
            foreach (LamsImageGallery.ImageGalleryItem que in LamsImageGallery.ImageGalleryItems.ImageGalleryItem)
            {
                if (que.Title == "" || que.Title == null)
                {
                    MessageBox.Show("Morate definisati naslov za Sliku broj " + que.SequenceId);
                    isError = true;
                }
            }
            if (!isError)
            {
                if (!isEdit)
                {
                    LearningObject.ToolList.Add(this.LamsImageGallery);                 
                }
                this.Close();
                DialogResult = DialogResult.OK;
            }
        }

        private void ImageGalleryForm_Resize(object sender, EventArgs e)
        {
            var w = flowWithHackedScroll1.Width - 20;
            foreach (var control in _urls)
            {
                control.Width = w;
            }
        }
    }
}
