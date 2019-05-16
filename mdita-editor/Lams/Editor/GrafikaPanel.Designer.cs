namespace mDitaEditor.Lams.Editor
{
    partial class GrafikaPanel
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
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelDelete = new System.Windows.Forms.PictureBox();
            this.labDelete = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // vScrollBar
            // 
            this.vScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.vScrollBar.Enabled = false;
            this.vScrollBar.Location = new System.Drawing.Point(181, 0);
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(17, 565);
            this.vScrollBar.TabIndex = 8;
            // 
            // toolTip
            // 
            this.toolTip.AutomaticDelay = 0;
            this.toolTip.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolTip.IsBalloon = true;
            this.toolTip.ToolTipTitle = "Dodavanje slajda";
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // panelDelete
            // 
            this.panelDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelDelete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panelDelete.Image = global::mDitaEditor.Properties.Resources.trash;
            this.panelDelete.Location = new System.Drawing.Point(0, 0);
            this.panelDelete.Margin = new System.Windows.Forms.Padding(0);
            this.panelDelete.Name = "panelDelete";
            this.panelDelete.Size = new System.Drawing.Size(181, 565);
            this.panelDelete.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.panelDelete.TabIndex = 9;
            this.panelDelete.TabStop = false;
            this.panelDelete.Visible = false;
            this.panelDelete.VisibleChanged += new System.EventHandler(this.panelDelete_VisibleChanged);
            // 
            // labDelete
            // 
            this.labDelete.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labDelete.BackColor = System.Drawing.SystemColors.Window;
            this.labDelete.Location = new System.Drawing.Point(3, 365);
            this.labDelete.Name = "labDelete";
            this.labDelete.Size = new System.Drawing.Size(175, 23);
            this.labDelete.TabIndex = 10;
            this.labDelete.Text = "Uklonite objekat sa platna.";
            this.labDelete.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labDelete.Visible = false;
            // 
            // GrafikaPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labDelete);
            this.Controls.Add(this.panelDelete);
            this.Controls.Add(this.vScrollBar);
            this.Name = "GrafikaPanel";
            this.Size = new System.Drawing.Size(900, 565);
            ((System.ComponentModel.ISupportInitialize)(this.panelDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.VScrollBar vScrollBar;
        public System.Windows.Forms.ToolTip toolTip;
        public System.Windows.Forms.PictureBox panelDelete;
        private System.Windows.Forms.Label labDelete;
    }
}
