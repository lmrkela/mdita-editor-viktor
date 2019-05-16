using mDitaEditor.CustomControls;

namespace mDitaEditor.Dita.Controls
{
    partial class LearningSectionControl
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
            this.tableColumns = new System.Windows.Forms.TableLayoutPanel();
            this.tabPanel = new System.Windows.Forms.TableLayoutPanel();
            this.txbNaslov = new mDitaEditor.CustomControls.CueTextBox();
            this.txbCilj = new mDitaEditor.CustomControls.CueTextBox();
            this.tabPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableColumns
            // 
            this.tableColumns.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableColumns.ColumnCount = 6;
            this.tableColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableColumns.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableColumns.Location = new System.Drawing.Point(0, 101);
            this.tableColumns.Margin = new System.Windows.Forms.Padding(0);
            this.tableColumns.Name = "tableColumns";
            this.tableColumns.RowCount = 1;
            this.tableColumns.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableColumns.Size = new System.Drawing.Size(845, 461);
            this.tableColumns.TabIndex = 2;
            // 
            // tabPanel
            // 
            this.tabPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabPanel.ColumnCount = 1;
            this.tabPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabPanel.Controls.Add(this.txbNaslov, 0, 0);
            this.tabPanel.Controls.Add(this.tableColumns, 0, 2);
            this.tabPanel.Controls.Add(this.txbCilj, 0, 1);
            this.tabPanel.Location = new System.Drawing.Point(0, 0);
            this.tabPanel.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.tabPanel.Name = "tabPanel";
            this.tabPanel.RowCount = 3;
            this.tabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tabPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 13F));
            this.tabPanel.Size = new System.Drawing.Size(845, 562);
            this.tabPanel.TabIndex = 3;
            // 
            // txbNaslov
            // 
            this.txbNaslov.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbNaslov.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbNaslov.Cue = "Naslov sekcije";
            this.txbNaslov.CueColor = System.Drawing.Color.Gray;
            this.txbNaslov.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbNaslov.ForeColor = System.Drawing.Color.Black;
            this.txbNaslov.Location = new System.Drawing.Point(2, 1);
            this.txbNaslov.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.txbNaslov.MaxLength = 200;
            this.txbNaslov.Multiline = true;
            this.txbNaslov.Name = "txbNaslov";
            this.txbNaslov.OldText = null;
            this.txbNaslov.Size = new System.Drawing.Size(841, 37);
            this.txbNaslov.TabIndex = 0;
            this.txbNaslov.TextChanged += new System.EventHandler(this.txbNaslov_TextChanged);
            this.txbNaslov.Enter += new System.EventHandler(this.txbNaslov_Enter);
            this.txbNaslov.Leave += new System.EventHandler(this.txbNaslov_Leave);
            this.txbNaslov.LostFocus += new System.EventHandler(this.txbNaslov_Validated);
            // 
            // txbCilj
            // 
            this.txbCilj.AcceptsReturn = true;
            this.txbCilj.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbCilj.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbCilj.Cue = "Poenta sekcije";
            this.txbCilj.CueColor = System.Drawing.Color.Gray;
            this.txbCilj.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbCilj.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(6)))), ((int)(((byte)(52)))));
            this.txbCilj.Location = new System.Drawing.Point(2, 53);
            this.txbCilj.Margin = new System.Windows.Forms.Padding(2, 14, 2, 19);
            this.txbCilj.MaxLength = 200;
            this.txbCilj.Multiline = true;
            this.txbCilj.Name = "txbCilj";
            this.txbCilj.OldText = null;
            this.txbCilj.Size = new System.Drawing.Size(841, 29);
            this.txbCilj.TabIndex = 1;
            this.txbCilj.TextChanged += new System.EventHandler(this.txbCilj_TextChanged);
            this.txbCilj.Enter += new System.EventHandler(this.txbCilj_Enter);
            this.txbCilj.Leave += new System.EventHandler(this.txbNaslov_Leave);
            this.txbCilj.LostFocus += new System.EventHandler(this.txbCilj_Validated);
            // 
            // LearningSectionControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabPanel);
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "LearningSectionControl";
            this.Size = new System.Drawing.Size(845, 562);
            this.tabPanel.ResumeLayout(false);
            this.tabPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableColumns;
        private System.Windows.Forms.TableLayoutPanel tabPanel;
        public CueTextBox txbNaslov;
        public CueTextBox txbCilj;
    }
}
