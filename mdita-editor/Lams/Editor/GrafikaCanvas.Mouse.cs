using System;
using System.Drawing;
using System.Windows.Forms;

namespace mDitaEditor.Lams.Editor
{
    partial class GrafikaCanvas
    {
        private void GrafikaCanvas_MouseWheel(object sender, MouseEventArgs e)
        {
            var startMouse = TranslateOffset(e.Location);
            Zoom += Math.Sign(e.Delta) * (Zoom >= 1 ? 0.1f : 0.05f);
            MainForm.Instance.numZoom.Value = (int)Math.Round(Zoom * 100);
            var endMouse = TranslateOffset(e.Location);
            Offset = new Point(Offset.X - startMouse.X + endMouse.X, Offset.Y - startMouse.Y + endMouse.Y);
            Invalidate();
        }

        private void GrafikaCanvas_MouseEnter(object sender, EventArgs e)
        {
            Listener.MouseEnter();
        }

        private void GrafikaCanvas_MouseLeave(object sender, EventArgs e)
        {
            StopArrows();
            Listener.MouseLeave();
        }

        private void GrafikaCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var arrow = ArrowAt(e.Location);
                if (arrow != null)
                {
                    arrow.MouseDown();
                    return;
                }
            }
            var mouse = TranslateOffset(e.Location);
            var handled = Listener.MouseDown(e.Button, mouse);
            if (!handled && e.Button == MouseButtons.Right)
            {
                if (_menuOpen != null)
                {
                    _menuOpen.Close();
                }
                HoverConnection = ConnectionAt(mouse);
                HoverObject = ObjectAt(mouse);
                if (HoverConnection != null)
                {
                    MenuConnection.Show(Cursor.Position);
                    HoverObject = null;
                }
                else if (HoverObject != null)
                {
                    MenuObject.Show(Cursor.Position);
                }
                else
                {
                    MenuPanel.Show(Cursor.Position);
                }
                Invalidate();
            }
        }

        public void GrafikaCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (CheckArrows())
            {
                return;
            }
            if (e.Button == MouseButtons.None)
            {
                HoverArrow = ArrowAt(e.Location);
            }
            else
            {
                HoverArrow = null;
            }
            var mouse = TranslateOffset(e.Location);
            Listener.MouseMove(mouse);
            Invalidate();
        }

        public void GrafikaCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (_menuOpen != null)
            {
                return;
            }
            if (!StopArrows())
            {
                var mouse = TranslateOffset(e.Location);
                Listener.MouseUp(e.Button, mouse);
            }
        }

        private void GrafikaCanvas_DoubleClick(object sender, EventArgs e)
        {
            if (HoverObject != null)
            {
                OpenObject_Click(sender, e);
            }
            else if (HoverConnection != null)
            {
                MenuConnection.Show(Cursor.Position);
            }
            Listener.MouseDown(MouseButtons.Right, PointToClient(Cursor.Position));
            Listener.MouseUp(MouseButtons.Left, PointToClient(Cursor.Position));
        }

        private void GrafikaCanvas_MouseHover(object sender, EventArgs e)
        {

        }

        private void GrafikaCanvas_DragEnter(object sender, DragEventArgs e)
        {
            var obj = e.Data.GetData(e.Data.GetFormats()[0]) as IGrafikaObject;
            if (obj != null)
            {
                e.Effect = DragDropEffects.Move;
                SetListener(MouseListener.Add, obj);
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void GrafikaCanvas_DragLeave(object sender, EventArgs e)
        {
            SetListener(MouseListener.Move);
            Invalidate();
        }

        private void GrafikaCanvas_DragDrop(object sender, DragEventArgs e)
        {
            Listener.MouseDown(MouseButtons.Left, Point.Empty);
            Listener.MouseUp(MouseButtons.Left, TranslateOffset(PointToClient(Cursor.Position)));
        }

        private void GrafikaCanvas_DragOver(object sender, DragEventArgs e)
        {
            Listener.MouseMove(TranslateOffset(PointToClient(Cursor.Position)));
        }
    }
}
