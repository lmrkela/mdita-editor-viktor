using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;
using mDitaEditor.Dita;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Project;
using Microsoft.Win32;
using System.Diagnostics;
using System.IO.Compression;

namespace mDitaEditor.Utils
{
    class Util
    {
        public static bool IsOfficeInstalled()
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\Winword.exe");
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
                Ping myPing = new Ping();
                string host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
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
            var scriptLocation = "https://cdn.mathjax.org/mathjax/2.6-latest/MathJax.js";
            var mathJaxLocation = Path.Combine(Program.ProgramPath, "mathjax", "MathJax.js");
         
            if (File.Exists(mathJaxLocation))
            {
                Debug.WriteLine("Mathjax Loaded");   
                var uri = new Uri(mathJaxLocation);
                scriptLocation = uri.AbsoluteUri;               
            }

           

            string doc = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                         "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                         "<html xml:lang=\"en-us\" lang=\"en-us\">" +
                         "<head>" +
                         "<link rel='stylesheet' href='\" + converted + \"' type='text/css'/>" +
                         "<script type=\"text/x-mathjax-config\">MathJax.Hub.Config({ TeX: { Macros: { RR: '{\\\\bf R}', tg: \"\\\\operatorname{tg}\", ctg: \"\\\\operatorname{ctg}\", arctg: \"\\\\operatorname{arctg}\", arcctg: \"\\\\operatorname{arcctg}\", bold: ['\\\\boldsymbol{#1}',1],\t gbreak: '\\\\mmlToken{mo}[linebreak=\"goodbreak\"]{}' } }, tex2jax: {inlineMath: [['$','$'], ['\\\\(','\\\\)']]},\"HTML-CSS\": {\t\tpreferredFont: \"STIX\",\t\tscale: 90, linebreaks: { automatic: true },showMathMenu: false} });</script>" +
                         $"<script type=\"text/javascript\" async src=\"{scriptLocation}?config=TeX-AMS-MML_HTMLorMML\"></script>" +
                         "</head>" +
                         "<body style='padding:0;margin:0;font-size: 12px !important; font-family: \"Open Sans\",sans-serif;'>" +
                         $"<p>{latex}</p>" +
                         $"</body>" +
                         $"</html>";
            return doc;
        }

        public static List<string> wordsToFix = new List<string>();

