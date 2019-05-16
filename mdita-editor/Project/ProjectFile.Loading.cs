using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using mDitaEditor.Dita;
using mDitaEditor.Lams;
using mDitaEditor.Lams.Editor;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Utils;

namespace mDitaEditor.Project
{
    partial class ProjectFile
    {
        public static string folderLekcije;
        public static IEnumerable<string> CustomSort(IEnumerable<string> list)
        {
            if (!list.Any())
            {
                return list;
            }

            int maxLen = list.Select(s => s.Length).Max();

            return list.Select(s => new
            {
                OrgStr = s,
                SortStr = Regex.Replace(s, @"(\d+)|(\D+)", m => m.Value.PadLeft(maxLen, char.IsDigit(m.Value[0]) ? ' ' : '\xffff'))
            })
            .OrderBy(x => x.SortStr)
            .Select(x => x.OrgStr);
        }

        /// <summary>
        /// Metoda koja ucitava fajl u klasu.
        /// </summary>
        /// <param name="filePath"></param>
        public static ProjectFile LoadFolder(string filePath)
        {
            folderLekcije = filePath;
            ProjectFile project;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string sifraPredmeta = reader.ReadLine();
                    string brojLekcije = reader.ReadLine();
                    string naslovLekcije = reader.ReadLine();
                    string godina = reader.ReadLine();
                    string autor = reader.ReadLine();
                    project = new ProjectFile(Directory.GetParent(filePath).FullName, sifraPredmeta, brojLekcije, naslovLekcije, godina, autor);
                }

                ProjectSingleton.Project = project; // TODO ukloni ProjectSingleton
                var files = CustomSort(Directory.GetFiles(project.ProjectDir, "*.dita"));
                foreach (string file in files)
                {
                    project.OpenContentFile(file, false);
                }
                if (!project.OpenToolsFile_Deprecated())
                {
                    Console.WriteLine("Otvara tools file");
                    project.OpenToolsFile();
                }

               

