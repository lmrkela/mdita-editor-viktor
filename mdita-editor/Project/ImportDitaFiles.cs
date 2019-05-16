using mDitaEditor.Dita;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System;
using System.Threading.Tasks;
using System.Net;
using mDitaEditor.CustomForms;
using mDitaEditor.Utils;
using System.Xml;
using System.Drawing;

namespace mDitaEditor.Project
{
    public class ImportDitaFiles
    {
        /// <summary>
        /// Kopira sadržaj postojećeg resource foldera
        /// u  resource folder projekta
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="resourceDir"></param>
        public static void CreateResourcesDir(string fileName, string resourceDir)
        {
            /**
            Kreiranje Resource dirketorijuma moze zahtevati vreme. Kako bi korisnik mogao
            neometano da radi i da mu se brzo Importuje projekat ovo pokrecemo u novom Threadu
            **/
            // MessageBox.Show(Path.GetDirectoryName(fileName) + "\\resources\\");
            if (Directory.Exists(Path.GetDirectoryName(fileName) + "\\resources\\"))
            {
                if(Directory.GetFiles(Path.GetDirectoryName(fileName) + "\\resources\\").Length > 0){

                    new CopyFilesForm(Path.GetDirectoryName(fileName) + "\\resources\\", resourceDir).ShowDialog();
                }
            }
            //Task.Run(() =>
            //{
            //    if (Directory.Exists(Path.GetDirectoryName(fileName) + "\\resources"))
            //    {
            //        Util.CopyFromDirToDir(Path.GetDirectoryName(fileName) + "\\resources", resourceDir);
            //    }
            //});
        }
        /// <summary>
        /// Metoda koja kopira sve slike iz resources foldera projekta iz kog se importuje objekat ucenja koje se nalaze u tom objektu (ImportDITAone).
        /// </summary>
        /// <param name="filePath"></param>
        public static void AppendFilesToResourceFrom(string filePath)
        {
            string choosenResource = Path.Combine(Path.GetDirectoryName(filePath), "resources");
            string resourceDir = ProjectSingleton.Project.ResourcesDir;
            List<string> imgList = Util.GetImageNamesFromObjectXml(File.ReadAllText(filePath));
            foreach (string img in imgList)
            {
                string imgFinal = img.Substring(img.IndexOf("-") + 1);
                try
                {
                    File.Copy(Path.Combine(choosenResource, imgFinal), Path.Combine(resourceDir, imgFinal), true);
                }
                catch { }
            }
        }
         /// <summary>
        /// 
        /// </summary>
        /// <param name="lc"></param>
        public static void DownloadImagesFromObjectToResource(LearningContent lc)
        {
            List<string> images = new List<string>();
            images = Util.GetImagesFromObjectOnImport(lc);
            string serverResourceUrl = lc.url.Substring(0, lc.url.LastIndexOf("/") + 1) + "resources/";
            string projectResource = ProjectSingleton.Project.ResourcesDir;
            using (WebClient wc = new WebClient())
            {
                foreach (string img in images)
                {
                    string imgFinal = img.Substring(img.IndexOf("-") + 1);
                    wc.DownloadFile(serverResourceUrl + "/" + imgFinal, Path.Combine(projectResource, imgFinal));
                }
            }
        }
        /// <summary>
        /// Kreira openFileDialog za Import projekta
        /// </summary>
        public static void CreateOpenFile()
        {
            OpenFileDialog open = Util.OpenDitaFiles();
            if (open.ShowDialog() == DialogResult.OK)
            {
                string fileNamea = Path.GetDirectoryName(open.FileName) + "//resources";
                string resourceDir = ProjectSingleton.Project.ResourcesDir;

                ImportDitaFiles.CreateResourcesDir(fileNamea, resourceDir);

                foreach (string fileName in open.FileNames)
                {
                    ProjectSingleton.Project.OpenContentFile(fileName);
                }
            }
        }
        /// <summary>
        /// Metoda koja se zove za učitavanje i dodavanje objekata u listu
        /// </summary>
        public static void ImportDITA()
        {
            if (ProjectSingleton.Project != null)
            {
                CreateOpenFile();
            }
            else
            {
                MessageBox.Show("Niste otvorili novi projekat ili učitali postojeći");
            }
        }
        /// <summary>
        /// Kreira openFileDialog za Import projekta
        /// </summary>
        /// 
        static Form selectDOForm = new Form();
        static ListBox lb = new ListBox();
        static Dictionary<string, string> dictionaryFLC = new Dictionary<string, string>();
        public static void CreateOpenFileOne()
        {
            string selectedFolder = "";
            using (var folderDialog = new FolderBrowserDialog())
            {
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFolder = folderDialog.SelectedPath;
                }
            }
            OpenFileDialog open = Util.OpenDitaFile();
            open.InitialDirectory = selectedFolder;

