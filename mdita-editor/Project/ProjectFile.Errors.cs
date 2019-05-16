using System.Collections.Generic;
using System.Xml;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Editor.XMLExporter;

namespace mDitaEditor.Project
{
    partial class ProjectFile
    {
        /// <summary>
        /// Metoda koja pronalazi greske u svim objektima.
        /// </summary>
        /// <returns></returns>
        public List<SavingError> CheckSavingErrors(bool lamsFirst = false)
        {
            List<LearningBase> objects = new List<LearningBase>();
            objects.Add(LearningOverview);
            foreach (var learningContent in LearningContents)
            {
                objects.Add(learningContent);
                objects.AddRange(learningContent.SubObjects);
            }
            objects.Add(LearningSummary);

            List<SavingError> errors = new List<SavingError>();
            if (lamsFirst)
            {
                //CheckLamsDesignerErrors(errors);
            }
            foreach (var learningBase in objects)
            {
                CheckLearningObject(learningBase, errors);
            }
            //if(!lamsFirst)
            {
                CheckLamsDesignerErrors(errors);
            }
            return errors;
        }

        private void CheckLamsDesignerErrors(List<SavingError> errors)
        {
            errors.AddRange(new LearningDesignDTO(MainForm.Instance.grafikaPanel, this, true).Errors);
        }

        /// <summary>
        /// Dodaje errore vezane za LearningBase
        /// </summary>
        /// <param name="learningBase"></param>
        /// <param name="errors"></param>
        private void CheckLearningObject(LearningBase learningBase, List<SavingError> errors)
        {
            if (learningBase is LearningContent)
            {
                var learningContent = (LearningContent)learningBase;
                if (learningContent.Title == "")
                {
                    errors.Add(new SavingError(learningContent,
                        $"OBJEKAT {learningContent.TitleDescription} nema naslov.", MainForm.Instance.contentControl.txbTitle));
                }
                else
                {
                    for (int j = LearningContents.IndexOf(learningContent) + 1; j < LearningContents.Count; ++j)
                    {
                        var content2 = LearningContents[j];
                        if (learningContent.Title == content2.Title)
                        {
                            errors.Add(new SavingError(content2,
                                $"OBJEKTI {learningContent.TitleDescription} i {content2.TitleDescription} imaju isti naslov.", MainForm.Instance.contentControl.txbTitle));
                        }
                    }
                }
                foreach (Draftcomment draft in learningContent.Shortdesc.Draftcomment)
                {
                    if (draft.Text == "")
                    {
                        errors.Add(new SavingError(learningContent,
                            $"Niste definisali parametar {draft.Disposition} u OBJEKTU {learningContent.TitleDescription}.", GetControlForDisposition(draft.Disposition)));
                    }
                }
                if (learningContent.LearningContentBody.Sections.Count > 0)
                {
                    if (learningContent.Title != "" && learningContent.Title == learningContent.LearningContentBody.Sections[0].Title)
                    {
                        Section sec = learningContent.LearningContentBody.Sections[0];
                        errors.Add(new SavingError(sec,
                            $"SEKCIJA {1} u objektu {learningContent.TitleDescription} ne sme imati isti naslov kao i taj objekat.", MainForm.Instance.sectionControl.txbNaslov));
                    }
                }
            }
            if (learningBase.LearningBody.Sections.Count == 0)
            {
                errors.Add(new SavingError(learningBase,
                    $"OBJEKAT {learningBase.TitleDescription} mora imati bar jednu sekciju.", null));
            }
            else
            {
                foreach (var sec in learningBase.LearningBody.Sections)
                {
                    CheckSection(sec, errors);
                }
            }
        }

