using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace mDitaEditor.CustomForms
{
    public partial class CopyFilesForm : Form
    {
        string FileSource { get; set; }
        string FileDestination { get; set; }

        public CopyFilesForm(string fileSource, string fileDestionation)
        {
            InitializeComponent();
            this.FileSource = fileSource;
            this.FileDestination = fileDestionation;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
            //File.Copy(fileName, fileNameNew, true);
        }

        private void CopyFilesForm_Load(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(FileSource);
            progressBar1.Maximum = files.Length;
            backgroundWorker_Copy.WorkerReportsProgress = true;
            backgroundWorker_Copy.RunWorkerAsync();
            backgroundWorker_Copy.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                this.Close();
            }
        }

        private void backgroundWorker_Copy_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = Directory.GetFiles(FileSource);
            int i = 0;
            foreach (string f in files)
            {
                File.Copy(FileSource + Path.GetFileName(f), FileDestination + Path.GetFileName(f), true);
                i++;
                backgroundWorker_Copy.ReportProgress(i);
            }
        }
    }
}
