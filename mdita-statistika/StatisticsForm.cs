using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata
{
    public partial class StatisticsForm : Form
    {
        decimal totalWordsDiff = 0;
        decimal totalObjectsDiff = 0;
        decimal totalObjectsAndSubDiff = 0;
        decimal totalFiguresDiff = 0;
        decimal totalSectionsDiff = 0;
        decimal totalWords = 0;
        decimal totalObjects = 0;
        decimal totalObjectsAndSub = 0;
        decimal totalFigures = 0;
        decimal totalSnippet = 0;
        decimal totalSnippetDiff = 0;
        decimal totalSections = 0;

        public StatisticsForm()
        {
            InitializeComponent();
        }
        private void LoadProjectStep(string file)
        {
            var project = ProjectSingleton.LoadFile(file);
            foreach (ProjectFile projectL in lvProjects.Items)
            {
                if (project.CourseCode == projectL.CourseCode && project.LessonNumber == projectL.LessonNumber)
                {
                    MessageBox.Show($"{project.CourseCode}-{project.LessonNumber} je već učitan. ({lvProjects.Items.IndexOf(projectL)})" +
                                    $"\nFajl: {file}");
                    return;
                }
            }

            lvProjects.Items.Add(project);
            statTotal.Statistics = statTotal.Statistics.Add(project.GetStatistics());
            //lvProjects.SelectedItem = project;         
            statAverage.Statistics = new ProjectFile.Statistics(statTotal.Statistics).Divide(lvProjects.Items.Count);
            statTotal.Statistics.ObjectsWithTestsPercent = statAverage.Statistics.ObjectsWithTestsPercent;


        }

        public void LoadStepTwo()
        {
            LoadTotalStatistics();

            if (listOldProjects.SelectedItem != null)
            {
                ProjectFile projecnewt = listOldProjects.SelectedItem as ProjectFile;
                LoadOneStatistics(projecnewt);
            }

            decimal sum = 0;
            foreach (ProjectFile file in lvProjects.Items)
            {
                sum += file.GetStatistics().ObjectsWithTestsPercent;
            }


            if (lvProjects.Items.Count > 0)
            {
                sum /= lvProjects.Items.Count;
            }
            statAverage.Statistics.ObjectsWithTestsPercent = sum;
            Console.WriteLine("Ima ukupno : " + sum);
        }

        private void LoadProject(string file)
        {
            var project = ProjectSingleton.LoadFile(file);
            foreach (ProjectFile projectL in lvProjects.Items)
            {
                if (project.CourseCode == projectL.CourseCode && project.LessonNumber == projectL.LessonNumber)
                {
                    MessageBox.Show("Postoji vec ova lekcija ucitana za statistiku");
                    return;
                }
            }

            lvProjects.Items.Add(project);
            lvProjects.SelectedItem = project;
            statTotal.Statistics = statTotal.Statistics.Add(project.GetStatistics());

            statAverage.Statistics = new ProjectFile.Statistics(statTotal.Statistics).Divide(lvProjects.Items.Count);
            statTotal.Statistics.ObjectsWithTestsPercent = statAverage.Statistics.ObjectsWithTestsPercent;
            LoadTotalStatistics();

            if (listOldProjects.SelectedItem != null)
            {
                ProjectFile projecnewt = listOldProjects.SelectedItem as ProjectFile;
                LoadOneStatistics(projecnewt);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "mDITA projekat (*.mdita)|*.mdita",
                FileName = "project.mdita"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadProject(dialog.FileName);
            }
        }

        private void lvProjects_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lvProjects_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                if (Directory.Exists(file))
                {
                    DirSearch(file);
                    LoadStepTwo();
                }
                else {
                    var path = Path.GetExtension(file);
                    if (path != null && path.Equals(".mdita"))
                    {
                        LoadProject(file);
                    }
                }
            }
        }

        private void lvProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            var project = lvProjects.SelectedItem as ProjectFile;
            if (project == null)
            {
                return;
            }
            statProject.Statistics = project.GetStatistics();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lvProjects.Items.Count == 0)
            {
                return;
            }

            var filename = "Statistika.xls";
            if (lvProjects.Items.Count > 0)
            {
                var project = lvProjects.Items[0] as ProjectFile;
                filename = project.CourseCode + "-Statistika.xls";
            }

            var fileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xls)|*.xls",
                FileName = filename
            };
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var toSave = "<table><tbody><tr><th>New lessons statistics</th></tr></tbody></table>" + "<table border='1' cellpadding='5' cellspacing='5'>" +
                        "<thead><th></th> <th>Words</th> <th>Objects</th> <th>Objects and subobjects</th> <th>Sections</th> <th>Tests after objects (FIN1)</th> <th>Audio</th> <th>Figures</th> <th>Videos (FMM) </th> <th>Q/A</th> <th>Forum</th> <th>Multiple Choice</th> <th>Submit files</th> <th>Shared resources</th> <th>Assessment</th> <th>Chat</th> <th>Javagrader</th> <th>Notebook</th> <th>Noticeboard</th> <th>Non-test activities (FIN2)</th><th>FIN1 count</th><th>Gallery count</th><th>Snippet words</th><th>Latex words</th> </thead>" +
                        "<tbody> " + statTotal.Statistics.GenerateHtml("TOTAL") + statAverage.Statistics.GenerateHtml("AVERAGE") + " </tbody> </table > ";
            toSave += "<table><tbody><tr><th>Old lessons statistics</th></tr></tbody></table>" + "<table border='1' cellpadding='5' cellspacing='5'><thead><tr><th>Total words</th><th>Total objects</th><th>Total objects and subobjects</th><th>Total sections</th><th>Total figures</th><th>Total snippet words</th></tr></thead><tbody>";
            toSave += $"<td>{totalWords:0.##}</td><td>{totalObjects:0.##}</td><td>{totalObjectsAndSub:0.##}</td><td>{totalSections:0.##}</td><td>{totalFigures:0.##}</td><td>{totalSnippet:0.##}</td>";
            toSave += "</tbody></table>";
            toSave += GetHtmlForComparation();

            File.WriteAllText(fileDialog.FileName, toSave);
            if (chbOpenFolder.Checked)
            {
                var folder = Path.GetDirectoryName(fileDialog.FileName);
                if (folder != null)
                {
                    Process.Start(folder);
                }
            }
        }

        public void LoadOneStatistics(ProjectFile file)
        {
            if (file == null)
            {
                return;
            }

            ProjectFile.Statistics stats = file.GetStatistics();
            ProjectFile fileForCompare = null;

            foreach (ProjectFile project in lvProjects.Items)
            {
                if (project.CourseCode == file.CourseCode && project.LessonNumber == file.LessonNumber)
                {
                    fileForCompare = project;
                    break;
                }
            }
            if (fileForCompare != null) {
                ProjectFile.Statistics statsCompare = fileForCompare.GetStatistics();
                txtWordsDiff.Text = $"Words difference: {-stats.WordCount + statsCompare.WordCount:0.##}";
                txtObjectsDiff.Text = $"Objects difference: {-stats.ObjectCount + statsCompare.ObjectCount:0.##}";
                txtObjAndSubDiff.Text = $"Objects and subobjects difference: {-stats.SubobjectCount + statsCompare.SubobjectCount:0.##}";
                txtSectionDiff.Text = $"Words difference: {-stats.SectionCount + statsCompare.SectionCount:0.##}";
                txtFigDiff.Text = $"Words difference: {-stats.FigureCount + statsCompare.FigureCount:0.##}";
            }
        }

        public void LoadTotalStatistics()
        {
            totalWordsDiff = 0;
            totalObjectsDiff = 0;
            totalObjectsAndSubDiff = 0;
            totalFiguresDiff = 0;
            totalSectionsDiff = 0;
            totalWords = 0;
            totalObjects = 0;
            totalObjectsAndSub = 0;
            totalFigures = 0;
            totalSnippet = 0;
            totalSnippetDiff = 0;
            totalSections = 0;
            foreach (ProjectFile file in listOldProjects.Items)
            {
                var stats = file.GetStatistics();
                totalWords += stats.WordCount;
                totalWordsDiff -= stats.WordCount;
                totalObjects += stats.ObjectCount;
                totalObjectsDiff -= stats.ObjectCount;
                totalObjectsAndSub += stats.SubobjectCount;
                totalObjectsAndSubDiff -= stats.SubobjectCount;
                totalFigures += stats.FigureCount;
                totalFiguresDiff -= stats.FigureCount;
                totalSections += stats.SectionCount;
                totalSectionsDiff -= stats.SectionCount;
                totalSnippet += stats.SnippetWords;
                totalSnippetDiff -= stats.SnippetWords;
            }
            totalWordsDiff += statTotal.Statistics.WordCount;
            totalObjectsDiff += statTotal.Statistics.ObjectCount;
            totalObjectsAndSubDiff += statTotal.Statistics.SubobjectCount;
            totalFiguresDiff += statTotal.Statistics.FigureCount;
            totalSectionsDiff += statTotal.Statistics.SectionCount;
            totalSnippetDiff += statTotal.Statistics.SnippetWords;

            txtWordTotalDiff.Text = $"Words difference: {totalWordsDiff:0.##}";
            txtObjectsTotalDiff.Text = $"Objects difference: {totalObjectsDiff:0.##}";
            txtObjectsAndSubTotalDiff.Text = $"Objects and subobjects difference: {totalObjectsAndSubDiff:0.##}";
            txtSectionTotalDiff.Text = $"Section difference: {totalSectionsDiff:0.##}";
            txtFigureTotalDiff.Text = $"Figures difference: {totalFiguresDiff:0.##}";

            txtWordDiffPercent.Text = $"Words difference: {calculatePercentage(statTotal.Statistics.WordCount, totalWords):0.##}%";
            txtObjectDiffPercent.Text = $"Objects difference: {calculatePercentage(statTotal.Statistics.ObjectCount, totalObjects):0.##}%";
            txtObjAndSubDiffPercent.Text = $"Objects and subobjects difference: {calculatePercentage(statTotal.Statistics.SubobjectCount, totalObjectsAndSub):0.##}%";
            txtSectionDiffPerrcent.Text = $"Section difference: {calculatePercentage(statTotal.Statistics.SectionCount, totalSections):0.##}%";
            txtFigureDiffPercent.Text = $"Figures difference: {calculatePercentage(statTotal.Statistics.FigureCount, totalFigures):0.##}%";
            LoadLessonsWithoutHomework();
            LoadLessonsWithoutPractice();

            RepaintLabels();
        }

        public void LoadLessonsWithoutPractice()
        {
            String lessonsWithoutPractice = "Lessons without practice: ";
            foreach (ProjectFile project in lvProjects.Items)
            {
                bool foundPractice = false;
                foreach (LearningContent baseLearning in project.LearningContents) {
                    //CountWords(content.Title);
                    foreach (var draftcomment in baseLearning.Shortdesc.Draftcomment)
                    {
                        if (draftcomment.Disposition == "Classification" &&
                            (draftcomment.Text.EndsWith("-V")))
                        {
                            foundPractice = true;
                            break;
                        }
                    }
                }
                if (!foundPractice)
                {
                    lessonsWithoutPractice += project.CourseCode + "-" + project.LessonNumber + ", ";
                }
            }
            lblLessonWithoutPractice.Text = lessonsWithoutPractice;
        }

        public void LoadLessonsWithoutHomework()
        {
            String lessonsWithoutHomework = "Lessons without homework: ";
            foreach (ProjectFile project in lvProjects.Items)
            {
                bool foundPractice = false;
                foreach (LearningContent baseLearning in project.LearningContents)
                {
                    //CountWords(content.Title);
                    foreach (var draftcomment in baseLearning.Shortdesc.Draftcomment)
                    {
                        if (draftcomment.Disposition == "Classification" &&
                            (draftcomment.Text.EndsWith("-DZ")))
                        {
                            foundPractice = true;
                            break;
                        }
                    }
                }
                if (!foundPractice)
                {
                    lessonsWithoutHomework += project.CourseCode + "-" + project.LessonNumber + ", ";
                }
            }
            lblLessonsWithoutHomework.Text = lessonsWithoutHomework;
        }

        public String GetEachLessonComparation() {
            string html = "";

            foreach(ProjectFile project in lvProjects.Items){
                var statsNew = project.GetStatistics();
                var statsOld = (from ProjectFile projectO in listOldProjects.Items where projectO.CourseCode == project.CourseCode && projectO.LessonNumber == project.LessonNumber select projectO.GetStatistics()).FirstOrDefault();

                if(statsOld != null)
                {
                    html += "<table border='1'><tr colspan='10'><th>" + project.CourseCode + "-" + project.LessonNumber + "</th></tr></table>";
                    html += "<table border='1' cellpadding='5' cellspacing='5'>" +
                     "<thead><tr><th></th><th>Words</th> <th>Objects</th> <th>Objects and subobjects</th> <th>Sections</th> <th>Tests after objects</th> <th>Audio</th> <th>Figures</th> <th>Videos</th> <th>Q/A</th> <th>Forum</th> <th>Multiple Choice</th> <th>Submit files</th> <th>Shared resources</th> <th>Assessment</th> <th>Chat</th> <th>Javagrader</th> <th>Notebook</th> <th>Noticeboard</th><th>Non-test activities (FIN2)</th><th>FIN1 count</th><th>Gallery count</th><th>Snippet words</th><th>Latex words</th></tr> </thead>" +
                    statsOld.GenerateHtml("Old");
                    html += "<table border='1' cellpadding='5' cellspacing='5'>" +
                     statsNew.GenerateHtml("New");

                    html += "<table border='1' cellpadding='5' cellspacing='5'>";
                    html += "<tbody>";
                    html += "<tr>";

                    html += $"<td></td><td> {-statsOld.WordCount + statsNew.WordCount:0.##}</td><td>{-statsOld.ObjectCount + statsNew.ObjectCount:0.##}</td><td> {-statsOld.SubobjectCount + statsNew.SubobjectCount:0.##}</td><td> {-statsOld.SectionCount + statsNew.SectionCount:0.##}</td><td></td></td></td><td></td><td>{-statsOld.FigureCount + statsNew.FigureCount:0.##}</td><td>{-statsOld.SnippetWords + statsNew.SnippetWords:0.##}</td><td>{-statsOld.LatexWords + statsNew.LatexWords:0.##}</td>";

                    html += "</tr></tbody></table>";
                }
                else
                {
                    html += "<table border='1'><tr colspan='10'><th>" + project.CourseCode + "-" + project.LessonNumber + "</th></tr></table>";
                    html += "<table border='1' cellpadding='5' cellspacing='5'>" +
                    "<thead><tr><th></th><th>Words</th> <th>Objects</th> <th>Objects and subobjects</th> <th>Sections</th> <th>Tests after objects</th> <th>Audio</th> <th>Figures</th> <th>Videos</th> <th>Q/A</th> <th>Forum</th> <th>Multiple Choice</th> <th>Submit files</th> <th>Shared resources</th> <th>Assessment</th> <th>Chat</th> <th>Javagrader</th> <th>Notebook</th> <th>Noticeboard</th><th>Non-test activities (FIN2)</th><th>FIN1 count</th><th>Gallery count</th><th>Snippet words</th><th>Latex words</th></tr> </thead><tbody>" +
                     statsNew.GenerateHtml("New");
                    html += "</tbody></table>";
                }
            }
            return html;
        }

        public String GetHtmlForComparation()
        {
            string html = "<table><tbody><tr><th>Old lessons comparation</th></tr></tbody></table>";
            html += GetEachLessonComparation();

            html+=   "<table border='1'><tbody><tr><th>Lesson</th><th>Diff words</th><th>Diff objects</th><th>Diff obj and sub</th><th>Diff sections</th><th>Diff figures</th><th>Diff snipppet</th></tr>";
            
            html += $"<tr><td>Total</td><td>{calculatePercentage(statTotal.Statistics.WordCount, totalWords):0.##}% (FOB)</td>";
            html += $"<td>{calculatePercentage(statTotal.Statistics.ObjectCount, totalObjects):0.##}%</td>";
            html += $"<td>{calculatePercentage(statTotal.Statistics.SubobjectCount, totalObjectsAndSub):0.##}%</td>";
            html += $"<td>{calculatePercentage(statTotal.Statistics.SectionCount, totalSections):0.##}%</td>";
            html += $"<td>{calculatePercentage(statTotal.Statistics.FigureCount, totalFigures):0.##}%</td>";
            html += $"<td>{calculatePercentage(statTotal.Statistics.SnippetWords, totalSnippet):0.##}%</td></tr>";
            html += "</tbody></table>";

            decimal FPM = ((calculatePercentage(statTotal.Statistics.WordCount, totalWords)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.6);
            FPM += ((calculatePercentage(statTotal.Statistics.ObjectCount, totalObjects)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            FPM += ((calculatePercentage(statTotal.Statistics.ObjectCount, totalObjectsAndSub)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            FPM += ((calculatePercentage(statTotal.Statistics.SectionCount, totalSections)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            FPM += ((calculatePercentage(statTotal.Statistics.FigureCount, totalFigures)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            FPM += 100 / 100 * Convert.ToDecimal(0.05);
            FPM += 100 / 100 * Convert.ToDecimal(0.05);
            FPM += 100 / 100 * Convert.ToDecimal(0.05);
            FPM += 100 / 100 * Convert.ToDecimal(0.05);
            FPM *= 100;
            html += $"<table border='1'><tbody><tr><th>FPM</th><td>{FPM:0.##}%</td></tr></tbody></table>";

            return html;
        }

        public decimal calculatePercentage(decimal oldFigure, decimal newFigure)
        {
            decimal percentChange = 0;
            if ((oldFigure != 0) && (newFigure != 0))
            {
                percentChange = (1 - oldFigure / newFigure) * 100;
            }
            return -percentChange;
           
        }

        public void RepaintLabels()
        {
            foreach (Control groupbox in this.Controls)
            {
                if (!(groupbox is GroupBox)) continue;
                foreach (Control con in groupbox.Controls)
                {
                    int val = 0;
                    String z = "";
                    for (int i = 0; i < con.Text.Length; i++)
                    {
                        if (Char.IsDigit(con.Text[i]) || con.Text[i] == '-')
                            z += con.Text[i];
                    }
                    if (z.Length > 0)
                        val = int.Parse(z);
                    ChangeColors(con as Label, val);
                }
            }
        }

        public void ChangeColors(Label label, decimal value)
        {
            if (value < 0)
            {
                label.ForeColor = Color.Red;
            }
            else
            {
                label.ForeColor = Color.Green;
            }
        }

        public void loadOldProjectStep(String[] dialogFileName)
        {
            String[] fileName = Path.GetFileName(dialogFileName[0]).Split('-');
            String course = fileName[0];
            String lesson = "";
            if (fileName.Length > 3)
            {
                lesson = fileName[2];
            }
            else {
                lesson = fileName[1];
            }
            String schoolYear = "2015/2016";
            String author = "Metropolitan";
            String path = Path.GetDirectoryName(dialogFileName[0]);
            ProjectFile project = new ProjectFile(path, course, lesson, "Statistika lekcije " + course + " - " + lesson, schoolYear, author);
            foreach (ProjectFile projectL in listOldProjects.Items)
            {
                if (project.CourseCode == projectL.CourseCode && project.LessonNumber == projectL.LessonNumber)
                {
                    MessageBox.Show("Postoji vec ova lekcija ucitana za statistiku");
                    return;
                }
            }
            listOldProjects.Items.Add(project);
            foreach (String file in dialogFileName)
            {
                project.OpenContentFile(file, false);
            }
       

        }

        public void loadOldStepTwo()
        {
            LoadTotalStatistics();
            if (listOldProjects.Items.Count > 0)
            {
                if (listOldProjects.Items[0] != null)
                {
                    LoadOneStatistics(listOldProjects.Items[0] as ProjectFile);
                }
            }
            RepaintLabels();
        }

        public void loadOldProject(String[] dialogFileName)
        {
            String[] fileName = Path.GetFileName(dialogFileName[0]).Split('-');
            String course = fileName[0];
            String lesson = fileName[1];
            String schoolYear = "2015/2016";
            String author = "Metropolitan";
            String path = Path.GetDirectoryName(dialogFileName[0]);
            ProjectFile project = new ProjectFile(path, course, lesson, "Statistika lekcije " + course + " - " + lesson, schoolYear, author);
            foreach (ProjectFile projectL in listOldProjects.Items)
            {
                if (project.CourseCode == projectL.CourseCode && project.LessonNumber == projectL.LessonNumber)
                {
                    MessageBox.Show("Postoji vec ova lekcija ucitana za statistiku");
                    return;
                }
            }
            listOldProjects.Items.Add(project);
            foreach (String file in dialogFileName)
            {
                project.OpenContentFile(file, false);
            }
            LoadTotalStatistics();
            LoadOneStatistics(project);
            RepaintLabels();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "DITA files (*.dita)|*.dita",
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileNames.Length > 0)
                {
                    loadOldProject(dialog.FileNames);
                }
            }
        }

        private void listOldProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProjectFile project = listOldProjects.SelectedItem as ProjectFile;
            LoadOneStatistics(project);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DirSearch(dialog.SelectedPath);
                LoadStepTwo();
            }
        }

        public void DirSearch(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    foreach (string f in Directory.GetFiles(d))
                    {
                        if (f.EndsWith(".mdita"))
                        {
                            LoadProjectStep(f);
                        }
                    }
                   
                    DirSearch(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }

        public void DirSearchDITA(string sDir)
        {
            try
            {
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    List<string> listDita = new List<string>();
                    foreach (string f in Directory.GetFiles(d))
                    {
                        if (f.EndsWith(".dita"))
                        {
                            listDita.Add(f);
                        }
                    }
                    if(listDita.Count > 0)
                    {
                        loadOldProjectStep(listDita.ToArray());
                    }
                    DirSearchDITA(d);
                }
            }
            catch (System.Exception excpt)
            {
                Console.WriteLine(excpt.Message);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DirSearchDITA(dialog.SelectedPath);
                loadOldStepTwo();
            }
        }

        private void listOldProjects_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (var file in files)
            {
                if (Directory.Exists(file))
                {
                    DirSearchDITA(file);
                    loadOldStepTwo();
                }
            }
        }

        private void listOldProjects_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void StatisticsForm_Load(object sender, EventArgs e)
        {

        }

        private void btnExportOther_Click(object sender, EventArgs e)
        {
            if (lvProjects.Items.Count == 0)
            {
                return;
            }

            var filename = ((ProjectFile)lvProjects.Items[0]).CourseCode + "-Statistika-ISUM.xls";
            var fileDialog = new SaveFileDialog
            {
                Filter = "Excel files (*.xls)|*.xls",
                FileName = filename
            };
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            decimal fob = calculatePercentage(statTotal.Statistics.WordCount, totalWords);
            decimal fpm = ((calculatePercentage(statTotal.Statistics.WordCount, totalWords)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.6);
            fpm += ((calculatePercentage(statTotal.Statistics.ObjectCount, totalObjects)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            fpm += ((calculatePercentage(statTotal.Statistics.ObjectCount, totalObjectsAndSub)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            fpm += ((calculatePercentage(statTotal.Statistics.SectionCount, totalSections)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            fpm += ((calculatePercentage(statTotal.Statistics.FigureCount, totalFigures)) / Convert.ToDecimal(100.0)) * Convert.ToDecimal(0.05);
            fpm += 100 / 100 * Convert.ToDecimal(0.05);
            fpm += 100 / 100 * Convert.ToDecimal(0.05);
            fpm += 100 / 100 * Convert.ToDecimal(0.05);
            fpm += 100 / 100 * Convert.ToDecimal(0.05);
            fpm *= 100;

            ExcelExporter.ExportToExcel(fileDialog.FileName, statTotal.Statistics, statAverage.Statistics, fob, fpm);
            if (chbOpenFolder.Checked)
            {
                var folder = Path.GetDirectoryName(fileDialog.FileName);
                if (folder != null)
                {
                    Process.Start(folder);
                }
            }
        }

        private bool _closeProgram = true;

        private void StatisticsPanel_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_closeProgram)
            {
                MainForm.Instance.Close();
                return;
            }

            var newForm = new StatisticsForm();
            newForm.Bounds = Bounds;
            newForm.WindowState = WindowState;

            newForm.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            _closeProgram = false;
            Close();
        }
    }
}
