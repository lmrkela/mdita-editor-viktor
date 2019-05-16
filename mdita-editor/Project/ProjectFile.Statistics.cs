using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using mDitaEditor.Dita;
using mDitaEditor.Lams;
using mDitaEditor.Utils;

namespace mDitaEditor.Project
{
    partial class ProjectFile
    {
        public class Statistics
        {
            public decimal WordCount { get; private set; }
            public decimal FigureCount { get; private set; }
            public decimal VideoCount { get; private set; }
            public decimal AudioCount { get; private set; }

            public decimal ObjectCount { get; private set; }
            public decimal SubobjectCount { get; private set; }
            public decimal SectionCount { get; private set; }
            public decimal ObjectsWithTestsCount { get; private set; }
            public decimal ObjectsWithTestsPercent { get; private set; }
            public decimal CountPracticeAndHomework { get; private set; }
            public decimal AssesmentCount { get; private set; }
            public decimal QaCount { get; private set; }
            public decimal McCount { get; private set; }
            public decimal SubmitFilesCount { get; private set; }
            public decimal ShareResorucesCount { get; private set; }
            public decimal ChatCount { get; private set; }
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
                WordCount = statistics.WordCount;
                FigureCount = statistics.FigureCount;
                VideoCount = statistics.VideoCount;
                AudioCount = statistics.AudioCount;

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
                NotebookCount = statistics.NotebookCount;
                NoticeboardCount = statistics.NoticeboardCount;
            }

            public string GenerateHtml(string title)
            {
                return "<tr> <td style='font-weight:bold'>" + title + "</td> "
                       + "<td>" + WordCount + "</td>"
                       + "<td>" + ObjectCount + "</td>"
                       + "<td>" + SubobjectCount + "</td>"
                       + "<td>" + SectionCount + "</td>"
                       + "<td>" + ObjectsWithTestsPercent + " %</td>"
                       + "<td>" + AudioCount + "</td>"
                       + "<td>" + FigureCount + "</td>"
                       + "<td>" + VideoCount + "</td>"
                       + "<td>" + QaCount + "</td>"
                       + "<td>" + ForumCount + "</td>"
                       + "<td>" + McCount + "</td>"
                       + "<td>" + SubmitFilesCount + "</td>"
                       + "<td>" + ShareResorucesCount + "</td>"
                       + "<td>" + AssesmentCount + "</td>"
                       + "<td>" + ChatCount + "</td>"
                       + "<td>" + GraderCount + "</td>"
                       + "<td>" + NotebookCount + "</td>"
                       + "<td>" + NoticeboardCount + "</td></tr>";
            }

            public Statistics Reset()
            {
                WordCount = 0;
                FigureCount = 0;
                VideoCount = 0;
                AudioCount = 0;

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
                    CountPracticeAndHomework = 0;
                    ObjectCount =  project.LearningContents.Count;
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
                    if (ObjectCount > 0)
                    {
                        if (CountPracticeAndHomework >= ObjectCount )
                        {
                            ObjectsWithTestsPercent = 100;
                        }
                        else {
                            ObjectsWithTestsPercent = (ObjectsWithTestsCount) / (ObjectCount - CountPracticeAndHomework) * 100;
                        }
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
                WordCount += statistics.WordCount;
                FigureCount += statistics.FigureCount;
                VideoCount += statistics.VideoCount;
                AudioCount += statistics.AudioCount;

                ObjectCount += statistics.ObjectCount;
                SubobjectCount += statistics.SubobjectCount;
                SectionCount += statistics.SectionCount;
                ObjectsWithTestsCount += statistics.ObjectsWithTestsCount;

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

                return this;
            }

            public Statistics Divide(int n)
            {
                WordCount /= n;
                FigureCount /= n;
                VideoCount /= n;
                AudioCount /= n;

                ObjectCount /= n;
                SubobjectCount /= n;
                SectionCount /= n;
                ObjectsWithTestsCount /= n;
                ObjectsWithTestsPercent = ObjectsWithTestsCount / ObjectCount * 100;

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

            private void CountWords(string text)
            {
                if (text == null)
                {
                    return;
                }
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
                WordCount += count;
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
                        if (draftcomment.Disposition == "Classification" &&
                            (draftcomment.Text.EndsWith("-V") || draftcomment.Text.EndsWith("-DZ")))
                        {
                            countWords = false;
                            if (content.Parent == null)
                            {
                                CountPracticeAndHomework++;
                            }
                        }
                        CountWords(draftcomment.Text);
                    }
                    var objectives = content.LearningContentBody.LcObjectives;
                    //CountWords(objectives.Title);
                    CountWords(objectives.LcDescription);
                    if (objectives.LcObjectivesGroup != null)
                    {
                        foreach (var objective in objectives.LcObjectivesGroup.LcObjective)
                        {
                            CountWords(objective);
                        }
                    }
                }
                SectionCount += obj.LearningBody.Sections.Count;
                if (countWords)
                {
                    foreach (var section in obj.LearningBody.Sections)
                    {
                        CountSection(section);
                    }
                }
                if (obj.ToolList.Any(tool => tool is LamsJavaGrader || tool is LamsMultipleChoice || tool is LamsAssessment))
                {
                    ++ObjectsWithTestsCount;
                }
            }

