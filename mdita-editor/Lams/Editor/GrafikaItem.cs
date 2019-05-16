using System.Collections.Generic;
using System.Drawing;
using mDitaEditor.Project;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace mDitaEditor.Lams.Editor
{
    public class GrafikaItem
    {
        public static GrafikaItem Create(GrafikaCanvas parent, Point location, IGrafikaObject obj, bool initialized = true)
        {
            if (obj is LamsBranch)
            {
                return new GrafikaBranchStartItem(parent, location, (LamsBranch) obj, initialized);
            }
            return new GrafikaItem(parent, location, obj, initialized);
        }

        private static readonly Size SizeWide = new Size(120, 80);
        private static readonly Size SizeNormal = new Size(80, 80);

        private static readonly Pen BorderPen = new Pen(Color.Black, 1);

        public IGrafikaObject GrafikaObject { get; private set; }

        public GrafikaCanvas Parent { get; private set; }

        public bool SequenceChoosing { get;  set; }
        public bool Optional { get; set; }

        private GrafikaItem _previousItem;
        public virtual GrafikaItem Previous
        {
            get { return _previousItem; }
            set
            {
                if (_previousItem == value)
                {
                    return;
                }
                if (PreviousConnection != null)
                {
                    PreviousConnection.Delete();
                }
                _previousItem = value;
                if (_previousItem != null)
                {
                    GrafikaConnection.Create(_previousItem, this);
                }
            }
        }

        private GrafikaItem _nextItem;
        public virtual GrafikaItem Next
        {
            get
            {
                return _nextItem;
            }
            set
            {
                if (_nextItem == value)
                {
                    return;
                }
                if (NextConnection != null)
                {
                    NextConnection.Delete();
                }
                _nextItem = value;
                if (_nextItem != null)
                {
                    _nextItem.Previous = this;
                }
            }
        }

        public virtual GrafikaConnection PreviousConnection { get; set; }
        public virtual GrafikaConnection NextConnection { get; set; }

        private Rectangle _bounds;
        public Rectangle Bounds
        {
            get { return _bounds; }
            set
            {
                _bounds = value;
                Center = new Point(X + Width / 2, Y + Height / 2);
                Edges[0] = new Point(X, Y);
                Edges[1] = new Point(X, Center.Y);
                Edges[2] = new Point(X, Y + Height);
                Edges[3] = new Point(Center.X, Y + Height);
                Edges[4] = new Point(X + Width, Y + Height);
                Edges[5] = new Point(X + Width, Center.Y);
                Edges[6] = new Point(X + Width, Y);
                Edges[7] = new Point(Center.X, Y);
                PreviousConnection?.CalculateConnections();
                NextConnection?.CalculateConnections();
                if (this is GrafikaBranchStartItem)
                {
                    foreach (var connection in Parent.Connections)
                    {
                        if (connection.StartItem == this)
                        {
                            connection.CalculateConnections();
                        }
                    }
                }
                else if (this is GrafikaBranchEndItem)
                {
                    foreach (var connection in Parent.Connections)
                    {
                        if (connection.EndItem == this)
                        {
                            connection.CalculateConnections();
                        }
                    }
                }
            }
        }

        public Point Location
        {
            get { return Bounds.Location; }
            set { Bounds = new Rectangle(value, Size); }
        }

        public int X
        {
            get { return Location.X; }
            set { Location = new Point(value, Location.Y); }
        }

        public int Y
        {
            get { return Location.Y; }
            set { Location = new Point(Location.X, value); }
        }

        public Size Size
        {
            get { return Bounds.Size; }
            private set { Bounds = new Rectangle(Location, value); }
        }

        public int Width
        {
            get { return Size.Width; }
            private set { Size = new Size(value, Size.Height); }
        }

        public int Height
        {
            get { return Size.Height; }
            private set { Size = new Size(Size.Width, value); }
        }

        public Point Center { get; private set; }

        public Point[] Edges { get; private set; }

        public bool Initialized { get; set; }
     

        protected GrafikaItem(GrafikaCanvas parent, Point location, IGrafikaObject obj, bool initialized)
        {
            Edges = new Point[8];
            Parent = parent;
            GrafikaObject = obj;

            Size size;
            if (obj is LamsTool)
            {
                size = SizeWide;
            }
            else
            {
                size = SizeNormal;
            }
            Bounds = new Rectangle(location, size);
            Initialized = initialized;
        }

        public virtual void Delete()
        {
            PreviousConnection?.Delete();
            NextConnection?.Delete();
            Parent.Items.Remove(this);
            Parent.ParentPanel.ListControl.ShowObject(GrafikaObject as LamsTool);
        }

        public virtual void Draw(Graphics g)
        {
            g.DrawImage(GrafikaObject.Icon, Bounds, new Rectangle(Point.Empty, GrafikaObject.Icon.Size), GraphicsUnit.Pixel);
            g.DrawRectangle(BorderPen, Bounds);
            if (!Initialized)
            {
                if (GrafikaObject is LamsGate)
                {
                    GrafikaUtils.DrawTitleText(g, "Postavite Kapiju.", new Point(Center.X, Y - 18), null, GrafikaUtils.StringFormatCenter);
                }
                else if (GrafikaObject is LamsOptional)
                {
                    GrafikaUtils.DrawTitleText(g, "Postavite Opcionu aktivnost.", new Point(Center.X, Y - 18), null, GrafikaUtils.StringFormatCenter);
                }
            }
        }

        public void CheckErrors(List<SavingError> errors)
        {
            if (this is GrafikaBranchEndItem)
            {
                return;
            }
            //if (Previous == null && Next == null)
            //{
            //    errors.Add(new SavingError(this, "Objekat " + GrafikaObject.TitleText + " nije povezan u sekvencu."));
            //}
            var branch = GrafikaObject as LamsBranch;
            if (branch != null)
            {
                if (branch.Branches.Count == 0)
                {
                    errors.Add(new SavingError(this, "Ne postoji nijedna putanja u Branch-u " + branch.TitleText + "."));
                }
                else if (branch.DefaultBranch == null)
                {
                    errors.Add(new SavingError(this, "Nije postavljena podrazumevana putanja u Branch-u " + branch.TitleText + "."));
                }
               
                return;
            }
            var gate = GrafikaObject as LamsGate;
            if (gate != null)
            {
                if (gate.Entries.Count == 0)
                {
                    errors.Add(new SavingError(this, "Nije izabran nijedan kriterijum u Gate-u " + gate.TitleText + "."));
                }
                return;
            }
        }
    }
}
