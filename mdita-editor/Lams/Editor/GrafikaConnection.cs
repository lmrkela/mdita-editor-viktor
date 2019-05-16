using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace mDitaEditor.Lams.Editor
{
    public class GrafikaConnection
    {
        public static GrafikaConnection Create(GrafikaItem start, GrafikaItem end)
        {
            GrafikaConnection conn;
            var branch = start as GrafikaBranchStartItem;
            if (branch != null)
            {
                conn = new GrafikaBranchConnection((GrafikaBranchStartItem)start, end);
            }
            else
            {
                conn = new GrafikaConnection(start, end);
            }
            start.Parent.Connections.Add(conn);
            return conn;
        }

        public readonly GrafikaItem StartItem;
        public readonly GrafikaItem EndItem;

        public virtual Point StartPoint { get; set; }

        public virtual Point EndPoint { get; set; }

        public bool SequenceChoosing { get; set; }

        protected GrafikaConnection(GrafikaItem startItem, GrafikaItem endItem)
        {
            StartItem = startItem;
            EndItem = endItem;
            StartItem.NextConnection = this;
            EndItem.PreviousConnection = this;
            CalculateConnections();
        }

        public virtual void Delete()
        {
            StartItem.NextConnection = null;
            EndItem.PreviousConnection = null;
            StartItem.Parent.Connections.Remove(this);
            StartItem.Next = null;
            EndItem.Previous = null;
        }

        public bool CheckMouse(Point mouse)
        {
            return (int)Math.Round(GrafikaUtils.Distance(StartPoint, mouse) * 3 + GrafikaUtils.Distance(mouse, EndPoint) * 3) == (int)Math.Round(GrafikaUtils.Distance(StartPoint, EndPoint) * 3);
        }

        public void CalculateConnections()
        {
            var startItem = StartItem;
            var endItem = EndItem;
            Point[] closest;
            var boundsStart = new Rectangle(startItem.X - 20, startItem.Y - 20, startItem.Width + 40, startItem.Height + 40);
            if (boundsStart.IntersectsWith(endItem.Bounds))
            {
                closest = new[] { startItem.Center, endItem.Center };
            }
            else
            {
                closest = GrafikaUtils.ClosestPoint(startItem.Edges, endItem.Edges);
                /*closest = new Point[2];
                if (startItem.Bounds.Top >= endItem.Bounds.Bottom)
                {
                    if (startItem.Bounds.Left >= endItem.Bounds.Right)
                    {
                        closest[0] = startItem.Edges[0];
                        closest[1] = endItem.Edges[7];
                    }
                    else if (startItem.Bounds.Right <= endItem.Bounds.Left)
                    {
                        closest[0] = startItem.Edges[5];
                        closest[1] = endItem.Edges[2];
                    }
                    else
                    {
                        closest[0] = startItem.Edges[3];
                        closest[1] = endItem.Edges[4];
                    }
                }
                else if (startItem.Bounds.Bottom <= endItem.Bounds.Top)
                {
                    if (startItem.Bounds.Left >= endItem.Bounds.Right)
                    {
                        closest[0] = startItem.Edges[2];
                        closest[1] = endItem.Edges[5];
                    }
                    else if (startItem.Bounds.Right <= endItem.Bounds.Left)
                    {
                        closest[0] = startItem.Edges[7];
                        closest[1] = endItem.Edges[0];
                    }
                    else
                    {
                        closest[0] = startItem.Edges[4];
                        closest[1] = endItem.Edges[3];
                    }
                }
                else
                {
                    if (startItem.Bounds.Left > endItem.Bounds.Right)
                    {
                        closest[0] = startItem.Edges[1];
                        closest[1] = endItem.Edges[6];
                    }
                    else if (startItem.Bounds.Right < endItem.Bounds.Left)
                    {
                        closest[0] = startItem.Edges[6];
                        closest[1] = endItem.Edges[1];
                    }
                    else
                    {
                        closest[0] = startItem.Center;
                        closest[1] = endItem.Center;
                    }
                }*/
            }

            StartPoint = closest[0];
            EndPoint = closest[1];
        }


        public static readonly Pen ConnectionPen = new Pen(Color.DarkGray, 2)
        {
            StartCap = LineCap.RoundAnchor,
            CustomEndCap = new AdjustableArrowCap(3, 5)
        };

        public virtual void Draw(Graphics g, bool hover = false)
        {
            ConnectionPen.Color = Color.Black;
            g.TranslateTransform(0, 1);
            g.DrawLine(ConnectionPen, StartPoint, EndPoint);

            ConnectionPen.Color = hover ? GrafikaUtils.HighlightYellow : Color.DarkGray;
            g.TranslateTransform(0, -1);
            g.DrawLine(ConnectionPen, StartPoint, EndPoint);
        }
    }
}
