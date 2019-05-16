using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using mDitaEditor.Dita.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using System.Diagnostics;

namespace mDitaEditor.Dita.Controls
{
    /// <summary>
    /// Snipet kontrola predstavlja komponentu u kojoj se prikazuje kod koji je unet.
    /// </summary>
    public class SnippetControl : FastColoredTextBox
    {
        private Random rand = new Random();
        public Sectiondiv rootSectionDiv { get; set; }
        public string Lang { get; set; }
        public const int LINE_HEIGHT = 15;
        private SelectableFlowPanel panel;

        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            var div = new Sectiondiv("vp1");
            Sectiondiv f11 = new Sectiondiv("f11");
            div.SectionDivs.Add(f11);
            root.SectionDivs.Add(div);
            return div;
        }
        public SnippetControl(int height, string language, string text, bool showLines, SelectableFlowPanel _panel, Sectiondiv div)
        {
            ContextMenuStripChanged += SnippetControl_ContextMenuStripChanged;
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            panel = _panel;
            Width = panel.Width;
            int maxHeight = panel.HeightLeftPanel() - Margin.Horizontal;
            ShowLineNumbers = showLines;
            Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            ReadOnly = true;
            Lang = language;
            Text = text;
            Text = Util.UnEscapeXml(Text);
            int lines = text.Count(c => c == '\n') + 1;
            int additionalSpace = (lines <= 1) ? 20 : 0;
            int defaultHeight = lines * LINE_HEIGHT + additionalSpace;
            maxHeight = (defaultHeight < maxHeight) ? defaultHeight : maxHeight;
            Height = (height != 0 && height <= maxHeight) ? height : maxHeight;
            Height = ((int)(Height / LINE_HEIGHT)) * LINE_HEIGHT;
            Location = new Point(0, panel.Height - Height);
            rootSectionDiv.SectionDivs[0].Content = GetXmlForElement();
        }

        /// <summary>
        /// Moteoda koja postavlja nove parametre za snipet kontrolu koje i poziva se prilikom
        /// update (azuriranja) sadrzaja snipeta koji se vec nalazi u panelu.
        /// </summary>
        /// <param name="height"></param>
        /// <param name="language"></param>
        /// <param name="text"></param>
        /// <param name="showLines"></param>
        public void RedefineControl(int height, string language, string text, bool showLines)
        {
            if (!showLines) ShowLineNumbers = false;
            else ShowLineNumbers = true;
            Text = text;
            SetHeight(height);
            Lang = language;
            rootSectionDiv.SectionDivs[0].Content = GetXmlForElement();
            //Invalidate();            
        }

        private void SnippetControl_ContextMenuStripChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("snippet rigth");
            if (ContextMenuStrip != null)
            {
                ContextMenuStrip.Items.Insert(0, new ToolStripSeparator());

                var updateSnippet = GuiUtil.EditButtonWithText("Edit");
                updateSnippet.Click += UpdateSnippet_Click;
                ContextMenuStrip.Items.Insert(0, updateSnippet);
            }
        }

        /// <summary>
        /// Metod koji setuje vrednost visine snipet kontrole u zavisnosti od preostalog prostora u panel-u.
        /// </summary>
        /// <param name="height"></param>
        public void SetHeight(int height)
        {
            SelectableFlowPanel panel = (SelectableFlowPanel)Parent.Parent;
            int maxHeight = panel.HeightLeftPanel() + Height;
            int lines = Text.Count(c => c == '\n') + 1;
            int additionalSpace = (lines <= 1) ? 20 : 0;
            int defaultHeight = lines * LINE_HEIGHT + additionalSpace;
            maxHeight = (defaultHeight < maxHeight) ? defaultHeight : maxHeight;
            Height = (height != 0 && height <= maxHeight) ? height : maxHeight;
            Height = ((int)(Height / LINE_HEIGHT)) * LINE_HEIGHT;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateSnippet_Click(object sender, EventArgs e)
        {
            EditSnippetForm paste = new EditSnippetForm(this);
            paste.ShowDialog();
        }

        /// <summary>
        /// Metoda koja vraca prograski jezik za kod koji ce se prikazivati u snipetu.
        /// </summary>
        /// <returns></returns>
        public Language GetLanguage()
        {
            switch (Lang.ToLower())
            {
                case "java":
                case "c++":
                case "c#":
                    return Language.CSharp;
                case "php":
                case "c":
                case "py":
                case "swift":
                    return Language.PHP;
                case "html":
                    return Language.HTML;
                case "sql":
                    return Language.SQL;
                case "css":
                    return Language.HTML;
                default:
                    return Language.JS;
            }
        }

        public string GetXmlForElement()
        {
            string linenum = (ShowLineNumbers) ? "linenums" : "";
            int linenumbers = (Height / LINE_HEIGHT);
            return "<pre outputclass=\"prettyprint " + "lang-" + Lang.ToString().ToLower() + " " + linenum + " noflines" + linenumbers + "\"" + " id=\"selectCS" + rand.Next(1000, 10000) + "\">" + Text + "</pre>";
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // SnippetControl
            // 
            this.AutoScrollMinSize = new System.Drawing.Size(27, 14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SnippetControl";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
        }
    }

}
