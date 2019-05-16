using System.Drawing;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Editor
{
    public class GrafikaBranchEndItem : GrafikaItem
    {
        public LamsBranch Branch
        {
            get { return StartItem.Branch; }
        }

        public GrafikaBranchStartItem StartItem { get; set; }
        
        public override GrafikaItem Previous
        {
            get { return StartItem.Previous; }
            set
            {
                if (value == null)
                {
                    return;
                }
                foreach (var conn in Parent.Connections)
                {
                    if (conn.StartItem == value && conn.EndItem == this)
                    {
                        return;
                    }
                }
                GrafikaConnection.Create(value, this);
            }
        }

        public override GrafikaItem Next
        {
            get { return base.Next; }
            set
            {
                if (value == StartItem)
                {
                    return;
                }
                base.Next = value;
            }
        }

        public override GrafikaConnection PreviousConnection
        {
            get { return null; }
            set { }
        }

        public GrafikaBranchEndItem(GrafikaBranchStartItem start)
            : base(start.Parent, start.Location, start.Branch, start.Initialized)
        {
            StartItem = start;
            StartItem.EndItem = this;
        }

        public override void Delete()
        {
            if (StartItem == null)
            {
                return;
            }
            base.Delete();
            foreach (var item in Parent.Items)
            {
                if (item.Next == this)
                {
                    item.NextConnection.Delete();
                }
            }
            var start = StartItem;
            StartItem = null;
            start.Delete();
        }

        private static readonly Pen BorderPen = new Pen(Color.Black, 1);

        public override void Draw(Graphics g)
        {
            g.DrawImage(Resources.hcnarb, Bounds, new Rectangle(Point.Empty, Resources.hcnarb.Size), GraphicsUnit.Pixel);
            g.DrawRectangle(BorderPen, Bounds);
            if (!Initialized)
            {
                GrafikaUtils.DrawTitleText(g, "Postavite krajnju tačku za Branch.", new Point(Center.X, Y - 18), null, GrafikaUtils.StringFormatCenter);
            }
        }
    }
}
