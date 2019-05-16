using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.CustomControls
{
    public partial class StatisticsControl : UserControl
    {
        private ProjectFile.Statistics _statistics;

        public ProjectFile.Statistics Statistics
        {
            get { return _statistics; }
            set
            {
                _statistics = value;
                RefreshDisplay();
            }
        }

        public StatisticsControl()
        {
            InitializeComponent();
            Statistics = new ProjectFile.Statistics();
        }

        private void RefreshDisplay()
        {
            lblWordsCount.Text = $"Words count: {Statistics.WordCount:0.##}";
            figureCount.Text = $"Figure count: {Statistics.FigureCount:0.##}";
            lblVideoCount.Text = $"Video count: {Statistics.VideoCount:0.##}";
            audioCount.Text = $"Audio count: {Statistics.AudioCount:0.##}";

            objCount.Text = $"Objects count: {Statistics.ObjectCount:0.##}";
            objSubCount.Text = $"Objects and subobjects count: {Statistics.SubobjectCount:0.##}";
            sectionCount.Text = $"Section count: {Statistics.SectionCount:0.##}";
            testPercent.Text = $"Tests after: {Statistics.ObjectsWithTestsPercent:0.##} %";

            lblAssesment.Text = $"Assessment count: {Statistics.AssesmentCount:0.##}";
            lblChat.Text = $"Chat count: {Statistics.ChatCount:0.##}";
            lblForum.Text = $"Forum count: {Statistics.ForumCount:0.##}";
            lblGrader.Text = $"Java Grader count: {Statistics.GraderCount:0.##}";
            lblMC.Text = $"Multiple choice count: {Statistics.McCount:0.##}";
            lblQA.Text = $"Q/A count: {Statistics.QaCount:0.##}";
            lblShareResources.Text = $"Shared resources count: {Statistics.ShareResorucesCount:0.##}";
            lblSubmitFiles.Text = $"Submit files count: {Statistics.SubmitFilesCount:0.##}";
            lblNoticeboard.Text = $"Noticeboard count: {Statistics.NoticeboardCount:0.##}";
            lblNotebook.Text = $"Notebook count: {Statistics.NotebookCount:0.##}";
        }

        private void btnRefresh_Click(object sender, System.EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                Statistics = ProjectSingleton.Project.GetStatistics();
            }
        }
    }
}
