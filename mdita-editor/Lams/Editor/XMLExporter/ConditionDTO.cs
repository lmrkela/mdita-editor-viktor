using System.Xml.Serialization;

namespace mDitaEditor.Lams.Editor.XMLExporter
{

    [XmlRoot(ElementName = "condition")]
    public class ConditionDTO
    {
        public enum ValueType
        {
            [XmlEnum(Name = "OUTPUT_BOOLEAN")]
            Bool,
            [XmlEnum(Name = "OUTPUT_LONG")]
            Long
        }

        public enum ConditionName
        {
            [XmlEnum(Name = "learner.mark")]
            Mark,
            [XmlEnum(Name = "learner.all.correct")]
            AllCorrect,
            [XmlEnum(Name = "learner.number.of.attempts")]
            NumberOfAttempts,
            [XmlEnum(Name = "learner.time.taken")]
            TimeTaken,
            [XmlEnum(Name = "learner.total.score")]
            TotalScore,
            [XmlEnum(Name = "learner.number.of.posts")]
            NumberOfPosts
        }

        [XmlElement(ElementName = "conditionId")]
        public long ConditionId { get; set; }

        [XmlElement(ElementName = "conditionUIID")]
        public long ConditionUIID { get; set; }

        [XmlElement(ElementName = "orderID")]
        public long OrderID { get; set; }

        [XmlElement(ElementName = "name")]
        public ConditionName Name { get; set; }

        [XmlElement(ElementName = "displayName")]
        public string DisplayName { get; set; }

        [XmlElement(ElementName = "type")]
        public ValueType Type { get; set; }

        [XmlElement(ElementName = "exactMatchValue")]
        public string ExactMatchValue { get; set; }

        public bool ShouldSerializeExactMatchValue()
        {
            return ExactMatchValue != null;
        }

        [XmlElement(ElementName = "startValue", IsNullable = true)]
        public long? StartValue { get; set; }

        public bool ShouldSerializeStartValue()
        {
            return StartValue.HasValue;
        }

        [XmlElement(ElementName = "endValue", IsNullable = true)]
        public long? EndValue { get; set; }

        public bool ShouldSerializeEndValue()
        {
            return EndValue.HasValue;
        }

        [XmlElement(ElementName = "toolActivityUIID")]
        public long ToolActivityUIID { get; set; }
    }
}
