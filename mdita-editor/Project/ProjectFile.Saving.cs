using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using mDitaEditor.Dita;
using mDitaEditor.Lams;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Utils;
using System.Windows.Forms;
using System.Diagnostics;

namespace mDitaEditor.Project
{
    partial class ProjectFile
    {
        public int SaveProject(bool export, bool branching, BackgroundWorker worker = null)
        {
            worker?.ReportProgress(0, "Proveravam projekat.");
            var errors = CheckSavingErrors();

            worker?.ReportProgress(5, "Čistim direktorijum.");
            DeleteProjectFiles();

            ProjectSingleton.ImagesToSaveOnClose.Clear();

            worker?.ReportProgress(10, "Čuvam podatke o projektu.");
            SaveProjectFile();

            worker?.ReportProgress(15, "Čuvam objekte.");
            SaveContents();
            Util.DeleteArrowSections(LearningOverview);

            worker?.ReportProgress(50, "Generišem mape objekata.");
            MapGenerator.GenerateDitaLAMSMap(this);
            MapGenerator.GenerateDitaPDFMap(this);

            worker?.ReportProgress(60, "Čuvam LAMS slajdove.");
            var lamsErrors = SaveLamsEditor(export,branching);

                        
            if (errors.Count > lamsErrors.Count)
            {
                worker?.ReportProgress(99,
                    "Lekcija je sačuvana! Postoje greške u lekciji." +
                    (lamsErrors.Count > 0 ? "\r\nPostoje greške u LAMS Designer-u." : ""));
            }
            else
            {                
                if (lamsErrors.Count > 0)
                {
                    worker?.ReportProgress(99, "Lekcija je sačuvana!" +
                        (lamsErrors.Count > 0 ? "\r\nPostoje greške u LAMS Designer-u." : ""));
                }
                else
                {
                    worker?.ReportProgress(100, "Lekcija je sačuvana!");
                }
            }
  
            DitaClipboard.saveCurrentState();
            return errors.Count+lamsErrors.Count;
        }

        public void ExportProject(bool branching, string exportPath, BackgroundWorker worker = null)
        {                                                                                        
                  ZipProject(branching,exportPath, worker);
                  worker?.ReportProgress(100, "Lekcija je uspešno eksportovana!");  
        }
        /// <summary>
        /// Metoda koja brise postojece fajlove iz projekta
        /// </summary>
        private void DeleteProjectFiles()
        {
            var files = Directory.GetFiles(ProjectDir, "*.dita");
            foreach (string file in files)
            {
                FileManager.DeleteFile(file);
            }
            var filemap = Directory.GetFiles(ProjectDir, "*.ditamap");
            foreach (string file in filemap)
            {
                FileManager.DeleteFile(file);
            }
            var archives = Directory.GetFiles(ProjectDir, "*.zip");
            foreach (string file in archives)
            {
                FileManager.DeleteFile(file);
            }
        }

