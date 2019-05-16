using System;
using System.IO;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Controls
{
    //partial class GalleryControl
    //{
    public partial class GalleryImageControl : UserControl
    {
        private GalleryControl _parentControl;

        private string _fileName;
        /// <summary>
        /// Override Xml string gettera da generiše xml za jednu sliku
        /// </summary>
        public string Xml
        {
            get
            {
                return string.Format("<galleryimage src=\"{0}-{1}\" title=\"{2}\" description=\"{3}\"/>", ProjectSingleton.Project.CourseCode, _fileName, txbTitle.Text, txbDescription.Text);
            }
        }

        /// <summary>
        /// Konstuktor koji definiše jednu sliku unutar galerije.
        /// Parent predstavlja galeriju kojoj sliak pripada
        /// Image name ime slike
        /// title naslov dok descrptionopis slike
        /// Pri loadu slika se kopira u Resources folder i dodaju se handleri za izmenu vrednosti
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="imageName"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public GalleryImageControl(GalleryControl parent, string imageName, string title = "", string description = "")
        {
            InitializeComponent();
            Disposed += OnDisposed;

            _parentControl = parent;

            _fileName = imageName;

            picBox.Image = Util.GetCopyImage(Path.Combine(ProjectSingleton.Project.ResourcesDir, _fileName));
            txbTitle.Text = title;
            txbDescription.Text = description;
            txbFileName.Text = _fileName;

            txbTitle.TextChanged += txbTitle_TextChanged;
            txbDescription.TextChanged += txbDescription_TextChanged;
        }

        /// <summary>
        /// Kada se element doda na galeriju menja mu se Parent pa se širina kontrole
        /// računa kao puna širina - 20
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent != null)
            {
                Width = Parent.Width - 20;
            }
        }
        /// <summary>
        /// Kada se dispozuje kontrole dispouzuje se i image zbog Bitmap problema
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void OnDisposed(object sender, EventArgs eventArgs)
        {
            if (picBox.Image != null)
            {
                picBox.Image.Dispose();
            }
        }
        /// <summary>
        /// Na delete se pravi novi state galerije i komponenta se briše iz galerije
        /// </summary>
        /// <param name="newState"></param>
        public void Delete(bool newState = false)
        {
            if (newState)
            {
                DitaClipboard.AddGalleryImageAddDeleteState(ProjectSingleton.SelectedSection, _parentControl._galleryImages.IndexOf(this), false, _fileName, txbTitle.Text, txbDescription.Text);
            }

            _parentControl._galleryImages.Remove(this);
            _parentControl.RelocateControls();
            _parentControl.UpdateContent();
            Dispose();
        }
        /// <summary>
        /// NA dugme delete zovemo delete metodu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete(true);
        }
        /// <summary>
        /// Move metoda omogućuje pomeranje kontrole na gore ili na dole i pravljenje
        /// state-a za undo-redo za pomeranje
        /// </summary>
        /// <param name="up"></param>
        /// <param name="newState"></param>
        public new void Move(bool up, bool newState = false)
        {
            var list = _parentControl._galleryImages;
            int index = list.IndexOf(this);
            int newIndex = index + (up ? -1 : 1);

            if (newIndex < 0 || newIndex >= list.Count)
            {
                return;
            }
            list[index] = list[newIndex];
            list[newIndex] = this;

            _parentControl.RelocateControls();
            _parentControl.UpdateContent();

            if (newState)
            {
                DitaClipboard.AddGalleryImageMovedState(ProjectSingleton.SelectedSection, index, up);
            }
        }
        /// <summary>
        /// Zove metodu za pomeranje komponente na gore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            Move(true, true);
        }
        /// <summary>
        /// Zove metodu za pomeranje komponente na dole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            Move(false, true);
        }
        /// <summary>
        /// Updejtuje sadržaj XML-a na promenu naslova slike
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbTitle_TextChanged(object sender, EventArgs e)
        {
            _parentControl.UpdateContent();
        }
        /// <summary>
        /// Updejtuje sadržaj XML-a na promenu opisa slike
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDescription_TextChanged(object sender, EventArgs e)
        {
            _parentControl.UpdateContent();
        }
        /// <summary>
        /// Kada se napusti naslov čekira greške ako korisnik nije uneo naslov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbTitle_Leave(object sender, EventArgs e)
        {
            MainForm.Instance.CheckErrorsAndStatistics();
        }
    }
}
//}
