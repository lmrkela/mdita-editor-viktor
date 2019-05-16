using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace mDitaEditor.Lams.Editor
{
    internal static class GrafikaUtils
    {
        public static readonly Color MetropolitanRed = Color.FromArgb(167, 5, 50);
        public static readonly Color HighlightYellow = Color.FromArgb(225, 211, 32);

        public static readonly SolidBrush TextBrush = new SolidBrush(Color.Black);
        public static readonly SolidBrush GridBrush = new SolidBrush(Color.Gray);
        public static readonly Pen HoverPen = new Pen(HighlightYellow, 2);
        public static readonly Pen OutlinePen = new Pen(Color.White, 3)
        {
            LineJoin = LineJoin.Round
        };

        public static readonly Font TitleFont = new Font("Helvetica", 14f);

        public static readonly Font TitleFontBold = new Font(TitleFont, FontStyle.Bold);

        public static readonly StringFormat StringFormatCenter = new StringFormat
        {
            Alignment = StringAlignment.Center
        };

        public static void DrawTitleText(Graphics g, string text, Point location, Font font = null, StringFormat sf = null)
        {
            if (font == null)
            {
                font = TitleFont;
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            GraphicsPath path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, location, sf);

            OutlinePen.Color = Color.White;
            g.DrawPath(OutlinePen, path);
            TextBrush.Color = Color.Black;
            g.FillPath(TextBrush, path);

            path.Dispose();

            //g.TranslateTransform(-1, -1);
            //TextBrush.Color = Color.White;
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(1, 0);
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(1, 0);
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(-2, 1);
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(2, 0);
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(-2, 1);
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(1, 0);
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(1, 0);
            //g.DrawString(text, font, TextBrush, location);
            //g.TranslateTransform(-1, -1);
            //TextBrush.Color = Color.Black;
            //g.DrawString(text, font, TextBrush, location);
        }

        public static void DrawTitleText(Graphics g, string text, Rectangle bounds, Color textColor, Color outlineColor, Font font, StringFormat sf = null)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            GraphicsPath path = new GraphicsPath();
            if (text != null)
            {
                path.AddString(text, font.FontFamily, (int)font.Style, font.Size, bounds, sf);
            }
            OutlinePen.Color = outlineColor;
            g.DrawPath(OutlinePen, path);
            TextBrush.Color = textColor;
            g.FillPath(TextBrush, path);

            path.Dispose();
        }

        public static double Distance(Point a, Point b)
        {
            var x = a.X - b.X;
            var y = a.Y - b.Y;
            return Math.Sqrt(x * x + y * y);
        }


        private static readonly Dictionary<int, int[]> PointsToCompare = new Dictionary<int, int[]>();
        //private static readonly Dictionary<int, int[]> PointsToCompareBranch = new Dictionary<int, int[]>();
        static GrafikaUtils()
        {
            PointsToCompare.Add(1, new[] { 5, 4, 6 });
            PointsToCompare.Add(3, new[] { 7, 0, 6 });
            PointsToCompare.Add(5, new[] { 1, 0, 2 });
            PointsToCompare.Add(7, new[] { 3, 2, 4 });
            PointsToCompare.Add(0, new[] { 4 });
            PointsToCompare.Add(2, new[] { 6 });
            PointsToCompare.Add(4, new[] { 0 });
            PointsToCompare.Add(6, new[] { 2 });

            //PointsToCompareBranch.Add(0, new[] { 3, 5, 4 });
            //PointsToCompareBranch.Add(2, new[] { 5, 7, 6 });
            //PointsToCompareBranch.Add(4, new[] { 1, 7, 0 });
            //PointsToCompareBranch.Add(6, new[] { 1, 3, 2 });
            //PointsToCompareBranch.Add(1, new[] { 5 });
            //PointsToCompareBranch.Add(3, new[] { 7 });
            //PointsToCompareBranch.Add(5, new[] { 1 });
            //PointsToCompareBranch.Add(7, new[] { 3 });
        }

        public static Point[] ClosestPoint(Point[] a, Point[] b)
        {
            var closest = new Point[2];

            var min = double.MaxValue;
            foreach (var i in PointsToCompare.Keys)
            {
                foreach (var j in PointsToCompare[i])
                {
                    var d = Distance(a[i], b[j]);
                    if (d < min)
                    {
                        min = d;
                        closest[0] = a[i];
                        closest[1] = b[j];
                    }
                }
            }

            return closest;
        }
    }
}
