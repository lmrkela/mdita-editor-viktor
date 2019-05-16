namespace mDitaEditor.Lams.Controls
{
    partial class ImageGalleryControl
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
            this.izaberiBtn = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOpis = new mDitaEditor.CustomControls.CueTextBox();
            this.txtUrl = new mDitaEditor.CustomControls.CueTextBox();
            this.txtTema = new mDitaEditor.CustomControls.CueTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Naslov slike:";
            // 
            // izaberiBtn
            // 
            this.izaberiBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.izaberiBtn.Location = new System.Drawing.Point(5, 220);
            this.izaberiBtn.Name = "izaberiBtn";
            this.izaberiBtn.Size = new System.Drawing.Size(75, 23);
            this.izaberiBtn.TabIndex = 43;
            this.izaberiBtn.Text = "Izaberi Sliku";
            this.izaberiBtn.UseVisualStyleBackColor = true;
            this.izaberiBtn.Click += new System.EventHandler(this.izaberiBtn_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Image = global::mDitaEditor.Properties.Resources.arrowdown;
            this.btnDown.Location = new System.Drawing.Point(626, 115);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 41;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = global::mDitaEditor.Properties.Resources.arrowup;
            this.btnUp.Location = new System.Drawing.Point(626, 78);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 32);
            this.btnUp.TabIndex = 40;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::mDitaEditor.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(626, 40);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 39;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(2, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "URL Slike:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Opis slike:";
            // 
            // txtOpis
            // 
            this.txtOpis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOpis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtOpis.Cue = null;
            this.txtOpis.CueColor = System.Drawing.Color.Gray;
            this.txtOpis.Location = new System.Drawing.Point(5, 105);
            this.txtOpis.Name = "txtOpis";
            this.txtOpis.OldText = null;
            this.txtOpis.Size = new System.Drawing.Size(618, 20);
            this.txtOpis.TabIndex = 46;
            this.txtOpis.TextChanged += new System.EventHandler(this.txtOpis_TextChanged);
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrl.Cue = null;
            this.txtUrl.CueColor = System.Drawing.Color.Gray;
            this.txtUrl.Enabled = false;
            this.txtUrl.Location = new System.Drawing.Point(5, 163);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.OldText = null;
            this.txtUrl.Size = new System.Drawing.Size(618, 20);
            this.txtUrl.TabIndex = 44;
            // 
            // txtTema
            // 
            this.txtTema.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTema.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTema.Cue = null;
            this.txtTema.CueColor = System.Drawing.Color.Gray;
            this.txtTema.Location = new System.Drawing.Point(5, 40);
            this.txtTema.Name = "txtTema";
            this.txtTema.OldText = null;
            this.txtTema.Size = new System.Drawing.Size(618, 20);
            this.txtTema.TabIndex = 23;
            // 
            // ImageGalleryControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOpis);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.izaberiBtn);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtTema);
            this.Name = "ImageGalleryControl";
            this.Size = new System.Drawing.Size(678, 250);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomControls.CueTextBox txtTema;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button izaberiBtn;
        public System.Windows.Forms.Button btnDown;
        public System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDelete;
        private CustomControls.CueTextBox txtUrl;
        private System.Windows.Forms.Label label2;
        private CustomControls.CueTextBox txtOpis;
        private System.Windows.Forms.Label label3;
    }
}
