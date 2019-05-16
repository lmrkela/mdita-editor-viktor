using System.Drawing;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    public class LamsGate : IGrafikaObject
    {
        public string TitleText { get; set; }

        public Image Icon { get { return null; } }

        public LamsTool InputTool { get; set; }

        //public List<ToolOutputGateActivityEntryDTO> Entries { get; set; }

        //public LamsGate()
        //{
        //    TitleText = "Gate";
        //    Entries = new List<ToolOutputGateActivityEntryDTO>();
        //}
    }
}
