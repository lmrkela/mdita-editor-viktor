namespace mDitaEditor.Lams.Forms
{
    partial class AdditionalActivitiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdditionalActivitiesForm));
            this.cmbSelectObject = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listActivities = new System.Windows.Forms.ListBox();
            this.btnAddNotebook = new System.Windows.Forms.Button();
            this.btnJavaGrader = new System.Windows.Forms.Button();
            this.btnChat = new System.Windows.Forms.Button();
            this.btnAssessment = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnAddSf = new System.Windows.Forms.Button();
            this.btnAddMc = new System.Windows.Forms.Button();
            this.btnAddForum = new System.Windows.Forms.Button();
            this.btnPaste = new System.Windows.Forms.Button();
            this.btnCut = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAddQA = new System.Windows.Forms.Button();
            this.btnAddNoticeboard = new System.Windows.Forms.Button();
            this.btnImageGallery = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbSelectObject
            // 
            this.cmbSelectObject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSelectObject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectObject.FormattingEnabled = true;
            this.cmbSelectObject.Location = new System.Drawing.Point(51, 12);
            this.cmbSelectObject.Name = "cmbSelectObject";
            this.cmbSelectObject.Size = new System.Drawing.Size(422, 21);
            this.cmbSelectObject.TabIndex = 0;
            this.cmbSelectObject.SelectedIndexChanged += new System.EventHandler(this.cmbSelectObject_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Object:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Additional activities:";
            // 
            // listActivities
            // 
            this.listActivities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listActivities.FormattingEnabled = true;
            this.listActivities.Location = new System.Drawing.Point(12, 59);
            this.listActivities.Name = "listActivities";
            this.listActivities.Size = new System.Drawing.Size(424, 238);
            this.listActivities.TabIndex = 3;
            // 
            // btnAddNotebook
            // 
            this.btnAddNotebook.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddNotebook.BackgroundImage = global::mDitaEditor.Properties.Resources.notebook;
            this.btnAddNotebook.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNotebook.Location = new System.Drawing.Point(12, 443);
            this.btnAddNotebook.Name = "btnAddNotebook";
            this.btnAddNotebook.Size = new System.Drawing.Size(110, 60);
            this.btnAddNotebook.TabIndex = 25;
            this.btnAddNotebook.Text = "Add Notebook";
            this.btnAddNotebook.UseVisualStyleBackColor = true;
            this.btnAddNotebook.Click += new System.EventHandler(this.btnAddNotebook_Click);
            // 
            // btnJavaGrader
            // 
            this.btnJavaGrader.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnJavaGrader.BackgroundImage = global::mDitaEditor.Properties.Resources.java;
            this.btnJavaGrader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnJavaGrader.Location = new System.Drawing.Point(364, 378);
            this.btnJavaGrader.Name = "btnJavaGrader";
            this.btnJavaGrader.Size = new System.Drawing.Size(110, 60);
            this.btnJavaGrader.TabIndex = 24;
            this.btnJavaGrader.Text = "Add Javagrader";
            this.btnJavaGrader.UseVisualStyleBackColor = true;
            this.btnJavaGrader.Click += new System.EventHandler(this.btnJavaGrader_Click);
            // 
            // btnChat
            // 
            this.btnChat.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnChat.BackgroundImage = global::mDitaEditor.Properties.Resources.lms_chat;
            this.btnChat.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnChat.Location = new System.Drawing.Point(247, 378);
            this.btnChat.Name = "btnChat";
            this.btnChat.Size = new System.Drawing.Size(110, 60);
            this.btnChat.TabIndex = 23;
            this.btnChat.Text = "Add Chat";
            this.btnChat.UseVisualStyleBackColor = true;
            this.btnChat.Click += new System.EventHandler(this.btnChat_Click);
            // 
            // btnAssessment
            // 
            this.btnAssessment.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAssessment.BackgroundImage = global::mDitaEditor.Properties.Resources.lms_assesment;
            this.btnAssessment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAssessment.Location = new System.Drawing.Point(129, 377);
            this.btnAssessment.Name = "btnAssessment";
            this.btnAssessment.Size = new System.Drawing.Size(110, 60);
            this.btnAssessment.TabIndex = 22;
            this.btnAssessment.Text = "Add Assessment";
            this.btnAssessment.UseVisualStyleBackColor = true;
            this.btnAssessment.Click += new System.EventHandler(this.btnAssessment_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDown.Image = global::mDitaEditor.Properties.Resources.arrowdown;
            this.btnDown.Location = new System.Drawing.Point(442, 127);
            this.btnDown.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(32, 32);
            this.btnDown.TabIndex = 21;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUp.Image = global::mDitaEditor.Properties.Resources.arrowup;
            this.btnUp.Location = new System.Drawing.Point(442, 93);
            this.btnUp.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(32, 32);
            this.btnUp.TabIndex = 20;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.BackgroundImage = global::mDitaEditor.Properties.Resources.lms_share_resources;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(12, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 60);
            this.button1.TabIndex = 19;
            this.button1.Text = "Add Share Resource";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnAddSR_Click);
            // 
            // btnAddSf
            // 
            this.btnAddSf.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddSf.BackgroundImage = global::mDitaEditor.Properties.Resources.lms_submit_files;
            this.btnAddSf.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddSf.Location = new System.Drawing.Point(364, 312);
            this.btnAddSf.Name = "btnAddSf";
            this.btnAddSf.Size = new System.Drawing.Size(110, 60);
            this.btnAddSf.TabIndex = 18;
            this.btnAddSf.Text = "Add Submit Filles";
            this.btnAddSf.UseVisualStyleBackColor = true;
            this.btnAddSf.Click += new System.EventHandler(this.btnAddSf_Click);
            // 
            // btnAddMc
            // 
            this.btnAddMc.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddMc.BackgroundImage = global::mDitaEditor.Properties.Resources.lms_multiple_choice;
            this.btnAddMc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddMc.Location = new System.Drawing.Point(247, 312);
            this.btnAddMc.Name = "btnAddMc";
            this.btnAddMc.Size = new System.Drawing.Size(110, 60);
            this.btnAddMc.TabIndex = 17;
            this.btnAddMc.Text = "Add Multiple Choice";
            this.btnAddMc.UseVisualStyleBackColor = true;
            this.btnAddMc.Click += new System.EventHandler(this.btnAddMc_Click);
            // 
            // btnAddForum
            // 
            this.btnAddForum.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddForum.BackgroundImage = global::mDitaEditor.Properties.Resources.lms_forum;
            this.btnAddForum.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddForum.Location = new System.Drawing.Point(129, 311);
            this.btnAddForum.Name = "btnAddForum";
            this.btnAddForum.Size = new System.Drawing.Size(110, 60);
            this.btnAddForum.TabIndex = 16;
            this.btnAddForum.Text = "Add Forum";
            this.btnAddForum.UseVisualStyleBackColor = true;
            this.btnAddForum.Click += new System.EventHandler(this.btnAddForum_Click);
            // 
            // btnPaste
            // 
            this.btnPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPaste.Image = global::mDitaEditor.Properties.Resources.paste;
            this.btnPaste.Location = new System.Drawing.Point(442, 229);
            this.btnPaste.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(32, 32);
            this.btnPaste.TabIndex = 15;
            this.btnPaste.UseVisualStyleBackColor = true;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnCut
            // 
            this.btnCut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCut.Image = global::mDitaEditor.Properties.Resources.cut;
            this.btnCut.Location = new System.Drawing.Point(442, 195);
            this.btnCut.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(32, 32);
            this.btnCut.TabIndex = 14;
            this.btnCut.UseVisualStyleBackColor = true;
            this.btnCut.Click += new System.EventHandler(this.btnCut_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopy.Image = global::mDitaEditor.Properties.Resources.copy;
            this.btnCopy.Location = new System.Drawing.Point(442, 161);
            this.btnCopy.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(32, 32);
            this.btnCopy.TabIndex = 13;
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Image = global::mDitaEditor.Properties.Resources.edit;
            this.btnEdit.Location = new System.Drawing.Point(442, 59);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(32, 32);
            this.btnEdit.TabIndex = 12;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = global::mDitaEditor.Properties.Resources.delete;
            this.btnDelete.Location = new System.Drawing.Point(442, 263);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAddQA
            // 
            this.btnAddQA.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddQA.BackgroundImage = global::mDitaEditor.Properties.Resources.lms_qa;
            this.btnAddQA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddQA.Location = new System.Drawing.Point(12, 311);
            this.btnAddQA.Name = "btnAddQA";
            this.btnAddQA.Size = new System.Drawing.Size(110, 60);
            this.btnAddQA.TabIndex = 4;
            this.btnAddQA.Text = "Add Q/A";
            this.btnAddQA.UseVisualStyleBackColor = true;
            this.btnAddQA.Click += new System.EventHandler(this.btnAddQA_Click);
            // 
            // btnAddNoticeboard
            // 
            this.btnAddNoticeboard.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddNoticeboard.BackgroundImage = global::mDitaEditor.Properties.Resources.noticeboard;
            this.btnAddNoticeboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAddNoticeboard.Location = new System.Drawing.Point(129, 443);
            this.btnAddNoticeboard.Name = "btnAddNoticeboard";
            this.btnAddNoticeboard.Size = new System.Drawing.Size(110, 60);
            this.btnAddNoticeboard.TabIndex = 26;
            this.btnAddNoticeboard.Text = "Add Noticeboard";
            this.btnAddNoticeboard.UseVisualStyleBackColor = true;
            this.btnAddNoticeboard.Click += new System.EventHandler(this.btnAddNoticeboard_Click);
            // 
            // btnImageGallery
            // 
            this.btnImageGallery.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnImageGallery.BackgroundImage")));
            this.btnImageGallery.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnImageGallery.Location = new System.Drawing.Point(247, 445);
            this.btnImageGallery.Name = "btnImageGallery";
            this.btnImageGallery.Size = new System.Drawing.Size(110, 58);
            this.btnImageGallery.TabIndex = 27;
            this.btnImageGallery.Text = "Add Image Gallery";
            this.btnImageGallery.UseVisualStyleBackColor = true;
            this.btnImageGallery.Click += new System.EventHandler(this.btnImageGallery_Click);
            // 
            // AdditionalActivitiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 516);
            this.Controls.Add(this.btnImageGallery);
            this.Controls.Add(this.btnAddNoticeboard);
            this.Controls.Add(this.btnAddNotebook);
            this.Controls.Add(this.btnJavaGrader);
            this.Controls.Add(this.cmbSelectObject);
            this.Controls.Add(this.btnChat);
            this.Controls.Add(this.btnAssessment);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAddSf);
            this.Controls.Add(this.btnAddMc);
            this.Controls.Add(this.btnAddForum);
            this.Controls.Add(this.btnPaste);
            this.Controls.Add(this.btnCut);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAddQA);
            this.Controls.Add(this.listActivities);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 483);
            this.Name = "AdditionalActivitiesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage Additional Activities";
            this.Load += new System.EventHandler(this.ManageAdditionalActivities_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSelectObject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listActivities;
        private System.Windows.Forms.Button btnAddQA;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnCut;
        private System.Windows.Forms.Button btnPaste;
        private System.Windows.Forms.Button btnAddForum;
        private System.Windows.Forms.Button btnAddMc;
        private System.Windows.Forms.Button btnAddSf;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button btnDown;
        public System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnAssessment;
        private System.Windows.Forms.Button btnChat;
        private System.Windows.Forms.Button btnJavaGrader;
        private System.Windows.Forms.Button btnAddNotebook;
        private System.Windows.Forms.Button btnAddNoticeboard;
        private System.Windows.Forms.Button btnImageGallery;
    }
}