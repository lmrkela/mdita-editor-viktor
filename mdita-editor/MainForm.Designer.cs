using System;
using System.Windows.Forms;
using mDitaEditor.CustomControls;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Lams.Editor;

namespace mDitaEditor
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            mDitaEditor.Project.ProjectFile.Statistics statistics1 = new mDitaEditor.Project.ProjectFile.Statistics();
            this.ribbonMenu = new System.Windows.Forms.Ribbon();
            this.btnNewProject = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnOpen = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator1 = new System.Windows.Forms.RibbonSeparator();
            this.btnSave = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnExport = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnExportBranching = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnEditProject = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator2 = new System.Windows.Forms.RibbonSeparator();
            this.btnImport = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnImportOneObject = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnMergeProject = new System.Windows.Forms.RibbonOrbMenuItem();
            this.ribbonSeparator3 = new System.Windows.Forms.RibbonSeparator();
            this.btnPreviewHTML = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnAbout = new System.Windows.Forms.RibbonOrbMenuItem();
            this.btnUpdate = new System.Windows.Forms.RibbonOrbMenuItem();
            this.tabDita = new System.Windows.Forms.RibbonTab();
            this.panDitaGeneral = new System.Windows.Forms.RibbonPanel();
            this.rbUndo = new System.Windows.Forms.RibbonButton();
            this.rbRedo = new System.Windows.Forms.RibbonButton();
            this.panDitaSlide = new System.Windows.Forms.RibbonPanel();
            this.btnInsertSlide = new System.Windows.Forms.RibbonButton();
            this.btnObjectSlide = new System.Windows.Forms.RibbonButton();
            this.btnSubObjectSlide = new System.Windows.Forms.RibbonButton();
            this.btnInsertSection = new System.Windows.Forms.RibbonButton();
            this.btnInsertSection1 = new System.Windows.Forms.RibbonButton();
            this.btnInsertSection2 = new System.Windows.Forms.RibbonButton();
            this.btnInsertSection3 = new System.Windows.Forms.RibbonButton();
            this.btnInsertSection12 = new System.Windows.Forms.RibbonButton();
            this.btnInsertSection21 = new System.Windows.Forms.RibbonButton();
            this.btnInsertSectionGallery = new System.Windows.Forms.RibbonButton();
            this.btnLayout = new System.Windows.Forms.RibbonButton();
            this.btnLC_1 = new System.Windows.Forms.RibbonButton();
            this.btnLC_2 = new System.Windows.Forms.RibbonButton();
            this.btnLC_3 = new System.Windows.Forms.RibbonButton();
            this.btnLC_1_2 = new System.Windows.Forms.RibbonButton();
            this.btnLC_2_1 = new System.Windows.Forms.RibbonButton();
            this.btnQA = new System.Windows.Forms.RibbonButton();
            this.panDitaInsert = new System.Windows.Forms.RibbonPanel();
            this.btnInsertTextBox = new System.Windows.Forms.RibbonButton();
            this.btnInsertNote = new System.Windows.Forms.RibbonButton();
            this.btnInsertSnippet = new System.Windows.Forms.RibbonButton();
            this.btnInsertLatex = new System.Windows.Forms.RibbonButton();
            this.btnInsertFigure = new System.Windows.Forms.RibbonButton();
            this.btnInsertYoutube = new System.Windows.Forms.RibbonButton();
            this.btnInsertVideo = new System.Windows.Forms.RibbonButton();
            this.btnInsertAudio = new System.Windows.Forms.RibbonButton();
            this.panDitaWords = new System.Windows.Forms.RibbonPanel();
            this.btnGroup1 = new System.Windows.Forms.RibbonItemGroup();
            this.btnBold = new System.Windows.Forms.RibbonButton();
            this.btnItalic = new System.Windows.Forms.RibbonButton();
            this.btnUnderline = new System.Windows.Forms.RibbonButton();
            this.btnSuperscript = new System.Windows.Forms.RibbonButton();
            this.btnGroupTwo = new System.Windows.Forms.RibbonItemGroup();
            this.btnBulletList = new System.Windows.Forms.RibbonButton();
            this.btnMultilevelBullet = new System.Windows.Forms.RibbonButton();
            this.btnNumberedList = new System.Windows.Forms.RibbonButton();
            this.btnMultilevel = new System.Windows.Forms.RibbonButton();
            this.btnLink = new System.Windows.Forms.RibbonButton();
            this.btnSubscript = new System.Windows.Forms.RibbonButton();
            this.btnKeyword = new System.Windows.Forms.RibbonButton();
            this.btnTerm = new System.Windows.Forms.RibbonButton();
            this.btnPhrase = new System.Windows.Forms.RibbonButton();
            this.btnHighlight = new System.Windows.Forms.RibbonButton();
            this.btnOther = new System.Windows.Forms.RibbonButton();
            this.btnForeignWord = new System.Windows.Forms.RibbonButton();
            this.btnReservedWord = new System.Windows.Forms.RibbonButton();
            this.btnClear = new System.Windows.Forms.RibbonButton();
            this.panDitaNote = new System.Windows.Forms.RibbonPanel();
            this.btnNoteTextColor = new System.Windows.Forms.RibbonButton();
            this.btnNoteTextWhite = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextBlack = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextBlue = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextRed = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextYellow = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextGreen = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextGray = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextOrange = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteTextCyan = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgColor = new System.Windows.Forms.RibbonButton();
            this.btnNoteBgWhite = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgBlack = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgBlue = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgRed = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgYellow = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgGreen = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgGray = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgOrange = new System.Windows.Forms.RibbonColorChooser();
            this.btnNoteBgCyan = new System.Windows.Forms.RibbonColorChooser();
            this.panDitaImage = new System.Windows.Forms.RibbonPanel();
            this.btnDitaImageChange = new System.Windows.Forms.RibbonButton();
            this.numDitaImageWidth = new System.Windows.Forms.RibbonUpDown();
            this.numDitaImageHeight = new System.Windows.Forms.RibbonUpDown();
            this.chbDitaImageShowDescription = new System.Windows.Forms.RibbonCheckBox();
            this.tabSearch = new System.Windows.Forms.RibbonTab();
            this.ribbonPanel1 = new System.Windows.Forms.RibbonPanel();
            this.btnAdvancedSearch = new System.Windows.Forms.RibbonButton();
            this.txtSearch = new System.Windows.Forms.RibbonTextBox();
            this.btnSearch = new System.Windows.Forms.RibbonButton();
            this.panelResults = new System.Windows.Forms.RibbonPanel();
            this.tabGrafika = new System.Windows.Forms.RibbonTab();
            this.panAdditionalActivities = new System.Windows.Forms.RibbonPanel();
            this.btnAdditionalActivitiesWindow = new System.Windows.Forms.RibbonButton();
            this.btnAddAditionalActivity = new System.Windows.Forms.RibbonButton();
            this.btnAssessment = new System.Windows.Forms.RibbonButton();
            this.btnChat = new System.Windows.Forms.RibbonButton();
            this.btnForum = new System.Windows.Forms.RibbonButton();
            this.btnMultipleChoice = new System.Windows.Forms.RibbonButton();
            this.btnQuestionAndAnswer = new System.Windows.Forms.RibbonButton();
            this.btnShareResources = new System.Windows.Forms.RibbonButton();
            this.btnSubmitFiles = new System.Windows.Forms.RibbonButton();
            this.btnJavagrader = new System.Windows.Forms.RibbonButton();
            this.chbShowTransparentObjects = new System.Windows.Forms.RibbonCheckBox();
            this.chbShowTransparentTools = new System.Windows.Forms.RibbonCheckBox();
            this.panGraphicsObjects = new System.Windows.Forms.RibbonPanel();
            this.btnGate = new System.Windows.Forms.RibbonButton();
            this.btnBranch = new System.Windows.Forms.RibbonButton();
            this.btnOptional = new System.Windows.Forms.RibbonButton();
            this.panGraphicTools = new System.Windows.Forms.RibbonPanel();
            this.btnGraphicsMove = new System.Windows.Forms.RibbonButton();
            this.btnGraphicsConnect = new System.Windows.Forms.RibbonButton();
            this.panGrid = new System.Windows.Forms.RibbonPanel();
            this.btnGraphicsAutoArrange = new System.Windows.Forms.RibbonButton();
            this.btnSortInColumns = new System.Windows.Forms.RibbonButton();
            this.btnSortInRows = new System.Windows.Forms.RibbonButton();
            this.btnSortRectangle = new System.Windows.Forms.RibbonButton();
            this.btnSortCircle = new System.Windows.Forms.RibbonButton();
            this.btnSortByObject = new System.Windows.Forms.RibbonButton();
            this.btnSortSnake = new System.Windows.Forms.RibbonButton();
            this.btnSortMaze = new System.Windows.Forms.RibbonButton();
            this.chbAutoArrange = new System.Windows.Forms.RibbonCheckBox();
            this.btnClearCanvas = new System.Windows.Forms.RibbonButton();
            this.btnCenterCanvas = new System.Windows.Forms.RibbonButton();
            this.chbShowGrid = new System.Windows.Forms.RibbonCheckBox();
            this.chbSnapToGrid = new System.Windows.Forms.RibbonCheckBox();
            this.panelControler = new System.Windows.Forms.Panel();
            this.contentControl = new mDitaEditor.Dita.Controls.LearningContentControl();
            this.sectionControl = new mDitaEditor.Dita.Controls.LearningSectionControl();
            this.ribbonUpDown1 = new System.Windows.Forms.RibbonUpDown();
            this.ribbonUpDown2 = new System.Windows.Forms.RibbonUpDown();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.numGridSize = new System.Windows.Forms.NumericUpDown();
            this.labGridSize = new System.Windows.Forms.Label();
            this.trbGridSize = new System.Windows.Forms.TrackBar();
            this.numZoom = new System.Windows.Forms.NumericUpDown();
            this.labelZoom = new System.Windows.Forms.Label();
            this.trbZoom = new System.Windows.Forms.TrackBar();
            this.tabGreskeIWord = new System.Windows.Forms.TabControl();
            this.tabGreske = new System.Windows.Forms.TabPage();
            this.errorListPanel = new mDitaEditor.CustomControls.ErrorListPanel();
            this.tabWord = new System.Windows.Forms.TabPage();
            this.wordImport1 = new mDitaEditor.CustomControls.WordImportPanel();
            this.tabStatistics = new System.Windows.Forms.TabPage();
            this.statisticsControl = new mDitaEditor.CustomControls.StatisticsControl();
            this.btnInsertCodePasting = new System.Windows.Forms.RibbonButton();
            this.slideList = new mDitaEditor.Dita.Controls.SlideListControl();
            this.grafikaPanel = new mDitaEditor.Lams.Editor.GrafikaPanel();
            this.transparentPanel = new mDitaEditor.CustomControls.TransparentPanel();
            this.panelControler.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbGridSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZoom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).BeginInit();
            this.tabGreskeIWord.SuspendLayout();
            this.tabGreske.SuspendLayout();
            this.tabWord.SuspendLayout();
            this.tabStatistics.SuspendLayout();
            this.SuspendLayout();
            // 
            // ribbonMenu
            // 
            this.ribbonMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ribbonMenu.BorderMode = System.Windows.Forms.RibbonWindowMode.NonClientAreaCustomDrawn;
            this.ribbonMenu.CaptionBarVisible = false;
            this.ribbonMenu.Cursor = System.Windows.Forms.Cursors.Default;
            this.ribbonMenu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ribbonMenu.Location = new System.Drawing.Point(0, 0);
            this.ribbonMenu.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonMenu.Minimized = false;
            this.ribbonMenu.Name = "ribbonMenu";
            // 
            // 
            // 
            this.ribbonMenu.OrbDropDown.BorderRoundness = 8;
            this.ribbonMenu.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnNewProject);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnOpen);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.ribbonSeparator1);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnSave);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnExport);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnExportBranching);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnEditProject);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.ribbonSeparator2);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnImport);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnImportOneObject);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnMergeProject);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.ribbonSeparator3);
            this.ribbonMenu.OrbDropDown.MenuItems.Add(this.btnPreviewHTML);
            this.ribbonMenu.OrbDropDown.Name = "";
            this.ribbonMenu.OrbDropDown.OptionItems.Add(this.btnAbout);
            this.ribbonMenu.OrbDropDown.OptionItems.Add(this.btnUpdate);
            this.ribbonMenu.OrbDropDown.RecentItemsCaption = "Recent items";
            this.ribbonMenu.OrbDropDown.Size = new System.Drawing.Size(527, 524);
            this.ribbonMenu.OrbDropDown.TabIndex = 0;
            this.ribbonMenu.OrbImage = null;
            this.ribbonMenu.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2013;
            this.ribbonMenu.OrbText = "File";
            // 
            // 
            // 
            this.ribbonMenu.QuickAcessToolbar.DropDownButtonVisible = false;
            this.ribbonMenu.QuickAcessToolbar.Enabled = false;
            this.ribbonMenu.QuickAcessToolbar.Text = "mDita Editor";
            this.ribbonMenu.QuickAcessToolbar.Visible = false;
            this.ribbonMenu.RibbonTabFont = new System.Drawing.Font("Trebuchet MS", 9F);
            this.ribbonMenu.Size = new System.Drawing.Size(1264, 118);
            this.ribbonMenu.TabIndex = 0;
            this.ribbonMenu.Tabs.Add(this.tabDita);
            this.ribbonMenu.Tabs.Add(this.tabSearch);
            this.ribbonMenu.Tabs.Add(this.tabGrafika);
            this.ribbonMenu.TabsMargin = new System.Windows.Forms.Padding(12, 2, 20, 0);
            this.ribbonMenu.Text = "mDita Editor";
            this.ribbonMenu.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            this.ribbonMenu.ActiveTabChanged += new System.EventHandler(this.ribbonMenu_ActiveTabChanged);
            this.ribbonMenu.ExpandedChanged += new System.EventHandler(this.ribbonMenu_ExpandedChanged);
            // 
            // btnNewProject
            // 
            this.btnNewProject.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNewProject.Image = global::mDitaEditor.Properties.Resources.newicon;
            this.btnNewProject.SmallImage = global::mDitaEditor.Properties.Resources.newicon;
            this.btnNewProject.Text = "New project";
            this.btnNewProject.Click += new System.EventHandler(this.btnNewProject_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnOpen.Image = global::mDitaEditor.Properties.Resources.open;
            this.btnOpen.MinimumSize = new System.Drawing.Size(100, 80);
            this.btnOpen.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Large;
            this.btnOpen.SmallImage = global::mDitaEditor.Properties.Resources.open;
            this.btnOpen.Text = "Open project";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnSave
            // 
            this.btnSave.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSave.Image = global::mDitaEditor.Properties.Resources.save;
            this.btnSave.SmallImage = global::mDitaEditor.Properties.Resources.save;
            this.btnSave.Text = "Save project";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExport
            // 
            this.btnExport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnExport.Image = global::mDitaEditor.Properties.Resources.export;
            this.btnExport.SmallImage = global::mDitaEditor.Properties.Resources.export;
            this.btnExport.Text = "Save Zip file";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnExportBranching
            // 
            this.btnExportBranching.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnExportBranching.Image = global::mDitaEditor.Properties.Resources.branch;
            this.btnExportBranching.SmallImage = global::mDitaEditor.Properties.Resources.branch24;
            this.btnExportBranching.Text = "Zip with branching";
            this.btnExportBranching.Click += new System.EventHandler(this.btnExportBranching_Click);
            // 
            // btnEditProject
            // 
            this.btnEditProject.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnEditProject.Image = global::mDitaEditor.Properties.Resources.edit_project;
            this.btnEditProject.SmallImage = global::mDitaEditor.Properties.Resources.edit_project;
            this.btnEditProject.Text = "Edit project";
            this.btnEditProject.Click += new System.EventHandler(this.btnEditProject_Click);
            // 
            // btnImport
            // 
            this.btnImport.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnImport.Image = global::mDitaEditor.Properties.Resources.import;
            this.btnImport.SmallImage = global::mDitaEditor.Properties.Resources.import;
            this.btnImport.Text = "Import dita files";
            this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
            // 
            // btnImportOneObject
            // 
            this.btnImportOneObject.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnImportOneObject.Image = global::mDitaEditor.Properties.Resources.import;
            this.btnImportOneObject.SmallImage = global::mDitaEditor.Properties.Resources.import;
            this.btnImportOneObject.Text = "Import dita object";
            this.btnImportOneObject.Click += new System.EventHandler(this.btnImportOneObject_Click);
            // 
            // btnMergeProject
            // 
            this.btnMergeProject.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnMergeProject.Image = global::mDitaEditor.Properties.Resources.mergeicon;
            this.btnMergeProject.SmallImage = global::mDitaEditor.Properties.Resources.mergeicon;
            this.btnMergeProject.Text = "Merge projects";
            this.btnMergeProject.Click += new System.EventHandler(this.btnMergeProject_Click);
            // 
            // btnPreviewHTML
            // 
            this.btnPreviewHTML.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnPreviewHTML.Image = global::mDitaEditor.Properties.Resources.html;
            this.btnPreviewHTML.SmallImage = global::mDitaEditor.Properties.Resources.html;
            this.btnPreviewHTML.Text = "Preview HTML";
            this.btnPreviewHTML.Click += new System.EventHandler(this.btnPreviewHTML_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnAbout.Image = global::mDitaEditor.Properties.Resources.mdita24;
            this.btnAbout.SmallImage = global::mDitaEditor.Properties.Resources.mdita24;
            this.btnAbout.Text = "About";
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnUpdate.Image = global::mDitaEditor.Properties.Resources.update24;
            this.btnUpdate.SmallImage = global::mDitaEditor.Properties.Resources.update24;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // tabDita
            // 
            this.tabDita.Panels.Add(this.panDitaGeneral);
            this.tabDita.Panels.Add(this.panDitaSlide);
            this.tabDita.Panels.Add(this.panDitaInsert);
            this.tabDita.Panels.Add(this.panDitaWords);
            this.tabDita.Panels.Add(this.panDitaNote);
            this.tabDita.Panels.Add(this.panDitaImage);
            this.tabDita.Text = "DITA Editor";
            // 
            // panDitaGeneral
            // 
            this.panDitaGeneral.ButtonMoreEnabled = false;
            this.panDitaGeneral.ButtonMoreVisible = false;
            this.panDitaGeneral.Items.Add(this.rbUndo);
            this.panDitaGeneral.Items.Add(this.rbRedo);
            this.panDitaGeneral.Text = "Clipboard";
            // 
            // rbUndo
            // 
            this.rbUndo.Enabled = false;
            this.rbUndo.Image = ((System.Drawing.Image)(resources.GetObject("rbUndo.Image")));
            this.rbUndo.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbUndo.SmallImage = global::mDitaEditor.Properties.Resources.undo_icon;
            this.rbUndo.ToolTip = "Undo";
            this.rbUndo.Click += new System.EventHandler(this.rbUndo_Click);
            // 
            // rbRedo
            // 
            this.rbRedo.Enabled = false;
            this.rbRedo.Image = ((System.Drawing.Image)(resources.GetObject("rbRedo.Image")));
            this.rbRedo.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.rbRedo.SmallImage = global::mDitaEditor.Properties.Resources.redo_icon;
            this.rbRedo.ToolTip = "Redo";
            this.rbRedo.Click += new System.EventHandler(this.rbRedo_Click);
            // 
            // panDitaSlide
            // 
            this.panDitaSlide.ButtonMoreEnabled = false;
            this.panDitaSlide.ButtonMoreVisible = false;
            this.panDitaSlide.Items.Add(this.btnInsertSlide);
            this.panDitaSlide.Items.Add(this.btnInsertSection);
            this.panDitaSlide.Items.Add(this.btnLayout);
            this.panDitaSlide.Items.Add(this.btnQA);
            this.panDitaSlide.Text = "Slide";
            // 
            // btnInsertSlide
            // 
            this.btnInsertSlide.DrawIconsBar = false;
            this.btnInsertSlide.DropDownArrowSize = new System.Drawing.Size(10, 10);
            this.btnInsertSlide.DropDownItems.Add(this.btnObjectSlide);
            this.btnInsertSlide.DropDownItems.Add(this.btnSubObjectSlide);
            this.btnInsertSlide.Image = global::mDitaEditor.Properties.Resources.slide;
            this.btnInsertSlide.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertSlide.MinimumSize = new System.Drawing.Size(64, 0);
            this.btnInsertSlide.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertSlide.SmallImage")));
            this.btnInsertSlide.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.btnInsertSlide.Text = "Insert Object";
            // 
            // btnObjectSlide
            // 
            this.btnObjectSlide.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnObjectSlide.Image = global::mDitaEditor.Properties.Resources.objectFull;
            this.btnObjectSlide.SmallImage = global::mDitaEditor.Properties.Resources.objectFull;
            this.btnObjectSlide.Text = "Insert Object";
            this.btnObjectSlide.Click += new System.EventHandler(this.btnObjectSlide_Click);
            // 
            // btnSubObjectSlide
            // 
            this.btnSubObjectSlide.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSubObjectSlide.Image = global::mDitaEditor.Properties.Resources.subobject;
            this.btnSubObjectSlide.SmallImage = global::mDitaEditor.Properties.Resources.subobject;
            this.btnSubObjectSlide.Text = "Insert Subobject";
            this.btnSubObjectSlide.Click += new System.EventHandler(this.btnSubObjectSlide_Click);
            // 
            // btnInsertSection
            // 
            this.btnInsertSection.DrawIconsBar = false;
            this.btnInsertSection.DropDownItems.Add(this.btnInsertSection1);
            this.btnInsertSection.DropDownItems.Add(this.btnInsertSection2);
            this.btnInsertSection.DropDownItems.Add(this.btnInsertSection3);
            this.btnInsertSection.DropDownItems.Add(this.btnInsertSection12);
            this.btnInsertSection.DropDownItems.Add(this.btnInsertSection21);
            this.btnInsertSection.DropDownItems.Add(this.btnInsertSectionGallery);
            this.btnInsertSection.Image = global::mDitaEditor.Properties.Resources.lc1;
            this.btnInsertSection.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertSection.MinimumSize = new System.Drawing.Size(64, 0);
            this.btnInsertSection.SmallImage = global::mDitaEditor.Properties.Resources.lc1;
            this.btnInsertSection.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.btnInsertSection.Text = "Insert Section";
            // 
            // btnInsertSection1
            // 
            this.btnInsertSection1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnInsertSection1.Image = global::mDitaEditor.Properties.Resources.lc1;
            this.btnInsertSection1.SmallImage = global::mDitaEditor.Properties.Resources.lc1;
            this.btnInsertSection1.Tag = "columns1";
            this.btnInsertSection1.Text = "Layout 1";
            this.btnInsertSection1.Click += new System.EventHandler(this.btnInsertSection_Click);
            // 
            // btnInsertSection2
            // 
            this.btnInsertSection2.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnInsertSection2.Image = global::mDitaEditor.Properties.Resources.lc2;
            this.btnInsertSection2.SmallImage = global::mDitaEditor.Properties.Resources.lc2;
            this.btnInsertSection2.Tag = "columns2";
            this.btnInsertSection2.Text = "Layout 2";
            this.btnInsertSection2.Click += new System.EventHandler(this.btnInsertSection_Click);
            // 
            // btnInsertSection3
            // 
            this.btnInsertSection3.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnInsertSection3.Image = global::mDitaEditor.Properties.Resources.lc3;
            this.btnInsertSection3.SmallImage = global::mDitaEditor.Properties.Resources.lc3;
            this.btnInsertSection3.Tag = "columns3";
            this.btnInsertSection3.Text = "Layout 3";
            this.btnInsertSection3.Click += new System.EventHandler(this.btnInsertSection_Click);
            // 
            // btnInsertSection12
            // 
            this.btnInsertSection12.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnInsertSection12.Image = global::mDitaEditor.Properties.Resources.lc12;
            this.btnInsertSection12.SmallImage = global::mDitaEditor.Properties.Resources.lc12;
            this.btnInsertSection12.Tag = "columns2-1-2";
            this.btnInsertSection12.Text = "Layout 1-2";
            this.btnInsertSection12.Click += new System.EventHandler(this.btnInsertSection_Click);
            // 
            // btnInsertSection21
            // 
            this.btnInsertSection21.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnInsertSection21.Image = global::mDitaEditor.Properties.Resources.lc21;
            this.btnInsertSection21.SmallImage = global::mDitaEditor.Properties.Resources.lc21;
            this.btnInsertSection21.Tag = "columns2-2-1";
            this.btnInsertSection21.Text = "Layout 2-1";
            this.btnInsertSection21.Click += new System.EventHandler(this.btnInsertSection_Click);
            // 
            // btnInsertSectionGallery
            // 
            this.btnInsertSectionGallery.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnInsertSectionGallery.Image = global::mDitaEditor.Properties.Resources.gallery;
            this.btnInsertSectionGallery.SmallImage = global::mDitaEditor.Properties.Resources.gallery;
            this.btnInsertSectionGallery.Text = "Gallery";
            this.btnInsertSectionGallery.Click += new System.EventHandler(this.btnInsertSectionGallery_Click);
            // 
            // btnLayout
            // 
            this.btnLayout.DrawIconsBar = false;
            this.btnLayout.DropDownArrowSize = new System.Drawing.Size(10, 10);
            this.btnLayout.DropDownItems.Add(this.btnLC_1);
            this.btnLayout.DropDownItems.Add(this.btnLC_2);
            this.btnLayout.DropDownItems.Add(this.btnLC_3);
            this.btnLayout.DropDownItems.Add(this.btnLC_1_2);
            this.btnLayout.DropDownItems.Add(this.btnLC_2_1);
            this.btnLayout.Image = global::mDitaEditor.Properties.Resources.insert_figure_group;
            this.btnLayout.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnLayout.MinimumSize = new System.Drawing.Size(64, 0);
            this.btnLayout.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnLayout.SmallImage")));
            this.btnLayout.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.btnLayout.Text = "Change Layout";
            // 
            // btnLC_1
            // 
            this.btnLC_1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnLC_1.Image = global::mDitaEditor.Properties.Resources.lc1;
            this.btnLC_1.SmallImage = global::mDitaEditor.Properties.Resources.lc1;
            this.btnLC_1.Tag = "columns1";
            this.btnLC_1.Text = "LC - 1";
            this.btnLC_1.Click += new System.EventHandler(this.btnChangeSectionLayout_Click);
            // 
            // btnLC_2
            // 
            this.btnLC_2.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnLC_2.Image = ((System.Drawing.Image)(resources.GetObject("btnLC_2.Image")));
            this.btnLC_2.SmallImage = global::mDitaEditor.Properties.Resources.lc2;
            this.btnLC_2.Tag = "columns2";
            this.btnLC_2.Text = "LC - 2";
            this.btnLC_2.Click += new System.EventHandler(this.btnChangeSectionLayout_Click);
            // 
            // btnLC_3
            // 
            this.btnLC_3.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnLC_3.Image = ((System.Drawing.Image)(resources.GetObject("btnLC_3.Image")));
            this.btnLC_3.SmallImage = global::mDitaEditor.Properties.Resources.lc3;
            this.btnLC_3.Tag = "columns3";
            this.btnLC_3.Text = "LC - 3";
            this.btnLC_3.Click += new System.EventHandler(this.btnChangeSectionLayout_Click);
            // 
            // btnLC_1_2
            // 
            this.btnLC_1_2.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnLC_1_2.Image = ((System.Drawing.Image)(resources.GetObject("btnLC_1_2.Image")));
            this.btnLC_1_2.SmallImage = global::mDitaEditor.Properties.Resources.lc12;
            this.btnLC_1_2.Tag = "columns2-1-2";
            this.btnLC_1_2.Text = "LC - 1-2";
            this.btnLC_1_2.Click += new System.EventHandler(this.btnChangeSectionLayout_Click);
            // 
            // btnLC_2_1
            // 
            this.btnLC_2_1.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnLC_2_1.Image = ((System.Drawing.Image)(resources.GetObject("btnLC_2_1.Image")));
            this.btnLC_2_1.SmallImage = global::mDitaEditor.Properties.Resources.lc21;
            this.btnLC_2_1.Tag = "columns2-2-1";
            this.btnLC_2_1.Text = "LC - 2-1";
            this.btnLC_2_1.Click += new System.EventHandler(this.btnChangeSectionLayout_Click);
            // 
            // btnQA
            // 
            this.btnQA.Image = global::mDitaEditor.Properties.Resources.forum;
            this.btnQA.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnQA.MinimumSize = new System.Drawing.Size(64, 0);
            this.btnQA.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnQA.SmallImage")));
            this.btnQA.Text = "Aditional Activities";
            this.btnQA.Click += new System.EventHandler(this.btnQA_Click);
            // 
            // panDitaInsert
            // 
            this.panDitaInsert.ButtonMoreEnabled = false;
            this.panDitaInsert.ButtonMoreVisible = false;
            this.panDitaInsert.Items.Add(this.btnInsertTextBox);
            this.panDitaInsert.Items.Add(this.btnInsertNote);
            this.panDitaInsert.Items.Add(this.btnInsertSnippet);
            this.panDitaInsert.Items.Add(this.btnInsertLatex);
            this.panDitaInsert.Items.Add(this.btnInsertFigure);
            this.panDitaInsert.Items.Add(this.btnInsertYoutube);
            this.panDitaInsert.Items.Add(this.btnInsertVideo);
            this.panDitaInsert.Items.Add(this.btnInsertAudio);
            this.panDitaInsert.Tag = "";
            this.panDitaInsert.Text = "Insert";
            // 
            // btnInsertTextBox
            // 
            this.btnInsertTextBox.Image = global::mDitaEditor.Properties.Resources.insert_textbox;
            this.btnInsertTextBox.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertTextBox.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertTextBox.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertTextBox.SmallImage")));
            this.btnInsertTextBox.Text = "Textbox";
            this.btnInsertTextBox.Click += new System.EventHandler(this.btnInsertTextBox_Click);
            // 
            // btnInsertNote
            // 
            this.btnInsertNote.Image = global::mDitaEditor.Properties.Resources.insert_note;
            this.btnInsertNote.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertNote.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertNote.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertNote.SmallImage")));
            this.btnInsertNote.Text = "Note";
            this.btnInsertNote.Click += new System.EventHandler(this.btnInsertNote_Click);
            // 
            // btnInsertSnippet
            // 
            this.btnInsertSnippet.DrawIconsBar = false;
            this.btnInsertSnippet.DropDownArrowSize = new System.Drawing.Size(10, 10);
            this.btnInsertSnippet.Image = global::mDitaEditor.Properties.Resources.insert_code_snippet;
            this.btnInsertSnippet.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertSnippet.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertSnippet.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertSnippet.SmallImage")));
            this.btnInsertSnippet.Text = "Snippet";
            this.btnInsertSnippet.Click += new System.EventHandler(this.btnInsertSnippet_Click);
            // 
            // btnInsertLatex
            // 
            this.btnInsertLatex.Image = global::mDitaEditor.Properties.Resources.equation;
            this.btnInsertLatex.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertLatex.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertLatex.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertLatex.SmallImage")));
            this.btnInsertLatex.Text = "Latex";
            this.btnInsertLatex.Click += new System.EventHandler(this.btnInsertLatex_Click);
            // 
            // btnInsertFigure
            // 
            this.btnInsertFigure.Image = global::mDitaEditor.Properties.Resources.insert_figure;
            this.btnInsertFigure.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertFigure.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertFigure.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertFigure.SmallImage")));
            this.btnInsertFigure.Text = "Image";
            this.btnInsertFigure.Click += new System.EventHandler(this.btnInsertFigure_Click);
            // 
            // btnInsertYoutube
            // 
            this.btnInsertYoutube.Image = global::mDitaEditor.Properties.Resources.youtube;
            this.btnInsertYoutube.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertYoutube.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertYoutube.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertYoutube.SmallImage")));
            this.btnInsertYoutube.Text = "YouTube";
            this.btnInsertYoutube.Click += new System.EventHandler(this.btnYouTube_Click);
            // 
            // btnInsertVideo
            // 
            this.btnInsertVideo.Image = global::mDitaEditor.Properties.Resources.video;
            this.btnInsertVideo.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertVideo.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertVideo.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertVideo.SmallImage")));
            this.btnInsertVideo.Text = "Video";
            this.btnInsertVideo.Click += new System.EventHandler(this.btnAddVideo_Click);
            // 
            // btnInsertAudio
            // 
            this.btnInsertAudio.Image = global::mDitaEditor.Properties.Resources.audiofile;
            this.btnInsertAudio.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnInsertAudio.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnInsertAudio.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnInsertAudio.SmallImage")));
            this.btnInsertAudio.Text = "Audio";
            this.btnInsertAudio.Click += new System.EventHandler(this.btnAddAudio_Click);
            // 
            // panDitaWords
            // 
            this.panDitaWords.ButtonMoreEnabled = false;
            this.panDitaWords.ButtonMoreVisible = false;
            this.panDitaWords.Items.Add(this.btnGroup1);
            this.panDitaWords.Items.Add(this.btnGroupTwo);
            this.panDitaWords.Items.Add(this.btnKeyword);
            this.panDitaWords.Items.Add(this.btnTerm);
            this.panDitaWords.Items.Add(this.btnPhrase);
            this.panDitaWords.Items.Add(this.btnHighlight);
            this.panDitaWords.Items.Add(this.btnOther);
            this.panDitaWords.Items.Add(this.btnClear);
            this.panDitaWords.Text = "Word format";
            // 
            // btnGroup1
            // 
            this.btnGroup1.DrawBackground = false;
            this.btnGroup1.Items.Add(this.btnBold);
            this.btnGroup1.Items.Add(this.btnItalic);
            this.btnGroup1.Items.Add(this.btnUnderline);
            this.btnGroup1.Items.Add(this.btnSuperscript);
            this.btnGroup1.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnGroup1.MinSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnGroup1.Text = "ribbonItemGroup1";
            // 
            // btnBold
            // 
            this.btnBold.Image = global::mDitaEditor.Properties.Resources.bold;
            this.btnBold.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnBold.SmallImage = global::mDitaEditor.Properties.Resources.bold;
            this.btnBold.Text = "ribbonButton1";
            this.btnBold.Click += new System.EventHandler(this.btnBold_Click);
            // 
            // btnItalic
            // 
            this.btnItalic.Image = ((System.Drawing.Image)(resources.GetObject("btnItalic.Image")));
            this.btnItalic.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnItalic.SmallImage = global::mDitaEditor.Properties.Resources.italic;
            this.btnItalic.Text = "ribbonButton1";
            this.btnItalic.Click += new System.EventHandler(this.btnItalic_Click);
            // 
            // btnUnderline
            // 
            this.btnUnderline.Image = ((System.Drawing.Image)(resources.GetObject("btnUnderline.Image")));
            this.btnUnderline.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnUnderline.SmallImage = global::mDitaEditor.Properties.Resources.underline;
            this.btnUnderline.Text = "ribbonButton2";
            this.btnUnderline.Click += new System.EventHandler(this.btnUnderline_Click);
            // 
            // btnSuperscript
            // 
            this.btnSuperscript.Image = ((System.Drawing.Image)(resources.GetObject("btnSuperscript.Image")));
            this.btnSuperscript.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnSuperscript.SmallImage = global::mDitaEditor.Properties.Resources.text_superscript;
            this.btnSuperscript.Text = "Superscript";
            this.btnSuperscript.Click += new System.EventHandler(this.btnSuperscript_Click);
            // 
            // btnGroupTwo
            // 
            this.btnGroupTwo.DrawBackground = false;
            this.btnGroupTwo.Items.Add(this.btnBulletList);
            this.btnGroupTwo.Items.Add(this.btnMultilevelBullet);
            this.btnGroupTwo.Items.Add(this.btnNumberedList);
            this.btnGroupTwo.Items.Add(this.btnMultilevel);
            this.btnGroupTwo.Items.Add(this.btnLink);
            this.btnGroupTwo.Items.Add(this.btnSubscript);
            this.btnGroupTwo.Text = "ribbonItemGroup2";
            // 
            // btnBulletList
            // 
            this.btnBulletList.Image = ((System.Drawing.Image)(resources.GetObject("btnBulletList.Image")));
            this.btnBulletList.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnBulletList.SmallImage = global::mDitaEditor.Properties.Resources.bulletlist;
            this.btnBulletList.Text = "ribbonButton1";
            this.btnBulletList.Click += new System.EventHandler(this.btnBulletList_Click);
            // 
            // btnMultilevelBullet
            // 
            this.btnMultilevelBullet.Image = global::mDitaEditor.Properties.Resources.multilist;
            this.btnMultilevelBullet.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnMultilevelBullet.SmallImage = global::mDitaEditor.Properties.Resources.toc_small;
            this.btnMultilevelBullet.Click += new System.EventHandler(this.btnMultilevelBullet_Click);
            // 
            // btnNumberedList
            // 
            this.btnNumberedList.Image = ((System.Drawing.Image)(resources.GetObject("btnNumberedList.Image")));
            this.btnNumberedList.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnNumberedList.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNumberedList.SmallImage")));
            this.btnNumberedList.Text = "ribbonButton2";
            this.btnNumberedList.Click += new System.EventHandler(this.ribbonButton2_Click);
            // 
            // btnMultilevel
            // 
            this.btnMultilevel.Image = global::mDitaEditor.Properties.Resources.toc_small_num;
            this.btnMultilevel.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnMultilevel.SmallImage = global::mDitaEditor.Properties.Resources.toc_small_num;
            this.btnMultilevel.Click += new System.EventHandler(this.btnMultilevel_Click);
            // 
            // btnLink
            // 
            this.btnLink.Image = global::mDitaEditor.Properties.Resources.link;
            this.btnLink.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnLink.SmallImage = global::mDitaEditor.Properties.Resources.link;
            this.btnLink.Text = "ribbonButton1";
            this.btnLink.Click += new System.EventHandler(this.btnHtml_Click);
            // 
            // btnSubscript
            // 
            this.btnSubscript.Image = ((System.Drawing.Image)(resources.GetObject("btnSubscript.Image")));
            this.btnSubscript.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.Compact;
            this.btnSubscript.SmallImage = global::mDitaEditor.Properties.Resources.text_subscript;
            this.btnSubscript.Text = "Subscript";
            this.btnSubscript.Click += new System.EventHandler(this.btnSubscript_Click);
            // 
            // btnKeyword
            // 
            this.btnKeyword.Image = global::mDitaEditor.Properties.Resources.foreign;
            this.btnKeyword.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnKeyword.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnKeyword.SmallImage = global::mDitaEditor.Properties.Resources.foreign;
            this.btnKeyword.Text = "Keyword";
            this.btnKeyword.Click += new System.EventHandler(this.btnKeyword_Click);
            // 
            // btnTerm
            // 
            this.btnTerm.Image = global::mDitaEditor.Properties.Resources.term1;
            this.btnTerm.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnTerm.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnTerm.SmallImage = global::mDitaEditor.Properties.Resources.term1;
            this.btnTerm.Text = "Term";
            this.btnTerm.Click += new System.EventHandler(this.btnTerm_Click);
            // 
            // btnPhrase
            // 
            this.btnPhrase.Image = global::mDitaEditor.Properties.Resources.phrase2;
            this.btnPhrase.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnPhrase.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnPhrase.SmallImage = global::mDitaEditor.Properties.Resources.phrase2;
            this.btnPhrase.Text = "Phrase";
            this.btnPhrase.Click += new System.EventHandler(this.btnPhrase_Click);
            // 
            // btnHighlight
            // 
            this.btnHighlight.Image = global::mDitaEditor.Properties.Resources.highlight;
            this.btnHighlight.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnHighlight.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnHighlight.SmallImage = global::mDitaEditor.Properties.Resources.highlight;
            this.btnHighlight.Text = "Highlight";
            this.btnHighlight.Click += new System.EventHandler(this.btnHighlight_Click);
            // 
            // btnOther
            // 
            this.btnOther.DropDownArrowSize = new System.Drawing.Size(10, 10);
            this.btnOther.DropDownItems.Add(this.btnForeignWord);
            this.btnOther.DropDownItems.Add(this.btnReservedWord);
            this.btnOther.Image = global::mDitaEditor.Properties.Resources.reserved;
            this.btnOther.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnOther.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnOther.SmallImage = global::mDitaEditor.Properties.Resources.reserved;
            this.btnOther.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.btnOther.Text = "Other";
            // 
            // btnForeignWord
            // 
            this.btnForeignWord.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnForeignWord.Image = global::mDitaEditor.Properties.Resources.foreign;
            this.btnForeignWord.SmallImage = global::mDitaEditor.Properties.Resources.foreign;
            this.btnForeignWord.Text = "Foreign word";
            this.btnForeignWord.Click += new System.EventHandler(this.btnForeignWord_Click);
            // 
            // btnReservedWord
            // 
            this.btnReservedWord.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnReservedWord.Image = global::mDitaEditor.Properties.Resources.term1;
            this.btnReservedWord.SmallImage = global::mDitaEditor.Properties.Resources.term1;
            this.btnReservedWord.Text = "Reserved word";
            this.btnReservedWord.Click += new System.EventHandler(this.btnReservedWord_Click);
            // 
            // btnClear
            // 
            this.btnClear.Image = global::mDitaEditor.Properties.Resources.clear;
            this.btnClear.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnClear.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnClear.SmallImage = global::mDitaEditor.Properties.Resources.clear;
            this.btnClear.Text = "Clear style";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panDitaNote
            // 
            this.panDitaNote.ButtonMoreEnabled = false;
            this.panDitaNote.ButtonMoreVisible = false;
            this.panDitaNote.Items.Add(this.btnNoteTextColor);
            this.panDitaNote.Items.Add(this.btnNoteBgColor);
            this.panDitaNote.Text = "Note options";
            // 
            // btnNoteTextColor
            // 
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextWhite);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextBlack);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextBlue);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextRed);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextYellow);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextGreen);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextGray);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextOrange);
            this.btnNoteTextColor.DropDownItems.Add(this.btnNoteTextCyan);
            this.btnNoteTextColor.Image = global::mDitaEditor.Properties.Resources.colortext;
            this.btnNoteTextColor.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextColor.SmallImage")));
            this.btnNoteTextColor.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.btnNoteTextColor.Text = "Text color";
            this.btnNoteTextColor.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextWhite
            // 
            this.btnNoteTextWhite.Color = System.Drawing.Color.White;
            this.btnNoteTextWhite.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextWhite.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextWhite.Image")));
            this.btnNoteTextWhite.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextWhite.SmallImage")));
            this.btnNoteTextWhite.Tag = "notewhite";
            this.btnNoteTextWhite.Text = "White";
            this.btnNoteTextWhite.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextBlack
            // 
            this.btnNoteTextBlack.Color = System.Drawing.Color.Black;
            this.btnNoteTextBlack.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextBlack.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextBlack.Image")));
            this.btnNoteTextBlack.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextBlack.SmallImage")));
            this.btnNoteTextBlack.Tag = "noteblack";
            this.btnNoteTextBlack.Text = "Black";
            this.btnNoteTextBlack.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextBlue
            // 
            this.btnNoteTextBlue.Color = System.Drawing.Color.Blue;
            this.btnNoteTextBlue.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextBlue.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextBlue.Image")));
            this.btnNoteTextBlue.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextBlue.SmallImage")));
            this.btnNoteTextBlue.Tag = "noteblue";
            this.btnNoteTextBlue.Text = "Blue";
            this.btnNoteTextBlue.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextRed
            // 
            this.btnNoteTextRed.Color = System.Drawing.Color.Red;
            this.btnNoteTextRed.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextRed.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextRed.Image")));
            this.btnNoteTextRed.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextRed.SmallImage")));
            this.btnNoteTextRed.Tag = "notered";
            this.btnNoteTextRed.Text = "Red";
            this.btnNoteTextRed.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextYellow
            // 
            this.btnNoteTextYellow.Color = System.Drawing.Color.Yellow;
            this.btnNoteTextYellow.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextYellow.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextYellow.Image")));
            this.btnNoteTextYellow.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextYellow.SmallImage")));
            this.btnNoteTextYellow.Tag = "noteyellow";
            this.btnNoteTextYellow.Text = "Yellow";
            this.btnNoteTextYellow.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextGreen
            // 
            this.btnNoteTextGreen.Color = System.Drawing.Color.Green;
            this.btnNoteTextGreen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextGreen.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextGreen.Image")));
            this.btnNoteTextGreen.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextGreen.SmallImage")));
            this.btnNoteTextGreen.Tag = "notegreen";
            this.btnNoteTextGreen.Text = "Green";
            this.btnNoteTextGreen.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextGray
            // 
            this.btnNoteTextGray.Color = System.Drawing.Color.Gray;
            this.btnNoteTextGray.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextGray.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextGray.Image")));
            this.btnNoteTextGray.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextGray.SmallImage")));
            this.btnNoteTextGray.Tag = "notegray";
            this.btnNoteTextGray.Text = "Gray";
            this.btnNoteTextGray.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextOrange
            // 
            this.btnNoteTextOrange.Color = System.Drawing.Color.Orange;
            this.btnNoteTextOrange.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextOrange.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextOrange.Image")));
            this.btnNoteTextOrange.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextOrange.SmallImage")));
            this.btnNoteTextOrange.Tag = "noteorange";
            this.btnNoteTextOrange.Text = "Orange";
            this.btnNoteTextOrange.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteTextCyan
            // 
            this.btnNoteTextCyan.Color = System.Drawing.Color.Cyan;
            this.btnNoteTextCyan.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteTextCyan.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteTextCyan.Image")));
            this.btnNoteTextCyan.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteTextCyan.SmallImage")));
            this.btnNoteTextCyan.Tag = "notecyan";
            this.btnNoteTextCyan.Text = "Cyan";
            this.btnNoteTextCyan.Click += new System.EventHandler(this.btnNoteTextColor_Click);
            // 
            // btnNoteBgColor
            // 
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgWhite);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgBlack);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgBlue);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgRed);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgYellow);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgGreen);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgGray);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgOrange);
            this.btnNoteBgColor.DropDownItems.Add(this.btnNoteBgCyan);
            this.btnNoteBgColor.Image = global::mDitaEditor.Properties.Resources.colorback;
            this.btnNoteBgColor.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgColor.SmallImage")));
            this.btnNoteBgColor.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.btnNoteBgColor.Tag = "";
            this.btnNoteBgColor.Text = "Background color";
            this.btnNoteBgColor.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgWhite
            // 
            this.btnNoteBgWhite.Color = System.Drawing.Color.White;
            this.btnNoteBgWhite.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgWhite.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgWhite.Image")));
            this.btnNoteBgWhite.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgWhite.SmallImage")));
            this.btnNoteBgWhite.Tag = "notewhite";
            this.btnNoteBgWhite.Text = "White";
            this.btnNoteBgWhite.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgBlack
            // 
            this.btnNoteBgBlack.Color = System.Drawing.Color.Black;
            this.btnNoteBgBlack.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgBlack.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgBlack.Image")));
            this.btnNoteBgBlack.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgBlack.SmallImage")));
            this.btnNoteBgBlack.Tag = "noteblack";
            this.btnNoteBgBlack.Text = "Black";
            this.btnNoteBgBlack.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgBlue
            // 
            this.btnNoteBgBlue.Color = System.Drawing.Color.Blue;
            this.btnNoteBgBlue.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgBlue.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgBlue.Image")));
            this.btnNoteBgBlue.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgBlue.SmallImage")));
            this.btnNoteBgBlue.Tag = "noteblue";
            this.btnNoteBgBlue.Text = "Blue";
            this.btnNoteBgBlue.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgRed
            // 
            this.btnNoteBgRed.Color = System.Drawing.Color.Red;
            this.btnNoteBgRed.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgRed.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgRed.Image")));
            this.btnNoteBgRed.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgRed.SmallImage")));
            this.btnNoteBgRed.Tag = "notered";
            this.btnNoteBgRed.Text = "Red";
            this.btnNoteBgRed.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgYellow
            // 
            this.btnNoteBgYellow.Color = System.Drawing.Color.Yellow;
            this.btnNoteBgYellow.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgYellow.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgYellow.Image")));
            this.btnNoteBgYellow.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgYellow.SmallImage")));
            this.btnNoteBgYellow.Tag = "noteyellow";
            this.btnNoteBgYellow.Text = "Yellow";
            this.btnNoteBgYellow.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgGreen
            // 
            this.btnNoteBgGreen.Color = System.Drawing.Color.Green;
            this.btnNoteBgGreen.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgGreen.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgGreen.Image")));
            this.btnNoteBgGreen.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgGreen.SmallImage")));
            this.btnNoteBgGreen.Tag = "notegreen";
            this.btnNoteBgGreen.Text = "Green";
            this.btnNoteBgGreen.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgGray
            // 
            this.btnNoteBgGray.Color = System.Drawing.Color.Gray;
            this.btnNoteBgGray.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgGray.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgGray.Image")));
            this.btnNoteBgGray.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgGray.SmallImage")));
            this.btnNoteBgGray.Tag = "notegray";
            this.btnNoteBgGray.Text = "Gray";
            this.btnNoteBgGray.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgOrange
            // 
            this.btnNoteBgOrange.Color = System.Drawing.Color.Orange;
            this.btnNoteBgOrange.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgOrange.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgOrange.Image")));
            this.btnNoteBgOrange.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgOrange.SmallImage")));
            this.btnNoteBgOrange.Tag = "noteorange";
            this.btnNoteBgOrange.Text = "Orange";
            this.btnNoteBgOrange.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // btnNoteBgCyan
            // 
            this.btnNoteBgCyan.Color = System.Drawing.Color.Cyan;
            this.btnNoteBgCyan.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnNoteBgCyan.Image = ((System.Drawing.Image)(resources.GetObject("btnNoteBgCyan.Image")));
            this.btnNoteBgCyan.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnNoteBgCyan.SmallImage")));
            this.btnNoteBgCyan.Tag = "notecyan";
            this.btnNoteBgCyan.Text = "Cyan";
            this.btnNoteBgCyan.Click += new System.EventHandler(this.btnNoteBgColor_Click);
            // 
            // panDitaImage
            // 
            this.panDitaImage.ButtonMoreEnabled = false;
            this.panDitaImage.ButtonMoreVisible = false;
            this.panDitaImage.Items.Add(this.btnDitaImageChange);
            this.panDitaImage.Items.Add(this.numDitaImageWidth);
            this.panDitaImage.Items.Add(this.numDitaImageHeight);
            this.panDitaImage.Items.Add(this.chbDitaImageShowDescription);
            this.panDitaImage.Text = "Image options";
            // 
            // btnDitaImageChange
            // 
            this.btnDitaImageChange.Image = global::mDitaEditor.Properties.Resources.insert_description;
            this.btnDitaImageChange.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnDitaImageChange.SmallImage")));
            this.btnDitaImageChange.Text = "Change image";
            this.btnDitaImageChange.Click += new System.EventHandler(this.btnDitaImageChange_Click);
            // 
            // numDitaImageWidth
            // 
            this.numDitaImageWidth.Checked = true;
            this.numDitaImageWidth.LabelWidth = 50;
            this.numDitaImageWidth.Text = "Width:";
            this.numDitaImageWidth.TextBoxText = "";
            this.numDitaImageWidth.TextBoxWidth = 50;
            this.numDitaImageWidth.Value = "";
            this.numDitaImageWidth.UpButtonClicked += new System.Windows.Forms.MouseEventHandler(this.numDitaImageWidth_UpButtonClicked);
            this.numDitaImageWidth.DownButtonClicked += new System.Windows.Forms.MouseEventHandler(this.numDitaImageWidth_DownButtonClicked);
            this.numDitaImageWidth.TextBoxTextChanged += new System.EventHandler(this.numDitaImageWidth_TextBoxTextChanged);
            this.numDitaImageWidth.TextBoxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numDitaImageSize_TextBoxKeyPress);
            this.numDitaImageWidth.TextBoxKeyDown += new System.Windows.Forms.KeyEventHandler(this.numDitaImageSize_TextBoxKeyDown);
            // 
            // numDitaImageHeight
            // 
            this.numDitaImageHeight.Checked = true;
            this.numDitaImageHeight.LabelWidth = 50;
            this.numDitaImageHeight.Text = "Height:";
            this.numDitaImageHeight.TextBoxText = "";
            this.numDitaImageHeight.TextBoxWidth = 50;
            this.numDitaImageHeight.UpButtonClicked += new System.Windows.Forms.MouseEventHandler(this.numDitaImageHeight_UpButtonClicked);
            this.numDitaImageHeight.DownButtonClicked += new System.Windows.Forms.MouseEventHandler(this.numDitaImageHeight_DownButtonClicked);
            this.numDitaImageHeight.TextBoxTextChanged += new System.EventHandler(this.numDitaImageHeight_TextBoxTextChanged);
            this.numDitaImageHeight.TextBoxKeyPress += new System.Windows.Forms.KeyPressEventHandler(this.numDitaImageSize_TextBoxKeyPress);
            this.numDitaImageHeight.TextBoxKeyDown += new System.Windows.Forms.KeyEventHandler(this.numDitaImageSize_TextBoxKeyDown);
            // 
            // chbDitaImageShowDescription
            // 
            this.chbDitaImageShowDescription.Text = "Show description";
            this.chbDitaImageShowDescription.CheckBoxCheckChanging += new System.ComponentModel.CancelEventHandler(this.chbDitaImageShowDescription_CheckBoxCheckChanging);
            // 
            // tabSearch
            // 
            this.tabSearch.Panels.Add(this.ribbonPanel1);
            this.tabSearch.Panels.Add(this.panelResults);
            this.tabSearch.Text = "DITA Repository";
            // 
            // ribbonPanel1
            // 
            this.ribbonPanel1.ButtonMoreEnabled = false;
            this.ribbonPanel1.ButtonMoreVisible = false;
            this.ribbonPanel1.Items.Add(this.btnAdvancedSearch);
            this.ribbonPanel1.Items.Add(this.txtSearch);
            this.ribbonPanel1.Items.Add(this.btnSearch);
            this.ribbonPanel1.Text = "Search";
            // 
            // btnAdvancedSearch
            // 
            this.btnAdvancedSearch.Image = global::mDitaEditor.Properties.Resources.searchbignewad;
            this.btnAdvancedSearch.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnAdvancedSearch.SmallImage")));
            this.btnAdvancedSearch.Text = "";
            this.btnAdvancedSearch.ToolTipTitle = "Advanced Search";
            this.btnAdvancedSearch.Click += new System.EventHandler(this.btnAdvancedSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Text = "Search:";
            this.txtSearch.TextBoxText = "";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::mDitaEditor.Properties.Resources.searchbignew;
            this.btnSearch.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.SmallImage")));
            this.btnSearch.Text = "";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panelResults
            // 
            this.panelResults.ButtonMoreEnabled = false;
            this.panelResults.ButtonMoreVisible = false;
            this.panelResults.Text = "Results";
            // 
            // tabGrafika
            // 
            this.tabGrafika.Panels.Add(this.panAdditionalActivities);
            this.tabGrafika.Panels.Add(this.panGraphicsObjects);
            this.tabGrafika.Panels.Add(this.panGraphicTools);
            this.tabGrafika.Panels.Add(this.panGrid);
            this.tabGrafika.Text = "LAMS Designer";
            // 
            // panAdditionalActivities
            // 
            this.panAdditionalActivities.ButtonMoreEnabled = false;
            this.panAdditionalActivities.ButtonMoreVisible = false;
            this.panAdditionalActivities.Items.Add(this.btnAdditionalActivitiesWindow);
            this.panAdditionalActivities.Items.Add(this.btnAddAditionalActivity);
            this.panAdditionalActivities.Items.Add(this.chbShowTransparentObjects);
            this.panAdditionalActivities.Items.Add(this.chbShowTransparentTools);
            this.panAdditionalActivities.Text = "Additional Activities";
            // 
            // btnAdditionalActivitiesWindow
            // 
            this.btnAdditionalActivitiesWindow.Image = global::mDitaEditor.Properties.Resources.forum;
            this.btnAdditionalActivitiesWindow.MaximumSize = new System.Drawing.Size(72, 0);
            this.btnAdditionalActivitiesWindow.MinimumSize = new System.Drawing.Size(64, 0);
            this.btnAdditionalActivitiesWindow.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnAdditionalActivitiesWindow.SmallImage")));
            this.btnAdditionalActivitiesWindow.Text = "Additional Activities";
            this.btnAdditionalActivitiesWindow.Click += new System.EventHandler(this.btnAdditionalActivitiesWindow_Click);
            // 
            // btnAddAditionalActivity
            // 
            this.btnAddAditionalActivity.DrawIconsBar = false;
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnAssessment);
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnChat);
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnForum);
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnMultipleChoice);
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnQuestionAndAnswer);
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnShareResources);
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnSubmitFiles);
            this.btnAddAditionalActivity.DropDownItems.Add(this.btnJavagrader);
            this.btnAddAditionalActivity.Enabled = false;
            this.btnAddAditionalActivity.Image = global::mDitaEditor.Properties.Resources.slide;
            this.btnAddAditionalActivity.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnAddAditionalActivity.MaxSizeMode = System.Windows.Forms.RibbonElementSizeMode.DropDown;
            this.btnAddAditionalActivity.MinimumSize = new System.Drawing.Size(64, 0);
            this.btnAddAditionalActivity.SmallImage = global::mDitaEditor.Properties.Resources.slide;
            this.btnAddAditionalActivity.Style = System.Windows.Forms.RibbonButtonStyle.DropDown;
            this.btnAddAditionalActivity.Text = "Insert";
            // 
            // btnAssessment
            // 
            this.btnAssessment.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnAssessment.Image = global::mDitaEditor.Properties.Resources.lms_assesment24;
            this.btnAssessment.SmallImage = global::mDitaEditor.Properties.Resources.lms_assesment24;
            this.btnAssessment.Text = "Assessment";
            this.btnAssessment.Click += new System.EventHandler(this.btnAssessment_Click);
            // 
            // btnChat
            // 
            this.btnChat.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnChat.Image = global::mDitaEditor.Properties.Resources.lms_chat;
            this.btnChat.SmallImage = global::mDitaEditor.Properties.Resources.lms_chat24;
            this.btnChat.Text = "Chat";
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // btnForum
            // 
            this.btnForum.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnForum.Image = global::mDitaEditor.Properties.Resources.lms_forum;
            this.btnForum.SmallImage = global::mDitaEditor.Properties.Resources.lms_forum24;
            this.btnForum.Text = "Forum";
            this.btnForum.Click += new System.EventHandler(this.btnForum_Click);
            // 
            // btnMultipleChoice
            // 
            this.btnMultipleChoice.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnMultipleChoice.Image = global::mDitaEditor.Properties.Resources.lms_multiple_choice;
            this.btnMultipleChoice.SmallImage = global::mDitaEditor.Properties.Resources.lms_multiple_choice24;
            this.btnMultipleChoice.Text = "Multiple Choice";
            this.btnMultipleChoice.Click += new System.EventHandler(this.btnMultipleChoice_Click);
            // 
            // btnQuestionAndAnswer
            // 
            this.btnQuestionAndAnswer.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnQuestionAndAnswer.Image = global::mDitaEditor.Properties.Resources.lms_qa;
            this.btnQuestionAndAnswer.SmallImage = global::mDitaEditor.Properties.Resources.lms_qa24;
            this.btnQuestionAndAnswer.Text = "Question & Answer";
            this.btnQuestionAndAnswer.Click += new System.EventHandler(this.btnQuestionAndAnswer_Click);
            // 
            // btnShareResources
            // 
            this.btnShareResources.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnShareResources.Image = global::mDitaEditor.Properties.Resources.lms_share_resources;
            this.btnShareResources.SmallImage = global::mDitaEditor.Properties.Resources.lms_share_resources24;
            this.btnShareResources.Text = "Share Resources";
            this.btnShareResources.Click += new System.EventHandler(this.btnShareResources_Click);
            // 
            // btnSubmitFiles
            // 
            this.btnSubmitFiles.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSubmitFiles.Image = global::mDitaEditor.Properties.Resources.lms_submit_files;
            this.btnSubmitFiles.SmallImage = global::mDitaEditor.Properties.Resources.lms_submit_files24;
            this.btnSubmitFiles.Text = "Submit Files";
            this.btnSubmitFiles.Click += new System.EventHandler(this.btnSubmitFiles_Click);
            // 
            // btnJavagrader
            // 
            this.btnJavagrader.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnJavagrader.Image = global::mDitaEditor.Properties.Resources.java;
            this.btnJavagrader.SmallImage = global::mDitaEditor.Properties.Resources.java24;
            this.btnJavagrader.Text = "JavaGrader";
            this.btnJavagrader.Click += new System.EventHandler(this.btnJavagrader_Click);
            // 
            // chbShowTransparentObjects
            // 
            this.chbShowTransparentObjects.Checked = true;
            this.chbShowTransparentObjects.Text = "Show added objects";
            this.chbShowTransparentObjects.ToolTip = "Prikaži objekte koji su već dodati na platno u listi slajdova.";
            this.chbShowTransparentObjects.CheckBoxCheckChanged += new System.EventHandler(this.chbShowTransparentObjects_CheckBoxCheckChanged);
            this.chbShowTransparentObjects.Click += new System.EventHandler(this.chbShowTransparentObjects_Click);
            // 
            // chbShowTransparentTools
            // 
            this.chbShowTransparentTools.Checked = true;
            this.chbShowTransparentTools.Text = "Show added activities";
            this.chbShowTransparentTools.ToolTip = "Prikaži dodatne aktivnosti koje su dodate na platno u listi slajdova.";
            this.chbShowTransparentTools.CheckBoxCheckChanged += new System.EventHandler(this.chbShowTransparentTools_CheckBoxCheckChanged);
            this.chbShowTransparentTools.Click += new System.EventHandler(this.chbShowTransparentTools_Click);
            // 
            // panGraphicsObjects
            // 
            this.panGraphicsObjects.ButtonMoreEnabled = false;
            this.panGraphicsObjects.ButtonMoreVisible = false;
            this.panGraphicsObjects.Items.Add(this.btnGate);
            this.panGraphicsObjects.Items.Add(this.btnBranch);
            this.panGraphicsObjects.Items.Add(this.btnOptional);
            this.panGraphicsObjects.Text = "Tools";
            // 
            // btnGate
            // 
            this.btnGate.Image = global::mDitaEditor.Properties.Resources.stop_sign24;
            this.btnGate.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnGate.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnGate.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnGate.SmallImage")));
            this.btnGate.Text = "Gate";
            this.btnGate.Click += new System.EventHandler(this.btnGate_Click);
            this.btnGate.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnGate_MouseUp);
            this.btnGate.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnGate_MouseMove);
            this.btnGate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnGate_MouseDown);
            this.btnGate.MouseEnter += new System.Windows.Forms.MouseEventHandler(this.btnGate_MouseEnter);
            // 
            // btnBranch
            // 
            this.btnBranch.Image = global::mDitaEditor.Properties.Resources.branch24;
            this.btnBranch.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnBranch.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnBranch.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnBranch.SmallImage")));
            this.btnBranch.Text = "Branch";
            this.btnBranch.Click += new System.EventHandler(this.btnBranch_Click);
            this.btnBranch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnBranch_MouseUp);
            this.btnBranch.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnBranch_MouseMove);
            this.btnBranch.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnBranch_MouseDown);
            this.btnBranch.MouseEnter += new System.Windows.Forms.MouseEventHandler(this.btnBranch_MouseEnter);
            // 
            // btnOptional
            // 
            this.btnOptional.Image = global::mDitaEditor.Properties.Resources.additional_activity24;
            this.btnOptional.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnOptional.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnOptional.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnOptional.SmallImage")));
            this.btnOptional.Text = "Optional Activity";
            this.btnOptional.Click += new System.EventHandler(this.btnOptional_Click);
            this.btnOptional.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnOptional_MouseUp);
            this.btnOptional.MouseMove += new System.Windows.Forms.MouseEventHandler(this.btnOptional_MouseMove);
            this.btnOptional.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnOptional_MouseDown);
            this.btnOptional.MouseEnter += new System.Windows.Forms.MouseEventHandler(this.btnOptional_MouseEnter);
            // 
            // panGraphicTools
            // 
            this.panGraphicTools.ButtonMoreEnabled = false;
            this.panGraphicTools.ButtonMoreVisible = false;
            this.panGraphicTools.Items.Add(this.btnGraphicsMove);
            this.panGraphicTools.Items.Add(this.btnGraphicsConnect);
            this.panGraphicTools.Text = "Mouse";
            // 
            // btnGraphicsMove
            // 
            this.btnGraphicsMove.Checked = true;
            this.btnGraphicsMove.CheckedGroup = "grafika_mouse";
            this.btnGraphicsMove.CheckOnClick = true;
            this.btnGraphicsMove.Image = global::mDitaEditor.Properties.Resources.hand_open;
            this.btnGraphicsMove.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnGraphicsMove.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnGraphicsMove.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnGraphicsMove.SmallImage")));
            this.btnGraphicsMove.Text = "Move";
            this.btnGraphicsMove.Click += new System.EventHandler(this.btnGraphicsMove_Click);
            // 
            // btnGraphicsConnect
            // 
            this.btnGraphicsConnect.CheckedGroup = "grafika_mouse";
            this.btnGraphicsConnect.CheckOnClick = true;
            this.btnGraphicsConnect.Image = global::mDitaEditor.Properties.Resources.cursor_crosshair;
            this.btnGraphicsConnect.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnGraphicsConnect.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnGraphicsConnect.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnGraphicsConnect.SmallImage")));
            this.btnGraphicsConnect.Text = "Connect";
            this.btnGraphicsConnect.Click += new System.EventHandler(this.btnGraphicsConnect_Click);
            // 
            // panGrid
            // 
            this.panGrid.ButtonMoreEnabled = false;
            this.panGrid.ButtonMoreVisible = false;
            this.panGrid.Items.Add(this.btnGraphicsAutoArrange);
            this.panGrid.Items.Add(this.btnClearCanvas);
            this.panGrid.Items.Add(this.btnCenterCanvas);
            this.panGrid.Items.Add(this.chbShowGrid);
            this.panGrid.Items.Add(this.chbSnapToGrid);
            this.panGrid.Text = "Canvas";
            // 
            // btnGraphicsAutoArrange
            // 
            this.btnGraphicsAutoArrange.DrawIconsBar = false;
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.btnSortInColumns);
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.btnSortInRows);
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.btnSortRectangle);
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.btnSortCircle);
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.btnSortByObject);
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.btnSortSnake);
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.btnSortMaze);
            this.btnGraphicsAutoArrange.DropDownItems.Add(this.chbAutoArrange);
            this.btnGraphicsAutoArrange.Image = global::mDitaEditor.Properties.Resources.add;
            this.btnGraphicsAutoArrange.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnGraphicsAutoArrange.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnGraphicsAutoArrange.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnGraphicsAutoArrange.SmallImage")));
            this.btnGraphicsAutoArrange.Style = System.Windows.Forms.RibbonButtonStyle.SplitDropDown;
            this.btnGraphicsAutoArrange.Text = "Auto arrange";
            this.btnGraphicsAutoArrange.Click += new System.EventHandler(this.btnGraphicsAutoArrange_Click);
            // 
            // btnSortInColumns
            // 
            this.btnSortInColumns.Checked = true;
            this.btnSortInColumns.CheckedGroup = "arranging";
            this.btnSortInColumns.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSortInColumns.Image = ((System.Drawing.Image)(resources.GetObject("btnSortInColumns.Image")));
            this.btnSortInColumns.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSortInColumns.SmallImage")));
            this.btnSortInColumns.Text = "Sort in columns";
            this.btnSortInColumns.Click += new System.EventHandler(this.btnSortInColumns_Click);
            // 
            // btnSortInRows
            // 
            this.btnSortInRows.CheckedGroup = "arranging";
            this.btnSortInRows.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSortInRows.Image = ((System.Drawing.Image)(resources.GetObject("btnSortInRows.Image")));
            this.btnSortInRows.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSortInRows.SmallImage")));
            this.btnSortInRows.Text = "Sort in rows";
            this.btnSortInRows.Click += new System.EventHandler(this.btnSortInRows_Click);
            // 
            // btnSortRectangle
            // 
            this.btnSortRectangle.CheckedGroup = "arranging";
            this.btnSortRectangle.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSortRectangle.Image = ((System.Drawing.Image)(resources.GetObject("btnSortRectangle.Image")));
            this.btnSortRectangle.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSortRectangle.SmallImage")));
            this.btnSortRectangle.Text = "Sort in rectangle";
            this.btnSortRectangle.Click += new System.EventHandler(this.btnSortRectangle_Click);
            // 
            // btnSortCircle
            // 
            this.btnSortCircle.CheckedGroup = "arranging";
            this.btnSortCircle.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSortCircle.Image = ((System.Drawing.Image)(resources.GetObject("btnSortCircle.Image")));
            this.btnSortCircle.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSortCircle.SmallImage")));
            this.btnSortCircle.Text = "Sort in circle";
            this.btnSortCircle.Click += new System.EventHandler(this.btnSortCircle_Click);
            // 
            // btnSortByObject
            // 
            this.btnSortByObject.CheckedGroup = "arranging";
            this.btnSortByObject.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSortByObject.Image = ((System.Drawing.Image)(resources.GetObject("btnSortByObject.Image")));
            this.btnSortByObject.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSortByObject.SmallImage")));
            this.btnSortByObject.Text = "Sort by object";
            this.btnSortByObject.Click += new System.EventHandler(this.btnSortByObject_Click);
            // 
            // btnSortSnake
            // 
            this.btnSortSnake.CheckedGroup = "arranging";
            this.btnSortSnake.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSortSnake.Image = ((System.Drawing.Image)(resources.GetObject("btnSortSnake.Image")));
            this.btnSortSnake.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSortSnake.SmallImage")));
            this.btnSortSnake.Text = "Snake sort";
            this.btnSortSnake.Click += new System.EventHandler(this.btnSortSnake_Click);
            // 
            // btnSortMaze
            // 
            this.btnSortMaze.CheckedGroup = "arranging";
            this.btnSortMaze.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnSortMaze.Image = ((System.Drawing.Image)(resources.GetObject("btnSortMaze.Image")));
            this.btnSortMaze.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnSortMaze.SmallImage")));
            this.btnSortMaze.Text = "Maze sort";
            this.btnSortMaze.Click += new System.EventHandler(this.btnSortMaze_Click);
            // 
            // chbAutoArrange
            // 
            this.chbAutoArrange.LabelWidth = 150;
            this.chbAutoArrange.Text = "Always auto arrange";
            this.chbAutoArrange.CheckBoxCheckChanged += new System.EventHandler(this.chbAutoArrange_CheckBoxCheckChanged);
            this.chbAutoArrange.CheckBoxCheckChanging += new System.ComponentModel.CancelEventHandler(this.chbAutoArrange_CheckBoxCheckChanging);
            this.chbAutoArrange.Click += new System.EventHandler(this.chbAutoArrange_Click);
            // 
            // btnClearCanvas
            // 
            this.btnClearCanvas.Image = global::mDitaEditor.Properties.Resources.delete;
            this.btnClearCanvas.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnClearCanvas.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnClearCanvas.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnClearCanvas.SmallImage")));
            this.btnClearCanvas.Text = "Clear";
            this.btnClearCanvas.Click += new System.EventHandler(this.btnClearCanvas_Click);
            // 
            // btnCenterCanvas
            // 
            this.btnCenterCanvas.Image = global::mDitaEditor.Properties.Resources.move;
            this.btnCenterCanvas.MaximumSize = new System.Drawing.Size(64, 0);
            this.btnCenterCanvas.MinimumSize = new System.Drawing.Size(48, 0);
            this.btnCenterCanvas.SmallImage = ((System.Drawing.Image)(resources.GetObject("btnCenterCanvas.SmallImage")));
            this.btnCenterCanvas.Text = "Center";
            this.btnCenterCanvas.Click += new System.EventHandler(this.btnCenterCanvas_Click);
            // 
            // chbShowGrid
            // 
            this.chbShowGrid.Checked = true;
            this.chbShowGrid.LabelWidth = 274;
            this.chbShowGrid.Text = "Show grid";
            this.chbShowGrid.CheckBoxCheckChanged += new System.EventHandler(this.chbShowGrid_CheckBoxCheckChanged);
            this.chbShowGrid.Click += new System.EventHandler(this.chbShowGrid_Click);
            // 
            // chbSnapToGrid
            // 
            this.chbSnapToGrid.Checked = true;
            this.chbSnapToGrid.LabelWidth = 274;
            this.chbSnapToGrid.Text = "Snap to grid";
            this.chbSnapToGrid.CheckBoxCheckChanged += new System.EventHandler(this.chbSnapToGrid_CheckBoxCheckChanged);
            this.chbSnapToGrid.Click += new System.EventHandler(this.chbSnapToGrid_Click);
            // 
            // panelControler
            // 
            this.panelControler.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelControler.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelControler.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelControler.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelControler.Controls.Add(this.contentControl);
            this.panelControler.Controls.Add(this.sectionControl);
            this.panelControler.Location = new System.Drawing.Point(1,1);
            this.panelControler.Margin = new System.Windows.Forms.Padding(5);
            this.panelControler.Name = "panelControler";
            this.panelControler.Size = new System.Drawing.Size(846, 563);
            this.panelControler.TabIndex = 4;
            // 
            // contentControl
            // 
            this.contentControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.contentControl.Content = null;
            this.contentControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentControl.Location = new System.Drawing.Point(0, 0);
            this.contentControl.Margin = new System.Windows.Forms.Padding(2);
            this.contentControl.Name = "contentControl";
            this.contentControl.Size = new System.Drawing.Size(844, 561);
            this.contentControl.TabIndex = 0;
            this.contentControl.Visible = false;
            // 
            // sectionControl
            // 
            this.sectionControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sectionControl.Location = new System.Drawing.Point(0, 0);
            this.sectionControl.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.sectionControl.Name = "sectionControl";
            this.sectionControl.Section = null;
            this.sectionControl.SectionGoal = "";
            this.sectionControl.SectionTitle = "";
            this.sectionControl.Size = new System.Drawing.Size(844, 561);
            this.sectionControl.TabIndex = 1;
            this.sectionControl.Visible = false;
            // 
            // ribbonUpDown1
            // 
            this.ribbonUpDown1.TextBoxText = "";
            this.ribbonUpDown1.TextBoxWidth = 50;
            // 
            // ribbonUpDown2
            // 
            this.ribbonUpDown2.TextBoxText = "";
            this.ribbonUpDown2.TextBoxWidth = 50;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vScrollBar.Enabled = false;
            this.vScrollBar.Location = new System.Drawing.Point(181, 116);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 565);
            this.vScrollBar.TabIndex = 7;
            // 
            // numGridSize
            // 
            this.numGridSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.numGridSize.Location = new System.Drawing.Point(919, 69);
            this.numGridSize.Maximum = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.numGridSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numGridSize.Name = "numGridSize";
            this.numGridSize.Size = new System.Drawing.Size(38, 20);
            this.numGridSize.TabIndex = 11;
            this.numGridSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.numGridSize.Visible = false;
            this.numGridSize.ValueChanged += new System.EventHandler(this.numGridSize_ValueChanged);
            // 
            // labGridSize
            // 
            this.labGridSize.AutoSize = true;
            this.labGridSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labGridSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labGridSize.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labGridSize.Location = new System.Drawing.Point(866, 70);
            this.labGridSize.Name = "labGridSize";
            this.labGridSize.Size = new System.Drawing.Size(54, 15);
            this.labGridSize.TabIndex = 10;
            this.labGridSize.Text = "Grid size:";
            this.labGridSize.Visible = false;
            // 
            // trbGridSize
            // 
            this.trbGridSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trbGridSize.Location = new System.Drawing.Point(863, 35);
            this.trbGridSize.Maximum = 80;
            this.trbGridSize.Minimum = 10;
            this.trbGridSize.Name = "trbGridSize";
            this.trbGridSize.Size = new System.Drawing.Size(99, 45);
            this.trbGridSize.TabIndex = 9;
            this.trbGridSize.TabStop = false;
            this.trbGridSize.Value = 20;
            this.trbGridSize.Visible = false;
            this.trbGridSize.ValueChanged += new System.EventHandler(this.trbGridSize_ValueChanged);
            // 
            // numZoom
            // 
            this.numZoom.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.numZoom.Location = new System.Drawing.Point(1027, 69);
            this.numZoom.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numZoom.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numZoom.Name = "numZoom";
            this.numZoom.Size = new System.Drawing.Size(38, 20);
            this.numZoom.TabIndex = 15;
            this.numZoom.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numZoom.Visible = false;
            this.numZoom.ValueChanged += new System.EventHandler(this.numZoom_ValueChanged);
            // 
            // labelZoom
            // 
            this.labelZoom.AutoSize = true;
            this.labelZoom.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelZoom.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelZoom.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelZoom.Location = new System.Drawing.Point(969, 70);
            this.labelZoom.Name = "labelZoom";
            this.labelZoom.Size = new System.Drawing.Size(60, 15);
            this.labelZoom.TabIndex = 14;
            this.labelZoom.Text = "Zoom(%):";
            this.labelZoom.Visible = false;
            // 
            // trbZoom
            // 
            this.trbZoom.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.trbZoom.LargeChange = 10;
            this.trbZoom.Location = new System.Drawing.Point(968, 35);
            this.trbZoom.Maximum = 200;
            this.trbZoom.Minimum = 50;
            this.trbZoom.Name = "trbZoom";
            this.trbZoom.Size = new System.Drawing.Size(99, 45);
            this.trbZoom.SmallChange = 5;
            this.trbZoom.TabIndex = 13;
            this.trbZoom.TabStop = false;
            this.trbZoom.Value = 100;
            this.trbZoom.Visible = false;
            this.trbZoom.Scroll += new System.EventHandler(this.trbZoom_Scroll);
            // 
            // tabGreskeIWord
            // 
            this.tabGreskeIWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabGreskeIWord.Controls.Add(this.tabGreske);
            this.tabGreskeIWord.Controls.Add(this.tabWord);
            this.tabGreskeIWord.Controls.Add(this.tabStatistics);
            this.tabGreskeIWord.Location = new System.Drawing.Point(1055, 116);
            this.tabGreskeIWord.Name = "tabGreskeIWord";
            this.tabGreskeIWord.SelectedIndex = 0;
            this.tabGreskeIWord.Size = new System.Drawing.Size(208, 565);
            this.tabGreskeIWord.TabIndex = 16;
            this.tabGreskeIWord.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabGreskeIWord_KeyDown);
            // 
            // tabGreske
            // 
            this.tabGreske.Controls.Add(this.errorListPanel);
            this.tabGreske.Location = new System.Drawing.Point(4, 22);
            this.tabGreske.Name = "tabGreske";
            this.tabGreske.Padding = new System.Windows.Forms.Padding(3);
            this.tabGreske.Size = new System.Drawing.Size(200, 539);
            this.tabGreske.TabIndex = 0;
            this.tabGreske.Text = "Greške";
            this.tabGreske.UseVisualStyleBackColor = true;
            // 
            // errorListPanel
            // 
            this.errorListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorListPanel.BackColor = System.Drawing.SystemColors.Control;
            this.errorListPanel.Errors = null;
            this.errorListPanel.Location = new System.Drawing.Point(-1, 0);
            this.errorListPanel.Margin = new System.Windows.Forms.Padding(2);
            this.errorListPanel.Name = "errorListPanel";
            this.errorListPanel.Size = new System.Drawing.Size(205, 539);
            this.errorListPanel.TabIndex = 6;
            // 
            // tabWord
            // 
            this.tabWord.Controls.Add(this.wordImport1);
            this.tabWord.Location = new System.Drawing.Point(4, 22);
            this.tabWord.Name = "tabWord";
            this.tabWord.Padding = new System.Windows.Forms.Padding(3);
            this.tabWord.Size = new System.Drawing.Size(200, 539);
            this.tabWord.TabIndex = 1;
            this.tabWord.Text = "Word import";
            this.tabWord.UseVisualStyleBackColor = true;
            // 
            // wordImport1
            // 
            this.wordImport1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wordImport1.Location = new System.Drawing.Point(-4, 0);
            this.wordImport1.Name = "wordImport1";
            this.wordImport1.Size = new System.Drawing.Size(209, 543);
            this.wordImport1.TabIndex = 0;
            // 
            // tabStatistics
            // 
            this.tabStatistics.Controls.Add(this.statisticsControl);
            this.tabStatistics.Location = new System.Drawing.Point(4, 22);
            this.tabStatistics.Name = "tabStatistics";
            this.tabStatistics.Size = new System.Drawing.Size(200, 539);
            this.tabStatistics.TabIndex = 2;
            this.tabStatistics.Text = "Statistics";
            this.tabStatistics.UseVisualStyleBackColor = true;
            // 
            // statisticsControl
            // 
            this.statisticsControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statisticsControl.Location = new System.Drawing.Point(4, 0);
            this.statisticsControl.Name = "statisticsControl";
            this.statisticsControl.Size = new System.Drawing.Size(196, 539);
            this.statisticsControl.Statistics = statistics1;
            this.statisticsControl.TabIndex = 0;
            // 
            // btnInsertCodePasting
            // 
            this.btnInsertCodePasting.DropDownArrowDirection = System.Windows.Forms.RibbonArrowDirection.Left;
            this.btnInsertCodePasting.Image = global::mDitaEditor.Properties.Resources.insert_code_snippet;
            this.btnInsertCodePasting.SmallImage = global::mDitaEditor.Properties.Resources.insert_code_snippet;
            this.btnInsertCodePasting.Text = "Insert snippet by pasting";
            this.btnInsertCodePasting.Click += new System.EventHandler(this.btnInsertCodePasting_Click);
            // 
            // slideList
            // 
            this.slideList.AllowDrop = true;
            this.slideList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.slideList.AutoScroll = true;
            this.slideList.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.slideList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.slideList.Location = new System.Drawing.Point(0, 116);
            this.slideList.Margin = new System.Windows.Forms.Padding(2);
            this.slideList.Name = "slideList";
            this.slideList.OpenSlideIndex = -1;
            this.slideList.SelectedSlide = null;
            this.slideList.Size = new System.Drawing.Size(198, 565);
            this.slideList.TabIndex = 5;
            this.slideList.WrapContents = false;
            // 
            // grafikaPanel
            // 
            this.grafikaPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grafikaPanel.AutoArrange = false;
            this.grafikaPanel.CanvasSortType = mDitaEditor.Lams.Editor.GrafikaCanvas.SortType.Columns;
            this.grafikaPanel.Location = new System.Drawing.Point(0, 116);
            this.grafikaPanel.Name = "grafikaPanel";
            this.grafikaPanel.Size = new System.Drawing.Size(1057, 565);
            this.grafikaPanel.TabIndex = 12;
            this.grafikaPanel.Visible = false;
            // 
            // transparentPanel
            // 
            this.transparentPanel.Location = new System.Drawing.Point(0, 0);
            this.transparentPanel.Name = "transparentPanel";
            this.transparentPanel.Size = new System.Drawing.Size(59, 28);
            this.transparentPanel.TabIndex = 17;
            this.transparentPanel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.tabGreskeIWord);
            this.Controls.Add(this.ribbonMenu);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.panelControler);
            this.Controls.Add(this.slideList);
            this.Controls.Add(this.grafikaPanel);
            this.Controls.Add(this.numZoom);
            this.Controls.Add(this.labelZoom);
            this.Controls.Add(this.numGridSize);
            this.Controls.Add(this.labGridSize);
            this.Controls.Add(this.trbZoom);
            this.Controls.Add(this.trbGridSize);
            this.Controls.Add(this.transparentPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mDita Editor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Click += new System.EventHandler(this.Form1_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panelControler.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbGridSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numZoom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbZoom)).EndInit();
            this.tabGreskeIWord.ResumeLayout(false);
            this.tabGreske.ResumeLayout(false);
            this.tabWord.ResumeLayout(false);
            this.tabStatistics.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion
        private System.Windows.Forms.RibbonButton btnInsertTextBox;
        private System.Windows.Forms.RibbonButton btnInsertNote;
        private System.Windows.Forms.RibbonButton btnInsertFigure;
        public System.Windows.Forms.Panel panelControler;
        private System.Windows.Forms.RibbonItemGroup btnGroup1;
        private System.Windows.Forms.RibbonButton btnBold;
        private System.Windows.Forms.RibbonButton btnItalic;
        private System.Windows.Forms.RibbonButton btnUnderline;
        private System.Windows.Forms.RibbonButton btnSuperscript;
        private System.Windows.Forms.RibbonButton btnSubscript;
        private System.Windows.Forms.RibbonButton btnLink;
        private System.Windows.Forms.RibbonButton btnBulletList;
        private System.Windows.Forms.RibbonButton btnNumberedList;
        private System.Windows.Forms.RibbonItemGroup btnGroupTwo;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator1;
        private System.Windows.Forms.RibbonSeparator ribbonSeparator2;
        private System.Windows.Forms.RibbonButton btnKeyword;
        private System.Windows.Forms.RibbonButton btnTerm;
        private System.Windows.Forms.RibbonButton btnPhrase;
        private System.Windows.Forms.RibbonButton btnHighlight;
        private System.Windows.Forms.RibbonUpDown ribbonUpDown1;
        private System.Windows.Forms.RibbonButton btnOther;
        private System.Windows.Forms.RibbonButton btnForeignWord;
        private System.Windows.Forms.RibbonButton btnReservedWord;
        private System.Windows.Forms.RibbonButton btnClear;
        private System.Windows.Forms.RibbonButton btnLayout;
        private System.Windows.Forms.RibbonButton btnLC_1;
        private System.Windows.Forms.RibbonButton btnLC_2;
        private System.Windows.Forms.RibbonButton btnLC_3;
        private System.Windows.Forms.RibbonButton btnLC_1_2;
        private System.Windows.Forms.RibbonButton btnLC_2_1;
        private System.Windows.Forms.RibbonPanel panDitaSlide;
        private System.Windows.Forms.RibbonButton btnInsertSnippet;
        private System.Windows.Forms.RibbonButton btnInsertLatex;
        private System.Windows.Forms.RibbonButton btnInsertSlide;
        private System.Windows.Forms.RibbonButton btnObjectSlide;
        private System.Windows.Forms.RibbonButton btnSubObjectSlide;
        private System.Windows.Forms.RibbonOrbMenuItem btnOpen;
        private System.Windows.Forms.RibbonOrbMenuItem btnImport;
        private RibbonOrbMenuItem btnNewProject;
        private RibbonOrbMenuItem btnImportOneObject;
        private RibbonButton btnInsertYoutube;
        private RibbonButton btnInsertVideo;
        private RibbonButton btnInsertSection;
        private RibbonButton btnInsertSection1;
        private RibbonButton btnInsertSection2;
        private RibbonButton btnInsertSection3;
        private RibbonButton btnInsertSection12;
        private RibbonButton btnInsertSection21;
        private RibbonTab tabSearch;
        private RibbonPanel ribbonPanel1;
        private RibbonTextBox txtSearch;
        private RibbonPanel panelResults;
        private RibbonButton btnSearch;
        public RibbonOrbMenuItem btnSave;
        private RibbonUpDown ribbonUpDown2;
        private RibbonOrbMenuItem btnEditProject;
        public RibbonButton rbUndo;
        public RibbonButton rbRedo;
        private RibbonButton btnInsertAudio;
        private RibbonButton btnInsertSectionGallery;
        private RibbonButton btnQA;
        public LearningContentControl contentControl;
        public LearningSectionControl sectionControl;
        private ErrorListPanel errorListPanel;
        private VScrollBar vScrollBar;
        private RibbonButton btnAdvancedSearch;
        private RibbonPanel panGraphicTools;
        private RibbonButton btnGraphicsAutoArrange;
        private RibbonPanel panGrid;
        private TrackBar trbGridSize;
        private NumericUpDown numGridSize;
        private Label labGridSize;
        public RibbonTab tabDita;
        private RibbonPanel panGraphicsObjects;
        private RibbonButton btnGate;
        private RibbonButton btnBranch;
        public RibbonButton btnGraphicsMove;
        public RibbonButton btnGraphicsConnect;
        private RibbonButton btnAdditionalActivitiesWindow;
        private RibbonButton btnAssessment;
        private RibbonButton btnChat;
        private RibbonButton btnForum;
        private RibbonButton btnMultipleChoice;
        private RibbonButton btnQuestionAndAnswer;
        private RibbonButton btnShareResources;
        private RibbonButton btnSubmitFiles;
        public RibbonButton btnAddAditionalActivity;
        private RibbonPanel panAdditionalActivities;
        private RibbonButton btnClearCanvas;
        public GrafikaPanel grafikaPanel;
        private RibbonCheckBox chbShowTransparentObjects;
        private RibbonCheckBox chbShowTransparentTools;
        private Label labelZoom;
        private TrackBar trbZoom;
        public NumericUpDown numZoom;
        public Ribbon ribbonMenu;
        private RibbonCheckBox chbAutoArrange;
        private RibbonButton btnSortInColumns;
        private RibbonButton btnSortInRows;
        private RibbonButton btnSortByObject;
        private RibbonButton btnSortSnake;
        private RibbonButton btnSortCircle;
        private RibbonButton btnSortRectangle;
        private RibbonButton btnSortMaze;
        public SlideListControl slideList;
        private RibbonButton btnCenterCanvas;
        private RibbonOrbMenuItem btnPreviewHTML;
        private RibbonButton btnOptional;
        private RibbonButton btnJavagrader;
        private RibbonOrbMenuItem btnUpdate;
        private TabPage tabGreske;
        private TabPage tabWord;
        public TabControl tabGreskeIWord;
        private WordImportPanel wordImport1;
        private RibbonSeparator ribbonSeparator3;
        private RibbonOrbMenuItem btnMergeProject;
        private TabPage tabStatistics;
        private StatisticsControl statisticsControl;
        private RibbonOrbMenuItem btnAbout;
        public RibbonCheckBox chbShowGrid;
        public RibbonCheckBox chbSnapToGrid;
        public RibbonTab tabGrafika;
        private RibbonButton btnInsertCodePasting;
        private RibbonPanel panDitaGeneral;
        public RibbonPanel panDitaWords;
        public RibbonPanel panDitaInsert;
        private RibbonPanel panDitaNote;
        private RibbonButton btnNoteTextColor;
        private RibbonButton btnNoteBgColor;
        private RibbonColorChooser btnNoteTextWhite;
        private RibbonColorChooser btnNoteTextBlack;
        private RibbonColorChooser btnNoteTextBlue;
        private RibbonColorChooser btnNoteTextRed;
        private RibbonColorChooser btnNoteTextYellow;
        private RibbonColorChooser btnNoteTextGreen;
        private RibbonColorChooser btnNoteTextGray;
        private RibbonColorChooser btnNoteTextOrange;
        private RibbonColorChooser btnNoteTextCyan;
        private RibbonColorChooser btnNoteBgWhite;
        private RibbonColorChooser btnNoteBgBlack;
        private RibbonColorChooser btnNoteBgBlue;
        private RibbonColorChooser btnNoteBgRed;
        private RibbonColorChooser btnNoteBgYellow;
        private RibbonColorChooser btnNoteBgGreen;
        private RibbonColorChooser btnNoteBgGray;
        private RibbonColorChooser btnNoteBgOrange;
        private RibbonColorChooser btnNoteBgCyan;
        private RibbonPanel panDitaImage;
        private RibbonUpDown numDitaImageWidth;
        private RibbonUpDown numDitaImageHeight;
        private RibbonButton btnDitaImageChange;
        private RibbonCheckBox chbDitaImageShowDescription;
        private TransparentPanel transparentPanel;
        private RibbonOrbMenuItem btnExport;
        private RibbonOrbMenuItem btnExportBranching;
        private RibbonButton btnMultilevel;
        private RibbonButton btnMultilevelBullet;
    }
}

