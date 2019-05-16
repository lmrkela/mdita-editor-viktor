namespace mDitaEditor.Dita.Forms
{
    partial class RecordAudioForm
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
            this.progressPlay = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.PictureBox();
            this.brnRecord = new System.Windows.Forms.PictureBox();
            this.btnPlay = new System.Windows.Forms.PictureBox();
            this.btnTakeAudio = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.brnRecord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).BeginInit();
            this.SuspendLayout();
            // 
            // progressPlay
            // 
            this.progressPlay.Location = new System.Drawing.Point(25, 107);
            this.progressPlay.Name = "progressPlay";
            this.progressPlay.Size = new System.Drawing.Size(220, 23);
            this.progressPlay.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Recording";
            this.label1.Visible = false;
            // 
            // btnStop
            // 
            this.btnStop.Image = global::mDitaEditor.Properties.Resources.stop;
            this.btnStop.Location = new System.Drawing.Point(103, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(65, 66);
            this.btnStop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnStop.TabIndex = 16;
            this.btnStop.TabStop = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            this.btnStop.MouseEnter += new System.EventHandler(this.btnStop_MouseEnter);
            this.btnStop.MouseLeave += new System.EventHandler(this.btnStop_MouseLeave);
            // 
            // brnRecord
            // 
            this.brnRecord.Image = global::mDitaEditor.Properties.Resources.record;
            this.brnRecord.Location = new System.Drawing.Point(25, 12);
            this.brnRecord.Name = "brnRecord";
            this.brnRecord.Size = new System.Drawing.Size(65, 66);
            this.brnRecord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.brnRecord.TabIndex = 15;
            this.brnRecord.TabStop = false;
            this.brnRecord.Click += new System.EventHandler(this.brnRecord_Click);
            this.brnRecord.MouseEnter += new System.EventHandler(this.brnRecord_MouseEnter);
            this.brnRecord.MouseLeave += new System.EventHandler(this.brnRecord_MouseLeave);
            // 
            // btnPlay
            // 
            this.btnPlay.Image = global::mDitaEditor.Properties.Resources.play2;
            this.btnPlay.Location = new System.Drawing.Point(180, 12);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(65, 66);
            this.btnPlay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnPlay.TabIndex = 14;
            this.btnPlay.TabStop = false;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            this.btnPlay.MouseEnter += new System.EventHandler(this.btnPlay_MouseEnter);
            this.btnPlay.MouseLeave += new System.EventHandler(this.btnPlay_MouseLeave);
            // 
            // btnTakeAudio
            // 
            this.btnTakeAudio.Location = new System.Drawing.Point(151, 137);
            this.btnTakeAudio.Name = "btnTakeAudio";
            this.btnTakeAudio.Size = new System.Drawing.Size(94, 23);
            this.btnTakeAudio.TabIndex = 17;
            this.btnTakeAudio.Text = "Use recording";
            this.btnTakeAudio.UseVisualStyleBackColor = true;
            this.btnTakeAudio.Click += new System.EventHandler(this.btnTakeAudio_Click);
            // 
            // RecordAudio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 172);
            this.Controls.Add(this.btnTakeAudio);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.brnRecord);
            this.Controls.Add(this.btnPlay);
            this.Controls.Add(this.progressPlay);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordAudio";
            this.Text = "RecordAudio";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecordAudio_FormClosing);
            this.Load += new System.EventHandler(this.RecordAudio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnStop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.brnRecord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPlay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressPlay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox btnStop;
        private System.Windows.Forms.PictureBox brnRecord;
        private System.Windows.Forms.PictureBox btnPlay;
        private System.Windows.Forms.Button btnTakeAudio;
    }
}