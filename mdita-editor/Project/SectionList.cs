using mDitaEditor.Dita;
using System;
using System.Collections.Generic;

namespace mDitaEditor.Project
{
    [Serializable]
    public class SectionList : List<Section>
    {
        public SectionList() : base() { }
        public SectionList(int size) : base(size) { }
        /// <summary>
        /// Funkcija koja ubacuje section objekat u listu posle objekta koji se prosledjuje kao parametar.
        /// </summary>
        /// <param name="preSection"></param>
        /// <param name="newSection"></param>
        public void InsertAfter(Section preSection, Section newSection)
        {
            Insert(IndexOf(preSection) + 1, newSection);
        }

        public SectionList Clone()
        {
            SectionList list = new SectionList(Count);
            foreach (Section s in this)
            {
                list.Add(s.Clone());
            }
            return list;

        }
    }
}