            private static readonly Regex TextBoxRegex = new Regex(@"<[^>]*>|&nbsp;");

            private void CountSection(Section section)
            {
                //CountWords(section.Title);
                if (section.SectionDivs.Count == 0)
                {
                    return;
                }
                CountWords(section.SectionDivs[0].Content);
                foreach (var columnDiv in section.SectionDivs[section.SectionDivs.Count - 1].SectionDivs)
                {
                    if (columnDiv.SectionDivs.Count == 0)
                    {
                        continue;
                    }
                    foreach (var div in columnDiv.SectionDivs)
                    {
                        if (div.Outputclass.StartsWith("vp"))
                        {
                          
                            if (div.SectionDivs.Count > 0 &&  div.SectionDivs[0].Outputclass.Substring(0, 1) == "f" )
                            {
                                string text;
                                if (div.SectionDivs[0].SectionDivs.Count > 0)
                                {
                                    text = div.SectionDivs[0].SectionDivs[0].Content;
                                }
                                else
                                {
                                    text = div.SectionDivs[0].Content;
                                    if (text.Contains("<pre "))
                                    {
                                        text = Util.UnEscapeXml(text);
                                    }
                                }
                                var replaced = TextBoxRegex.Replace(text, "");
                                //_sb.AppendLine("REPLACING: " + text + "\nWITH: " + replaced);
                                CountWords(replaced);
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
                                    CountWords(xml.Root.Element("desc")?.Value);
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
                                    CountWords(replaced);
                                    break;
                                case "flexslider":
                                    try
                                    {
                                        var xml = XDocument.Parse(div.Content);
                                        if (xml.Root == null)
                                        {
                                            break;
                                        }
                                        foreach (var image in xml.Root.Elements("galleryimage"))
                                        {
                                            //CountWords(image.Attribute("title")?.Value);
                                            CountWords(image.Attribute("description")?.Value);
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
                    }
                    else if (tool is LamsForum)
                    {
                        ++ForumCount;
                    }
                    else if (tool is LamsMultipleChoice)
                    {
                        ++McCount;
                    }
                    else if (tool is LamsQa)
                    {
                        ++QaCount;
                    }
                    else if (tool is LamsShareResource)
                    {
                        ++ShareResorucesCount;
                    }
                    else if (tool is LamsSubmitFiles)
                    {
                        ++SubmitFilesCount;
                    }
                    else if (tool is LamsChat)
                    {
                        ++ChatCount;
                    }
                    else if (tool is LamsJavaGrader)
                    {
                        ++GraderCount;
                    }
                    else if (tool is LamsNoticeboard)
                    {
                        var noti = (LamsNoticeboard)tool;
                        if (noti.LearningObject == null)
                        {
                            ++NoticeboardCount;
                        }
                    }
                    else if (tool is LamsNotebook)
                    {
                        ++NotebookCount;
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
