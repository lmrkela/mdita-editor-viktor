using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName = "updateDate")]
    public class UpdateDate
    {

        public UpdateDate()
        {

        }
        public UpdateDate(string className, string text)
        {
            Class = className;
            Text = text;
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.qa.QaQueContent")]
    public class QaQueContent
    {

        public QaQueContent()
        {
            Question = "";
        }
        [XmlElement(ElementName = "question")]
        public string Question { get; set; }
        [XmlElement(ElementName = "displayOrder")]
        public string DisplayOrder { get; set; }
        [XmlElement(ElementName = "feedback")]
        public string Feedback { get; set; }
        [XmlElement(ElementName = "required")]
        public string Required { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "qaQueContents")]
    public class QaQueContents
    {
        public QaQueContents()
        {

        }
        public QaQueContents(string className)
        {
            Class = className;
            Nocomparator = "";
        }
        [XmlElement(ElementName = "no-comparator")]
        public string Nocomparator { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.qa.QaQueContent")]
        public List<QaQueContent> QaQueContent { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "comparator")]
    public class Comparator
    {
        public Comparator()
        {

        }
        public Comparator(string compClass)
        {
            Class = compClass;
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "questions")]
    public class Questions
    {
        public Questions()
        {

        }
        public Questions(string className)
        {
            Class = className;
        }
        [XmlElement(ElementName = "comparator")]
        public Comparator Comparator { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "temporaryQuestionDTOSet")]
    public class TemporaryQuestionDTOSet
    {
        public TemporaryQuestionDTOSet()
        {

        }
        public TemporaryQuestionDTOSet(string className, Comparator compartor)
        {
            Class = className;
            Comparator = compartor;
        }

        [XmlElement(ElementName = "comparator")]
        public Comparator Comparator { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.qa.QaCondition")]
    public class QaCondition
    {

        public QaCondition()
        {
            AllWords = "LAMS";
            ConditionsParsed = "false";
            AllWordsCondition = "";
            AnyWordsCondition = "";
            ExcludedWordsCondition = "";
            OrderId = "1";
            Name = "user.answers.output.definition.qa#11";
            DisplayName = "First answer contains word &quot;LAMS&quot;";
            Type = "OUTPUT_COMPLEX";
        }

        [XmlElement(ElementName = "questions")]
        public Questions Questions { get; set; }
        [XmlElement(ElementName = "temporaryQuestionDTOSet")]
        public TemporaryQuestionDTOSet TemporaryQuestionDTOSet { get; set; }
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
    public class Conditions
    {
        public Conditions()
        {

        }
        public Conditions(string v)
        {
            Class = v;
        }

        [XmlElement(ElementName = "comparator")]
        public Comparator Comparator { get; set; }
        [XmlElement(ElementName = "org.lamsfoundation.lams.tool.qa.QaCondition")]
        public QaCondition QaCondition { get; set; }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
    }
    [Serializable]
    [XmlRoot("org.lamsfoundation.lams.tool.qa.QaContent")]
    public class LamsQA : LamsTool
    {
        public LamsQA()
        {
            QaContentId = "101";
            CreatedBy = "1";
            Title = "";
            Instructions = "";
            Reflect = "false";
            DefineLater = "false";
            AllowRichEditor = "false";
            ReflectionSubject = "";
            QuestionsSequenced = "false";
            LockWhenFinished = "false";
            ShowOtherAnswers = "true";
            UsernameVisible = "false";
            UseSelectLeaderToolOuput = "false";
            AllowRateAnswers = "false";
            NotifyTeachersOnResponseSubmit = "true";
        }
        [XmlElement(ElementName = "qaContentId")]
        public string QaContentId { get; set; }
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }
        [XmlElement(ElementName = "createdBy")]
        public string CreatedBy { get; set; }
        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }
        [XmlElement(ElementName = "reflect")]
        public string Reflect { get; set; }
        [XmlElement(ElementName = "reflectionSubject")]
        public string ReflectionSubject { get; set; }
        [XmlElement(ElementName = "questionsSequenced")]
        public string QuestionsSequenced { get; set; }
        [XmlElement(ElementName = "lockWhenFinished")]
        public string LockWhenFinished { get; set; }
        [XmlElement(ElementName = "showOtherAnswers")]
        public string ShowOtherAnswers { get; set; }
        [XmlElement(ElementName = "allowRichEditor")]
        public string AllowRichEditor { get; set; }
        [XmlElement(ElementName = "useSelectLeaderToolOuput")]
        public string UseSelectLeaderToolOuput { get; set; }
        [XmlElement(ElementName = "usernameVisible")]
        public string UsernameVisible { get; set; }
        [XmlElement(ElementName = "allowRateAnswers")]
        public string AllowRateAnswers { get; set; }
        [XmlElement(ElementName = "notifyTeachersOnResponseSubmit")]
        public string NotifyTeachersOnResponseSubmit { get; set; }
        [XmlElement(ElementName = "updateDate")]
        public UpdateDate UpdateDate { get; set; }
        [XmlElement(ElementName = "qaQueContents")]
        public QaQueContents QaQueContents { get; set; }
        [XmlElement(ElementName = "conditions")]
        public Conditions Conditions { get; set; }

        public override string ToString()
        {
            return "Q&A - " + Title;
        }

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
            get { return "Each learner answers one or more questions in short answer format and then sees answers from all learners collated on the next page."; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/laqa11"; }
        }

        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 11; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "laqa11"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 11; }
        }

        [XmlIgnore]
        public override long ToolContentID
        {
            get { return long.Parse(QaContentId); }
            set { QaContentId = value.ToString(); }
        }

        [XmlIgnore]
        public override string ToolDisplayName
        {
            get { return "Question and Answer"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140527; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/laqa11/authoringStarter.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/laqa11/monitoringStarter.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 6; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/laqa11/images/icon_questionanswer.swf"; }
        }
    }



}
