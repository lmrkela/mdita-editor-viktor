using System.Drawing;

namespace mDitaEditor.Lams.Editor
{
    public class GrafikaBranchConnection : GrafikaConnection
    {
        public override Point StartPoint
        {
            get { return base.StartPoint; }
            set
            {
                base.StartPoint = value;
                CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            }
        }
        public override Point EndPoint
        {
            get { return base.EndPoint; }
            set
            {
                base.EndPoint = value;
                CenterPoint = new Point((StartPoint.X + EndPoint.X) / 2, (StartPoint.Y + EndPoint.Y) / 2);
            }
        }

        public Point CenterPoint { get; private set; }

        public LamsBranch Branch { get; private set; }

        public string Title
        {
            get
            {
                return "Branch " + BranchIndex;
            }
        }

        public int BranchIndex
        {
            get { return Branch.Branches.IndexOf(this) + 1; }
        }

        public GrafikaBranchConnection(GrafikaBranchStartItem startItem, GrafikaItem endItem) : base(startItem, endItem)
        {
            Branch = startItem.Branch;
            Branch.Branches.Add(this);
        }

        public override void Delete()
        {
            base.Delete();
            Branch.Branches.Remove(this);
            if (Branch.DefaultBranch == this)
            {
                Branch.DefaultBranch = null;
            }
        }

        public override void Draw(Graphics g, bool hover = false)
        {
            base.Draw(g, hover);
            GrafikaUtils.DrawTitleText(g, Title, new Point(CenterPoint.X, CenterPoint.Y - 16), Branch.DefaultBranch == this ? GrafikaUtils.TitleFontBold : GrafikaUtils.TitleFont, GrafikaUtils.StringFormatCenter);
        }

        public override string ToString()
        {
            return Title;
        }
    }
}
