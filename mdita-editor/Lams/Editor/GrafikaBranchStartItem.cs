using System.Drawing;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Editor
{
    public class GrafikaBranchStartItem : GrafikaItem
    {
        public LamsBranch Branch { get; private set; }

        public GrafikaBranchEndItem EndItem { get; set; }

        public override GrafikaItem Next
        {
            get { return EndItem.Next; }
            set
            {
                if (value == null || value is GrafikaBranchEndItem)
                {
                    return;
                }
                value.Previous = this;
            }
        }

        public override GrafikaConnection NextConnection
        {
            get { return null; }
            set { }
        }

        public GrafikaBranchStartItem(GrafikaCanvas parent, Point location, LamsBranch obj, bool initialized)
            : base(parent, location, obj, initialized)
        {
            Branch = obj;
        }

        public override void Delete()
        {
            if (EndItem == null)
            {
                return;
            }
            base.Delete();
            foreach (var item in Parent.Items)
            {
                if (item.Previous == this)
                {
                    item.PreviousConnection.Delete();
                }
            }
            foreach (var connection in Parent.Connections)
            {
                if (connection.StartItem == this && connection.EndItem == EndItem)
                {
                    connection.Delete();
                    break;
                }
            }
            var end = EndItem;
            EndItem = null;
            end.Delete();
        }

        private static readonly Pen BorderPen = new Pen(Color.Black, 1);

        public override void Draw(Graphics g)
        {
            g.DrawImage(Resources.branch, Bounds, new Rectangle(Point.Empty, Resources.branch.Size), GraphicsUnit.Pixel);
            g.DrawRectangle(BorderPen, Bounds);
            if (!Initialized)
            {
                GrafikaUtils.DrawTitleText(g, "Postavite početnu tačku za Branch.", new Point(Center.X, Y - 18), null, GrafikaUtils.StringFormatCenter);
            }
        }

        public bool CheckConnections()
        {
            if (Branch.Branches.Count == 0)
            {
                return false;
            }
            foreach (var connection in Branch.Branches)
            {
                for(var item = connection.EndItem; item != EndItem; item = item.Next)
                {
                    if (item == null)
                    {
                        return false;
                    }
                    var branch = item as GrafikaBranchStartItem;
                    if (branch != null && !branch.CheckConnections())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
