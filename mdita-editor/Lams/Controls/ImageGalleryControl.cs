using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mDitaEditor.Lams.Forms;
using System.Net;
using mDitaEditor.Utils;
using System.Collections.Specialized;
using mDitaEditor.Project;
using System.Threading;
using System.IO;

namespace mDitaEditor.Lams.Controls
{
    public partial class ImageGalleryControl : UserControl
    {
        private ImageGalleryForm ParentControl;
        public LamsImageGallery.ImageGalleryItem ImageGalleryItem;
        public LamsImageGallery.ImageGalleryItemInstruction ImageGalleryItemInstruction;

        string newImagePath;
        int redniBrojSlike;

        public ImageGalleryControl()
        {
            InitializeComponent();
        }

        public ImageGalleryControl(LamsImageGallery.ImageGalleryItem imageGalleryItem, ImageGalleryForm parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            ImageGalleryItem = imageGalleryItem;
            this.ParentControl = parent;
            txtUrl.Text = ImageGalleryItem.imagePath;
            txtTema.Text = ImageGalleryItem.Title;
            txtOpis.Text = ImageGalleryItem.Description;
            txtUrl.TextChanged += TxtUrl_TextChanged;
            txtTema.TextChanged += TxtTema_TextChanged;
            txtOpis.TextChanged += txtOpis_TextChanged;
        }

        public ImageGalleryControl(LamsImageGallery.ImageGalleryItemInstruction imageGalleryItemInstruction, ImageGalleryForm parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            ImageGalleryItemInstruction = imageGalleryItemInstruction;
            this.ParentControl = parent;
            txtUrl.Text = ImageGalleryItem.imagePath;
            txtTema.Text = ImageGalleryItem.Title;
            txtOpis.Text = ImageGalleryItem.Description;
            txtUrl.TextChanged += TxtUrl_TextChanged;
            txtTema.TextChanged += TxtTema_TextChanged;
        }

        /// <summary>
        /// Event na promenu textbox-a url-a
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtUrl_TextChanged(object sender, EventArgs e)
        {
            //ImageGalleryItem.Url = txtUrl.Text;
        }
        /// <summary>
        /// Event na promenu textbox-a teme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTema_TextChanged(object sender, EventArgs e)
        {
            ImageGalleryItem.Title = txtTema.Text;
        }