        /// <summary>
        /// Cuva sve objekte u projektu u odgovarajuce fajlove.
        /// </summary>
        private void SaveContents()
        {
            string fileNameFormat = Path.Combine(ProjectDir, string.Format("{0}-{1}-pptl{2}.dita", CourseCode, LessonNumber, "{0}{1}"));
            string fileName;
            int i = 0;
            string xml;

            GenerateOverview();
            if (LearningOverview != null)
            {
                fileName = string.Format(fileNameFormat, 'o', "");
                LearningOverview toSerialize = (LearningOverview)LearningOverview.Clone();

                XmlSerializer serializer = new XmlSerializer(toSerialize.GetType());
                using (StringWriter textWriter = new StringWriter())
                {
                    serializer.Serialize(textWriter, toSerialize);
                   // Debug.WriteLine(textWriter.ToString().Substring(0,1000));
                }
                Debug.WriteLine("--------------------------------------------------");
                xml = Util.SerializeWithEncoding(toSerialize);
                //Debug.WriteLine(xml.Substring(0, 1000));
                File.WriteAllText(fileName, xml);
            }
            foreach (var learningContent in LearningContents)
            {
                if (learningContent.LearningBody.LcObjectives.LcObjectivesGroup == null)
                {
                    learningContent.LearningBody.LcObjectives.LcObjectivesGroup = new LcObjectivesGroup();
                    learningContent.LearningBody.LcObjectives.LcObjectivesGroup.LcObjective = new List<string>();
                }
                learningContent.LearningBody.LcObjectives.LcObjectivesGroup.LcObjective.Clear();
                AddLCObjectives(learningContent);
                fileName = string.Format(fileNameFormat, 'c', ++i);
                xml = Util.SerializeWithEncoding(learningContent);
                File.WriteAllText(fileName, xml);
                foreach (var learningSubContent in learningContent.SubObjects)
                {
                    if (learningSubContent.LearningBody.LcObjectives.LcObjectivesGroup == null)
                    {
                        learningSubContent.LearningBody.LcObjectives.LcObjectivesGroup = new LcObjectivesGroup();
                        learningSubContent.LearningBody.LcObjectives.LcObjectivesGroup.LcObjective = new List<string>();
                    }
                    learningSubContent.LearningBody.LcObjectives.LcObjectivesGroup.LcObjective.Clear();
                    AddLCObjectives(learningSubContent);
                    fileName = string.Format(fileNameFormat, 'c', ++i);
                    xml = Util.SerializeWithEncoding(learningSubContent);
                    File.WriteAllText(fileName, xml);
                }
            }
            if (LearningSummary != null)
            {
                fileName = string.Format(fileNameFormat, 's', ++i);
                xml = Util.SerializeWithEncoding(LearningSummary);
                File.WriteAllText(fileName, xml);
            }
        }
        /// <summary>
        /// Dodaje LCObjectives na objekat ili podobjekat
        /// </summary>
        /// <param name="content"></param>
        private void AddLCObjectives(LearningContent content)
        {
            content.LearningBody.LcObjectives.Title = content.Title;
            if (content.SubObjects.Any())
            {
                foreach (var subObject in content.SubObjects)
                {
                    content.LearningBody.LcObjectives.LcObjectivesGroup.LcObjective.Add(Util.EscapeXml(subObject.Title));
                }
            }
            else
            {
                foreach (var subSection in content.LearningContentBody.Sections)
                {
                    content.LearningBody.LcObjectives.LcObjectivesGroup.LcObjective.Add(Util.EscapeXml(subSection.Title));
                }
            }
        }
        public void saveActivity(AuthoringActivityDTO activity, string subDirectory, XmlWriterSettings settings, bool export = false)
        {
            if (activity.Tool == null)
            {
                return;
            }
            if (!Directory.Exists(subDirectory))
            {
                FileManager.CreateDir(subDirectory);
            }
            using (var writer = XmlWriter.Create(subDirectory + "\\tool.xml", settings))
            {
                    var noticeboard = activity.Tool as LamsNoticeboard;

                    //LOL
                    if (noticeboard != null)
                    {
                        noticeboard.LearningObject = noticeboard.LearningObject;
                    }

                    else
                    {
                        var qa = activity.Tool as LamsQa;
                        if (qa != null)
                        {
                            var list = qa.QaQueContents.QaQueContent;
                            for (int i = 0, cnt = list.Count; i < cnt; ++i)
                            {
                                list[i].DisplayOrder = $"{i + 1}";
                            }
                        }
                    }
                if (export)
                {
                    activity.Tool.ignoreDesignerParentIndex();
                }
                else
                {
                   activity.Tool.UpdateParentIndex();
                }
                
                new XmlSerializer(activity.Tool.GetType()).Serialize(writer, activity.Tool);
                
            }
        }
        private List<SavingError> SaveLamsEditor(bool export, bool branching)
        {        
        
            var design = new LearningDesignDTO(MainForm.Instance.grafikaPanel, this);
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };
            Util.createDir(ToolsDir);
            var exportToolDir = ToolsDir + "Lams";
            if (export)
            {
               Util.createDir(exportToolDir);
            }

            AuthoringActivityDTO aktivnost = new AuthoringActivityDTO();
            LamsNoticeboard lntb = new LamsNoticeboard();
            lntb.Title = "Map";
            lntb.ToolContentID = 999;
            aktivnost.Tool = lntb;
            aktivnost.Initialised = true;
            lntb.Parent = null;
   
            Debug.WriteLine("TOOL ID: " + design.Activities.List.First().ToolContentID);

            

           // design.Activities.List.Insert(1, aktivnost );

