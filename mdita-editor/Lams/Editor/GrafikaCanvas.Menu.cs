using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Lams.Editor.Conditions;
using mDitaEditor.Lams.Forms;
using mDitaEditor.Project;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Editor
{
    public partial class GrafikaCanvas
    {
        private readonly ContextMenuStrip MenuConnection = new ContextMenuStrip();
        private readonly ContextMenuStrip MenuObject = new ContextMenuStrip();
        private readonly ContextMenuStrip MenuPanel = new ContextMenuStrip();

        private ContextMenuStrip _menuOpen;

        private void InitializeMenu()
        {
            ToolStripLabel label = new ToolStripLabel();

            MenuPanel.Opened += Menu_Opened;
            MenuPanel.Closed += Menu_Closed;
            MenuPanel.Opening += MenuPanel_Opening;
            MenuPanel.Items.Add(new ToolStripMenuItem("Move objects", Resources.hand_open, MoveListener_Click));
            MenuPanel.Items.Add(new ToolStripMenuItem("Connect objects", Resources.cursor_crosshair, ConnectListener_Click));
            MenuPanel.Items.Add(new ToolStripSeparator());
            MenuPanel.Items.Add(new ToolStripMenuItem("Zoom in", Resources.zoom_in, ZoomIn_Click));
            MenuPanel.Items.Add(new ToolStripMenuItem("Zoom out", Resources.zoom_out, ZoomOut_Click));
            MenuPanel.Items.Add(new ToolStripMenuItem("Toggle grid", Resources.grid_dot, ToggleGrid_Click));
            MenuPanel.Items.Add(new ToolStripSeparator());
            MenuPanel.Items.Add(new ToolStripMenuItem("New Gate", Resources.stop_sign24, NewGate_Click));
            MenuPanel.Items.Add(new ToolStripMenuItem("New Branch", Resources.branch24, NewBranch_Click));
            MenuPanel.Items.Add(new ToolStripMenuItem("New Optional Activity", Resources.additional_activity24, NewOptional_Click));

            MenuConnection.Opened += Menu_Opened;
            MenuConnection.Closed += Menu_Closed;
            MenuConnection.Opening += MenuConnection_Opening;
            label = new ToolStripLabel() { Font = new Font(label.Font, FontStyle.Bold) };
            MenuConnection.Items.Add(label);
            MenuConnection.Items.Add(new ToolStripSeparator());
            MenuConnection.Items.Add(new ToolStripMenuItem("Set Default", Resources.yes, SetDefault_Click));
            MenuConnection.Items.Add(new ToolStripSeparator());
            MenuConnection.Items.Add(new ToolStripMenuItem("Remove", Resources.delete, DeleteConnection_Click));

            MenuObject.Opened += Menu_Opened;
            MenuObject.Closed += Menu_Closed;
            MenuObject.Opening += MenuObject_Opening;
            label = new ToolStripLabel() { Font = new Font(label.Font, FontStyle.Bold) };
            MenuObject.Items.Add(label);
            MenuObject.Items.Add(new ToolStripSeparator());
            MenuObject.Items.Add(new ToolStripMenuItem("Edit", Resources.edit, OpenObject_Click));
            MenuObject.Items.Add(new ToolStripMenuItem("Remove", Resources.delete, DeleteObject_Click));
            MenuObject.Items.Add(new ToolStripSeparator());
            MenuObject.Items.Add(new ToolStripMenuItem("Bring to front", Resources.bring_to_front24, BringToFront_Click));
            MenuObject.Items.Add(new ToolStripMenuItem("Send to back", Resources.send_to_back24, SendToBack_Click));
        }

        private Point _menuLocation;

        private void Menu_Opened(object sender, EventArgs e)
        {
            _menuLocation = TranslateOffset(PointToClient(Cursor.Position));
            _menuOpen = sender as ContextMenuStrip;
            Cursor = Cursors.Arrow;
        }

        private void Menu_Closed(object sender, EventArgs e)
        {
            _menuOpen = null;
        }

        private void MenuPanel_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var form = MainForm.Instance;
            MenuPanel.Items[0].BackColor = Listener == MoveItemListener ? SystemColors.MenuHighlight : MenuPanel.BackColor;
            MenuPanel.Items[1].BackColor = Listener == ConnectListener ? SystemColors.MenuHighlight : MenuPanel.BackColor;
            MenuPanel.Items[3].Enabled = form.numZoom.Value < form.numZoom.Maximum;
            MenuPanel.Items[4].Enabled = form.numZoom.Value > form.numZoom.Minimum;
            MenuPanel.Items[5].Text = (form.chbShowGrid.Checked ? "Hide" : "Show") + " grid";
            for (int i = 7; i <= 9; ++i)
            {
                MenuPanel.Items[i].Enabled = ProjectSingleton.Project != null;
            }
        }

        private void MoveListener_Click(object sender, EventArgs e)
        {
            SetListener(MouseListener.Move);
        }

        private void ConnectListener_Click(object sender, EventArgs e)
        {
            SetListener(MouseListener.Connect);
        }

        private void ZoomIn_Click(object sender, EventArgs e)
        {
            var mouse = TranslateOffset(PointToClient(Cursor.Position));
            GrafikaCanvas_MouseWheel(sender, new MouseEventArgs(MouseButtons.None, 0, mouse.X, mouse.Y, 1));
        }

        private void ZoomOut_Click(object sender, EventArgs e)
        {
            var mouse = TranslateOffset(PointToClient(Cursor.Position));
            GrafikaCanvas_MouseWheel(sender, new MouseEventArgs(MouseButtons.None, 0, mouse.X, mouse.Y, -1));
        }

        private void ToggleGrid_Click(object sender, EventArgs e)
        {
            var form = MainForm.Instance;
            var snap = form.chbSnapToGrid.Checked;
            form.chbShowGrid.Checked = !form.chbShowGrid.Checked;
            form.chbShowGrid.OnCheckChanged(e);
            form.chbSnapToGrid.Checked = snap;
            form.chbSnapToGrid.OnCheckChanged(e);
        }

        private void AddObject(IGrafikaObject tool)
        {
            if (ProjectSingleton.Project == null)
            {
                return;
            }
            SetListener(MouseListener.Add, tool);
            Listener.MouseMove(_menuLocation);
            Listener.MouseDown(MouseButtons.Left, _menuLocation);
            Listener.MouseUp(MouseButtons.Left, _menuLocation);
        }

        private void NewGate_Click(object sender, EventArgs e)
        {
            AddObject(new LamsGate());
            MainForm.Instance.CheckErrorsAndStatistics(true);
        }

        private void NewBranch_Click(object sender, EventArgs e)
        {
            AddObject(new LamsBranch());
        }
        
        private void NewOptional_Click(object sender, EventArgs e)
        {
            AddObject(new LamsOptional());
            MainForm.Instance.CheckErrorsAndStatistics(true);
        }

        private void MenuConnection_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (HoverConnection == null)
            {
                e.Cancel = true;
                return;
            }
            var branchConnection = HoverConnection as GrafikaBranchConnection;
            if (branchConnection != null)
            {
                MenuConnection.Items[0].Text = branchConnection.Branch.TitleText + "  →  " + branchConnection.Title;
                MenuConnection.Items[1].Visible = true;
                MenuConnection.Items[2].Visible = true;
                MenuConnection.Items[2].Enabled = branchConnection.Branch.DefaultBranch != branchConnection;
            }
            else
            {
                MenuConnection.Items[0].Text = HoverConnection.StartItem.GrafikaObject.TitleText + "  →  " +
                                               HoverConnection.EndItem.GrafikaObject.TitleText;
                MenuConnection.Items[1].Visible = false;
                MenuConnection.Items[2].Visible = false;
            }
            ++MenuConnection.Width;
        }

        private void DeleteConnection_Click(object sender, EventArgs e)
        {
            HoverConnection.Delete();
            MainForm.Instance.CheckErrorsAndStatistics(true);
        }

        private void SetDefault_Click(object sender, EventArgs e)
        {
            var branchConnection = (GrafikaBranchConnection) HoverConnection;
            branchConnection.Branch.DefaultBranch = branchConnection;
            MainForm.Instance.CheckErrorsAndStatistics(true);
        }

        private void MenuObject_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (HoverObject == null)
            {
                e.Cancel = true;
                return;
            }
            MenuObject.Items[0].Text = HoverObject.GrafikaObject.TitleText;
            ++MenuObject.Width;
        }

        private void OpenObject_Click(object sender, EventArgs e)
        {
            var obj = HoverObject.GrafikaObject;
            if (obj == null)
            {
                return;
            }
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
                    form.ShowDialog();
                }
            }
            else if (obj is LamsBranch)
            {
                var branch = (LamsBranch)obj;
                var form = new LamsBranchForm(branch, HoverObject);
                form.ShowDialog();

            }
            else if (obj is LamsGate)
            {
                var gate = (LamsGate)obj;
                var form = new LamsGateForm(gate, HoverObject);
                form.ShowDialog();
            }
            else if (obj is LamsOptional)
            {
                var optional = (LamsOptional)obj;
                var form = new LamsOptionalForm(optional);
                form.ShowDialog();
            }
            else if (obj is LamsAssessment)
            {
                var ass = (LamsAssessment)obj;
                var form = new AssessmentForm(null, ass);
                form.ShowDialog();
            }
            else if (obj is LamsChat)
            {
                var chat = (LamsChat)obj;
                var form = new ChatForm(null, chat);
                form.ShowDialog();
            }
            else if (obj is LamsForum)
            {
                var forum = (LamsForum)obj;
                var form = new ForumForm(null, forum);
                form.ShowDialog();
            }
            else if (obj is LamsMultipleChoice)
            {
                var mc = (LamsMultipleChoice)obj;
                var form = new MultipleChoiceForm(null, mc);
                form.ShowDialog();
            }
            else if (obj is LamsQa)
            {
                var qa = (LamsQa)obj;
                var form = new QaForm(null, qa);
                form.ShowDialog();
            }
            else if (obj is LamsShareResource)
            {
                var share = (LamsShareResource)obj;
                var form = new ShareResourcesForm(null, share);
                form.ShowDialog();
            }
            else if (obj is LamsSubmitFiles)
            {
                var submit = (LamsSubmitFiles)obj;
                var form = new SubmitFilesForm(null, submit);
                form.ShowDialog();
            }
            else if (obj is LamsJavaGrader)
            {
                var grader = (LamsJavaGrader)obj;
                var form = new JavaGraderGui(null, grader);
                form.ShowDialog();
            }
            else if (obj is LamsNotebook)
            {
                var notebook = (LamsNotebook)obj;
                var form = new NotebookForm(null, notebook);
                form.ShowDialog();
            }
            MainForm.Instance.CheckErrorsAndStatistics(true);
        }

        public void DeleteObject_Click(object sender, EventArgs e)
        {
            HoverObject.Delete();
            HoverObject = null;
            MainForm.Instance.CheckErrorsAndStatistics(true);
        }

        private void SendToBack_Click(object sender, EventArgs e)
        {
            if (Items.Count <= 1)
            {
                return;
            }
            Items.Remove(HoverObject);
            Items.Insert(0, HoverObject);
        }

        private void BringToFront_Click(object sender, EventArgs e)
        {
            if (Items.Count <= 1)
            {
                return;
            }
            Items.Remove(HoverObject);
            Items.Add(HoverObject);
        }
    }
}