        private void txtOpis_TextChanged(object sender, EventArgs e)
        {
            ImageGalleryItem.Description = txtOpis.Text;
        }
        /// <summary>
        /// Metoda koja vrsi dispose textbox-a sadrzaj ukoliko nije null
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDisposed(object sender, EventArgs e)
        {
            if (txtTema.Text != null)
            {
                txtTema.Dispose();
            }
            if (txtOpis.Text != null)
            {
                txtOpis.Dispose();
            }
            if (txtUrl.Text != null)
            {
                txtUrl.Dispose();
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
            var list = ParentControl._urls;
            int index = list.IndexOf(this);
            int newIndex = index + (up ? -1 : 1);

            if (newIndex < 0 || newIndex >= list.Count)
            {
                return;
            }
            list[index] = list[newIndex];
            list[newIndex] = this;

            var list2 = ParentControl.LamsImageGallery.ImageGalleryItems.ImageGalleryItem;
            int index2 = list2.IndexOf(this.ImageGalleryItem);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].SequenceId;
            list2[index2].SequenceId = this.ImageGalleryItem.SequenceId;
            list2[newIndex2] = this.ImageGalleryItem;
            list2[newIndex2].SequenceId = dipslayTemp;
            ParentControl.RelocateControls();
        }

        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {
            ParentControl._urls.Remove(this);
            if (ImageGalleryItem != null)
            {
                if(txtUrl.Text != "") File.Delete(ImageGalleryItem.imagePath);
            }
            ParentControl.LamsImageGallery.ImageGalleryItems.ImageGalleryItem.Remove(ImageGalleryItem);
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

        private void izaberiBtn_Click(object sender, EventArgs e)
        {
            if (Util.checkIfHasInternetConnection())
            {
               
                string _boundaryNo = DateTime.Now.Ticks.ToString("x");
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (Util.checkIfHasInternetConnection())
                    {
                        string filePath = dialog.FileName;
                        int pos = filePath.LastIndexOf("\\") + 1;
                        string fileName = filePath.Substring(pos, filePath.Length - pos);

                        ImageGalleryItem.FileName = fileName;
                        ImageGalleryItem.OriginalFile.FileName = fileName;
                        ImageGalleryItem.MediumFile.FileName = "medium_" + fileName;
                        ImageGalleryItem.ThumbnailFile.FileName = "thumbnail_" + fileName;


                        Random rnd = new Random();
                        int num = rnd.Next(1000);

                        Random mrnd = new Random();
                        int mnum = rnd.Next(1000);

                        Random trnd = new Random();
                        int tnum = rnd.Next(1000);
                        int index = fileName.IndexOf('.');
                        string imgName = fileName.Substring(0, index);
                        string originalfileUid = "" + num;
                        string mediumfileUid = "" + mnum;
                        string thumblrfileUid = "" + tnum;
                        ImageGalleryItem.OriginalFileUuid = originalfileUid;
                        ImageGalleryItem.MediumFileUuid = mediumfileUid;
                        ImageGalleryItem.ThumbnailFileUuid = thumblrfileUid;


                        ImageGalleryItem.OriginalFile.FileUuid = originalfileUid;
                        ImageGalleryItem.MediumFile.FileUuid = mediumfileUid;
                        ImageGalleryItem.ThumbnailFile.FileUuid = thumblrfileUid;

                        string fileType = Path.GetExtension(dialog.FileName);
                        string fileTypeName = fileType.Substring(fileType.IndexOf(".") + 1, fileType.Length - 1);

                        ImageGalleryItem.FileType = "image/" + fileTypeName;


                        Console.WriteLine("Image gallery 1 " + ProjectFile.folderLekcije);
                        string folderPath = "";
                        if (ProjectFile.folderLekcije.Contains("project.mdita"))
                        {
                            folderPath = ProjectFile.folderLekcije.Substring(0, ProjectFile.folderLekcije.LastIndexOf('\\'));
                        }
                        else
                        {
                            folderPath = ProjectFile.folderLekcije;
                        }
                        
                        string galleryPath = folderPath + "\\resources\\imagegallery";

                        Console.WriteLine("Image gallery 2 " + folderPath);

                        System.IO.Directory.CreateDirectory(galleryPath);

                        List<string> galleryFiles = new List<string>(Directory.GetFiles(galleryPath));
                        List<int> galleryFilesNums = new List<int>();
                        galleryFilesNums.Clear();
                        foreach (string file in galleryFiles)
                        {
                            string imgPath = file;
                            int imgPos = imgPath.LastIndexOf("\\") + 1;
                            string imgFileName = imgPath.Substring(imgPos, imgPath.Length - imgPos);
                            int imgIndex = imgFileName.IndexOf('-');
                            if (index > 0)
                            {
                                string imgNum = imgFileName.Substring(0, imgIndex);
                                int i = Int32.Parse(imgNum);
                                galleryFilesNums.Add(i);
                            }
                        }

                        if (galleryFilesNums.Count < 1)
                        {
                            redniBrojSlike = 1;
                        }
                        else
                        {
                            redniBrojSlike = galleryFilesNums.Max()+1;
                        }

                        string uidImgName = redniBrojSlike + "-" + ImageGalleryItem.FileName;
                        newImagePath = galleryPath + "\\" + uidImgName;
                        
                        if (ImageGalleryItem.imagePath != null) File.Delete(ImageGalleryItem.imagePath);

                        File.Copy(filePath, Path.Combine(@galleryPath + "\\", uidImgName));
                        
                        ImageGalleryItem.imagePath = newImagePath;
                        txtUrl.Text = newImagePath;
                    }

                    else
                    {
                        MessageBox.Show("Potrebna Vam je internet konekcija");
                    }

                }

            }

            else
            {

                MessageBox.Show("Potrebna Vam je internet konekcija");
            }
        }
        
    }
}
