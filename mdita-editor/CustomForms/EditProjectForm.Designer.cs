﻿namespace mDitaEditor.CustomForms
{
    partial class EditProjectForm
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
            this.btnUpdateProject = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txbNaslov = new System.Windows.Forms.TextBox();
            this.txbGodina = new System.Windows.Forms.TextBox();
            this.txbAutor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txbSifraPredmeta = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txbBrojLekcije = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUpdateProject
            // 
            this.btnUpdateProject.Location = new System.Drawing.Point(242, 151);
            this.btnUpdateProject.Name = "btnUpdateProject";
            this.btnUpdateProject.Size = new System.Drawing.Size(96, 23);
            this.btnUpdateProject.TabIndex = 0;
            this.btnUpdateProject.Text = "Update project";
            this.btnUpdateProject.UseVisualStyleBackColor = true;
            this.btnUpdateProject.Click += new System.EventHandler(this.btnUpdateProject_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Naslov lekcije:";
            // 
            // txbNaslov
            // 
            this.txbNaslov.Location = new System.Drawing.Point(116, 65);
            this.txbNaslov.Name = "txbNaslov";
            this.txbNaslov.Size = new System.Drawing.Size(222, 20);
            this.txbNaslov.TabIndex = 5;
            this.txbNaslov.Text = "Naziv Vaše lekcije";
            // 
            // txbGodina
            // 
            this.txbGodina.Location = new System.Drawing.Point(116, 89);
            this.txbGodina.Name = "txbGodina";
            this.txbGodina.Size = new System.Drawing.Size(222, 20);
            this.txbGodina.TabIndex = 7;
            this.txbGodina.Text = "2016/2017";
            // 
            // txbAutor
            // 
            this.txbAutor.Location = new System.Drawing.Point(116, 114);
            this.txbAutor.Name = "txbAutor";
            this.txbAutor.ReadOnly = true;
            this.txbAutor.Size = new System.Drawing.Size(222, 20);
            this.txbAutor.TabIndex = 8;
            this.txbAutor.Text = "Metropolitan";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Autor:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 92);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Školska godina:";
            // 
            // txbSifraPredmeta
            // 
            this.txbSifraPredmeta.FormattingEnabled = true;
            this.txbSifraPredmeta.Location = new System.Drawing.Point(116, 14);
            this.txbSifraPredmeta.Name = "txbSifraPredmeta";
            this.txbSifraPredmeta.Size = new System.Drawing.Size(223, 21);
            this.txbSifraPredmeta.TabIndex = 15;
            this.txbSifraPredmeta.Text = "SE311";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Broj lekcije:";
            // 
            // txbBrojLekcije
            // 
            this.txbBrojLekcije.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txbBrojLekcije.Location = new System.Drawing.Point(116, 39);
            this.txbBrojLekcije.Name = "txbBrojLekcije";
            this.txbBrojLekcije.Size = new System.Drawing.Size(222, 20);
            this.txbBrojLekcije.TabIndex = 13;
            this.txbBrojLekcije.Text = "L01";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Šifra predmeta:";
            // 
            // EditProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 184);
            this.Controls.Add(this.txbSifraPredmeta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txbBrojLekcije);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txbAutor);
            this.Controls.Add(this.txbGodina);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbNaslov);
            this.Controls.Add(this.btnUpdateProject);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "EditProjectForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update Project";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnUpdateProject;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txbNaslov;
        private System.Windows.Forms.TextBox txbGodina;
        private System.Windows.Forms.TextBox txbAutor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox txbSifraPredmeta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txbBrojLekcije;
        private System.Windows.Forms.Label label1;
    }
}