using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using mDitaEditor.Dita.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Controls
{
    public partial class GalleryControl : UserControl
    {
        public List<GalleryImageControl> _galleryImages;

        private Sectiondiv _sectiondiv;

        /// <summary>
        /// Override gettera za String xml koji generiše novi XML sa komponentu
        /// </summary>
        public string Xml
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<slides>");
                foreach (var image in _galleryImages)
                {
                    sb.AppendLine(image.Xml);
                }
                sb.AppendLine("</slides>");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Učitava postojeću galeriju slika po Sectiondiv-u
        /// </summary>
        /// <param name="div"></param>
        public GalleryControl(Sectiondiv div)
        {
            InitializeComponent();

            _galleryImages = new List<GalleryImageControl>(25);
            _sectiondiv = div;

            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(div.Content);
                var nodes = xmlDoc.SelectNodes("slides/galleryimage");
                foreach (XmlNode childrenNode in nodes)
                {
                    string src = Util.PictureNameForWithoutDITA(childrenNode.Attributes["src"].Value);
                    string title = childrenNode.Attributes["title"].Value;
                    string desc = childrenNode.Attributes["description"].Value;
                    _galleryImages.Add(new GalleryImageControl(this, src, title, desc));
                }
                UpdateContent();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Menja veličinu kontrole na maksimum pri promeni Parent-a
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
        /// Metoda koja služi dodavanju slike. Kopira sliku u Resources folder
        /// i daje korisniku da unese pre toga ime slike za čuvanje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_galleryImages.Count >= 25)
            {
                MessageBox.Show("Galerija ne može imati više od 25 slika.");
                return;
            }
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Image Files|*.png;*.jpg;*.gif;*.JPG";
            if (imageDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }   
            PictureSaveForm pic = new PictureSaveForm(null, true);
            if (pic.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string fileName = pic.ImeSlike + Path.GetExtension(imageDialog.FileName);
            string copyPath = Path.Combine(ProjectSingleton.Project.ResourcesDir, fileName);
            try
            {
                File.Copy(imageDialog.FileName, copyPath, true);
            }
            catch
            {
                MessageBox.Show("Nije moguće prekopirati sliku.");
                return;
            }

            var image = new GalleryImageControl(this, fileName);
            _galleryImages.Add(image);
            RelocateControls();
            UpdateContent();
            MainForm.Instance.CheckErrorsAndStatistics();
            DitaClipboard.AddGalleryImageAddDeleteState(ProjectSingleton.SelectedSection, _galleryImages.IndexOf(image), true, fileName, "", "");
        }
        /// <summary>
        /// Iscrtava ponovo sve kontrole unutar panela sa listom kontrola
        /// </summary>
        /// <param name="suspendLayout"></param>
        public void RelocateControls(bool suspendLayout = true)
        {
            if (suspendLayout)
            {
                SuspendLayout();
            }

            panList.Controls.Clear();
            for(int i = 0; i < _galleryImages.Count; i++)
            {
                var image = _galleryImages[i];
                panList.Controls.Add(image);
                image.TabIndex = i;
                image.btnUp.Enabled = i != 0;
                image.btnDown.Enabled = i != _galleryImages.Count - 1;
            }
            vScroll.Visible = !panList.VerticalScroll.Visible;
            labEmptyList.Visible = _galleryImages.Count == 0;

            ResumeLayout();
        }
        /// <summary>
        /// Menja stanje sectionDiv-a sa trenutnim Xml-om
        /// </summary>
        public void UpdateContent()
        {
            _sectiondiv.Content = Xml;
        }
        /// <summary>
        /// Pomera sliku po indexu gore ili dole
        /// </summary>
        /// <param name="index"></param>
        /// <param name="up"></param>
        public void MoveImage(int index, bool up)
        {
            _galleryImages[index].Move(up);
        }
        /// <summary>
        /// Dodaje sliku na galeriju sa imenom, opisom kao i naslovom slike
        /// </summary>
        /// <param name="index"></param>
        /// <param name="imageName"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public void AddImage(int index, string imageName, string title, string description)
        {
            _galleryImages.Insert(index, new GalleryImageControl(this, imageName, title, description));
            RelocateControls();
            UpdateContent();
        }
        /// <summary>
        /// Briše sliku po indeksu sa liste slika
        /// </summary>
        /// <param name="index"></param>
        public void DeleteImage(int index)
        {
            _galleryImages[index].Delete();
        }
    }
}
