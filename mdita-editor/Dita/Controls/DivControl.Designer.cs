using mDitaEditor.CustomControls;

namespace mDitaEditor.Dita.Controls
{
    partial class DivControl
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.picMove = new System.Windows.Forms.PictureBox();
            this.panTransparent = new TransparentPanel();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMove)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripMenuItem1,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
            this.contextMenuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Table;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 142);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Image = global::mDitaEditor.Properties.Resources.arrowup;
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Image = global::mDitaEditor.Properties.Resources.arrowdown;
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.moveDownToolStripMenuItem.Text = "Move Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::mDitaEditor.Properties.Resources.delete;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(135, 6);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Image = global::mDitaEditor.Properties.Resources.cut;
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Image = global::mDitaEditor.Properties.Resources.copy;
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Image = global::mDitaEditor.Properties.Resources.paste;
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(138, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // picMove
            // 
            this.picMove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picMove.BackColor = System.Drawing.Color.Transparent;
            this.picMove.ContextMenuStrip = this.contextMenuStrip1;
            this.picMove.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.picMove.Image = global::mDitaEditor.Properties.Resources.move;
            this.picMove.Location = new System.Drawing.Point(25, 3);
            this.picMove.Name = "picMove";
            this.picMove.Size = new System.Drawing.Size(20, 20);
            this.picMove.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picMove.TabIndex = 0;
            this.picMove.TabStop = false;
            this.picMove.Visible = false;
            this.picMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DivControl_MouseDown);
            this.picMove.MouseEnter += new System.EventHandler(this.DivControl_MouseEnter);
            this.picMove.MouseLeave += new System.EventHandler(this.DivControl_MouseLeave);
            this.picMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DivControl_MouseMove);
            this.picMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DivControl_MouseUp);
            // 
            // panTransparent
            // 
            this.panTransparent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panTransparent.Location = new System.Drawing.Point(0, 0);
            this.panTransparent.Name = "panTransparent";
            this.panTransparent.Size = new System.Drawing.Size(48, 29);
            this.panTransparent.TabIndex = 1;
            this.panTransparent.DragLeave += new System.EventHandler(this.panTransparent_MouseLeave);
            this.panTransparent.MouseEnter += new System.EventHandler(this.DivControl_MouseEnter);
            this.panTransparent.MouseLeave += new System.EventHandler(this.DivControl_MouseLeave);
            // 
            // DivControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.picMove);
            this.Controls.Add(this.panTransparent);
            this.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.Name = "DivControl";
            this.Size = new System.Drawing.Size(48, 29);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.DivControl_Layout);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DivControl_MouseDown);
            this.MouseEnter += new System.EventHandler(this.DivControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DivControl_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DivControl_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DivControl_MouseUp);
            this.ParentChanged += new System.EventHandler(this.DivControl_ParentChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picMove)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picMove;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private TransparentPanel panTransparent;
    }
}
