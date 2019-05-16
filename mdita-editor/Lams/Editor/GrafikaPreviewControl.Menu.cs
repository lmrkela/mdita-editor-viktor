using System;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Forms;

namespace mDitaEditor.Lams.Editor
{
    partial class GrafikaPreviewControl
    {
        private void InitializeMenu()
        {
            contextMenuPreview.Closed += ContextMenuPreview_Closed;
            itemEdit.Click += ItemEdit_Click;
            itemDelete.Click += ItemDelete_Click;
            itemAddAssessment.Click += ItemAddAssessment_Click;
            itemAddChat.Click += ItemAddChat_Click;
            itemAddForum.Click += ItemAddForum_Click;
            itemAddMC.Click += ItemAddMC_Click;
            itemAddQA.Click += ItemAddQA_Click;
            itemAddShareResources.Click += ItemAddShareResources_Click;
            itemAddSubmitFiles.Click += ItemAddSubmitFiles_Click;
            itemAddJavagrader.Click += ItemAddJavagrader_Click;
            itemAddNotebook.Click += ItemAddNotebook_Click;
            itemAddNoticeboard.Click += ItemAddNoticeboard_Click;
        }

        private void ItemAddNoticeboard_Click(object sender, EventArgs e)
        {
            var obj = (GrafikaObject as LamsNoticeboard)?.LearningObject;
            if (obj == null)
            {
                var tool = GrafikaObject as LamsTool;
                if (tool == null)
                {
                    return;
                }
                obj = tool.Parent;
                if (obj == null)
                {
                    return;
                }
            }

            var form = new NoticeboardAddForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddNotebook_Click(object sender, EventArgs e)
        {
            var obj = (GrafikaObject as LamsNoticeboard)?.LearningObject;
            if (obj == null)
            {
                var tool = GrafikaObject as LamsTool;
                if (tool == null)
                {
                    return;
                }
                obj = tool.Parent;
                if (obj == null)
                {
                    return;
                }
            }

            var form = new NotebookForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ContextMenuPreview_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            Invalidate();
        }

        private void ItemEdit_Click(object sender, EventArgs e)
        {
            var obj = GrafikaObject;
            if (obj == null)
            {
                return;
            }

            var result = DialogResult.No;
            if (obj is LamsNoticeboard)
            {
                LamsNoticeboard lams = (LamsNoticeboard)obj;
                if (lams.LearningObject != null)
                {
                    MainForm.Instance.OpenSlide(((LamsNoticeboard)obj).LearningObject);
                    MainForm.Instance.tabDita.Owner.ActiveTab = MainForm.Instance.tabDita;
                }
                else
                {
                    var form = new NoticeboardAddForm(null, lams);
                    result = form.ShowDialog();
                }
            }
            else if (obj is LamsAssessment)
            {
                var ass = (LamsAssessment)obj;
                var form = new AssessmentForm(null, ass);
                result = form.ShowDialog();
            }
            else if (obj is LamsChat)
            {
                var chat = (LamsChat)obj;
                var form = new ChatForm(null, chat);
                result = form.ShowDialog();
            }
            else if (obj is LamsForum)
            {
                var forum = (LamsForum)obj;
                var form = new ForumForm(null, forum);
                result = form.ShowDialog();
            }
            else if (obj is LamsMultipleChoice)
            {
                var mc = (LamsMultipleChoice)obj;
                var form = new MultipleChoiceForm(null, mc);
                result = form.ShowDialog();
            }
            else if (obj is LamsQa)
            {
                var qa = (LamsQa)obj;
                var form = new QaForm(null, qa);
                result = form.ShowDialog();
            }
            else if (obj is LamsShareResource)
            {
                var share = (LamsShareResource)obj;
                var form = new ShareResourcesForm(null, share);
                result = form.ShowDialog();
            }
            else if (obj is LamsSubmitFiles)
            {
                var submit = (LamsSubmitFiles)obj;
                var form = new SubmitFilesForm(null, submit);
                result = form.ShowDialog();
            }
            else if (obj is LamsJavaGrader)
            {
                var grader = (LamsJavaGrader)obj;
                var form = new JavaGraderGui(null, grader);
                result = form.ShowDialog();
            }
            else if (obj is LamsNotebook)
            {
                var grader = (LamsNotebook)obj;
                var form = new NotebookForm(null, grader);
                result = form.ShowDialog();
            }
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemDelete_Click(object sender, EventArgs e)
        {
            var obj = GrafikaObject as LamsTool;
            if (obj == null)
            {
                return;
            }

            var result = MessageBox.Show("Da li želite da obrišete aktivnost " + obj.TitleText + "?", "Obrisati aktivnost?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                obj.Parent.ToolList.Remove(obj);
                ((GrafikaListControl) Parent).ParentPanel.LoadProject();
            }
        }

        public LearningBase ParentObject
        {
            get
            {
                var obj = (GrafikaObject as LamsNoticeboard)?.LearningObject;
                if (obj == null)
                {
                    var tool = GrafikaObject as LamsTool;
                    obj = tool?.Parent;
                }
                var content = obj as LearningContent;
                if (content?.Parent != null)
                {
                    obj = content.Parent;
                }
                return obj;
            }
        }

        private void ItemAddAssessment_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new AssessmentForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddChat_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new ChatForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddForum_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new ForumForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddMC_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new MultipleChoiceForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddQA_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new QaForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddShareResources_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new ShareResourcesForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddSubmitFiles_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new SubmitFilesForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }

        private void ItemAddJavagrader_Click(object sender, EventArgs e)
        {
            var obj = ParentObject;
            var form = new JavaGraderGui(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                ((GrafikaListControl)Parent).ParentPanel.LoadProject();
            }
        }
    }
}
