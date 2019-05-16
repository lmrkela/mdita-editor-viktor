using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Editor
{
    abstract class GrafikaMouseListener
    {
        public static readonly Cursor CursorOpen = new Cursor(Resources.hand_open.GetHicon());
        public static readonly Cursor CursorGrab = new Cursor(Resources.hand_grab.GetHicon());

        protected bool MouseScroll { get; set; }

        protected void ScrollMeMaybe()
        {
            MouseScroll = true;

            Point p = Parent.PointToClient(Cursor.Position);
            Point scrollJump = Point.Empty;

            if (!Parent.ClientRectangle.Contains(p))
            {
                Parent.Scroll.Stop();
                return;
            }

            if (p.X < ScrollOffset.X)
            {
                scrollJump.X = (-ScrollOffset.X + p.X)/4;
            }
            else if (p.X >= Parent.ClientSize.Width - ScrollOffset.X)
            {
                scrollJump.X = (ScrollOffset.X - Parent.ClientSize.Width + p.X)/4;
            }
            else
            {
                scrollJump.X = 0;
            }

            if (p.Y < ScrollOffset.Y)
            {
                scrollJump.Y = (-ScrollOffset.Y + p.Y)/4;
            }
            else if (p.Y >= Parent.ClientSize.Height - ScrollOffset.Y)
            {
                scrollJump.Y = (ScrollOffset.Y - Parent.ClientSize.Height + p.Y)/4;
            }
            else
            {
                scrollJump.Y = 0;
            }

            if (scrollJump != Point.Empty)
            {
                Parent.Scroll.Start(scrollJump);
            }
            else
            {
                Parent.Scroll.Stop();
            }
        }

        protected void StopScroll()
        {
            MouseScroll = false;
            Parent.Scroll.Stop();
        }

        public GrafikaCanvas Parent { get; private set; }

        protected bool MousePressed { get; private set; }
        private Point _mouseStart = Point.Empty;
        private Point _pointStart = Point.Empty;

        protected GrafikaMouseListener(GrafikaCanvas parent)
        {
            Parent = parent;
            CalculateSize();
        }

        public void SetParentCursor(Point mouse)
        {
            Cursor cursor;
            if (MousePressed)
            {
                cursor = CursorGrab;
            }
            else
            {
                cursor = GetCursor(mouse);
            }

            if (Parent.Cursor != cursor)
            {
                Parent.Cursor = cursor;
            }
        }

        protected virtual Cursor GetCursor(Point mouse)
        {
            var conn = Parent.ConnectionAt(mouse);
            if (conn == null)
            {
                var obj = Parent.ObjectAt(mouse);
                if (obj == null)
                {
                    return CursorOpen;
                }
            }
            return Cursors.Arrow;
        }

        public virtual bool MouseEnter()
        {
            return false;
        }

        public virtual bool MouseLeave()
        {
            MousePressed = false;
            StopScroll();
            return true;
        }

        public virtual bool MouseMove(Point mouse)
        {
            var moveX = mouse.X - _mouseStart.X;
            var moveY = mouse.Y - _mouseStart.Y;
            if (MousePressed)
            {
                Parent.Offset = new Point(moveX + Parent.Offset.X, moveY + Parent.Offset.Y);
                Parent.Invalidate();
            }
            else
            {
                var conn = Parent.ConnectionAt(mouse);
                if (Parent.HoverConnection != conn)
                {
                    Parent.HoverConnection = conn;
                    Parent.Invalidate();
                }
                if (conn != null)
                {
                    if (Parent.HoverObject != null)
                    {
                        Parent.HoverObject = null;
                        Parent.Invalidate();
                    }
                }
                else
                {
                    var obj = Parent.ObjectAt(mouse);
                    if (Parent.HoverObject != obj)
                    {
                        Parent.HoverObject = obj;
                        Parent.Invalidate();
                    }
                }
                Parent.Cursor = GetCursor(mouse);//Parent.HoverConnection == null && Parent.HoverObject == null && Parent.HoverArrow == null ? CursorOpen : Cursors.Arrow);
            }
            return true;
        }

        public virtual bool MouseHover(Point mouse)
        {
            return false;
        }

        public virtual bool MouseDown(MouseButtons btn, Point mouse)
        {
            if (btn == MouseButtons.Left)
            {
                _mouseStart = mouse;
                _pointStart = Parent.Offset;
                MousePressed = true;
                Parent.Cursor = CursorGrab;
            }
            else if (btn == MouseButtons.Right)
            {
                if (!MousePressed)
                {
                    return false;
                }
                Parent.Offset = _pointStart;
                MousePressed = false;
            }
            return true;
        }

        public virtual bool MouseUp(MouseButtons btn, Point mouse)
        {
            if (btn == MouseButtons.Left)
            {
                MousePressed = false;
                Parent.Cursor = CursorOpen;
                StopScroll();
                return true;
            }
            return false;
        }

        public virtual bool DoubleClick(MouseButtons btn, Point mouse)
        {
            return false;
        }

        private Rectangle RectScrollUp;
        private Rectangle RectScrollDown;
        private Rectangle RectScrollLeft;
        private Rectangle RectScrollRight;

        private Point PointArrowUp;
        private Point PointArrowDown;
        private Point PointArrowLeft;
        private Point PointArrowRight;

        private Point ScrollOffset;

        public void CalculateSize()
        {
            var w = Parent.Width;
            var h = Parent.Height;

            ScrollOffset = new Point(w/12, h/12);
            if (ScrollOffset.X < 40)
            {
                ScrollOffset.X = 40;
            }
            if (ScrollOffset.Y < 40)
            {
                ScrollOffset.Y = 40;
            }

            RectScrollUp = new Rectangle(0, 0, ScrollOffset.X, h);
            RectScrollDown = new Rectangle(w - ScrollOffset.X, 0, ScrollOffset.X, h);
            RectScrollLeft = new Rectangle(0, 0, w, ScrollOffset.Y);
            RectScrollRight = new Rectangle(0, h - ScrollOffset.Y, w, ScrollOffset.Y);
            PointArrowUp = new Point(w / 2 - 20, ScrollOffset.Y/2 - 20);
            PointArrowDown = new Point(w / 2 - 20, h - ScrollOffset.Y / 2 - 20);
            PointArrowLeft = new Point(ScrollOffset.X/2 - 20, h / 2 - 20);
            PointArrowRight = new Point(w - ScrollOffset.X/2 - 20, h / 2 - 20);

        }

        private static Brush ScrollBrush = new SolidBrush(Color.FromArgb(50, Color.DarkGray));

        public virtual void Draw(Graphics g)
        {
            if (!MouseScroll)
            {
                return;
            }
            var transform = g.Transform.Clone();
            g.ResetTransform();

            g.FillRectangle(ScrollBrush, RectScrollUp);
            g.FillRectangle(ScrollBrush, RectScrollDown);
            g.FillRectangle(ScrollBrush, RectScrollLeft);
            g.FillRectangle(ScrollBrush, RectScrollRight);
            g.DrawImage(Resources.arrow_up, PointArrowUp);
            g.DrawImage(Resources.arrow_down, PointArrowDown);
            g.DrawImage(Resources.arrow_left, PointArrowLeft);
            g.DrawImage(Resources.arrow_right, PointArrowRight);

            g.Transform = transform;
        }
    }
}
