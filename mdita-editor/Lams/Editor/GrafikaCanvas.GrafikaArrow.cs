using System.Drawing;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Editor
{
    partial class GrafikaCanvas {
        public class GrafikaArrow
        {
            public GrafikaCanvas Parent { get; private set; }

            public Image Image { get; private set; }

            public Point Location
            {
                get { return Bounds.Location; }
                set { Bounds = new Rectangle(value, Image.Size); }
            }

            public Rectangle Bounds { get; private set; }

            public enum EDirection
            {
                Up,
                Down,
                Left,
                Right
            }

            private EDirection _direction;

            public EDirection Direction
            {
                get { return _direction; }
                set
                {
                    _direction = value;
                    switch (_direction)
                    {
                        case EDirection.Up:
                            Image = Resources.arrow_up;
                            _scrollJump = new Point(0, -30);
                            break;
                        case EDirection.Down:
                            Image = Resources.arrow_down;
                            _scrollJump = new Point(0, 30);
                            break;
                        case EDirection.Left:
                            Image = Resources.arrow_left;
                            _scrollJump = new Point(-30, 0);
                            break;
                        case EDirection.Right:
                            Image = Resources.arrow_right;
                            _scrollJump = new Point(30, 0);
                            break;
                    }
                }
            }

            private Point _scrollJump = Point.Empty;

            public bool ScrollStarted { get; private set; }

            public GrafikaArrow(GrafikaCanvas parent, EDirection direction)
            {
                Parent = parent;
                Direction = direction;
            }

            public void MouseDown()
            {
                if (Parent == null)
                {
                    return;
                }
                Parent.Scroll.Start(_scrollJump);
                ScrollStarted = true;
                Parent.Invalidate();
            }

            public bool MouseUp()
            {
                if (Parent == null)
                {
                    return false;
                }
                if (ScrollStarted)
                {
                    Parent.Scroll.Stop();
                    ScrollStarted = false;
                    Parent.Invalidate();
                    return true;
                }
                return false;
            }

            public void Draw(Graphics g)
            {
                g.DrawImage(Image, Location);
                if (ScrollStarted)
                {
                    g.DrawImage(Image, Location);
                }
            }
        }
    }
}
