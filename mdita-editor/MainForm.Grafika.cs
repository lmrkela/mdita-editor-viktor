using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams;
using mDitaEditor.Lams.Controls;
using mDitaEditor.Lams.Editor;
using mDitaEditor.Lams.Forms;
using mDitaEditor.Project;

namespace mDitaEditor
{
    partial class MainForm
    {
        private void ribbonMenu_ActiveTabChanged(object sender, EventArgs e)
        {
            SuspendLayout();
            if (tabGrafika.Active)
            {
                panelControler.Visible = false;
                slideList.Visible = false;
                trbGridSize.Visible = true;
                numGridSize.Visible = true;
                labGridSize.Visible = true;
                trbZoom.Visible = true;
                labelZoom.Visible = true;
                numZoom.Visible = true;

                trbGridSize.BringToFront();
                labGridSize.BringToFront();
                numGridSize.BringToFront();
                trbZoom.BringToFront();
                labelZoom.BringToFront();
                numZoom.BringToFront();

                grafikaPanel.LoadProject();
                grafikaPanel.Visible = true;
            }
            else
            {
                grafikaPanel.Visible = false;
                panelControler.Visible = true;
                slideList.Visible = true;
                trbGridSize.Visible = false;
                numGridSize.Visible = false;
                labGridSize.Visible = false;
                trbZoom.Visible = false;
                labelZoom.Visible = false;
                numZoom.Visible = false;
            }
            ResumeLayout();
        }

        private void ribbonMenu_ExpandedChanged(object sender, EventArgs e)
        {
            if (!ribbonMenu.Expanded)
            {
                ribbonMenu.Expanded = true;
            }
        }

