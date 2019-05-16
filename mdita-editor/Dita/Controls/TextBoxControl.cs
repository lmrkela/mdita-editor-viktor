using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using mshtml;
using NSoup;
using NSoup.Nodes;
using System.Diagnostics;

namespace mDitaEditor.Dita.Controls
{
    public class TextBoxControl : WebBrowser
    {
        private HtmlElement current;
        private Sectiondiv rootSectionDiv;
        public Timer t;
        public bool isInit = true;
        public static Font defaultFont = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
  
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

        /// <summary>
        /// Inicializuje prazan section div element za TextBox
        /// </summary>
        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            Sectiondiv div = new Sectiondiv("vp1");
            div.SectionDivs = new List<Sectiondiv>();
            Sectiondiv f11 = new Sectiondiv("f11");
            div.SectionDivs.Add(f11);            
            root.SectionDivs.Add(div);
            return div;
        }

        public void InitDocumentSettigns()
        {
            foreach (HtmlElement el in Document.All)
            {
                el.SetAttribute("unselectable", "on");
                el.SetAttribute("contenteditable", "false");
            }
            Document.Body.SetAttribute("width", Width.ToString() + "px");
            Document.Body.SetAttribute("contenteditable", "true");
            Document.Body.SetAttribute("spellcheck", "true");
            Document.MouseDown += Doc_MouseDown;
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
            Document.AttachEventHandler("onkeydown", DocumentKeyDown);
            Document.DetachEventHandler("ondblclick", Document_DoubleClick);
            Document.AttachEventHandler("ondblclick", Document_DoubleClick);
            Document.AttachEventHandler("onclick", document_click);
        }
        public string CurrentSelection()
        {
            IHTMLDocument2 doc = Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject sel = doc.selection;
            IHTMLTxtRange rng2 = sel.createRange() as IHTMLTxtRange;
            rng2.expand("word");

            if(rng2.text != null)
            {
                return rng2.text;
            }
            else
            {
                return "";
            }
        }
        private void document_click(object sender, EventArgs e)
        {
            Console.WriteLine("CLICK");
            Point ScreenCoord = new Point(MousePosition.X, MousePosition.Y);
            Point BrowserCoord = PointToClient(ScreenCoord);
            HtmlElement elem = Document.GetElementFromPoint(BrowserCoord);
            Console.WriteLine(elem.TagName);
            current = elem;
        }

        // Add this to your project
        private void Document_DoubleClick(object sender, EventArgs e)
        {
            IHTMLDocument2 doc = Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject sel = doc.selection;
            IHTMLTxtRange rng2 = sel.createRange() as IHTMLTxtRange;
            rng2.expand("word");
            IHTMLTxtRange rng = sel.createRange() as IHTMLTxtRange;
            rng.expand("word");
            rng.moveEnd("character", -1);
            IHTMLSelectionObject currentSelection = doc.selection;
            if (rng2.text != null)
            {
                if (rng2.text.EndsWith(" "))
                {
                    rng.select();
                }
                else
                {
                    rng2.select();
                }
            }
        }
        public void CheckSelection()
        {
            IHTMLDocument2 doc = Document.DomDocument as IHTMLDocument2;
            IHTMLSelectionObject sel = doc.selection;
            IHTMLTxtRange rng2 = sel.createRange() as IHTMLTxtRange;
            string rngOld = rng2.text;
           
            rng2.expand("word");
            if(rngOld?.Trim() != rng2.text?.Trim())
            {
                return;
            }
            IHTMLTxtRange rng = sel.createRange() as IHTMLTxtRange;
            rng.expand("word");
            rng.moveEnd("character", -1);
            IHTMLSelectionObject currentSelection = doc.selection;
            if (rng2.text != null)
            {
                if (rng2.text.EndsWith(" "))
                {
                    rng.select();
                }
                else
                {
                    rng2.select();
                }
            }
        }

        /// <summary>
        /// Metoda koja prosiruje textbox u odnosu na velicinu 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentKeyDown(object sender, EventArgs e)
        {
            Height = Document.Body.ScrollRectangle.Height;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            MainForm.Instance.CheckErrorsAndStatistics();
        }

