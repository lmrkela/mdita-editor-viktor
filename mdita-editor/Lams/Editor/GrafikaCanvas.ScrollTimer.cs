using System;
using System.Drawing;
using System.Windows.Forms;

namespace mDitaEditor.Lams.Editor
{
    partial class GrafikaCanvas
    {
        public class ScrollTimer : Timer
        {
            private Point _scrollJump;

            public GrafikaCanvas Parent { get; private set; }

            public ScrollTimer(GrafikaCanvas parent)
            {
                Parent = parent;
                Interval = 20;
                Tick += ScrollTimer_Tick;
            }

            private void ScrollTimer_Tick(object sender, EventArgs e)
            {
                if (Parent.ClientRectangle.Contains(Parent.PointToClient(Cursor.Position)))
                {
                    Point p = Parent.Offset;
                    Parent.Offset = new Point(p.X - _scrollJump.X, p.Y - _scrollJump.Y);
                    Parent.Invalidate();
                }
                else
                {
                    _scrollJump = Point.Empty;
                    Stop();
                }
            }

            public void Start(Point scrollJump)
            {
                _scrollJump = scrollJump;
                if (scrollJump.Y < 0)
                {
                    if (scrollJump.X > 0)
                    {
                        Parent.Cursor = Cursors.PanNE;
                    }
                    else if (scrollJump.X == 0)
                    {
                        Parent.Cursor = Cursors.PanNorth;
                    }
                    else
                    {
                        Parent.Cursor = Cursors.PanNW;
                    }
                }
                else if (scrollJump.Y == 0)
                {
                    if (scrollJump.X > 0)
                    {
                        Parent.Cursor = Cursors.PanEast;
                    }
                    else if (scrollJump.X == 0)
                    {
                        Stop();
                        return;
                    }
                    else
                    {
                        Parent.Cursor = Cursors.PanWest;
                    }
                }
                else
                {
                    if (scrollJump.X > 0)
                    {
                        Parent.Cursor = Cursors.PanSE;
                    }
                    else if (scrollJump.X == 0)
                    {
                        Parent.Cursor = Cursors.PanSouth;
                    }
                    else
                    {
                        Parent.Cursor = Cursors.PanSW;
                    }
                }
                if (!Enabled)
                {
                    Start();
                }
            }

            public new void Stop()
            {
                base.Stop();
                Parent.Listener.SetParentCursor(Parent.TranslateOffset(Cursor.Position));
            }
        }
    }
}
