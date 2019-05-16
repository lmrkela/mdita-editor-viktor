using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Lams.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.Lams.Controls
{
    public partial class AssessmentImageControl : UserControl
    {
        private AssessmentImageForm ParentControl;

        public LamsShareResource.ResourceItem ResourceItem;

        public LamsShareResource.ResourceItemInstruction ResourceItemInstruction;
        WebClient webClient = new WebClient();

        public string url;


        public AssessmentImageControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Konstruktor koji prima parametre resourceItem i parent 
        /// </summary>
        /// <param name="resourceItem"></param>
        /// <param name="parent"></param>
        public AssessmentImageControl(LamsShareResource.ResourceItem resourceItem, AssessmentImageForm parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            ResourceItem = resourceItem;
            this.ParentControl = parent;
            txtUrl.Text = ResourceItem.Url;
            txtTema.Text = ResourceItem.Title;
            txtUrl.TextChanged += TxtUrl_TextChanged;
            txtTema.TextChanged += TxtTema_TextChanged;
            webClient.UploadFileCompleted += UploadFileCompletedCallback;
            webClient.UploadProgressChanged += UploadProgressCallback;
        }
        public AssessmentImageControl(LamsShareResource.ResourceItemInstruction resourceItemInstruction, AssessmentImageForm parent)
        {
            InitializeComponent();
            Disposed += OnDisposed;
            ResourceItemInstruction = resourceItemInstruction;
            this.ParentControl = parent;
            txtUrl.Text = ResourceItem.Url;
            txtTema.Text = ResourceItem.Title;
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
            ResourceItem.Url = txtUrl.Text;
        }
        /// <summary>
        /// Event na promenu textbox-a teme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtTema_TextChanged(object sender, EventArgs e)
        {
            ResourceItem.Title = txtTema.Text;
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

            var list2 = ParentControl.LamsShareResource.ResourceItems.ResourceItem;
            int index2 = list2.IndexOf(this.ResourceItem);
            int newIndex2 = index2 + (up ? -1 : 1);

            if (newIndex2 < 0 || newIndex2 >= list2.Count)
            {
                return;
            }
            list2[index2] = list2[newIndex2];
            string dipslayTemp = list2[index2].OrderId;
            list2[index2].OrderId = this.ResourceItem.OrderId;
            list2[newIndex2] = this.ResourceItem;
            list2[newIndex2].OrderId = dipslayTemp;
            ParentControl.RelocateControls();
        }
        /// <summary>
        /// Metoda koja vrsi brisanje kontrole
        /// </summary>
        public void Delete()
        {


            ParentControl._urls.Remove(this);
            ParentControl.LamsShareResource.ResourceItems.ResourceItem.Remove(ResourceItem);
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

        public void UploadFileCompletedCallback(object sender, UploadFileCompletedEventArgs e)
        {
            BeginInvoke(
                new MethodInvoker(() =>
                {
                    txtUrl.Text = Encoding.UTF8.GetString(e.Result);
                    progressBarUpload.Value = 100;
                    button1.Enabled = true;
                    MessageBox.Show(txtUrl.Text);
                    AssessmentMcForm ass = new AssessmentMcForm();
                    ass.pictureBoxPitanje.ImageLocation = txtUrl.Text;
                }));
        }

        private void UploadProgressCallback(object sender, UploadProgressChangedEventArgs e)
        {

            BeginInvoke(
                new MethodInvoker(() =>
                {
                    progressBarUpload.Value = e.ProgressPercentage;
                }));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string _boundaryNo = DateTime.Now.Ticks.ToString("x");
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {

                string fileName = dialog.FileName;
                NameValueCollection query = new NameValueCollection();
                query.Add("pass", "da8s4ada1s8d4sadas484ds89a4d8a");
                query.Add("lesson", ProjectSingleton.Project.LessonNumber);
                query.Add("subject", ProjectSingleton.Project.CourseCode);
                webClient.QueryString = query;
                //webClient.Headers["Content-type"] = "multipart/form-data; boundary=---------------------------" + _boundaryNo;
                new Thread(() => webClient.UploadFileAsync(new Uri("http://mdita.metropolitan.ac.rs/uploader.php"), "POST", fileName)).Start();

                //AssessmentMCQuestionControlForm ass = new AssessmentMCQuestionControlForm();

                //ass.pictureBoxPitanje.Image = new Bitmap(dialog.FileName);
                //this.Controls.Add(ass.pictureBoxPitanje);

            }

        }

        private void AssessmentQuestionAddImageControl_Load(object sender, EventArgs e)
        {

        }
    }
}
