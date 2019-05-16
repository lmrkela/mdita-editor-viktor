namespace mDitaEditor.Lams.Forms
{
    partial class AssessmentTfForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.labelPitanje = new System.Windows.Forms.Label();
            this.txtPitanjeNaziv = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPitanjeText = new System.Windows.Forms.TextBox();
            this.progressBarUpload = new System.Windows.Forms.ProgressBar();
            this.pictureBoxPitanje = new System.Windows.Forms.PictureBox();
            this.dodajSlikuPitanje = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPitanje)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(451, 263);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 97;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Tačno",
            "Netačno"});
            this.comboBox1.Location = new System.Drawing.Point(12, 265);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 89;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 249);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 88;
            this.label5.Text = "Tačan odgovor";
            // 
            // labelPitanje
            // 
            this.labelPitanje.AutoSize = true;
            this.labelPitanje.Location = new System.Drawing.Point(12, 9);
            this.labelPitanje.Name = "labelPitanje";
            this.labelPitanje.Size = new System.Drawing.Size(42, 13);
            this.labelPitanje.TabIndex = 83;
            this.labelPitanje.Text = "Pitanje:";
            this.labelPitanje.Click += new System.EventHandler(this.labelPitanje_Click);
            // 
            // txtPitanjeNaziv
            // 
            this.txtPitanjeNaziv.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPitanjeNaziv.Location = new System.Drawing.Point(12, 25);
            this.txtPitanjeNaziv.Name = "txtPitanjeNaziv";
            this.txtPitanjeNaziv.Size = new System.Drawing.Size(517, 20);
            this.txtPitanjeNaziv.TabIndex = 102;
            this.txtPitanjeNaziv.TextChanged += new System.EventHandler(this.txtPitanjeNaziv_TextChanged);
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
            // txtPitanjeText
            // 
            this.txtPitanjeText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPitanjeText.Location = new System.Drawing.Point(12, 64);
            this.txtPitanjeText.Name = "txtPitanjeText";
            this.txtPitanjeText.Size = new System.Drawing.Size(517, 20);
            this.txtPitanjeText.TabIndex = 103;
            this.txtPitanjeText.TextChanged += new System.EventHandler(this.txtPitanjeText_TextChanged);
            // 
            // progressBarUpload
            // 
            this.progressBarUpload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBarUpload.Location = new System.Drawing.Point(12, 233);
            this.progressBarUpload.Name = "progressBarUpload";
            this.progressBarUpload.Size = new System.Drawing.Size(514, 13);
            this.progressBarUpload.TabIndex = 106;
            // 
            // pictureBoxPitanje
            // 
            this.pictureBoxPitanje.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxPitanje.BackColor = System.Drawing.SystemColors.Info;
            this.pictureBoxPitanje.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxPitanje.Location = new System.Drawing.Point(12, 90);
            this.pictureBoxPitanje.Name = "pictureBoxPitanje";
            this.pictureBoxPitanje.Size = new System.Drawing.Size(176, 137);
            this.pictureBoxPitanje.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxPitanje.TabIndex = 105;
            this.pictureBoxPitanje.TabStop = false;
            // 
            // dodajSlikuPitanje
            // 
            this.dodajSlikuPitanje.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dodajSlikuPitanje.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dodajSlikuPitanje.Location = new System.Drawing.Point(194, 204);
            this.dodajSlikuPitanje.Name = "dodajSlikuPitanje";
            this.dodajSlikuPitanje.Size = new System.Drawing.Size(69, 23);
            this.dodajSlikuPitanje.TabIndex = 104;
            this.dodajSlikuPitanje.Text = "Dodaj sliku";
            this.dodajSlikuPitanje.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dodajSlikuPitanje.UseVisualStyleBackColor = true;
            this.dodajSlikuPitanje.Click += new System.EventHandler(this.dodajSlikuPitanje_Click);
            // 
            // AssessmentTFQuestionControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(538, 298);
            this.Controls.Add(this.progressBarUpload);
            this.Controls.Add(this.pictureBoxPitanje);
            this.Controls.Add(this.dodajSlikuPitanje);
            this.Controls.Add(this.txtPitanjeText);
            this.Controls.Add(this.txtPitanjeNaziv);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelPitanje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "AssessmentTFQuestionControlForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AssessmentTFQuestionControlForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AssessmentTFQuestionControlForm_FormClosing);
            this.Load += new System.EventHandler(this.AssessmentTFQuestionControlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPitanje)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label labelPitanje;
        private System.Windows.Forms.TextBox txtPitanjeNaziv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPitanjeText;
        private System.Windows.Forms.ProgressBar progressBarUpload;
        public System.Windows.Forms.PictureBox pictureBoxPitanje;
        private System.Windows.Forms.Button dodajSlikuPitanje;
    }
}