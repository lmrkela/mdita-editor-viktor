using System.Collections.Generic;
using System.Drawing;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    public class LamsOptional : IGrafikaObject
    {
        public string TitleText { get; set; }

        public Image Icon { get { return null; } }

        public List<IGrafikaObject> SubObjects { get; private set; } 

        public LamsOptional()
        {
            TitleText = "Optional Activity";
            SubObjects = new List<IGrafikaObject>();
        }
    }
}
