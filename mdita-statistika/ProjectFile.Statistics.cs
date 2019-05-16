using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using StatistikaProjekata.DITA;
using StatistikaProjekata.LAMS;

namespace StatistikaProjekata
{
    partial class ProjectFile
    {
        public class Statistics
        {
            public string Predmet { get; private set; }
            public string Lekcija { get; private set; }

            public decimal WordCount;
            public decimal FigureCount { get; private set; }
            public decimal VideoCount { get; private set; }
            public decimal AudioCount { get; private set; }

            public decimal SnippetWords;
            public decimal LatexWords;
            public decimal ObjectCount { get; private set; }
            public decimal SubobjectCount { get; private set; }
            public decimal SectionCount { get; private set; }
            public decimal ObjectsWithTestsCount { get; private set; }
            public decimal ObjectsWithTestsPercent { get; set; }
            public decimal CountPracticeAndHomework { get; private set; }
            public decimal AssesmentCount { get; private set; }
            public decimal QaCount { get; private set; }
            public decimal McCount { get; private set; }

            public decimal Fin2Count { get; private set; }

            public decimal Fin1Count { get; private set; }
            public decimal SubmitFilesCount { get; private set; }
            public decimal ShareResorucesCount { get; private set; }
            public decimal ChatCount { get; private set; }
            public decimal GalleryCount { get; private set; }
            public decimal GraderCount { get; private set; }
            public decimal ForumCount { get; private set; }
            public decimal NotebookCount { get; private set; }
            public decimal NoticeboardCount { get; private set; }

            //private StringBuilder _sb = new StringBuilder();

            public Statistics()
            { }

            public Statistics(ProjectFile project)
            {
                UpdateStats(project);
            }

            public Statistics(Statistics statistics)
            {
                Predmet = statistics.Predmet;
                Lekcija = statistics.Lekcija;

                WordCount = statistics.WordCount;
                SnippetWords = statistics.SnippetWords;
                LatexWords = statistics.LatexWords;
                FigureCount = statistics.FigureCount;
                VideoCount = statistics.VideoCount;
                AudioCount = statistics.AudioCount;
                GalleryCount = statistics.GalleryCount;
                ObjectCount = statistics.ObjectCount;
                SubobjectCount = statistics.SubobjectCount;
                SectionCount = statistics.SectionCount;
                ObjectsWithTestsCount = statistics.ObjectsWithTestsCount;
                ObjectsWithTestsPercent = statistics.ObjectsWithTestsPercent;

                AssesmentCount = statistics.AssesmentCount;
                QaCount = statistics.QaCount;
                McCount = statistics.McCount;
                SubmitFilesCount = statistics.SubmitFilesCount;
                ShareResorucesCount = statistics.ShareResorucesCount;
                ChatCount = statistics.ChatCount;
                GraderCount = statistics.GraderCount;
                ForumCount = statistics.ForumCount;
                Fin2Count = statistics.Fin2Count;
                Fin1Count = statistics.Fin1Count;
                NotebookCount = statistics.NotebookCount;
                NoticeboardCount = statistics.NoticeboardCount;
            }
    
