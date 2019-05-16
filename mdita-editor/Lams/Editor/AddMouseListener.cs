using System.Drawing;
using System.Windows.Forms;

namespace mDitaEditor.Lams.Editor
{
    class AddMouseListener : GrafikaMouseListener
    {
        public AddMouseListener(GrafikaCanvas parent) : base(parent)
        {}

        private GrafikaItem _newObject;
        public GrafikaItem NewObject
        {
            get
            {
                return _newObject;
            }
            set
            {
                _newObject = value;
                if (_newObject != null)
                {
                    _centerPoint = new Point(_newObject.Width/2, _newObject.Height/2);
                }
                else
                {
                    Parent.SetListener(GrafikaCanvas.MouseListener.Move);
                }
            }
        }

        private Point _centerPoint = Point.Empty;
        private bool _mouseDown = false;

        public override bool MouseMove(Point mouse)
        {
            if (NewObject != null)
            {
                var location = new Point(mouse.X - _centerPoint.X, mouse.Y - _centerPoint.Y);
                NewObject.Location = Parent.SnapToGrid ? Parent.TranslateToGrid(location) : location;
                ScrollMeMaybe();
                Parent.Invalidate();
                return true;
            }
            return false;
        }

        public override bool MouseDown(MouseButtons btn, Point mouse)
        {
            if (btn == MouseButtons.Left)
            {
                if (NewObject != null)
                {
                    if (!(NewObject is GrafikaBranchStartItem))
                    {
                        Parent.Items.Add(NewObject);
                        Parent.Invalidate();
                    }
                    _mouseDown = true;
                    return true;
                }
            }
            else if (btn == MouseButtons.Right)
            {
                _mouseDown = false;
                if (NewObject != null)
                {
                    Parent.Items.Remove(NewObject);
                    NewObject = null;
                    Parent.Invalidate();
                    return true;
                }
            }
            return false;
        }

        public override bool MouseUp(MouseButtons btn, Point mouse)
        {
            StopScroll();
            if (btn == MouseButtons.Left)
            {
                _mouseDown = false;
                if (NewObject != null)
                {
                    var branch = NewObject as GrafikaBranchStartItem;
                    if (branch != null)
                    {
                        branch.Initialized = true;
                        NewObject = new GrafikaBranchEndItem(branch);
                        NewObject.Initialized = false;
                    }
                    else
                    {
                        var branchEnd = NewObject as GrafikaBranchEndItem;
                        if (branchEnd != null)
                        {
                            Parent.Items.Add(branchEnd.StartItem);
                            var branchStart = branchEnd.StartItem;
                            if (new Rectangle(branchEnd.X - 5, branchEnd.Y - 5, 10, 10).IntersectsWith(new Rectangle(branchStart.X - 5, branchStart.Y - 5, 10, 10)))
                            {
                                branchEnd.Location = new Point(branchEnd.StartItem.X + branchEnd.StartItem.Width + 20, branchEnd.StartItem.Y);
                            }
                        }
                        else
                        {
                            Parent.ParentPanel.ListControl.HideObject(NewObject.GrafikaObject);
                        }
                        NewObject.Initialized = true;
                        NewObject = null;
                        MainForm.Instance.CheckErrorsAndStatistics();
                    }
                    Parent.Invalidate();
                    return true;
                }
            }
            return false;
        }

        public override bool MouseLeave()
        {
            StopScroll();
            return true;
        }

        public override void Draw(Graphics g)
        {
            if (NewObject != null && !_mouseDown)
            {
                var branchEnd = NewObject as GrafikaBranchEndItem;
                if (branchEnd != null)
                {
                    branchEnd.StartItem.Draw(g);
                }
                NewObject.Draw(g);
            }
        }
    }
}