                return project;
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.FileName + " File not found!");
                return null;
            }
        }

        public void MergeByProjectFile(string path)
        {
            var maxId = LearningContents.Last().Id;
            int id = int.Parse(maxId.Split('-')[1]);
            ProjectFile project;
            using (StreamReader reader = new StreamReader(path))
            {
                string sifraPredmeta = reader.ReadLine();
                string brojLekcije = reader.ReadLine();
                string naslovLekcije = reader.ReadLine();
                string godina = reader.ReadLine();
                string autor = reader.ReadLine();
                project = new ProjectFile(Directory.GetParent(path).FullName, sifraPredmeta, brojLekcije, naslovLekcije, godina, autor);
            }
            var files = Directory.GetFiles(project.ProjectDir, "*.dita");
            List<string> objectFiles = new List<string>();
            foreach (var f in files.ToArray())
            {
                if (f.Contains("pptlc"))
                {
                    objectFiles.Add(f);
                }
            }
            bool isError = false;
            var filesNew = CustomSort(objectFiles);
            List<LearningContent> contentAdded = new List<LearningContent>();
            foreach (string f in filesNew)
            {
                if (f.Contains("pptlc"))
                {
                    LearningContent content = OpenContentWithIdMax(f, id);
                    if (content != null)
                    {
                        contentAdded.Add(content);
                    }
                    else
                    {
                        isError = true;
                    }
                }
            }
            DitaClipboard.AddMergeProjectsState(contentAdded);
            if (isError)
            {
                MessageBox.Show("Ubacili ste projekat koji zajedno sa postojećim projektom ima više od 14 objekata. Objekti do 14-tog su učitani.");
            }
            MainForm.Instance.RecreateMenu();
        }

        public LearningContent OpenContentWithIdMax(string fileName, int id)
        {
            try
            {
                if (LearningContents.Count < 14)
                {
                    ImportDitaFiles.AppendFilesToResourceFrom(fileName);
                    XDocument doc = CopyAndGetXmlDoc(fileName, false, true);
                    if (doc.Root.Name.LocalName == "learningContent")
                    {
                        LearningContent learning = (LearningContent)Util.FromXml(doc.ToString(), typeof(LearningContent));
                        int currentId = int.Parse(learning.Id.Split('-')[1]);
                        int newid = currentId + id;
                        var newId = (newid > 9) ? "LC-" + newid : "LC-0" + newid;
                        learning.Id = newId;
                        LearningContents.Add(learning);
                        AddAuthorAndSchoolYearForImport(learning);
                        foreach (var section in learning.LearningBody.Sections)
                        {
                            DeleteUnusedTextBoxesFromImport(section);
                        }
                        return learning;
                    }
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
        public  string FixFigureThatIsUsedWrong(string forReturn)
        {
            forReturn = Regex.Replace(forReturn, "<sectiondiv outputclass=\"vp[0-9]*\">[ \t\r\n\v\f]*<sectiondiv outputclass=\"f[0-9]*(.[0-9]*)?\">[ \t\r\n\v\f]*&lt;image href(.*?)<\\/sectiondiv>[ \t\r\n\v\f]*<\\/sectiondiv>", delegate (Match match)
            {
                string full = match.Groups[0].ToString();
                Match matchnew = Regex.Match(full, "<sectiondiv outputclass=\"f[0-9]*(.[0-9]*)?\">[ \t\r\n\v\f]*(.*?)<\\/sectiondiv>", RegexOptions.Singleline);
                string imageField = matchnew.Groups[2].Value;
                Match paragraphMatch = Regex.Match(imageField, "&lt;p&gt;[ \t\r\n\v\f]*(.*?)&lt;/p&gt;", RegexOptions.Singleline);
                string paragraph = paragraphMatch.Groups[1].Value;
                Match imageMatch = Regex.Match(imageField, "&lt;image[ \t\r\n\v\f]*(.*?)/&gt;", RegexOptions.Singleline);
                string image = imageMatch.Groups[0].Value;
                string final = "<sectiondiv outputclass=\"vp1\">  \r\n  &lt;fig&gt; \r\n    &lt;title&gt;" + paragraph + "&lt;/title&gt;     " + image + " \r\n  &lt;/fig&gt;  \r\n  </sectiondiv>";
                return final;
            }, RegexOptions.Singleline);
            forReturn = forReturn.Replace("\"&gt;", "\">");
            forReturn = forReturn.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
            return forReturn;
        }
        /// <summary>
        /// Metoda koja radi kada je ucitano vise dita fajlova
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="copyFiles"></param>
        public void OpenContentFile(string fileName, bool copyFiles = true)
        {
            try
            {
                var doc = CopyAndGetXmlDoc(fileName, copyFiles);
                var fixedImagesDoc = FixFigureThatIsUsedWrong(doc.ToString());
                if (doc.Root.Name.LocalName == "learningContent")
                {
                    var learning = (LearningContent)Util.FromXml(fixedImagesDoc, typeof(LearningContent));
                    LearningContents.Add(learning);
                    AddAuthorAndSchoolYearForImport(learning);
                    foreach (var section in learning.LearningBody.Sections)
                    {
                        DeleteUnusedTextBoxesFromImport(section);
                    }
                }
                else if (doc.Root.Name.LocalName == "learningSummary")
                {
                    var learning = (LearningSummary)Util.FromXml(fixedImagesDoc, typeof(LearningSummary));
                    LearningSummary = learning;

                    foreach (var section in learning.LearningBody.Sections)
                    {
                        DeleteUnusedTextBoxesFromImport(section);
                    }
                }
                else if (doc.Root.Name.LocalName == "learningOverview")
                {
                    var learning = (LearningOverview)Util.FromXml(fixedImagesDoc, typeof(LearningOverview));
                    learning.Id = "LO-" + LessonNumber.Replace("L", "");
                    LessonTitle = learning.Title;
                    LearningOverview = learning;
              

                    Util.DeleteArrowSections(learning);

                    foreach (var section in learning.LearningBody.Sections)
                    {
                        DeleteUnusedTextBoxesFromImport(section);
                    }
                }
            }
            catch (DitaObjectCannotBeLoadedException e)
            {
                var xmlE = e.InnerException as XmlException;
                var message = e.Message;
                if (xmlE != null)
                {
                    message = string.Format("{0} (Linija broj: {1}; Pozicija u liniji: {2}; Fajl: {3};) Ovaj objekat neće biti učitan. Trebalo bi da pošaljete lekciju adminsitratorima na ispravku.", message, xmlE.LineNumber, xmlE.LinePosition, fileName);
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
            XDocument doc = CopyAndGetXmlDoc(fileName, copyFiles);

            if (doc.Root.Name.LocalName == "learningContent")
            {
                var learning = (LearningContent)Util.FromXml(doc.ToString(), typeof(LearningContent));
                AddAuthorAndSchoolYearForImport(learning); // FIX ZA STARE LEKCIJE
                foreach (var section in learning.LearningBody.Sections)
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
        /// <summary>
        /// FIX ZA STARE LEKCIJE
        /// </summary>
        private void AddAuthorAndSchoolYearForImport(LearningContent learning)
        {
            var oldDesc = learning.Shortdesc.Draftcomment;
            learning.Shortdesc.Draftcomment = new List<Draftcomment>(LearningContent.DraftDispositions.Length);
            foreach (var draftDisposition in LearningContent.DraftDispositions)
            {
                Draftcomment draf = new Draftcomment
                {
                    Text = "",
                    Disposition = draftDisposition
                };
                learning.Shortdesc.Draftcomment.Add(draf);
            }
            foreach (var oldDraft in oldDesc)
            {
                foreach (var draftcomment in learning.Shortdesc.Draftcomment)
                {
                    if (draftcomment.Disposition == oldDraft.Disposition)
                    {
                        draftcomment.Text = oldDraft.Text;
                        break;
                    }
                }
            }
            learning.Shortdesc.Draftcomment[0].Text = Author;
            learning.Shortdesc.Draftcomment[1].Text = Schoolyear;
            learning.Shortdesc.Draftcomment[5].Text = CourseCode;
        }

        /// <summary>
        /// Opens Tools XML file
        /// </summary>
        public bool OpenToolsFile_Deprecated()
        {
            string toolPath = ProjectDir + "\\" + "tools.xml";
            if (!File.Exists(toolPath))
            {
                return false;
            }
            using (var myFileStream = new FileStream(toolPath, FileMode.Open))
            {
                var mySerializer = new XmlSerializer(typeof(LamsToolFileList));
                var tools = (LamsToolFileList)mySerializer.Deserialize(myFileStream);
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
            using (var fileStream = new FileStream(designerFile, FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LearningDesignDTO));
                LearningDesignDTO designer = (LearningDesignDTO)serializer.Deserialize(fileStream);

                var items = new Dictionary<long, GrafikaItem>(designer.Activities.List.Count);

                LearningBase learningObject = null;
                var tools = new Dictionary<long, LamsTool>();
                var dirs = Directory.GetDirectories(ToolsDir);
                foreach (var dir in dirs)
                {
                    var tool = LoadToolFromXml(dir + "\\tool.xml");
                    if (tools.ContainsKey(tool.ToolContentID))
                    {
                        continue;
                    }
                    tools.Add(tool.ToolContentID, tool);
                    if (tool is LamsNoticeboard)
                    {
                        var noticeboard = (LamsNoticeboard)tool;
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
                    Console.WriteLine("Parent: " + tool.DesignerParentIndex);
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
                }
                var branchPaths = new List<AuthoringActivityDTO>();
                var branches = new List<AuthoringActivityDTO>();
                foreach (var activity in designer.Activities.List)
                {
                    IGrafikaObject obj = null;
                    if (!activity.Initialised)
                    {
                        continue;
                    }
                    switch (activity.ActivityTypeID)
                    {
                        case 12:
                            branches.Add(activity);
                            var branch = new LamsBranch
                            {
                                TitleText = activity.ActivityTitle
                            };
                            branch.Entries.AddRange(
                                designer.BranchMappings.BranchList.FindAll(x => x.BranchingActivityUIID == activity.ActivityUIID));
                            if (branch.Entries.Count != 0)
                            {
                                var uiId = branch.Entries[0].Condition.ToolActivityUIID;
                                var contentId = designer.Activities.List.Find(x => x.ActivityUIID == uiId)?.ToolContentID;
                                branch.InputTool = tools[contentId.Value];
                            }
                            obj = branch;
                            break;
                        case 8:
                            branchPaths.Add(activity);
                            break;
                        case 14:
                            var gate = new LamsGate
                            {
                                TitleText = activity.ActivityTitle
                            };
                            gate.Entries.AddRange(
                                designer.BranchMappings.GateList.FindAll(x => x.GateActivityUIID == activity.ActivityUIID));
                            if (gate.Entries.Count != 0)
                            {
                                var uiId = gate.Entries[0].Condition.ToolActivityUIID;
                                var contentId = designer.Activities.List.Find(x => x.ActivityUIID == uiId)?.ToolContentID;
                                gate.InputTool = tools[contentId.Value];
                            }
                            obj = gate;
                            break;
                        case 7:
                            var optional = new LamsOptional
                            {
                                TitleText = activity.ActivityTitle
                            };
                            obj = optional;
                            break;
                        default:
                            if (tools.ContainsKey(activity.ActivityID))
                            {
                                LamsTool tool;
                                tools.TryGetValue(activity.ActivityID, out tool);
                                if (tool != null)
                                {
                                    if (activity.ParentActivityId.HasValue)
                                    {
                                        if (items.ContainsKey(activity.ParentActivityId.Value))
                                        {
                                            var parent = (LamsOptional) items[activity.ParentActivityId.Value].GrafikaObject;
                                            parent.SubObjects.Add(tool);
                                        }
                                        else
                                        {
                                            obj = tool;
                                        }
                                    }
                                    else
                                    {
                                        obj = tool;
                                    }
                                }
                            }
                            break;
                    }
                    if (obj != null)
                    {
                        var item = GrafikaItem.Create(MainForm.Instance.grafikaPanel.Canvas, new Point(activity.XCoord.Value, activity.YCoord.Value), obj);
                        items.Add(activity.ActivityID, item);
                        if (item is GrafikaBranchStartItem)
                        {
                            var endItem = new GrafikaBranchEndItem((GrafikaBranchStartItem) item)
                            {
                                X = activity.EndXCoord.Value,
                                Y = activity.EndYCoord.Value
                            };
                            items.Add(-activity.ActivityID, endItem);
                        }
                    }
                }
                MainForm.Instance.grafikaPanel.Canvas.Items.AddRange(items.Values);

                foreach (var transition in designer.Transitions.List)
                {
                    var startItem = items.ContainsKey(transition.FromActivityID) ? items[transition.FromActivityID] : null;
                    var endItem = items.ContainsKey(transition.ToActivityID) ? items[transition.ToActivityID] : null;
                    if (startItem != null)
                    {
                        var branchStartItem = startItem as GrafikaBranchStartItem;
                        if (branchStartItem != null)
                        {
                            branchStartItem.EndItem.Next = endItem;
                        }
                        else
                        {
                            startItem.Next = endItem;
                        }
                    }
                }

                foreach (var activity in branchPaths)
                {
                    var branch = items[activity.ParentActivityId.Value] as GrafikaBranchStartItem;
                    var item = items[activity.DefaultActivityUiId.Value];
                    branch.Next = item;
                    while (item.Next != null)
                    {
                        item = item.Next;
                    }
                    var branchStartItem = item as GrafikaBranchStartItem;
                    if (branchStartItem != null)
                    {
                        branchStartItem.EndItem.Next = branch.EndItem;
                    }
                    else
                    {
                        item.Next = branch.EndItem;
                    }
                }

                foreach (var activity in branches)
                {
                    if (!activity.DefaultActivityUiId.HasValue)
                    {
                        continue;
                    }

                    var branch = items[activity.ActivityID] as GrafikaBranchStartItem;

                    var item =
                        items[
                            branchPaths.Find(b => b.ActivityUIID == activity.DefaultActivityUiId.Value)
                                .DefaultActivityUiId.Value];
                    foreach (var branchConnection in branch.Branch.Branches)
                    {
                        if (branchConnection.EndItem == item)
                        {
                            branch.Branch.DefaultBranch = branchConnection;
                            break;
                        }
                    }

                    foreach (var entry in branch.Branch.Entries)
                    {
                        var act = branchPaths.Find(b => b.ActivityUIID == entry.SequenceActivityUIID);
                        if (act == null)
                        {
                            continue;
                        }
                        item = items[act.DefaultActivityUiId.Value];
                        foreach (var branchConnection in branch.Branch.Branches)
                        {
                            if (branchConnection.EndItem == item)
                            {
                                entry.BranchPath = branchConnection;
                                break;
                            }
                        }
                    }
                }
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
            var context = new XmlParserContext(null, new MyXmlNamespaceManager(settings.NameTable), null, XmlSpace.Default);

            if (!File.Exists(filename))
            {

                return null;
            }
            var text = File.ReadAllText(filename);
            XDocument doc;
            using (XmlReader reader = XmlReader.Create(new StringReader(text), settings, context))
            {
                doc = XDocument.Load(reader);
            }

            switch (doc.Root.Name.LocalName)
            {
                case "org.lamsfoundation.lams.tool.qa.QaContent":
                    return (LamsQa)Util.FromXml(doc.ToString(), typeof(LamsQa));
                case "org.lamsfoundation.lams.tool.forum.persistence.Forum":
                    return (LamsForum)Util.FromXml(doc.ToString(), typeof(LamsForum));
                case "org.lamsfoundation.lams.tool.mc.pojos.McContent":
                    return (LamsMultipleChoice)Util.FromXml(doc.ToString(), typeof(LamsMultipleChoice));
                case "org.lamsfoundation.lams.tool.sbmt.SubmitFilesContent":
                    return (LamsSubmitFiles)Util.FromXml(doc.ToString(), typeof(LamsSubmitFiles));
                case "org.lamsfoundation.lams.tool.rsrc.model.Resource":
                    return (LamsShareResource)Util.FromXml(doc.ToString(), typeof(LamsShareResource));
                case "org.lamsfoundation.lams.tool.assessment.model.Assessment":
                    return (LamsAssessment)Util.FromXml(doc.ToString(), typeof(LamsAssessment));
                case "org.lamsfoundation.lams.tool.chat.model.Chat":
                    return (LamsChat)Util.FromXml(doc.ToString(), typeof(LamsChat));
                case "org.lamsfoundation.lams.tool.noticeboard.NoticeboardContent":
                    return (LamsNoticeboard)Util.FromXml(doc.ToString(), typeof(LamsNoticeboard));
                case "org.lamsfoundation.lams.tool.javagrader.model.Javagrader":
                    return (LamsJavaGrader)Util.FromXml(doc.ToString(), typeof(LamsJavaGrader));
                case "org.lamsfoundation.lams.tool.notebook.model.Notebook":
                    return (LamsNotebook)Util.FromXml(doc.ToString(), typeof(LamsNotebook));
                case "org.lamsfoundation.lams.tool.imageGallery.model.ImageGallery":
                    return (LamsImageGallery)Util.FromXml(doc.ToString(), typeof(LamsImageGallery));

            }
            return null;
        }

        
    }
}
