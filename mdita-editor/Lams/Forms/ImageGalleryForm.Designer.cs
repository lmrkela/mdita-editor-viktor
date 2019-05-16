namespace mDitaEditor.Lams.Forms
{
    partial class ImageGalleryForm
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
            this.naslovTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.instrukcijeTB = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.dodajButton = new System.Windows.Forms.Button();
            this.flowWithHackedScroll1 = new mDitaEditor.CustomControls.FlowWithHackedScroll();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.flowWithHackedScroll1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Naslov";
            // 
            // naslovTB
            // 
            this.naslovTB.Location = new System.Drawing.Point(12, 29);
            this.naslovTB.Name = "naslovTB";
            this.naslovTB.Size = new System.Drawing.Size(718, 20);
            this.naslovTB.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Instrukcije";
            // 
            // instrukcijeTB
            // 
            this.instrukcijeTB.Location = new System.Drawing.Point(12, 77);
            this.instrukcijeTB.Name = "instrukcijeTB";
            this.instrukcijeTB.Size = new System.Drawing.Size(718, 20);
            this.instrukcijeTB.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(655, 380);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Save";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dodajButton
            // 
            this.dodajButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dodajButton.Image = global::mDitaEditor.Properties.Resources.add;
            this.dodajButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dodajButton.Location = new System.Drawing.Point(533, 380);
            this.dodajButton.Name = "dodajButton";
            this.dodajButton.Size = new System.Drawing.Size(116, 23);
            this.dodajButton.TabIndex = 20;
            this.dodajButton.Text = "Dodaj Novu Sliku";
            this.dodajButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dodajButton.UseVisualStyleBackColor = true;
            this.dodajButton.Click += new System.EventHandler(this.dodajButton_Click);
            // 
            // flowWithHackedScroll1
            // 
            this.flowWithHackedScroll1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowWithHackedScroll1.AutoScroll = true;
            this.flowWithHackedScroll1.BackColor = System.Drawing.SystemColors.Control;
            this.flowWithHackedScroll1.Controls.Add(this.vScrollBar);
            this.flowWithHackedScroll1.Location = new System.Drawing.Point(12, 103);
            this.flowWithHackedScroll1.Name = "flowWithHackedScroll1";
            this.flowWithHackedScroll1.Size = new System.Drawing.Size(718, 270);
            this.flowWithHackedScroll1.TabIndex = 6;
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.Location = new System.Drawing.Point(0, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(20, 500);
            this.vScrollBar.TabIndex = 23;
            // 
            // ImageGalleryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 413);
            this.Controls.Add(this.dodajButton);
            this.Controls.Add(this.flowWithHackedScroll1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.instrukcijeTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.naslovTB);
            this.Controls.Add(this.label1);
            this.Name = "ImageGalleryForm";
            this.Text = "ImageGalleryForm";
            this.Resize += new System.EventHandler(this.ImageGalleryForm_Resize);
            this.flowWithHackedScroll1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox naslovTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox instrukcijeTB;
        private System.Windows.Forms.Button button2;
        private CustomControls.FlowWithHackedScroll flowWithHackedScroll1;
        private System.Windows.Forms.Button dodajButton;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}