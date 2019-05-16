namespace mDitaEditor.Dita.Forms
{
    partial class ChooseObjectForm
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
            this.btnChangeObjectToSubobject = new System.Windows.Forms.Button();
            this.cmbSelectObject = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // btnChangeObjectToSubobject
            // 
            this.btnChangeObjectToSubobject.Location = new System.Drawing.Point(56, 60);
            this.btnChangeObjectToSubobject.Name = "btnChangeObjectToSubobject";
            this.btnChangeObjectToSubobject.Size = new System.Drawing.Size(215, 23);
            this.btnChangeObjectToSubobject.TabIndex = 0;
            this.btnChangeObjectToSubobject.Text = "Convert to sub-object of selected object";
            this.btnChangeObjectToSubobject.UseVisualStyleBackColor = true;
            this.btnChangeObjectToSubobject.Click += new System.EventHandler(this.btnChangeObjectToSubobject_Click);
            // 
            // cmbSelectObject
            // 
            this.cmbSelectObject.FormattingEnabled = true;
            this.cmbSelectObject.Location = new System.Drawing.Point(25, 24);
            this.cmbSelectObject.Name = "cmbSelectObject";
            this.cmbSelectObject.Size = new System.Drawing.Size(269, 21);
            this.cmbSelectObject.TabIndex = 1;
            this.cmbSelectObject.SelectedIndexChanged += new System.EventHandler(this.cmbSelectObject_SelectedIndexChanged);
            // 
            // ChooseObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 104);
            this.Controls.Add(this.cmbSelectObject);
            this.Controls.Add(this.btnChangeObjectToSubobject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ChooseObject";
            this.Text = "Choose object that will contain selected object";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChooseObject_FormClosing);
            this.Load += new System.EventHandler(this.ChooseObject_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnChangeObjectToSubobject;
        private System.Windows.Forms.ComboBox cmbSelectObject;
    }
}