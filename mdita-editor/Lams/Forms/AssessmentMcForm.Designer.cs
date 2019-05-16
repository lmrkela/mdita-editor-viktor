using mDitaEditor.CustomControls;

namespace mDitaEditor.Lams.Forms
{
    partial class AssessmentMcForm
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
            this.dodajButton = new System.Windows.Forms.Button();
            this.panelOdgovori = new System.Windows.Forms.FlowLayoutPanel();
            this.boxShuffle = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPitanje = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtPitanjeText = new CueTextBox();
            this.txtPitanjeNaziv = new CueTextBox();
            this.dodajSlikuPitanje = new System.Windows.Forms.Button();
            this.pictureBoxPitanje = new System.Windows.Forms.PictureBox();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPitanje)).BeginInit();
            this.SuspendLayout();
            // 
            // dodajButton
            // 
            this.dodajButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dodajButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dodajButton.Location = new System.Drawing.Point(576, 225);
            this.dodajButton.Name = "dodajButton";
            this.dodajButton.Size = new System.Drawing.Size(87, 23);
            this.dodajButton.TabIndex = 74;
            this.dodajButton.Text = "Dodaj odgovor";
            this.dodajButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dodajButton.UseVisualStyleBackColor = true;
            this.dodajButton.Click += new System.EventHandler(this.btnAddAnswer_Click);
            // 
            // panelOdgovori
            // 
            this.panelOdgovori.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOdgovori.AutoScroll = true;
            this.panelOdgovori.Location = new System.Drawing.Point(12, 254);
            this.panelOdgovori.Name = "panelOdgovori";
            this.panelOdgovori.Size = new System.Drawing.Size(651, 210);
            this.panelOdgovori.TabIndex = 72;
            // 
            // boxShuffle
            // 
            this.boxShuffle.AutoSize = true;
            this.boxShuffle.Location = new System.Drawing.Point(12, 229);
            this.boxShuffle.Name = "boxShuffle";
            this.boxShuffle.Size = new System.Drawing.Size(164, 17);
            this.boxShuffle.TabIndex = 71;
            this.boxShuffle.Text = "Slučajan redosled odgovora?";
            this.boxShuffle.UseVisualStyleBackColor = true;
            this.boxShuffle.CheckedChanged += new System.EventHandler(this.boxShuffle_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 65;
            this.label2.Text = "Tekst:";
            // 
            // labelPitanje
            // 
            this.labelPitanje.AutoSize = true;
            this.labelPitanje.Location = new System.Drawing.Point(12, 9);
            this.labelPitanje.Name = "labelPitanje";
            this.labelPitanje.Size = new System.Drawing.Size(42, 13);
            this.labelPitanje.TabIndex = 63;
            this.labelPitanje.Text = "Pitanje:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(588, 470);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 82;
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
            this.txtPitanjeText.Location = new System.Drawing.Point(12, 65);
            this.txtPitanjeText.Name = "txtPitanjeText";
            this.txtPitanjeText.Size = new System.Drawing.Size(651, 20);
            this.txtPitanjeText.TabIndex = 73;
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
            this.txtPitanjeNaziv.Location = new System.Drawing.Point(12, 26);
            this.txtPitanjeNaziv.Name = "txtPitanjeNaziv";
            this.txtPitanjeNaziv.Size = new System.Drawing.Size(651, 20);
            this.txtPitanjeNaziv.TabIndex = 64;
            this.txtPitanjeNaziv.TextChanged += new System.EventHandler(this.txtPitanjeNaziv_TextChanged);
            // 
            // dodajSlikuPitanje
            // 
            this.dodajSlikuPitanje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dodajSlikuPitanje.Location = new System.Drawing.Point(202, 171);
            this.dodajSlikuPitanje.Name = "dodajSlikuPitanje";
            this.dodajSlikuPitanje.Size = new System.Drawing.Size(69, 23);
            this.dodajSlikuPitanje.TabIndex = 84;
            this.dodajSlikuPitanje.Text = "Dodaj sliku";
            this.dodajSlikuPitanje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dodajSlikuPitanje.UseVisualStyleBackColor = true;
            this.dodajSlikuPitanje.Click += new System.EventHandler(this.dodajSlikuPitanje_Click);
            // 
            // pictureBoxPitanje
            // 
            this.pictureBoxPitanje.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBoxPitanje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPitanje.Location = new System.Drawing.Point(12, 91);
            this.pictureBoxPitanje.Name = "pictureBoxPitanje";
            this.pictureBoxPitanje.Size = new System.Drawing.Size(184, 103);
            this.pictureBoxPitanje.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPitanje.TabIndex = 85;
            this.pictureBoxPitanje.TabStop = false;
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(12, 200);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(651, 19);
            this.progressBarUpload.TabIndex = 86;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.Enabled = false;
            this.vScrollBar.Location = new System.Drawing.Point(644, 254);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 210);
            this.vScrollBar.TabIndex = 87;
            // 
            // AssessmentMCQuestionControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 505);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.progressBarUpload);
            this.Controls.Add(this.pictureBoxPitanje);
            this.Controls.Add(this.dodajSlikuPitanje);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dodajButton);
            this.Controls.Add(this.txtPitanjeText);
            this.Controls.Add(this.txtPitanjeNaziv);
            this.Controls.Add(this.panelOdgovori);
            this.Controls.Add(this.boxShuffle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPitanje);
            this.Name = "AssessmentMCQuestionControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssessmentMCQuestionControlForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssessmentMCQuestionControlForm_FormClosing);
            this.Load += new System.EventHandler(this.AssessmentMCQuestionControlForm_Load);
            this.Resize += new System.EventHandler(this.AssessmentMCQuestionControlForm_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPitanje)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button dodajButton;
        private CueTextBox txtPitanjeText;
        public CueTextBox txtPitanjeNaziv;
     //   private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.FlowLayoutPanel panelOdgovori;
        private System.Windows.Forms.CheckBox boxShuffle;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label labelPitanje;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button dodajSlikuPitanje;
        public System.Windows.Forms.PictureBox pictureBoxPitanje;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}