using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Project;

namespace mDitaEditor.CustomForms
{
    public partial class ObjectPreviewForm : Form
    {
        public LearningContent Content { get; set; }        

        public ObjectPreviewForm(LearningContent content)
        {
            InitializeComponent();            
            Content = content;
            OpenSlide(Content);
            foreach(Section sec in Content.LearningBody.Sections)
            {
                OpenSlide(sec);
            }
        }

        private void ObjectPreviewForm_Load(object sender, EventArgs e)
        {

        }
        public void OpenSlide(LearningBase learningObject)
        {
            ProjectSingleton.SelectedContent = learningObject;
            if (learningObject is LearningContent)
            {
                panel1.Controls.Add(new LearningContentControl((LearningContent)learningObject));
            }
        }

        public void OpenSlide(Section section)
        {
            LearningSectionControl control = new LearningSectionControl(section,true);
            Control controlLast = panel1.Controls[panel1.Controls.Count - 1];
            control.Location = new Point(0, controlLast.Height + controlLast.Location.Y + 10);
            panel1.Controls.Add(control);
        }

        private void btnImportObject_Click(object sender, EventArgs e)
        {
            if(ProjectSingleton.Project != null)
            {
                string lastContentId = "LC-00";
                if (ProjectSingleton.Project.LearningContents.Count > 0)
                {
                    lastContentId = ProjectSingleton.Project.LearningContents[ProjectSingleton.Project.LearningContents.Count - 1].Id;
                }
                Content.Id = lastContentId;
                Content.IncrementId();

                ImportDitaFiles.DownloadImagesFromObjectToResource(Content);

                LearningContent copy = Content.DeepClone() as LearningContent;
                ProjectSingleton.Project.LearningContents.Add(copy);
                MainForm.Instance.RecreateMenu();
                this.Close();
            }
            else
            {
                MessageBox.Show("Niste ucitali objekat");
            }
        }
    }
}
