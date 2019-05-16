using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Drawing.Imaging;
using System.Security.Principal;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata
{
    class Util
    {
        public static bool IsOfficeInstalled()
        {
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Winword.exe");
            if (key != null)
            {
                key.Close();
            }
            return key != null;
        }
        public static bool checkIfHasInternetConnection()
        {
            try
            {
                var myPing = new Ping();
                var host = "google.com";
                var buffer = new byte[32];
                var timeout = 1000;
                var pingOptions = new PingOptions();
                var reply = myPing.Send(host, timeout, buffer, pingOptions);
                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool HasBinaryContent(string content)
        {


            return content.Any(ch => char.IsControl(ch) && ch != '\r' && ch != '\n' && ch != '	');
        }
        /// <summary>
        /// Vraca HTML za prikaz LaTeX koda na osnovu prosleđenog LaTeX-a
        /// </summary>
        /// <param name="latex"></param>
        /// <returns></returns>
        public static string LatexWebForCode(string latex)
        {
            var uri = new Uri(Path.GetFullPath("style.css"));
            var converted = uri.AbsoluteUri;
            var doc = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<!DOCTYPE html\r\n  PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xml:lang=\"en-us\" lang=\"en-us\">\r\n<head><link rel='stylesheet' href='\" + converted + \"' type='text/css'/>\r\n<script type=\"text/x-mathjax-config\">\r\n  MathJax.Hub.Config({ \r\n   TeX: {\r\n    Macros: {\r\n      RR: '{\\\\bf R}',  tg: \"\\\\operatorname{tg}\", ctg: \"\\\\operatorname{ctg}\", arctg: \"\\\\operatorname{arctg}\", arcctg: \"\\\\operatorname{arcctg}\",     \r\n      bold: ['\\\\boldsymbol{#1}',1],\r\n\t  gbreak: '\\\\mmlToken{mo}[linebreak=\"goodbreak\"]{}'\r\n    }\r\n  },\r\n  tex2jax: {inlineMath: [['$','$'], ['\\\\(','\\\\)']]},\"HTML-CSS\": {\r\n\t\tpreferredFont: \"STIX\",\r\n\t\tscale: 90\r\n, linebreaks: { automatic: true },showMathMenu: false} });\r\n</script>\r\n<script type=\"text/javascript\" async\r\n  src=\"https://cdn.mathjax.org/mathjax/latest/MathJax.js?config=TeX-AMS-MML_HTMLorMML\">\r\n</script>\r\n</head>\r\n<body style='padding:0;margin:0;font-size: 12px !important; font-family: \"Open Sans\",sans-serif;'>\r\n\t<p>";
            doc += latex;
            doc += "\r\n\t\r\n\t</p>\r\n</body>\r\n</html>\r\n";
            return doc;
        }

        public static List<string> wordsToFix = new List<string>();

        public static string ReplaceStringSpecialChars(string forReturn)
        {
            if (wordsToFix.Count == 0)
            {
                var words = new List<string>();
                var line = "";
                var file = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\" + "Words.txt");
                while ((line = file.ReadLine()) != null)
                {
                    words.Add(line);
                }
                file.Close();
                wordsToFix = words;

            }
            var newString = forReturn;
            for (var i = 0; i < wordsToFix.Count; i += 2)
            {
                newString = newString.Replace(wordsToFix[i + 1], wordsToFix[i]);
            }

            return newString;
        }
        public static String FixStringForPath(String path)
        {
            var regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            var r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            var pathnew = r.Replace(path, "");
            return pathnew;
        }
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            var codecs = ImageCodecInfo.GetImageDecoders();

            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        /// <summary>
        /// Metoda koja skalira sliku na najbolju mogucu velicinu u acpect ratio 
        /// za prosledjenu sirinu i visinu
        /// </summary>
        /// <param name="image">Slika koja treba da se skalira</param>
        /// <param name="maxWidth">Maksimalna sirina</param>
        /// <param name="maxHeight">Maksimalna visina</param>
        /// <returns>Skalirana slika</returns>
        public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        /// <summary>
        /// Proverava da li user pripada grupi administratora racunara
        /// </summary>
        /// <returns></returns>
        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            WindowsIdentity user = null;
            try
            {
                //get the currently logged in user
                user = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch
            {
                isAdmin = false;
            }
            finally
            {
                if (user != null)
                    user.Dispose();
            }
            return isAdmin;
        }


        private static float ratio = 16f / 9f;

        public static Point ScaleVideo(int maxWidth, int maxHeight)
        {
            var h = maxWidth / ratio;
            if (h > maxHeight)
            {
                h = maxHeight;
            }
            var w = h * ratio;

            return new Point((int)w, (int)h);

            
        }

    

        /// <summary>
        /// Generiše OpenFileDialog za Dita fajlove sa MultiSelektom
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog OpenDitaFiles()
        {
            var OpenFiles = new OpenFileDialog();
            OpenFiles.Filter = "Dita Files|*.dita";
            OpenFiles.Multiselect = true;
            return OpenFiles;
        }

        /// <summary>
        /// Generiše OpenFileDialog za Dita fajlove
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog OpenDitaFile()
        {
            var OpenFiles = new OpenFileDialog();
            OpenFiles.Filter = "Dita Files|*.dita";
            return OpenFiles;
        }

        /// <summary>
        /// Generiše OpenFileDialog za podrzave Video fajlove
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog OpenVideoFiles()
        {
            var OpenFiles = new OpenFileDialog();
            OpenFiles.Filter = "MP4|*.mp4|OGG|*.ogg|WebM|*.webm";
            return OpenFiles;
        }
        /// <summary>
        /// Generiše OpenFileDialog za podrzave Video fajlove
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog OpenAudioFiles()
        {
            var OpenFiles = new OpenFileDialog();
            OpenFiles.Filter = "MP3|*.mp3|OGG|*.ogg|WAV|*.wav";
            return OpenFiles;
        }
        /// <summary>
        /// Funkcija koja parsira cdata sadrzaj iz xml dokumenta u odgovarajuci html sadrzaj.
        /// </summary>
        /// <param name="decodeHtml"></param>
        /// <returns></returns>
        public static string DecodeCdata(string decodeHtml)
        {
            var xmlData = XElement.Parse("<root><sectiondiv>" + decodeHtml + "</sectiondiv></root>");
            var nav = xmlData.CreateNavigator();
            var descriptionNode =
                nav.SelectSingleNode("/sectiondiv");
            var desiredValue =
                Regex.Replace(descriptionNode.Value
                                         .Replace(Environment.NewLine, string.Empty)
                                         .Trim(), @"\s+", " ");
            return desiredValue;
        }

        public static string EscapeXml(string s)
        {
            return s.Replace("&", "&amp;").Replace(">", "&gt;").Replace("<", "&lt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }

        public static string UnEscapeXml(string s)
        {
            return s.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&apos;", "'");
        }

        /// <summary>
        /// Funkcija serijalizuje xml dokument koji se prosledjuje kao string u odgovarajucu
        /// hijerahiju objekata.
        /// </summary>
        /// <param name="Xml"></param>
        /// <param name="ObjType"></param>
        /// <returns></returns>
        public static object FromXml(string Xml, Type ObjType)
        {
            // Console.WriteLine(Xml);
            var newXml = Xml;
            //Fix zeznuti naslovi

            try
            {
                newXml = Regex.Replace(Xml, "<title>[ \t\r\n\v\f]*(.*?)<\\/title>", delegate (Match match)
                {
                    var v = match.Groups[1].ToString();
                    var vb = Regex.Replace(v, @"<[^>]*>", String.Empty);
                    var next = "<title>" + vb + "</title>";

                    return next;
                }, RegexOptions.Singleline);
            }
            catch { }
            XmlSerializer ser;
            ser = new XmlSerializer(ObjType);

            //Console.WriteLine(newXml);


            StringReader stringReader;
            stringReader = new StringReader(newXml);
            XmlTextReader xmlReader;
            var xmlReaderSettings = new XmlReaderSettings { IgnoreComments = true };
            xmlReader = new XmlTextReader(stringReader);
            xmlReader.XmlResolver = null;
            var xml = xmlReader.ReadInnerXml();
            object obj;
            obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();

            if (obj is LearningBase)
            {
                var baseObj = (LearningBase)obj;
                FixLatexOnLearningBase(baseObj);
            }
            return obj;
        }

        public static void FixLatexOnLearningBase(LearningBase baseObj)
        {
            foreach (var sec in baseObj.LearningBody.Section)
            {
                foreach (var divSekSek2 in sec.Sectiondiv.ToArray())
                {
                    foreach (var divSekSek3 in divSekSek2.SectionDiv.ToArray())

                        foreach (var divSekSek4 in divSekSek3.SectionDiv.ToArray())
                        {
                            if (divSekSek4.Outputclass == "latex")
                            {
                                divSekSek4.Content = UnEscapeXml(divSekSek4.Content);
                            }
                        }
                }
            }
        }

        /// <summary>
        /// Funkcija proverava da li je xml sectiondiv element poslednji (najdublji) u xml strukturi sa tim
        /// nazivom. Vraca odgovarajucu bool vrednost.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool isLastLevel(XElement element)
        {
            var childList = element.Descendants("sectiondiv");
            return !childList.Any();
        }

        /// <summary>
        /// Funkcija uklanja sectiondiv tag i vraca samo sadrzaj prosledjenog sectiondiv elementa.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string RemoveSectionDivTag(string str)
        {
            str = str.Substring(str.IndexOf(">") + 1);
            if (str.Length > 0)
            {
                str = str.Substring(0, str.Length - 13);
            }
            return str;
        }

        /// <summary>
        /// Metoda koja kopira sve iz jednog direktorijuma u drugi
        /// Korisceno za Resources folder
        /// </summary>
        /// <param name="SourcePath"></param>
        /// <param name="DestinationPath"></param>
        public static void CopyFromDirToDir(string SourcePath, string DestinationPath)
        {
            foreach (var dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
            }

            foreach (var newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
            {
                try
                {
                    File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath), true);
                }
                catch (IOException ex) { MessageBox.Show(ex.Message); }
            }
        }

        public static string PictureNameForWithoutDITA(string name)
        {
            var myString = name;
            var newString = myString.Substring(myString.IndexOf('-') + 1);

            return newString;
        }

        /// <summary>
        /// Vraca sadrzaj fajla po prosledjenom path-u
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetFileContent(string path)
        {
            return File.ReadAllText(path);
        }

        /// <summary>
        /// Brise sekcije koje predstavljaju liste objekata
        /// </summary>
        /// <param name="overview"></param>
        public static void DeleteArrowSections(LearningOverview overview)
        {
            var sectionsToDelete = overview.LearningOverviewbody.Section.Where(x => (x.Id != null && x.Id != "S-UVOD")).ToList();
            foreach (var sec in sectionsToDelete)
            {
                overview.LearningOverviewbody.Section.Remove(sec);
            }
        }

        ///// <summary>
        ///// Serializuje objekat i sredjuje tagove
        ///// </summary>
        ///// <param name="item"></param>
        ///// <returns></returns>
        //public static string SerializeWithEncoding(object item)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    XmlSerializer serializer = new XmlSerializer(item.GetType());
        //    using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true }))
        //    {
        //        checkAndDeleteWhatIsNeccecery(item as LearningBase);
        //        serializer.Serialize(writer, item);
        //        sb = DITAXmlFixer.FixEncoding(sb, item);
        //        string forReturn = sb.ToString();
        //        forReturn = DITAXmlFixer.FixFigure(forReturn);
        //        forReturn = DITAXmlFixer.FixTextBoxes(forReturn);
        //        forReturn = DITAXmlFixer.FixNote(forReturn);
        //        forReturn = DITAXmlFixer.FixLatex(forReturn);
        //        forReturn = DITAXmlFixer.FixSubtitleAndTags(forReturn);
        //        forReturn = DITAXmlFixer.FixPre(forReturn);
        //        forReturn = DITAXmlFixer.FixYouTube(forReturn);
        //        forReturn = DITAXmlFixer.FixVideo(forReturn);
        //        forReturn = DITAXmlFixer.FixAudio(forReturn);
        //        forReturn = DITAXmlFixer.FixSlides(forReturn);
        //        forReturn = DITAXmlFixer.FixIrvasiMajmuni(forReturn);
        //        forReturn = DITAXmlFixer.FixIntroSections(forReturn);
        //        forReturn = forReturn.Replace("<d4p_mathml>", "<d4p_MathML>");
        //        forReturn = forReturn.Replace("</d4p_mathml>", "</d4p_MathML>");
        //        return forReturn;
        //    }
        //}
        /// <summary>
        /// Validacija svih ne potrebnih komponenti pri Save-u da ne bude bagova pri Load-u
        /// </summary>
        /// <param name="baseC"></param>
        public static void checkAndDeleteWhatIsNeccecery(LearningBase baseC)
        {
            foreach (var columnB in baseC.LearningBody.Section)
            {
                foreach (var column in columnB.Sectiondiv)
                {
                    foreach (var columns in column.SectionDiv)
                        foreach (var divSekSek in columns.SectionDiv.ToArray())
                        {
                            // Brise glupe irvasove Sectiondivove koji su potpuno prazni
                            if (divSekSek.Content == null && divSekSek.SectionDiv.Count == 0)
                            {
                                columns.SectionDiv.Remove(divSekSek);
                            }

                            if (divSekSek.Outputclass == "latex" || divSekSek.Outputclass == "youtube" || divSekSek.Outputclass == "video" || divSekSek.Outputclass == "audio" || divSekSek.Outputclass == "flexslider")
                            {

                            }
                            else if (divSekSek.Outputclass.Substring(0, 1) == "v" &&
                                     (divSekSek.Content == "" || divSekSek.Content == null) && divSekSek.SectionDiv.Count == 0)
                            {
                                columns.SectionDiv.Remove(divSekSek);
                            }
                            else if (divSekSek.SectionDiv.Count > 0 &&
                                     divSekSek.SectionDiv[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDiv[0].SectionDiv.Count == 1 &&
                                     divSekSek.SectionDiv[0].SectionDiv[0].Outputclass != null &&
                                     divSekSek.SectionDiv[0].SectionDiv[0].Outputclass.Contains("note"))
                            {
                            }
                            else if (divSekSek.SectionDiv.Count > 0 &&
                                     divSekSek.SectionDiv[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDiv[0].Content != null &&
                                     divSekSek.SectionDiv[0].Content.Contains("d4p_eqn_inline"))
                            {
                            }
                            else if (divSekSek.SectionDiv.Count > 0 &&
                                     divSekSek.SectionDiv[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDiv[0].SectionDiv.Count == 0 &&
                                     !divSekSek.SectionDiv[0].Content.Contains("<pre"))
                            {
                            }
                            else if (divSekSek.SectionDiv.Count > 0 &&
                                     divSekSek.SectionDiv[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDiv[0].SectionDiv.Count == 1 &&
                                     !divSekSek.SectionDiv[0].Content.Contains("<pre"))
                            {
                                divSekSek.SectionDiv[0].SectionDiv.RemoveAt(0);
                                if (divSekSek.Content == null || divSekSek.Content == "")
                                {
                                    divSekSek.Content = "<p></p>";
                                }
                            }
                            else if (divSekSek.SectionDiv.Count > 0 &&
                                     divSekSek.SectionDiv[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDiv[0].SectionDiv.Count == 0 &&
                                     divSekSek.SectionDiv[0].Content.Contains("<pre"))
                            {

                            }
                            else if (divSekSek.Content != null && divSekSek.Content.Contains("<fig>"))
                            {
                            }
                            else
                            {
                                columns.SectionDiv.Remove(divSekSek);
                            }
                        }
                }
            }
        }

        /// <summary>
        /// Proverava da li fajl postoji u resource folderu bez obzira na ekstenziju
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //public static bool CheckIfFileExistsWithAnyExtensionInResourcesFolder(string fileName)
        //{
        //    DirectoryInfo root = new DirectoryInfo(ProjectSingleton.Project.ResourcesDir);
        //    FileInfo[] listfiles = root.GetFiles(fileName + ".*");
        //    return listfiles.Length > 0;
        //}

        /// <summary>
        /// Pravi kopiju slike u memoriji
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Image GetCopyImage(string path)
        {
            using (var im = Image.FromFile(path))
            {
                var bm = new Bitmap(im);
                return bm;
            }
        }
        //public static Image GetCopyImage(Image path)
        //{
        //    if (path != null)
        //    {
        //        Bitmap bm = new Bitmap(path);
        //        return bm;
        //    }
        //    else
        //    {
        //        return Properties.Resources.imagenotfound;
        //    }
        //}

        /// <summary>
        /// Pretvara ICollection kolekciju u listu 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="other"></param>
        /// <returns></returns>
        public static List<T> CollectionToList<T>(System.Collections.ICollection other)
        {
            var output = new List<T>(other.Count);

            output.AddRange(other.Cast<T>());
            return output;
        }

        /// <summary>
        /// Daje ispravnu poruku za DraftComment combo box
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetFormatedDraftComment(string s)
        {
            var diff = s;
            if (diff != "")
            {
                diff = char.ToUpper(diff[0]) + diff.ToLower().Substring(1);
                diff = (diff == "Elementarni") ? "Osnovni" : diff;

            }
            return diff;
        }
        /// <summary>
        /// Radi dispose objekata u panelu. Clear metoda poziva Clear komponente
        /// po redu pri cemu se prozor rekreira vise puta i automatski
        /// se vezuje za Windows Handler pa komponenta ne moze biti izbrisana iz
        /// memorije. Kako bi se ovo resilo treba zvati Dispose metodu nad listom 
        /// komponenti u Panelu od zadnje do prve komponente. Kako se lista nebi
        /// automatski menjala i kako se ne bi zvao Windows Handler.
        /// </summary>
        /// <param name="panelControler"></param>
        public static void DisposePanelControls(Panel panelControler)
        {
            for (var ix = panelControler.Controls.Count - 1; ix >= 0; --ix)
            {
                var ctl = panelControler.Controls[ix];
                if (ctl != panelControler) ctl.Dispose();
            }
        }

        /// <summary>
        /// Metoda koja vraca listu naziva svih slika u okviru objekta koji se prosledjuje kao xml string.
        /// </summary>
        /// <param name="ditaXml"></param>
        /// <returns></returns>
        public static List<string> GetImageNamesFromObjectXml(string ditaXml)
        {
            var result = new List<string>();
            var doc = XDocument.Parse(ditaXml);
            foreach (var img in doc.Descendants("image"))
            {
                result.Add(img.Attribute("href").Value);
            }
            return result;
        }

        /// <summary>
        /// Metoda koja vraca listu naziva svih slika u okviru objekta (Learning Content-a) koji se nalazi na serveru i koji se prosledjuje kao argument.
        /// </summary>
        /// <param name="lc"></param>
        /// <returns></returns>
        public static List<string> GetImagesFromObjectOnImport(LearningContent lc)
        {
            List<string> result;
            var xml = "";
            using (var wc = new WebClient())
            {
                xml = wc.DownloadString(lc.url);
            }
            result = GetImageNamesFromObjectXml(xml);
            return result;
        }


        public static string GetLearningContentIdForLesson(int i)
        {
            return string.Format("LC-{0:D2}", i);
        }


    }
}