            foreach (var activity in design.Activities.List)
            {
                
                Debug.WriteLine(activity.ActivityTitle + " " + activity.ParentActivityId);
                

                string subDir = ToolsDir + "\\" + activity.Tool.ToolContentID;
                if (!Directory.Exists(subDir))
                {
                    FileManager.CreateDir(subDir);
                }
                if (activity.Tool.ToString().Contains("Image Gallery"))
                {
                    LamsImageGallery galerija = (LamsImageGallery)activity.Tool;

                    foreach (LamsImageGallery.ImageGalleryItem item in galerija.ImageGalleryItems.ImageGalleryItem)
                    {
                        if (galerija.ImageGalleryItems.ImageGalleryItem.Count > 0)
                        {
                            if (!(item.imagePath == null))
                            {
                                int imgPos = item.imagePath.LastIndexOf("\\") + 1;
                                string imgFileName = item.imagePath.Substring(imgPos, item.imagePath.Length - imgPos);
                                string imgPath = ProjectDir + "\\resources\\imagegallery\\" + imgFileName;
                                //Console.WriteLine(imgPath + "imagee path " + item.imagePath + " Sub dir " + subDir);
                                item.imagePath = imgPath;
                                File.Copy(imgPath, Path.Combine(@subDir + "\\", item.OriginalFileUuid));
                                File.Copy(imgPath, Path.Combine(@subDir + "\\", item.MediumFileUuid));
                                File.Copy(imgPath, Path.Combine(@subDir + "\\", item.ThumbnailFileUuid));
                            }
                        }
                    }
                }
                saveActivity(activity, subDir, settings, false);
                //export zip
                if (export)
                {
                    subDir = exportToolDir + "\\" + activity.Tool.ToolContentID;
                    saveActivity(activity, subDir, settings, export);

                    if (activity.Tool.ToString().Contains("Image Gallery"))
                    {
                        LamsImageGallery galerija = (LamsImageGallery)activity.Tool;

                        foreach (LamsImageGallery.ImageGalleryItem item in galerija.ImageGalleryItems.ImageGalleryItem)
                        {
                            if (galerija.ImageGalleryItems.ImageGalleryItem.Count > 0)
                            {
                                if (!(item.imagePath == null))
                                {
                                    File.Copy(item.imagePath, Path.Combine(@subDir + "\\", item.OriginalFileUuid));
                                    File.Copy(item.imagePath, Path.Combine(@subDir + "\\", item.MediumFileUuid));
                                    File.Copy(item.imagePath, Path.Combine(@subDir + "\\", item.ThumbnailFileUuid));
                                    item.ignoreImagePath();
                                }
                            }
                        }
                        saveActivity(activity, subDir, settings, export);
     
                    }

                    if (activity.GrafikaObject is LamsQa)
                    {
                        subDir = exportToolDir + "\\" + (activity.Tool.ToolContentID + 1);
                        saveActivity(activity, subDir, settings, export);
                    }                    
                }
            }
            if (!design.CanvasEmpty)
            {
                for (int i = 0; i < design.Activities.List.Count; ++i)
                {
                    var activity = design.Activities.List[i];
                    if (!activity.Initialised)
                    {
                        design.Activities.List.Remove(activity);
                        --i;
                    }
                }
            }
            using (var writer = XmlWriter.Create(ToolsDir + "\\learning_design.xml", settings))
            {
                new XmlSerializer(typeof (LearningDesignDTO)).Serialize(writer, design);
            }

            if (export && !branching)
            {
                using (var writer = XmlWriter.Create(exportToolDir + "\\learning_design.xml", settings))
                {
                    new XmlSerializer(typeof(LearningDesignDTO)).Serialize(writer, design);
                }
            }

            var toolsFile = ProjectDir + "\\tools.xml";

            if (File.Exists(toolsFile))
            {
                FileManager.DeleteFile(toolsFile);
            }
            return design.Errors;
        }
        /// <summary>
        /// Čuva sadrzaj svih Xml alata po objektu
        ///  i čuva aktivnosti dodate posle zaključka
        /// </summary>
        private void SaveXmlToolContent()
        {
            //DeleteToolsAndCreateDirectory();
            int idTool = 1;
            LamsToolFileList tools = new LamsToolFileList();
            foreach (LearningContent learningContent in LearningContents)
            {
                LamsToolFileInfo objectLearning = new LamsToolFileInfo();
                objectLearning.Id = learningContent.Id;
                foreach (object obj in learningContent.ToolList)
                {
                    objectLearning.Tool.Add(new LamsSavingTool("item" + idTool + ".xml"));
                    SerializeOneItemToFile(obj, idTool);
                    idTool++;
                }
                tools.ToolFileInfo.Add(objectLearning);
            }
            LamsToolFileInfo objectLearningSum = new LamsToolFileInfo();
            objectLearningSum.Id = "summary";
            foreach (object obj in LearningSummary.ToolList)
            {
                objectLearningSum.Tool.Add(new LamsSavingTool("item" + idTool + ".xml"));
                SerializeOneItemToFile(obj, idTool);
                idTool++;
            }
            tools.ToolFileInfo.Add(objectLearningSum);
            XmlSerializer writer = new XmlSerializer(typeof(LamsToolFileList));
            XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = false,
                Encoding = Encoding.UTF8
            };
            var path = ProjectDir + "\\tools.xml";

