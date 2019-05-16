﻿namespace mDitaEditor.CustomForms
{
    partial class CopyFilesForm
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
            this.btnOk = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.txtProgressComplete = new System.Windows.Forms.Label();
            this.backgroundWorker_Copy = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Copying files";
            // 
            // btnOk
            // 
            this.btnOk.Enabled = false;
            this.btnOk.Location = new System.Drawing.Point(277, 73);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 36);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(337, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // txtProgressComplete
            // 
            this.txtProgressComplete.AutoSize = true;
            this.txtProgressComplete.Location = new System.Drawing.Point(12, 78);
            this.txtProgressComplete.Name = "txtProgressComplete";
            this.txtProgressComplete.Size = new System.Drawing.Size(91, 13);
            this.txtProgressComplete.TabIndex = 3;
            this.txtProgressComplete.Text = "Copying complete";
            // 
            // backgroundWorker_Copy
            // 
            this.backgroundWorker_Copy.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_Copy_DoWork);
            // 
            // CopyFilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 108);
            this.Controls.Add(this.txtProgressComplete);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CopyFilesForm";
            this.Text = "Copying files";
            this.Load += new System.EventHandler(this.CopyFilesForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label txtProgressComplete;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Copy;
    }
}