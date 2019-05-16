using System.Collections.Generic;
using System.Drawing;
using mDitaEditor.Lams.Editor;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    public class LamsGate : IGrafikaObject
    {
        public string TitleText { get; set; }

        public Image Icon { get { return Resources.stop_sign; } }

        public LamsTool InputTool { get; set; }

        public List<ToolOutputGateActivityEntryDTO> Entries { get; set; }

        public LamsGate()
        {
            TitleText = "Gate";
            Entries = new List<ToolOutputGateActivityEntryDTO>();
        }
    }
}
