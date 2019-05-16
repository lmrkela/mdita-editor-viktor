namespace mDitaEditor.Dita.Controls
{
    partial class GalleryControl
    {
        private void InitializeComponent()
        {
            this.panList = new System.Windows.Forms.FlowLayoutPanel();
            this.vScroll = new System.Windows.Forms.VScrollBar();
            this.labEmptyList = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panList
            // 
            this.panList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panList.AutoScroll = true;
            this.panList.BackColor = System.Drawing.SystemColors.Info;
            this.panList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panList.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panList.Location = new System.Drawing.Point(0, 36);
            this.panList.Name = "panList";
            this.panList.Size = new System.Drawing.Size(840, 426);
            this.panList.TabIndex = 0;
            this.panList.WrapContents = false;
            // 
            // vScroll
            // 
            this.vScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScroll.Enabled = false;
            this.vScroll.Location = new System.Drawing.Point(821, 37);
            this.vScroll.Name = "vScroll";
            this.vScroll.Size = new System.Drawing.Size(17, 424);
            this.vScroll.TabIndex = 0;
            // 
            // labEmptyList
            // 
            this.labEmptyList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labEmptyList.BackColor = System.Drawing.SystemColors.Info;
            this.labEmptyList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labEmptyList.Image = global::mDitaEditor.Properties.Resources.gallery;
            this.labEmptyList.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labEmptyList.Location = new System.Drawing.Point(230, 118);
            this.labEmptyList.Name = "labEmptyList";
            this.labEmptyList.Size = new System.Drawing.Size(400, 70);
            this.labEmptyList.TabIndex = 2;
            this.labEmptyList.Text = "Galerija je prazna.\r\nKliknite na dugme \'Dodaj sliku\' da ubacite novu sliku.";
            this.labEmptyList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Image = global::mDitaEditor.Properties.Resources.add;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(85, 36);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.TabStop = false;
            this.btnAdd.Text = "Dodaj sliku";
            this.btnAdd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // GalleryControl
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.labEmptyList);
            this.Controls.Add(this.vScroll);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panList);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GalleryControl";
            this.Size = new System.Drawing.Size(840, 462);
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.FlowLayoutPanel panList;
        private System.Windows.Forms.VScrollBar vScroll;
        private System.Windows.Forms.Label labEmptyList;
    }
}