            public string GenerateHtml(string title)
            {
                return "<tr> <td style='font-weight:bold'>" + title + "</td> "
                       + "<td>" + WordCount.ToString("0.##") + "</td>"
                       + "<td>" + ObjectCount.ToString("0.##") + "</td>"
                       + "<td>" + SubobjectCount.ToString("0.##") + "</td>"
                       + "<td>" + SectionCount.ToString("0.##") + "</td>"
                       + "<td>" + ObjectsWithTestsPercent.ToString("0.##") + " %</td>"
                       + "<td>" + AudioCount.ToString("0.##") + "</td>"
                       + "<td>" + FigureCount.ToString("0.##") + "</td>"
                       + "<td>" + VideoCount.ToString("0.##") + "</td>"
                       + "<td>" + QaCount.ToString("0.##") + "</td>"
                       + "<td>" + ForumCount.ToString("0.##") + "</td>"
                       + "<td>" + McCount.ToString("0.##") + "</td>"
                       + "<td>" + SubmitFilesCount.ToString("0.##") + "</td>"
                       + "<td>" + ShareResorucesCount.ToString("0.##") + "</td>"
                       + "<td>" + AssesmentCount.ToString("0.##") + "</td>"
                       + "<td>" + ChatCount.ToString("0.##") + "</td>"
                       + "<td>" + GraderCount.ToString("0.##") + "</td>"
                       + "<td>" + NotebookCount.ToString("0.##") + "</td>"
                       + "<td>" + NoticeboardCount.ToString("0.##") + "</td>"+
                       "<td>" + Fin2Count.ToString("0.##") + "</td>" +
                       "<td>" + Fin1Count.ToString("0.##") + "</td>" +
                        "<td>" + GalleryCount.ToString("0.##") + "</td>" +
                        "<td>" + SnippetWords.ToString("0.##") + "</td>" +
                        "<td>" + LatexWords.ToString("0.##") + "</td>" +
                      " </tr>";
            }

            public Statistics Reset()
            {
                Predmet = "";
                Lekcija = "";

                WordCount = 0;
                FigureCount = 0;
                VideoCount = 0;
                AudioCount = 0;
                SnippetWords = 0;
                LatexWords = 0;
                ObjectCount = 0;
                SubobjectCount = 0;
                SectionCount = 0;
                ObjectsWithTestsCount = 0;
                ObjectsWithTestsPercent = 0;

                AssesmentCount = 0;
                QaCount = 0;
                McCount = 0;
                SubmitFilesCount = 0;
                ShareResorucesCount = 0;
                ChatCount = 0;
                GraderCount = 0;
                ForumCount = 0;
                NotebookCount = 0;
                NoticeboardCount = 0;

                return this;
            }

            public void UpdateStats(ProjectFile project)
            {
                //_sb.Clear();
                Reset();
                if (project != null)
                {
                    Predmet = project.CourseCode;
                    Lekcija = project.LessonNumber;

                    CountPracticeAndHomework = 0;
                    ObjectCount = project.LearningContents.Count;
                    CountObject(project.LearningOverview);
                    if (ObjectCount > 0)
                    {
                        foreach (var content in project.LearningContents)
                        {
                            CountObject(content);
                            SubobjectCount += content.SubObjects.Count;
                            foreach (var subObject in content.SubObjects)
                            {
                                CountObject(subObject);
                            }
                        }
                    }
                    SubobjectCount += ObjectCount;
                    CountObject(project.LearningSummary);
                    WordCount += LatexWords;
                    if (ObjectCount > 0)
                    {
                        ObjectsWithTestsPercent = (ObjectsWithTestsCount) / (ObjectCount) * 100;
                    }
                    else
                    {
                        ObjectsWithTestsPercent = 100;
                    }
                    CountLamsTools(project.LamsTools);
                }
                //Directory.CreateDirectory("log");
                //File.WriteAllText("log\\statistics" + DateTime.Now.Ticks + ".txt", _sb.ToString());
            }

            public Statistics Add(Statistics statistics)
            {
                Predmet = statistics.Predmet;

                WordCount += statistics.WordCount;
                SnippetWords += statistics.SnippetWords;
                LatexWords += statistics.LatexWords;
                FigureCount += statistics.FigureCount;
                VideoCount += statistics.VideoCount;
                GalleryCount += statistics.GalleryCount;
                AudioCount += statistics.AudioCount;

                ObjectCount += statistics.ObjectCount;
                SubobjectCount += statistics.SubobjectCount;
                SectionCount += statistics.SectionCount;
                ObjectsWithTestsCount += statistics.ObjectsWithTestsCount;
                ObjectsWithTestsPercent += ObjectsWithTestsPercent;
                AssesmentCount += statistics.AssesmentCount;
                QaCount += statistics.QaCount;
                McCount += statistics.McCount;
                SubmitFilesCount += statistics.SubmitFilesCount;
                ShareResorucesCount += statistics.ShareResorucesCount;
                ChatCount += statistics.ChatCount;
                GraderCount += statistics.GraderCount;
                ForumCount += statistics.ForumCount;
                NotebookCount += statistics.NotebookCount;
                NoticeboardCount += statistics.NoticeboardCount;
                Fin2Count += statistics.Fin2Count;
                Fin1Count += statistics.Fin1Count;
                return this;
            }

