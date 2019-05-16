using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.CustomForms
{
    public partial class ProjectSavingForm : Form
    {
        public bool FileSaved { get; private set; }
        public bool isExportAllowed { get; private set; }
        public string ExportPath { get; set; }
        private bool _export = false;
        public bool Branching { get; set; }

        public ProjectSavingForm( bool export, bool branching)
        {
            _export = export;
            Branching = branching;
            InitializeComponent();
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            FileSaved = false;

            
            int errors = ProjectSingleton.Project.SaveProject(_export, Branching, backgroundWorker);       
            if(_export)
            {
                if (errors == 0)
                {
                    ProjectSingleton.Project.ExportProject(Branching, ExportPath, backgroundWorker);
                }
                else{                    
                    throw new Exception("Nije moguće eksportovati lekciju!");
                }
                
            }
           
        
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            labProgress.Text = e.ProgressPercentage + "%";

            var text = e.UserState.ToString();
            if (e.ProgressPercentage >= 99)
            {
                text += "\r\n--------------------";
            }
            if (rtbStatus.Text.Length > 0)
            {
                text += "\r\n ✓";
            }
            text += rtbStatus.Text;
            rtbStatus.Text = text;

            int startIndex;
            if (e.ProgressPercentage == 99)
            {
                startIndex = rtbStatus.Text.IndexOf("\n");
                if (startIndex >= 0)
                {
                    rtbStatus.Select(startIndex, rtbStatus.Text.IndexOf("\n--") - startIndex);
                    rtbStatus.SelectionColor = Color.DarkRed;
                }
            }
            startIndex = rtbStatus.Text.IndexOf("✓");
            if (startIndex >= 0)
            {
                rtbStatus.Select(startIndex, rtbStatus.Text.Length - startIndex);
                rtbStatus.SelectionColor = Color.Green;
            }
            rtbStatus.Select(0, 0);
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                FileSaved = true;
                if (progressBar.Value == 100)
                {
                    Close();
                }
                progressBar.Value = 100;
                labProgress.Text = "Done!";
            }
            else
            {
                progressBar.ForeColor = Color.DarkRed;
                rtbStatus.Text = e.Error.Message + "\r\n--------------------\r\n ✘  " + rtbStatus.Text;
                var startIndex = rtbStatus.Text.IndexOf("✘");
                if (startIndex >= 0)
                {
                    rtbStatus.Select(startIndex, rtbStatus.Text.IndexOf("\n", startIndex) - startIndex);
                    rtbStatus.SelectionColor = Color.DarkRed;
                }
                startIndex = rtbStatus.Text.IndexOf("✓");
                if (startIndex >= 0)
                {
                    rtbStatus.Select(startIndex, rtbStatus.Text.Length - startIndex);
                    rtbStatus.SelectionColor = Color.Green;
                }
                labProgress.Text = "Error!";
            }
            rtbStatus.Select(0, 0);
            btnClose.Enabled = true;
            btnClose.Visible = true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ProjectSavingForm_Shown(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            labProgress.Text = "0%";
            rtbStatus.Text = "";
            btnClose.Visible = false;
            btnClose.Enabled = false;
            backgroundWorker.RunWorkerAsync();
        }
    }
}
