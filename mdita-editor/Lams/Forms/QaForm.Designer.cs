using System.Windows.Forms;
using mDitaEditor.CustomControls;
using mDitaEditor.Lams.Controls;

namespace mDitaEditor.Lams.Forms
{
    partial class QaForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.instrukcijeTextBox = new mDitaEditor.CustomControls.CueTextBox();
            this.naslovTextBox = new mDitaEditor.CustomControls.CueTextBox();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.dodajButton = new System.Windows.Forms.Button();
            this.panelListQA = new mDitaEditor.CustomControls.FlowWithHackedScroll();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Instrukcije:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Naslov:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(655, 227);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // instrukcijeTextBox
            // 
            this.instrukcijeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instrukcijeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.instrukcijeTextBox.Cue = null;
            this.instrukcijeTextBox.CueColor = System.Drawing.Color.Gray;
            this.instrukcijeTextBox.Location = new System.Drawing.Point(15, 65);
            this.instrukcijeTextBox.Name = "instrukcijeTextBox";
            this.instrukcijeTextBox.OldText = null;
            this.instrukcijeTextBox.Size = new System.Drawing.Size(715, 20);
            this.instrukcijeTextBox.TabIndex = 9;
            this.instrukcijeTextBox.TextChanged += new System.EventHandler(this.instrukcijeTextBox_TextChanged);
            // 
            // naslovTextBox
            // 
            this.naslovTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.naslovTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.naslovTextBox.Cue = null;
            this.naslovTextBox.CueColor = System.Drawing.Color.Gray;
            this.naslovTextBox.Location = new System.Drawing.Point(15, 26);
            this.naslovTextBox.Name = "naslovTextBox";
            this.naslovTextBox.OldText = null;
            this.naslovTextBox.Size = new System.Drawing.Size(715, 20);
            this.naslovTextBox.TabIndex = 8;
            this.naslovTextBox.TextChanged += new System.EventHandler(this.naslovTextBox_TextChanged);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.vScrollBar.Enabled = false;
            this.vScrollBar.Location = new System.Drawing.Point(713, 91);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 130);
            this.vScrollBar.TabIndex = 15;
            // 
            // dodajButton
            // 
            this.dodajButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.dodajButton.Image = global::mDitaEditor.Properties.Resources.add;
            this.dodajButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.dodajButton.Location = new System.Drawing.Point(551, 227);
            this.dodajButton.Name = "dodajButton";
            this.dodajButton.Size = new System.Drawing.Size(98, 23);
            this.dodajButton.TabIndex = 12;
            this.dodajButton.Text = "Dodaj pitanje";
            this.dodajButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.dodajButton.UseVisualStyleBackColor = true;
            this.dodajButton.Click += new System.EventHandler(this.dodajButton_Click);
            // 
            // panelListQA
            // 
            this.panelListQA.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelListQA.AutoScroll = true;
            this.panelListQA.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelListQA.Location = new System.Drawing.Point(15, 91);
            this.panelListQA.Margin = new System.Windows.Forms.Padding(2);
            this.panelListQA.Name = "panelListQA";
            this.panelListQA.Size = new System.Drawing.Size(715, 130);
            this.panelListQA.TabIndex = 13;
            this.panelListQA.WrapContents = false;
            this.panelListQA.SizeChanged += new System.EventHandler(this.panelListQA_SizeChanged);
            // 
            // QaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(739, 258);
            this.Controls.Add(this.vScrollBar);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.panelListQA);
            this.Controls.Add(this.dodajButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.instrukcijeTextBox);
            this.Controls.Add(this.naslovTextBox);
            this.Name = "QaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "QAControlForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button dodajButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CueTextBox instrukcijeTextBox;
        private CueTextBox naslovTextBox;
        private System.Windows.Forms.Button btnSave;
        private VScrollBar vScrollBar;
        private FlowWithHackedScroll panelListQA;
    }
}