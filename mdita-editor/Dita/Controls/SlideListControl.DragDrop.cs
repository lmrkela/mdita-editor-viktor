using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Controls
{
    partial class SlideListControl
    {
        public void StartDrag(LearningContent draggedContent)
        {
            bool showSubobjects = draggedContent.Parent != null;
            SuspendLayout();
            foreach (var control in SlideList)
            {
                if (control.Slide is Section)
                {
                    // Iz nekog razloga ne radi ako stavim da nisu Visible,
                    // pa sam smanjio velicinu i margine na 0, sto izgleda isto.
                    control.Height = 0;
                    control.Margin = new Padding();
                    //control.Visible = false;
                }
                else if (!showSubobjects && control.Slide is LearningContent && ((LearningContent)control.Slide).Parent != null)
                {
                    control.Height = 0;
                    control.Margin = new Padding();
                    //control.Visible = false;
                }
            }
            ResumeLayout();
            ShowSlide(draggedContent, true);
        }

        public void StopDrag()
        {
            SuspendLayout();
            foreach (var control in SlideList)
            {
                control.Height = 125;
                control.Margin = new Padding(2);
                //control.Visible = true;
            }
            ResumeLayout();
        }

        private readonly Timer _scrollTimer;

        private int _scrollJump;

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            if (ClientRectangle.Contains(PointToClient(MousePosition)))
            {
                Point p = AutoScrollPosition;
                AutoScrollPosition = new Point(-p.X, -p.Y + _scrollJump);
            }
            else
            {
                _scrollJump = 0;
                _scrollTimer.Stop();
            }
        }

        private SlidePreviewControl _destination = null;

        private void SlideListControl_DragEnter(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(typeof(SlidePreviewControl)) as SlidePreviewControl;
            if (data != null)
            {
                e.Effect = DragDropEffects.Move;
                if (data.Slide is LearningContent)
                {
                    StartDrag((LearningContent) data.Slide);
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SlideListControl_DragLeave(object sender, EventArgs e)
        {
            var data = SlidePreviewControl.DraggedControl;
            if (data != null)
            {
                Controls.SetChildIndex(data, _previewList.IndexOf(data));
                if (data.Slide is LearningContent)
                {
                    StopDrag();
                }
            }
            _destination = null;
        }

        private void SlideListControl_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(SlidePreviewControl)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var data = (SlidePreviewControl) e.Data.GetData(typeof(SlidePreviewControl));
            Point p = PointToClient(new Point(e.X, e.Y));
            var destination = GetChildAtPoint(p) as SlidePreviewControl;

            if (destination == null || (data.Slide is LearningContent && destination.Slide is Section))
            {
                return;
            }
            if (destination != null)
            {
                //Debug.WriteLine("MOVING " + Controls.IndexOf(data) + " to " + Controls.IndexOf(destination));
                Controls.SetChildIndex(data, Controls.IndexOf(destination));
                _destination = null;
                for (int i = Controls.IndexOf(data) - 1; i >= 0; --i)
                {
                    if (Controls[i].Height > 0)
                    {
                        _destination = Controls[i] as SlidePreviewControl;
                        break;
                    }
                }
                e.Effect = CheckMove(data, _destination) ? DragDropEffects.Move : DragDropEffects.Scroll;
            }

            if (p.Y < 70)
            {
                _scrollJump = -70 + p.Y;
                _scrollTimer.Start();
            }
            else if (p.Y >= ClientSize.Height - 70)
            {
                _scrollJump = 70 - ClientSize.Height + p.Y;
                _scrollTimer.Start();
            }
            else
            {
                _scrollJump = 0;
                _scrollTimer.Stop();
            }
        }

        private void SlideListControl_DragDrop(object sender, DragEventArgs e)
        {
            _scrollTimer.Stop();
            SlidePreviewControl.DraggedControl = null;

            if (!e.Data.GetDataPresent(typeof(SlidePreviewControl)))
            {
                e.Effect = DragDropEffects.None;
                return;
            }
            var data = (SlidePreviewControl)e.Data.GetData(typeof(SlidePreviewControl));
            var ddd = data.Slide;
            
            if (!CheckMove(data, _destination))
            {
                Controls.SetChildIndex(data, _previewList.IndexOf(data));
                StopDrag();
                return;
            }

            bool result = false;
            if (data.Slide is Section)
            {
                var section = (Section)data.Slide;
                if (_destination.Slide is Section)
                {
                    var destSection = (Section) _destination.Slide;
                    var destIndex = destSection.Parent.LearningBody.Sections.IndexOf(destSection) + 1;
                    result = MoveSection(section, destSection.Parent, destIndex);
                }
                else if (_destination.Slide is LearningBase)
                {
                    result = MoveSection(section, (LearningBase) _destination.Slide, 0);
                }
            }
            else if (data.Slide is LearningContent)
            {
                var content = (LearningContent) data.Slide;
                var destContent = _destination.Slide as LearningContent;
                if (content.Parent == null)
                {
                    var destIndex = MainForm.Instance.Project.LearningContents.IndexOf(destContent) + 1;
                    result = MoveContent(content, destIndex);
                }
                else
                {
                    var dest = (destContent.Parent == null) ? destContent : destContent.Parent;
                    var destIndex = dest.SubObjects.IndexOf(destContent) + 1;
                    result = MoveContent(content, dest, destIndex);
                }
            }
            if (!result)
            {
                Controls.SetChildIndex(data, _previewList.IndexOf(data));
            }

            if (ddd is LearningContent)
            {
                StopDrag();
                ShowSlide(ddd, true);
            }
        }
        private static bool CheckMove(SlidePreviewControl data, SlidePreviewControl destination)
        {
            if (data == null || destination == null || data == destination)
            {
                return false;
            }
            if (data.Slide is LearningContent && ((LearningContent)data.Slide).Parent == null)
            {
                if (destination.Slide is LearningSummary)
                {
                    return false;
                }
            }
            else
            {
                if (destination.Slide is LearningOverview || destination.Slide is LearningSummary)
                {
                    return false;
                }
            }
            if (destination.Slide is Section)
            {
                var section = (Section)destination.Slide;
                if (section.Parent is LearningOverview || section.Parent is LearningSummary)
                {
                    return false;
                }
            }
            return true;
        }

        private static bool MoveSection(Section section, LearningBase destination, int index)
        {
            int startIndex = section.Parent.LearningBody.Sections.IndexOf(section);
            //Debug.WriteLine("Move section {0} to {1} - {2}", section.Title, destination.TitleDescription, index);

            if (destination == section.Parent)
            {
                if (index > startIndex)
                {
                    --index;
                }
                if (startIndex == index)
                {
                    return false;
                }
            }

            DitaClipboard.AddSectionMovedState(section, section.Parent, startIndex, destination, index);
            section.MoveTo(destination, index);
            
            MainForm.Instance.RecreateMenu();
            return true;
        }

        private static bool MoveContent(LearningContent content, int index)
        {
            int startIndex = MainForm.Instance.Project.LearningContents.IndexOf(content);
            //Debug.WriteLine("Move content {0} to {1}", content.TitleDescription, index);

            if (index > startIndex)
            {
                --index;
            }
            if (startIndex == index)
            {
                return false;
            }

            DitaClipboard.AddContentMovedState(content, startIndex, index);
            content.MoveTo(index);

            MainForm.Instance.RecreateMenu();
            if (ProjectSingleton.SelectedContent == content)
            {
                MainForm.Instance.OpenSlide(content);
            }
            return true;
        }

        private static bool MoveContent(LearningContent content, LearningContent destination, int index)
        {
            int startIndex = content.Parent.SubObjects.IndexOf(content);
            //Debug.WriteLine("Move section {0} to {1} - {2}", content.TitleDescription, destination.TitleDescription, index);

            if (destination == content.Parent)
            {
                if (index > startIndex)
                {
                    --index;
                }
                if (startIndex == index)
                {
                    return false;
                }
            }

            DitaClipboard.AddContentMovedState(content, content.Parent, startIndex, destination, index);
            content.MoveTo(destination, index);

            MainForm.Instance.RecreateMenu();
            if (ProjectSingleton.SelectedContent == content)
            {
                MainForm.Instance.OpenSlide(content);
            }
            return true;
        }
    }
}