using mDitaEditor.CustomControls;

namespace mDitaEditor.Lams.Controls
{
    partial class AssessmentMcControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.box = new System.Windows.Forms.CheckBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.pictureBoxOdgovor = new System.Windows.Forms.PictureBox();
            this.dodajSlikuPitanje = new System.Windows.Forms.Button();
            this.odgovorTextBox = new CueTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOdgovor)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Odgovor";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // box
            // 
            this.box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.box.AutoSize = true;
            this.box.Location = new System.Drawing.Point(348, 23);
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(15, 14);
            this.box.TabIndex = 46;
            this.box.UseVisualStyleBackColor = true;
            this.box.CheckedChanged += new System.EventHandler(this.box_CheckedChanged);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Image = global::mDitaEditor.Properties.Resources.arrowdown;
            this.btnDown.Location = new System.Drawing.Point(369, 41);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 52;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = global::mDitaEditor.Properties.Resources.arrowup;
            this.btnUp.Location = new System.Drawing.Point(369, 3);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 32);
            this.btnUp.TabIndex = 51;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::mDitaEditor.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(369, 79);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 50;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(182, 122);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(219, 22);
            this.progressBarUpload.TabIndex = 89;
            // 
            // pictureBoxOdgovor
            // 
            this.pictureBoxOdgovor.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBoxOdgovor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxOdgovor.Location = new System.Drawing.Point(3, 48);
            this.pictureBoxOdgovor.Name = "pictureBoxOdgovor";
            this.pictureBoxOdgovor.Size = new System.Drawing.Size(173, 96);
            this.pictureBoxOdgovor.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxOdgovor.TabIndex = 88;
            this.pictureBoxOdgovor.TabStop = false;
            // 
            // dodajSlikuPitanje
            // 
            this.dodajSlikuPitanje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dodajSlikuPitanje.Location = new System.Drawing.Point(182, 93);
            this.dodajSlikuPitanje.Name = "dodajSlikuPitanje";
            this.dodajSlikuPitanje.Size = new System.Drawing.Size(69, 23);
            this.dodajSlikuPitanje.TabIndex = 87;
            this.dodajSlikuPitanje.Text = "Dodaj sliku";
            this.dodajSlikuPitanje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dodajSlikuPitanje.UseVisualStyleBackColor = true;
            this.dodajSlikuPitanje.Click += new System.EventHandler(this.dodajSlikuPitanje_Click);
            // 
            // odgovorTextBox
            // 
            this.odgovorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.odgovorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.odgovorTextBox.Cue = null;
            this.odgovorTextBox.CueColor = System.Drawing.Color.Gray;
            this.odgovorTextBox.Location = new System.Drawing.Point(3, 17);
            this.odgovorTextBox.Multiline = true;
            this.odgovorTextBox.Name = "odgovorTextBox";
            this.odgovorTextBox.Size = new System.Drawing.Size(339, 25);
            this.odgovorTextBox.TabIndex = 40;
            this.odgovorTextBox.TextChanged += new System.EventHandler(this.odgovorTextBox_TextChanged);
            // 
            // AssessmentMCAnswerAddControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.progressBarUpload);
            this.Controls.Add(this.pictureBoxOdgovor);
            this.Controls.Add(this.dodajSlikuPitanje);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.box);
            this.Controls.Add(this.odgovorTextBox);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.Name = "AssessmentMcControl";
            this.Size = new System.Drawing.Size(404, 148);
            this.Load += new System.EventHandler(this.AssessmentMCAnswerAddControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxOdgovor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public CueTextBox odgovorTextBox;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnDown;
        public System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        public System.Windows.Forms.PictureBox pictureBoxOdgovor;
        private System.Windows.Forms.Button dodajSlikuPitanje;
        public System.Windows.Forms.CheckBox box;
    }
}
