using System.Drawing;
using System.Windows.Forms;

namespace mDitaEditor.Lams.Editor
{
    class ConnectMouseListener : GrafikaMouseListener
    {
        public ConnectMouseListener(GrafikaCanvas parent) : base(parent)
        {}

        protected override Cursor GetCursor(Point mouse)
        {
            if (_mouseObject != null || Parent.HoverObject != null)
            {
                return Cursors.Cross;
            }
            if (_mouseConnection != null)
            {
                return Cursors.Arrow;
            }
            return base.GetCursor(mouse);
        }

        private GrafikaConnection _mouseConnection = null;
        private GrafikaItem _mouseObject = null;
        private bool _holdObject = false;
        private Point _mouseLocation = Point.Empty;

        public override bool MouseMove(Point mouse)
        {
            if (!MousePressed)
            {
                _mouseLocation = mouse;
                if (_mouseConnection != null)
                {
                    ScrollMeMaybe();
                    foreach (var p in _mouseConnection.StartItem.Edges)
                    {
                        if (GrafikaUtils.Distance(mouse, p) < 20)
                        {
                            _mouseConnection.StartPoint = p;
                            return true;
                        }
                    }
                    foreach (var p in _mouseConnection.EndItem.Edges)
                    {
                        if (GrafikaUtils.Distance(mouse, p) < 20)
                        {
                            _mouseConnection.EndPoint = p;
                            return true;
                        }
                    }
                    return true;
                }
                if (_mouseObject != null)
                {
                    Parent.HoverObject = Parent.ObjectAt(mouse);
                    ScrollMeMaybe();
                    Parent.Invalidate();
                    return true;
                }
            }
            return base.MouseMove(mouse);
        }

        public override bool MouseDown(MouseButtons btn, Point mouse)
        {
            if (btn == MouseButtons.Left)
            {
                _mouseConnection = Parent.ConnectionAt(mouse);
                if (_mouseConnection != null)
                {
                    _mouseObject = null;
                }
                else
                {
                    var obj = Parent.ObjectAt(mouse);
                    if (_mouseObject != null && _mouseObject != obj)
                    {
                        _holdObject = true;
                    }
                    else
                    {
                        if (obj != null)
                        {
                            _mouseObject = obj;
                            _holdObject = false;
                        }
                        else
                        {
                            base.MouseDown(btn, mouse);
                        }
                    }
                    Parent.Invalidate();
                    return true;
                }
            }
            else if(btn == MouseButtons.Right)
            {
                _mouseConnection = null;
                if (_mouseObject != null)
                {
                    _mouseObject = null;
                    Parent.Invalidate();
                    return true;
                }
                return base.MouseDown(btn, mouse);
            }
            return false;
        }

        public override bool MouseUp(MouseButtons btn, Point mouse)
        {
            if (MousePressed)
            {
                return base.MouseUp(btn, mouse);
            }
            StopScroll();
            if (btn == MouseButtons.Left)
            {
                _mouseConnection = null;
                if (_mouseObject != null)
                {
                    var obj = Parent.ObjectAt(mouse);
                    if (obj != _mouseObject)
                    {
                        if (obj != null)
                        {
                            _mouseObject.Next = obj;
                            MainForm.Instance.CheckErrorsAndStatistics();
                        }
                        _mouseObject = _holdObject ? obj : null;
                    }
                    Parent.Invalidate();
                    return true;
                }
            }
            return false;
        }

        
        public override void Draw(Graphics g)
        {
            base.Draw(g);
            if (_mouseObject != null)
            {
                GrafikaConnection.ConnectionPen.Color = Color.Black;
                g.TranslateTransform(0, 1);
                g.DrawLine(GrafikaConnection.ConnectionPen, _mouseObject.Center, Parent.HoverObject != null && Parent.HoverObject != _mouseObject ? Parent.HoverObject.Center : _mouseLocation);

                GrafikaConnection.ConnectionPen.Color = GrafikaUtils.HighlightYellow;
                g.TranslateTransform(0, -1);
                g.DrawLine(GrafikaConnection.ConnectionPen, _mouseObject.Center, Parent.HoverObject != null && Parent.HoverObject != _mouseObject ? Parent.HoverObject.Center : _mouseLocation);
            }
        }
    }
}
