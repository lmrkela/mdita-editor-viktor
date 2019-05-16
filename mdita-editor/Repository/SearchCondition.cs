using System;

namespace mDitaEditor.Repository
{
    class SearchCondition
    {
        public string Field { get; set; }
        public string ConditionType { get; set; }
        public string Match { get; set; }

        public SearchCondition(string field, string conditionType, string match)
        {
            Field = field;
            ConditionType = conditionType;
            Match = match;
        }


        public override string ToString()
        {
            return Field + " " + ConditionType.ToLower() + " \"" + Match + "\"";
        }
    }
}