        public static string ReplaceStringSpecialChars(string forReturn)
        {
            if (wordsToFix.Count == 0)
            {
                List<string> words = new List<string>();
                string line = "";
                System.IO.StreamReader file = new System.IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\" + "Words.txt");
                while ((line = file.ReadLine()) != null)
                {
                    words.Add(line);
                }
                file.Close();
                wordsToFix = words;

            }
            string newString = forReturn;
            for (int i = 0; i < wordsToFix.Count; i += 2)
            {
                newString = newString.Replace(wordsToFix[i + 1], wordsToFix[i]);
            }

            return newString;
        }
        public static string FixStringForPath(string path)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
            string pathnew = r.Replace(path, "");
            return pathnew;
        }
        public static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
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
                WindowsPrincipal principal = new WindowsPrincipal(user);
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
            float h = maxWidth / ratio;
            if (h > maxHeight)
            {
                h = maxHeight;
            }
            float w = h * ratio;

            return new Point((int)w, (int)h);
        }

        public static bool CheckText(SelectableFlowPanel pan)
        {
            return pan.HeightLeftPanel() >= 0;
        }

        public static int CheckPanelOverlap(SelectableFlowPanel panel)
        {
            int limit = panel.Controls[panel.Controls.Count - 1].Location.Y + panel.Controls[panel.Controls.Count - 1].Height;
            int toLower = limit - panel.Height;
            return toLower;
        }

        /// <summary>
        /// Generiše OpenFileDialog za Dita fajlove sa MultiSelektom
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog OpenDitaFiles()
        {
            OpenFileDialog OpenFiles = new OpenFileDialog();
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
            OpenFileDialog OpenFiles = new OpenFileDialog();
            OpenFiles.Filter = "Dita Files|*.dita";
            return OpenFiles;
        }

        /// <summary>
        /// Generiše OpenFileDialog za podrzave Video fajlove
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog OpenVideoFiles()
        {
            OpenFileDialog OpenFiles = new OpenFileDialog();
            OpenFiles.Filter = "MP4|*.mp4|OGG|*.ogg|WebM|*.webm";
            return OpenFiles;
        }
        /// <summary>
        /// Generiše OpenFileDialog za podrzave Video fajlove
        /// </summary>
        /// <returns></returns>
        public static OpenFileDialog OpenAudioFiles()
        {
            OpenFileDialog OpenFiles = new OpenFileDialog();
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
            XElement xmlData = XElement.Parse("<root><sectiondiv>" + decodeHtml + "</sectiondiv></root>");
            XPathNavigator nav = xmlData.CreateNavigator();
            XPathNavigator descriptionNode =
                nav.SelectSingleNode("/sectiondiv");
            string desiredValue =
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
            if (s.Contains("&amp;amp;"))
            {
                return s.Replace(";amp;", ";").Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&apos;", "'");
            }
            else
            {
                return s.Replace("&amp;", "&").Replace("&gt;", ">").Replace("&lt;", "<").Replace("&quot;", "\"").Replace("&apos;", "'");
            }
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
            string newXml = Xml;
            //Fix zeznuti naslovi

            try
            {
                newXml = Regex.Replace(Xml, "<title>[ \t\r\n\v\f]*(.*?)<\\/title>", delegate (Match match)
                {
                    string v = match.Groups[1].ToString();
                    string vb = Regex.Replace(v, @"<[^>]*>", string.Empty);
                    string next = "<title>" + vb + "</title>";

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
            string xml = xmlReader.ReadInnerXml();
            object obj;
            obj = ser.Deserialize(xmlReader);
            xmlReader.Close();
            stringReader.Close();

            if (obj is LearningBase)
            {
                LearningBase baseObj = (LearningBase)obj;
                FixLatexOnLearningBase(baseObj);
            }
            return obj;
        }

        public static void FixLatexOnLearningBase(LearningBase baseObj)
        {
            foreach (Section sec in baseObj.LearningBody.Sections)
            {
                foreach (Sectiondiv divSekSek2 in sec.SectionDivs.ToArray())
                {
                    foreach (Sectiondiv divSekSek3 in divSekSek2.SectionDivs.ToArray())

                        foreach (Sectiondiv divSekSek4 in divSekSek3.SectionDivs.ToArray())
                        {
                            if (divSekSek4.Outputclass == "latex")
                            {
                                divSekSek4.Content = Util.UnEscapeXml(divSekSek4.Content);
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
            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
            {
                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));
            }

            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
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
            string myString = name;
            string newString = myString.Substring(myString.IndexOf('-') + 1);

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
            List<Section> sectionsToDelete = overview.LearningOverviewbody.Sections.Where(x => (x.Id != null && x.Id != "S-UVOD")).ToList();
            foreach (Section sec in sectionsToDelete)
            {
                overview.LearningOverviewbody.Sections.Remove(sec);
            }
        }

        /// <summary>
        /// Serializuje objekat i sredjuje tagove
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string SerializeWithEncoding(object item)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(item.GetType());
            using (XmlWriter writer = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true }))
            {
                checkAndDeleteWhatIsNeccecery(item as LearningBase);
                serializer.Serialize(writer, item);
                sb = DitaXmlFixer.FixEncoding(sb, item);
                string forReturn = sb.ToString();

        
             
                    forReturn = DitaXmlFixer.FixFigure(forReturn); //Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixTextBoxes(forReturn);// Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixNote(forReturn); //Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixLatex(forReturn);// Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixSubtitleAndTags(forReturn);// Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixPre(forReturn);// Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixYouTube(forReturn); //Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixVideo(forReturn); //Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixAudio(forReturn); //Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixSlides(forReturn);// Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixIrvasiMajmuni(forReturn); ///Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixIntroSections(forReturn);/// Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                    forReturn = DitaXmlFixer.FixXrefTags(forReturn); //Debug.WriteLine("1==========="); Debug.WriteLine(forReturn.Substring(0, 1000));
                
                forReturn = forReturn.Replace("<d4p_mathml>", "<d4p_MathML>");
                forReturn = forReturn.Replace("</d4p_mathml>", "</d4p_MathML>");
                return forReturn;
            }
        }
        /// <summary>
        /// Validacija svih ne potrebnih komponenti pri Save-u da ne bude bagova pri Load-u
        /// </summary>
        /// <param name="baseC"></param>
        public static void checkAndDeleteWhatIsNeccecery(LearningBase baseC)
        {
            foreach (Section columnB in baseC.LearningBody.Sections)
            {
                foreach (Sectiondiv column in columnB.SectionDivs)
                {
                    foreach (Sectiondiv columns in column.SectionDivs)
                        foreach (Sectiondiv divSekSek in columns.SectionDivs.ToArray())
                        {

                          

                            // Brise glupe irvasove Sectiondivove koji su potpuno prazni
                            if (divSekSek.Content == null && divSekSek.SectionDivs.Count == 0)
                            {
                                columns.SectionDivs.Remove(divSekSek);
                            }

                    

                            if (divSekSek.Outputclass == "latex" || divSekSek.Outputclass == "youtube" || divSekSek.Outputclass == "video" || divSekSek.Outputclass == "audio" || divSekSek.Outputclass == "flexslider")
                            {

                            }
                            else if (divSekSek.Outputclass.Substring(0, 1) == "v" &&
                                     (divSekSek.Content == "" || divSekSek.Content == null) && divSekSek.SectionDivs.Count == 0)
                            {
                                columns.SectionDivs.Remove(divSekSek);
                            }
                            else if (divSekSek.SectionDivs.Count > 0 &&
                                     divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDivs[0].SectionDivs.Count == 1 &&
                                     divSekSek.SectionDivs[0].SectionDivs[0].Outputclass != null &&
                                     divSekSek.SectionDivs[0].SectionDivs[0].Outputclass.Contains("note"))
                            {
                            }
                            else if (divSekSek.SectionDivs.Count > 0 &&
                                     divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDivs[0].Content != null &&
                                     divSekSek.SectionDivs[0].Content.Contains("d4p_eqn_inline"))
                            {
                            }
                            else if (divSekSek.SectionDivs.Count > 0 &&
                                     divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDivs[0].SectionDivs.Count == 0 &&
                                     !divSekSek.SectionDivs[0].Content.Contains("<pre"))
                            {
                            }
                            else if (divSekSek.SectionDivs.Count > 0 &&
                                     divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDivs[0].SectionDivs.Count == 1 &&
                                     !divSekSek.SectionDivs[0].Content.Contains("<pre"))
                            {
                                divSekSek.SectionDivs[0].SectionDivs.RemoveAt(0);
                                if (divSekSek.Content == null || divSekSek.Content == "")
                                {
                                    divSekSek.Content = "<p></p>";
                                }
                            }
                            else if (divSekSek.SectionDivs.Count > 0 &&
                                     divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                                     divSekSek.SectionDivs[0].SectionDivs.Count == 0 &&
                                     divSekSek.SectionDivs[0].Content.Contains("<pre"))
                            {

                            }
                            else if (divSekSek.Content != null && divSekSek.Content.Contains("<fig>"))
                            {
                            }
                            else
                            {
                                columns.SectionDivs.Remove(divSekSek);
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
        public static bool CheckIfFileExistsWithAnyExtensionInResourcesFolder(string fileName)
        {
            DirectoryInfo root = new DirectoryInfo(ProjectSingleton.Project.ResourcesDir);
            FileInfo[] listfiles = root.GetFiles(fileName + ".*");
            return listfiles.Length > 0;
        }

        /// <summary>
        /// Pravi kopiju slike u memoriji
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Image GetCopyImage(string path)
        {
            using (Image im = Image.FromFile(path))
            {
                Bitmap bm = new Bitmap(im);
                return bm;
            }
        }
        public static Image GetCopyImage(Image path)
        {
            if (path != null)
            {
                Bitmap bm = new Bitmap(path);
                return bm;
            }
            else
            {
                return Properties.Resources.imagenotfound;
            }
        }

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
            string diff = s;
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
            for (int ix = panelControler.Controls.Count - 1; ix >= 0; --ix)
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
            List<string> result = new List<string>();
            XDocument doc = XDocument.Parse(ditaXml);
            foreach (XElement img in doc.Descendants("image"))
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
            string xml = "";
            using (WebClient wc = new WebClient())
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


        public static void AddFileToZipWithReplace(string zipPath, string filePath)
        {
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                var fileName = Path.GetFileName(filePath);
                var entry = archive.GetEntry(fileName);
                if (entry != null)
                {
                    entry.Delete();
                }
                archive.CreateEntryFromFile(filePath, fileName);                
            }
        }


        public static void AddFolderToZip(string zipPath, string folderPath, string folderName, bool recursive = false)
        {
            Debug.WriteLine(zipPath);
            Debug.WriteLine(folderPath);
                       
                       

            Debug.WriteLine(folderName);

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
            {
                ZipArchiveEntry readmeEntry;
                DirectoryInfo d = new DirectoryInfo(folderPath);
                FileInfo[] Files = d.GetFiles("*");
                foreach (FileInfo file in Files)
                {
                    readmeEntry = archive.CreateEntryFromFile(folderPath + "\\" + file.Name, folderName + "/" + file.Name);
                }
            }

            if (recursive)
            {
                DirectoryInfo dir = new DirectoryInfo(folderPath);
                DirectoryInfo[] dirs = dir.GetDirectories();
                foreach (DirectoryInfo subdir in dirs)
                {
                   
                    // Create the subdirectory.
                    string temppath = Path.Combine(folderPath, subdir.Name);

                    // Copy the subdirectories.
                    AddFolderToZip(zipPath, temppath, folderName +"/"+ subdir.Name, true);
                    
                }

            }


        }

        public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, List<string> excludeFolders)
        {
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            // If the source directory does not exist, throw an exception.
            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory does not exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }


            // Get the file contents of the directory to copy.
            FileInfo[] files = dir.GetFiles();

            foreach (FileInfo file in files)
            {
                // Create the path to the new copy of the file.
                string temppath = Path.Combine(destDirName, file.Name);

                // Copy the file.
                file.CopyTo(temppath, false);
            }

            // If copySubDirs is true, copy the subdirectories.
            if (copySubDirs)
            {

                foreach (DirectoryInfo subdir in dirs)
                {
                    if (!excludeFolders.Contains(subdir.Name))
                    {
                        // Create the subdirectory.
                        string temppath = Path.Combine(destDirName, subdir.Name);

                        // Copy the subdirectories.
                        DirectoryCopy(subdir.FullName, temppath, copySubDirs,excludeFolders);
                    }
                }
            }
        }

        public static void createDir(string dir)
        {            
            if (Directory.Exists(dir))
            {
                FileManager.DeleteDir(dir, true);
            }
            Directory.CreateDirectory(dir);
        }
    


}
}
