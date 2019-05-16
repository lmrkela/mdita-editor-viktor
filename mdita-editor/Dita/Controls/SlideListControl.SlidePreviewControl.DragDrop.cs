using System.Windows.Forms;

namespace mDitaEditor.Dita.Controls
{
    partial class SlideListControl
    {
        partial class SlidePreviewControl
        {
            private static SlidePreviewControl _draggedControl;
            public static SlidePreviewControl DraggedControl
            {
                get { return _draggedControl; }
                set
                {
                    if (_draggedControl != null)
                    {
                        _draggedControl._isDragging = false;
                        _draggedControl.Invalidate();
                    }
                    _draggedControl = value;
                    if (_draggedControl != null)
                    {
                        _draggedControl._isDragging = true;
                        if (_draggedControl.Slide is LearningContent)
                        {
                            _draggedControl._parentList.ShowSlide(_draggedControl.Slide, true);
                        }
                        _draggedControl.Invalidate();
                    }
                }
            }

            private bool _allowDrag = false;
            private bool _isDragging = false;

            private readonly int DDradius = 40;
            private int _mX = 0;
            private int _mY = 0;

            private void SlidePreviewControl_MouseDown(object sender, MouseEventArgs e)
            {
                _mX = e.X;
                _mY = e.Y;
            }

            private void SlidePreviewControl_MouseMove(object sender, MouseEventArgs e)
            {
                if (!_allowDrag || _isDragging)
                {
                    return;
                }
                if (e.Button == MouseButtons.Left)
                {
                    int x = _mX - e.X;
                    int y = _mY - e.Y;
                    if (x*x + y*y > DDradius)
                    {
                        DraggedControl = this;
                        DoDragDrop(this, DragDropEffects.All);
                    }
                }
            }

            private static void SlidePreviewControl_MouseUp(object sender, MouseEventArgs e)
            {
                DraggedControl = null;
            }
        }
    }
}
