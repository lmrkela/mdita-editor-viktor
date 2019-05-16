namespace mDitaEditor.Lams.Editor
{
    public partial class GrafikaListControl
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
            this.SuspendLayout();
            // 
            // GrafikaListControl
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Size = new System.Drawing.Size(261, 150);
            this.WrapContents = false;
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.GrafikaListControl_Layout);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
