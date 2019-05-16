using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName = "created")]
    public class Created
    {
        public Created()
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
    [XmlRoot(ElementName = "updated")]
    public class Updated
    {
        public Updated()
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
    [XmlRoot(ElementName = "lastReplyDate")]
    public class LastReplyDate
    {
        public LastReplyDate()
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
    [XmlRoot(ElementName = "attachments")]
    public class Attachments
    {
        public Attachments()
        {

        }

        public Attachments(string className)
        {

            Class = className;
            Nocomparator = "";

        }
        [XmlElement(ElementName = "no-comparator")]
        public string Nocomparator { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.Message")]
    public class Message
    {

        public Message(string referenca)
        {
            Reference = referenca;
            Subject = null;
            Body = null;
            IsAuthored = null;
            IsAnonymous = null;
            HideFlag = null;
            Created = null;
            Updated = null;
            LastReplyDate = null;
        }

        public Message()
        {
            Subject = "";
            Body = "";
            IsAuthored = "true";
            IsAnonymous = "false";
            HideFlag = "false";
            Created = new Created();
            Updated = new Updated();
            LastReplyDate = new LastReplyDate();


        }
        [XmlElement(ElementName = "subject")]
        public string Subject { get; set; }
        [XmlElement(ElementName = "body")]
        public string Body { get; set; }
        [XmlElement(ElementName = "sequenceId")]
        public string SequenceId { get; set; }
        [XmlElement(ElementName = "isAuthored")]
        public string IsAuthored { get; set; }
        [XmlElement(ElementName = "isAnonymous")]
        public string IsAnonymous { get; set; }
        [XmlElement(ElementName = "created")]
        public Created Created { get; set; }
        [XmlElement(ElementName = "updated")]
        public Updated Updated { get; set; }
        [XmlElement(ElementName = "lastReplyDate")]
        public LastReplyDate LastReplyDate { get; set; }
        [XmlElement(ElementName = "replyNumber")]
        public string ReplyNumber { get; set; }
        [XmlElement(ElementName = "hideFlag")]
        public string HideFlag { get; set; }
        [XmlElement(ElementName = "attachments")]
        public Attachments Attachments { get; set; }
        [XmlAttribute(AttributeName = "reference")]
        public string Reference { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "messages")]
    public class Messages
    {

        public Messages()
        {
            Message = new List<Message>();
        }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.Message")]
        public List<Message> Message { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "comparator")]
    public class ComparatorForum
    {

        public ComparatorForum()
        {

        }

        public ComparatorForum(string className)
        {
            Class = className;
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "topics")]
    public class Topics
    {

        public Topics()
        {
            Class = "tree-set";
            Comparator = new ComparatorForum("org.lamsfoundation.lams.tool.forum.util.ConditionTopicComparator");
            Message = new Message("../../../../messages/org.lamsfoundation.lams.tool.forum.persistence.Message");


        }
        [XmlElement(ElementName = "comparator")]
        public ComparatorForum Comparator { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.Message")]
        public Message Message { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.ForumCondition")]
    public class ForumCondition
    {
        public ForumCondition()
        {
            Topics = new Topics();
            AllWords = "LAMS";
            ConditionsParsed = "false";
            AllWordsCondition = "";
            AnyWordsCondition = "";
            ExcludedWordsCondition = "";
            OrderId = "1";
            Name = "topic.name.to.answers.output.definition.forum#6";
            DisplayName = "Posts to the first topic contain word &quot;LAMS&quot;";
            Type = "OUTPUT_COMPLEX";
        }
        [XmlElement(ElementName = "topics")]
        public Topics Topics { get; set; }
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

    [XmlRoot(ElementName = "conditions")]

    [Serializable]
    public class ConditionsForum
    {

        public ConditionsForum()
        {
            Class = "tree-set";
            Comparator = new ComparatorForum("org.lamsfoundation.lams.learningdesign.TextSearchConditionComparator");
            ForumCondition = new ForumCondition();


        }
        [XmlElement(ElementName = "comparator")]
        public ComparatorForum Comparator { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.ForumCondition")]
        public ForumCondition ForumCondition { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.Forum")]
    public class LamsForum : LamsTool
    {
        public LamsForum()
        {
            ContentId = "101";
            Title = "";
            LockWhenFinished = "false";
            AllowAnonym = "false";
            AllowEdit = "true";
            AllowNewTopic = "true";
            AllowUpload = "true";
            AllowRateMessages = "false";
            MaximumReply = "0";
            MinimumReply = "0";
            MaximumRate = "0";
            MinimumRate = "0";
            AllowRichEditor = "true";
            Instructions = "";
            DefineLater = "false";
            ContentInUse = "false";
            Created = new Created();
            Updated = new Updated();
            LimitedChar = "0";
            LimitedInput = "false";
            ReflectOnActivity = "false";
            ReflectInstructions = "";
            NotifyLearnersOnMarkRelease = "true";
            NotifyLearnersOnForumPosting = "true";
            NotifyTeachersOnForumPosting = "true";
            Messages = new Messages();
            Conditions = new ConditionsForum();
        }

        public override string ToString()
        {
            return "Forum - " + Title;
        }
        [XmlElement(ElementName = "contentId")]
        public string ContentId { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "lockWhenFinished")]
        public string LockWhenFinished { get; set; }
        [XmlElement(ElementName = "allowAnonym")]
        public string AllowAnonym { get; set; }
        [XmlElement(ElementName = "allowEdit")]
        public string AllowEdit { get; set; }
        [XmlElement(ElementName = "allowNewTopic")]
        public string AllowNewTopic { get; set; }
        [XmlElement(ElementName = "allowUpload")]
        public string AllowUpload { get; set; }
        [XmlElement(ElementName = "allowRateMessages")]
        public string AllowRateMessages { get; set; }
        [XmlElement(ElementName = "maximumReply")]
        public string MaximumReply { get; set; }
        [XmlElement(ElementName = "minimumReply")]
        public string MinimumReply { get; set; }
        [XmlElement(ElementName = "maximumRate")]
        public string MaximumRate { get; set; }
        [XmlElement(ElementName = "minimumRate")]
        public string MinimumRate { get; set; }
        [XmlElement(ElementName = "allowRichEditor")]
        public string AllowRichEditor { get; set; }
        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }
        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }
        [XmlElement(ElementName = "contentInUse")]
        public string ContentInUse { get; set; }
        [XmlElement(ElementName = "created")]
        public Created Created { get; set; }
        [XmlElement(ElementName = "updated")]
        public Updated Updated { get; set; }
        [XmlElement(ElementName = "messages")]
        public Messages Messages { get; set; }
        [XmlElement(ElementName = "limitedChar")]
        public string LimitedChar { get; set; }
        [XmlElement(ElementName = "limitedInput")]
        public string LimitedInput { get; set; }
        [XmlElement(ElementName = "reflectOnActivity")]
        public string ReflectOnActivity { get; set; }
        [XmlElement(ElementName = "reflectInstructions")]
        public string ReflectInstructions { get; set; }
        [XmlElement(ElementName = "notifyLearnersOnMarkRelease")]
        public string NotifyLearnersOnMarkRelease { get; set; }
        [XmlElement(ElementName = "notifyLearnersOnForumPosting")]
        public string NotifyLearnersOnForumPosting { get; set; }
        [XmlElement(ElementName = "notifyTeachersOnForumPosting")]
        public string NotifyTeachersOnForumPosting { get; set; }
        [XmlElement(ElementName = "conditions")]
        public ConditionsForum Conditions { get; set; }

        [XmlIgnore]
        public override string TitleText { get { return Title; } }
        [XmlIgnore]
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
            get { return "Discussion tool useful for long running collaborations and situations where learners are not all on line at the same time."; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/lafrum11"; }
        }

        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 6; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "lafrum11"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 6; }
        }

        [XmlIgnore]
        public override long ToolContentID
        {
            get { return long.Parse(ContentId); }
            set { ContentId = value.ToString(); }
        }

        [XmlIgnore]
        public override string ToolDisplayName
        {
            get { return "Forum"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140102; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/lafrum11/authoring.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/lafrum11/monitoring.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 2; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/lafrum11/images/icon_forum.swf"; }
        }

       
    }
}