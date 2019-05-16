using mDitaEditor.CustomControls;

namespace mDitaEditor.Dita.Controls
{
    //partial class GalleryControl
    //{
    public partial class GalleryImageControl
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
            this.components = new System.ComponentModel.Container();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txbFileName = new System.Windows.Forms.TextBox();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.toolTipBtnUp = new System.Windows.Forms.ToolTip(this.components);
            this.txbDescription = new CueTextBox();
            this.txbTitle = new CueTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnUp.Image = global::mDitaEditor.Properties.Resources.arrowup;
            this.btnUp.Location = new System.Drawing.Point(811, 54);
            this.btnUp.Margin = new System.Windows.Forms.Padding(2);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 32);
            this.btnUp.TabIndex = 3;
            this.btnUp.TabStop = false;
            this.toolTipBtnUp.SetToolTip(this.btnUp, "Pomeri gore");
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDown.Image = global::mDitaEditor.Properties.Resources.arrowdown;
            this.btnDown.Location = new System.Drawing.Point(811, 86);
            this.btnDown.Margin = new System.Windows.Forms.Padding(2);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 4;
            this.btnDown.TabStop = false;
            this.toolTipBtnUp.SetToolTip(this.btnDown, "Pomeri dole");
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnDelete.Image = global::mDitaEditor.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(811, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.TabStop = false;
            this.toolTipBtnUp.SetToolTip(this.btnDelete, "Obriši sliku");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txbFileName
            // 
            this.txbFileName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbFileName.Location = new System.Drawing.Point(166, 56);
            this.txbFileName.Name = "txbFileName";
            this.txbFileName.ReadOnly = true;
            this.txbFileName.Size = new System.Drawing.Size(639, 20);
            this.txbFileName.TabIndex = 8;
            this.txbFileName.TabStop = false;
            this.toolTipBtnUp.SetToolTip(this.txbFileName, "TitleDescription fajla");
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.picBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picBox.Location = new System.Drawing.Point(0, 0);
            this.picBox.Margin = new System.Windows.Forms.Padding(2);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(160, 120);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 0;
            this.picBox.TabStop = false;
            this.toolTipBtnUp.SetToolTip(this.picBox, "Slika");
            // 
            // txbDescription
            // 
            this.txbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbDescription.Cue = "Opis slike";
            this.txbDescription.CueColor = System.Drawing.Color.Gray;
            this.txbDescription.Location = new System.Drawing.Point(166, 31);
            this.txbDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txbDescription.Name = "txbDescription";
            this.txbDescription.Size = new System.Drawing.Size(639, 20);
            this.txbDescription.TabIndex = 2;
            this.toolTipBtnUp.SetToolTip(this.txbDescription, "Opis slike");
            // 
            // txbTitle
            // 
            this.txbTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbTitle.Cue = "Naslov slike";
            this.txbTitle.CueColor = System.Drawing.Color.Gray;
            this.txbTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbTitle.Location = new System.Drawing.Point(166, 8);
            this.txbTitle.Margin = new System.Windows.Forms.Padding(2);
            this.txbTitle.Name = "txbTitle";
            this.txbTitle.Size = new System.Drawing.Size(639, 19);
            this.txbTitle.TabIndex = 1;
            this.toolTipBtnUp.SetToolTip(this.txbTitle, "Naslov slike");
            this.txbTitle.Leave += new System.EventHandler(this.txbTitle_Leave);
            // 
            // GalleryImageControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.txbFileName);
            this.Controls.Add(this.txbDescription);
            this.Controls.Add(this.txbTitle);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.picBox);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.Name = "GalleryImageControl";
            this.Size = new System.Drawing.Size(845, 120);
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.PictureBox picBox;
            private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txbFileName;
        public System.Windows.Forms.Button btnUp;
        public System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.ToolTip toolTipBtnUp;
        private CueTextBox txbTitle;
        private CueTextBox txbDescription;
    }
    }
//}
