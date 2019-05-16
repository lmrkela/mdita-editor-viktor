using System;
using System.Drawing;
using System.Windows.Forms;

namespace mDitaEditor.Dita.Controls
{
    public class ResizeableControlImage
    {
        public interface ISectiondivContainter
        {
            void PrepareState();

            void AddState();
        }

        private ISectiondivContainter _containter;

        private bool mMouseDown = false;
        private EdgeEnum mEdge = EdgeEnum.None;
        private int mWidth = 4;

        private bool mOutlineDrawn = false;
        private enum EdgeEnum
        {
            None,
            Right,
            Left,
            Top,
            Bottom,
            TopLeft,
            BottomRight
        }

        public static void Create(Control control, ISectiondivContainter container)
        {
            new ResizeableControlImage(control, container);
        }

        private ResizeableControlImage(Control control, ISectiondivContainter containter)
        {
            control.MouseDown += mControl_MouseDown;
            control.MouseUp += mControl_MouseUp;
            control.MouseMove += mControl_MouseMove;
            control.MouseLeave += mControl_MouseLeave;
            _containter = containter;
        }

        private void mControl_MouseDown(object sender, MouseEventArgs e)
        {
            Control c = (Control)sender;
            if (e.Button == MouseButtons.Left)
            {
                mMouseDown = true;
                if (_containter != null)
                {
                    _containter.PrepareState();
                }
                c.Refresh();
            }
        }

        private void mControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mMouseDown = false;
                if (_containter != null)
                {
                    _containter.AddState();
                }
            }
        }

        private void mControl_MouseMove(object sender, MouseEventArgs e)
        {
            Control c = (Control)sender;
            Graphics g = c.CreateGraphics();
            Color col = ColorTranslator.FromHtml("#a70532");
            Brush brush = new SolidBrush(col);
            switch (mEdge)
            {
                case EdgeEnum.BottomRight:
                    g.FillRectangle(brush, 0, c.Height - mWidth, c.Width, mWidth);
                    g.FillRectangle(brush, c.Width - mWidth, 0, c.Width, c.Height);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Bottom:
                    g.FillRectangle(brush, 0, c.Height - mWidth, c.Width, mWidth);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.Right:
                    g.FillRectangle(brush, c.Width - mWidth, 0, c.Width, c.Height);
                    mOutlineDrawn = true;
                    break;
                case EdgeEnum.None:
                    if (mOutlineDrawn)
                    {
                        c.Refresh();
                        mOutlineDrawn = false;
                    }
                    break;
            }

            if (mMouseDown && mEdge != EdgeEnum.None)
            {
                SelectableFlowPanel panel;
                if (((Control)_containter).Parent is SelectableFlowPanel)
                {
                    panel = (SelectableFlowPanel)((Control)_containter).Parent;
                }
                else
                {
                    panel = (SelectableFlowPanel)((Control)_containter).Parent.Parent;
                }

                c.SuspendLayout();

                if (mEdge == EdgeEnum.Right)
                {
                    int x = e.X;
                    int xMin = 50;
                    if (x < xMin)
                    {
                        x = xMin;
                    }
                    int xMax = panel.Width;
                    if (x > xMax)
                    {
                        x = xMax;
                    }
                    c.Size = new Size(x, c.Height);
                }
                if (mEdge == EdgeEnum.Bottom)
                {
                    int y = e.Y;
                    int yMin = 50;
                    if (y < yMin)
                    {
                        y = yMin;
                    }
                    int yMax = c.Height + panel.HeightLeftPanel() - 5;
                    if (y > yMax)
                    {
                        y = yMax;
                    }
                    c.Size = new Size(c.Width, y);
                }
                if (mEdge == EdgeEnum.BottomRight)
                {
                    int xOriginal = c.Width;
                    int yOriginal = c.Height;
                    int xMax = panel.Width;
                    int yMax = c.Height + panel.HeightLeftPanel() - 5;
                    int x = e.X;
                    int y = e.Y;
                    int yMin = 50;
                    if (y < yMin)
                    {
                        y = yMin;
                    }
                    int xMin = 50;
                    if (x < xMin)
                    {
                        x = xMin;
                    }
                    int procenat = (100 * y) / yOriginal;
                    decimal proc2 = (procenat * xOriginal);
                    x = (int)Math.Round(proc2 / 100,0);
                    decimal proc = (procenat * yOriginal);
                    y = (int)Math.Round(proc / 100,0);
                    if (x > xMax || y > yMax)
                    {

                    }
                    else {

                        c.Size = new Size(x, y);
                    }

                }

                c.ResumeLayout();
            }
            else
            {
                if (e.X > (c.Width + 8) - (mWidth + 1) || e.X > (c.Width - 8) - (mWidth + 1))
                {
                    if (e.Y > (c.Height + 8) - (mWidth + 1) || e.Y > (c.Height - 8) - (mWidth + 1))
                    {
                        //bottom right edge
                        c.Cursor = Cursors.SizeNWSE;
                        mEdge = EdgeEnum.BottomRight;
                        
                    }
                    else
                    {
                        //right edge
                        c.Cursor = Cursors.SizeWE;
                        mEdge = EdgeEnum.Right;
                    }
                }
                else if (e.Y > (c.Height + 8) - (mWidth + 1) || e.Y > (c.Height - 8) - (mWidth + 1))
                {
                    //bottom edge
                    c.Cursor = Cursors.SizeNS;
                    mEdge = EdgeEnum.Bottom;
                }
                else
                {
                    //no edge
                    if (c is RichTextBox)
                    {
                        c.Cursor = Cursors.IBeam;
                    }
                    else
                    {
                        c.Cursor = Cursors.Default;
                    }
                    mEdge = EdgeEnum.None;

                }
            }
        }

        private void mControl_MouseLeave(object sender, System.EventArgs e)
        {
            Control c = (Control)sender;
            mEdge = EdgeEnum.None;
            c.Refresh();
        }

    }

}
