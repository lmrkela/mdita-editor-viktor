using mDitaEditor.Lams.Editor.XMLExporter;

namespace mDitaEditor.Lams.Editor.Conditions
{
    public class LamsConditionType
    {
        public string Description { get; private set; }
        public ConditionDTO.ConditionName Name { get; private set; }
        public ConditionDTO.ValueType Type { get; private set; }
        public long MinValue { get; private set; }
        public long MaxValue { get; private set; }

        public LamsConditionType(string description, ConditionDTO.ConditionName name, ConditionDTO.ValueType type, long min = 0, long max = long.MaxValue)
        {
            Description = description;
            Name = name;
            Type = type;
            MinValue = min;
            MaxValue = max;
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
