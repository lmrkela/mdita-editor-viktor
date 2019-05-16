using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using mshtml;
using NSoup;
using NSoup.Nodes;

namespace mDitaEditor.Dita.Controls
{
    public class NoteControl : WebBrowser
    {
        private Sectiondiv rootSectionDiv;
        public Timer t;
        public bool isInit = true;
        public string docInitText = "";
        string currentBackground = "notewhite";
        string currentColor = "noteblack";

        /// <summary>
        /// Pali border na WebBrowser kompoenenti
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style |= 0x800000;  // Turn on WS_BORDER
                return parms;
            }
        }


        public string CurrentColor
        {
            get
            {
                return currentColor;
            }
        }

        public string CurrentBackground
        {
            get
            {
                return currentBackground;
            }
        }

        public string replaceHTML(string replacement)
        {
            string rep = replacement;
            rep = replaceHrefHTML(rep);
            return rep;
        }
        /// <summary>
        /// Metoda koja konvertuje a href u xref href
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceHrefToXref(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<a href=\"(.*?)\">(.*?)<\\/a>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string href = match.Groups[1].ToString();
                    string text = match.Groups[2].ToString();
                    bodyText = bodyText.Replace("<a href=\"" + href + "\">" + text + "</a>", "<xref href=\"" + href + "\" scope=\"external\" format=\"html\">" + text + "</xref>");
                }
            }
            return bodyText;
        }
        /// <summary>
        /// Metoda koja konvertuje a href u xref href
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceHrefHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<xref href=\"(.*?)\" scope=\"external\"(.*?)>(.*?)<\\/xref>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string href = match.Groups[1].ToString();
                    string format = match.Groups[2].ToString();
                    string text = match.Groups[3].ToString();
                    if (format == "")
                    {
                        bodyText = bodyText.Replace("<xref href=\"" + href + "\" scope=\"external\">" + text + "</xref>", "<a href=\"" + href + "\">" + text + "</a>");
                    }
                    else
                    {
                        bodyText = bodyText.Replace("<xref href=\"" + href + "\" scope=\"external\"" + format + ">" + text + "</xref>", "<a href=\"" + href + "\">" + text + "</a>");
                    }
                }
            }
            return bodyText;
        }
        /// <summary>
        /// Inicializuje prazan section div element za TextBox
        /// </summary>
        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            var div = new Sectiondiv("vp1");
            div.SectionDivs = new System.Collections.Generic.List<Sectiondiv>();
            Sectiondiv f11 = new Sectiondiv("f11");
            div.SectionDivs.Add(f11);
            Sectiondiv note = new Sectiondiv("note notewhite noteblack");
            f11.SectionDivs = new System.Collections.Generic.List<Sectiondiv>();
            f11.SectionDivs.Add(note);
            note.Content = "";
            root.SectionDivs.Add(div);
            return div;
        }
        public bool CopySelectedText()
        {
            IHTMLDocument2 document = this.Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject sel = document.selection;
            IHTMLTxtRange range = sel.createRange() as IHTMLTxtRange;
            if (range.text != "" && range.text != null)
            {
                Clipboard.SetText(range.text);
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool CutSelectedText()
        {
            IHTMLDocument2 document = this.Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject sel = document.selection;
            IHTMLTxtRange range = sel.createRange() as IHTMLTxtRange;
            if (range.text != "" && range.text != null)
            {
                Clipboard.SetText(range.text);
                document.execCommand("Cut", false, null);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Undo()
        {
            this.Document.ExecCommand("Undo", false, Type.Missing);
        }

        public void Redo()
        {
            this.Document.ExecCommand("Redo", false, Type.Missing);
        }
        public void Paste()
        {
            string text = Clipboard.GetText();
            Clipboard.Clear();
            try
            {
                // Radi trim neuobicajnih ASCII karaktera (rešava problem sa bulletima kopiranih iz worda)
                for (int i = 57344; i <= 63743; i++)
                {
                    text = text.Replace((char)i, ' ');
                }
                text = text.Replace("&shy;", "");
                text = Regex.Replace(text, @"[\u00AD]", string.Empty);
                Clipboard.SetText(text);
            }
            catch { }
            this.Document.ExecCommand("Paste", false, Type.Missing);

        }
        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == 0x101    //WM_KEYUP
             && msg.WParam.ToInt32() == (int)Keys.S && ModifierKeys == Keys.Control)
            {
                MainForm.Instance.btnSave.PerformClick();
                return true;
            }
            return base.PreProcessMessage(ref msg);
        }

        public void InitDocumentSettigns()
        {
            foreach (HtmlElement el in this.Document.All)
            {
                el.SetAttribute("unselectable", "on");
                el.SetAttribute("contenteditable", "false");
            }
            this.Document.Body.SetAttribute("width", this.Width.ToString() + "px");
            //  this.Document.Body.SetAttribute("height", "100%");
            this.Document.Body.SetAttribute("contenteditable", "true");
            this.Document.MouseDown += Doc_MouseDown;
            try
            {
                IHTMLDocument2 doc = Document.DomDocument as IHTMLDocument2;
                doc.designMode = "On";
            }
            catch { }
            try
            {
                Document.DomDocument.GetType().GetProperty("designMode").SetValue(Document.DomDocument, "On", null);
            }
            catch { }
            IsWebBrowserContextMenuEnabled = false;
            PreviewKeyDown += WebBrowserCustom_PreviewKeyDown;
            this.Document.AttachEventHandler("onkeydown", DocumentKeyDown);
        }

        /// <summary>
        /// Metoda koja prosiruje textbox u odnosu na velicinu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentKeyDown(object sender, EventArgs e)
        {
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }

        /// <summary>
        /// Konstuktor koji kreira webBrowser za text
        /// </summary>
        /// <param name="div"></param>
        public NoteControl(Sectiondiv div)
        {
            ScrollBarsEnabled = false;
            ScriptErrorsSuppressed = true;
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            this.Height = 16;
            //Pravimo meni i event za mouse down Delete dugmeta
            Application.DoEvents();
            this.Navigate("about:blank");
            Application.DoEvents();
            var uri = new System.Uri(Path.GetFullPath("style.css"));
            var converted = uri.AbsoluteUri;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            string text = rootSectionDiv.SectionDivs[0].SectionDivs[0].Content;
            text = replaceHrefHTML(text);
            string[] splittedColors = rootSectionDiv.SectionDivs[0].SectionDivs[0].Outputclass.Split(' ');
            if(splittedColors.Length > 1)
            {
                currentBackground = removeBackgroundLastChar(splittedColors[1]);
                currentColor = splittedColors[2];
            }
            rootSectionDiv.SectionDivs[0].SectionDivs[0].Outputclass = "note " + currentBackground + "b " + currentColor;
            string color = getColorFromName(currentBackground);
            string textColor = getColorFromName(currentColor);
            text = text.Replace("\r\n", "");
            if (text == "")
            {
                this.Document.OpenNew(true).Write("<html><head><link rel='stylesheet' href='" + converted + "' type='text/css'/><body style='font-size: 13px;margin-top: 1em !important; margin-bottom: 1em !important; background-color: " + color + " !important; color:" + textColor + " !important; padding: 6px 10px !important; border-radius: 8px !important; -moz-border-radius: 8px !important; -webkit-border-radius: 8px !important; font-family: \"Open Sans\",sans-serif;padding:0;margin:0;margin-left:10px;margin-right:10px;line-height: 18px !important;-ms-word-break:keep-all;word-wrap: break-word;word-break: keep-all;'><p>" + text + "</p></body></html>");
            }
            else
            {
                this.Document.OpenNew(true).Write("<html><head><link rel='stylesheet' href='" + converted + "' type='text/css'/><body style='font-size: 13px;margin-top: 1em !important; margin-bottom: 1em !important; background-color: " + color + " !important; color:" + textColor + " !important; padding: 6px 10px !important; border-radius: 8px !important; -moz-border-radius: 8px !important; -webkit-border-radius: 8px !important; font-family: \"Open Sans\",sans-serif;padding:0;margin:0;margin-left:10px;margin-right:10px;line-height: 18px !important;-ms-word-break:keep-all;word-wrap: break-word;word-break: keep-all;'>" + text + "</body></html>");

            }
            Application.DoEvents();
            InitDocumentSettigns();
            t = new Timer();
            t.Interval = 100;
            t.Tick += T_Tick;
            t.Start();
        }

        private string removeBackgroundLastChar(string s)
        {
            if(s.Length > 1 && s[s.Length-1] == 'b')
            {
                return s.Remove(s.Length - 1);
            }
            return s;
        }
        
        public void ReinitColors()
        {
            var uri = new System.Uri(Path.GetFullPath("style.css"));
            var converted = uri.AbsoluteUri;
            string text = rootSectionDiv.SectionDivs[0].SectionDivs[0].Content;
            string color2 = getColorFromName(currentBackground);
            string textColor2 = getColorFromName(currentColor);
            if (text == "")
            {
                this.Document.OpenNew(true).Write("<html><head><link rel='stylesheet' href='" + converted + "' type='text/css'/><body style='font-size: 13px;margin-top: 1em !important; margin-bottom: 1em !important; background-color: " + color2 + " !important; color:" + textColor2 + " !important; padding: 6px 10px !important; border-radius: 8px !important; -moz-border-radius: 8px !important; -webkit-border-radius: 8px !important; font-family: \"Open Sans\",sans-serif;padding:0;margin:0;margin-left:10px;margin-right:10px;line-height: 18px !important;-ms-word-break:keep-all;word-wrap: break-word;word-break: keep-all;'><p>" + text + "</p></body></html>");
            }
            else
            {
                this.Document.OpenNew(true).Write("<html><head><link rel='stylesheet' href='" + converted + "' type='text/css'/><body style='font-size: 13px;margin-top: 1em !important; margin-bottom: 1em !important; background-color: " + color2 + " !important; color:" + textColor2 + " !important; padding: 6px 10px !important; border-radius: 8px !important; -moz-border-radius: 8px !important; -webkit-border-radius: 8px !important; font-family: \"Open Sans\",sans-serif;padding:0;margin:0;margin-left:10px;margin-right:10px;line-height: 18px !important;-ms-word-break:keep-all;word-wrap: break-word;word-break: keep-all;'>" + text + "</body></html>");
            }
            rootSectionDiv.SectionDivs[0].SectionDivs[0].Outputclass = "note " + currentBackground + "b " + currentColor;
        }

        public void ReinitBackground(string color)
        {
            currentBackground = color;
            ReinitColors();
        }

        public void ReinitColor(string color)
        {
            currentColor = color;
            ReinitColors();
        }

        public string getColorFromName(string name)
        {
            string color = "";
            switch (name)
            {
                case "notewhite":
                    color = "white";
                    break;
                case "noteblack":
                    color = "black";
                    break;
                case "notered":
                    color = "#9a2617";
                    break;
                case "noteyellow":
                    color = "#ebc944";
                    break;
                case "noteblue":
                    color = "#107896";
                    break;
                case "notegreen":
                    color = "#829356";
                    break;
                case "notegray":
                    color = "#eaeaea";
                    break;
                case "noteorange":
                    color = "#c2571a";
                    break;
                case "notecyan":
                    color = "#43abc9";
                    break;
            }
            return color;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            ReInitData();
        }

        public void UnFocusAll()
        {
            foreach (Control conp in Parent.Parent.Controls)
            {
                foreach (Control con in conp.Controls)
                {
                    if (con is NoteControl && con != this)
                    {
                        NoteControl co = (NoteControl)con;
                        co.Focus();
                        IHTMLDocument2 htmlDocument = co.Document.DomDocument as IHTMLDocument2;
                        htmlDocument.selection.empty();
                        co.Document.ExecCommand("Unselect", false, Type.Missing);
                        co.t.Stop();
                    }
                }
            }
        }
        /// <summary>
        /// MouseDown metoda za dokument web browsera
        /// Napravljeno jer MouseDown event nad WebBrowserom ne radi (U Focus-u je dokument) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Doc_MouseDown(object sender, HtmlElementEventArgs e)
        {
            UnFocusAll();
            this.t.Start();
            this.Focus();
        }

        public void ReInitData()
        {
            if (!this.IsDisposed)
            {
                if (Document != null && Document.Body != null && Document.Body.ScrollRectangle != null)
                    this.Height = this.Document.Body.ScrollRectangle.Height;

                if (Document != null && Document.Body != null)
                {
                    if (isInit)
                    {
                        Document.Body.DragOver += BodyOnDragOver;
                        docInitText = GetXmlForElement();
                        isInit = false;
                    }
                    if (GetXmlForElement() != docInitText) 
                        rootSectionDiv.SectionDivs[0].SectionDivs[0].Content = GetXmlForElement();
                }
                if (Parent != null)
                {
                    //((DivControl)this.Parent).CheckMouseLeave();
                    if (!Util.CheckText((SelectableFlowPanel)Parent.Parent) && this.Focused)
                    {
                        DoUndo();
                    }
                }
            }
            else
            {
                t.Dispose();
            }
        }

        private void BodyOnDragOver(object sender, HtmlElementEventArgs htmlElementEventArgs)
        {
            ((DivControl)Parent).StartDrag();
        }

        /// <summary>
        /// Hvata klik tastature 
        /// Metoda resava PASTE SPECIAL .
        /// Auto resize i slicno
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebBrowserCustom_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Control)
            {
                string text = Clipboard.GetText();
                Clipboard.Clear();
                try
                {
                    Clipboard.SetText(text);
                }
                catch { }
            }
        }

        private void DoUndo()
        {
            Document.ExecCommand("Undo", false, null);
        }
        
        /// <summary>
        /// HTML get from browser
        /// </summary>
        /// <returns></returns>
        public string HTML()
        {
            mshtml.HTMLDocument doc = this.Document.DomDocument as mshtml.HTMLDocument;
            string html = doc.body.innerHTML;
            if (html != null)
            {
                Document document = NSoupClient.Parse(html);
                string bodyText = document.Select("body").First.Html();
                bodyText = bodyText.Replace("<em>", "<i>").Replace("</em>", "</i>")
                    .Replace("<strong>", "<b>").Replace("</strong>", "</b>");
                return bodyText;
            }
            else
            {
                return "";
            }
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (Parent != null)
            {
                //if (!((SelectableFlowPanel)this.Parent.Parent).Column.SectionDiv.Contains(rootSectionDiv))
                //    ((SelectableFlowPanel)this.Parent.Parent).Column.SectionDiv.Add(rootSectionDiv);
                this.Width = Parent.Width;
            }
        }

        /// <summary>
        /// Metoda koja konvertuje foreign word u font color 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceForeignWordHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<ph outputclass=\"foreignword\">(.*?)<\\/ph>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<ph outputclass=\"foreignword\">" + s + "</ph>", "<font color=\"#ba3b06\">" + s + "</font>");
                }
            }
            return bodyText;
        }

        public string GetXmlForElement()
        {
            if (!HTML().StartsWith("<p>"))
            {

                if (HTML() == "")
                {
                    return "";
                }
                string html = HTML();
                if (html.Contains("style"))
                {
                    html = html.Replace("<font style=\"BACKGROUND-COLOR: #ffffff\">", "");
                    html = html.Replace("</font>", "");
                }
                html = replaceHrefToXref(html);
                return "<p>" + html + "</p>";
            }
            else
            {
                string html = HTML();
                if (html.Contains("style"))
                {

                    html = html.Replace("<font style=\"BACKGROUND-COLOR: #ffffff\">", "");
                    html = html.Replace("</font>", "");
                }
                html = replaceHrefToXref(html);
                return html;
            }
        }

    }
}
