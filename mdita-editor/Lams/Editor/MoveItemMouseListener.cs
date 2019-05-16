using System;
using System.Drawing;
using System.Windows.Forms;

namespace mDitaEditor.Lams.Editor
{
    class MoveItemMouseListener : GrafikaMouseListener
    {
        public MoveItemMouseListener(GrafikaCanvas parent) : base(parent)
        { }

        protected override Cursor GetCursor(Point mouse)
        {
            if (_mouseObject != null)
            {
                var pan = Parent.ParentPanel.panelDelete;
                if (pan.ClientRectangle.Contains(pan.PointToClient(Cursor.Position)))
                {
                    return Cursors.Hand;
                }
                return Cursors.SizeAll;
            }
            return base.GetCursor(mouse);
        }

        private bool _mouseDown = false;
        private Point _mouseStart = Point.Empty;
        private Point _pointStart = Point.Empty;
        private GrafikaItem _mouseObject = null;
        private GrafikaConnection _mouseConnection = null;

        public override bool MouseLeave()
        {
            if (MousePressed)
            {
                return base.MouseLeave();
            }
            Parent.ParentPanel.panelDelete.Visible = false;
            _mouseObject = null;
            _mouseDown = false;
            StopScroll();
            return true;
        }

        private Point _prevPoint = Point.Empty;

        public override bool MouseMove(Point mouse)
        {
            if (MousePressed)
            {
                return base.MouseMove(mouse);
            }
            if (_mouseDown)
            {
                var moveX = mouse.X - _mouseStart.X;
                var moveY = mouse.Y - _mouseStart.Y;
                if (Math.Sqrt(moveX*moveX + moveY*moveY) < 10)
                {
                    if (_mouseObject != null)
                    {
                        _mouseObject.Location = _pointStart;
                    }
                    return true;
                }
                if (_mouseObject != null)
                {
                    var pan = Parent.ParentPanel.panelDelete;
                    pan.Visible = true;
                    //var toolTip = Parent.ParentPanel.toolTip;
                    //if (pan.ClientRectangle.Contains(pan.PointToClient(Cursor.Position)))
                    //{
                    //    var p = MainForm.Instance.PointToClient(Cursor.Position);
                    //    if(string.IsNullOrEmpty(toolTip.GetToolTip(Parent.ParentPanel.panelDelete)) || GrafikaUtils.Distance(_prevPoint, p) > 20)
                    //    {
                    //        _prevPoint = p;
                    //        toolTip.UseAnimation = false;
                    //        toolTip.UseFading = true;
                    //        toolTip.ToolTipTitle = "Uklonite slajd sa platna.";
                    //        toolTip.Show("Ovo neće obrisati objekat iz editora.", MainForm.Instance, p);
                    //    }
                    //}
                    //else
                    //{
                    //    toolTip.Hide(MainForm.Instance);
                    //}
                    if (Parent.SnapToGrid)
                    {
                        _mouseObject.Location = new Point(Parent.TranslateToGrid(_pointStart.X + moveX),
                            Parent.TranslateToGrid(_pointStart.Y + moveY));
                    }
                    else
                    {
                        _mouseObject.Location = new Point(_pointStart.X + moveX, _pointStart.Y + moveY);
                    }
                    ScrollMeMaybe();
                    Parent.Invalidate();
                    return true;
                }
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
                return false;
            }
            return base.MouseMove(mouse);
        }

        public override bool MouseDown(MouseButtons btn, Point mouse)
        {
            if (btn == MouseButtons.Left)
            {
                _mouseStart = mouse;
                _mouseConnection = Parent.ConnectionAt(_mouseStart);
                if (_mouseConnection != null)
                {
                    _mouseObject = null;
                    _mouseDown = true;
                }
                else
                {
                    _mouseObject = Parent.ObjectAt(_mouseStart);
                    if (_mouseObject != null)
                    {
                        _pointStart = _mouseObject.Location;
                        _mouseDown = true;
                    }
                    else
                    {
                        return base.MouseDown(btn, mouse);
                    }
                }
            }
            else if (btn == MouseButtons.Right)
            {
                if (MousePressed)
                {
                    return base.MouseDown(btn, mouse);
                }
                if (!_mouseDown)
                {
                    return false;
                }
                _mouseConnection = null;
                if (_mouseObject != null)
                {
                    _mouseObject.Location = _pointStart;
                    _mouseObject = null;
                }
                _mouseDown = false;
            }
            return true;
        }

        public override bool MouseUp(MouseButtons btn, Point mouse)
        {
            //Parent.ParentPanel.toolTip.Hide(MainForm.Instance);
            if (MousePressed)
            {
                return base.MouseUp(btn, mouse);
            }
            if (btn == MouseButtons.Left)
            {
                if (Parent.HoverObject != null)
                {
                    var pan = Parent.ParentPanel.panelDelete;
                    if (pan.ClientRectangle.Contains(pan.PointToClient(Cursor.Position)))
                    {
                        Parent.DeleteObject_Click(null, null);
                        Parent.Invalidate();
                    }
                    pan.Visible = false;
                }
                _mouseDown = false;
                _mouseObject = null;
                StopScroll();
                return true;
            }
            return false;
        }
    }
}
