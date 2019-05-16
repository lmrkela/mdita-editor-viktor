using HtmlAgilityPack;
using mDitaEditor.Dita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace mDitaEditor.Utils
{
    class DitaXmlFixer
    {
        /// <summary>
        /// Getuje DTD za objekat
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string DtdForObject(object item)
        {
            if (item is LearningContent)
            {
                return
                    "<!DOCTYPE learningContent PUBLIC \"-//OASIS//DTD DITA 1.2 Learning Content//EN\" \"learningContent.dtd\">";
            }
            else if (item is LearningOverview)
            {
                return
                    "<!DOCTYPE learningOverview PUBLIC \"-//OASIS//DTD DITA 1.2 Learning Overview//EN\" \"learningOverview.dtd\">";
            }
            else
            {
                return
                    "<!DOCTYPE learningSummary PUBLIC \"-//OASIS//DTD DITA 1.2 Learning Summary//EN\" \"learningSummary.dtd\">";
            }
        }

        public static StringBuilder FixEncoding(StringBuilder sb, object item)
        {
            sb.Replace("encoding=\"utf-16\"", "encoding=\"utf-8\"");
            sb.Replace(
                " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"",
                "");
            sb.Replace("encoding=\"utf-8\"?>", "encoding=\"utf-8\"?>" + DtdForObject(item));
            return sb;
        }

        /// <summary>
        /// Radi skidanje CDATA sa Figure Tag-a
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixFigure(string forReturn)
        {
            forReturn = forReturn.Replace("&lt;fig&gt;", "<fig>");
            forReturn = Regex.Replace(forReturn,
                "<sectiondiv outputclass=\"vp[0-9]\">[ \t\r\n\v\f]*<fig>(.*?)<\\/sectiondiv>", delegate(Match match)
                {
                    string v = match.ToString();
                    return WebUtility.HtmlDecode(v).Replace("&nbsp;", "");
                }, RegexOptions.Singleline);
            return forReturn;
        }

        /// <summary>
        /// Radi skidanje CDATA sa YouTube Tag-a
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixYouTube(string forReturn)
        {
            forReturn = Regex.Replace(forReturn,
                "&lt;p&gt;[ \t\r\n\v\f]*&lt;youtube[^<>]*>(.*?)&lt;/youtube&gt;[ \t\r\n\v\f]*&lt;/p&gt;",
                delegate(Match match)
                {
                    string full = match.Groups[0].ToString();
                    full = WebUtility.HtmlDecode(full);
                    return full;
                }, RegexOptions.Singleline);
            return forReturn;
        }

        /// <summary>
        /// Radi skidanje CDATA sa Slides Tag-a
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixSlides(string forReturn)
        {
            forReturn = Regex.Replace(forReturn, "&lt;slides&gt;[ \t\r\n\v\f]*(.*?)[ \t\r\n\v\f]*&lt;/slides&gt;",
                delegate(Match match)
                {
                    string full = match.Groups[0].ToString();
                    full = WebUtility.HtmlDecode(full);
                    return full;
                }, RegexOptions.Singleline);
            return forReturn;
        }

        /// <summary>
        /// Radi skidanje CDATA sa YouTube Tag-a
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixVideo(string forReturn)
        {
            forReturn = Regex.Replace(forReturn,
                "&lt;p&gt;[ \t\r\n\v\f]*&lt;video[^<>]*>(.*?)&lt;/video&gt;[ \t\r\n\v\f]*&lt;/p&gt;",
                delegate(Match match)
                {
                    string full = match.Groups[0].ToString();
                    full = WebUtility.HtmlDecode(full);
                    return full;
                }, RegexOptions.Singleline);
            return forReturn;
        }

        /// <summary>
        /// Radi skidanje CDATA sa Audio Tag-a
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixAudio(string forReturn)
        {
            forReturn = Regex.Replace(forReturn,
                "&lt;p&gt;[ \t\r\n\v\f]*&lt;audio[^<>]*>(.*?)&lt;/audio&gt;[ \t\r\n\v\f]*&lt;/p&gt;",
                delegate(Match match)
                {
                    string full = match.Groups[0].ToString();
                    full = WebUtility.HtmlDecode(full);
                    return full;
                }, RegexOptions.Singleline);
            return forReturn;
        }

        /// <summary>
        /// Radi HTML Decode na Note tag
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixNote(string forReturn)
        {
            forReturn = Regex.Replace(forReturn,
                "<sectiondiv outputclass=\"note[^<>]*\">[ \t\r\n\v\f]*&lt;p(.*?)<\\/sectiondiv>", delegate(Match match)
                {
                    string v = match.ToString();
                    return WebUtility.HtmlDecode(v).Replace("&nbsp;", "");
                }, RegexOptions.Singleline);
            forReturn = forReturn.Replace("&not;", "¬");
            forReturn = forReturn.Replace("&times;", "×");
            forReturn = forReturn.Replace("&divide;", "÷");
            forReturn = forReturn.Replace("&plusmn;", "±");
            forReturn = forReturn.Replace("&micro;", "µ");
            forReturn = Util.ReplaceStringSpecialChars(forReturn);
            return forReturn;
        }

        /// <summary>
        /// Radi HTML encode paragrafa za Latex a CDATA encode za sadrzaja
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixLatex(string forReturn)
        {
            forReturn = Regex.Replace(forReturn,
                "<sectiondiv outputclass=\"latex\">[ \t\r\n\v\f]*&lt;p&gt;[ \t\r\n\v\f]*(.*?)&lt;\\/p&gt;[ \t\r\n\v\f]*<\\/sectiondiv>",
                delegate(Match match)
                {
                    string full = match.Groups[0].ToString();
                    string v = match.Groups[1].ToString();
                    return "<sectiondiv outputclass=\"latex\"><p>" + v.Replace("&nbsp;", "") + "</p></sectiondiv>";
                }, RegexOptions.Singleline);
            forReturn = forReturn.Replace("&lt;br/&gt;", "<br/>");
            forReturn = forReturn.Replace("&lt;br /&gt;", "<br/>");
            return forReturn;
        }

        /// <summary>
        /// HTML Encode Pre taga i CDATA encode sadrzaja koda
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixPre(string forReturn)
        {

            forReturn = forReturn.Replace("&lt;pre ", "<pre ");
            forReturn = forReturn.Replace("&lt;/pre&gt;", "</pre>");
            forReturn = Regex.Replace(forReturn, "<pre[^<>]*>(.*?)<\\/pre>", delegate(Match match)
            {
                string full = match.Groups[0].ToString();
                string v = match.Groups[1].ToString();
                if (v != "")
                {
                    full = WebUtility.HtmlDecode(full.Replace(v, ""));
                }
                full = full.Replace("\r\n", "").Remove(full.Length - 6);
                return full + Util.EscapeXml(Util.UnEscapeXml(v.Replace("&nbsp;", ""))) + "</pre>";
            }, RegexOptions.Singleline);

            return forReturn;
        }

        /// <summary>
        /// Metoda koja radi CDATA enkoding subtitle-a
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixSubtitleAndTags(string forReturn)
        {

            //forReturn = Regex.Replace(forReturn, "<sectiondiv outputclass=\"subtitle\">[ \t\r\n\v\f]*(.*?)<\\/sectiondiv>", delegate (Match match)
            //{
            //    string full = match.Groups[0].ToString();
            //    string v = match.Groups[1].ToString();
            //    return "<sectiondiv outputclass=\"subtitle\">" + Util.EscapeXml(Util.UnEscapeXml(v.Replace("&nbsp;", ""))) + "</sectiondiv>";
            //}, RegexOptions.Singleline);

            
            forReturn = forReturn.Replace("\"&gt;", "\">");
            forReturn = forReturn.Replace("&amp;lt;", "&lt;");
            forReturn = forReturn.Replace("&amp;gt;", "&gt;");
            forReturn = forReturn.Replace("&amp;amp;", "&amp;");

            forReturn =
                forReturn.Replace(
                    "xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"",
                    "");
            return forReturn;
        }

        public static string FixIrvasiMajmuni(string forReturn)
        {
            forReturn = Regex.Replace(forReturn, "&lt;p outputclass=\"aligncenter\">[ \t\r\n\v\f]*(.*?)&lt;\\/p&gt;",
                delegate(Match match)
                {
                    string v = match.ToString();
                    string a = HttpUtility.HtmlDecode(v).Replace("&nbsp;", " ").Replace("&middot;", "*");
                    return FixUnclosedOrOnlyOpenedHTML(a);
                }, RegexOptions.Singleline);

            forReturn = Regex.Replace(forReturn, "&lt;p outputclass=\"left\">[ \t\r\n\v\f]*(.*?)&lt;\\/p&gt;;",
                delegate(Match match)
                {
                    string v = match.ToString();
                    string a = HttpUtility.HtmlDecode(v).Replace("&nbsp;", " ").Replace("&middot;", "*");
                    return FixUnclosedOrOnlyOpenedHTML(a);
                }, RegexOptions.Singleline);

            forReturn = Regex.Replace(forReturn, "&lt;p outputclass=\"right\">[ \t\r\n\v\f]*(.*?)&lt;\\/p&gt;",
                delegate(Match match)
                {
                    string v = match.ToString();
                    string a = HttpUtility.HtmlDecode(v).Replace("&nbsp;", " ").Replace("&middot;", "*");
                    return FixUnclosedOrOnlyOpenedHTML(a);
                }, RegexOptions.Singleline);

            return forReturn;
        }

        public static string FixIntroSections(string forReturn)
        {

            forReturn = Regex.Replace(forReturn,
                "<\\/title>[ \t\r\n\v\f]*&lt;ul(.*?)&lt;\\/ul&gt;[ \t\r\n\v\f]*<\\/section>", delegate(Match match)
                {
                    string v = match.ToString();
                    string a = HttpUtility.HtmlDecode(v).Replace("&nbsp;", " ").Replace("&middot;", "*");
                    return a;
                }, RegexOptions.Singleline);

            return forReturn;
        }

        /// <summary>
        /// Radi HTML Decode za sadrzaj TextBox
        /// </summary>
        /// <param name="forReturn"></param>
        /// <returns></returns>
        public static string FixTextBoxes(string forReturn)
        {

            forReturn = Regex.Replace(forReturn,
                "<sectiondiv outputclass=\"f[0-9]*(.[0-9]*)?\">[ \t\r\n\v\f]*(.*?)<\\/sectiondiv>",
                delegate(Match match)
                {
                    string v = match.ToString();
                    if (!v.Contains("<pre") && !v.Contains("&lt;pre") && !v.Contains("outputclass=\"note"))
                    {
                        string a = HttpUtility.HtmlDecode(v).Replace("&nbsp;", " ").Replace("&middot;", "*");
                        return FixUnclosedOrOnlyOpenedHTML(a);
                    }
                    else
                    {
                        return v;
                    }
                }, RegexOptions.Singleline);

            forReturn = forReturn.Replace("&not;", "¬");
            forReturn = forReturn.Replace("&times;", "×");
            forReturn = forReturn.Replace("&divide;", "÷");
            forReturn = forReturn.Replace("&plusmn;", "±");
            forReturn = forReturn.Replace("&micro;", "µ");
            forReturn = forReturn.Replace("&quote;", "\"");
            forReturn = Regex.Replace(forReturn,
                "<(\\w+)(?:\\s+\\w+=\"\"[^\"\"]+(?:\"\"\\$[^\"\"]+\"[^\"\"]+)?\"\")*>\\s*</\\1>", string.Empty);
            forReturn = Regex.Replace(forReturn,
                @"<(\w*(term|b|i|u|keyword|phrase|foreignword|reservedword|font|ph|sub|sup|xref))\s*[^\/>]*><\/\1>",
                string.Empty);
            return forReturn;
        }

        static void RemoveEmptyNodes(HtmlNode containerNode, List<string> _notToRemove)
        {
            if (containerNode.Attributes.Count == 0 && !_notToRemove.Contains(containerNode.Name) &&
                (containerNode.InnerText == null || containerNode.InnerText == string.Empty))
            {
                containerNode.Remove();
            }
            else
            {
                for (int i = containerNode.ChildNodes.Count - 1; i >= 0; i--)
                {
                    RemoveEmptyNodes(containerNode.ChildNodes[i], _notToRemove);
                }
            }

        }

        public static string FixUnclosedOrOnlyOpenedHTML(string text)
        {
            try
            {
                List<string> _notToRemove = new List<string>();
                _notToRemove.Add("br");
                _notToRemove.Add("m:math");
                _notToRemove.Add("m:mi");
                _notToRemove.Add("m:mo");
                _notToRemove.Add("m:sub");
                HtmlDocument htmlDoc = new HtmlDocument();
                htmlDoc.OptionFixNestedTags = true;
                htmlDoc.OptionOutputAsXml = true;
                htmlDoc.OptionWriteEmptyNodes = false;
                htmlDoc.LoadHtml(text);
                RemoveEmptyNodes(htmlDoc.DocumentNode, _notToRemove);
                string textFinal =
                    htmlDoc.DocumentNode.OuterHtml.Replace("<?xml version=\"1.0\" encoding=\"iso-8859-1\"?>", "")
                        .Replace("</span>", "")
                        .Replace("<span>", "")
                        .Replace("<?xml version=\"1.0\" encoding=\"iso-8859-2\"?>", "")
                        .Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
                Regex rgx = new Regex("<\\?xml.*?\\?>");
                textFinal = rgx.Replace(textFinal, "");
                textFinal = textFinal.Replace("</_m3a_", "</m:");
                textFinal = textFinal.Replace("<_m3a_", "<m:");
                textFinal = textFinal.Replace("_xmlns3a_m=", "xmlns:m=");
                return textFinal;
            }
            catch
            {
                return "";
            }
        }

        public static string FixXrefTags(string text)
        {
            return Regex.Replace(text, "<xref [^>]*>(.*?)</xref>",
                m =>
                {
                    var codeString = m.Groups[1].Value;
                    Console.WriteLine(codeString);
                    var list = new List<string>();
                    var str = Regex.Replace(codeString, "<[^>]*>", m1 =>
                    {
                        var bracketString = m1.Value;
                        list.Add(bracketString);
                        return "";
                    });
                    var result = m.Groups[0].Value;
                    if (list.Count > 0)
                    {
                        result = result.Replace(codeString, str);
                        foreach (var s in list)
                        {
                            result += s;
                        }
                    }
                    return result;
                });
        }
    }
}
