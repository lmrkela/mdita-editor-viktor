using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using System.Diagnostics;

namespace mDitaEditor.Dita.Controls
{
    public partial class SelectableFlowPanel
    {
        private int GetIndexAt(Point p)
        {
            int y = p.Y;
            for (int i = 0; i < _controls.Count; ++i)
            {
                var c = _controls[i];
                int h = c.Height / 2;
                int y0 = c.Location.Y + h;
                if (y < y0)
                {
                    return i;
                }
                y0 += h;
                if (y < y0)
                {
                    return i + 1;
                }
            }
            return _controls.Count;
        }

        public void MoveControl(DivControl movedDiv, SelectableFlowPanel destination, int destIndex)
        {
            int sourceIndex = Column.SectionDivs.IndexOf(movedDiv.Div);
            if (destination != this)
            {
                if (destination.HeightLeftPanel() < movedDiv.Height)
                {
                    return;
                }
            }
            else if (destIndex == sourceIndex)
            {
                return;
            }

            Column.SectionDivs.Remove(movedDiv.Div);
            _controls.Remove(movedDiv);
            destination.Column.SectionDivs.Insert(destIndex, movedDiv.Div);
            destination._controls.Insert(destIndex, movedDiv);
            if (!IsPreview)
            {
                DitaClipboard.AddSectionDivMovedState(ProjectSingleton.SelectedSection, movedDiv.Div, Column, sourceIndex, destination.Column, destIndex);
            }
            RelocateControls();
            if (destination != this)
            {
                destination.RelocateControls();
            }
            MainForm.Instance.enumerateFigures();
            MainForm.Instance.OpenSlide(ProjectSingleton.SelectedSection);
        }

        private void SelectableFlowPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(DivControl)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void SelectableFlowPanel_DragLeave(object sender, EventArgs e)
        {
            Invalidate();
        }


        private void SelectableFlowPanel_DragOver(object sender, DragEventArgs e)
        {
            var div = e.Data.GetData(typeof(DivControl)) as DivControl;
            if (div == null)
            {
               
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files != null)
                {
                    if (files.Length == 0)
                    {
                        e.Effect = DragDropEffects.None;                    
                    }
                    else
                    {
                        e.Effect = DragDropEffects.Move;
                    }
                    return;
                }

                var text = (string)e.Data.GetData(DataFormats.Text);

                if (e.Data.GetDataPresent(DataFormats.EnhancedMetafile) & e.Data.GetDataPresent(DataFormats.MetafilePict) || text != null)
                {
                        e.Effect = DragDropEffects.Copy;                
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }

                return;                              
            }

            int y = 1;
            Point p = PointToClient(new Point(e.X, e.Y));
            int i = GetIndexAt(p) - 1;
            if (i >= 0 && i < _controls.Count)
            {
                var control = _controls[i];
                y += control.Location.Y + control.Height;
            }

            SelectableFlowPanel source = (SelectableFlowPanel)div.Parent;
            SelectableFlowPanel destination = (SelectableFlowPanel)sender;

            Pen pen;
            if (source == destination || destination.HeightLeftPanel() >= div.Height)
            {
                pen = new Pen(new SolidBrush(Color.FromArgb(167, 5, 50)), 2);
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                pen = new Pen(new SolidBrush(Color.DarkGray), 2);
                e.Effect = DragDropEffects.None;
            }

            Graphics g = CreateGraphics();
            g.Clear(BackColor);
            g.DrawLine(pen, 0, y, Width, y);
        }
        
                 

        private void SelectableFlowPanel_DragDrop(object sender, DragEventArgs e)
        {
            var control = (DivControl)e.Data.GetData(typeof(DivControl));
            SelectableFlowPanel destination = (SelectableFlowPanel)sender;
            if (control == null)
            {

                var text = (string)e.Data.GetData(DataFormats.UnicodeText);
                if(text != null)
                {
                    DitaClipboard.pasteText(destination, text, false);
                    MainForm.Instance.OpenSlide(ProjectSingleton.SelectedSection);
                    return;
                }

                DitaClipboard.pasteImage(destination, e.Data);
                return;
            }

            SelectableFlowPanel source = (SelectableFlowPanel)control.Parent;

            Point p = destination.PointToClient(new Point(e.X, e.Y));
            int destIndex = GetIndexAt(p);
            int sourceIndex = Column.SectionDivs.IndexOf(control.Div);

            if (source == destination && destIndex > sourceIndex)
            {
                --destIndex;
            }
            source.MoveControl(control, destination, destIndex);            
            Invalidate();
        }
    }
}
