namespace mDitaEditor.CustomForms
{
    partial class ObjectPreviewForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnImportObject = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(16, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 535);
            this.panel1.TabIndex = 0;
            // 
            // btnImportObject
            // 
            this.btnImportObject.Location = new System.Drawing.Point(821, 553);
            this.btnImportObject.Name = "btnImportObject";
            this.btnImportObject.Size = new System.Drawing.Size(75, 23);
            this.btnImportObject.TabIndex = 1;
            this.btnImportObject.Text = "Import";
            this.btnImportObject.UseVisualStyleBackColor = true;
            this.btnImportObject.Click += new System.EventHandler(this.btnImportObject_Click);
            // 
            // ObjectPreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 588);
            this.Controls.Add(this.btnImportObject);
            this.Controls.Add(this.panel1);
            this.Name = "ObjectPreviewForm";
            this.Text = "ObjectPreviewForm";
            this.Load += new System.EventHandler(this.ObjectPreviewForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnImportObject;
    }
}