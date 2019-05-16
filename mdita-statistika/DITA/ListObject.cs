using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    public class ListObject : List<LearningContent>
    {
        /// <summary>
        /// Dodaje novi learningContent. Ukoliko je u pitanju pod-objekat
        /// stavlja ga u listu pod objekata
        /// </summary>
        /// <param name="item"></param>
        public new void Add(LearningContent item, ProjectFile project)
        {
            foreach (var sec in item.LearningContentBody.Section)
            {
                sec.Parent = item;
            }
            foreach (var learningContent in project.LearningContents)
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
        public void InsertObject(int index, LearningContent lc)
        {
            if (lc.Parent != null)
            {
                lc.Parent.SubObjects.Insert(index, lc);
                return;
            }
            lc.IncrementId();
            for (var i = index; i < Count; i++)
            {
                this[i].IncrementId();
            }
            Insert(index, lc);
        }

        /// <summary>
        /// Metoda koja sredjuje stare objekte da imaju skolsku godinu 
        /// i postavlja podrazumevane vrednosti na draftcomments
        /// </summary>
        /// <param name="item"></param>
        public static void FixDraftComments(LearningContent item, ProjectFile project)
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
            item.Shortdesc.Draftcomment[0].Text = project.Author;
            item.Shortdesc.Draftcomment[1].Text = project.Schoolyear;
            item.Shortdesc.Draftcomment[5].Text = project.CourseCode;
        }

        /// <summary>
        /// Brise objekat sa podobjektima ili u drugom slucaju ako je u pitanju podobjekat brise
        /// podobjekat. 
        /// U slucaju da je u pitanju objekat radi se dekrement svih naziva learning objekata
        /// </summary>
        /// <param name="item"></param>
        public new void Remove(LearningContent item, ProjectFile project)
        {
            if (item.Parent == null)
            {
                List<LearningContent> learningContents = project.LearningContents;
                var index = learningContents.IndexOf(item);
                for (var i = index; i < learningContents.Count; i++)
                {
                    var lc = learningContents[i];
                    foreach (var lcs in lc.SubObjects)
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
        public ListObject GetCopy()
        {
            var copy = new ListObject();
            for (var i = 0; i < Count; i++)
            {
                copy.Insert(i, (LearningContent)this[i].Clone());
            }
            return copy;
        }
    }
}
