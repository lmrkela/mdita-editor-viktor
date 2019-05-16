using System.Collections.Generic;
using System.Drawing;
using mDitaEditor.Lams.Editor;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    public class LamsBranch : IGrafikaObject
    {
        public string TitleText { get; set; }

        public Image Icon { get { return Resources.branch; } }

        public LamsTool InputTool { get; set; }

        public List<ToolOutputBranchActivityEntryDTO> Entries { get; set; }
        
        public List<GrafikaBranchConnection> Branches { get; private set; }
        
        public GrafikaBranchConnection DefaultBranch { get; set; }

        public bool SequenceChoosing { get; set; }

        public LamsBranch()
        {
            TitleText = "Branching";
            Entries = new List<ToolOutputBranchActivityEntryDTO>();
            Branches = new List<GrafikaBranchConnection>();
        }
    }
}
