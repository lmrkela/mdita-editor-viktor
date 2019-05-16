namespace mDitaEditor.Lams.Editor
{
    public partial class GrafikaCanvas
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
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // GrafikaCanvas
            // 
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.SizeChanged += new System.EventHandler(this.GrafikaCanvas_SizeChanged);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.GrafikaCanvas_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.GrafikaCanvas_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.GrafikaCanvas_DragOver);
            this.DragLeave += new System.EventHandler(this.GrafikaCanvas_DragLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GrafikaCanvas_Paint);
            this.DoubleClick += new System.EventHandler(this.GrafikaCanvas_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.GrafikaCanvas_MouseDown);
            this.MouseEnter += new System.EventHandler(this.GrafikaCanvas_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.GrafikaCanvas_MouseLeave);
            this.MouseHover += new System.EventHandler(this.GrafikaCanvas_MouseHover);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.GrafikaCanvas_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.GrafikaCanvas_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