        /// <summary>
        /// Konstuktor koji kreira webBrowser za text
        /// </summary>
        /// <param name="div"></param>
        public TextBoxControl(Sectiondiv div)
        {
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            Height = 16;
            ScrollBarsEnabled = false;
            ScriptErrorsSuppressed = true;
            this.AllowWebBrowserDrop = false;
            //Pravimo meni i event za mouse down Delete dugmeta
            Application.DoEvents();
            Navigate("about:blank");
            Application.DoEvents();
            var uri = new Uri(Path.GetFullPath("style.css"));
            var converted = uri.AbsoluteUri;
            Font = defaultFont;
            string text = rootSectionDiv.SectionDivs[0].Content;
            text = replaceHTML(text);
            if (text == "")
            {
                text = "<p> </p>";
            }
            Document.OpenNew(true).Write("<html><head><link rel='stylesheet' href='" + converted + "' type='text/css'/><body spellcheck='true' style='font-size: 12px !important; font-family: \"Open Sans\",sans-serif;padding:0;margin:0;line-height: 18px !important;-ms-word-break:keep-all;word-wrap: break-word;word-break: keep-all;margin-right:2px !important;'>" + text + "</body></html>");
            Application.DoEvents();
            InitDocumentSettigns();


            t = new Timer();
            t.Interval = 100;
            t.Tick += T_Tick;
            t.Start();
        }
        private void T_Tick(object sender, EventArgs e)
        {
            ReInitData();
        }

        public void Paste()
        {
            string text = Clipboard.GetText();
            //Debug.WriteLine("Paste: " + text);
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
            catch { Debug.WriteLine("Problem"); }
            this.Document.ExecCommand("Paste", false, Type.Missing);
            Debug.WriteLine(DocumentText);

        }

        public void UnFocusAll()
        {
            foreach (Control conp in Parent.Parent.Controls)
            {
                foreach (Control con in conp.Controls)
                {
                    if (con is TextBoxControl && con != this)
                    {
                        TextBoxControl co = (TextBoxControl)con;
                        co.Focus();
                        IHTMLDocument2 htmlDocument = co.Document.DomDocument as IHTMLDocument2;
                        htmlDocument.selection.empty();
                        co.t.Stop();
                        co.Document.ExecCommand("Unselect", false, Type.Missing);
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
            Focus();
            t.Start();
        }

        public void Undo()
        {
            this.Document.ExecCommand("Undo", false, Type.Missing);
        }

        public void Redo()
        {
            this.Document.ExecCommand("Redo", false, Type.Missing);
        }

        public void ReInitData()
        {

           // if (!IsDisposed && ((SelectableFlowPanel)((DivControl)Parent).Parent).HeightLeftPanel() < 15) {                
           //     t.Dispose();           
           //     DitaClipboard.Undo();           
           //    MessageBox.Show("Nema više mesta na odabranoj sekciji");
           //     return;
           // }
           

            if (!IsDisposed)
            {
                if (Document != null && Document.Body != null && Document.Body.ScrollRectangle != null)
                {
                   
                    Height = Document.Body.ScrollRectangle.Height;
                                       
                      
                    
                }

                if (Document != null && Document.Body != null)
                {
                    if (isInit)
                    {
                        Document.Body.DragOver += BodyOnDragOver;
                        isInit = false;
                    }
                    // if (GetXmlForElement() != docInitText)
                    //   {
                   
                        rootSectionDiv.SectionDivs[0].Content = GetXmlForElement();
//}
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

        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == 0x101 && msg.WParam.ToInt32() == (int)Keys.S && ModifierKeys == Keys.Control)
            {
                MainForm.Instance.btnSave.PerformClick();
                return true;
            }
            return base.PreProcessMessage(ref msg);
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

        /// <summary>
        /// Hvata klik tastature 
        /// Metoda resava PASTE SPECIAL.
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
            }
            if ((e.KeyCode == Keys.Enter && e.Shift) || e.KeyCode == Keys.Enter)
            {
                ClearFormat();
            }
            if (e.KeyCode == Keys.Space)
            {
                ClearFormat();
            }
        }

        private void DoUndo()
        {
            Document.ExecCommand("Undo", false, null);
        }

        /// <summary>
        /// Bold selected text in Browser
        /// </summary>
        public void BoldSelected()
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            Console.WriteLine(Document.ActiveElement.TagName);

            if (currentSelection != null)
            {
                var selectionText = currentSelection.createRange();
                if (selectionText.Text != "" && selectionText.Text != null)
                {
                    Document.ExecCommand("Bold", false, null);
                    Height = Document.Body.ScrollRectangle.Height;
                }
            }
        }

        /// <summary>
        /// Italic selected text in Browser
        /// </summary>
        public void ItalicSelected()
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                var selectionText = currentSelection.createRange();
                if (selectionText.Text != "" && selectionText.Text != null)
                {
                    Document.ExecCommand("Italic", false, null);
                    Height = Document.Body.ScrollRectangle.Height;
                }
            }
        }

