using mDitaEditor.CustomControls;

namespace mDitaEditor.Dita.Controls
{
    partial class LearningContentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbDifficulty = new mDitaEditor.CustomControls.CueComboBox();
            this.txbDescription = new mDitaEditor.CustomControls.CueTextBox();
            this.txbDuration = new mDitaEditor.CustomControls.CueTextBox();
            this.txbKeywords = new mDitaEditor.CustomControls.CueTextBox();
            this.txbClassification = new mDitaEditor.CustomControls.CueTextBox();
            this.txbTitle = new mDitaEditor.CustomControls.CueTextBox();
            this.SuspendLayout();
            // 
            // cmbDifficulty
            // 
            this.cmbDifficulty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbDifficulty.Cue = "Nivo";
            this.cmbDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDifficulty.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDifficulty.FormattingEnabled = true;
            this.cmbDifficulty.Items.AddRange(new object[] {
            "Osnovni",
            "Srednji",
            "Napredni"});
            this.cmbDifficulty.Location = new System.Drawing.Point(267, 124);
            this.cmbDifficulty.Margin = new System.Windows.Forms.Padding(2);
            this.cmbDifficulty.Name = "cmbDifficulty";
            this.cmbDifficulty.OldIndex = 0;
            this.cmbDifficulty.Size = new System.Drawing.Size(515, 35);
            this.cmbDifficulty.TabIndex = 9;
            this.cmbDifficulty.UndoRedoEvent = false;
            this.cmbDifficulty.SelectedIndexChanged += new System.EventHandler(this.cmbDifficulty_SelectedIndexChanged);
            this.cmbDifficulty.Leave += new System.EventHandler(this.txbField_Leave);
            // 
            // txbDescription
            // 
            this.txbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbDescription.Cue = "Opis";
            this.txbDescription.CueColor = System.Drawing.Color.Gray;
            this.txbDescription.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDescription.Location = new System.Drawing.Point(36, 258);
            this.txbDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txbDescription.MaxLength = 200;
            this.txbDescription.Multiline = true;
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.OldText = null;
            this.txbDescription.Size = new System.Drawing.Size(784, 99);
            this.txbDescription.TabIndex = 6;
            this.txbDescription.TextChanged += new System.EventHandler(this.txbDescription_TextChanged);
            this.txbDescription.Enter += new System.EventHandler(this.txbField_Enter);
            this.txbDescription.Leave += new System.EventHandler(this.txbField_Leave);
            this.txbDescription.LostFocus += new System.EventHandler(this.txbField_Validated);
            // 
            // txbDuration
            // 
            this.txbDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbDuration.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbDuration.Cue = "Trajanje";
            this.txbDuration.CueColor = System.Drawing.Color.Gray;
            this.txbDuration.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbDuration.Location = new System.Drawing.Point(267, 202);
            this.txbDuration.Margin = new System.Windows.Forms.Padding(2);
            this.txbDuration.MaxLength = 50;
            this.txbDuration.Name = "txbDuration";
            this.txbDuration.OldText = null;
            this.txbDuration.Size = new System.Drawing.Size(515, 35);
            this.txbDuration.TabIndex = 5;
            this.txbDuration.TextChanged += new System.EventHandler(this.txbDuration_TextChanged);
            this.txbDuration.Enter += new System.EventHandler(this.txbField_Enter);
            this.txbDuration.Leave += new System.EventHandler(this.txbField_Leave);
            this.txbDuration.LostFocus += new System.EventHandler(this.txbField_Validated);
            // 
            // txbKeywords
            // 
            this.txbKeywords.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbKeywords.Cue = "Ključne reči";
            this.txbKeywords.CueColor = System.Drawing.Color.Gray;
            this.txbKeywords.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbKeywords.Location = new System.Drawing.Point(267, 163);
            this.txbKeywords.Margin = new System.Windows.Forms.Padding(2);
            this.txbKeywords.MaxLength = 300;
            this.txbKeywords.Name = "txbKeywords";
            this.txbKeywords.OldText = null;
            this.txbKeywords.Size = new System.Drawing.Size(515, 35);
            this.txbKeywords.TabIndex = 3;
            this.txbKeywords.TextChanged += new System.EventHandler(this.txbKeywords_TextChanged);
            this.txbKeywords.Enter += new System.EventHandler(this.txbField_Enter);
            this.txbKeywords.Leave += new System.EventHandler(this.txbField_Leave);
            this.txbKeywords.LostFocus += new System.EventHandler(this.txbField_Validated);
            // 
            // txbClassification
            // 
            this.txbClassification.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txbClassification.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbClassification.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbClassification.Cue = "Klasifikacija";
            this.txbClassification.CueColor = System.Drawing.Color.Gray;
            this.txbClassification.Font = new System.Drawing.Font("Times New Roman", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbClassification.Location = new System.Drawing.Point(267, 84);
            this.txbClassification.Margin = new System.Windows.Forms.Padding(2);
            this.txbClassification.MaxLength = 300;
            this.txbClassification.Name = "txbClassification";
            this.txbClassification.OldText = null;
            this.txbClassification.Size = new System.Drawing.Size(515, 35);
            this.txbClassification.TabIndex = 1;
            this.txbClassification.TextChanged += new System.EventHandler(this.txbClassification_TextChanged);
            this.txbClassification.Enter += new System.EventHandler(this.txbField_Enter);
            this.txbClassification.Leave += new System.EventHandler(this.txbField_Leave);
            this.txbClassification.LostFocus += new System.EventHandler(this.txbField_Validated);
            // 
            // txbTitle
            // 
            this.txbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbTitle.BackColor = System.Drawing.SystemColors.Window;
            this.txbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbTitle.Cue = "Naslov objekta učenja";
            this.txbTitle.CueColor = System.Drawing.Color.Gray;
            this.txbTitle.Font = new System.Drawing.Font("Arial Narrow", 27F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTitle.Location = new System.Drawing.Point(36, 29);
            this.txbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.txbTitle.MaxLength = 50;
            this.txbTitle.Name = "txbTitle";
            this.txbTitle.OldText = null;
            this.txbTitle.Size = new System.Drawing.Size(784, 49);
            this.txbTitle.TabIndex = 0;
            this.txbTitle.TextChanged += new System.EventHandler(this.txbTitle_TextChanged);
            this.txbTitle.Enter += new System.EventHandler(this.txbField_Enter);
            this.txbTitle.Leave += new System.EventHandler(this.txbField_Leave);
            this.txbTitle.LostFocus += new System.EventHandler(this.txbField_Validated);
            // 
            // LearningContentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.cmbDifficulty);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.txbDuration);
            this.Controls.Add(this.txbKeywords);
            this.Controls.Add(this.txbClassification);
            this.Controls.Add(this.txbTitle);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LearningContentControl";
            this.Size = new System.Drawing.Size(845, 562);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private CueTextBox txbDescription;
        public CueTextBox txbTitle;
        public CueTextBox txbClassification;
        public CueTextBox txbKeywords;
        public CueTextBox txbDuration;
        public CueComboBox cmbDifficulty;
    }
}