            public Statistics Divide(int n)
            {
                WordCount /= n;
                LatexWords /= n;
                SnippetWords /= n;
                FigureCount /= n;
                VideoCount /= n;
                GalleryCount /= n;
                AudioCount /= n;

                ObjectCount /= n;
                SubobjectCount /= n;
                SectionCount /= n;
                ObjectsWithTestsCount /= n;

                Fin2Count /= n;
                Fin1Count /= n;
                AssesmentCount /= n;
                QaCount /= n;
                McCount /= n;
                SubmitFilesCount /= n;
                ShareResorucesCount /= n;
                ChatCount /= n;
                GraderCount /= n;
                ForumCount /= n;
                NotebookCount /= n;
                NoticeboardCount /= n;

                return this;
            }

            private void CountWords(string text, ref decimal TypeCount)
            {

                if (text == null)
                {
                    return;
                }
                text = System.Text.RegularExpressions.Regex.Replace(text, @"\s{2,}", " "); ;
                int index = 0;
                int count = 0;
                // skip starting whitespaces
                while (index < text.Length && char.IsWhiteSpace(text[index]))
                {
                    index++;
                }
                while (index < text.Length)
                {
                    // check if current char is part of a word
                    while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    {
                        index++;
                    }
                    ++count;
                    // skip whitespace until next word
                    while (index < text.Length && char.IsWhiteSpace(text[index]))
                    {
                        index++;
                    }
                }
                TypeCount += count;
                //_sb.AppendLine(count + " WORDS IN: " + text);
            }

            private void CountObject(LearningBase obj)
            {
                bool countWords = true;
                var content = obj as LearningContent;
                if (content != null)
                {
                    //CountWords(content.Title);
                    foreach (var draftcomment in content.Shortdesc.Draftcomment)
                    {
                        if (draftcomment.Disposition == "Classification")
                        if (draftcomment.Text.Trim().EndsWith("-V") || draftcomment.Text.Trim().EndsWith("-DZ"))
                        {
                            --ObjectCount;
                            ++CountPracticeAndHomework;
                        }
                        CountWords(draftcomment.Text,ref WordCount);
                    }
                    var objectives = content.LearningContentBody.LcObjectives;
                    //CountWords(objectives.Title);
                    CountWords(objectives.LcDescription,ref WordCount);
                    if (objectives.LcObjectivesGroup != null)
                    {
                        foreach (var objective in objectives.LcObjectivesGroup.LcObjective)
                        {
                            CountWords(objective,ref WordCount);
                        }
                    }
                }
                SectionCount += obj.LearningBody.Section.Count;
                if (countWords)
                {
                    foreach (var section in obj.LearningBody.Section)
                    {
                        CountSection(section);
                    }
                }
                if (obj.ToolList.Any(tool => tool is Javagrader || tool is LamsMultipleChoice || tool is LamsAssessment || tool is LamsQA))
                {
                    ++ObjectsWithTestsCount;
                }
            }

            private static readonly Regex TextBoxRegex = new Regex(@"<[^>]*>|&nbsp;");

