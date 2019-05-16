using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Project;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Forms
{
    public partial class AdditionalActivitiesForm : Form
    {
        private LearningBase objectBase = null;

        public AdditionalActivitiesForm(LearningBase learningBase)
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Resources.forum.GetHicon());
            objectBase = learningBase;
        }

        /// <summary>
        /// Učitavanje postojećih objekata u projektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManageAdditionalActivities_Load(object sender, EventArgs e)
        {
            foreach (LearningContent learningContent in ProjectSingleton.Project.LearningContents)
            {
                cmbSelectObject.Items.Add(learningContent);
            }
            cmbSelectObject.Items.Add(ProjectSingleton.Project.LearningSummary);
            if (objectBase != null && !(objectBase is LearningOverview))
            {
                cmbSelectObject.SelectedItem = objectBase;
            }
            else
            {
                cmbSelectObject.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Event na klik Edit dugmeta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Form globalForm = null;
            if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsQa)
            {
                globalForm = new QaForm((LearningBase)cmbSelectObject.SelectedItem, (LamsQa)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsForum)
            {
                globalForm = new ForumForm((LearningBase)cmbSelectObject.SelectedItem, (LamsForum)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsMultipleChoice)
            {
                globalForm = new MultipleChoiceForm((LearningBase)cmbSelectObject.SelectedItem, (LamsMultipleChoice)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsSubmitFiles)
            {
                globalForm = new SubmitFilesForm((LearningBase)cmbSelectObject.SelectedItem, (LamsSubmitFiles)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsShareResource)
            {
                globalForm = new ShareResourcesForm((LearningBase)cmbSelectObject.SelectedItem, (LamsShareResource)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsAssessment)
            {
                globalForm  = new AssessmentForm((LearningBase)cmbSelectObject.SelectedItem, (LamsAssessment)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsChat)
            {
                globalForm = new ChatForm((LearningBase)cmbSelectObject.SelectedItem, (LamsChat)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsJavaGrader)
            {
                globalForm = new JavaGraderGui((LearningBase)cmbSelectObject.SelectedItem, (LamsJavaGrader)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsNotebook)
            {
                globalForm = new NotebookForm((LearningBase)cmbSelectObject.SelectedItem, (LamsNotebook)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsNoticeboard)
            {
                globalForm = new NoticeboardAddForm((LearningBase)cmbSelectObject.SelectedItem, (LamsNoticeboard)listActivities.SelectedItem);
            }
            else if (listActivities.SelectedItem != null && listActivities.SelectedItem is LamsImageGallery)
            {
                globalForm = new ImageGalleryForm((LearningBase)cmbSelectObject.SelectedItem, (LamsImageGallery)listActivities.SelectedItem);
            }
            if (globalForm != null)
            {
                if (globalForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
        }

        /// <summary>
        /// Poziv move na dole
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            Move(false);
        }

        /// <summary>
        /// Poziv move na gore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            Move(true);
        }

        /// <summary>
        /// Move gore i dole za dodatnu aktivnost
        /// </summary>
        /// <param name="up"></param>
        public new void Move(bool up)
        {
            if (listActivities.SelectedItem != null)
            {
                var list = objectBase.ToolList;
                var temp = listActivities.SelectedItem as LamsTool;
                int index = list.IndexOf(listActivities.SelectedItem as LamsTool);
                int newIndex = index + (up ? -1 : 1);

                if (newIndex < 0 || newIndex >= list.Count)
                {
                    return;
                }
                list[index] = list[newIndex];
                list[newIndex] = temp;
            }
            ReloadListOfActivites();
        }

        /// <summary>
        /// Event za dodavanje QA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddQA_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                QaForm addQAForm = new QaForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }

            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        /// <summary>
        /// Reload liste aktivnosti
        /// </summary>
        public void ReloadListOfActivites()
        {
            listActivities.Items.Clear();
            if (cmbSelectObject.SelectedItem != null)
            {
                LearningBase learningObject = (LearningBase)cmbSelectObject.SelectedItem;
                foreach (object objectName in learningObject.ToolList)
                {
                    listActivities.Items.Add(objectName);
                }
            }
        }

        /// <summary>
        /// Metoda koja se pokreće na promenu objekta za koji se dodaju dodatne aktivnosti
        /// Učitava aktivnosti za selektovan objekat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSelectObject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                ReloadListOfActivites();
                objectBase = (LearningBase)cmbSelectObject.SelectedItem;
            }
        }

        /// <summary>
        /// Metoda za brisanje dodatne aktivnosti u obeleženom objektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listActivities.SelectedItem != null)
            {
                objectBase.ToolList.Remove(listActivities.SelectedItem as LamsTool);
                ReloadListOfActivites();
            }
        }

        /// <summary>
        /// Event koji pokrece kopiranje dodatne aktivnosti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (listActivities.SelectedItem != null)
            {
                LamsClipboard.CopiedObject = listActivities.SelectedItem;
                Clipboard.SetDataObject(listActivities.SelectedItem);
            }
        }

        /// <summary>
        /// Event koji se dešava na Paste dodatne aktivnosti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                LamsClipboard.Paste((LearningBase)cmbSelectObject.SelectedItem);
            }
            ReloadListOfActivites();
        }

        /// <summary>
        /// Event koji se dešava na Cut dodatne aktivnosti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCut_Click(object sender, EventArgs e)
        {
            if (listActivities.SelectedItem != null)
            {
                LamsClipboard.CopiedObject = listActivities.SelectedItem;
                Clipboard.SetDataObject(listActivities.SelectedItem);
                objectBase.ToolList.Remove(listActivities.SelectedItem as LamsTool);
            }
            ReloadListOfActivites();

        }

        private void btnAddForum_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                ForumForm addQAForm = new ForumForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnAddMc_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                MultipleChoiceForm addQAForm = new MultipleChoiceForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }

        }

        private void btnAddSf_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                SubmitFilesForm addQAForm = new SubmitFilesForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnAddSR_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                ShareResourcesForm addQAForm = new ShareResourcesForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnAssessment_Click(object sender, EventArgs e)
        {

            if (cmbSelectObject.SelectedItem != null)
            {
                AssessmentForm addQAForm = new AssessmentForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                ChatForm addQAForm = new ChatForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnJavaGrader_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                JavaGraderGui addQAForm = new JavaGraderGui((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnAddNotebook_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                NotebookForm addQAForm = new NotebookForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnAddNoticeboard_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                NoticeboardAddForm addQAForm = new NoticeboardAddForm((LearningBase)cmbSelectObject.SelectedItem);
                if (addQAForm.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }

        private void btnImageGallery_Click(object sender, EventArgs e)
        {
            if (cmbSelectObject.SelectedItem != null)
            {
                ImageGalleryForm imagegallery = new ImageGalleryForm((LearningBase)cmbSelectObject.SelectedItem);
                if (imagegallery.ShowDialog() == DialogResult.OK)
                {
                    ReloadListOfActivites();
                }
            }
            else
            {
                MessageBox.Show("You have not selected the object");
            }
        }
    }
}
