using mDitaEditor.CustomControls;

namespace mDitaEditor.Lams.Controls
{
    partial class AssessmentNumericalControl
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
            this.odgovorTextBox = new CueTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 68;
            this.label1.Text = "Odgovor";
            // 
            // odgovorTextBox
            // 
            this.odgovorTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.odgovorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.odgovorTextBox.Cue = null;
            this.odgovorTextBox.CueColor = System.Drawing.Color.Gray;
            this.odgovorTextBox.Location = new System.Drawing.Point(57, 10);
            this.odgovorTextBox.Name = "odgovorTextBox";
            this.odgovorTextBox.Size = new System.Drawing.Size(305, 20);
            this.odgovorTextBox.TabIndex = 69;
            this.odgovorTextBox.TextChanged += new System.EventHandler(this.odgovorTextBox_TextChanged);
            // 
            // AssessmentNumericalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.odgovorTextBox);
            this.Controls.Add(this.label1);
            this.Name = "AssessmentNumericalControl";
            this.Size = new System.Drawing.Size(373, 43);
            this.Load += new System.EventHandler(this.AssessmentNumericalAnswerAddControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private CueTextBox odgovorTextBox;
    }
}
