namespace mDitaEditor.Lams.Controls
{
    partial class AssessmentEssayControl
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
            this.odgovorTextBox = new mDitaEditor.CustomControls.CueTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // odgovorTextBox
            // 
            this.odgovorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.odgovorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.odgovorTextBox.Cue = null;
            this.odgovorTextBox.CueColor = System.Drawing.Color.Gray;
            this.odgovorTextBox.Location = new System.Drawing.Point(16, 32);
            this.odgovorTextBox.Multiline = true;
            this.odgovorTextBox.Name = "odgovorTextBox";
            this.odgovorTextBox.OldText = null;
            this.odgovorTextBox.Size = new System.Drawing.Size(287, 119);
            this.odgovorTextBox.TabIndex = 91;
            this.odgovorTextBox.TextChanged += new System.EventHandler(this.odgovorTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 90;
            this.label1.Text = "Esej";
            // 
            // AssessmentEssayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.odgovorTextBox);
            this.Controls.Add(this.label1);
            this.Name = "AssessmentEssayControl";
            this.Size = new System.Drawing.Size(322, 174);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public CustomControls.CueTextBox odgovorTextBox;
        private System.Windows.Forms.Label label1;
    }
}
