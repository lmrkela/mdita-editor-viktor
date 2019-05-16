using mDitaEditor.CustomControls;

namespace mDitaEditor.Lams.Forms
{
    partial class AssessmentNumericalForm
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
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPitanjeText = new CueTextBox();
            this.txtPitanjeNaziv = new CueTextBox();
            this.panelOdgovori = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPitanje = new System.Windows.Forms.Label();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.pictureBoxPitanje = new System.Windows.Forms.PictureBox();
            this.dodajSlikuPitanje = new System.Windows.Forms.Button();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPitanje)).BeginInit();
            this.SuspendLayout();
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(550, 350);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 90;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPitanjeText
            // 
            this.txtPitanjeText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPitanjeText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPitanjeText.Cue = null;
            this.txtPitanjeText.CueColor = System.Drawing.Color.Gray;
            this.txtPitanjeText.Location = new System.Drawing.Point(12, 64);
            this.txtPitanjeText.Name = "txtPitanjeText";
            this.txtPitanjeText.Size = new System.Drawing.Size(613, 20);
            this.txtPitanjeText.TabIndex = 88;
            this.txtPitanjeText.TextChanged += new System.EventHandler(this.txtPitanjeText_TextChanged);
            // 
            // txtPitanjeNaziv
            // 
            this.txtPitanjeNaziv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPitanjeNaziv.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPitanjeNaziv.Cue = null;
            this.txtPitanjeNaziv.CueColor = System.Drawing.Color.Gray;
            this.txtPitanjeNaziv.Location = new System.Drawing.Point(12, 25);
            this.txtPitanjeNaziv.Name = "txtPitanjeNaziv";
            this.txtPitanjeNaziv.Size = new System.Drawing.Size(613, 20);
            this.txtPitanjeNaziv.TabIndex = 84;
            this.txtPitanjeNaziv.TextChanged += new System.EventHandler(this.txtPitanjeNaziv_TextChanged);
            // 
            // panelOdgovori
            // 
            this.panelOdgovori.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOdgovori.AutoScroll = true;
            this.panelOdgovori.Location = new System.Drawing.Point(12, 207);
            this.panelOdgovori.Name = "panelOdgovori";
            this.panelOdgovori.Size = new System.Drawing.Size(613, 137);
            this.panelOdgovori.TabIndex = 87;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 85;
            this.label2.Text = "Tekst:";
            // 
            // labelPitanje
            // 
            this.labelPitanje.AutoSize = true;
            this.labelPitanje.Location = new System.Drawing.Point(12, 9);
            this.labelPitanje.Name = "labelPitanje";
            this.labelPitanje.Size = new System.Drawing.Size(42, 13);
            this.labelPitanje.TabIndex = 83;
            this.labelPitanje.Text = "Pitanje:";
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(12, 178);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(613, 23);
            this.progressBarUpload.TabIndex = 93;
            // 
            // pictureBoxPitanje
            // 
            this.pictureBoxPitanje.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBoxPitanje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPitanje.Location = new System.Drawing.Point(12, 90);
            this.pictureBoxPitanje.Name = "pictureBoxPitanje";
            this.pictureBoxPitanje.Size = new System.Drawing.Size(128, 82);
            this.pictureBoxPitanje.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPitanje.TabIndex = 92;
            this.pictureBoxPitanje.TabStop = false;
            // 
            // dodajSlikuPitanje
            // 
            this.dodajSlikuPitanje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dodajSlikuPitanje.Location = new System.Drawing.Point(146, 149);
            this.dodajSlikuPitanje.Name = "dodajSlikuPitanje";
            this.dodajSlikuPitanje.Size = new System.Drawing.Size(69, 23);
            this.dodajSlikuPitanje.TabIndex = 91;
            this.dodajSlikuPitanje.Text = "Dodaj sliku";
            this.dodajSlikuPitanje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dodajSlikuPitanje.UseVisualStyleBackColor = true;
            this.dodajSlikuPitanje.Click += new System.EventHandler(this.dodajSlikuPitanje_Click);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.Enabled = false;
            this.vScrollBar.Location = new System.Drawing.Point(606, 207);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 137);
            this.vScrollBar.TabIndex = 94;
            // 
            // AssessmentNumericalControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(637, 385);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.progressBarUpload);
            this.Controls.Add(this.pictureBoxPitanje);
            this.Controls.Add(this.dodajSlikuPitanje);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPitanjeText);
            this.Controls.Add(this.txtPitanjeNaziv);
            this.Controls.Add(this.panelOdgovori);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPitanje);
            this.Name = "AssessmentNumericalControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssessmentNumericalControlForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssessmentNumericalControlForm_FormClosing);
            this.Resize += new System.EventHandler(this.AssessmentNumericalControlForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPitanje)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Button btnSave;
        private CueTextBox txtPitanjeText;
        private CueTextBox txtPitanjeNaziv;
        private System.Windows.Forms.FlowLayoutPanel panelOdgovori;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label labelPitanje;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        public System.Windows.Forms.PictureBox pictureBoxPitanje;
        private System.Windows.Forms.Button dodajSlikuPitanje;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}