        /// <summary>
        /// Makes link for  text in Browser
        /// </summary>
        public void MakeLink(string link)
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                var selectionText = currentSelection.createRange();
                if (selectionText.Text != "" && selectionText.Text != null)
                {
                    Document.ExecCommand("createlink", false, link);
                    Height = Document.Body.ScrollRectangle.Height;
                }
            }
        }

        /// <summary>
        /// Underline selected text in Browser
        /// </summary>
        public void UnderlineSelected()
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                var selectionText = currentSelection.createRange();
                if (selectionText.Text != "" && selectionText.Text != null)
                {
                    Document.ExecCommand("Underline", false, null);
                    Height = Document.Body.ScrollRectangle.Height;
                }
            }
        }

        /// <summary>
        /// Change selected text to hex color
        /// </summary>
        /// <param name="Color">Hex color</param>
        public void ColorSelected(string Color)
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                var selectionText = currentSelection.createRange();
                if (selectionText.Text != "" && selectionText.Text != null)
                {
                    Document.ExecCommand("ForeColor", false, Color);
                    Height = Document.Body.ScrollRectangle.Height;
                }
            }
        }

        /// <summary>
        ///  Create Bullet list for selected text
        /// </summary>
        public void CreateListBullet()
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            
            if (currentSelection != null)
            {
                Document.ExecCommand("InsertUnorderedList", false, null);   
                            
                Height = Document.Body.ScrollRectangle.Height;
            }
        }

        public void CreateSubList(string s)
        {          

            if (current != null && current.TagName == "LI")
            {
                Debug.WriteLine(current.TagName);
                HtmlElement hdoc = Document.CreateElement(s);
                HtmlElement item = Document.CreateElement("li"); 
                hdoc.AppendChild(item);
                current.AppendChild(hdoc);     
                item.InnerText = "";
                item.Focus();
                
       
            }
            else
            {
                MessageBox.Show("Prvo morate kliknuti na listu u okviru teskta!");
            }
        }
        
        string output = "";
        
        /// <summary>
        /// Create Numerical list for selected text
        /// </summary>
        public void CreateListNumbered()
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;
            if (currentSelection != null)
            {

                Form listForm = new Form();
                listForm.Text = "Od kog broja zelite da lista pocne?";
                listForm.Width = 400;
                listForm.Height = 130;
                TextBox txtResult = new TextBox();
                txtResult.Location = new System.Drawing.Point(10, 10);

                Button button1 = new Button();
                button1.Location = new System.Drawing.Point(10, 50);
                button1.Text = "Nova lista";

                Button button2 = new Button();
                button2.Location = new System.Drawing.Point(100, 50);
                button2.Text = "Nastavi listu";
                listForm.Controls.AddRange(new Control[] { txtResult, button1, button2 });


                button1.Click += (sender, args) =>
                {
                    Document.ExecCommand("InsertOrderedList", false, null);
                    Height = Document.Body.ScrollRectangle.Height;
                    listForm.Dispose();
                };

                button2.Click += (sender, args) =>
                {
                    int value;
                    if (txtResult.Text == "") MessageBox.Show("Morate uneti broj!");
                    if (int.TryParse(txtResult.Text, out value) && value >= 1)
                    {
                        output = txtResult.Text;
                        Document.ExecCommand("InsertOrderedList", false, null);
                        Height = Document.Body.ScrollRectangle.Height;
                        var result = Document.GetElementsByTagName("ol");

                        result[0].SetAttribute("start", "" + output);

                        listForm.Dispose();
                    }

                };


                if (listForm.ShowDialog(this) == DialogResult.OK)
                {

                }
                else
                {
                }
                
            }
        }

        public void CreateListNumbered(int start)
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                Document.ExecCommand("InsertOrderedList", false, null);
                Height = Document.Body.ScrollRectangle.Height;
                var result = Document.GetElementsByTagName("ol");
                result[0].SetAttribute("start", ""+start);

            }
        }

        public string replaceHTML(string replacement)
        {
            string rep = replacement;

            rep = replaceTermHTML(rep);
            rep = replaceForeignWordHTML(rep);
            rep = replaceHighlightHTML(rep);
            rep = replaceKeywordHTML(rep);
            rep = replaceReservedWordHTML(rep);
            rep = replacePhraseHTML(rep);
            rep = replaceHrefHTML(rep);
            return rep;
        }

        public void MessageBoxHTML()
        {

            HTMLDocument doc = Document.DomDocument as HTMLDocument;
            string html = doc.body.innerHTML;
            MessageBox.Show(html);
        }

        /// <summary>
        /// HTML get from browser
        /// </summary>
        /// <returns></returns>
        public string HTML()
        {
            HTMLDocument doc = Document.DomDocument as HTMLDocument;
            string html = doc.body.innerHTML;
            if (html != null)
            {
                Document document = NSoupClient.Parse(html);
                string bodyText = document.Select("body").First.Html();
                bodyText = bodyText.Replace("<em>", "<i>").Replace("</em>", "</i>")
                    .Replace("<strong>", "<b>").Replace("</strong>", "</b>");

                bodyText = replaceTerm(bodyText);
                bodyText = replaceForeignWord(bodyText);
                bodyText = replaceHighlight(bodyText);
                bodyText = replaceKeyword(bodyText);
                bodyText = replaceReservedWord(bodyText);
                bodyText = replacePhrase(bodyText);
                bodyText = replaceHrefToXref(bodyText);
                return bodyText;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// Deletes format from selected text
        /// </summary>
        public void ClearFormat()
        {
            Document.Focus();
            Document.ExecCommand("removeFormat", false, "foreColor");
            Height = Document.Body.ScrollRectangle.Height;
        }

        private void SuppressScriptErrorsOnly(WebBrowser browser)
        {
            // Ensure that ScriptErrorsSuppressed is set to false.
            browser.ScriptErrorsSuppressed = true;
            // Handle DocumentCompleted to gain access to the Document object.
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            ((WebBrowser)sender).Document.Window.Error += new HtmlElementErrorEventHandler(Window_Error);
        }

        private void Window_Error(object sender, HtmlElementErrorEventArgs e)
        {
            // Ignore the error and suppress the error dialog box. 
            e.Handled = true;
        }

        /// <summary>
        /// Metoda koja konvertuje font color u DITA term
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceTerm(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<font color=\"#0173b9\"><b><i>(.*?)<\\/i><\\/b><\\/font>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<font color=\"#0173b9\"><b><i>" + match.Groups[1].ToString() + "</i></b></font>", "<term outputclass=\"standard\">" + s + toAppend + "</term>");
                }
            }
            Regex regex2 = new Regex("<b><i><font color=\"#0173b9\">(.*?)<\\/font><\\/i><\\/b>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex2.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<b><i><font color=\"#0173b9\">" + match.Groups[1].ToString() + "</font></i></b>", "<term outputclass=\"standard\">" + s + toAppend + "</term>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje font color u highlight
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceHighlight(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<font color=\"#c4b64a\">(.*?)<\\/font>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<font color=\"#c4b64a\">" + match.Groups[1].ToString() + "</font>", "<ph outputclass=\"pulledquote\">" + s+ toAppend + "</ph>");
                }
            }
            return bodyText;
        }

        public static string ScrubHtml(string value)
        {
            var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
            var step2 = Regex.Replace(step1, @"\s{2,}", " ");
            return step2;
        }

        public string ToAppendString(string s)
        {
            string toAppend = "";
            if (s.EndsWith(" "))
            {
                toAppend = " ";
            }
            return toAppend;
        }
        /// <summary>
        /// Metoda koja konvertuje font color u Keyword
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceKeyword(string current)
        {
            string bodyText = current;
            Regex regex2 = new Regex("<u><font color=\"#ff0000\">(.*?)<\\/font><\\/u>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex2.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<u><font color=\"#ff0000\">" + match.Groups[1].ToString() + "</font></u>", "<keyword>" + s + toAppend + "</keyword>");
                }
            }
            Regex regex = new Regex("<font color=\"#ff0000\"><u>(.*?)<\\/u><\\/font>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<font color=\"#ff0000\"><u>" + match.Groups[1].ToString() + "</u></font>", "<keyword>" + s + toAppend + "</keyword>");
                }
            }

            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje font color u frazu
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replacePhrase(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<b><i><font color=\"#412977\">(.*?)<\\/font><\\/i><\\/b>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<b><i><font color=\"#412977\">" + match.Groups[1].ToString() + "</font></i></b>", "<ph outputclass=\"phrase\">" + s + toAppend + "</ph>");
                }
            }
            Regex regex2 = new Regex("<font color=\"#412977\"><b><i>(.*?)<\\/i><\\/b><\\/font>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex2.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<font color=\"#412977\"><b><i>" + match.Groups[1].ToString() + "</i></b></font>", "<ph outputclass=\"phrase\">" + s + toAppend + "</ph>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje font color u reserved word
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceReservedWord(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<font color=\"#00a651\">(.*?)<\\/font>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    string toAppend = ToAppendString(s);
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<font color=\"#00a651\">" + match.Groups[1].ToString() + "</font>", "<ph outputclass=\"reservedword\">" + s + toAppend + "</ph>");
                }
            }
            return bodyText;
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
        /// Superscript selected text in Browser
        /// </summary>
        public void Superscript()
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                var selectionText = currentSelection.createRange();
                if (selectionText.Text != "" && selectionText.Text != null)
                {
                    Document.ExecCommand("superscript", false, null);
                    Height = Document.Body.ScrollRectangle.Height;
                }
            }
        }

        /// <summary>
        /// Superscript selected text in Browser
        /// </summary>
        public void Subscript()
        {
            IHTMLDocument2 htmlDocument = this.Document.DomDocument as IHTMLDocument2;

            IHTMLSelectionObject currentSelection = htmlDocument.selection;

            if (currentSelection != null)
            {
                var selectionText = currentSelection.createRange();
                if (selectionText.Text != "" && selectionText.Text != null)
                {
                    Document.ExecCommand("subscript", false, null);
                    Height = Document.Body.ScrollRectangle.Height;
                }
            }
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
        /// Metoda koja konvertuje font color u foreign word
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceForeignWord(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<font color=\"#ba3b06\">(.*?)<\\/font>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    s = ScrubHtml(s);
                    bodyText = bodyText.Replace("<font color=\"#ba3b06\">" + match.Groups[1].ToString() + "</font>", "<ph outputclass=\"foreignword\">" + s + "</ph>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje  DITA term u font color
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceTermHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<term outputclass=\"standard\">(.*?)<\\/term>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<term outputclass=\"standard\">" + s + "</term>", "<font color=\"#0173b9\"><b><i>" + s + "</i></b></font>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje highlight u font color 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceHighlightHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<ph outputclass=\"pulledquote\">(.*?)<\\/ph>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<ph outputclass=\"pulledquote\">" + s + "</ph>", "<font color=\"#c4b64a\">" + s + "</font>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje  Keyword u font color 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceKeywordHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<keyword>(.*?)<\\/keyword>", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<keyword>" + s + "</keyword>", "<font color=\"#ff0000\"><u>" + s + "</u></font>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje frazu u font color
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replacePhraseHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<ph outputclass=\"phrase\">(.*?)<\\/ph>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<ph outputclass=\"phrase\">" + s + "</ph>", "<b><i><font color=\"#412977\">" + s + "</font></i></b>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje reserved word u font color 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceReservedWordHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<ph outputclass=\"reservedword\">(.*?)<\\/ph>", RegexOptions.Singleline);
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<ph outputclass=\"reservedword\">" + s + "</ph>", "<font color=\"#00a651\">" + s + "</font>");
                }
            }
            return bodyText;
        }

        /// <summary>
        /// Metoda koja konvertuje foreign word u font color 
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceForeignWordHTML(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<ph outputclass=\"foreignword\">(.*?)<\\/ph>", RegexOptions.Singleline);
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
            string html = HTML();
            if (!html.StartsWith("<p>"))
            {
                if (html == "")
                {
                    return "";
                }
                html = "<p>" + html + "</p>";
            }
            return html;
        }
    }
}
