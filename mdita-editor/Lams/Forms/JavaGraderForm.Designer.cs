using mDitaEditor.CustomControls;

namespace mDitaEditor.Lams.Forms
{
    partial class JavaGraderGui
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JavaGraderGui));
            this.txtAnswer = new FastColoredTextBoxNS.FastColoredTextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddEdit = new System.Windows.Forms.Button();
            this.picBoxCorrect = new System.Windows.Forms.PictureBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtResults10 = new CueTextBox();
            this.txtParam10 = new CueTextBox();
            this.txtResults9 = new CueTextBox();
            this.txtParam9 = new CueTextBox();
            this.txtResults8 = new CueTextBox();
            this.txtParam8 = new CueTextBox();
            this.txtResults7 = new CueTextBox();
            this.txtParam7 = new CueTextBox();
            this.txtResults6 = new CueTextBox();
            this.txtParam6 = new CueTextBox();
            this.txtResults5 = new CueTextBox();
            this.txtParam5 = new CueTextBox();
            this.txtResults4 = new CueTextBox();
            this.txtParam4 = new CueTextBox();
            this.txtResults3 = new CueTextBox();
            this.txtParam3 = new CueTextBox();
            this.txtResults2 = new CueTextBox();
            this.txtParam2 = new CueTextBox();
            this.txtResults1 = new CueTextBox();
            this.txtParam1 = new CueTextBox();
            this.txtTekstPitanja = new CueTextBox();
            this.txtNaslov = new CueTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txtAnswer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCorrect)).BeginInit();
            this.SuspendLayout();
            // 
            // txtAnswer
            // 
            this.txtAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAnswer.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtAnswer.AutoIndentCharsPatterns = "\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);\n";
            this.txtAnswer.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.txtAnswer.BackBrush = null;
            this.txtAnswer.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.txtAnswer.CharHeight = 14;
            this.txtAnswer.CharWidth = 8;
            this.txtAnswer.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtAnswer.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtAnswer.HighlightingRangeType = FastColoredTextBoxNS.HighlightingRangeType.AllTextRange;
            this.txtAnswer.IsReplaceMode = false;
            this.txtAnswer.Language = FastColoredTextBoxNS.Language.CSharp;
            this.txtAnswer.LeftBracket = '(';
            this.txtAnswer.LeftBracket2 = '{';
            this.txtAnswer.Location = new System.Drawing.Point(12, 271);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Paddings = new System.Windows.Forms.Padding(0);
            this.txtAnswer.RightBracket = ')';
            this.txtAnswer.RightBracket2 = '}';
            this.txtAnswer.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtAnswer.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtAnswer.ServiceColors")));
            this.txtAnswer.Size = new System.Drawing.Size(559, 119);
            this.txtAnswer.TabIndex = 0;
            this.txtAnswer.Zoom = 100;
            this.txtAnswer.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.txtAnswer_TextChanged);
            this.txtAnswer.Load += new System.EventHandler(this.fastColoredTextBox1_Load);
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 70);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(559, 121);
            this.listBox1.TabIndex = 1;
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Image = global::mDitaEditor.Properties.Resources.arrowdown;
            this.btnDown.Location = new System.Drawing.Point(579, 166);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 16;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = global::mDitaEditor.Properties.Resources.arrowup;
            this.btnUp.Location = new System.Drawing.Point(579, 134);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 32);
            this.btnUp.TabIndex = 15;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::mDitaEditor.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(579, 102);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Naslov:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Lista pitanja:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Tekst pitanja:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 255);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Java odgovor:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 406);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Test slučajevi:";
            // 
            // btnAddEdit
            // 
            this.btnAddEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddEdit.Location = new System.Drawing.Point(490, 419);
            this.btnAddEdit.Name = "btnAddEdit";
            this.btnAddEdit.Size = new System.Drawing.Size(119, 23);
            this.btnAddEdit.TabIndex = 45;
            this.btnAddEdit.Text = "Add/Edit question";
            this.btnAddEdit.UseVisualStyleBackColor = true;
            this.btnAddEdit.Click += new System.EventHandler(this.button1_Click);
            // 
            // picBoxCorrect
            // 
            this.picBoxCorrect.Location = new System.Drawing.Point(579, 271);
            this.picBoxCorrect.Name = "picBoxCorrect";
            this.picBoxCorrect.Size = new System.Drawing.Size(30, 28);
            this.picBoxCorrect.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picBoxCorrect.TabIndex = 46;
            this.picBoxCorrect.TabStop = false;
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = global::mDitaEditor.Properties.Resources.edit;
            this.btnEdit.Location = new System.Drawing.Point(579, 70);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(32, 32);
            this.btnEdit.TabIndex = 47;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(492, 656);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 48;
            this.button1.Text = "Save Javagrader";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // txtResults10
            // 
            this.txtResults10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults10.Cue = null;
            this.txtResults10.CueColor = System.Drawing.Color.Gray;
            this.txtResults10.Enabled = false;
            this.txtResults10.Location = new System.Drawing.Point(212, 656);
            this.txtResults10.Name = "txtResults10";
            this.txtResults10.Size = new System.Drawing.Size(172, 20);
            this.txtResults10.TabIndex = 44;
            // 
            // txtParam10
            // 
            this.txtParam10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam10.Cue = null;
            this.txtParam10.CueColor = System.Drawing.Color.Gray;
            this.txtParam10.Location = new System.Drawing.Point(15, 656);
            this.txtParam10.Name = "txtParam10";
            this.txtParam10.Size = new System.Drawing.Size(172, 20);
            this.txtParam10.TabIndex = 43;
            this.txtParam10.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults9
            // 
            this.txtResults9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults9.Cue = null;
            this.txtResults9.CueColor = System.Drawing.Color.Gray;
            this.txtResults9.Enabled = false;
            this.txtResults9.Location = new System.Drawing.Point(212, 630);
            this.txtResults9.Name = "txtResults9";
            this.txtResults9.Size = new System.Drawing.Size(172, 20);
            this.txtResults9.TabIndex = 42;
            // 
            // txtParam9
            // 
            this.txtParam9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam9.Cue = null;
            this.txtParam9.CueColor = System.Drawing.Color.Gray;
            this.txtParam9.Location = new System.Drawing.Point(15, 630);
            this.txtParam9.Name = "txtParam9";
            this.txtParam9.Size = new System.Drawing.Size(172, 20);
            this.txtParam9.TabIndex = 41;
            this.txtParam9.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults8
            // 
            this.txtResults8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults8.Cue = null;
            this.txtResults8.CueColor = System.Drawing.Color.Gray;
            this.txtResults8.Enabled = false;
            this.txtResults8.Location = new System.Drawing.Point(212, 604);
            this.txtResults8.Name = "txtResults8";
            this.txtResults8.Size = new System.Drawing.Size(172, 20);
            this.txtResults8.TabIndex = 40;
            // 
            // txtParam8
            // 
            this.txtParam8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam8.Cue = null;
            this.txtParam8.CueColor = System.Drawing.Color.Gray;
            this.txtParam8.Location = new System.Drawing.Point(15, 604);
            this.txtParam8.Name = "txtParam8";
            this.txtParam8.Size = new System.Drawing.Size(172, 20);
            this.txtParam8.TabIndex = 39;
            this.txtParam8.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults7
            // 
            this.txtResults7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults7.Cue = null;
            this.txtResults7.CueColor = System.Drawing.Color.Gray;
            this.txtResults7.Enabled = false;
            this.txtResults7.Location = new System.Drawing.Point(212, 578);
            this.txtResults7.Name = "txtResults7";
            this.txtResults7.Size = new System.Drawing.Size(172, 20);
            this.txtResults7.TabIndex = 38;
            // 
            // txtParam7
            // 
            this.txtParam7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam7.Cue = null;
            this.txtParam7.CueColor = System.Drawing.Color.Gray;
            this.txtParam7.Location = new System.Drawing.Point(15, 578);
            this.txtParam7.Name = "txtParam7";
            this.txtParam7.Size = new System.Drawing.Size(172, 20);
            this.txtParam7.TabIndex = 37;
            this.txtParam7.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults6
            // 
            this.txtResults6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults6.Cue = null;
            this.txtResults6.CueColor = System.Drawing.Color.Gray;
            this.txtResults6.Enabled = false;
            this.txtResults6.Location = new System.Drawing.Point(212, 552);
            this.txtResults6.Name = "txtResults6";
            this.txtResults6.Size = new System.Drawing.Size(172, 20);
            this.txtResults6.TabIndex = 36;
            // 
            // txtParam6
            // 
            this.txtParam6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam6.Cue = null;
            this.txtParam6.CueColor = System.Drawing.Color.Gray;
            this.txtParam6.Location = new System.Drawing.Point(15, 552);
            this.txtParam6.Name = "txtParam6";
            this.txtParam6.Size = new System.Drawing.Size(172, 20);
            this.txtParam6.TabIndex = 35;
            this.txtParam6.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults5
            // 
            this.txtResults5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults5.Cue = null;
            this.txtResults5.CueColor = System.Drawing.Color.Gray;
            this.txtResults5.Enabled = false;
            this.txtResults5.Location = new System.Drawing.Point(212, 526);
            this.txtResults5.Name = "txtResults5";
            this.txtResults5.Size = new System.Drawing.Size(172, 20);
            this.txtResults5.TabIndex = 34;
            // 
            // txtParam5
            // 
            this.txtParam5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam5.Cue = null;
            this.txtParam5.CueColor = System.Drawing.Color.Gray;
            this.txtParam5.Location = new System.Drawing.Point(15, 526);
            this.txtParam5.Name = "txtParam5";
            this.txtParam5.Size = new System.Drawing.Size(172, 20);
            this.txtParam5.TabIndex = 33;
            this.txtParam5.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults4
            // 
            this.txtResults4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults4.Cue = null;
            this.txtResults4.CueColor = System.Drawing.Color.Gray;
            this.txtResults4.Enabled = false;
            this.txtResults4.Location = new System.Drawing.Point(212, 500);
            this.txtResults4.Name = "txtResults4";
            this.txtResults4.Size = new System.Drawing.Size(172, 20);
            this.txtResults4.TabIndex = 32;
            // 
            // txtParam4
            // 
            this.txtParam4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam4.Cue = null;
            this.txtParam4.CueColor = System.Drawing.Color.Gray;
            this.txtParam4.Location = new System.Drawing.Point(15, 500);
            this.txtParam4.Name = "txtParam4";
            this.txtParam4.Size = new System.Drawing.Size(172, 20);
            this.txtParam4.TabIndex = 31;
            this.txtParam4.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults3
            // 
            this.txtResults3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults3.Cue = null;
            this.txtResults3.CueColor = System.Drawing.Color.Gray;
            this.txtResults3.Enabled = false;
            this.txtResults3.Location = new System.Drawing.Point(212, 474);
            this.txtResults3.Name = "txtResults3";
            this.txtResults3.Size = new System.Drawing.Size(172, 20);
            this.txtResults3.TabIndex = 30;
            // 
            // txtParam3
            // 
            this.txtParam3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam3.Cue = null;
            this.txtParam3.CueColor = System.Drawing.Color.Gray;
            this.txtParam3.Location = new System.Drawing.Point(15, 474);
            this.txtParam3.Name = "txtParam3";
            this.txtParam3.Size = new System.Drawing.Size(172, 20);
            this.txtParam3.TabIndex = 29;
            this.txtParam3.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults2
            // 
            this.txtResults2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults2.Cue = null;
            this.txtResults2.CueColor = System.Drawing.Color.Gray;
            this.txtResults2.Enabled = false;
            this.txtResults2.Location = new System.Drawing.Point(212, 448);
            this.txtResults2.Name = "txtResults2";
            this.txtResults2.Size = new System.Drawing.Size(172, 20);
            this.txtResults2.TabIndex = 28;
            // 
            // txtParam2
            // 
            this.txtParam2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam2.Cue = null;
            this.txtParam2.CueColor = System.Drawing.Color.Gray;
            this.txtParam2.Location = new System.Drawing.Point(15, 448);
            this.txtParam2.Name = "txtParam2";
            this.txtParam2.Size = new System.Drawing.Size(172, 20);
            this.txtParam2.TabIndex = 27;
            this.txtParam2.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtResults1
            // 
            this.txtResults1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtResults1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResults1.Cue = null;
            this.txtResults1.CueColor = System.Drawing.Color.Gray;
            this.txtResults1.Enabled = false;
            this.txtResults1.Location = new System.Drawing.Point(212, 422);
            this.txtResults1.Name = "txtResults1";
            this.txtResults1.Size = new System.Drawing.Size(172, 20);
            this.txtResults1.TabIndex = 26;
            // 
            // txtParam1
            // 
            this.txtParam1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtParam1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtParam1.Cue = null;
            this.txtParam1.CueColor = System.Drawing.Color.Gray;
            this.txtParam1.Location = new System.Drawing.Point(15, 422);
            this.txtParam1.Name = "txtParam1";
            this.txtParam1.Size = new System.Drawing.Size(172, 20);
            this.txtParam1.TabIndex = 24;
            this.txtParam1.TextChanged += new System.EventHandler(this.txtParam1_TextChanged);
            // 
            // txtTekstPitanja
            // 
            this.txtTekstPitanja.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTekstPitanja.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTekstPitanja.Cue = null;
            this.txtTekstPitanja.CueColor = System.Drawing.Color.Gray;
            this.txtTekstPitanja.Location = new System.Drawing.Point(12, 221);
            this.txtTekstPitanja.Name = "txtTekstPitanja";
            this.txtTekstPitanja.Size = new System.Drawing.Size(597, 20);
            this.txtTekstPitanja.TabIndex = 21;
            // 
            // txtNaslov
            // 
            this.txtNaslov.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNaslov.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNaslov.Cue = null;
            this.txtNaslov.CueColor = System.Drawing.Color.Gray;
            this.txtNaslov.Location = new System.Drawing.Point(12, 30);
            this.txtNaslov.Name = "txtNaslov";
            this.txtNaslov.Size = new System.Drawing.Size(597, 20);
            this.txtNaslov.TabIndex = 18;
            this.txtNaslov.TextChanged += new System.EventHandler(this.txtNaslov_TextChanged);
            // 
            // JavaGraderGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(623, 693);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.picBoxCorrect);
            this.Controls.Add(this.btnAddEdit);
            this.Controls.Add(this.txtResults10);
            this.Controls.Add(this.txtParam10);
            this.Controls.Add(this.txtResults9);
            this.Controls.Add(this.txtParam9);
            this.Controls.Add(this.txtResults8);
            this.Controls.Add(this.txtParam8);
            this.Controls.Add(this.txtResults7);
            this.Controls.Add(this.txtParam7);
            this.Controls.Add(this.txtResults6);
            this.Controls.Add(this.txtParam6);
            this.Controls.Add(this.txtResults5);
            this.Controls.Add(this.txtParam5);
            this.Controls.Add(this.txtResults4);
            this.Controls.Add(this.txtParam4);
            this.Controls.Add(this.txtResults3);
            this.Controls.Add(this.txtParam3);
            this.Controls.Add(this.txtResults2);
            this.Controls.Add(this.txtParam2);
            this.Controls.Add(this.txtResults1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtParam1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTekstPitanja);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNaslov);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.txtAnswer);
            this.Name = "JavaGraderGui";
            this.Text = "JavaGraderGui";
            this.Load += new System.EventHandler(this.JavaGraderGUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txtAnswer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxCorrect)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox txtAnswer;
        private System.Windows.Forms.ListBox listBox1;
        public System.Windows.Forms.Button btnDown;
        public System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label1;
        private CueTextBox txtNaslov;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private CueTextBox txtTekstPitanja;
        private System.Windows.Forms.Label label4;
        private CueTextBox txtParam1;
        private System.Windows.Forms.Label label5;
        private CueTextBox txtResults1;
        private CueTextBox txtParam2;
        private CueTextBox txtResults2;
        private CueTextBox txtResults4;
        private CueTextBox txtParam4;
        private CueTextBox txtResults3;
        private CueTextBox txtParam3;
        private CueTextBox txtResults6;
        private CueTextBox txtParam6;
        private CueTextBox txtResults5;
        private CueTextBox txtParam5;
        private CueTextBox txtResults8;
        private CueTextBox txtParam8;
        private CueTextBox txtResults7;
        private CueTextBox txtParam7;
        private CueTextBox txtResults10;
        private CueTextBox txtParam10;
        private CueTextBox txtResults9;
        private CueTextBox txtParam9;
        private System.Windows.Forms.Button btnAddEdit;
        private System.Windows.Forms.PictureBox picBoxCorrect;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button button1;
    }
}