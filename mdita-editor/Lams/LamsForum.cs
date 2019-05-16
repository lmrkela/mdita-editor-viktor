using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Lams.Editor.Conditions;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.Forum")]
    public class LamsForum : LamsTool, IHasConditions
    {
        [Serializable]
        [XmlRoot(ElementName = "attachments")]
        public class Attachments
        {
            public Attachments()
            {

            }

            public Attachments(string className)
            {

                this.Class = className;
                this.Nocomparator = "";

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
                this.Reference = referenca;
                this.Subject = null;
                this.Body = null;
                this.IsAuthored = null;
                this.IsAnonymous = null;
                this.HideFlag = null;
            }

            public Message()
            {
                this.Subject = "";
                this.Body = "";
                this.IsAuthored = "true";
                this.IsAnonymous = "false";
                this.HideFlag = "false";


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
        public class MessagesClass
        {

            public MessagesClass()
            {
                this.Message = new List<Message>();
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
                this.Class = className;
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
                this.Class = "tree-set";
                this.Comparator = new ComparatorForum("org.lamsfoundation.lams.tool.forum.util.ConditionTopicComparator");
                this.Message = new Message("../../../../messages/org.lamsfoundation.lams.tool.forum.persistence.Message");


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
                this.Topics = new Topics();
                this.AllWords = "LAMS";
                this.ConditionsParsed = "false";
                this.AllWordsCondition = "";
                this.AnyWordsCondition = "";
                this.ExcludedWordsCondition = "";
                this.OrderId = "1";
                this.Name = "topic.name.to.answers.output.definition.forum#6";
                this.DisplayName = "Posts to the first topic contain word &quot;LAMS&quot;";
                this.Type = "OUTPUT_COMPLEX";
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
                this.Class = "tree-set";
                this.Comparator = new ComparatorForum("org.lamsfoundation.lams.learningdesign.TextSearchConditionComparator");
                this.ForumCondition = new ForumCondition();


            }
            [XmlElement(ElementName = "comparator")]
            public ComparatorForum Comparator { get; set; }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.forum.persistence.ForumCondition")]
            public ForumCondition ForumCondition { get; set; }
            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }

        public LamsForum()
        {
            this.ContentId = "101";
            this.Title = "";
            this.LockWhenFinished = "false";
            this.AllowAnonym = "false";
            this.AllowEdit = "true";
            this.AllowNewTopic = "true";
            this.AllowUpload = "true";
            this.AllowRateMessages = "false";
            this.MaximumReply = "0";
            this.MinimumReply = "0";
            this.MaximumRate = "0";
            this.MinimumRate = "0";
            this.AllowRichEditor = "true";
            this.Instructions = "";
            this.DefineLater = "false";
            this.ContentInUse = "false";
            this.LimitedChar = "0";
            this.LimitedInput = "false";
            this.ReflectOnActivity = "false";
            this.ReflectInstructions = "";
            this.NotifyLearnersOnMarkRelease = "true";
            this.NotifyLearnersOnForumPosting = "true";
            this.NotifyTeachersOnForumPosting = "true";
            this.Messages = new MessagesClass();
            this.Conditions = new ConditionsForum();
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
        [XmlElement(ElementName = "messages")]
        public MessagesClass Messages { get; set; }
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
        public override Image Icon { get { return Resources.lms_forum; } }
        
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

        [XmlIgnore]
        public LamsConditionType[] ConditionsAvailable
        {
            get
            {
                return new[]
                {
                    new LamsConditionType("Number of posts", ConditionDTO.ConditionName.NumberOfPosts, ConditionDTO.ValueType.Long)
                };
            }
        }
    }
}