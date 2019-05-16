using System.Xml.Serialization;

namespace mDitaEditor.Lams.Editor.XMLExporter
{
    [XmlRoot(ElementName = "org.lamsfoundation.lams.learningdesign.dto.ToolOutputGateActivityEntryDTO")]
    public class ToolOutputGateActivityEntryDTO
    {
        [XmlElement(ElementName = "gateActivityUIID")]
        public long GateActivityUIID { get; set; }

        [XmlElement(ElementName = "gateOpenWhenConditionMet")]
        public bool GateOpenWhenConditionMet { get; set; }

        [XmlElement(ElementName = "condition")]
        public ConditionDTO Condition { get; set; }

        [XmlElement(ElementName = "entryID")]
        public long EntryID { get; set; }

        [XmlElement(ElementName = "entryUIID")]
        public long EntryUIID { get; set; }

        [XmlElement(ElementName = "branchingActivityUIID")]
        public long BranchingActivityUIID { get; set; }

        public ToolOutputGateActivityEntryDTO()
        {
            Condition = new ConditionDTO();
        }
    }
}
