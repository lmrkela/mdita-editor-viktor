using System;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.chat.model.Chat")]
    public class LamsChat : LamsTool
    {
        [Serializable]
        [XmlRoot(ElementName = "comparator")]
        public class Comparator
        {
            public Comparator()
            {

                this.Class = "org.lamsfoundation.lams.learningdesign.TextSearchConditionComparator";
            }

            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.chat.model.ChatCondition")]
        public class Condition
        {

            public Condition()
            {

                this.AllWords = "LAMS";
                this.ConditionsParsed = "false";
                this.AllWordsCondition = "";
                this.AnyWordsCondition = "";
                this.ExcludedWordsCondition = "";
                this.OrderId = "1";
                this.Name = "user.messages.output.definition.chat#3";
                this.DisplayName = "user messages output definition chat default condition";
                this.Type = "OUTPUT_COMPLEX";

            }

            [XmlElement(ElementName = "allWords")]
            public string AllWords { get; set; }
            [XmlElement(ElementName = "conditionsParsed")]
            public string ConditionsParsed { get; set; }
            [XmlElement(ElementName = "allWordsCondition")]
            public string AllWordsCondition { get; set; }
            [XmlElement(ElementName = "anyWordsCondition")]
            public string AnyWordsCondition { get; set; }
            [XmlElement(ElementName = "excludedWordsCondition")]
            public string ExcludedWordsCondition { get; set; }
            [XmlElement(ElementName = "orderId")]
            public string OrderId { get; set; }
            [XmlElement(ElementName = "name")]
            public string Name { get; set; }
            [XmlElement(ElementName = "displayName")]
            public string DisplayName { get; set; }
            [XmlElement(ElementName = "type")]
            public string Type { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "conditions")]
        public class ConditionsChatClass
        {

            public ConditionsChatClass()
            {

                this.Class = "tree-set";
                this.Comparator = new Comparator();
                this.ChatCondition = new Condition();

            }
            [XmlElement(ElementName = "comparator")]
            public Comparator Comparator { get; set; }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.chat.model.ChatCondition")]
            public Condition ChatCondition { get; set; }
            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }

        public LamsChat()
        {

            this.Title = "";
            this.Instructions = "";
            this.LockOnFinished = "false";
            this.ReflectOnActivity = "false";
            this.ReflectInstructions = "";
            this.FilteringEnabled = "false";
            this.FilterKeywords = "";
            this.ContentInUse = "false";
            this.DefineLater = "false";
            this.ToolContentId = "101";
            this.ConditionsChat = new ConditionsChatClass();

        }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }
        [XmlElement(ElementName = "lockOnFinished")]
        public string LockOnFinished { get; set; }
        [XmlElement(ElementName = "reflectOnActivity")]
        public string ReflectOnActivity { get; set; }
        [XmlElement(ElementName = "reflectInstructions")]
        public string ReflectInstructions { get; set; }
        [XmlElement(ElementName = "filteringEnabled")]
        public string FilteringEnabled { get; set; }
        [XmlElement(ElementName = "filterKeywords")]
        public string FilterKeywords { get; set; }
        [XmlElement(ElementName = "contentInUse")]
        public string ContentInUse { get; set; }
        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }
        [XmlElement(ElementName = "toolContentId")]
        public string ToolContentId { get; set; }
        [XmlElement(ElementName = "conditions")]
        public ConditionsChatClass ConditionsChat { get; set; }


        public override string ToString()
        {
            return "Chat - " + Title;
        }

        public override string TitleText { get { return Title; } }

        public override Image Icon { get { return Resources.lms_chat; } }
        
        [XmlIgnore]
        public override string Description
        {
            get { return Instructions; }
        }

        [XmlIgnore]
        public override string ActivityTitle
        {
            get { return Title; }
        }

        [XmlIgnore]
        public override string HelpText
        {
            get { return "Syncronous Chat tool"; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/lachat11"; }
        }
        
        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 3; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "lachat11"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 3; }
        }

        [XmlIgnore]
        public override long ToolContentID
        {
            get { return long.Parse(ToolContentId); }
            set { ToolContentId = value.ToString(); }
        }

        [XmlIgnore]
        public override string ToolDisplayName
        {
            get { return "Chat"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140102; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/lachat11/authoring.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/lachat11/monitoring.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 2; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/lachat11/images/icon_chat.swf"; }
        }
    }
}
