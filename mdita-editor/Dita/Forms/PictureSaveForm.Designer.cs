namespace mDitaEditor.Dita.Forms
{
    partial class PictureSaveForm
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
            this.labelPictureSave = new System.Windows.Forms.Label();
            this.textBoxPictureSave = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelPictureSave
            // 
            this.labelPictureSave.AutoSize = true;
            this.labelPictureSave.Location = new System.Drawing.Point(12, 11);
            this.labelPictureSave.Name = "labelPictureSave";
            this.labelPictureSave.Size = new System.Drawing.Size(86, 13);
            this.labelPictureSave.TabIndex = 0;
            this.labelPictureSave.Text = "Save Picture As:";
            // 
            // textBoxPictureSave
            // 
            this.textBoxPictureSave.Location = new System.Drawing.Point(12, 27);
            this.textBoxPictureSave.Name = "textBoxPictureSave";
            this.textBoxPictureSave.Size = new System.Drawing.Size(344, 20);
            this.textBoxPictureSave.TabIndex = 1;
            this.textBoxPictureSave.TextChanged += new System.EventHandler(this.textBoxPictureSave_TextChanged);
            this.textBoxPictureSave.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxPictureSave_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(269, 53);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Picture";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // PictureSaveForm
            // 
            this.ClientSize = new System.Drawing.Size(364, 101);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.textBoxPictureSave);
            this.Controls.Add(this.labelPictureSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PictureSaveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PictureSave";
            this.Load += new System.EventHandler(this.PictureSave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPictureSave;
        private System.Windows.Forms.TextBox textBoxPictureSave;
        private System.Windows.Forms.Button btnSave;
    }
}