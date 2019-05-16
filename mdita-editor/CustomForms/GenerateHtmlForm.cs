using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.CustomForms
{
    public partial class GenerateHtmlForm : Form
    {
        private bool _running;

        private ProjectFile _project = ProjectSingleton.Project;
        private string _outputFolder;

        private List<string> _htmlFiles = new List<string>(); 

        public GenerateHtmlForm()
        {
            InitializeComponent();
            _outputFolder = Program.HtmlFolder + $@"\OUTPUT\{_project.CourseCode}\{_project.LessonNumber}";
       
        }

        private void ProcessPreview_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("MDITA: " + Program.DitaFolder);

            if (!Directory.Exists(Program.DitaFolder))
            {
                lblProgress.Text = "Nije moguće izgenerisati HTML.";
                txbStatus.Text = "Nema dita foldera.";
                return;
            }
            backgroundWorker.RunWorkerAsync();
        }

        public static int CountStringOccurrences(string text, string pattern)
        {
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }

        private void ProcessPreview_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_running)
            {
                e.Cancel = true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(_outputFolder + @"\index.html");
            }
            catch { }
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.CurrentThread.IsBackground = true;
            _running = true;

            _htmlFiles.Clear();
            PrepareDirectory();
            var success = GenerateHtml();
            if (success)
            {
                ProcessHtml();
                DeleteTempFiles();
            }

            backgroundWorker.ReportProgress(1);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var line = e.UserState as string;
            if (line != null)
            {
                txbStatus.AppendText(line + "\r\n");
                txbStatus.SelectionStart = txbStatus.Text.Length;
                txbStatus.ScrollToCaret();
            }
            progressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            if (e.Error != null)
            {
                lblProgress.Text = "HTML nije uspešno izgenerisan.\n" + e.Error.Message;
            }
            else if (txbStatus.Text.Contains("BUILD SUCCESSFUL") ||
                     txbStatus.Text.Contains("Number of Errors : " +
                                             CountStringOccurrences(txbStatus.Text, "is not unique")))
            {
                lblProgress.Text = "HTML je uspešno izgenerisan!";
            

                //tabPreview.Enabled = true;
                //tabControl.SelectedTab = tabPreview;
                //if (lwLinks.Items.Count > 0)
                //{
                //    lwLinks.Items[0].Selected = true;
                //}
                try
                {
                    Process.Start(_outputFolder + @"\index.html");
                }
                catch { }
            }
            else
            {
                lblProgress.Text = "HTML nije uspešno izgenerisan.";
            }
            _running = false;
        }

        private void PrepareDirectory()
        {
            backgroundWorker.ReportProgress(0, "Preparing output directory.");
            if (!Directory.Exists(_outputFolder))
            {
                Directory.CreateDirectory(_outputFolder);
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(_outputFolder);
                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
            }

            backgroundWorker.ReportProgress(0, "\r\nPreparing css and javascript files. ");
            try
            {
                ZipFile.ExtractToDirectory(Program.DitaFolder + "\\mditaoutput.zip", Program.HtmlFolder);
            }
            catch (Exception ex)
            {
                backgroundWorker.ReportProgress(0, "Unable to extract output resources " + ex.Message);
            }

            var outputResFile = $"{_outputFolder}//{_project.CourseCode}-";
            var extensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            var resFiles = Directory.GetFiles(_project.ResourcesDir, "*.*", SearchOption.AllDirectories);
            foreach (var resFile in resFiles.Where(s => extensions.Contains(Path.GetExtension(s)?.ToLower())))
            {
                backgroundWorker.ReportProgress(0, "  Copying " + resFile);
                File.Copy(resFile, resFile.Replace(_project.ResourcesDir, outputResFile), true);
            }
        }

        private bool GenerateHtml()
        {
            backgroundWorker.ReportProgress(0, "\r\nStarting HTML generation." +
                                               "\r\n---------------------------------------\r\n");
            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Path.Combine(Program.ProgramPath, "dita", "generate_html.bat"),
                    WorkingDirectory = Program.ProgramPath,
                    Arguments = $"\"{Program.DitaFolder}\" " +
                                $"\"{_project.ProjectDir}\\{_project.CourseCode}-{_project.LessonNumber}LAMS.ditamap\" " +
                                $"\"{Program.HtmlFolder}\" " +
                                $"\"{_outputFolder}\" ",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    Verb = "runas"
                }
            };
            proc.Start();
            while (!proc.StandardOutput.EndOfStream)
            {
                backgroundWorker.ReportProgress(0, proc.StandardOutput.ReadLine());
            }
            return true;
        }

        private void ProcessHtml()
        {
            backgroundWorker.ReportProgress(0, "\r\nProcessing HTML files: ");
            var htmlFiles = Directory.GetFiles(_outputFolder, "*.html", SearchOption.AllDirectories);
            foreach (var htmlFile in htmlFiles)
            {
                backgroundWorker.ReportProgress(0, $"  {htmlFile}");
                var html = File.ReadAllText(htmlFile);
                html = html
                    .Replace("href=\"css/", "href=\"../../../css/")
                    .Replace("src=\"jscripts/", "src=\"../../../jscripts/")
                    .Replace("href=\"jscripts/", "href=\"../../../jscripts/")
                    .Replace("http://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_SVG&amp;delayStartupUntil=configured", "http://cdn.mathjax.org/mathjax/2.6-latest/MathJax.js?config=TeX-AMS-MML_CHTML")
                    .Replace("jax: [\"input/TeX\", \"output/SVG\"],", "jax: [\"input/TeX\", \"output/HTML-CSS\"]," +
                                                                      "\"fast-preview\": {"+
                                                                        "Chunks: {EqnChunk: 10000, EqnChunkFactor: 1, EqnChunkDelay: 0},"+
                                                                        "color: \"inherit!important\","+
                                                                        "updateTime: 30, updateDelay: 6,"+
                                                                        "messageStyle: \"none\","+
                                                                        "disabled: false"+
                                                                      "},")
                    .Replace("http://cdn.mathjax.org/mathjax/latest/MathJax.js", "");
                File.WriteAllText(htmlFile, html);

                var fileName = Path.GetFileName(htmlFile);
                if (fileName.Contains("index"))
                {
                    continue;
                }
                _htmlFiles.Add(fileName);
            }
            _htmlFiles.Sort(new CustomComparer());
        }

        public class CustomComparer : IComparer<string>
        {
            private Regex _regex = new Regex("(.*?)([0-9]+)\\.html");

            public int Compare(string x, string y)
            {
                // run the regex on both strings
                var xRegexResult = _regex.Match(x);
                var yRegexResult = _regex.Match(y);

                // check if they are both numbers
                if (xRegexResult.Success && yRegexResult.Success)
                {
                    return int.Parse(xRegexResult.Groups[2].Value).CompareTo(int.Parse(yRegexResult.Groups[2].Value));
                }
                if (yRegexResult.Success)
                {
                    return -1;
                }
                if (xRegexResult.Success)
                {
                    return 1;
                }
                // otherwise return as string comparison
                return x.CompareTo(y);
            }
        }

        private void DeleteTempFiles()
        {
            backgroundWorker.ReportProgress(0, "\r\nDeleting temp files.");
            try
            {
                File.Delete(_outputFolder + @"\dita.list");
                File.Delete(_outputFolder + @"dita.xml.properties");
            }
            catch (Exception)
            {
                // nothing
            }
        }

        private void lwLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
              
      
        }

        private void lblProgress_Click(object sender, EventArgs e)
        {

        }
    }
}
