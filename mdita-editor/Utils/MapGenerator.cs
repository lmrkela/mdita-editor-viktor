using mDitaEditor.Dita;
using mDitaEditor.Project;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using LearningContentList = mDitaEditor.Project.LearningContentList;

namespace mDitaEditor.Utils
{
    class MapGenerator
    {
        /// <summary>
        /// Metoda koja generise sadrzaj mape za PDF
        /// </summary>
        /// <returns></returns>
        private static string GetPDFmapContent(ProjectFile project)
        {
            LearningContentList objects = project.LearningContents;
            string lesson = project.CourseCode + "-" + project.LessonNumber;
            string fileBase = lesson + "-pptlc";
            StringBuilder strBuild = new StringBuilder("");
            strBuild.AppendLine("<?xml version= \"1.0\" encoding =\"UTF-8\"?>");
            strBuild.AppendLine("<!DOCTYPE map PUBLIC \"-//OASIS//DTD DITA 1.2 Learning Map//EN\" \"learningMap.dtd\">");
            strBuild.Append("<map title=\"" + lesson + "-PDF\" chunk=\"to-content\">\n" +
                            "<title>" + lesson + "</title>\n" +
                            "<learningObject navtitle = \"LearningObject\">\n" +
                            "<learningOverviewRef href=\"" + (lesson + "-pptlo.dita") + "\" navtitle=\"LearningOverview\" chunk=\"by-document\"/>\n");

            // dodavanje tagova za objekate i podobjekate u mapu
            int objCounter = 1;
            foreach (LearningContent obj in objects)
            {
                if (obj.SubObjects.Count == 0)
                {
                    strBuild.Append("<learningContentRef href=\"" + fileBase + objCounter + ".dita" + "\" navtitle=\"LearningContent\" chunk=\"by-document\"/>\n");
                    objCounter++;
                }
                else
                {
                    strBuild.Append("<learningContentRef href=\"" + fileBase + objCounter + ".dita" + "\" navtitle=\"LearningContent\" chunk=\"by-document\">\n");
                    objCounter++;
                    foreach (LearningContent subobj in obj.SubObjects)
                    {
                        strBuild.Append("\t<learningContentComponentRef chunk=\"by-document\" href=\"" + fileBase + objCounter + ".dita" + "\" navtitle = \"LearningContent\"/>\n");
                        objCounter++;
                    }
                    strBuild.Append("</learningContentRef>\n");
                }
            }
            strBuild.Append("<learningSummaryRef href=\"" + lesson + "-pptls" + objCounter + ".dita" + "\" navtitle=\"LearningContent\" chunk=\"by-document\" />");
            strBuild.Append(" </learningObject>\n</map>");
            return strBuild.ToString();
        }


        /// <summary>
        /// Metoda koja generise sadrzaj mape za LAMS
        /// </summary>
        /// <returns></returns>
        private static string GetLAMSmapContent(ProjectFile project)
        {
            LearningContentList objects = project.LearningContents;
            string lesson = project.CourseCode + "-" + project.LessonNumber;
            string fileBase = lesson + "-pptlc";
            StringBuilder strBuild = new StringBuilder("");
            strBuild.AppendLine("<?xml version= \"1.0\" encoding =\"UTF-8\"?>");
            strBuild.AppendLine("<!DOCTYPE map PUBLIC \"-//OASIS//DTD DITA 1.2 Learning Map//EN\" \"learningMap.dtd\">");
            strBuild.Append("<map title=\"" + lesson + "\">\n" +
                            "<title>" + lesson + "</title>\n" +
                            "<learningObject navtitle = \"LearningObject\">\n" +
                            "<learningOverviewRef href=\"" + (lesson + "-pptlo.dita") + "\" navtitle=\"LearningOverview\"/>\n");

            // dodavanje tagova za objekate i podobjekate u mapu
            int objCounter = 1;
            foreach (LearningContent obj in objects)
            {
                strBuild.Append("<learningContentRef href=\"" + fileBase + objCounter + ".dita" + "\" navtitle=\"LearningContent\"/>\n");
                objCounter++;
                foreach (LearningContent subobj in obj.SubObjects)
                {
                    strBuild.Append("<learningContentRef href=\"" + fileBase + objCounter + ".dita" + "\" navtitle=\"LearningContent\"/>\n");
                    objCounter++;
                }
            }

            strBuild.Append("<learningSummaryRef href=\"" + lesson + "-pptls" + objCounter+ ".dita" + "\" navtitle=\"LearningSummary\"/>");
            strBuild.Append(" </learningObject>\n</map>");
            return strBuild.ToString();
        }

        /// <summary>
        /// Metoda koja kreira ditamap fajl za PDF i u nju upisuje odgovarajuci sadrzaj mape
        /// </summary>
        public static void GenerateDitaPDFMap(ProjectFile project)
        {
            string path = project.ProjectDir + "\\" + project.CourseCode + "-" + project.LessonNumber;            
            File.WriteAllText(path + "-PDF" + ".ditamap", GetPDFmapContent(project));
        }

        /// <summary>
        /// Metoda koja kreira ditamap fajl za LAMS i u nju upisuje odgovarajuci sadrzaj mape
        /// </summary>
        public static void GenerateDitaLAMSMap(ProjectFile project)
        {
            string path = project.ProjectDir + "\\"+ project.CourseCode + "-" + project.LessonNumber;            
            File.WriteAllText(path + "LAMS" + ".ditamap", GetLAMSmapContent(project));
        }
    }


}
