using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mdita_install
{
    [RunInstaller(true)]
    public partial class MditaInstaller : System.Configuration.Install.Installer
    {
        public MditaInstaller()
        {
            InitializeComponent();
        }

        private void MditaInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            try
            {
                Directory.Delete(appData + @"\DITA-OT1.8.4", true);
            }
            catch (Exception) { }
            try
            {
                Directory.Delete(appData + @"\mditaoutput", true);
            }
            catch (Exception) { }
        }

        private void MditaInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            var installPath = Context.Parameters["assemblypath"];
            installPath = installPath.Substring(0, installPath.LastIndexOf('\\'));
            var mathjaxZip = installPath + @"\mathjax.zip";
            try
            {
                ZipFile.ExtractToDirectory(mathjaxZip, installPath);
                File.Delete(mathjaxZip);
            }
            catch(Exception) { }
        }

        private void MditaInstaller_AfterUninstall(object sender, InstallEventArgs e)
        {
            var installPath = Context.Parameters["assemblypath"];
            installPath = installPath.Substring(0, installPath.LastIndexOf('\\'));
            try
            {
                Directory.Delete(installPath + @"\mathjax", true);
            }
            catch (Exception) { }
        }
    }
}
