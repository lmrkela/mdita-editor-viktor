using mDitaEditor.CustomControls;

namespace mDitaEditor.Lams.Controls
{
    partial class McQuestionAnswerControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddAnswer = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pitanjeTextBox = new CueTextBox();
            this.panelOdgovori = new FlowWithHackedScroll();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "Odgovori:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Pitanje:";
            // 
            // btnAddAnswer
            // 
            this.btnAddAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAnswer.Image = global::mDitaEditor.Properties.Resources.add;
            this.btnAddAnswer.Location = new System.Drawing.Point(151, 116);
            this.btnAddAnswer.Name = "btnAddAnswer";
            this.btnAddAnswer.Size = new System.Drawing.Size(32, 32);
            this.btnAddAnswer.TabIndex = 28;
            this.btnAddAnswer.UseVisualStyleBackColor = true;
            this.btnAddAnswer.Click += new System.EventHandler(this.btnAddAnswer_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Image = global::mDitaEditor.Properties.Resources.arrowdown;
            this.btnDown.Location = new System.Drawing.Point(151, 79);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 35;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = global::mDitaEditor.Properties.Resources.arrowup;
            this.btnUp.Location = new System.Drawing.Point(151, 41);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 32);
            this.btnUp.TabIndex = 34;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::mDitaEditor.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(151, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pitanjeTextBox
            // 
            this.pitanjeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pitanjeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pitanjeTextBox.Cue = null;
            this.pitanjeTextBox.CueColor = System.Drawing.Color.Gray;
            this.pitanjeTextBox.Location = new System.Drawing.Point(3, 19);
            this.pitanjeTextBox.Multiline = true;
            this.pitanjeTextBox.Name = "pitanjeTextBox";
            this.pitanjeTextBox.Size = new System.Drawing.Size(142, 25);
            this.pitanjeTextBox.TabIndex = 24;
            this.pitanjeTextBox.TextChanged += new System.EventHandler(this.PitanjeTextBox_TextChanged);
            // 
            // panelOdgovori
            // 
            this.panelOdgovori.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelOdgovori.AutoSize = true;
            this.panelOdgovori.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelOdgovori.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelOdgovori.Location = new System.Drawing.Point(6, 63);
            this.panelOdgovori.MinimumSize = new System.Drawing.Size(139, 85);
            this.panelOdgovori.Name = "panelOdgovori";
            this.panelOdgovori.Size = new System.Drawing.Size(139, 85);
            this.panelOdgovori.TabIndex = 25;
            this.panelOdgovori.WrapContents = false;
            // 
            // McQuestionAnswerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.Controls.Add(this.pitanjeTextBox);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddAnswer);
            this.Controls.Add(this.panelOdgovori);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.Name = "McQuestionAnswerControl";
            this.Size = new System.Drawing.Size(186, 151);
            this.SizeChanged += new System.EventHandler(this.MCQuestionAnswerControl_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddAnswer;
        private CueTextBox pitanjeTextBox;
        private FlowWithHackedScroll panelOdgovori;
        public System.Windows.Forms.Button btnDown;
        public System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnDelete;
    }
}