            SortedDictionary<int, string> dictionaryLC = new SortedDictionary<int, string>();
            dictionaryFLC.Clear();
            lb.Items.Clear();
            try
            {
                if (selectedFolder != "")
                {
                    foreach (string fileDita in Directory.EnumerateFiles(selectedFolder, "*.dita"))
                    {
                        if (fileDita.Contains("lc"))
                        {
                            Console.WriteLine(fileDita);
                            string contents = File.ReadAllText(fileDita);

                            int pFrom = contents.IndexOf("<title>") + "<title>".Length;
                            int pTo = contents.IndexOf("</title>");

                            String result = contents.Substring(pFrom, pTo - pFrom);

                            int pos = fileDita.LastIndexOf("\\") + 1;
                            string learningContent = fileDita.Substring(pos, fileDita.Length - pos);


                            int numFrom = learningContent.IndexOf("pptlc") + "pptlc".Length;
                            int numTo = learningContent.IndexOf(".dita");

                            string learningContentNum = learningContent.Substring(numFrom, numTo - numFrom);

                            dictionaryLC.Add(Int32.Parse(learningContentNum), learningContent + " - " + result);

                            dictionaryFLC.Add(fileDita, learningContent + " - " + result);
                        }
                    }
                }
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("Folder does not exist.", "Unable to load project", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            selectDOForm.Text = @"Choose a dita object";
            selectDOForm.Width = 500;
            selectDOForm.Height = 500;
            lb.Width = 400;
            lb.Height = 400;
            lb.Location = new Point(20, 20);

            foreach (KeyValuePair<int, string> kvp in dictionaryLC)
            {
                lb.Items.Add(kvp.Value);
            }

            Button okBtn = new Button();
            okBtn.Text = "OK";
            okBtn.Width = 50;
            okBtn.Height = 25;
            okBtn.Location = new Point(370, 425);
            okBtn.Click += OkBtnClick;
            
            selectDOForm.Controls.Add(lb);
            selectDOForm.Controls.Add(okBtn);
            if(selectedFolder != "") selectDOForm.ShowDialog();
     
        }
        static void OkBtnClick(Object sender, EventArgs e)
        {
            if (lb.SelectedItem != null)
            {
                string file = "";
                foreach (KeyValuePair<string, string> kvp in dictionaryFLC)
                {
                    if (kvp.Value == lb.SelectedItem.ToString()) file = kvp.Key;
                }

                string path = (Path.GetDirectoryName(file));
                string projectPath = Path.GetFullPath(ProjectSingleton.Project.ProjectDir);

                if (path == projectPath)
                {
                    MessageBox.Show("Ne mozete učitati fajl koji je već učitan u projekat");
                }
                else
                {
                    int count = Path.GetFileName(file).Split('-').Length - 1;
                    if (count == 2)
                    {
                        try
                        {
                            ProjectSingleton.Project.OpenContentFileOne(file);
                            ImportDitaFiles.AppendFilesToResourceFrom(file);
                            selectDOForm.Close();
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        MessageBox.Show("Niste ubacili validan DITA fajl.");
                    }
                }
            }
        }
        /// <summary>
        /// Importuje samo jedan DITA fajl
        /// </summary>
        internal static void ImportDITAOne()
        {
            if (ProjectSingleton.Project != null)
            {
                CreateOpenFileOne();
            }
            else
            {
                MessageBox.Show("Niste otvorili novi projekat ili učitali postojeći");
            }
        }
    }
}
