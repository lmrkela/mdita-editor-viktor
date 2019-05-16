using System;
using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Forms
{
    public partial class VideoForm : Form
    {
        private SelectableFlowPanel panel;
        WebClient webClient = new WebClient();
        bool isUploadCompleted = true;

        public VideoForm(SelectableFlowPanel _panel)
        {
            panel = _panel;
            InitializeComponent();
            webClient.UploadFileCompleted += UploadFileCompletedCallback;
            webClient.UploadProgressChanged += UploadProgressCallback;
        }

        public void UploadFileCompletedCallback(object sender, UploadFileCompletedEventArgs e)
        {
            BeginInvoke(
                new MethodInvoker(() =>
                {
                    string url = Encoding.UTF8.GetString(e.Result);
                    progressBarUpload.Value = 100;
                    isUploadCompleted = true;
                    btnBrowseFile.Enabled = true;
                    btnOk.Enabled = true;
                    if (url != "")
                    {
                        txtFilePath.Text = url;
                    }
                }));


        }

        private void UploadProgressCallback(object sender, UploadProgressChangedEventArgs e)
        {
            if (!progressBarUpload.IsHandleCreated) { 
                progressBarUpload.CreateControl();

            BeginInvoke(
                new MethodInvoker(() =>
                {
                        progressBarUpload.Value = e.ProgressPercentage;
                }));
            }
        }

        private void YouTubeVideoForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = Util.OpenVideoFiles();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                isUploadCompleted = false;
                btnBrowseFile.Enabled = false;
                btnOk.Enabled = false;
                string fileName = dialog.FileName;
                NameValueCollection query = new NameValueCollection();
                query.Add("pass", "da8s4ada1s8d4sadas484ds89a4d8a");
                query.Add("lesson", ProjectSingleton.Project.LessonNumber);
                query.Add("subject", ProjectSingleton.Project.CourseCode);
                webClient.QueryString = query;
                //webClient.Headers["Content-type"] = "multipart/form-data; boundary=---------------------------" + _boundaryNo;
                new Thread(() => webClient.UploadFileAsync(new Uri("http://mdita.metropolitan.ac.rs/uploader.php"), "POST", fileName)).Start();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (isUploadCompleted)
            {
                if (txtFilePath.Text != "")
                {
                    ControlFactory.getVideo(panel, txtFilePath.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Niste odabrali video fajl");
                }
            }
            else
            {
                MessageBox.Show("Upload video fajla još uvek nije gotov. Molimo Vas sačekajte da se upload završi kako bi dodali video fajl");
            }
        }

        private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUploadCompleted)
            {
                e.Cancel = false;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Upload video fajla još uvek nije gotov. Da li ste sigurni da želite da izađete?.", "Da li ste sigurni?", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    e.Cancel = false;
                }
                else if (dialogResult == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
