using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    public class ListSection : List<Section>
    {
        public ListSection() : base() { }

        public ListSection(int size) : base(size) { }

        /// <summary>
        /// Funkcija koja ubacuje section objekat u listu posle objekta koji se prosledjuje kao parametar.
        /// </summary>
        /// <param name="preSection"></param>
        /// <param name="newSection"></param>
        public void InsertAfter(Section preSection, Section newSection)
        {
            Insert(IndexOf(preSection) + 1, newSection);
        }

        public ListSection Clone()
        {
            var list = new ListSection(Count);
            foreach (var s in this)
            {
                list.Add(s.Clone());
            }
            return list;

        }
    }
}