        /// <summary>
        /// Dodaje errore vezane za sekciju.
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="errors"></param>
        private void CheckSection(Section sec, List<SavingError> errors)
        {
            if (sec.Title == "")
            {
                errors.Add(new SavingError(sec,
                    $"SEKCIJA {sec.Parent.LearningBody.Sections.IndexOf(sec) + 1} u objektu {sec.Parent.TitleDescription} nema naslov.", MainForm.Instance.sectionControl.txbNaslov));
            }

            Sectiondiv divCilj = sec.SectionDivs.Find(x => x.Outputclass == "subtitle");
            string cilj = (divCilj != null) ? divCilj.Content : "";
            if (sec.Parent is LearningContent && cilj == "")
            {
                errors.Add(new SavingError(sec,
                    $"SEKCIJA {sec.Parent.LearningBody.Sections.IndexOf(sec) + 1} u objektu {sec.Parent.TitleDescription} nema poentu sekcije.", MainForm.Instance.sectionControl.txbCilj));
            }

            Sectiondiv divSekSek = sec.SectionDivs.Find(x => x.Outputclass != "subtitle");
            if (divSekSek != null)
            {
                foreach (Sectiondiv divSekSek2 in divSekSek.SectionDivs.ToArray())
                {
                    foreach (Sectiondiv divSekSek3 in divSekSek2.SectionDivs.ToArray())
                    {
                        if (divSekSek3.SectionDivs.Count > 0 &&
                                 divSekSek3.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                                 !divSekSek3.SectionDivs[0].Content.Contains("<pre"))
                        {
                            if (divSekSek3.SectionDivs[0].SectionDivs.Count == 0 && (divSekSek3.SectionDivs[0].Content == null || divSekSek3.SectionDivs[0].Content == "" || divSekSek3.SectionDivs[0].Content == "<p></p>" || divSekSek3.SectionDivs[0].Content == "<p>&nbsp;</p>"))
                            {
                                errors.Add(new SavingError(sec, string.Format("SEKCIJA {0} u objektu {1} ima prazan text box.", sec.Parent.LearningBody.Sections.IndexOf(sec) + 1, sec.Parent.TitleDescription), null));
                            }
                            else if((divSekSek3.SectionDivs[0].SectionDivs.Count != 0 && divSekSek3.SectionDivs[0].SectionDivs[0].Outputclass != null && divSekSek3.SectionDivs[0].SectionDivs[0].Outputclass.Contains("note")) && (divSekSek3.SectionDivs[0].SectionDivs[0].Content == null || divSekSek3.SectionDivs[0].SectionDivs[0].Content == "" || divSekSek3.SectionDivs[0].SectionDivs[0].Content == "<p></p>" || divSekSek3.SectionDivs[0].SectionDivs[0].Content == "<p>&nbsp;</p>"))
                            {
                                errors.Add(new SavingError(sec, string.Format("SEKCIJA {0} u objektu {1} ima prazan note.", sec.Parent.LearningBody.Sections.IndexOf(sec) + 1, sec.Parent.TitleDescription), null));
                            }
                        }
                    }
                }
            }
            if (sec.Title != "" || cilj != "")
            {
                var parentList = sec.Parent.LearningBody.Sections;
                int i = parentList.IndexOf(sec);
                for (int j = i + 1; j < parentList.Count; ++j)
                {
                    var sec2 = parentList[j];

                    if (sec.Title != "" && sec.Title == sec2.Title)
                    {
                        errors.Add(new SavingError(sec2,
                            $"SEKCIJE {i + 1} i {j + 1} u objektu {sec.Parent.TitleDescription} imaju isti naslov.", MainForm.Instance.sectionControl.txbNaslov));
                    }
                    if (cilj != "")
                    {
                        Sectiondiv div2 = sec2.SectionDivs.Find(x => x.Outputclass == "subtitle");
                        string cilj2 = (div2 != null) ? div2.Content : "";
                        if (cilj == cilj2)
                        {
                            errors.Add(new SavingError(sec2,
                                $"SEKCIJE {i + 1} i {j + 1} u objektu {sec.Parent.TitleDescription} imaju istu poentu sekcije.", MainForm.Instance.sectionControl.txbCilj));
                        }
                    }
                }
            }

            foreach (var sectiondiv in sec.SectionDivs)
            {
                if (sectiondiv.Outputclass == "columns1")
                {
                    var div = sectiondiv.SectionDivs[0];
                    if (div == null || div.SectionDivs.Count == 0)
                    {
                        return;
                    }
                    div = div.SectionDivs[0];
                    if (div != null && div.Outputclass == "flexslider")
                    {
                        CheckGallery(sec, div, errors);
                    }
                }
            }
        }

        /// <summary>
        /// Dodaje errore vezane za galeriju.
        /// </summary>
        /// <param name="sec"></param>
        /// <param name="div"></param>
        /// <param name="errors"></param>
        private void CheckGallery(Section sec, Sectiondiv div, List<SavingError> errors)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(div.Content);
                var nodes = xmlDoc.SelectNodes("slides/galleryimage");
                int i = 0;
                foreach (XmlNode childrenNode in nodes)
                {
                    ++i;
                    string title = childrenNode.Attributes["title"].Value;
                    if (string.IsNullOrEmpty(title))
                    {
                        errors.Add(new SavingError(sec,
                            $"SLIKA {i} u Galeriji \"{sec.Parent.TitleDescription}: {sec.Title}\" nema naslov.", null));
                    }
                }
            }
            catch
            { }
        }
    }
}
