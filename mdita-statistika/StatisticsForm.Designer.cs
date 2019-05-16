namespace StatistikaProjekata
{
    partial class StatisticsForm
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
            StatistikaProjekata.ProjectFile.Statistics statistics1 = new StatistikaProjekata.ProjectFile.Statistics();
            StatistikaProjekata.ProjectFile.Statistics statistics2 = new StatistikaProjekata.ProjectFile.Statistics();
            StatistikaProjekata.ProjectFile.Statistics statistics3 = new StatistikaProjekata.ProjectFile.Statistics();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StatisticsForm));
            this.lvProjects = new System.Windows.Forms.ListBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statProject = new StatistikaProjekata.StatisticsControl();
            this.statTotal = new StatistikaProjekata.StatisticsControl();
            this.statAverage = new StatistikaProjekata.StatisticsControl();
            this.button1 = new System.Windows.Forms.Button();
            this.listOldProjects = new System.Windows.Forms.ListBox();
            this.txtWordsDiff = new System.Windows.Forms.Label();
            this.txtObjectsDiff = new System.Windows.Forms.Label();
            this.txtObjAndSubDiff = new System.Windows.Forms.Label();
            this.txtSectionDiff = new System.Windows.Forms.Label();
            this.txtFigDiff = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtWordTotalDiff = new System.Windows.Forms.Label();
            this.txtFigureTotalDiff = new System.Windows.Forms.Label();
            this.txtObjectsTotalDiff = new System.Windows.Forms.Label();
            this.txtSectionTotalDiff = new System.Windows.Forms.Label();
            this.txtObjectsAndSubTotalDiff = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtWordDiffPercent = new System.Windows.Forms.Label();
            this.txtFigureDiffPercent = new System.Windows.Forms.Label();
            this.txtObjectDiffPercent = new System.Windows.Forms.Label();
            this.txtSectionDiffPerrcent = new System.Windows.Forms.Label();
            this.txtObjAndSubDiffPercent = new System.Windows.Forms.Label();
            this.lblLessonWithoutPractice = new System.Windows.Forms.Label();
            this.lblLessonsWithoutHomework = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnExportOther = new System.Windows.Forms.Button();
            this.chbOpenFolder = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvProjects
            // 
            this.lvProjects.AllowDrop = true;
            this.lvProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvProjects.FormattingEnabled = true;
            this.lvProjects.Location = new System.Drawing.Point(3, 16);
            this.lvProjects.Name = "lvProjects";
            this.lvProjects.Size = new System.Drawing.Size(134, 472);
            this.lvProjects.TabIndex = 0;
            this.lvProjects.SelectedIndexChanged += new System.EventHandler(this.lvProjects_SelectedIndexChanged);
            this.lvProjects.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvProjects_DragDrop);
            this.lvProjects.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvProjects_DragEnter);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpen.Location = new System.Drawing.Point(12, 537);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "Open";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblTotal.Location = new System.Drawing.Point(352, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(36, 13);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Total";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(143, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Project";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "List projects";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(561, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Avarage";
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExport.Location = new System.Drawing.Point(12, 566);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(113, 23);
            this.btnExport.TabIndex = 9;
            this.btnExport.Text = "Export to Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblTotal, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lvProjects, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.statProject, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.statTotal, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.statAverage, 3, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(767, 491);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // statProject
            // 
            this.statProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statProject.Location = new System.Drawing.Point(143, 16);
            this.statProject.Name = "statProject";
            this.statProject.Size = new System.Drawing.Size(203, 472);
            statistics1.ObjectsWithTestsPercent = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.statProject.Statistics = statistics1;
            this.statProject.TabIndex = 8;
            // 
            // statTotal
            // 
            this.statTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statTotal.Location = new System.Drawing.Point(352, 16);
            this.statTotal.Name = "statTotal";
            this.statTotal.Size = new System.Drawing.Size(203, 472);
            statistics2.ObjectsWithTestsPercent = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.statTotal.Statistics = statistics2;
            this.statTotal.TabIndex = 9;
            // 
            // statAverage
            // 
            this.statAverage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.statAverage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statAverage.Location = new System.Drawing.Point(561, 16);
            this.statAverage.Name = "statAverage";
            this.statAverage.Size = new System.Drawing.Size(203, 472);
            statistics3.ObjectsWithTestsPercent = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.statAverage.Statistics = statistics3;
            this.statAverage.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(975, 537);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "Load old lesson";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listOldProjects
            // 
            this.listOldProjects.AllowDrop = true;
            this.listOldProjects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.listOldProjects.FormattingEnabled = true;
            this.listOldProjects.Location = new System.Drawing.Point(793, 28);
            this.listOldProjects.Name = "listOldProjects";
            this.listOldProjects.Size = new System.Drawing.Size(152, 472);
            this.listOldProjects.TabIndex = 12;
            this.listOldProjects.SelectedIndexChanged += new System.EventHandler(this.listOldProjects_SelectedIndexChanged);
            this.listOldProjects.DragDrop += new System.Windows.Forms.DragEventHandler(this.listOldProjects_DragDrop);
            this.listOldProjects.DragEnter += new System.Windows.Forms.DragEventHandler(this.listOldProjects_DragEnter);
            // 
            // txtWordsDiff
            // 
            this.txtWordsDiff.AutoSize = true;
            this.txtWordsDiff.Location = new System.Drawing.Point(6, 29);
            this.txtWordsDiff.Name = "txtWordsDiff";
            this.txtWordsDiff.Size = new System.Drawing.Size(86, 13);
            this.txtWordsDiff.TabIndex = 13;
            this.txtWordsDiff.Text = "Word difference:";
            // 
            // txtObjectsDiff
            // 
            this.txtObjectsDiff.AutoSize = true;
            this.txtObjectsDiff.Location = new System.Drawing.Point(6, 55);
            this.txtObjectsDiff.Name = "txtObjectsDiff";
            this.txtObjectsDiff.Size = new System.Drawing.Size(96, 13);
            this.txtObjectsDiff.TabIndex = 14;
            this.txtObjectsDiff.Text = "Objects difference:";
            // 
            // txtObjAndSubDiff
            // 
            this.txtObjAndSubDiff.AutoSize = true;
            this.txtObjAndSubDiff.Location = new System.Drawing.Point(6, 80);
            this.txtObjAndSubDiff.Name = "txtObjAndSubDiff";
            this.txtObjAndSubDiff.Size = new System.Drawing.Size(165, 13);
            this.txtObjAndSubDiff.TabIndex = 15;
            this.txtObjAndSubDiff.Text = "Objects and subojects difference:";
            // 
            // txtSectionDiff
            // 
            this.txtSectionDiff.AutoSize = true;
            this.txtSectionDiff.Location = new System.Drawing.Point(6, 105);
            this.txtSectionDiff.Name = "txtSectionDiff";
            this.txtSectionDiff.Size = new System.Drawing.Size(96, 13);
            this.txtSectionDiff.TabIndex = 16;
            this.txtSectionDiff.Text = "Section difference:";
            // 
            // txtFigDiff
            // 
            this.txtFigDiff.AutoSize = true;
            this.txtFigDiff.Location = new System.Drawing.Point(6, 130);
            this.txtFigDiff.Name = "txtFigDiff";
            this.txtFigDiff.Size = new System.Drawing.Size(89, 13);
            this.txtFigDiff.TabIndex = 17;
            this.txtFigDiff.Text = "Figure difference:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtWordsDiff);
            this.groupBox1.Controls.Add(this.txtFigDiff);
            this.groupBox1.Controls.Add(this.txtObjectsDiff);
            this.groupBox1.Controls.Add(this.txtSectionDiff);
            this.groupBox1.Controls.Add(this.txtObjAndSubDiff);
            this.groupBox1.Location = new System.Drawing.Point(960, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(220, 160);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Per lesson";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.txtWordTotalDiff);
            this.groupBox2.Controls.Add(this.txtFigureTotalDiff);
            this.groupBox2.Controls.Add(this.txtObjectsTotalDiff);
            this.groupBox2.Controls.Add(this.txtSectionTotalDiff);
            this.groupBox2.Controls.Add(this.txtObjectsAndSubTotalDiff);
            this.groupBox2.Location = new System.Drawing.Point(960, 363);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 161);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Total";
            // 
            // txtWordTotalDiff
            // 
            this.txtWordTotalDiff.AutoSize = true;
            this.txtWordTotalDiff.Location = new System.Drawing.Point(6, 23);
            this.txtWordTotalDiff.Name = "txtWordTotalDiff";
            this.txtWordTotalDiff.Size = new System.Drawing.Size(86, 13);
            this.txtWordTotalDiff.TabIndex = 13;
            this.txtWordTotalDiff.Text = "Word difference:";
            // 
            // txtFigureTotalDiff
            // 
            this.txtFigureTotalDiff.AutoSize = true;
            this.txtFigureTotalDiff.Location = new System.Drawing.Point(6, 124);
            this.txtFigureTotalDiff.Name = "txtFigureTotalDiff";
            this.txtFigureTotalDiff.Size = new System.Drawing.Size(89, 13);
            this.txtFigureTotalDiff.TabIndex = 17;
            this.txtFigureTotalDiff.Text = "Figure difference:";
            // 
            // txtObjectsTotalDiff
            // 
            this.txtObjectsTotalDiff.AutoSize = true;
            this.txtObjectsTotalDiff.Location = new System.Drawing.Point(6, 49);
            this.txtObjectsTotalDiff.Name = "txtObjectsTotalDiff";
            this.txtObjectsTotalDiff.Size = new System.Drawing.Size(96, 13);
            this.txtObjectsTotalDiff.TabIndex = 14;
            this.txtObjectsTotalDiff.Text = "Objects difference:";
            // 
            // txtSectionTotalDiff
            // 
            this.txtSectionTotalDiff.AutoSize = true;
            this.txtSectionTotalDiff.Location = new System.Drawing.Point(6, 99);
            this.txtSectionTotalDiff.Name = "txtSectionTotalDiff";
            this.txtSectionTotalDiff.Size = new System.Drawing.Size(96, 13);
            this.txtSectionTotalDiff.TabIndex = 16;
            this.txtSectionTotalDiff.Text = "Section difference:";
            // 
            // txtObjectsAndSubTotalDiff
            // 
            this.txtObjectsAndSubTotalDiff.AutoSize = true;
            this.txtObjectsAndSubTotalDiff.Location = new System.Drawing.Point(6, 74);
            this.txtObjectsAndSubTotalDiff.Name = "txtObjectsAndSubTotalDiff";
            this.txtObjectsAndSubTotalDiff.Size = new System.Drawing.Size(165, 13);
            this.txtObjectsAndSubTotalDiff.TabIndex = 15;
            this.txtObjectsAndSubTotalDiff.Text = "Objects and subojects difference:";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.txtWordDiffPercent);
            this.groupBox3.Controls.Add(this.txtFigureDiffPercent);
            this.groupBox3.Controls.Add(this.txtObjectDiffPercent);
            this.groupBox3.Controls.Add(this.txtSectionDiffPerrcent);
            this.groupBox3.Controls.Add(this.txtObjAndSubDiffPercent);
            this.groupBox3.Location = new System.Drawing.Point(960, 194);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 161);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Total percent";
            // 
            // txtWordDiffPercent
            // 
            this.txtWordDiffPercent.AutoSize = true;
            this.txtWordDiffPercent.Location = new System.Drawing.Point(6, 23);
            this.txtWordDiffPercent.Name = "txtWordDiffPercent";
            this.txtWordDiffPercent.Size = new System.Drawing.Size(86, 13);
            this.txtWordDiffPercent.TabIndex = 13;
            this.txtWordDiffPercent.Text = "Word difference:";
            // 
            // txtFigureDiffPercent
            // 
            this.txtFigureDiffPercent.AutoSize = true;
            this.txtFigureDiffPercent.Location = new System.Drawing.Point(6, 124);
            this.txtFigureDiffPercent.Name = "txtFigureDiffPercent";
            this.txtFigureDiffPercent.Size = new System.Drawing.Size(89, 13);
            this.txtFigureDiffPercent.TabIndex = 17;
            this.txtFigureDiffPercent.Text = "Figure difference:";
            // 
            // txtObjectDiffPercent
            // 
            this.txtObjectDiffPercent.AutoSize = true;
            this.txtObjectDiffPercent.Location = new System.Drawing.Point(6, 49);
            this.txtObjectDiffPercent.Name = "txtObjectDiffPercent";
            this.txtObjectDiffPercent.Size = new System.Drawing.Size(96, 13);
            this.txtObjectDiffPercent.TabIndex = 14;
            this.txtObjectDiffPercent.Text = "Objects difference:";
            // 
            // txtSectionDiffPerrcent
            // 
            this.txtSectionDiffPerrcent.AutoSize = true;
            this.txtSectionDiffPerrcent.Location = new System.Drawing.Point(6, 99);
            this.txtSectionDiffPerrcent.Name = "txtSectionDiffPerrcent";
            this.txtSectionDiffPerrcent.Size = new System.Drawing.Size(96, 13);
            this.txtSectionDiffPerrcent.TabIndex = 16;
            this.txtSectionDiffPerrcent.Text = "Section difference:";
            // 
            // txtObjAndSubDiffPercent
            // 
            this.txtObjAndSubDiffPercent.AutoSize = true;
            this.txtObjAndSubDiffPercent.Location = new System.Drawing.Point(6, 74);
            this.txtObjAndSubDiffPercent.Name = "txtObjAndSubDiffPercent";
            this.txtObjAndSubDiffPercent.Size = new System.Drawing.Size(165, 13);
            this.txtObjAndSubDiffPercent.TabIndex = 15;
            this.txtObjAndSubDiffPercent.Text = "Objects and subojects difference:";
            // 
            // lblLessonWithoutPractice
            // 
            this.lblLessonWithoutPractice.AutoSize = true;
            this.lblLessonWithoutPractice.Location = new System.Drawing.Point(15, 519);
            this.lblLessonWithoutPractice.Name = "lblLessonWithoutPractice";
            this.lblLessonWithoutPractice.Size = new System.Drawing.Size(127, 13);
            this.lblLessonWithoutPractice.TabIndex = 21;
            this.lblLessonWithoutPractice.Text = "Lessons without practice:";
            // 
            // lblLessonsWithoutHomework
            // 
            this.lblLessonsWithoutHomework.AutoSize = true;
            this.lblLessonsWithoutHomework.Location = new System.Drawing.Point(15, 506);
            this.lblLessonsWithoutHomework.Name = "lblLessonsWithoutHomework";
            this.lblLessonsWithoutHomework.Size = new System.Drawing.Size(138, 13);
            this.lblLessonsWithoutHomework.TabIndex = 22;
            this.lblLessonsWithoutHomework.Text = "Lessons without homework:";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(93, 537);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 23;
            this.button2.Text = "Open folder";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(1056, 537);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 23);
            this.button3.TabIndex = 24;
            this.button3.Text = "Load old folder";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnExportOther
            // 
            this.btnExportOther.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExportOther.Location = new System.Drawing.Point(131, 566);
            this.btnExportOther.Name = "btnExportOther";
            this.btnExportOther.Size = new System.Drawing.Size(102, 23);
            this.btnExportOther.TabIndex = 25;
            this.btnExportOther.Text = "Export for ISUM";
            this.btnExportOther.UseVisualStyleBackColor = true;
            this.btnExportOther.Click += new System.EventHandler(this.btnExportOther_Click);
            // 
            // chbOpenFolder
            // 
            this.chbOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbOpenFolder.AutoSize = true;
            this.chbOpenFolder.Checked = true;
            this.chbOpenFolder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOpenFolder.Location = new System.Drawing.Point(239, 570);
            this.chbOpenFolder.Name = "chbOpenFolder";
            this.chbOpenFolder.Size = new System.Drawing.Size(163, 17);
            this.chbOpenFolder.TabIndex = 26;
            this.chbOpenFolder.Text = "Show in folder after exporting";
            this.chbOpenFolder.UseVisualStyleBackColor = true;
            // 
            // btnReset
            // 
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Location = new System.Drawing.Point(1097, 566);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 27;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // StatisticsForm
            // 
            this.ClientSize = new System.Drawing.Size(1184, 601);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.chbOpenFolder);
            this.Controls.Add(this.btnExportOther);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblLessonsWithoutHomework);
            this.Controls.Add(this.lblLessonWithoutPractice);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listOldProjects);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnOpen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1200, 640);
            this.Name = "StatisticsForm";
            this.Text = "Statistika mDita projekta";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StatisticsPanel_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lvProjects;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private StatisticsControl statProject;
        private StatisticsControl statTotal;
        private StatisticsControl statAverage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listOldProjects;
        private System.Windows.Forms.Label txtWordsDiff;
        private System.Windows.Forms.Label txtObjectsDiff;
        private System.Windows.Forms.Label txtObjAndSubDiff;
        private System.Windows.Forms.Label txtSectionDiff;
        private System.Windows.Forms.Label txtFigDiff;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label txtWordTotalDiff;
        private System.Windows.Forms.Label txtFigureTotalDiff;
        private System.Windows.Forms.Label txtObjectsTotalDiff;
        private System.Windows.Forms.Label txtSectionTotalDiff;
        private System.Windows.Forms.Label txtObjectsAndSubTotalDiff;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label txtWordDiffPercent;
        private System.Windows.Forms.Label txtFigureDiffPercent;
        private System.Windows.Forms.Label txtObjectDiffPercent;
        private System.Windows.Forms.Label txtSectionDiffPerrcent;
        private System.Windows.Forms.Label txtObjAndSubDiffPercent;
        private System.Windows.Forms.Label lblLessonWithoutPractice;
        private System.Windows.Forms.Label lblLessonsWithoutHomework;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnExportOther;
        private System.Windows.Forms.CheckBox chbOpenFolder;
        private System.Windows.Forms.Button btnReset;
    }
}

