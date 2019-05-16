using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Dita.Controls;

namespace mDitaEditor.Utils
{
    class GuiUtil
    {
        /// <summary>
        /// P
        /// </summary>
        /// <returns></returns>
        public static ContextMenuStrip createSelectablePanelRightClickMenu() {
            ContextMenuStrip menuDitaObject;            
            menuDitaObject = new ContextMenuStrip();
            menuDitaObject.SuspendLayout();
            menuDitaObject.Items.AddRange(new ToolStripItem[] {
            PasteButton() });
            menuDitaObject.Name = "spMenuDitaObject";
            menuDitaObject.Size = new Size(108, 26);
            menuDitaObject.ResumeLayout(false);
            return menuDitaObject;
        }
        
        public static Image DrawControlToImage(Control control)
        {
            var bmp = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(bmp, new Rectangle(0, 0, control.Width, control.Height));

            var newImage = ResizeImage(bmp, 170, 140);
            bmp.Dispose();

            return newImage;
        }

        public static Bitmap ResizeImage(Bitmap imgToResize, int width, int height)
        {
            try
            {
                Bitmap bmp = new Bitmap(width, height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.DrawImage(imgToResize, 0, 0, width, height);
                }
                return bmp;
            }
            catch
            {
                return imgToResize;
            }
        }

        /// <summary>
        /// Pravi ToolStripMenuItem za edit dugme sa specificnim tekstom
        /// </summary>
        /// <returns></returns>
        public static ToolStripMenuItem EditButtonWithText(string text)
        {
            ToolStripMenuItem editEquation = new ToolStripMenuItem(text);
            editEquation.Image = Properties.Resources.edit;
            editEquation.Size = new Size(107, 22);
            return editEquation;
        }

        /// <summary>
        /// Pravi ToolStripMenuItem za dugme za swap ka gore
        /// </summary>
        /// <returns></returns>
        public static ToolStripMenuItem UpButtonWithText()
        {
            ToolStripMenuItem editEquation = new ToolStripMenuItem("Move up");
            editEquation.Image = Properties.Resources.arrowup;
            editEquation.Size = new Size(107, 22);
            return editEquation;
        }

        /// <summary>
        /// Pravi ToolStripMenuItem za dugme za swap ka dole
        /// </summary>
        /// <returns></returns>
        public static ToolStripMenuItem DownButtonWithText()
        {
            ToolStripMenuItem editEquation = new ToolStripMenuItem("Move down");
            editEquation.Image = Properties.Resources.arrowdown;
            editEquation.Size = new Size(107, 22);
            return editEquation;
        }
        
        /// <summary>
        /// Duplicate button Gen
        /// </summary>
        /// <returns></returns>
        public static ToolStripMenuItem CopyButton() {
            ToolStripMenuItem copyBtn = new ToolStripMenuItem("Copy");
            copyBtn.Image = Properties.Resources.copy;
            copyBtn.Size = new Size(107, 22);
            return copyBtn;
        }

        /// <summary>
        /// Duplicate button Gen
        /// </summary>
        /// <returns></returns>
        public static ToolStripMenuItem DuplicateButton()
        {
            ToolStripMenuItem duplicateBtn = new ToolStripMenuItem("Duplicate");
            duplicateBtn.Image = Properties.Resources.duplicate;
            duplicateBtn.Size = new Size(107, 22);
            return duplicateBtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ToolStripMenuItem CutButton()
        {
            ToolStripMenuItem cutBtn = new ToolStripMenuItem("Cut");
            cutBtn.Image = Properties.Resources.cut;
            cutBtn.Size = new Size(107, 22);
            return cutBtn;
        }

        public static ToolStripMenuItem UndoButton()
        {
            ToolStripMenuItem cutBtn = new ToolStripMenuItem("Undo");
            cutBtn.Image = Properties.Resources.undo_icon;
            cutBtn.Size = new Size(107, 22);
            return cutBtn;
        }

        public static ToolStripMenuItem RedoButton()
        {
            ToolStripMenuItem cutBtn = new ToolStripMenuItem("Redo");
            cutBtn.Image = Properties.Resources.redo_icon;
            cutBtn.Size = new Size(107, 22);
            return cutBtn;
        }

        /// <summary>
        /// Kreira i vraca Paste dugme za padajuci RightClick meni.
        /// </summary>
        public static ToolStripMenuItem PasteButton()
        {
            ToolStripMenuItem pasteToolStripMenuItem = new ToolStripMenuItem("Paste");
            pasteToolStripMenuItem.Image = Properties.Resources.paste;            
            pasteToolStripMenuItem.Size = new Size(107, 22);
            return pasteToolStripMenuItem;
        }        

        /// <summary>
        /// Ukoliko je up parametar 0 element se spušta dole dok ako je 1 ide gore
        /// Metoda radi swap objekata u panelu gore ili dole
        /// </summary>
        /// <param name="swapObj">Objekat koji se swapuje</param>
        /// <param name="up">Oš gore ili dole</param>
        public static void SwapComponent(Control swapObj, int up, Sectiondiv rootSectionDiv, Sectiondiv divParent)
        {
            // TODO: ADD STATE

            SelectableFlowPanel panel = (SelectableFlowPanel) swapObj.Parent;
            int swapObjIndex = panel._controls.IndexOf((DivControl)swapObj);
            SectionsGuiUtil.SwapSectionDivs(rootSectionDiv, up, divParent);

            if (up == 1 && swapObjIndex > 0)
            {
                Control temp = panel._controls[swapObjIndex];
                Control temp2 = panel._controls[swapObjIndex - 1];
                panel.Controls.SetChildIndex(temp, swapObjIndex - 1);
                panel.Controls.SetChildIndex(temp2, swapObjIndex);
            }
            else if (up == 0 && swapObjIndex < panel.Controls.Count - 1)
            {
                Control temp = panel._controls[swapObjIndex];
                Control temp2 = panel._controls[swapObjIndex + 1];
                panel.Controls.SetChildIndex(temp, swapObjIndex + 1);
                panel.Controls.SetChildIndex(temp2, swapObjIndex);
            }
        }        
    }
}
