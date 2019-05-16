using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Properties;

namespace mDitaEditor.CustomControls
{
    public partial class ErrorListPanel : UserControl
    {
        private List<SavingError> _errors = null;

        /// <summary>
        /// Definiše ne standardni setter za Error listu.
        /// Kada se lista setuje postavlja sve greške na listu grešaka sa leve strane 
        /// koja je u ovom slučaju FlowLayout i definiše sliku da li projekat nema ili ima 
        /// grešaka.
        /// </summary>
        public List<SavingError> Errors
        {
            get { return _errors; }
            set
            {
                _errors = value;

                flowLayout.SuspendLayout();

                int count = _errors != null ? _errors.Count : 0;
                while (flowLayout.Controls.Count > count)
                {
                    flowLayout.Controls[count].Dispose();
                }

                if (count > 0)
                {
                    for (int i = 0; i < count; ++i)
                    {
                        if (flowLayout.Controls.Count > i)
                        {
                            ((ErrorControl)flowLayout.Controls[i]).Error = _errors[i];
                        }
                        else
                        {
                            flowLayout.Controls.Add(new ErrorControl(_errors[i]));
                        }
                    }

                    btnRefresh.Text = count.ToString();
                    if (MainForm.Instance != null)
                    {
                        if (MainForm.Instance.tabGreskeIWord.TabPages.Count > 0)
                        {
                            MainForm.Instance.tabGreskeIWord.TabPages[0].Text = "Greške (" + count + ")";
                        }
                    }
                    label.Image = Resources.error;
                    labOk.Visible = false;
                }
                else
                {
                    if (ProjectSingleton.Project != null)
                    {
                        label.Image = Resources.yes;
                        labOk.Visible = true;
                    }
                    else
                    {
                        label.Image = null;
                        labOk.Visible = false;
                    }
                    btnRefresh.Text = "0";
                    if (MainForm.Instance != null)
                    {
                        if (MainForm.Instance.tabGreskeIWord.TabPages.Count > 0)
                        {
                            MainForm.Instance.tabGreskeIWord.TabPages[0].Text = "Greške (0)";
                        }
                    }
                }

                flowLayout.ResumeLayout();
            }
        }
        /// <summary>
        /// Konstuktor koji inicializuje GUI komponente
        /// </summary>
        public ErrorListPanel()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Metoda koja se pokreće klikom na dugme Refresh
        /// Ova metoda updejtuje listu grešaka u projektu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (ProjectSingleton.Project != null)
            {
                Errors = ProjectSingleton.Project.CheckSavingErrors(MainForm.Instance.tabGrafika.Active);
            }
            else
            {
                Errors = null;
            }
        }
    }
}
