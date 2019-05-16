using mDitaEditor.Dita;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using mDitaEditor.Utils;
using mDitaEditor.Project;
using System.Drawing;
using mDitaEditor.Properties;
using System.Web.Script.Serialization;
using System.Net;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using mdita_update;
using mDitaEditor.CustomControls;
using mDitaEditor.CustomForms;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Dita.Forms;
using mDitaEditor.Lams;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Lams.Forms;
using mDitaEditor.Repository;
using FolderBrowserDialog = mDitaEditor.CustomControls.FolderBrowserDialog;

namespace mDitaEditor
{
    partial class MainForm : Form
    {
        public static MainForm Instance { get; private set; }

        private ProjectFile _project;

        public ProjectFile Project
        {
            get { return _project; }
            private set
            {
                _project = value;
                ProjectSingleton.Project = _project;
                if (_project != null)
                {
                    setTitle();
                    RecreateMenu();
                    grafikaPanel.LoadProject();
                    CheckErrorsAndStatistics();
                    RecentItemsManager.Add(_project.ProjectDir);
                    RefreshRecentItems();
                    ProjectControlsEnabled = true;
                    OpenSlide((IDitaSlide)Project.LearningOverview);
                }
                else
                {
                    RecreateMenu();
                    grafikaPanel.Canvas.Clear();
                    CloseSlide();
                    ProjectControlsEnabled = false;
                }
                DitaClipboard.ResetStates();
            }
        }

        private void setTitle()
        {
            Text = "mDitaEditor - " + Project.ProjectDir;
        }

        private Control _selectedControl;
        public Control SelectedControl
        {

            get { return _selectedControl; }
            set


            {


                //Console.WriteLine("SELECTED " + value);
                if (_selectedControl == value)
                {
                    return;
                }
                _selectedControl = value;

                ribbonMenu.SuspendLayout();

                panDitaWords.Visible = false;
                panDitaNote.Visible = false;
                panDitaImage.Visible = false;
                if (_selectedControl is TextBoxControl)
                {
                    panDitaWords.Visible = true;
                }
                else if (_selectedControl is NoteControl)
                {
                    panDitaNote.Visible = true;
                }
                else if (_selectedControl is ImageBoxControl)
                {
                    var img = (ImageBoxControl)_selectedControl;
                    numDitaImageHeight.TextBoxText = img.PicBox.Height.ToString();
                    numDitaImageWidth.TextBoxText = img.PicBox.Width.ToString();
                    panDitaImage.Visible = true;
                }
                if (ribbonMenu.ActiveTab == tabDita)
                {
                    ribbonMenu.ActiveTab = tabDita;
                }

                ribbonMenu.ResumeLayout();

            }
        }

        private SelectableFlowPanel _selectedPanel;

        public SelectableFlowPanel SelectedPanel
        {
            get { return _selectedPanel; }
            set
            {
                _selectedPanel = value;
                panDitaInsert.Enabled = _selectedPanel != null;
                SelectedControl = null;
            }
        }

        public MainForm()
        {
            Instance = this;
            InitializeComponent();
            ribbonMenu.OrbClicked += (sender, args) =>
            {
                transparentPanel.Visible = true;
                transparentPanel.BringToFront();
            };
            transparentPanel.MouseEnter += (sender, args) =>
            {
                transparentPanel.Visible = false;
            };
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Console.WriteLine("mDitaEditor v" + Program.AppVersion);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            SelectedControl = new Control();
            string[] args = Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                string projectPath = args[1];
                LoadProject(projectPath);
            }
            else
            {
                RefreshRecentItems();
                ProjectControlsEnabled = false;
            }
            MditaUpdater.GetVersionsInBackground(VersionCheckCompleted, Program.AppVersion);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Focus();
            e.Cancel = !CloseProject();
            ProjectSingleton.SaveUnsavedImages();
            Settings.Default.Save();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            new ExceptionForm(ex).ShowDialog();
        }