        private void btnAdditionalActivitiesWindow_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                AdditionalActivitiesForm manage = new AdditionalActivitiesForm(grafikaPanel.ListControl.SelectedBase);
                manage.ShowDialog();
                grafikaPanel.LoadProject();
            }
        }

        private void chbShowTransparentObjects_Click(object sender, EventArgs e)
        {
            if (!chbShowTransparentObjects.Enabled)
            {
                return;
            }
            chbShowTransparentObjects.Checked = !chbShowTransparentObjects.Checked;
            chbShowTransparentObjects.OnCheckChanged(e);
        }

        private void chbShowTransparentObjects_CheckBoxCheckChanged(object sender, EventArgs e)
        {
            grafikaPanel.ListControl.ShowTransparentObjects = chbShowTransparentObjects.Checked;
            if (chbShowTransparentObjects.Checked)
            {
                chbShowTransparentTools.Enabled = true;
                chbShowTransparentTools.Checked = true;
                chbShowTransparentTools.OnCheckChanged(e);
            }
            else
            {
                chbShowTransparentTools.Checked = false;
                chbShowTransparentTools.OnCheckChanged(e);
                chbShowTransparentTools.Enabled = false;
            }
        }

        private void chbShowTransparentTools_Click(object sender, EventArgs e)
        {
            if (!chbShowTransparentTools.Enabled)
            {
                return;
            }
            chbShowTransparentTools.Checked = !chbShowTransparentTools.Checked;
            chbShowTransparentTools.OnCheckChanged(e);
        }

        private void chbShowTransparentTools_CheckBoxCheckChanged(object sender, EventArgs e)
        {
            grafikaPanel.ListControl.ShowTransparentTools = chbShowTransparentTools.Checked;
        }

        private void btnAssessment_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new AssessmentForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnChat_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new ChatForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnForum_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new ForumForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnMultipleChoice_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new MultipleChoiceForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnQuestionAndAnswer_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new QaForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnShareResources_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new ShareResourcesForm(obj);

            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnSubmitFiles_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new SubmitFilesForm(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnJavagrader_Click(object sender, EventArgs e)
        {
            var obj = grafikaPanel.ListControl.SelectedBase;
            if (obj == null)
            {
                return;
            }

            var form = new JavaGraderGui(obj);
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                grafikaPanel.LoadProject();
            }
        }

        private void btnGraphicsMove_Click(object sender, EventArgs e)
        {
            grafikaPanel.Canvas.SetListener(GrafikaCanvas.MouseListener.Move);
        }

        private void btnGraphicsConnect_Click(object sender, EventArgs e)
        {
            grafikaPanel.Canvas.SetListener(GrafikaCanvas.MouseListener.Connect);
        }

        private void btnSortInColumns_Click(object sender, EventArgs e)
        {
            grafikaPanel.CanvasSortType = GrafikaCanvas.SortType.Columns;
            btnSortInColumns.Checked = true;
            btnGraphicsAutoArrange_Click(sender, e);
        }

        private void btnSortInRows_Click(object sender, EventArgs e)
        {
            grafikaPanel.CanvasSortType = GrafikaCanvas.SortType.Rows;
            btnSortInRows.Checked = true;
            btnGraphicsAutoArrange_Click(sender, e);
        }

        private void btnSortRectangle_Click(object sender, EventArgs e)
        {
            grafikaPanel.CanvasSortType = GrafikaCanvas.SortType.Rectange;
            btnSortRectangle.Checked = true;
            btnGraphicsAutoArrange_Click(sender, e);
        }

        private void btnSortCircle_Click(object sender, EventArgs e)
        {
            grafikaPanel.CanvasSortType = GrafikaCanvas.SortType.Circle;
            btnSortCircle.Checked = true;
            btnGraphicsAutoArrange_Click(sender, e);
        }

        private void btnSortByObject_Click(object sender, EventArgs e)
        {
            grafikaPanel.CanvasSortType = GrafikaCanvas.SortType.ByObjects;
            btnSortByObject.Checked = true;
            btnGraphicsAutoArrange_Click(sender, e);
        }

        private void btnSortSnake_Click(object sender, EventArgs e)
        {
            grafikaPanel.CanvasSortType = GrafikaCanvas.SortType.Snake;
            btnSortSnake.Checked = true;
            btnGraphicsAutoArrange_Click(sender, e);
        }

        private void btnSortMaze_Click(object sender, EventArgs e)
        {
            grafikaPanel.CanvasSortType = GrafikaCanvas.SortType.Maze;
            btnSortMaze.Checked = true;
            btnGraphicsAutoArrange_Click(sender, e);
        }

        private void chbAutoArrange_Click(object sender, EventArgs e)
        {
            var args = new CancelEventArgs();
            chbAutoArrange.OnCheckChanging(args);
            if (args.Cancel)
            {
                return;
            }
            chbAutoArrange.Checked = !chbAutoArrange.Checked;
            chbAutoArrange.OnCheckChanged(e);
        }

        private void btnGraphicsAutoArrange_Click(object sender, EventArgs e)
        {
            if (grafikaPanel.Canvas.Items.Count > 0)
            {
                var result =
                    MessageBox.Show(
                        "Sve izmene koje ste učinili na platnu biće uklonjene \ni objekti će biti automatski raspoređeni.",
                        "Automatski sortirati slajdove?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                {
                    return;
                }
            }
            grafikaPanel.ArrangeCanvas();
            grafikaPanel.Canvas.SetListener(GrafikaCanvas.MouseListener.Move);
            CheckErrorsAndStatistics();
        }

        private void chbAutoArrange_CheckBoxCheckChanging(object sender, CancelEventArgs e)
        {
            if (!chbAutoArrange.Checked)
            {
                if (grafikaPanel.Canvas.Items.Count > 0)
                {
                    var result =
                        MessageBox.Show(
                            "Sve izmene koje ste učinili na platnu biće uklonjene \ni objekti će biti automatski raspoređeni svaki put kada učinite izmenu u projektu.",
                            "Automatski sortirati slajdove?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void chbAutoArrange_CheckBoxCheckChanged(object sender, EventArgs e)
        {
            grafikaPanel.AutoArrange = chbAutoArrange.Checked;
        }

        private void btnClearCanvas_Click(object sender, EventArgs e)
        {
            if (grafikaPanel.Canvas.Items.Count == 0)
            {
                return;
            }
            var result =
                    MessageBox.Show(
                        "Svi objekti koje ste dodali na platno biće uklonjeni.",
                        "Očistiti platno?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (result != DialogResult.OK)
            {
                return;
            }
            chbAutoArrange.Checked = false;
            chbAutoArrange.OnCheckChanged(e);
            grafikaPanel.Canvas.Clear();
            CheckErrorsAndStatistics();
        }
        private void btnCenterCanvas_Click(object sender, EventArgs e)
        {
            grafikaPanel.Canvas.CenterCoordinates();
            grafikaPanel.Canvas.Invalidate();
        }

        private void chbShowGrid_Click(object sender, EventArgs e)
        {
            chbShowGrid.Checked = !chbShowGrid.Checked;
            chbShowGrid.OnCheckChanged(e);
        }

        private void chbShowGrid_CheckBoxCheckChanged(object sender, EventArgs e)
        {
            grafikaPanel.Canvas.ShowGrid = chbShowGrid.Checked;
            if (chbShowGrid.Checked)
            {
                chbSnapToGrid.Enabled = true;
            }
            else
            {
                chbSnapToGrid.Checked = false;
                chbSnapToGrid.OnCheckChanged(e);
                chbSnapToGrid.Enabled = false;
            }
            grafikaPanel.Canvas.Invalidate();
        }

        private void chbSnapToGrid_Click(object sender, EventArgs e)
        {
            chbSnapToGrid.Checked = !chbSnapToGrid.Checked;
            chbSnapToGrid.OnCheckChanged(e);
        }

        private void chbSnapToGrid_CheckBoxCheckChanged(object sender, EventArgs e)
        {
            grafikaPanel.Canvas.SnapToGrid = chbSnapToGrid.Checked;
        }

        private void numGridSize_ValueChanged(object sender, EventArgs e)
        {
            trbGridSize.Value = (int)numGridSize.Value;
            grafikaPanel.Canvas.GridSize = (int)numGridSize.Value;
            if (grafikaPanel.Canvas.ShowGrid)
            {
                grafikaPanel.Canvas.Invalidate();
            }
        }

        private void trbGridSize_ValueChanged(object sender, EventArgs e)
        {
            numGridSize.Value = trbGridSize.Value;
        }

        private void numZoom_ValueChanged(object sender, EventArgs e)
        {
            trbZoom.Value = (int)numZoom.Value;

            var p = new Point(grafikaPanel.Canvas.Width/2, grafikaPanel.Canvas.Height/2);
            var startP = grafikaPanel.Canvas.TranslateOffset(p);
            grafikaPanel.Canvas.Zoom = ((float)numZoom.Value) / 100f;
            var endP = grafikaPanel.Canvas.TranslateOffset(p);

            grafikaPanel.Canvas.Offset = new Point(grafikaPanel.Canvas.Offset.X - startP.X + endP.X, grafikaPanel.Canvas.Offset.Y - startP.Y + endP.Y);
            grafikaPanel.Canvas.Invalidate();
        }

        private void trbZoom_Scroll(object sender, EventArgs e)
        {
            numZoom.Value = trbZoom.Value;
        }

        private bool btnGate_clicked = false;

        private bool btnGate_dragging = false;

        private Point btnGate_mouseLocation;

        private void btnGate_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project == null)
            {
                return;
            }
            grafikaPanel.Canvas.SetListener(GrafikaCanvas.MouseListener.Add, new LamsGate());
        }

        private void btnGate_MouseEnter(object sender, MouseEventArgs e)
        {
            btnGate_dragging = false;
            btnGate_clicked = false;
        }

        private void btnGate_MouseDown(object sender, MouseEventArgs e)
        {
            btnGate_mouseLocation = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                btnGate_clicked = true;
            }
        }

        private void btnGate_MouseUp(object sender, MouseEventArgs e)
        {
            btnGate_dragging = false;
            btnGate_clicked = false;
        }

        private void btnGate_MouseMove(object sender, MouseEventArgs e)
        {
            if (!btnGate_clicked || ProjectSingleton.Project == null || btnGate_dragging)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                int x = btnGate_mouseLocation.X - e.X;
                int y = btnGate_mouseLocation.Y - e.Y;
                if (x * x + y * y > 20)
                {
                    btnGate_dragging = true;
                    DoDragDrop(new LamsGate(), DragDropEffects.All);
                }
            }
        }

        private bool btnOptional_clicked = false;

        private bool btnOptional_dragging = false;

        private Point btnOptional_mouseLocation;

        private void btnOptional_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project == null)
            {
                return;
            }
            grafikaPanel.Canvas.SetListener(GrafikaCanvas.MouseListener.Add, new LamsOptional());
        }

        private void btnOptional_MouseEnter(object sender, MouseEventArgs e)
        {
            btnOptional_dragging = false;
            btnOptional_clicked = false;
        }

        private void btnOptional_MouseDown(object sender, MouseEventArgs e)
        {
            btnOptional_mouseLocation = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                btnOptional_clicked = true;
            }
        }

        private void btnOptional_MouseUp(object sender, MouseEventArgs e)
        {
            btnOptional_dragging = false;
            btnOptional_clicked = false;
        }

        private void btnOptional_MouseMove(object sender, MouseEventArgs e)
        {
            if (!btnOptional_clicked || ProjectSingleton.Project == null || btnOptional_dragging)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                int x = btnOptional_mouseLocation.X - e.X;
                int y = btnOptional_mouseLocation.Y - e.Y;
                if (x * x + y * y > 20)
                {
                    btnOptional_dragging = true;
                    DoDragDrop(new LamsOptional(), DragDropEffects.All);
                }
            }
        }


        private bool btnBranch_clicked = false;

        private bool btnBranch_dragging = false;

        private Point btnBranch_mouseLocation;

        private void btnBranch_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project == null)
            {
                return;
            }
            grafikaPanel.Canvas.SetListener(GrafikaCanvas.MouseListener.Add, new LamsBranch());
        }

        private void btnBranch_MouseEnter(object sender, MouseEventArgs e)
        {
            btnBranch_dragging = false;
            btnBranch_clicked = false;
        }

        private void btnBranch_MouseDown(object sender, MouseEventArgs e)
        {
            btnBranch_mouseLocation = new Point(e.X, e.Y);
            if (e.Button == MouseButtons.Left)
            {
                btnBranch_clicked = true;
            }
        }

        private void btnBranch_MouseUp(object sender, MouseEventArgs e)
        {
            btnBranch_dragging = false;
            btnBranch_clicked = false;
        }

        private void btnBranch_MouseMove(object sender, MouseEventArgs e)
        {
            if (!btnBranch_clicked || ProjectSingleton.Project == null || btnBranch_dragging)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                int x = btnBranch_mouseLocation.X - e.X;
                int y = btnBranch_mouseLocation.Y - e.Y;
                if (x * x + y * y > 20)
                {
                    btnBranch_dragging = true;
                    DoDragDrop(new LamsBranch(), DragDropEffects.All);
                }
            }
        }
    }
}
