namespace mDitaEditor.CustomControls
{
    partial class WordImportPanel
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
            this.tablControl = new System.Windows.Forms.TabControl();
            this.tabParagraphs = new System.Windows.Forms.TabPage();
            this.flowParagraphs = new System.Windows.Forms.FlowLayoutPanel();
            this.tabImages = new System.Windows.Forms.TabPage();
            this.flowImages = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.lblFileOpened = new System.Windows.Forms.Label();
            this.btnAddText = new System.Windows.Forms.Button();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.lblPages = new System.Windows.Forms.Label();
            this.picLoading = new System.Windows.Forms.PictureBox();
            this.tablControl.SuspendLayout();
            this.tabParagraphs.SuspendLayout();
            this.tabImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // tablControl
            // 
            this.tablControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tablControl.Controls.Add(this.tabParagraphs);
            this.tablControl.Controls.Add(this.tabImages);
            this.tablControl.Location = new System.Drawing.Point(3, 54);
            this.tablControl.Name = "tablControl";
            this.tablControl.SelectedIndex = 0;
            this.tablControl.Size = new System.Drawing.Size(188, 773);
            this.tablControl.TabIndex = 0;
            this.tablControl.SelectedIndexChanged += new System.EventHandler(this.tablControl_SelectedIndexChanged);
            this.tablControl.TabIndexChanged += new System.EventHandler(this.tablControl_TabIndexChanged);
            // 
            // tabParagraphs
            // 
            this.tabParagraphs.Controls.Add(this.lblPages);
            this.tabParagraphs.Controls.Add(this.btnNext);
            this.tabParagraphs.Controls.Add(this.btnPrevious);
            this.tabParagraphs.Controls.Add(this.flowParagraphs);
            this.tabParagraphs.Location = new System.Drawing.Point(4, 22);
            this.tabParagraphs.Name = "tabParagraphs";
            this.tabParagraphs.Padding = new System.Windows.Forms.Padding(3);
            this.tabParagraphs.Size = new System.Drawing.Size(180, 747);
            this.tabParagraphs.TabIndex = 0;
            this.tabParagraphs.Text = "Paragraphs";
            this.tabParagraphs.UseVisualStyleBackColor = true;
            // 
            // flowParagraphs
            // 
            this.flowParagraphs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowParagraphs.AutoScroll = true;
            this.flowParagraphs.Location = new System.Drawing.Point(0, 0);
            this.flowParagraphs.Name = "flowParagraphs";
            this.flowParagraphs.Size = new System.Drawing.Size(180, 697);
            this.flowParagraphs.TabIndex = 3;
            // 
            // tabImages
            // 
            this.tabImages.Controls.Add(this.flowImages);
            this.tabImages.Location = new System.Drawing.Point(4, 22);
            this.tabImages.Name = "tabImages";
            this.tabImages.Padding = new System.Windows.Forms.Padding(3);
            this.tabImages.Size = new System.Drawing.Size(233, 747);
            this.tabImages.TabIndex = 1;
            this.tabImages.Text = "Images";
            this.tabImages.UseVisualStyleBackColor = true;
            // 
            // flowImages
            // 
            this.flowImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowImages.AutoScroll = true;
            this.flowImages.Location = new System.Drawing.Point(3, 3);
            this.flowImages.Name = "flowImages";
            this.flowImages.Size = new System.Drawing.Size(230, 744);
            this.flowImages.TabIndex = 4;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 15);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Open ";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblFileOpened
            // 
            this.lblFileOpened.AutoSize = true;
            this.lblFileOpened.Location = new System.Drawing.Point(88, 20);
            this.lblFileOpened.Name = "lblFileOpened";
            this.lblFileOpened.Size = new System.Drawing.Size(0, 13);
            this.lblFileOpened.TabIndex = 2;
            // 
            // btnAddText
            // 
            this.btnAddText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddText.Location = new System.Drawing.Point(114, 46);
            this.btnAddText.Name = "btnAddText";
            this.btnAddText.Size = new System.Drawing.Size(75, 23);
            this.btnAddText.TabIndex = 3;
            this.btnAddText.Text = "Add text";
            this.btnAddText.UseVisualStyleBackColor = true;
            this.btnAddText.Click += new System.EventHandler(this.btnAddText_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrevious.Location = new System.Drawing.Point(3, 724);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(75, 23);
            this.btnPrevious.TabIndex = 4;
            this.btnPrevious.Text = "<---";
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Location = new System.Drawing.Point(102, 724);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 5;
            this.btnNext.Text = "--->";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // lblPages
            // 
            this.lblPages.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPages.AutoSize = true;
            this.lblPages.Location = new System.Drawing.Point(71, 705);
            this.lblPages.Name = "lblPages";
            this.lblPages.Size = new System.Drawing.Size(0, 13);
            this.lblPages.TabIndex = 6;
            // 
            // picLoading
            // 
            this.picLoading.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picLoading.Image = global::mDitaEditor.Properties.Resources.ajax_load;
            this.picLoading.Location = new System.Drawing.Point(2, 47);
            this.picLoading.Name = "picLoading";
            this.picLoading.Size = new System.Drawing.Size(190, 780);
            this.picLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLoading.TabIndex = 15;
            this.picLoading.TabStop = false;
            this.picLoading.Visible = false;
            // 
            // WordImportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.picLoading);
            this.Controls.Add(this.btnAddText);
            this.Controls.Add(this.lblFileOpened);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tablControl);
            this.Name = "WordImportPanel";
            this.Size = new System.Drawing.Size(194, 827);
            this.Load += new System.EventHandler(this.WordImport_Load);
            this.tablControl.ResumeLayout(false);
            this.tabParagraphs.ResumeLayout(false);
            this.tabParagraphs.PerformLayout();
            this.tabImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tablControl;
        private System.Windows.Forms.TabPage tabParagraphs;
        private System.Windows.Forms.TabPage tabImages;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblFileOpened;
        private System.Windows.Forms.FlowLayoutPanel flowParagraphs;
        private System.Windows.Forms.FlowLayoutPanel flowImages;
        private System.Windows.Forms.Button btnAddText;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Label lblPages;
        private System.Windows.Forms.PictureBox picLoading;
    }
}
