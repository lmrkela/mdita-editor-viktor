namespace mDitaEditor.Dita.Forms
{
    partial class EditSnippetForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.chkShowLineNumber = new System.Windows.Forms.CheckBox();
            this.btnInsertSnippet = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtInsertCode = new System.Windows.Forms.TextBox();
            this.cmbSelectLanguage = new System.Windows.Forms.ComboBox();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.numLineCount = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numLineCount)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(281, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of lines:";
            // 
            // chkShowLineNumber
            // 
            this.chkShowLineNumber.AutoSize = true;
            this.chkShowLineNumber.Checked = true;
            this.chkShowLineNumber.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowLineNumber.Location = new System.Drawing.Point(426, 8);
            this.chkShowLineNumber.Name = "chkShowLineNumber";
            this.chkShowLineNumber.Size = new System.Drawing.Size(110, 17);
            this.chkShowLineNumber.TabIndex = 4;
            this.chkShowLineNumber.Text = "Show line number";
            this.chkShowLineNumber.UseVisualStyleBackColor = true;
            // 
            // btnInsertSnippet
            // 
            this.btnInsertSnippet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertSnippet.Location = new System.Drawing.Point(499, 290);
            this.btnInsertSnippet.Name = "btnInsertSnippet";
            this.btnInsertSnippet.Size = new System.Drawing.Size(75, 23);
            this.btnInsertSnippet.TabIndex = 6;
            this.btnInsertSnippet.Text = "Insert";
            this.btnInsertSnippet.UseVisualStyleBackColor = true;
            this.btnInsertSnippet.Click += new System.EventHandler(this.btnInsertSnippet_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Programming language:";
            // 
            // txtInsertCode
            // 
            this.txtInsertCode.AcceptsTab = true;
            this.txtInsertCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInsertCode.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInsertCode.HideSelection = false;
            this.txtInsertCode.Location = new System.Drawing.Point(12, 33);
            this.txtInsertCode.Multiline = true;
            this.txtInsertCode.Name = "txtInsertCode";
            this.txtInsertCode.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInsertCode.Size = new System.Drawing.Size(562, 251);
            this.txtInsertCode.TabIndex = 1;
            this.txtInsertCode.WordWrap = false;
            this.txtInsertCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtInsertCode_KeyPress);
            // 
            // cmbSelectLanguage
            // 
            this.cmbSelectLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbSelectLanguage.FormattingEnabled = true;
            this.cmbSelectLanguage.Items.AddRange(new object[] {
            "Java",
            "C",
            "C++",
            "C#",
            "Py",
            "SQL",
            "HTML",
            "CSS",
            "JS",
            "PHP",
            "Perl",
            "Ruby",
            "Shell",
            "Swift"});
            this.cmbSelectLanguage.Location = new System.Drawing.Point(136, 6);
            this.cmbSelectLanguage.Name = "cmbSelectLanguage";
            this.cmbSelectLanguage.Size = new System.Drawing.Size(139, 21);
            this.cmbSelectLanguage.TabIndex = 2;
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowseFile.Location = new System.Drawing.Point(91, 290);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFile.TabIndex = 5;
            this.btnBrowseFile.Text = "Browse";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // numLineCount
            // 
            this.numLineCount.Location = new System.Drawing.Point(370, 7);
            this.numLineCount.Name = "numLineCount";
            this.numLineCount.Size = new System.Drawing.Size(50, 20);
            this.numLineCount.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 295);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 22;
            this.label2.Text = "Load from file:";
            // 
            // EditSnippetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 325);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numLineCount);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.cmbSelectLanguage);
            this.Controls.Add(this.txtInsertCode);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnInsertSnippet);
            this.Controls.Add(this.chkShowLineNumber);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(558, 316);
            this.Name = "EditSnippetForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Snippet Editor";
            ((System.ComponentModel.ISupportInitialize)(this.numLineCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkShowLineNumber;
        private System.Windows.Forms.Button btnInsertSnippet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtInsertCode;
        private System.Windows.Forms.ComboBox cmbSelectLanguage;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.NumericUpDown numLineCount;
        private System.Windows.Forms.Label label2;
    }
}