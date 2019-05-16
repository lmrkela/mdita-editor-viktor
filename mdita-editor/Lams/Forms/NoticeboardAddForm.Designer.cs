using mDitaEditor.CustomControls;
using mDitaEditor.Lams.Controls;

namespace mDitaEditor.Lams.Forms
{
    partial class NoticeboardAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoticeboardAddForm));
            this.btnSave = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.instrukcijeTextBox = new Controls.WebEditorControl();
            this.naslovTextBox = new CueTextBox();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.AutoSize = true;
            this.btnSave.Location = new System.Drawing.Point(547, 279);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 99;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 98;
            this.label2.Text = "Sadrzaj";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 97;
            this.label1.Text = "Naslov:";
            // 
            // instrukcijeTextBox
            // 
            this.instrukcijeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instrukcijeTextBox.BodyBackgroundColor = System.Drawing.Color.White;
            this.instrukcijeTextBox.BodyHtml = null;
            this.instrukcijeTextBox.BodyText = null;
            this.instrukcijeTextBox.DocumentText = resources.GetString("instrukcijeTextBox.DocumentText");
            this.instrukcijeTextBox.EditorBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.instrukcijeTextBox.EditorForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.instrukcijeTextBox.FontSize = FontSize.Three;
            this.instrukcijeTextBox.Html = null;
            this.instrukcijeTextBox.Location = new System.Drawing.Point(12, 74);
            this.instrukcijeTextBox.Name = "instrukcijeTextBox";
            this.instrukcijeTextBox.Size = new System.Drawing.Size(610, 186);
            this.instrukcijeTextBox.TabIndex = 100;
            // 
            // naslovTextBox
            // 
            this.naslovTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.naslovTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.naslovTextBox.Cue = null;
            this.naslovTextBox.CueColor = System.Drawing.Color.Gray;
            this.naslovTextBox.Location = new System.Drawing.Point(12, 26);
            this.naslovTextBox.Name = "naslovTextBox";
            this.naslovTextBox.Size = new System.Drawing.Size(610, 20);
            this.naslovTextBox.TabIndex = 95;
            // 
            // NoticeboardAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(641, 320);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.instrukcijeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.naslovTextBox);
            this.Name = "NoticeboardAddForm";
            this.Text = "NoticeboardAddForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CueTextBox naslovTextBox;
        private Controls.WebEditorControl instrukcijeTextBox;
    }
}