            using (XmlWriter xmlWriter = XmlWriter.Create(path, xmlWriterSettings))
            {
                writer.Serialize(xmlWriter, tools);
            }
        }
        /// <summary>
        /// Kreira ili briše sadržaj postojećeg Tool direktorijuma
        /// </summary>
        private void DeleteToolsAndCreateDirectory()
        {
            string toolsDir = ToolsDir;
            if (Directory.Exists(toolsDir))
            {
                FileManager.DeleteDir(toolsDir);
            }
            FileManager.CreateDir(toolsDir);
        }
        /// <summary>
        /// Briše postojeci ZIP projekta
        /// </summary>
        private void DeleteZipFile()
        {
            string zipPath = (ProjectDir + "\\" + CourseCode + "-" + LessonNumber + ".zip");
            if (File.Exists(zipPath))
            {
                FileManager.DeleteFile(zipPath);
            }
        }
        /// <summary>
        /// Metoda pravi novi ZIP projekta
        /// </summary>
        /// 
        private void ZipProject(bool branching, string zipPath = null, BackgroundWorker worker = null)
        {
            worker?.ReportProgress(60, "Kreiram privremeni direktorijum.");
            string tmpToZipParent = ProjectDir + "\\tmp";
            string tmpToZip = tmpToZipParent + "\\";

            Util.DirectoryCopy(ProjectDir, tmpToZip, true, new List<string> {"resources"});

            Directory.Delete(ProjectDir + "\\toolsLams",true);
            ZipFile.CreateFromDirectory(tmpToZip + "\\toolsLams", tmpToZip + "\\" + CourseCode + "-" + LessonNumber + "-LAMS.zip");
           
            try
            {
                string zipName = CourseCode + "-" + LessonNumber + ".zip";

                if (zipPath == null)
                {
                    zipPath = ProjectDir + "\\" + zipName;
                }


                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    OmitXmlDeclaration = false,
                    Encoding = Encoding.UTF8
                };
                if (branching)
                {
                    LearningDesignDTO design = new LearningDesignDTO(MainForm.Instance.grafikaPanel, null, false, true);

                    Debug.WriteLine(design.Activities.List.Count);

                    var lamsDesignerPath = tmpToZip + "\\toolsLams\\learning_design.xml";

                    if (File.Exists(lamsDesignerPath))
                    {
                        File.Delete(lamsDesignerPath);
                    }

                    if (File.Exists(lamsDesignerPath))
                    {
                        FileManager.DeleteFile(lamsDesignerPath);
                    }
                    using (var writer = XmlWriter.Create(lamsDesignerPath, settings))
                    {
                        new XmlSerializer(typeof(LearningDesignDTO)).Serialize(writer, design);
                    }
                    Util.AddFileToZipWithReplace(tmpToZip + "\\" + CourseCode + "-" + LessonNumber + "-LAMS.zip", lamsDesignerPath);

                    MainForm.Instance.grafikaPanel.Canvas.removeSequenceChoosingTmpElements();

                }
                Directory.Delete(tmpToZip + "\\toolsLams", true);
                
                worker?.ReportProgress(65, "Brišem postojeći ZIP fajl.");
                if (File.Exists(zipPath))
                {
                    FileManager.DeleteFile(zipPath);
                }
                worker?.ReportProgress(70, "Kreiram novi ZIP fajl.");
                for (int i = 0; ; ++i)
                {
                    try
                    {
                        ZipFile.CreateFromDirectory(tmpToZipParent, zipPath);
                        string resourcesPath = ProjectDir + "\\resources";
                        if (Directory.Exists(resourcesPath))
                        {                           
                            Util.AddFolderToZip(zipPath, resourcesPath, new DirectoryInfo(resourcesPath).Name, true);
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (i > 100)
                        {
                            FileManager.DeleteDir(tmpToZipParent, true);
                            if (File.Exists(zipPath))
                            {
                                FileManager.DeleteFile(zipPath);
                            }
                            throw new Exception("Trenutno nije moguće napraviti Zip projekta.", ex);
                        }
                    }
                }
                worker?.ReportProgress(90, "Brišem privremeni direktorijum.");
            }
            catch (Exception)
            {
                FileManager.DeleteDir(tmpToZipParent, true);
                throw;
            }
            finally
            {
               FileManager.DeleteDir(tmpToZipParent, true);
            }
        }
    }
}
