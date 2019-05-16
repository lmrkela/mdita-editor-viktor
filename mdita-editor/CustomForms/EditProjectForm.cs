using System;
using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.CustomForms
{
    public partial class EditProjectForm : Form
    {
        public EditProjectForm()
        {
            InitializeComponent();

            txbNaslov.Text = ProjectSingleton.Project.LessonTitle;
            txbGodina.Text = ProjectSingleton.Project.Schoolyear;
            txbAutor.Text = ProjectSingleton.Project.Author;
            txbSifraPredmeta.Text = ProjectSingleton.Project.CourseCode;
            txbBrojLekcije.Text = ProjectSingleton.Project.LessonNumber;
        }

        /// <summary>
        /// Event Handler koji submituje formu pritiskom na enter.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                UpdateProjectData();
            }
        }

        /// <summary>
        /// Metoda koja ažurira podatke u okviru projekta koje korisnik definise u formi.
        /// </summary>
        private void UpdateProjectData() {
            string title = txbNaslov.Text;
            string year = txbGodina.Text;
            string author = txbAutor.Text;
            string courseCode = txbSifraPredmeta.Text;
            string lessonNumber = txbBrojLekcije.Text;

            try
            {
                ProjectSingleton.Project.LearningOverview.Title = title;
                ProjectSingleton.Project.UpdateProject(courseCode, lessonNumber, title, year, author);
                ProjectSingleton.Project.SaveProjectFile();
                DialogResult = DialogResult.OK;
                
                
            }
            catch (ArgumentException ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnUpdateProject_Click(object sender, EventArgs e)
        {
            UpdateProjectData();
        }
    }
}
