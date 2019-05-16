using System;
using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.Dita.Forms
{
    public partial class ChooseObjectForm : Form
    {
        public LearningContent LearningContent = null;
        private LearningContent SelectedContent = null;
        public ChooseObjectForm(LearningContent selectedContent)
        {
            SelectedContent = selectedContent;
            InitializeComponent();
        }

        private void ChooseObject_Load(object sender, EventArgs e)
        {
            foreach (LearningContent learningContent in ProjectSingleton.Project.LearningContents)
            {
                if (learningContent != SelectedContent)
                {
                    cmbSelectObject.Items.Add(learningContent);
                }
            }

            if(cmbSelectObject.Items.Count > 0)
            {
                cmbSelectObject.SelectedIndex = 0;
            }
        }

        private void cmbSelectObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                LearningContent = (LearningContent)cmbSelectObject.SelectedItem;
            }
        }

        private void btnChangeObjectToSubobject_Click(object sender, EventArgs e)
        {
            if (LearningContent != null)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void ChooseObject_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(LearningContent == null)
            {
                DialogResult = DialogResult.Cancel;
            }
        }
    }
}
