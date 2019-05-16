using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mDitaEditor.LAMS;
using mDitaEditor.LAMS.Controls;

namespace mDitaEditor.Grafika
{
    public class GrafikaSingleItem : GrafikaItem
    {
        private static readonly Size SizeWide = new Size(120, 80);
        private static readonly Size SizeNormal = new Size(80, 80);

        private static readonly Pen BorderPen = new Pen(Color.Black, 1);

        public GrafikaSingleItem(GrafikaCanvas parent, Point location, IGrafikaObject obj, bool initialized = true) : base(parent, location, obj, initialized)
        {
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
        }

        public override void Draw(Graphics g)
        {
            try
            {
                g.DrawImage(GrafikaObject.Icon, Bounds, new Rectangle(Point.Empty, GrafikaObject.Icon.Size),
                    GraphicsUnit.Pixel);
                g.DrawRectangle(BorderPen, Bounds);
            }
            catch
            {}
        }
    }
}