            private void CountSection(Section section)
            {
                //CountWords(section.Title);
                if (section.Sectiondiv.Count == 0)
                {
                    return;
                }
                CountWords(section.Sectiondiv[0].Content,ref WordCount);
                foreach (var columnDiv in section.Sectiondiv[section.Sectiondiv.Count - 1].SectionDiv)
                {
                    if (columnDiv.SectionDiv.Count == 0)
                    {
                        continue;
                    }
                    foreach (var div in columnDiv.SectionDiv)
                    {
                        if (div.Outputclass.StartsWith("vp"))
                        {

                            if (div.SectionDiv.Count > 0 && div.SectionDiv[0].Outputclass.Substring(0, 1) == "f")
                            {
                                string text;
                                if (div.SectionDiv[0].SectionDiv.Count > 0)
                                {
                                    text = div.SectionDiv[0].SectionDiv[0].Content;
                                    var replaced = TextBoxRegex.Replace(text, "");
                                    CountWords(replaced, ref WordCount);
                                }
                                else
                                {
                                    text = div.SectionDiv[0].Content;
                                    if (text.Contains("<pre "))
                                    {
                                        text = Util.UnEscapeXml(text);
                                        var replaced = TextBoxRegex.Replace(text, "");
                                        CountWords(replaced, ref SnippetWords);
                                    }
                                    else
                                    {

                                        var replaced = TextBoxRegex.Replace(text, "");
                                        CountWords(replaced, ref WordCount);
                                    }
                                }
                                //_sb.AppendLine("REPLACING: " + text + "\nWITH: " + replaced);
                            }
                            else if (div.Content.Contains("<fig>"))
                            {
                                try
                                {
                                    var xml = XDocument.Parse(div.Content);
                                    if (xml.Root == null)
                                    {
                                        continue;
                                    }
                                    //CountWords(xml.Root.Element("title")?.Value);
                                    CountWords(xml.Root.Element("desc")?.Value,ref WordCount);
                                    ++FigureCount;
                                }
                                catch (XmlException)
                                {
                                }
                            }
                        }
                        else
                        {
                            switch (div.Outputclass)
                            {
                                case "latex":
                                    var text = div.Content;
                                    var replaced = TextBoxRegex.Replace(text, "");
                                    //_sb.AppendLine("REPLACING: " + text + "\nWITH: " + replaced);
                                    CountWords(replaced,ref LatexWords);
                                    break;
                                case "flexslider":
                                    try
                                    {
                                        var xml = XDocument.Parse(div.Content);
                                        if (xml.Root == null)
                                        {
                                            break;
                                        }
                                        GalleryCount++;
                                        foreach (var image in xml.Root.Elements("galleryimage"))
                                        {
                                            //CountWords(image.Attribute("title")?.Value);
                                            CountWords(image.Attribute("description")?.Value,ref WordCount);
                                            ++FigureCount;
                                        }
                                    }
                                    catch (XmlException)
                                    {
                                    }
                                    break;
                                case "video":
                                case "youtube":
                                    ++VideoCount;
                                    break;
                                case "audio":
                                    ++AudioCount;
                                    break;
                            }
                        }
                    }
                }
            }

            private void CountLamsTools(List<LamsTool> tools)
            {
                foreach (var tool in tools)
                {
                    if (tool is LamsAssessment)
                    {
                        ++AssesmentCount;
                        ++Fin1Count;
                    }
                    else if (tool is LamsForum)
                    {
                        ++ForumCount;
                        ++Fin2Count;
                    }
                    else if (tool is LamsMultipleChoice)
                    {
                        ++McCount;
                        ++Fin1Count;
                    }
                    else if (tool is LamsQA)
                    {
                        ++QaCount;
                        ++Fin1Count;
                    }
                    else if (tool is LamsShareResource)
                    {
                        ++ShareResorucesCount;
                        ++Fin2Count;
                    }
                    else if (tool is LamsSubmitFiles)
                    {
                        ++SubmitFilesCount;
                        ++Fin2Count;
                    }
                    else if (tool is LamsChat)
                    {
                        ++ChatCount;
                        ++Fin2Count;
                    }
                    else if (tool is Javagrader)
                    {
                        ++GraderCount;
                        ++Fin1Count;
                    }
                    else if (tool is LamsNoticeboard)
                    {
                        var noti = (LamsNoticeboard)tool;
                        if (noti.LearningObject == null)
                        {
                            ++NoticeboardCount;
                            ++Fin2Count;
                        }
                    }
                    else if (tool is LamsNotebook)
                    {
                        ++NotebookCount;
                        ++Fin2Count;
                    }
                }
            }
        }

        public Statistics GetStatistics()
        {
            return new Statistics(this);
        }
    }
}
