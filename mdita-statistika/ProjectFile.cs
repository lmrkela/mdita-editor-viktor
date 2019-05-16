using StatistikaProjekata.DITA;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using StatistikaProjekata.LAMS;

namespace StatistikaProjekata
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
            return char.IsLetter(subject[0]) && char.IsLetter(subject[1]) && char.IsNumber(subject[2]) &&
                   char.IsNumber(subject[3]) && char.IsNumber(subject[4]);
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
            return double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any,
                System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
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
                var resPath = Path.Combine(ProjectDir, "resources");
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

        public override string ToString()
        {
            return CourseCode + " - " + LessonNumber;
        }

        public List<LamsTool> LamsTools = new List<LamsTool>();

        private LearningOverview _learningOverview;

        public LearningOverview LearningOverview
        {
            get { return _learningOverview; }
            set
            {
                _learningOverview = value;
                foreach (var sec in _learningOverview.LearningOverviewbody.Section)
                {
                    sec.Parent = _learningOverview;
                }
            }
        }

        public ListObject LearningContents { get; set; }

        private LearningSummary _learningSummary;

        public LearningSummary LearningSummary
        {
            get { return _learningSummary; }
            set
            {
                _learningSummary = value;
                foreach (var sec in _learningSummary.LearningSummarybody.Section)
                {
                    sec.Parent = _learningSummary;
                }
            }
        }



        public ProjectFile(string projectPath, string courseCode, string lessonNumber, string lessonTitle,
            string schoolyear, string author, bool isMain = true)
        {
            if (!IsValidSubject(courseCode))
            {
                throw new ArgumentException("Šifra predmeta mora biti u formatu: SE311");
            }
            CourseCode = courseCode;

            if (!IsValidLesson(lessonNumber))
            {
                //throw new ArgumentException("Broj lekcije mora biti u formatu: L01");
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

            LearningContents = new ListObject();
            if (isMain)
            {

            }
            LearningSummary = new LearningSummary();
            InitOverview();
        }

        /// <summary>
        /// Metoda koja azurira stanje atributa naslov, godina i autor.
        /// </summary>
        /// <param name="naslovLekcije"></param>
        /// <param name="godina"></param>
        /// <param name="autor"></param>
        public void UpdateProject(string naslovLekcije, string godina, string autor)
        {
            if (string.IsNullOrEmpty(naslovLekcije))
            {
                throw new ArgumentException("Naslov lekcije ne može biti prazan");
            }
            LessonTitle = naslovLekcije;

            if (!IsValidSchoolYear(godina))
            {
                throw new ArgumentException("Školska godina mora biti u formatu: 2015/2016.");
            }
            Schoolyear = godina;

            if (string.IsNullOrEmpty(autor))
            {
                throw new ArgumentException("Ime autora ne može biti prazno");
            }
            Author = autor;

            UpdateEachObject(autor, godina);
        }

        /// <summary>
        /// Metoda koja setuje autora i skolsku godinu za svaki objekat ucenja.
        /// </summary>
        /// <param name="author"></param>
        /// <param name="schoolYear"></param>
        private void UpdateEachObject(string author, string schoolYear)
        {
            foreach (var lc in LearningContents)
            {
                lc.Shortdesc.Draftcomment[0].Text = author;
                lc.Shortdesc.Draftcomment[1].Text = schoolYear;
                foreach (var slc in lc.SubObjects)
                {
                    lc.Shortdesc.Draftcomment[0].Text = author;
                    lc.Shortdesc.Draftcomment[1].Text = schoolYear;
                }
            }
        }

        public static ProjectFile OpenProjectForFile(string filePath, bool isMain = true)
        {
            using (var reader = new StreamReader(filePath))
            {
                var sifraPredmeta = reader.ReadLine();
                var brojLekcije = reader.ReadLine();
                var naslovLekcije = reader.ReadLine();
                var godina = reader.ReadLine();
                var autor = reader.ReadLine();
                return new ProjectFile(Directory.GetParent(filePath).FullName, sifraPredmeta, brojLekcije, naslovLekcije,
                    godina, autor, isMain);
            }
        }

        public XDocument CopyAndGetXmlDoc(string fileName, bool copyFiles = true, bool keepOriginal = false)
        {
            var fileNameNew = (keepOriginal)
                ? fileName
                : Path.Combine(ProjectDir,
                    CourseCode + "-" + LessonNumber + "-" + Path.GetFileName(fileName).Split('-')[2]);

            var resourceDir = ResourcesDir;
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
            var context = new XmlParserContext(null, manager, null, XmlSpace.Default);

            var text = File.ReadAllText(fileNameNew);
            //Menja sifru predmeta u okviru DITA fajla zbog slika 
            text = text.Replace(Path.GetFileName(fileName).Split('-')[0] + "-", CourseCode + "-");
            var error = false;
            using (var reader = XmlReader.Create(new StringReader(text), settings, context))
            {
                try
                {
                    doc = XDocument.Load(reader);
                }
                catch (XmlException ex)
                {
                    error = true;
                }

            }
            if (!error)
            {
                var root = doc.Root;
                foreach (var element in root.Descendants("sectiondiv"))
                {
                    if (Util.isLastLevel(element))
                    {
                        var content = Util.RemoveSectionDivTag(element.ToString());
                        if (content.Length > 0)
                        {
                            element.Value = content;
                        }
                    }
                }
            }
            else
            {
                //throw new DITAObjectCannotBeLoadedException("DITA object cannot be loadded", innerEx);
            }
            return doc;
        }

        /// <summary>
        /// Treba testirati trebalo bi na importu da skine sve prazne TextBox-ove
        /// </summary>
        /// <param name="sec"></param>
        public void DeleteUnusedTextBoxesFromImport(Section sec)
        {
            var divSekSek = sec.Sectiondiv.Find(x => x.Outputclass != "subtitle");
            if (divSekSek == null)
            {
                return;
            }
            foreach (var divSekSek2 in divSekSek.SectionDiv.ToArray())
            {
                foreach (var divSekSek3 in divSekSek2.SectionDiv.ToArray())
                {
                    if (divSekSek3.SectionDiv.Count > 0 &&
                        divSekSek3.SectionDiv[0].Outputclass.Substring(0, 1) == "f" &&
                        !divSekSek3.SectionDiv[0].Content.Contains("<pre"))
                    {
                        if (divSekSek3.SectionDiv[0].SectionDiv.Count == 0 ||
                            (string.IsNullOrEmpty(divSekSek3.SectionDiv[0].SectionDiv[0].Content) &&
                             divSekSek3.SectionDiv[0].SectionDiv.Count == 1))
                        {
                            var text = divSekSek3.SectionDiv[0].Content.Trim();
                            text = text.Replace("\t", "");
                            text = text.Replace("\n", "");
                            text = text.Replace("\r", "");
                            text = text.Replace("&nbsp;", "");
                            text = text.Replace("<p>", "");
                            text = text.Replace("</p>", "");
                            text = text.Replace(" ", "");
                            if (string.IsNullOrEmpty(text.Trim()))
                            {
                                divSekSek2.SectionDiv.Remove(divSekSek3);
                            }
                        }
                    }
                }
            }
        }

        public void GetLastIdForObject(LearningContent learning)
        {
            var lastContentId = "LC-00";
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
                    Section = new ListSection()
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
            var writer = new XmlSerializer(objectName.GetType());
            var path = ProjectDir + "//tools//item" + id + ".xml";
            var file = File.Create(path);
            writer.Serialize(file, objectName);
            file.Close();
        }

        /// <summary>
        /// Metoda koja dodaje prvu sekciju na Overview
        /// </summary>
        /// <param name="overview"></param>
        public void AddOverviewSection(LearningOverview overview)
        {
            var section = new Section(overview, "section-columns1");
            section.Id = "S-UVOD";
            section.Title = "Uvod";
            var subtitle = new Sectiondiv("subtitle");
            subtitle.Content = "";
            var zakljucakDiv = new Sectiondiv("columns1");
            section.Sectiondiv.Add(subtitle);
            section.Sectiondiv.Add(zakljucakDiv);
            var lmrc = new Sectiondiv("lmrc");
            zakljucakDiv.SectionDiv.Add(lmrc);
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

        /// <summary>
        /// Metoda koja upisuje informacije o projektu u fajl
        /// Upisuje se ime predmeta, lekcija kao i skolska godina kao globalna promenjiva
        /// </summary>
        public void SaveProjectFile()
        {
            string[] lines = {CourseCode, LessonNumber, LessonTitle, Schoolyear, Author};
            File.WriteAllLines(Path.Combine(ProjectDir, "project.mdita"), lines);
        }

        public void OpenContentFile(string fileName, bool copyFiles = true)
        {
            try
            {
                var doc = CopyAndGetXmlDoc(fileName, copyFiles);
                if (doc.Root.Name.LocalName == "learningContent")
                {
                    var learning = (LearningContent) Util.FromXml(doc.ToString(), typeof (LearningContent));
                    learning.Project = this;
                    LearningContents.Add(learning, this);
                    foreach (var section in learning.LearningBody.Section)
                    {
                        DeleteUnusedTextBoxesFromImport(section);
                    }
                }
                else if (doc.Root.Name.LocalName == "learningSummary")
                {
                    var learning = (LearningSummary) Util.FromXml(doc.ToString(), typeof (LearningSummary));
                    learning.Project = this;
                    LearningSummary = learning;
                    foreach (var section in learning.LearningBody.Section)
                    {
                        DeleteUnusedTextBoxesFromImport(section);
                    }
                }
                else if (doc.Root.Name.LocalName == "learningOverview")
                {
                    var learning = (LearningOverview) Util.FromXml(doc.ToString(), typeof (LearningOverview));
                    learning.Project = this;
                    learning.Id = "LO-" + LessonNumber.Replace("L", "");
                    LessonTitle = learning.Title;
                    LearningOverview = learning;
                    Util.DeleteArrowSections(learning);
                    foreach (var section in learning.LearningBody.Section)
                    {
                        DeleteUnusedTextBoxesFromImport(section);
                    }
                }
            }
            catch (Exception e)
            {
                var xmlE = e.InnerException as XmlException;
                var message = e.Message;
                if (xmlE != null)
                {
                    message =
                        string.Format(
                            "{0} (Linija broj: {1}; Pozicija u liniji: {2}; Fajl: {3};) Ovaj objekat neće biti učitan. Trebalo bi da pošaljete lekciju adminsitratorima na ispravku.",
                            message, xmlE.LineNumber, xmlE.LinePosition, fileName);
                    MessageBox.Show(message, "Postoji greška u fajlu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Metoda za ucitavanje samo jednog objekta
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="copyFiles"></param>
        public void OpenContentFileOne(string fileName, bool copyFiles = true)
        {
            var doc = CopyAndGetXmlDoc(fileName, copyFiles);

            if (doc.Root.Name.LocalName == "learningContent")
            {
                var learning = (LearningContent) Util.FromXml(doc.ToString(), typeof (LearningContent));
                foreach (var section in learning.LearningBody.Section)
                {
                    DeleteUnusedTextBoxesFromImport(section);
                }
                GetLastIdForObject(learning);
                LearningContents.Add(learning);
            }
            else
            {
                MessageBox.Show("Niste učitali DITA objekat");
            }
        }

        public bool OpenToolsFile_Deprecated()
        {
            var toolPath = ProjectDir + "\\" + "tools.xml";
            if (!File.Exists(toolPath))
            {
                return false;
            }
            using (var myFileStream = new FileStream(toolPath, FileMode.Open))
            {
                var mySerializer = new XmlSerializer(typeof (LamsToolFileList));
                var tools = (LamsToolFileList) mySerializer.Deserialize(myFileStream);
                foreach (var obj in tools.ToolFileInfo)
                {
                    LearningBase content = LearningContents.Find(x => x.Id == obj.Id);
                    if (content == null)
                    {
                        content = LearningSummary;
                    }
                    foreach (var tool in obj.Tool)
                    {
                        var lamsTool = LoadToolFromXml(ToolsDir + "\\" + tool.ToolFile);
                        if (lamsTool != null)
                        {
                            content.ToolList.Add(lamsTool);
                        }
                    }
                }
            }
            return true;
        }

        public void OpenToolsFile()
        {
            var designerFile = ToolsDir + "\\learning_design.xml";
            if (!File.Exists(designerFile))
            {
                return;
            }
            LearningBase learningObject = null;
            var tools = new Dictionary<long, LamsTool>();
            var dirs = Directory.GetDirectories(ToolsDir);
            foreach (var dir in dirs)
            {
                var tool = LoadToolFromXml(dir + "\\tool.xml");
                tools.Add(tool.ToolContentID, tool);
                if (tool is LamsNoticeboard)
                {
                    var noticeboard = (LamsNoticeboard) tool;
                    var match = Regex.Match(noticeboard.Content, "-[a-z0-9]*.html");
                    if (match.Value != "")
                    {
                        var ppt = match.Value.Substring(1, match.Value.Length - 1);
                        ppt = ppt.Substring(0, ppt.Length - 5);
                        learningObject = FindObjectByPpt(ppt);
                        noticeboard.LearningObject = learningObject;
                        continue;
                    }
                }
                if (tool.DesignerParentIndex < -1)
                {
                    learningObject.ToolList.Add(tool);
                }
                else if (tool.DesignerParentIndex == -1)
                {
                    LearningSummary.ToolList.Add(tool);
                }
                else
                {
                    LearningContents[tool.DesignerParentIndex].ToolList.Add(tool);
                }
                LamsTools.Add(tool);
            }
        }

        /// <summary>
        /// Loaduje LamsTool preko datog XML-a.
        /// </summary>
        private LamsTool LoadToolFromXml(string filename)
        {
            var settings = new XmlReaderSettings
            {
                NameTable = new NameTable(),
                DtdProcessing = DtdProcessing.Ignore
            };
            var context = new XmlParserContext(null, new MyXmlNamespaceManager(settings.NameTable), null,
                XmlSpace.Default);

            if (!File.Exists(filename))
            {
                return null;
            }
            var text = File.ReadAllText(filename);
            XDocument doc;
            using (var reader = XmlReader.Create(new StringReader(text), settings, context))
            {
                doc = XDocument.Load(reader);
            }

            switch (doc.Root.Name.LocalName)
            {
                case "org.lamsfoundation.lams.tool.qa.QaContent":
                    return (LamsQA) Util.FromXml(doc.ToString(), typeof (LamsQA));
                case "org.lamsfoundation.lams.tool.forum.persistence.Forum":
                    return (LamsForum) Util.FromXml(doc.ToString(), typeof (LamsForum));
                case "org.lamsfoundation.lams.tool.mc.pojos.McContent":
                    return (LamsMultipleChoice) Util.FromXml(doc.ToString(), typeof (LamsMultipleChoice));
                case "org.lamsfoundation.lams.tool.sbmt.SubmitFilesContent":
                    return (LamsSubmitFiles) Util.FromXml(doc.ToString(), typeof (LamsSubmitFiles));
                case "org.lamsfoundation.lams.tool.rsrc.model.Resource":
                    return (LamsShareResource) Util.FromXml(doc.ToString(), typeof (LamsShareResource));
                case "org.lamsfoundation.lams.tool.assessment.model.Assessment":
                    return (LamsAssessment) Util.FromXml(doc.ToString(), typeof (LamsAssessment));
                case "org.lamsfoundation.lams.tool.chat.model.Chat":
                    return (LamsChat) Util.FromXml(doc.ToString(), typeof (LamsChat));
                case "org.lamsfoundation.lams.tool.noticeboard.NoticeboardContent":
                    return (LamsNoticeboard) Util.FromXml(doc.ToString(), typeof (LamsNoticeboard));
                case "org.lamsfoundation.lams.tool.javagrader.model.Javagrader":
                    return (Javagrader) Util.FromXml(doc.ToString(), typeof (Javagrader));
                case "org.lamsfoundation.lams.tool.notebook.model.Notebook":
                    return (LamsNotebook) Util.FromXml(doc.ToString(), typeof (LamsNotebook));
            }
            return null;
        }
    }
}
