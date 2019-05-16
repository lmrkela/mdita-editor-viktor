using System;
using System.Drawing;
using System.Xml.Serialization;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName = "updateDate")]
    public class UpdateDateChat
    {

        public UpdateDateChat()
        {
            Class = "sql-timestamp";
            Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "comparator")]
    public class ComparatorChat
    {
        public ComparatorChat()
        {

            Class = "org.lamsfoundation.lams.learningdesign.TextSearchConditionComparator";
        }

        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.chat.model.ChatCondition")]
    public class ChatCondition
    {

        public ChatCondition()
        {

            AllWords = "LAMS";
            ConditionsParsed = "false";
            AllWordsCondition = "";
            AnyWordsCondition = "";
            ExcludedWordsCondition = "";
            OrderId = "1";
            Name = "user.messages.output.definition.chat#3";
            DisplayName = "user messages output definition chat default condition";
            Type = "OUTPUT_COMPLEX";

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
    public class ConditionsChat
    {

        public ConditionsChat()
        {

            Class = "tree-set";
            Comparator = new Comparator("org.lamsfoundation.lams.learningdesign.TextSearchConditionComparator");
            ChatCondition = new ChatCondition();

        }
        [XmlElement(ElementName = "comparator")]
        public Comparator Comparator { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.chat.model.ChatCondition")]
        public ChatCondition ChatCondition { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.chat.model.Chat")]
    public class LamsChat : LamsTool
    {
        public LamsChat()
        {

            CreateDate = "2016-03-10 11:13:38.5 CET";
            UpdateDateChat = new UpdateDateChat();
            Title = "";
            Instructions = "";
            LockOnFinished = "false";
            ReflectOnActivity = "false";
            ReflectInstructions = "";
            FilteringEnabled = "false";
            FilterKeywords = "";
            ContentInUse = "false";
            DefineLater = "false";
            ToolContentId = "101";
            ConditionsChat = new ConditionsChat();

        }
        [XmlElement(ElementName = "createDate")]
        public string CreateDate { get; set; }
        [XmlElement(ElementName = "updateDate")]
        public UpdateDateChat UpdateDateChat { get; set; }
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
        public ConditionsChat ConditionsChat { get; set; }


        public override string ToString()
        {
            return "Chat - " + Title;
        }

        public override string TitleText { get { return Title; } }

        public override Image Icon { get { return null; } }
        
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
