using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using mDitaEditor.Dita;
using mDitaEditor.Lams;
using mDitaEditor.Utils;
using System.Diagnostics;

namespace mDitaEditor.Project
{
    public partial class ProjectFile
    {
        /// <summary>
        /// Provera da li je Sifra predmeta validna
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        private static bool IsValidSubject(string subject)
        {
            if (subject.Length != 5)
            {
                return false;
            }
            return char.IsLetter(subject[0]) && char.IsLetter(subject[1]) && char.IsNumber(subject[2]) && char.IsNumber(subject[3]) && char.IsNumber(subject[4]);
        }

        /// <summary>
        /// Provera da li je lekcija validna
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        private static bool IsValidLesson(string lesson)
        {
            if (lesson.Length != 3)
            {
                return false;
            }
            return char.IsLetter(lesson[0]) && char.IsNumber(lesson[1]) && char.IsNumber(lesson[2]);
        }

        /// <summary>
        /// Metoda koja proverava da li je String numeric
        /// </summary>
        /// <param name="Expression"></param>
        /// <returns></returns>
        private static bool IsNumeric(object Expression)
        {
            double retNum;
            return double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        /// <summary>
        /// Provera validnosti skolske godine
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        private static bool IsValidSchoolYear(string lesson)
        {
            if (lesson.Length != 9)
            {
                return false;
            }
            return IsNumeric(lesson.Substring(0, 4)) && lesson[4] == '/' && IsNumeric(lesson.Substring(5, 4));
        }

        public string ProjectDir { get; set; }

        public string ToolsDir
        {
            get { return ProjectDir + "\\tools"; }
        }

        public string ResourcesDir
        {
            get
            {
                string resPath = Path.Combine(ProjectDir, "resources");
                if (!Directory.Exists(resPath))
                {
                    Directory.CreateDirectory(resPath);
                }
                return resPath + "\\";
            }
        }

        public string CourseCode { get; set; }

        public string LessonNumber { get; set; }

        public string LessonTitle { get; set; }

        public string Schoolyear { get; set; }

        public string Author { get; set; }

        private LearningOverview _learningOverview;
        public LearningOverview LearningOverview
        {
            get { return _learningOverview; }
            set
            {
                _learningOverview = value;
                foreach (Section sec in _learningOverview.LearningOverviewbody.Sections)
                {
                    sec.Parent = _learningOverview;
                }

                //Dodaje iza UVODA 
                //problem sa duplikatima
                // kada se ide save pa se ponovo otvori lekcija onda
                //on opet napravi mapu
                //kada se otvara lekcija on ne vidi da vec postoji jer se prilikoom ucitavanja
                //ne postavlja mapa u toolslist uvoda ili drugi nacin provere ili da se stavi u toolslist
                //ne sme da se vidi ili bar da se zabrani edit u lams dizajneru u mditi
                //
                //}
            }
        }

        public LearningContentList LearningContents { get; set; }

        private LearningSummary _learningSummary;
        public LearningSummary LearningSummary
        {
            get
            {
                return _learningSummary;
            }
            set
            {
                _learningSummary = value;
                foreach (Section sec in _learningSummary.LearningSummarybody.Sections)
                {
                    sec.Parent = _learningSummary;
                }
            }
        }

        public List<LamsTool> LamsTools
        {
            get
            {
                var list = new List<LamsTool>();
                foreach (var content in LearningContents)
                {
                    list.AddRange(content.ToolList);
                    foreach (var subObject in content.SubObjects)
                    {
                        list.AddRange(subObject.ToolList);
                    }
                }
                list.AddRange(LearningSummary.ToolList);
                return list;
            }
        } 

        public LearningBase FindObjectByPpt(string ppt)
        {
            if (ppt == LearningOverview.FileNamePpt)
            {
                return LearningOverview;
            }
            foreach (var learningContent in LearningContents)
            {
                if (ppt == learningContent.FileNamePpt)
                {
                    return learningContent;
                }
                foreach (var subObject in learningContent.SubObjects)
                {
                    if (ppt == subObject.FileNamePpt)
                    {
                        return subObject;
                    }
                }
            }
            if (ppt == LearningSummary.FileNamePpt)
            {
                return LearningSummary;
            }
            return null;
        }

        public ProjectFile(string projectPath, string courseCode, string lessonNumber, string lessonTitle,
            string schoolyear, string author)
        {
            if (!IsValidSubject(courseCode))
            {
                throw new ArgumentException("Šifra predmeta mora biti u formatu: SE311");
            }
            CourseCode = courseCode;

            if (!IsValidLesson(lessonNumber))
            {
                throw new ArgumentException("Broj lekcije mora biti u formatu: L01");
            }
            LessonNumber = lessonNumber;

            if (string.IsNullOrEmpty(lessonTitle))
            {
                throw new ArgumentException("Naslov lekcije ne može biti prazan");
            }
            LessonTitle = lessonTitle;

            if (!IsValidSchoolYear(schoolyear))
            {
                throw new ArgumentException("Školska godina mora biti u formatu: 2015/2016.");
            }
            Schoolyear = schoolyear;

            if (string.IsNullOrEmpty(author))
            {
                throw new ArgumentException("Ime autora ne može biti prazno");
            }
            Author = author;

            ProjectDir = projectPath;
            Directory.CreateDirectory(ProjectDir);

            LearningContents = new LearningContentList();
            LearningSummary = new LearningSummary();
            InitOverview();
        }

        /// <summary>
        /// Metoda koja azurira stanje atributa sifra predmeta, broj lekcije, naslov, godina i autor.
        /// </summary>
        /// <param name="courseCode"></param>
        /// <param name="lessonNumber"></param>
        /// <param name="lessonTitle"></param>
        /// <param name="year"></param>
        /// <param name="author"></param>
        public void UpdateProject(string courseCode, string lessonNumber, string lessonTitle, string year, string author)
        {
            string oldProjectDir = "";
            if (ProjectDir.EndsWith("\\"))
            {
                oldProjectDir = ProjectDir.Substring(0, ProjectDir.LastIndexOf('\\'));
            }
            string oldGalleryPath = ProjectDir + "\\resources\\imagegallery";

            Console.WriteLine("*************** Old " + oldGalleryPath);

            if (!IsValidSubject(courseCode))
            {
                throw new ArgumentException("Šifra predmeta mora biti u formatu: SE311");
            }           

            if (!IsValidLesson(lessonNumber))
            {
                throw new ArgumentException("Broj lekcije mora biti u formatu: L01");
            }
      


            if (string.IsNullOrEmpty(lessonTitle))
            {
                throw new ArgumentException("Naslov lekcije ne može biti prazan");
            }
            LessonTitle = lessonTitle;

            if (!IsValidSchoolYear(year))
            {
                throw new ArgumentException("Školska godina mora biti u formatu: 2015/2016.");
            }
            Schoolyear = year;

            if (string.IsNullOrEmpty(author))
            {
                throw new ArgumentException("Ime autora ne može biti prazno");
            }
            Author = author;
                   
                                    
            string nPath = createNewPath(ProjectDir, courseCode , lessonNumber);

            
            Debug.WriteLine(ProjectDir);      
            Debug.WriteLine(nPath);
            
            if (CourseCode != courseCode || LessonNumber != lessonNumber)
            {
                try
                {
                    if (CourseCode != courseCode)
                    {
                        DirectoryInfo dir = new DirectoryInfo(nPath);
                        string trimmedPath = dir.Parent.FullName;
                        Directory.CreateDirectory(trimmedPath);

                        
                    }

                    if (!Directory.Exists(nPath))
                    {

                        Directory.Move(ProjectDir, nPath);
                                                
                    }
                    else
                    {
                        throw new ArgumentException("Već postoji lekcija " + lessonNumber + " u okviru predmeta " + courseCode);
                    }

                }
                catch (IOException)
                {

                    throw new ArgumentException("Zatvorite sve fajlove i foldere vezane za projekat i pokušajte ponovo!");
                }


                CourseCode = courseCode;
                LessonNumber = lessonNumber;


                RecentItemsManager.Remove(ProjectDir);
                RecentItemsManager.Add(nPath);
                
                ProjectDir = nPath;

                ProjectFile.folderLekcije = ProjectDir;
            }
            

            UpdateEachObject(author, year);
        }

        /// <summary>
        /// Metoda koja setuje autora i skolsku godinu za svaki objekat ucenja.
        /// </summary>
        /// <param name="author"></param>
        /// <param name="schoolYear"></param>
        private void UpdateEachObject(string author, string schoolYear)
        {
            foreach (LearningContent lc in LearningContents)
            {
                lc.Shortdesc.Draftcomment[0].Text = author;
                lc.Shortdesc.Draftcomment[1].Text = schoolYear;
                foreach (LearningContent slc in lc.SubObjects)
                {
                    lc.Shortdesc.Draftcomment[0].Text = author;
                    lc.Shortdesc.Draftcomment[1].Text = schoolYear;
                }
            }
        }

        private string createNewPath(string oldPath, string courseCode, string lessonNumber)
        {          

            DirectoryInfo dir = new DirectoryInfo(oldPath);
            string trimmedPath = dir.Parent.Parent.FullName;
           
            return Path.Combine(trimmedPath, courseCode, lessonNumber);          

        }

        public XDocument CopyAndGetXmlDoc(string fileName, bool copyFiles = true, bool keepOriginal = false)
        {
            string fileNameNew = (keepOriginal) ? fileName : Path.Combine(ProjectDir, CourseCode + "-" + LessonNumber + "-" + Path.GetFileName(fileName).Split('-')[2]);
         
            string resourceDir = ResourcesDir;
            if (copyFiles)
            {
                File.Copy(fileName, fileNameNew, true);
            }
            //
            //  text = text.Replace("&#x09;", "");
            // File.WriteAllText(fileNameNew, text);
            XDocument doc = null;
            var settings = new XmlReaderSettings();
            settings.NameTable = new NameTable();
            settings.DtdProcessing = DtdProcessing.Ignore;
            var manager = new MyXmlNamespaceManager(settings.NameTable);
            XmlParserContext context = new XmlParserContext(null, manager, null, XmlSpace.Default);

            string text = File.ReadAllText(fileNameNew);
            //Menja sifru predmeta u okviru DITA fajla zbog slika 
            text = text.Replace(Path.GetFileName(fileName).Split('-')[0] + "-", CourseCode + "-");
            bool error = false;
            Exception innerEx = null;
            using (XmlReader reader = XmlReader.Create(new StringReader(text), settings, context))
            {
                try
                {
                    doc = XDocument.Load(reader);
                }
                catch (XmlException ex)
                {

                    innerEx = ex;
                    error = true;
                }

            }
            if (!error)
            {
                var root = doc.Root;
                foreach (XElement element in root.Descendants("sectiondiv"))
                {
                    if (Util.isLastLevel(element))
                    {
                        string content = Util.RemoveSectionDivTag(element.ToString());
                        if (content.Length > 0)
                        {
                            element.Value = content;
                        }
                    }
                }
            }
            else
            {
                throw new DitaObjectCannotBeLoadedException("DITA object cannot be loadded", innerEx);
            }
            return doc;
        }

        /// <summary>
        /// Treba testirati trebalo bi na importu da skine sve prazne TextBox-ove  
        /// </summary>
        /// <param name="sec"></param>
        public void DeleteUnusedTextBoxesFromImport(Section sec)
        {
            Sectiondiv divSekSek = sec.SectionDivs.Find(x => x.Outputclass != "subtitle");
            if (divSekSek == null)
            {
                return;
            }
            foreach (Sectiondiv divSekSek2 in divSekSek.SectionDivs.ToArray())
            {
                foreach (Sectiondiv divSekSek3 in divSekSek2.SectionDivs.ToArray())
                {
                    if (divSekSek3.SectionDivs.Count > 0 &&
                             divSekSek3.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                             !divSekSek3.SectionDivs[0].Content.Contains("<pre"))
                    {
                        if (divSekSek3.SectionDivs[0].SectionDivs.Count == 0 || (string.IsNullOrEmpty(divSekSek3.SectionDivs[0].SectionDivs[0].Content) && divSekSek3.SectionDivs[0].SectionDivs.Count == 1))
                        {
                            string text = divSekSek3.SectionDivs[0].Content.Trim();
                            text = text.Replace("\t", "");
                            text = text.Replace("\n", "");
                            text = text.Replace("\r", "");
                            text = text.Replace("&nbsp;", "");
                            text = text.Replace("<p>", "");
                            text = text.Replace("</p>", "");
                            text = text.Replace(" ", "");
                            if (string.IsNullOrEmpty(text.Trim()))
                            {
                                divSekSek2.SectionDivs.Remove(divSekSek3);
                            }
                        }
                    }
                }
            }
        }
        
        public void GetLastIdForObject(LearningContent learning)
        {
            string lastContentId = "LC-00";
            if (LearningContents.Count > 0)
            {
                lastContentId = LearningContents[LearningContents.Count - 1].Id;
            }
            learning.Id = lastContentId;
            learning.IncrementId();
        }

        /// <summary>
        /// Inicijalizuje LearningOverview objekat tako sto definise atrubute id, title i listu seckija.
        /// </summary>
        private void InitOverview()
        {
            LearningOverview = new LearningOverview()
            {
                Id = LessonNumber.Replace("L", "LO-"),
                Title = LessonTitle,
                LearningOverviewbody = new LearningBody()
                {
                    Sections = new SectionList()
                }
            };
            AddOverviewSection(LearningOverview);
        }

        /// <summary>
        /// Čuva jedan Tool Pod Id-om
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="id"></param>
        public void SerializeOneItemToFile(object objectName, int id)
        {
            XmlSerializer writer = new XmlSerializer(objectName.GetType());
            var path = ProjectDir + "//tools//item" + id + ".xml";
            FileStream file = File.Create(path);
            writer.Serialize(file, objectName);
            file.Close();
        }

        /// <summary>
        /// Metoda koja dodaje prvu sekciju na Overview
        /// </summary>
        /// <param name="overview"></param>
        public void AddOverviewSection(LearningOverview overview)
        {
            Section section = new Section(overview, "section-columns1");
            section.Id = "S-UVOD";
            section.Title = "Uvod";
            Sectiondiv subtitle = new Sectiondiv("subtitle");
            subtitle.Content = "";
            Sectiondiv zakljucakDiv = new Sectiondiv("columns1");
            section.SectionDivs.Add(subtitle);
            section.SectionDivs.Add(zakljucakDiv);
            Sectiondiv lmrc = new Sectiondiv("lmrc");
            zakljucakDiv.SectionDivs.Add(lmrc);

        }

        /// <summary>
        /// Metoda koja dodaje sekcije za sadrzaj na LearningOverview
        /// </summary>
        private void GenerateOverview()
        {
            var overview = LearningOverview.LearningOverviewbody;
            Util.DeleteArrowSections(LearningOverview);
            // LearningOverview.LearningBody.Section.Clear();
            foreach (Section sec in LearningOverview.LearningBody.Sections.ToArray())
            {
                sec.Outputclass = null;
                sec.Id = "S-UVOD";
            }

            for (int j = 0; j < LearningOverview.LearningBody.Sections.Count; j++)
            {
                int i = 0;
                LearningOverview.LearningBody.Sections[j].Parent = LearningOverview;
                LearningOverview.LearningBody.Sections[j].Outputclass = null;
                if (!LearningOverview.LearningBody.Sections[j].Id.ToString().StartsWith("S-") || LearningOverview.LearningBody.Sections[j].Id.ToString().Equals("S-UVOD"))
                {
                        LearningOverview.LearningBody.Sections[j].Id = "S-UVOD";
                }
                if (j == 0)
                {

                    int count = LearningContents.Count;
                    int toHave = 4;
                    if (count > 4)
                    {
                        toHave = 9;
                    } else if(count > 9)
                    {
                        toHave = 14;
                    }
                    int toAdd = toHave - count;


                    foreach (var content in LearningContents)
                    {
                        var sb = new StringBuilder();
                        if (content.SubObjects.Any())
                        {
                            sb.AppendLine().AppendLine("      <ul>");
                            foreach (var subObject in content.SubObjects)
                            {
                                sb.AppendFormat("        <li>{0}</li>", Util.EscapeXml(subObject.Title)).AppendLine();
                            }
                            sb.AppendLine("      </ul>");
                        }
                        else
                        {
                            sb.AppendLine().AppendLine("      <ul>");
                            foreach (var subSection in content.LearningContentBody.Sections)
                            {
                                sb.AppendFormat("        <li outputclass=\"arrow\">{0}</li>", Util.EscapeXml(subSection.Title)).AppendLine();
                            }
                            sb.AppendLine("      </ul>");
                        }
                        Section section = new Section(LearningOverview, "")
                        {
                            Id = string.Format("S-{0:D2}", ++i),
                            Title = content.Title,
                            Content = sb.ToString()
                        };
                    }

                    for(int k=0;k< toAdd; k++)
                    {
                        Section section = new Section(LearningOverview, "")
                        {
                            Id = string.Format("S-{0:D2}", ++i),
                            Title = "",
                            Content = ""
                        };
                    }
                }
            }
                   
            LearningOverview.LearningOverviewbody.Sections.Sort((one, two) => one.Id.Length.CompareTo(two.Id.Length));
            if(LearningOverview.LearningOverviewbody.Sections.FindAll(x => x.Id == "S-UVOD").Count > 0)
            {
               Section start = LearningOverview.LearningOverviewbody.Sections.Find(x => x.Id == "S-UVOD");
                LearningOverview.LearningOverviewbody.Sections.Remove(start);
                if (LearningOverview.LearningOverviewbody.Sections.Count > 0)
                {
                    LearningOverview.LearningOverviewbody.Sections.Insert(0, start);
                }
                else
                {
                    LearningOverview.LearningOverviewbody.Sections.Add(start);
                }
            }

        }

        /// <summary>
        /// Metoda koja upisuje informacije o projektu u fajl
        /// Upisuje se ime predmeta, lekcija kao i skolska godina kao globalna promenjiva
        /// </summary>
        public void SaveProjectFile()
        {
            string[] lines = { CourseCode, LessonNumber, LessonTitle, Schoolyear, Author };
            File.WriteAllLines(Path.Combine(ProjectDir, "project.mdita"), lines);
        }

        private Control GetControlForDisposition(string disposition)
        {
            switch (disposition)
            {
                case "Classification":
                    return MainForm.Instance.contentControl.txbClassification;
                case "Difficulty level":
                    return MainForm.Instance.contentControl.cmbDifficulty;
                case "Keywords":
                    return MainForm.Instance.contentControl.txbKeywords;
                case "Learning duration":
                    return MainForm.Instance.contentControl.txbDuration;
            }
            return null;
        }
    }
}
