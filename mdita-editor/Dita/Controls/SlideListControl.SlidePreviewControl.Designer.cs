using System.Windows.Forms;

namespace mDitaEditor.Dita.Controls
{
    partial class SlideListControl
    {
        partial class SlidePreviewControl
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
                ((System.ComponentModel.ISupportInitialize) (this)).BeginInit();
                this.SuspendLayout();
                // 
                // SlidePreviewControl
                // 
                this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                this.Size = new System.Drawing.Size(240, 136);
                this.Margin = new System.Windows.Forms.Padding(2);
                this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
                this.Click += new System.EventHandler(this.SlidePreviewControl_Click);
                ((System.ComponentModel.ISupportInitialize) (this)).EndInit();
                this.MouseEnter += SlidePreviewControl_MouseEnter;
                this.MouseLeave += SlidePreviewControl_MouseLeave;
                this.MouseDown += SlidePreviewControl_MouseDown;
                this.MouseMove += SlidePreviewControl_MouseMove;
                this.MouseUp += SlidePreviewControl_MouseUp;
                Disposed += SlidePreviewControl_Disposed;
                Paint += SlidePreviewControl_Paint;
                this.Anchor = AnchorStyles.None;
                this.ResumeLayout(false);
            }

            #endregion
        }
    }
}
