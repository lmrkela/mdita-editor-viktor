using System;
using System.IO;
using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.CustomForms
{
    public partial class NewProjectForm : Form
    {

        public string LessonPath { get; private set; }

        public string ProjectPath { get; private set; }

        public string SifraPredmeta
        {
            get { return txbSifraPredmeta.Text.ToUpper(); }
        }

        public string BrojLekcije
        {
            get { return txbBrojLekcije.Text.ToUpper(); }
        }

        public string Naslov
        {
            get { return txbNaslov.Text; }
        }

        public string Godina
        {
            get { return txbGodina.Text; }
        }

        public string Autor
        {
            get { return txbAutor.Text; }
        }

        public ProjectFile ProjectFile { get; private set; }

        public NewProjectForm(string projectPath)
        {
            InitializeComponent();
            ProjectPath = projectPath;
        }

        private void btnCreateProject_Click(object sender, EventArgs e)
        {
            CreateProject();
        }

        private void txb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CreateProject();
            }
        }

        private void CreateProject()
        {
            try
            {
                string sifraPredmeta = txbSifraPredmeta.Text.ToUpper();
                string brojLekcije = txbBrojLekcije.Text.ToUpper();
                string naslov = txbNaslov.Text;
                string godina = txbGodina.Text;
                string autor = txbAutor.Text;

                string path = Path.Combine(ProjectPath, sifraPredmeta, brojLekcije) + "\\";
                LessonPath = path;
                
                bool shouldPass = true;
                if (Directory.Exists(path))
                {
                    shouldPass = false;
                    var result = MessageBox.Show("Postoji već projekat sa ovim brojem lekcije želite li da ga obrišete i kreirate novi?", "Sačuvajte?",
              MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            shouldPass = true;
                            break;
                        case DialogResult.No:
                            shouldPass = false;
                            break;
                    }
                }
                if (shouldPass)
                {
                    ProjectFile = new ProjectFile(path, sifraPredmeta, brojLekcije, naslov, godina, autor);
                    string lastCourses = Properties.Settings.Default.lastCourses;
                    if (!lastCourses.Contains(sifraPredmeta) || lastCourses == null)
                    {
                        if (lastCourses == null || lastCourses == "")
                        {
                            lastCourses = sifraPredmeta;
                        }
                        else
                        {
                            lastCourses += "," + sifraPredmeta;
                        }
                        Properties.Settings.Default.lastCourses = lastCourses;
                        Properties.Settings.Default.lastAuthor = autor;
                    }
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška!");
            }
        }

        private void cmbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NewProject_Load(object sender, EventArgs e)
        {

            string lastCourses = Properties.Settings.Default.lastCourses;
            if (lastCourses != null && lastCourses != "")
            {
                string[] courses = lastCourses.Split(',');
                foreach (string course in courses)
                {
                    txbSifraPredmeta.Items.Add(course);
                }
            }
            if (txbSifraPredmeta.Items.Count > 0)
            {
                txbSifraPredmeta.SelectedIndex = txbSifraPredmeta.Items.Count - 1;
            }


            string lastAuthor = Properties.Settings.Default.lastAuthor;
            if (lastAuthor != "")
            {
                txbAutor.Text = lastAuthor;
            }

        }

        private void txbSifraPredmeta_TextChanged(object sender, EventArgs e)
        {

        }

        private void txbSifraPredmeta_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                e.KeyChar = char.ToUpper(e.KeyChar);
            }
        }
    }
}
