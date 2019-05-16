namespace mDitaEditor.Dita.Controls
{
    partial class SlideListControl
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
            // SlideListControl
            // 
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Size = new System.Drawing.Size(261, 150);
            this.WrapContents = false;
            this.VisibleChanged += new System.EventHandler(this.SlideListControl_VisibleChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SlideListControl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SlideListControl_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.SlideListControl_DragOver);
            this.DragLeave += new System.EventHandler(this.SlideListControl_DragLeave);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
