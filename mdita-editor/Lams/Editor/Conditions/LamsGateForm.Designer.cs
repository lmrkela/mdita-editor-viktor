namespace mDitaEditor.Lams.Editor.Conditions
{
    partial class LamsGateForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.cmbInputTool = new System.Windows.Forms.ComboBox();
            this.lvConditions = new System.Windows.Forms.ListView();
            this.headerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.headerValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.cmbInputType = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbEndValue = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chbStartValue = new System.Windows.Forms.CheckBox();
            this.numStartValue = new System.Windows.Forms.NumericUpDown();
            this.numEndValue = new System.Windows.Forms.NumericUpDown();
            this.panNumerical = new System.Windows.Forms.TableLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.rdbOpen = new System.Windows.Forms.RadioButton();
            this.rdbClosed = new System.Windows.Forms.RadioButton();
            this.grbGateOptions = new System.Windows.Forms.GroupBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txbName = new System.Windows.Forms.TextBox();
            this.labEmpty = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndValue)).BeginInit();
            this.panNumerical.SuspendLayout();
            this.grbGateOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Input tool:";
            // 
            // cmbInputTool
            // 
            this.cmbInputTool.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInputTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputTool.FormattingEnabled = true;
            this.cmbInputTool.Location = new System.Drawing.Point(75, 38);
            this.cmbInputTool.Name = "cmbInputTool";
            this.cmbInputTool.Size = new System.Drawing.Size(310, 21);
            this.cmbInputTool.TabIndex = 2;
            this.cmbInputTool.SelectedIndexChanged += new System.EventHandler(this.cmbInputTool_SelectedIndexChanged);
            // 
            // lvConditions
            // 
            this.lvConditions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvConditions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.headerName,
            this.headerValue});
            this.lvConditions.Location = new System.Drawing.Point(9, 115);
            this.lvConditions.Margin = new System.Windows.Forms.Padding(0);
            this.lvConditions.MultiSelect = false;
            this.lvConditions.Name = "lvConditions";
            this.lvConditions.Size = new System.Drawing.Size(379, 235);
            this.lvConditions.TabIndex = 9;
            this.lvConditions.UseCompatibleStateImageBehavior = false;
            this.lvConditions.View = System.Windows.Forms.View.Details;
            this.lvConditions.SelectedIndexChanged += new System.EventHandler(this.lvConditions_SelectedIndexChanged);
            // 
            // headerName
            // 
            this.headerName.Text = "Condition";
            this.headerName.Width = 250;
            // 
            // headerValue
            // 
            this.headerValue.Text = "Gate value";
            this.headerValue.Width = 100;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Input type:";
            // 
            // cmbInputType
            // 
            this.cmbInputType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbInputType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInputType.FormattingEnabled = true;
            this.cmbInputType.Location = new System.Drawing.Point(75, 65);
            this.cmbInputType.Name = "cmbInputType";
            this.cmbInputType.Size = new System.Drawing.Size(310, 21);
            this.cmbInputType.TabIndex = 3;
            this.cmbInputType.SelectedIndexChanged += new System.EventHandler(this.cmbInputType_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chbEndValue);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chbStartValue);
            this.panel1.Location = new System.Drawing.Point(83, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 20);
            this.panel1.TabIndex = 8;
            // 
            // chbEndValue
            // 
            this.chbEndValue.AutoSize = true;
            this.chbEndValue.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chbEndValue.Checked = true;
            this.chbEndValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbEndValue.Location = new System.Drawing.Point(89, 1);
            this.chbEndValue.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.chbEndValue.Name = "chbEndValue";
            this.chbEndValue.Size = new System.Drawing.Size(38, 17);
            this.chbEndValue.TabIndex = 6;
            this.chbEndValue.Text = "<=";
            this.chbEndValue.UseVisualStyleBackColor = true;
            this.chbEndValue.CheckedChanged += new System.EventHandler(this.chbGreaterThan_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(41, -2);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "VALUE";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chbStartValue
            // 
            this.chbStartValue.AutoSize = true;
            this.chbStartValue.Checked = true;
            this.chbStartValue.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbStartValue.Location = new System.Drawing.Point(3, 1);
            this.chbStartValue.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.chbStartValue.Name = "chbStartValue";
            this.chbStartValue.Size = new System.Drawing.Size(38, 17);
            this.chbStartValue.TabIndex = 5;
            this.chbStartValue.Text = "<=";
            this.chbStartValue.UseVisualStyleBackColor = true;
            this.chbStartValue.CheckedChanged += new System.EventHandler(this.chbLessThan_CheckedChanged);
            // 
            // numStartValue
            // 
            this.numStartValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numStartValue.Location = new System.Drawing.Point(0, 0);
            this.numStartValue.Margin = new System.Windows.Forms.Padding(0);
            this.numStartValue.Name = "numStartValue";
            this.numStartValue.Size = new System.Drawing.Size(83, 20);
            this.numStartValue.TabIndex = 4;
            // 
            // numEndValue
            // 
            this.numEndValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.numEndValue.Location = new System.Drawing.Point(213, 0);
            this.numEndValue.Margin = new System.Windows.Forms.Padding(0);
            this.numEndValue.Name = "numEndValue";
            this.numEndValue.Size = new System.Drawing.Size(83, 20);
            this.numEndValue.TabIndex = 7;
            // 
            // panNumerical
            // 
            this.panNumerical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panNumerical.ColumnCount = 3;
            this.panNumerical.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panNumerical.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.panNumerical.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.panNumerical.Controls.Add(this.numStartValue, 0, 0);
            this.panNumerical.Controls.Add(this.panel1, 1, 0);
            this.panNumerical.Controls.Add(this.numEndValue, 2, 0);
            this.panNumerical.Location = new System.Drawing.Point(12, 92);
            this.panNumerical.Margin = new System.Windows.Forms.Padding(0);
            this.panNumerical.Name = "panNumerical";
            this.panNumerical.RowCount = 1;
            this.panNumerical.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panNumerical.Size = new System.Drawing.Size(296, 20);
            this.panNumerical.TabIndex = 9;
            this.panNumerical.Visible = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Enabled = false;
            this.btnAdd.Location = new System.Drawing.Point(311, 89);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // rdbOpen
            // 
            this.rdbOpen.AutoSize = true;
            this.rdbOpen.Location = new System.Drawing.Point(9, 19);
            this.rdbOpen.Name = "rdbOpen";
            this.rdbOpen.Size = new System.Drawing.Size(51, 17);
            this.rdbOpen.TabIndex = 10;
            this.rdbOpen.TabStop = true;
            this.rdbOpen.Text = "Open";
            this.rdbOpen.UseVisualStyleBackColor = true;
            this.rdbOpen.CheckedChanged += new System.EventHandler(this.rdbOpen_CheckedChanged);
            // 
            // rdbClosed
            // 
            this.rdbClosed.AutoSize = true;
            this.rdbClosed.Location = new System.Drawing.Point(66, 19);
            this.rdbClosed.Name = "rdbClosed";
            this.rdbClosed.Size = new System.Drawing.Size(57, 17);
            this.rdbClosed.TabIndex = 11;
            this.rdbClosed.TabStop = true;
            this.rdbClosed.Text = "Closed";
            this.rdbClosed.UseVisualStyleBackColor = true;
            this.rdbClosed.CheckedChanged += new System.EventHandler(this.rdbOpen_CheckedChanged);
            // 
            // grbGateOptions
            // 
            this.grbGateOptions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbGateOptions.Controls.Add(this.btnRemove);
            this.grbGateOptions.Controls.Add(this.rdbOpen);
            this.grbGateOptions.Controls.Add(this.rdbClosed);
            this.grbGateOptions.Enabled = false;
            this.grbGateOptions.Location = new System.Drawing.Point(9, 353);
            this.grbGateOptions.Name = "grbGateOptions";
            this.grbGateOptions.Size = new System.Drawing.Size(379, 44);
            this.grbGateOptions.TabIndex = 14;
            this.grbGateOptions.TabStop = false;
            this.grbGateOptions.Text = "Gate state";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(298, 16);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 12;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Name:";
            // 
            // txbName
            // 
            this.txbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbName.Location = new System.Drawing.Point(75, 12);
            this.txbName.Name = "txbName";
            this.txbName.Size = new System.Drawing.Size(310, 20);
            this.txbName.TabIndex = 1;
            this.txbName.TextChanged += new System.EventHandler(this.txbName_TextChanged);
            // 
            // labEmpty
            // 
            this.labEmpty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labEmpty.Location = new System.Drawing.Point(9, 62);
            this.labEmpty.Name = "labEmpty";
            this.labEmpty.Size = new System.Drawing.Size(379, 50);
            this.labEmpty.TabIndex = 17;
            this.labEmpty.Text = "Nema nijednog odgovarajućeg ulaznog Tool-a.\r\nProverite da li je Gate povezan sa d" +
    "rugim objektima.";
            this.labEmpty.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labEmpty.Visible = false;
            // 
            // LamsGateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 409);
            this.Controls.Add(this.labEmpty);
            this.Controls.Add(this.txbName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.grbGateOptions);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panNumerical);
            this.Controls.Add(this.cmbInputType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbInputTool);
            this.Controls.Add(this.lvConditions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LamsGateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "LAMS Gate Properties";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numStartValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numEndValue)).EndInit();
            this.panNumerical.ResumeLayout(false);
            this.grbGateOptions.ResumeLayout(false);
            this.grbGateOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbInputTool;
        private System.Windows.Forms.ListView lvConditions;
        private System.Windows.Forms.ColumnHeader headerName;
        private System.Windows.Forms.ColumnHeader headerValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbInputType;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox chbEndValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chbStartValue;
        private System.Windows.Forms.NumericUpDown numEndValue;
        private System.Windows.Forms.NumericUpDown numStartValue;
        private System.Windows.Forms.TableLayoutPanel panNumerical;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.RadioButton rdbOpen;
        private System.Windows.Forms.RadioButton rdbClosed;
        private System.Windows.Forms.GroupBox grbGateOptions;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txbName;
        private System.Windows.Forms.Label labEmpty;
    }
}