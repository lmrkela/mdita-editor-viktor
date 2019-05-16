using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using mdita_update.Properties;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace mdita_update
{
    public partial class UpdateForm : Form
    {
        private static readonly string DOWNLOAD_DIR = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\mDitaEditor\update";
        private static readonly string DOWNLOAD_INFO_FILE = DOWNLOAD_DIR + @"\update.config";

        private static readonly Font FONT_TITLE = new Font("Segoe UI", 14f, FontStyle.Bold);
        private static readonly Font FONT_SUBTITLE = new Font("Segoe UI", 9f, FontStyle.Bold);
        private static readonly Font FONT_REGULAR = new Font("Segoe UI", 9f, FontStyle.Regular);

        private readonly long _currentVersion;
        private string _downloadFile;

        private bool _working;

        public MditaVersion[] Versions { get; private set; }

        private WebClient _webClient;
        private bool _downloaded;
        
        public UpdateForm(long currentVersion = 0)
        {
            InitializeComponent();
            _currentVersion = currentVersion;
            if (_currentVersion != 0)
            {
                MinimizeBox = false;
                StartPosition = FormStartPosition.CenterParent;
            }
            Versions = new MditaVersion[0];
            _webClient = new WebClient();
            _webClient.DownloadProgressChanged += _webClient_DownloadProgressChanged;
            _webClient.DownloadFileCompleted += _webClient_DownloadFileCompleted;
        }

        private void Update_Load(object sender, EventArgs e)
        {
            lblState.Text = "Proveravam da li ima novih verzija...";
            
            progressBar.Style = ProgressBarStyle.Marquee;
            MditaUpdater.GetVersionsInBackground(VersionCheckCompleted, _currentVersion);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (!_downloaded)
            {
                DeleteConfig();
                var link = Versions[0].Link;
                Directory.CreateDirectory(DOWNLOAD_DIR);
                _webClient.DownloadFileAsync(new Uri(link), _downloadFile);
                lblState.Text = "Skida se instalacija nove verzije.";
                btnDownload.Enabled = false;
            }
            else
            {
                if (_currentVersion == 0)
                {
                    Enabled = false;
                    _working = true;

                    var guid = FindInstalledGuid();
                    if (guid != null)
                    {
                        var uninstallProcess = RunUninstaller(guid);
                        uninstallProcess?.WaitForExit();

                        if (uninstallProcess != null &&
                            uninstallProcess.ExitCode != 0 &&
                            uninstallProcess.ExitCode != 1605)
                        {
                            MessageBox.Show(
                                "Unable to uninstall previous version of mDita Editor.",
                                "Error!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);

                            _working = false;
                            Enabled = true;
                            btnDownload.Enabled = true;
                            return;
                        }
                    }

                    RunUpdateSetup();
                }
                _working = false;
                DialogResult = DialogResult.Yes;
                Close();
            }
        }

        private void VersionCheckCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar.Style = ProgressBarStyle.Blocks;
            if (e.Error != null)
            {
                lblState.Text = "Nije moguće proveriti verziju.\n" + e.Error.Message;
                ClearChangelog();
                rtbChangelog.Text = e.Error.StackTrace;
                return;
            }

            Versions = e.Result as MditaVersion[] ?? new MditaVersion[0];
            if (Versions.Length > 0)
            {
                var config = ReadConfig();
                var downloadedVersion = config["downloaded_version"];

                ClearChangelog();
                foreach (var version in Versions)
                {
                    AppendVersion(version);
                }
                rtbChangelog.SelectionStart = 0;
                rtbChangelog.ScrollToCaret();

                var latestVersion = Versions[0];
                _downloadFile = DOWNLOAD_DIR + "\\" +
                                latestVersion.Link.Substring(latestVersion.Link.LastIndexOf('/') + 1);

                if (downloadedVersion == null || Versions[0].Id > downloadedVersion.Value<long>())
                {
                    lblState.Text = "Pronađena je nova verzija.\nPritisnite dugme 'Download' da započnete skidanje.";
                    btnDownload.Enabled = true;
                    if (_currentVersion == 0)
                    {
                        btnDownload.PerformClick();
                    }
                }
                else
                {
                    progressBar.Value = progressBar.Maximum;
                    lblState.Text = "Update je skinut.\nPritisnite dugme 'Install' da pokrenete instalaciju nove verzije.";
                    btnDownload.Text = "Install";
                    btnDownload.Image = Resources.install;
                    btnDownload.Enabled = true;
                    btnCancel.Text = "Close";
                    _downloaded = true;
                }
                btnDownload.Select();
            }
            else
            {
                progressBar.Value = progressBar.Maximum;
                lblState.Text = "Vi već koristite najnoviju verziju programa.";
                btnCancel.Text = "Close";
                btnCancel.Select();
            }
        }

        private void _webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            lblProgress.Text = $"{e.BytesReceived >> 10}/{e.TotalBytesToReceive >> 10} KB";
        }

        private void _webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                File.Delete(_downloadFile);
                return;
            }
            if (e.Error != null)
            {
                lblState.Text = "Nije moguće skinuti verziju.\n" + e.Error.Message;
                ClearChangelog();
                rtbChangelog.Text = e.Error.InnerException?.Message + Environment.NewLine + e.Error.InnerException?.StackTrace;
                return;
            }

            var config = new JObject();
            config["downloaded_version"] = Versions[0].Id;
            WriteConfig(config);

            progressBar.Value = progressBar.Maximum;
            lblState.Text = "Update je skinut.\nPritisnite dugme 'Install' da pokrenete instalaciju nove verzije.";
            btnDownload.Text = "Install";
            btnDownload.Image = Resources.install;
            btnDownload.Enabled = true;
            btnCancel.Text = "Close";
            _downloaded = true;
        }

        private void UpdateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_working)
            {
                e.Cancel = true;
                return;
            }
            if (_webClient.IsBusy)
            {
                var result = MessageBox.Show("Da li želite da prekinete skidanje programa?", "Update je u toku",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _webClient.CancelAsync();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        private readonly string[] _oldGuids =
           {
                "{C0172AA7-E27E-4290-A768-0D46BA5456A5}",
                "{3F964046-541D-4E27-B48F-1BABFE1F34D9}",
                "{05A2DA01-A340-4431-8D46-2E69E1536F03}",
                "{6F7F6158-4081-4229-AC32-9D343C60207B}",
                "{B4AE5BA8-38D8-47C8-B96A-F6FE9C0DE61E}",
                "{A2FEDEC1-A653-4E38-9084-0E96F1C81CE3}"
            };

        public string FindInstalledGuid()
        {
            return _oldGuids.FirstOrDefault(key => Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\" + key) != null)
                   ??
                   _oldGuids.FirstOrDefault(key => Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\" + key) != null);
        }

        public Process RunUninstaller(string guid)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "MsiExec.exe",
                    Arguments = $"/x{guid} /passive"
                }
            };
            process.Start();
            return process;
        }

        public Process RunUpdateSetup()
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo(_downloadFile)
            };
            process.Start();
            return process;
        }

        private void ClearChangelog()
        {
            rtbChangelog.Text = "";
            rtbChangelog.DeselectAll();
            rtbChangelog.SelectionFont = FONT_REGULAR;
        }

        private void AppendVersion(MditaVersion version)
        {
            AppendTitle("v" + version.Version);
            AppendLine(version.Date.ToString("dd.MM.yyyy."));
            AppendLine();
            var lines = version.Changelog.Split("$$".ToCharArray());
            foreach (var line in lines)
            {
                var l = line.Trim();
                if (l.Length > 0)
                {
                    FormatLine(line);
                }
            }
            AppendLine();
            AppendLine();
        }

        private void AppendTitle(string text)
        {
            rtbChangelog.SelectionFont = FONT_TITLE;
            rtbChangelog.AppendText(text);
            rtbChangelog.SelectionFont = FONT_REGULAR;
            rtbChangelog.AppendText(Environment.NewLine);
        }

        private void AppendSubtitle(string text)
        {
            rtbChangelog.SelectionFont = FONT_SUBTITLE;
            rtbChangelog.AppendText(text);
            rtbChangelog.SelectionFont = FONT_REGULAR;
            rtbChangelog.AppendText(Environment.NewLine);
        }

        private void AppendLine(string text = "")
        {
            rtbChangelog.AppendText(text);
            rtbChangelog.AppendText(Environment.NewLine);
        }

        private void FormatLine(string line)
        {
            if (line.StartsWith("## "))
            {
                AppendSubtitle(line.Substring(3));
            }
            else if (line.StartsWith("- "))
            {
                rtbChangelog.SelectionBullet = true;
                rtbChangelog.AppendText(line.Substring(2));
                AppendLine();
            }
            else
            {
                rtbChangelog.AppendText(line);
                AppendLine();
            }
            rtbChangelog.SelectionBullet = false;
        }

        private JObject ReadConfig()
        {
            try
            {
                var configJson = File.ReadAllText(DOWNLOAD_INFO_FILE);
                return JObject.Parse(configJson);
            }
            catch (Exception)
            {}
            return new JObject();
        }

        private void WriteConfig(JObject json)
        {
            try
            {
                File.WriteAllText(DOWNLOAD_INFO_FILE, json.ToString());
            }
            catch (Exception)
            {}
        }

        private void DeleteConfig()
        {
            try
            {
                File.Delete(DOWNLOAD_INFO_FILE);
            }
            catch (Exception)
            {}
        }
    }
}
