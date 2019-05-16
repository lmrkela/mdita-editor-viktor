using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams;
using mDitaEditor.Lams.Editor;

namespace mDitaEditor.Project
{
    public class SavingError
    {
        public readonly string Text;

        public readonly GrafikaItem Item;
        public readonly IGrafikaObject GrafikaObject;
        public readonly IDitaSlide Slide;

        public readonly Control FocusControl;
        public readonly bool LamsDesigner;

        public SavingError(IDitaSlide slide, string text, Control control)
        {
            Slide = slide;
            Text = text;
            FocusControl = control;
            LamsDesigner = false;
        }

        public SavingError(IGrafikaObject obj, string text)
        {
            GrafikaObject = obj;
            Text = text;
            LamsDesigner = true;
        }

        public SavingError(GrafikaItem item, string text)
        {
            Item = item;
            Text = text;
            LamsDesigner = true;
        }

        public SavingError(string text)
        {
            Text = text;
            LamsDesigner = true;
        }

        public void FocusRelevantItem()
        {
            var form = MainForm.Instance;
            if (LamsDesigner)
            {
                form.ribbonMenu.ActiveTab = form.tabGrafika;
                if (Item != null)
                {
                    var canvas = form.grafikaPanel.Canvas;
                    var visibleSize = canvas.VisibleSize;
                    canvas.HoverObject = Item;
                    canvas.Offset = new Point(visibleSize.Width / 2 - Item.Center.X, visibleSize.Height / 2 - Item.Center.Y);
                }
                else if (GrafikaObject != null)
                {
                    var listControl = form.grafikaPanel.ListControl;
                    var nb0 = GrafikaObject as LamsNoticeboard;
                    foreach (var control in listControl.PreviewControls)
                    {
                        var nb1 = control.GrafikaObject as LamsNoticeboard;
                        if (control.GrafikaObject == GrafikaObject || (nb0?.LearningObject != null && nb0?.LearningObject == nb1?.LearningObject))
                        {
                            listControl.SelectedControl = control;
                            listControl.ScrollControlIntoView(control);
                            form.grafikaPanel.Canvas.HoverObject = null;
                            control.Invalidate();
                            break;
                        }
                    }
                }
            }
            else
            {
                form.ribbonMenu.ActiveTab = form.tabDita;
                form.OpenSlide(Slide);
                FocusControl?.Focus();
            }
        }
    }
}
