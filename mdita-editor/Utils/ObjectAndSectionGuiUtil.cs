using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Project;

namespace mDitaEditor.Utils
{
    class SectionsGuiUtil
    {
        /// <summary>
        /// Metod koji dodaje novu sekciju u selektovani objekat ili podobjekat.
        /// Kao argument prima output klasu sekcije koja ce se dodati (1 col, 1-2 col, 2-1 col, col 2, col 3).
        /// </summary>
        public static Section AddNewSection(string outputclass, bool createUndo = true, bool openSlide = true, LearningBase parent = null, int index = -1)
        {
            if (ProjectSingleton.Project == null)
            {
                MessageBox.Show("Niste kreirali nov projekat ili ucitali postojeci. ");
                return null;
            }
            if (parent == null)
            {
                if (ProjectSingleton.SelectedContent == null)
                {
                    MessageBox.Show("Morate selektovati objekat za koji zelite da dodate sekciju. ");
                    return null;
                }
                parent = ProjectSingleton.SelectedContent;
            }

            Section newSection = new Section(parent, outputclass, index);
            Sectiondiv subtitle = new Sectiondiv("subtitle");
            newSection.SectionDivs.Add(subtitle);
            subtitle.Content = "";
            string outputclassNew = outputclass.Substring(outputclass.IndexOf("-") + 1);
            Sectiondiv columns = new Sectiondiv(outputclassNew);
            columns.AddSections();
            newSection.SectionDivs.Add(columns);
            //Resava bug sa generisanjem slike
            newSection.GeneratePreviewImage();

            if (openSlide)
            {
                MainForm.Instance.RecreateMenu();
                MainForm.Instance.OpenSlide(newSection);
                MainForm.Instance.slideList.setSectionActive();
            }

            if (createUndo)
            {
                DitaClipboard.AddSectionAddedState(newSection);
                MainForm.Instance.CheckErrorsAndStatistics();
            }

            return newSection;
        }

        /// <summary>
        /// Metoda koja pravi Swap Section Div-ova u projektu
        /// </summary>
        /// <param name="rootSectionDiv"></param>
        /// <param name="up"></param>
        /// <param name="parentSectionDiv"></param>
        public static void SwapSectionDivs(Sectiondiv rootSectionDiv, int up, Sectiondiv parentSectionDiv)
        {
            int index = parentSectionDiv.SectionDivs.IndexOf(rootSectionDiv);
            if (index > 0 && up == 1)
            {
                Sectiondiv temp = parentSectionDiv.SectionDivs[index - 1];
                parentSectionDiv.SectionDivs[index - 1] = rootSectionDiv;
                parentSectionDiv.SectionDivs[index] = temp;
            }
            if (index < (parentSectionDiv.SectionDivs.Count - 1) && up == 0)
            {
                Sectiondiv temp = parentSectionDiv.SectionDivs[index + 1];
                parentSectionDiv.SectionDivs[index + 1] = rootSectionDiv;
                parentSectionDiv.SectionDivs[index] = temp;
            }
        }
    }

}
