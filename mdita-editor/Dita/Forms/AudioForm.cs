using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Forms
{
    public partial class AudioForm : Form
    {
        private SelectableFlowPanel panel;
        WebClient webClient = new WebClient();
        bool isUploadCompleted = true;
        [DllImport("winmm.dll", SetLastError = true)]
        static extern uint waveInGetNumDevs();
        [DllImport("winmm.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint waveInGetDevCaps(uint hwo, ref WAVEOUTCAPS pwoc, uint cbwoc);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct WAVEOUTCAPS
        {
            public ushort wMid;
            public ushort wPid;
            public uint vDriverVersion;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
            public string szPname;
            public uint dwFormats;
            public ushort wChannels;
            public ushort wReserved1;
            public uint dwSupport;
        }
        public bool GetSoundDevices()
        {
            uint devices = waveInGetNumDevs();
            string[] result = new string[devices];
            WAVEOUTCAPS caps = new WAVEOUTCAPS();
            for (uint i = 0; i < devices; i++)
            {
                waveInGetDevCaps(i, ref caps, (uint)Marshal.SizeOf(caps));
                result[i] = caps.szPname;
                Console.WriteLine(caps.szPname);
            }

            return (devices > 0) ? true : false;
        }
        Random rand = new Random();
        public AudioForm(SelectableFlowPanel _panel)
        {
            panel = _panel;
            InitializeComponent(); webClient.UploadFileCompleted += UploadFileCompletedCallback;
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
                    btnRecordAudio.Enabled = true;
                    btnOk.Enabled = true;
                    if (url != "")
                    {
                        txtFilePath.Text = url;
                    }
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

        private void YouTubeVideoForm_Load(object sender, EventArgs e)
        {

        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = Util.OpenAudioFiles();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                isUploadCompleted = false;
                btnBrowseFile.Enabled = false;
                btnRecordAudio.Enabled = false;
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
                    try
                    {
                        ControlFactory.getAudio(panel, txtFilePath.Text);
                        //DitaClipboard.AddUndoState(ProjectSingleton.SelectedSection);
                        this.Close();
                    }
                    catch
                    {

                    }
                }
                else
                {
                    MessageBox.Show("Niste odabrali audio fajl");
                }
            }
            else
            {
                MessageBox.Show("Upload audio fajla još uvek nije gotov. Molimo Vas sačekajte da se upload završi kako bi dodali audio fajl");
            }
        }

        private void btnRecordAudio_Click(object sender, EventArgs e)
        {
            RecordAudioForm recordForm = new RecordAudioForm();
            if (!GetSoundDevices())
            {
                MessageBox.Show("There is no microphone installed on your PC");
            }
            else {
                if (recordForm.ShowDialog() == DialogResult.OK)
                {
                    int randomNum = rand.Next(10000, 50000);
                    string pasteFileName = "recording" + Path.GetFileName(recordForm.Musica).Replace(" ", "").Replace("record", randomNum + "").ToLower();
                    isUploadCompleted = false;
                    string fileName = recordForm.Musica;
                    btnBrowseFile.Enabled = false;
                    btnRecordAudio.Enabled = false;
                    btnOk.Enabled = false;
                    NameValueCollection query = new NameValueCollection();
                    query.Add("pass", "da8s4ada1s8d4sadas484ds89a4d8a");
                    query.Add("lesson", ProjectSingleton.Project.LessonNumber);
                    query.Add("subject", ProjectSingleton.Project.CourseCode);
                    query.Add("newfilename", pasteFileName);
                    webClient.QueryString = query;
                    new Thread(() => webClient.UploadFileAsync(new Uri("http://mdita.metropolitan.ac.rs/uploader.php"), "POST", fileName)).Start();
                }
            }
        }

        private void AudioForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUploadCompleted)
            {
                e.Cancel = false;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Upload audio fajla još uvek nije gotov. Da li ste sigurni da želite da izađete.", "Da li ste sigurni?", MessageBoxButtons.YesNo);
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