        private void VersionCheckCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                return;
            }

            var versions = e.Result as MditaVersion[];
            if (versions == null || versions.Length == 0)
            {
                return;
            }

            var result = MessageBox.Show($"Pronađena je nova verzija programa ({versions[0].Version}).\nDa li želite da je skinete i instalirate?",
                "Mdita Update", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                btnUpdate.PerformClick();
            }
        }

        private bool _projectControlsEnabled;

        public bool ProjectControlsEnabled
        {
            get { return _projectControlsEnabled; }
            set
            {
                _projectControlsEnabled = value;
                foreach (var tab in ribbonMenu.Tabs)
                {
                    foreach (var panel in tab.Panels)
                    {
                        panel.Enabled = _projectControlsEnabled;
                    }
                }
                var menuItems = ribbonMenu.OrbDropDown.MenuItems;
                for (var i = 2; i < menuItems.Count; ++i)
                {
                    menuItems[i].Enabled = _projectControlsEnabled;
                }

                tabGreskeIWord.Enabled = _projectControlsEnabled;

                grafikaPanel.Enabled = _projectControlsEnabled;
                trbGridSize.Enabled = _projectControlsEnabled;
                numGridSize.Enabled = _projectControlsEnabled;
                labGridSize.Enabled = _projectControlsEnabled;
                trbZoom.Enabled = _projectControlsEnabled;
                labelZoom.Enabled = _projectControlsEnabled;
                numZoom.Enabled = _projectControlsEnabled;
            }
        }

        public void RefreshRecentItems()
        {
            ribbonMenu.OrbDropDown.RecentItems.Clear();
            var recentList = RecentItemsManager.RecentProjects;
            recentList = recentList.GetRange(0, recentList.Count > 15 ? 15 : recentList.Count);
            foreach (var path in recentList)
            {
                var recentItem = new RibbonOrbRecentItem(path);
                recentItem.Click += (sender, args) =>
                {
                    var arg = (MouseEventArgs)args;
                    switch (arg.Button)
                    {
                        case MouseButtons.Left:
                            LoadProject(path + @"\project.mdita");
                            break;
                    }


                };
                ribbonMenu.OrbDropDown.RecentItems.Add(recentItem);
            }
        }

        public int OpenSlideIndex
        {
            get { return slideList.OpenSlideIndex; }
            set { slideList.OpenSlideIndex = value; }
        }

        public bool ScrollVisible
        {
            get { return vScrollBar.Visible; }
            set { vScrollBar.Visible = value; }
        }

        public void RecreateMenu()
        {
            slideList.RecreateMenu();
        }

        private void btnInsertNote_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null)
            {
                Focus();
                ControlFactory.getNoteForPanel(SelectedPanel);
                DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);
            }
        }

        private void btnInsertTextBox_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null)
            {
                Focus();
                ControlFactory.getTextFieldForPanel(SelectedPanel);
                DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column,
                DitaClipboard.ActiveSectiondiv, true);
            }
        }

        private void btnInsertFigure_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null)
            {
                ControlFactory.getPictureBoxForPanel(SelectedPanel);
                MainForm.Instance.enumerateFigures();
                DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);
            }
            MainForm.Instance.OpenSlide(ProjectSingleton.SelectedSection);

        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.BoldSelected();
            }
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.ItalicSelected();
            }
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.UnderlineSelected();
            }
        }

        private void btnSuperscript_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.Superscript();
            }
        }

        private void btnSubscript_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                TextBoxControl selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.Subscript();
            }
        }

        private void btnHtml_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                CreateLinkForm link = new CreateLinkForm();
                if (link.ShowDialog(this) == DialogResult.OK)
                {
                    selectedRichTextBox.MakeLink(link.txtInputLink.Text);
                }

            }
            //if (control != null && control is TextBoxControl && !control.IsDisposed)
            //{
            //    var selectedRichTextBox = (TextBoxControl)control;
            //    MessageBox.Show(selectedRichTextBox.HTML());
            //}
        }

        private void btnBulletList_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CreateListBullet();
            }

        }

        private void ribbonButton2_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CreateListNumbered();
            }
        }

     

        private void ribbonButton2Custom_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CreateListNumbered(5);
            }
        }



        private void btnTerm_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CheckSelection();
                selectedRichTextBox.ClearFormat();
                selectedRichTextBox.BoldSelected();
                selectedRichTextBox.ItalicSelected();
                selectedRichTextBox.ColorSelected("#0173b9");
            }
        }

        private void btnHighlight_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CheckSelection();
                selectedRichTextBox.ClearFormat();
                selectedRichTextBox.ColorSelected("#c4b64a");
            }
        }

        private void btnKeyword_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                string keywordSelection = selectedRichTextBox.CurrentSelection();
                if (ProjectSingleton.SelectedContent is LearningContent)
                {
                    LearningContent content = ProjectSingleton.SelectedContent as LearningContent;
                    string currentKeyword = content.Shortdesc.Draftcomment[4].Text.ToLower();
                    if (!currentKeyword.Contains(keywordSelection.ToLower()))
                    {
                        content.Shortdesc.Draftcomment[4].Text += ", " + keywordSelection;
                    }
                }
                selectedRichTextBox.CheckSelection();
                selectedRichTextBox.ClearFormat();
                selectedRichTextBox.UnderlineSelected();
                selectedRichTextBox.ColorSelected("#ff0000");
            }
        }

        private void btnPhrase_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CheckSelection();
                selectedRichTextBox.ClearFormat();
                selectedRichTextBox.ColorSelected("#412977");
                selectedRichTextBox.BoldSelected();
                selectedRichTextBox.ItalicSelected();
            }
        }

        private void btnReservedWord_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CheckSelection();
                selectedRichTextBox.ClearFormat();
                selectedRichTextBox.ColorSelected("#00a651");
            }
        }

        private void btnForeignWord_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CheckSelection();
                selectedRichTextBox.ClearFormat();
                selectedRichTextBox.ColorSelected("#ba3b06");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.ClearFormat();
            }
        }

        private void btnInsertCodePasting_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null)
            {
                EditSnippetForm attach = new EditSnippetForm();
                attach.ShowDialog();
                DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);
            }
        }

        private void btnInsertLatex_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null)
            {
                InsertLatexForm attach = new InsertLatexForm(SelectedPanel);
                attach.ShowDialog();
                DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            // objectPanel.Visible = false;
        }

        private void btnInsertSnippet_Click(object sender, EventArgs e)
        {
            if (SelectedPanel != null)
            {
                EditSnippetForm attach = new EditSnippetForm();
                attach.ShowDialog();
                DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);
            }
        }

        /// <summary>
        /// Importuje sadržaj DITA fajlova
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (Project == null)
            {
                return;
            }
            if (Project.LearningContents.Count == 0)
            {
                ImportDitaFiles.ImportDITA();

                RecreateMenu();
                grafikaPanel.LoadProject();
            }
            else
            {
                MessageBox.Show(
                    "Da bi koristili Import starog DITA projekta ne smete imati ručno napravljenih objekata. Ukoliko želite da dodate samo jedan objekat koristite Import one DITA file opciju");
            }
            CheckErrorsAndStatistics();
        }

        private void btnObjectSlide_Click(object sender, EventArgs e)
        {
            if (Project == null)
            {
                MessageBox.Show("Niste kreirali projekat ili ucitali postojeci. ");
                return;
            }

            if (Project.LearningContents.Count < 14)
            {
                LearningContent learningNew = new LearningContent(null);
                ProjectSingleton.SelectedContent = learningNew;
                SectionsGuiUtil.AddNewSection("section-columns1", false);

                RecreateMenu();
                CheckErrorsAndStatistics();
                DitaClipboard.AddObjectAddedState(learningNew);
            }
            else
            {
                MessageBox.Show("Ne mozete dodati vise od 14 objekata.");
            }
        }

        private void btnSubObjectSlide_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                if (ProjectSingleton.SelectedContent != null)
                {
                    if (ProjectSingleton.SelectedContent is LearningContent)
                    {
                        LearningContent learningNew = new LearningContent(ProjectSingleton.SelectedContent as LearningContent);
                        ProjectSingleton.SelectedContent = learningNew;
                        SectionsGuiUtil.AddNewSection("section-columns1", false);

                        DitaClipboard.AddObjectAddedState(learningNew);

                        CheckErrorsAndStatistics();
                        RecreateMenu();
                        OpenSlide(learningNew);
                    }
                }
                else
                {
                    MessageBox.Show("Morate selektovati objekat za koji zelite da kreirate podobjekat. ");
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            LoadProject();
        }

        private void btnNewProject_Click(object sender, EventArgs e)
        {
            CreateNewProject();
        }

        public bool CloseProject()
        {
            if (Project == null)
            {
                return true;
            }

            if (!DitaClipboard.isStateChanged())
            {
                return true;
            }

            Debug.WriteLine(DitaClipboard.isStateChanged());

            var result = MessageBox.Show("Da li želite da sačuvate projekat?", "Sačuvajte?",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (result)
            {
                case DialogResult.Yes:
                    if (!SaveProject())
                    {
                        return false;
                    }
                    break;
                case DialogResult.Cancel:
                    return false;
            }
            ProjectSingleton.SaveUnsavedImages();
            Project = null;
            return true;
        }

        public void CreateNewProject()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowNewFolderButton = true;
            if (Settings.Default.lastPath != "" || Settings.Default.lastPath != null)
            {
                dialog.SelectedPath = Settings.Default.lastPath;
            }
            Settings.Default.Save();
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string path = dialog.SelectedPath;
            if (path == "")
            {
                return;
            }
            Settings.Default.lastPath = path;

            NewProjectForm projectDialog = new NewProjectForm(path);
            if (projectDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (!CloseProject())
            {
                return;
            }

            Project = projectDialog.ProjectFile;

            ProjectFile.folderLekcije = projectDialog.LessonPath;
        }

        public void LoadProject(string path = null)
        {

            string folderPath = "";
            if (path == null)
            {
                OpenFileDialog dialog = new OpenFileDialog
                {
                    Multiselect = false,
                    Filter = "mDITA projekat (*.mdita)|*.mdita",
                    FileName = "project.mdita"
                };
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                path = dialog.FileName;
            }

            if (!CloseProject())
            {
                return;
            }

            ProjectSingleton.SaveUnsavedImages();
            try
            {
                
                folderPath = path.Substring(0, path.LastIndexOf('\\'));
                //Console.WriteLine(folderPath);
                foreach (string file in Directory.EnumerateFiles(folderPath, "*.dita"))
                {
                    string contents = File.ReadAllText(file);
                    string newContent = contents;


                    do
                    {
                        newContent = contents.Replace(";amp;", ";");
                        contents = newContent;
                        File.WriteAllText(file, contents);
                    } while (contents.Contains(";amp;"));

                    do
                    {
                        newContent = contents.Replace("&amp;lt;", "&lt;");
                        contents = newContent;
                        File.WriteAllText(file, contents);
                    } while (contents.Contains("&amp;lt;"));

                    do
                    {
                        newContent = contents.Replace("&amp;gt;", "&gt;");
                        contents = newContent;
                        File.WriteAllText(file, contents);
                    } while (contents.Contains("&amp;gt;"));

                  
                }
                Project = ProjectFile.LoadFolder(path);
                Debug.WriteLine("Loaded tools: " + Project.LearningOverview.ToolList.Count);

                //Ako ne postoji mapa dodaje mapu
                if (!Project.LearningOverview.ToolList.Any(l => l.ActivityTitle.Equals("MindMapMozak123")))
                {
                    var lntb = new LamsNoticeboard
                    {
                        Title = "MindMapMozak123",
                        ToolContentID = 999,
                        Parent = Project.LearningOverview
                    };
                    Project.LearningOverview.ToolList.Insert( 0,lntb );
                }

                enumerateFigures();

            }
            catch (DirectoryNotFoundException)
            {
                RecentItemsManager.Remove(path.Substring(0, path.LastIndexOf('\\')));
                RefreshRecentItems();
                MessageBox.Show("Folder does not exist.", "Unable to load project", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Project = null;
            }
        }


        //Automatic figures numbering
        public void enumerateFigures()
        {
            //Objects
            foreach (LearningContent lcp in Project.LearningContents)
            {
                enumerateFiguresInObject(lcp.LearningBody);
                //Subobjects
                foreach (LearningContent lc in lcp.SubObjects)
                {
                    enumerateFiguresInObject(lc.LearningBody);
                }
            }
            enumerateFiguresInObject(Project.LearningOverview.LearningBody);
            enumerateFiguresInObject(Project.LearningSummary.LearningBody);
        }

        private void enumerateFiguresInObject(LearningBody lc)
        {
            int num = 1;
            foreach (Section s in lc.Sections)
            {
                foreach (Sectiondiv sd in s.SectionDivs.FindAll(e => e.Outputclass != "subtitle"))
                {
                    foreach (Sectiondiv sdsd in sd.SectionDivs)
                    {
                        foreach (Sectiondiv sdsdsd in sdsd.SectionDivs)
                        {

                            if (sdsdsd.Content.Contains("<fig>"))
                            {
                                sdsdsd.Content = Regex.Replace(sdsdsd.Content, @">Slika-[0-9]*:? ?", ">Slika-" + num + " ");
                                if(sdsdsd.Content.Trim() == "")
                                {
                                    sdsdsd.Content = "Slika-" + num + " ";
                                }
                                num++;
                            }

                        }
                    }

                }

            }
        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveProject();
            enumerateFigures();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (ExportProject(_savingWithExport))
            {
                MessageBox.Show("Lekcija je uspešno eksportovana.");
            }
        }

        private void btnExportBranching_Click(object sender, EventArgs e)
        {
            if (ExportProject(_savingWithExportAndBranching))
            {
                MessageBox.Show("Lekcija je uspešno eksportovana.");
            }
        }

        private void btnImportOneObject_Click(object sender, EventArgs e)
        {
           
            if (Project == null)
            {
                MessageBox.Show("Niste otvorili projekat");
                return;
            }

            if (Project.LearningContents.Count == 14)
            {
                MessageBox.Show("Ne možete dodati više od 14 objekata.");
                return;
            }
            ImportDitaFiles.ImportDITAOne();
            RecreateMenu();
            grafikaPanel.LoadProject();
            CheckErrorsAndStatistics();
        }

        private void btnYouTube_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                if (SelectedPanel != null)
                {
                    YouTubeVideoForm video = new YouTubeVideoForm(SelectedPanel);
                    video.ShowDialog(this);
                    DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);

                }
            }

        }

        private void btnAddVideo_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                if (SelectedPanel != null)
                {
                    if (Util.checkIfHasInternetConnection())
                    {
                        VideoForm video = new VideoForm(SelectedPanel);
                        video.ShowDialog(this);
                        DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);
                    }
                    else
                    {
                        MessageBox.Show("Potrebna je internet konekcija da biste postavili Video materijal na mDita server");
                    }
                }
            }
        }

        public bool CheckErrorsAndStatistics(bool lamsFirst = false)
        {
            if (ProjectSingleton.Project == null)
            {
                return false;
            }
            List<SavingError> errors = Project.CheckSavingErrors(lamsFirst);
            errorListPanel.Errors = errors;
            statisticsControl.Statistics = Project.GetStatistics();
            return errors.Count > 0;
        }

        public void OpenSlide(IDitaSlide slide)
        {
            if (slide is LearningBase)
            {
                OpenSlide((LearningBase) slide);
            }
            else if (slide is Section)
            {
                OpenSlide((Section) slide);
            }
            panelControler.Tag = slide;
            slideList.SelectedSlide = slide;

            ProjectSingleton.SelectedContent = contentControl.Content;
            ProjectSingleton.SelectedSection = sectionControl.Section;
            SelectedPanel = null;
            SelectedControl = null;

            if (sectionControl.Section != null)
            {
                ProjectSingleton.SelectedContent = sectionControl.Section.Parent;
                SelectedPanel = sectionControl._panels[0];
            }
            slideList.Focus();
        }

        public void OpenSlide(LearningBase learningObject)
        {
            panelControler.SuspendLayout();

            sectionControl.Section = null;
            if (learningObject is LearningOverview)
            {
                panelControler.BackgroundImage = Resources.uvod;
                contentControl.Content = null;
            }
            else if (learningObject is LearningContent)
            {
                panelControler.BackgroundImage = null;
                contentControl.Content = (LearningContent)learningObject;
                contentControl.Visible = true;
            }
            else if (learningObject is LearningSummary)
            {
                panelControler.BackgroundImage = Resources.zakljucak;
                contentControl.Content = null;
            }

            panelControler.ResumeLayout();
        }

        public void OpenSlide(Section section)
        {
            
            panelControler.SuspendLayout();

            panelControler.BackgroundImage = null;
            contentControl.Content = null;
            sectionControl.Section = section;

            panelControler.ResumeLayout();
        }

        public void CloseSlide()
        {
            panelControler.SuspendLayout();
                     
            panelControler.BackgroundImage = null;
            contentControl.Content = null;
            sectionControl.Section = null;

            ProjectSingleton.SelectedContent = null;
            ProjectSingleton.SelectedSection = null;
            SelectedPanel = null;
            SelectedControl = null;

            slideList.SelectedSlide = null;
            panelControler.ResumeLayout();
        }

        private void btnInsertSection_Click(object sender, EventArgs e)
        {
            var btn = sender as RibbonButton;
            if (btn == null)
            {
                return;
            }
            SectionsGuiUtil.AddNewSection("section-" + btn.Tag);
        }

        private void btnInsertSectionGallery_Click(object sender, EventArgs e)
        {
            Section sec = SectionsGuiUtil.AddNewSection("section-columns1", false, false);
            if (sec == null)
            {
                return;
            }
            sec.SectionDivs[1].SectionDivs[0].SectionDivs.Add(new Sectiondiv("flexslider"));

            CheckErrorsAndStatistics();
            RecreateMenu();
            OpenSlide(sec);

            DitaClipboard.AddSectionAddedState(sec);
        }

        private void btnChangeSectionLayout_Click(object sender, EventArgs e)
        {
            var btn = sender as RibbonButton;
            if (btn == null)
            {
                return;
            }

            var layout = btn.Tag.ToString();
            var section = ProjectSingleton.SelectedSection;
            if (section == null)
            {
                return;
            }

            var json = new JavaScriptSerializer().Serialize(section);
            if (json.Contains("flexslider"))
            {
                MessageBox.Show("Galeriji ne možete promeniti Layout");
                return;
            }

            var tempLayout = section.SectionDivs[section.SectionDivs.Count - 1].Outputclass;
            if (section.ChangeLayout(layout))
            {
                OpenSlide(section);
                DitaClipboard.ChangeLayoutUndoState(tempLayout, layout);
            }
            else
            {
                MessageBox.Show("Postojeće kontrole ne mogu da stanu na zadati layout.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            panelResults.Items.Clear();
            string searchText = txtSearch.TextBoxText;
            List<LearningContent> objectData = ObjectSearch.GetListOfObjects(searchText);
            LoadSearch(objectData);
        }

        public void LoadSearch(List<LearningContent> objectData)
        {
            if (objectData.Count == 0)
            {
                MessageBox.Show("Nema rezultata za zadatu pretragu.");
            }
            foreach (LearningContent data in objectData)
            {
                RibbonButton picBox = new RibbonButton();
                picBox.Image = Resources.objectsearch;
                picBox.Click += PicBox_Click;
                picBox.Tag = data;
                picBox.Text = data.Title.Trim();
                picBox.ToolTipTitle = data.Shortdesc.Draftcomment[5].Text.Trim() + "-" + data.lesson + " - " + data.Id;
                picBox.ToolTip = data.Title.Trim();
                panelResults.Items.Add(picBox);
            }
            ribbonMenu.ActiveTab = tabSearch;
        }

        private void PicBox_Click(object sender, EventArgs e)
        {
            LearningContent content = (LearningContent)((RibbonItem)sender).Tag;
            ObjectPreviewForm form = new ObjectPreviewForm(content);
            form.ShowDialog();
        }

   
        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        btnSave.PerformClick();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Z:
                        DitaClipboard.Undo();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.N:
                        btnNewProject.PerformClick();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.O:
                        btnOpen.PerformClick();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.I:
                        btnImportOneObject.PerformClick();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Y:
                        DitaClipboard.Redo();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.V:
                        PerformPaste();
                        //e.SuppressKeyPress = true;
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.PageDown:
                        ++OpenSlideIndex;
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.PageUp:
                        if (OpenSlideIndex > 0)
                        {
                            --OpenSlideIndex;
                        }
                        e.SuppressKeyPress = true;
                        break;
                }
            }
            
        }

        private void PerformPaste()
        {
            if (Instance.SelectedPanel != null && Instance.SelectedPanel.Focused){
                Instance.SelectedPanel.Paste();
            }
        }

        /// <summary>
        /// Event handler koji se pokrece klikom na dugme za izmenu podataka o projektu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditProject_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                EditProjectForm updateForm = new EditProjectForm();
                if (updateForm.ShowDialog() == DialogResult.OK)
                {
                    //MessageBox.Show("Nakon sledećeg čuvanja izmene će se primeniti nad svim objektima učenja.");
                    slideList.referesh();
                    SaveProject();
                    setTitle();
                    RefreshRecentItems();
                }
            }
            else
            {
                MessageBox.Show("Niste kreirali projekat ili učitali postojeći.");
            }
        }

        private void rbUndo_Click(object sender, EventArgs e)
        {
            //Focus();
            DitaClipboard.Undo();
          
        }

        private void rbRedo_Click(object sender, EventArgs e)
        {
            //Focus();
            DitaClipboard.Redo();
         
        }

        private void btnAddAudio_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                if (SelectedPanel != null)
                {
                    if (Util.checkIfHasInternetConnection())
                    {
                        AudioForm audio = new AudioForm(SelectedPanel);
                        audio.ShowDialog(this);
                        DitaClipboard.ControlAddOrDeleteState(SelectedPanel.Column, DitaClipboard.ActiveSectiondiv, true);
                    }
                    else
                    {
                        MessageBox.Show("Greska pri konekciji na server. Pokusajte ponovo. ");
                    }
                }
            }
        }

        private ProjectSavingForm _savingForm = new ProjectSavingForm(false, false);
        private ProjectSavingForm _savingWithExport = new ProjectSavingForm(true,false);
        private ProjectSavingForm _savingWithExportAndBranching = new ProjectSavingForm(true, true);

        private bool SaveProject()
        {
            CheckErrorsAndStatistics();
            grafikaPanel.LoadProject();
            if (Project == null)
            {
                return true;
            }
            _savingForm.ShowDialog();
            return _savingForm.FileSaved;
        }

        private bool ExportProject(ProjectSavingForm psv)
        {
            CheckErrorsAndStatistics();
            grafikaPanel.LoadProject();
            if (Project == null)
            {
                return false;
            }
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.OverwritePrompt = true;
            saveDialog.FileName = ProjectSingleton.Project.CourseCode + "-" + ProjectSingleton.Project.LessonNumber + ".zip";
            saveDialog.Filter = "Zip Files (*.zip)|*.zip";
            saveDialog.DefaultExt = "zip";
            saveDialog.AddExtension = true;
            DialogResult dgResult = saveDialog.ShowDialog();
            if (dgResult == DialogResult.OK)
            {
                psv.ExportPath = Path.GetFullPath(saveDialog.FileName);
                psv.ShowDialog();
                return psv.FileSaved;
            }
            return false;
               
        }

        private void btnQA_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                AdditionalActivitiesForm manage = new AdditionalActivitiesForm(ProjectSingleton.SelectedContent);
                manage.ShowDialog();
            }
        }

        private void btnAdvancedSearch_Click(object sender, EventArgs e)
        {
            AdvancedSearchForm advanced = AdvancedSearchForm.getIntance();
            advanced.ShowDialog();
            if (advanced.DialogResult == DialogResult.OK)
            {
                panelResults.Items.Clear();
                string searchText = advanced.Result;
                List<LearningContent> objectData = ObjectSearch.GetListOfObjects(searchText);
                LoadSearch(objectData);
            }
        }

        private void btnPreviewHTML_Click(object sender, EventArgs e)
        {
            if (Project == null)
            {
                return;
            }

            if (CheckErrorsAndStatistics())
            {
                MessageBox.Show("Imate greške u projektu zbog kojih ne možemo da napravimo HTML Preview");
            }
            else
            {
                btnSave.PerformClick();
                new GenerateHtmlForm().ShowDialog();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var updateForm = new UpdateForm(Program.AppVersion);
            var result = updateForm.ShowDialog();
            if (result == DialogResult.Yes)
            {
                Close();
                if (IsDisposed)
                {
                    updateForm.RunUpdateSetup();
                }
            }
        }

        private void btnMergeProject_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "mDITA projekat (*.mdita)|*.mdita",
                FileName = "project.mdita"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Project.MergeByProjectFile(dialog.FileName);
            }
        }

        private void tabGreskeIWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                e.Handled = true;
            }
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            AboutUsForm about = new AboutUsForm();
            about.ShowDialog();
        }

        private void btnNoteTextColor_Click(object sender, EventArgs e)
        {
            var btn = sender as RibbonColorChooser;
            if (btn == null)
            {
                return;
            }

            var note = SelectedControl as NoteControl;
            string textColor = btn.Tag.ToString();

            Debug.WriteLine(note.CurrentBackground);
            Debug.WriteLine(textColor);

            if (note.CurrentBackground == textColor)
            {
                MessageBox.Show("Boja teksta i boja pozadine ne može biti ista.");
            }
            else
            {
                note?.ReinitColor(textColor);
            }
        }

        private void btnNoteBgColor_Click(object sender, EventArgs e)
        {
            var btn = sender as RibbonColorChooser;
            if (btn == null)
            {
                return;
            }

            var note = SelectedControl as NoteControl;
            string bgColor = btn.Tag.ToString();

            if (note.CurrentColor == bgColor)
            {
                MessageBox.Show("Boja teksta i boja pozadine ne može biti ista.");
            }
            else
            {
                note?.ReinitBackground(bgColor);
            }
        }

        private void btnDitaImageChange_Click(object sender, EventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }
            img.ChangeImage();
        }

        private void numDitaImageWidth_TextBoxTextChanged(object sender, EventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }

            try
            {
                int w = int.Parse(numDitaImageWidth.TextBoxText);
                if (w < 1)
                {
                    w = 1;
                }
                else if (w > SelectedPanel.Width)
                {
                    w = SelectedPanel.Width;
                }
                img.PicBox.Width = w;
            }
            catch (Exception)
            { }
            if (numDitaImageWidth.TextBoxText != img.PicBox.Width.ToString())
            {
                numDitaImageWidth.TextBoxText = img.PicBox.Width.ToString();
            }
        }

        private void numDitaImageWidth_UpButtonClicked(object sender, MouseEventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }
            numDitaImageWidth.TextBoxText = (img.PicBox.Width + 1).ToString();
        }

        private void numDitaImageWidth_DownButtonClicked(object sender, MouseEventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }
            numDitaImageWidth.TextBoxText = (img.PicBox.Width - 1).ToString();
        }

        private void numDitaImageHeight_TextBoxTextChanged(object sender, EventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }

            try
            {
                int h = int.Parse(numDitaImageHeight.TextBoxText);
                if (h < 1)
                {
                    h = 1;
                }
                else if (h - img.PicBox.Height > SelectedPanel.HeightLeftPanel())
                {
                    h = img.PicBox.Height + SelectedPanel.HeightLeftPanel();
                }
                img.PicBox.Height = h;
            }
            catch (Exception)
            { }
            if (numDitaImageHeight.TextBoxText != img.PicBox.Height.ToString())
            {
                numDitaImageHeight.TextBoxText = img.PicBox.Height.ToString();
            }
        }

        private void numDitaImageHeight_UpButtonClicked(object sender, MouseEventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }
            numDitaImageHeight.TextBoxText = (img.PicBox.Height + 1).ToString();
        }

        private void numDitaImageHeight_DownButtonClicked(object sender, MouseEventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }
            numDitaImageHeight.TextBoxText = (img.PicBox.Height + 1).ToString();
        }

        private void chbDitaImageShowDescription_CheckBoxCheckChanging(object sender, CancelEventArgs e)
        {
            var img = SelectedControl as ImageBoxControl;
            if (img == null)
            {
                return;
            }

            e.Cancel = !img.ShowDescription(!chbDitaImageShowDescription.Checked);
            if (e.Cancel)
            {
                MessageBox.Show("Nema mesta na sekciji.");
            }
        }

        private void numDitaImageSize_TextBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '0':
                case '\b':
                    break;
                default:
                    e.Handled = true;
                    break;
            }
        }

        private void numDitaImageSize_TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:
                    e.SuppressKeyPress = true;
                    e.Handled = true;
                    break;
            }
        }



        private void btnMultilevel_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CreateSubList("ol");
            }
        }

        private void btnMultilevelBullet_Click(object sender, EventArgs e)
        {
            Control control = SelectedControl;
            if (control != null && control is TextBoxControl && !control.IsDisposed)
            {
                var selectedRichTextBox = (TextBoxControl)control;
                selectedRichTextBox.CreateSubList("ul");


                
            }

        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            panelControler.Hide();
            panelControler.Width = Convert.ToInt32(this.Width * 0.65);
            panelControler.Height = Convert.ToInt32(this.Height * 0.75);
            if ( this.Height / 5 - 20 > 0 )
            {
                panelControler.Location = new Point( this.Width / 6, this.Height / 6 );
            }
            else
            {
                
            }

            panelControler.Show();
           
        }
    }
}
