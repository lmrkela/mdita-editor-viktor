using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using NSoup;
using NSoup.Nodes;

namespace mDitaEditor.Dita.Controls
{
    public class MathMlLoader : WebBrowser
    {
        private Sectiondiv rootSectionDiv;
        private Timer t;

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
            var div = new Sectiondiv("vp1");
            div.SectionDivs = new List<Sectiondiv>();
            Sectiondiv f11 = new Sectiondiv("f11");
            div.SectionDivs.Add(f11);
            root.SectionDivs.Add(div);
            return div;
        }

        public void InitMenu()
        {
            ScrollBarsEnabled = false;
        }

        public void InitDocumentSettigns()
        {
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
        public MathMlLoader(Sectiondiv div)
        {
            ScrollBarsEnabled = false;
            ScriptErrorsSuppressed = true;
            rootSectionDiv = div;
            Height = 16;
            InitMenu();
            //Pravimo meni i event za mouse down Delete dugmeta
            Application.DoEvents();
            this.Navigate("about:blank");
            Application.DoEvents();
            var uri = new System.Uri(Path.GetFullPath("style.css"));
            var converted = uri.AbsoluteUri;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            string text = rootSectionDiv.SectionDivs[0].Content;
            text = replaceHTML(text);

            text = text.Replace("<m:", "<");
            text = text.Replace("</m:", "</");
            if (text == "")
            {
               DocumentText = ("<?xml version =\"1.0\" encoding=\"UTF-8\"?>\r\n<!DOCTYPE html\r\n  PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xml:lang=\"en-us\" lang=\"en-us\">\r\n<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=9; IE=8; IE=7\"><link rel='stylesheet' href='" + converted + "' type='text/css'/>\r\n<style>*{display: inline-block; text-align:left !important;float:left;} .MathJax_Display {margin: 0em 0em !important;}</style><script type=\"text/x-mathjax-config\">\r\n  MathJax.Hub.Config({ tex2jax: {inlineMath: [['$','$'], ['\\\\(','\\\\)']]},\"HTML-CSS\": {\r\n\t\tpreferredFont: \"STIX\",\r\n\t\tscale: 90\r\n    } });\r\n</script>\r\n<script type=\"text/javascript\" async\r\n  src=\"https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML\">\r\n</script>\r\n</head>\r\n<body style='font-size: 12px !important; font-family: \"Open Sans\",sans-serif;padding:0;margin:0;-ms-word-break:keep-all;word-wrap: break-word;word-break: keep-all;margin-right:2px !important;'><p>" + text + "</p></body></html>");

            }
            else
            {
               DocumentText = ("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<!DOCTYPE html\r\n  PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xml:lang=\"en-us\" lang=\"en-us\">\r\n<head><meta http-equiv=\"X-UA-Compatible\" content=\"IE=9; IE=8; IE=7\"><link rel='stylesheet' href='" + converted + "' type='text/css'/>\r\n<style>d4p_eqn_inline{display: inline-block;text-align:left !important; } .MathJax_Display {margin: 0em 0em !important;} </style><script type=\"text/x-mathjax-config\">\r\n  MathJax.Hub.Config({ tex2jax: {inlineMath: [['$','$'], ['\\\\(','\\\\)']]},\"HTML-CSS\": {\r\n\t\tpreferredFont: \"STIX\",\r\n\t\tscale: 90\r\n    } });\r\n</script>\r\n<script type=\"text/javascript\" async\r\n  src=\"https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML\">\r\n</script>\r\n</head>\r\n<body style='font-size: 12px !important; font-family: \"Open Sans\",sans-serif;padding:0;margin:0;margin-right:2px !important;'>" + text + "</body></html>");
            }

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

        public void ReInitData()
        {
            if (!this.IsDisposed)
            {
                if (Document != null && Document.Body != null && Document.Body.ScrollRectangle != null)
                    this.Height = this.Document.Body.ScrollRectangle.Height+10;
                if (Parent != null)
                {
                    if (!Util.CheckText((SelectableFlowPanel)Parent.Parent) && this.Focused)
                    {
                        DoUndo();
                    }
                }
            }
            else
            {
              //  t.Dispose();
            }
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
            Document.ExecCommand("Bold", false, null);
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }

        /// <summary>
        /// Italic selected text in Browser
        /// </summary>
        public void ItalicSelected()
        {
            Document.ExecCommand("Italic", false, null);
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }

        /// <summary>
        /// Makes link for  text in Browser
        /// </summary>
        public void MakeLink(string link)
        {
            Document.ExecCommand("createlink", false, link);
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }

        /// <summary>
        /// Underline selected text in Browser
        /// </summary>
        public void UnderlineSelected()
        {
            Document.ExecCommand("Underline", false, null);
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }

        /// <summary>
        /// Change selected text to hex color
        /// </summary>
        /// <param name="Color">Hex color</param>
        public void ColorSelected(string Color)
        {
            Document.ExecCommand("ForeColor", false, Color);
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }

        /// <summary>
        ///  Create Bullet list for selected text
        /// </summary>
        public void CreateListBullet()
        {
            Document.ExecCommand("InsertUnorderedList", false, null);
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }

        /// <summary>
        /// Create Numerical list for selected text
        /// </summary>
        public void CreateListNumbered()
        {
            Document.ExecCommand("InsertOrderedList", false, null);
            this.Height = this.Document.Body.ScrollRectangle.Height;
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
            this.Height = this.Document.Body.ScrollRectangle.Height;
        }
        
        /// <summary>
        /// Metoda koja konvertuje font color u DITA term
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceTerm(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<font color=\"#0173b9\"><b><i>(.*?)<\\/i><\\/b><\\/font>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<font color=\"#0173b9\"><b><i>" + s + "</i></b></font>", "<term outputclass=\"standard\">" + s + "</term>");
                }
            }
            Regex regex2 = new Regex("<b><i><font color=\"#0173b9\">(.*?)<\\/font><\\/i><\\/b>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex2.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<b><i><font color=\"#0173b9\">" + s + "</font></i></b>", "<term outputclass=\"standard\">" + s + "</term>");
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
            Regex regex = new Regex("<font color=\"#c4b64a\">(.*?)<\\/font>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<font color=\"#c4b64a\">" + s + "</font>", "<ph outputclass=\"pulledquote\">" + s + "</ph>");
                }
            }
            return bodyText;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            //if (Parent != null)
            //{
            //    if (!((SelectableFlowPanel)Parent.Parent).Column.SectionDiv.Contains(rootSectionDiv))
            //        ((SelectableFlowPanel)Parent.Parent).Column.SectionDiv.Add(rootSectionDiv);
            //    this.Width = Parent.Width;
            //}
        }

        /// <summary>
        /// Metoda koja konvertuje font color u Keyword
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        public string replaceKeyword(string current)
        {
            string bodyText = current;
            Regex regex = new Regex("<font color=\"#ff0000\"><u>(.*?)<\\/u><\\/font>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<font color=\"#ff0000\"><u>" + s + "</u></font>", "<keyword>" + s + "</keyword>");
                }
            }
            Regex regex2 = new Regex("<u><font color=\"#ff0000\">(.*?)<\\/font><\\/u>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex2.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<u><font color=\"#ff0000\">" + s + "</font></u>", "<keyword>" + s + "</keyword>");
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
            Regex regex = new Regex("<b><i><font color=\"#412977\">(.*?)<\\/font><\\/i><\\/b>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<b><i><font color=\"#412977\">" + s + "</font></i></b>", "<ph outputclass=\"phrase\">" + s + "</ph>");
                }
            }
            Regex regex2 = new Regex("<font color=\"#412977\"><b><i>(.*?)<\\/i><\\/b><\\/font>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex2.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<font color=\"#412977\"><b><i>" + s + "</i></b></font>", "<ph outputclass=\"phrase\">" + s + "</ph>");
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
            Regex regex = new Regex("<font color=\"#00a651\">(.*?)<\\/font>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<font color=\"#00a651\">" + s + "</font>", "<ph outputclass=\"reservedword\">" + s + "</ph>");
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
            Regex regex = new Regex("<a href=\"(.*?)\">(.*?)<\\/a>");
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
            Regex regex = new Regex("<xref href=\"(.*?)\" scope=\"external\"(.*?)>(.*?)<\\/xref>");
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
            Regex regex = new Regex("<font color=\"#ba3b06\">(.*?)<\\/font>");
            // var v = regex.Match(bodyText);
            foreach (Match match in regex.Matches(bodyText))
            {
                if (match.Groups.Count > 0)
                {
                    string s = match.Groups[1].ToString();
                    bodyText = bodyText.Replace("<font color=\"#ba3b06\">" + s + "</font>", "<ph outputclass=\"foreignword\">" + s + "</ph>");
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
            Regex regex = new Regex("<term outputclass=\"standard\">(.*?)<\\/term>");
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
            Regex regex = new Regex("<ph outputclass=\"pulledquote\">(.*?)<\\/ph>");
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
            Regex regex = new Regex("<keyword>(.*?)<\\/keyword>", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
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
            Regex regex = new Regex("<ph outputclass=\"phrase\">(.*?)<\\/ph>");
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
            Regex regex = new Regex("<ph outputclass=\"reservedword\">(.*?)<\\/ph>");
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
    }
}
