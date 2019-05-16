using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName="updateDate")]
	public class UpdateDateMc {

        public UpdateDateMc() {
            Class = "sql-timestamp";
            Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");

        }
        [XmlAttribute(AttributeName="class")]
		public string Class { get; set; }
		[XmlText]
		public string Text { get; set; }
	}

    [Serializable]
    [XmlRoot(ElementName="mcContent")]
	public class McContent {

        public McContent() {
            Reference = "../../..";

       }
		[XmlAttribute(AttributeName="reference")]
		public string Reference { get; set; }
	}

    [Serializable]
    [XmlRoot(ElementName="mcQueContent")]
	public class McQueContent {

        public McQueContent() {
            Reference = "../../..";
        }
		[XmlAttribute(AttributeName="reference")]
		public string Reference { get; set; }
	}

    [Serializable]
    [XmlRoot(ElementName="org.lamsfoundation.lams.tool.mc.pojos.McOptsContent")]
	public class McOptsContent {

        public McOptsContent() {
            CorrectOption = "false";
            McQueOptionText = "Odgovor";
            DisplayOrder = "1";
            Selected = "false";
            McQueContent = new McQueContent();

        }
		[XmlElement(ElementName="correctOption")]
		public string CorrectOption { get; set; }
		[XmlElement(ElementName="mcQueOptionText")]
		public string McQueOptionText { get; set; }
		[XmlElement(ElementName="displayOrder")]
		public string DisplayOrder { get; set; }
		[XmlElement(ElementName="mcQueContent")]
		public McQueContent McQueContent { get; set; }
		[XmlElement(ElementName="selected")]
		public string Selected { get; set; }
	}

    [Serializable]
    [XmlRoot(ElementName="mcOptionsContents")]
	public class McOptionsContents {

        public McOptionsContents() {
            Class = "tree-set";
            Nocomparator = "";
            McOptsContent = new List<McOptsContent>();
        }
		[XmlElement(ElementName="no-comparator")]
		public string Nocomparator { get; set; }
		[XmlElement(ElementName="org.lamsfoundation.lams.tool.mc.pojos.McOptsContent")]
		public List<McOptsContent> McOptsContent { get; set; }
		[XmlAttribute(AttributeName="class")]
		public string Class { get; set; }
	}

    [Serializable]
    [XmlRoot(ElementName="org.lamsfoundation.lams.tool.mc.pojos.McQueContent")]
	public class McQueContentMc
    {

        public McQueContentMc() {
            Question = "Pitanje";
            DisplayOrder = "1";
            Mark = "1";
            McContent = new McContent();
            Feedback = "";
            McOptionsContents = new McOptionsContents();
        }
		[XmlElement(ElementName="question")]
		public string Question { get; set; }
		[XmlElement(ElementName="displayOrder")]
		public string DisplayOrder { get; set; }
		[XmlElement(ElementName="mark")]
		public string Mark { get; set; }
		[XmlElement(ElementName="feedback")]
		public string Feedback { get; set; }
		[XmlElement(ElementName="mcContent")]
		public McContent McContent { get; set; }
		[XmlElement(ElementName="mcOptionsContents")]
		public McOptionsContents McOptionsContents { get; set; }
	}

    [Serializable]
    [XmlRoot(ElementName="mcQueContents")]
	public class McQueContents {

        public McQueContents() {
            Class = "tree-set";
            Nocomparator = "";
            McQueContentMc = new List<McQueContentMc>();
            // this.McQueContentMc

        }
		[XmlElement(ElementName="no-comparator")]
		public string Nocomparator { get; set; }
		[XmlElement(ElementName="org.lamsfoundation.lams.tool.mc.pojos.McQueContent")]
		public List<McQueContentMc> McQueContentMc { get; set; }
		[XmlAttribute(AttributeName="class")]
		public string Class { get; set; }
	}

    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.mc.pojos.McContent")]
    public class LamsMultipleChoice : LamsTool
    {
        public LamsMultipleChoice()
        {
            McContentId = "101";
            Title = "";
            Instructions = "";
            DefineLater = "false";
            Reflect = "false";
            UpdateDateMc = new UpdateDateMc();
            QuestionsSequenced = "false";
            CreatedBy = "1";
            Retries = "false";
            ShowReport = "false";
            Randomize = "false";
            DisplayAnswers = "true";
            ShowMarks = "false";
            UseSelectLeaderToolOuput = "false";
            PrefixAnswersWithLetters = "true";
            PassMark = "0";
            ReflectionSubject = "";
            McQueContents = new McQueContents();
        }

        [XmlElement(ElementName = "mcContentId")]
        public string McContentId { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }

        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }

        [XmlElement(ElementName = "reflect")]
        public string Reflect { get; set; }

        [XmlElement(ElementName = "updateDate")]
        public UpdateDateMc UpdateDateMc { get; set; }

        [XmlElement(ElementName = "questionsSequenced")]
        public string QuestionsSequenced { get; set; }

        [XmlElement(ElementName = "createdBy")]
        public string CreatedBy { get; set; }

        [XmlElement(ElementName = "retries")]
        public string Retries { get; set; }

        [XmlElement(ElementName = "showReport")]
        public string ShowReport { get; set; }

        [XmlElement(ElementName = "randomize")]
        public string Randomize { get; set; }

        [XmlElement(ElementName = "displayAnswers")]
        public string DisplayAnswers { get; set; }

        [XmlElement(ElementName = "showMarks")]
        public string ShowMarks { get; set; }

        [XmlElement(ElementName = "useSelectLeaderToolOuput")]
        public string UseSelectLeaderToolOuput { get; set; }

        [XmlElement(ElementName = "prefixAnswersWithLetters")]
        public string PrefixAnswersWithLetters { get; set; }

        [XmlElement(ElementName = "passMark")]
        public string PassMark { get; set; }

        [XmlElement(ElementName = "reflectionSubject")]
        public string ReflectionSubject { get; set; }

        [XmlElement(ElementName = "mcQueContents")]
        public McQueContents McQueContents { get; set; }

        public override string ToString()
        {
            return "Multiple Choice - " + Title;
        }


        [XmlIgnore]
        public override string TitleText
        {
            get { return Title; }
        }

        [XmlIgnore]
        public override Image Icon
        {
            get { return null; }
        }

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
            get
            {
                return
                    "Learner answers a series of automated assessment questions. e.g. Multiple choice and true/false questions. Optional features include feedback on each question and scoring. Questions are weighted for scoring.";
            }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/lamc11"; }
        }

        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 10; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "lamc11"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 10; }
        }

        [XmlIgnore]
        public override long ToolContentID
        {
            get { return long.Parse(McContentId); }
            set { McContentId = value.ToString(); }
        }

        [XmlIgnore]
        public override string ToolDisplayName
        {
            get { return "MCQ"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140512; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/lamc11/authoringStarter.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/lamc11/monitoringStarter.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 3; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/lamc11/images/icon_mcq.swf"; }
        }

    }
}
