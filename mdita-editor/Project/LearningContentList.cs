using mDitaEditor.Dita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mDitaEditor.Project
{
    [Serializable]
    public class LearningContentList : List<LearningContent>
    {
        /// <summary>
        /// Dodaje novi learningContent. Ukoliko je u pitanju pod-objekat
        /// stavlja ga u listu pod objekata
        /// </summary>
        /// <param name="item"></param>
        public new void Add(LearningContent item)
        {
            foreach (Section sec in item.LearningContentBody.Sections)
            {
                sec.Parent = item;
            }
            foreach (var learningContent in this)
            {
                if (item.Id == learningContent.Id)
                {
                    item.Parent = learningContent;
                    learningContent.SubObjects.Add(item);
                    return;
                }
            }
            base.Add(item);
        }

        /// <summary>
        /// Metoda koja ubacuje LearningContent objekat na odredjeno mesto u listi i azurira id-jeve ostalih objekata.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="lc"></param>
        public void InsertObject(int index, LearningContent lc) {            
            if (lc.Parent != null) {
                lc.Parent.SubObjects.Insert(index, lc);
                return;
            }
            lc.IncrementId();
            for (int i = index; i < this.Count; i++)
            {  
                this[i].IncrementId();
            }
            base.Insert(index, lc);
        }

        /// <summary>
        /// Metoda koja sredjuje stare objekte da imaju skolsku godinu 
        /// i postavlja podrazumevane vrednosti na draftcomments
        /// </summary>
        /// <param name="item"></param>
        public static void FixDraftComments(LearningContent item)
        {
            if (item.Shortdesc.Draftcomment.Count < 7)
            {
                item.Shortdesc.Draftcomment.Insert(1, new Draftcomment("SchoolYear", ""));
            }
            else
            {
                item.Shortdesc.Draftcomment[1].Disposition = "SchoolYear";
            }
            //Setuje vrednosti iz projekta
            item.Shortdesc.Draftcomment[0].Text = ProjectSingleton.Project.Author;
            item.Shortdesc.Draftcomment[1].Text = ProjectSingleton.Project.Schoolyear;
            item.Shortdesc.Draftcomment[5].Text = ProjectSingleton.Project.CourseCode;
        }

        /// <summary>
        /// Brise objekat sa podobjektima ili u drugom slucaju ako je u pitanju podobjekat brise
        /// podobjekat. 
        /// U slucaju da je u pitanju objekat radi se dekrement svih naziva learning objekata
        /// </summary>
        /// <param name="item"></param>
        public new void Remove(LearningContent item)
        {
            if (item.Parent == null)
            {
                List<LearningContent> learningContents = ProjectSingleton.Project.LearningContents;
                int index = learningContents.IndexOf(item);
                for (int i = index; i < learningContents.Count; i++)
                {
                    LearningContent lc = learningContents[i];
                    foreach (LearningContent lcs in lc.SubObjects)
                    {
                        lcs.DecrementId();
                    }
                    lc.DecrementId();
                }
                base.Remove(item);
            }
            else
            {
                item.Parent.SubObjects.Remove(item);
            }
        }

        /// <summary>
        /// Metoda koja vraca kopiju liste.
        /// </summary>
        /// <returns></returns>    
        public LearningContentList GetCopy()
        {
            LearningContentList copy = new LearningContentList();
            for (int i = 0; i < this.Count; i++)
            {
                copy.Insert(i, (LearningContent) this[i].Clone());
            }
            return copy;
        }
    }
}
