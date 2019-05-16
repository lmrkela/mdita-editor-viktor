using System.Xml.Serialization;

namespace mDitaEditor.Lams.Editor.XMLExporter
{
    [XmlRoot(ElementName = "org.lamsfoundation.lams.learningdesign.dto.ToolOutputBranchActivityEntryDTO")]
    public class ToolOutputBranchActivityEntryDTO
    {

        [XmlElement(ElementName = "condition")]
        public ConditionDTO Condition { get; set; }

        [XmlElement(ElementName = "entryID")]
        public long EntryID { get; set; }

        [XmlElement(ElementName = "entryUIID")]
        public long EntryUIID { get; set; }

        [XmlElement(ElementName = "branchingActivityUIID", IsNullable = true)]
        public long? BranchingActivityUIID { get; set; }

        public bool ShouldSerializeBranchingActivityUIID()
        {
            return BranchingActivityUIID.HasValue;
        }

        [XmlElement(ElementName = "sequenceActivityUIID", IsNullable = true)]
        public long? SequenceActivityUIID { get; set; }

        public bool ShouldSerializeSequenceActivityUIID()
        {
            return SequenceActivityUIID.HasValue;
        }

        [XmlIgnore]
        public GrafikaBranchConnection BranchPath { get; set; }

        public ToolOutputBranchActivityEntryDTO()
        {
            Condition = new ConditionDTO();
        }
    }
}
