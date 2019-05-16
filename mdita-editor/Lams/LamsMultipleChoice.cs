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
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.mc.pojos.McContent")]
    public class LamsMultipleChoice : LamsTool, IHasConditions
    {
        [Serializable]
        [XmlRoot(ElementName = "mcContent")]
        public class McContent
        {

            public McContent()
            {
                this.Reference = "../../..";

            }
            [XmlAttribute(AttributeName = "reference")]
            public string Reference { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "mcQueContent")]
        public class McQueContent
        {

            public McQueContent()
            {
                this.Reference = "../../..";
            }
            [XmlAttribute(AttributeName = "reference")]
            public string Reference { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.mc.pojos.McOptsContent")]
        public class McOptsContent
        {

            public McOptsContent()
            {
                this.CorrectOption = "false";
                this.McQueOptionText = "Odgovor";
                this.DisplayOrder = "1";
                this.Selected = "false";
                this.McQueContent = new McQueContent();

            }
            [XmlElement(ElementName = "correctOption")]
            public string CorrectOption { get; set; }
            [XmlElement(ElementName = "mcQueOptionText")]
            public string McQueOptionText { get; set; }
            [XmlElement(ElementName = "displayOrder")]
            public string DisplayOrder { get; set; }
            [XmlElement(ElementName = "mcQueContent")]
            public McQueContent McQueContent { get; set; }
            [XmlElement(ElementName = "selected")]
            public string Selected { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "mcOptionsContents")]
        public class McOptionsContents
        {

            public McOptionsContents()
            {
                this.Class = "tree-set";
                this.Nocomparator = "";
                this.McOptsContent = new List<McOptsContent>();
            }
            [XmlElement(ElementName = "no-comparator")]
            public string Nocomparator { get; set; }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.mc.pojos.McOptsContent")]
            public List<McOptsContent> McOptsContent { get; set; }
            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.mc.pojos.McQueContent")]
        public class McQueContentMc
        {

            public McQueContentMc()
            {
                this.Question = "Pitanje";
                this.DisplayOrder = "1";
                this.Mark = "1";
                this.McContent = new McContent();
                this.Feedback = "";
                this.McOptionsContents = new McOptionsContents();
            }
            [XmlElement(ElementName = "question")]
            public string Question { get; set; }
            [XmlElement(ElementName = "displayOrder")]
            public string DisplayOrder { get; set; }
            [XmlElement(ElementName = "mark")]
            public string Mark { get; set; }
            [XmlElement(ElementName = "feedback")]
            public string Feedback { get; set; }
            [XmlElement(ElementName = "mcContent")]
            public McContent McContent { get; set; }
            [XmlElement(ElementName = "mcOptionsContents")]
            public McOptionsContents McOptionsContents { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "mcQueContents")]
        public class McQueContentsClass
        {

            public McQueContentsClass()
            {
                this.Class = "tree-set";
                this.Nocomparator = "";
                this.McQueContentMc = new List<McQueContentMc>();
                // this.McQueContentMc

            }
            [XmlElement(ElementName = "no-comparator")]
            public string Nocomparator { get; set; }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.mc.pojos.McQueContent")]
            public List<McQueContentMc> McQueContentMc { get; set; }
            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }

        public LamsMultipleChoice()
        {
            this.McContentId = "101";
            this.Title = "";
            this.Instructions = "";
            this.DefineLater = "false";
            this.Reflect = "false";
            this.QuestionsSequenced = "false";
            this.CreatedBy = "1";
            this.Retries = "false";
            this.ShowReport = "false";
            this.Randomize = "false";
            this.DisplayAnswers = "true";
            this.ShowMarks = "false";
            this.UseSelectLeaderToolOuput = "false";
            this.PrefixAnswersWithLetters = "true";
            this.PassMark = "0";
            this.ReflectionSubject = "";
            this.McQueContents = new McQueContentsClass();
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
        public McQueContentsClass McQueContents { get; set; }

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
            get { return Resources.lms_multiple_choice; }
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

        [XmlIgnore]
        public LamsConditionType[] ConditionsAvailable
        {
            get
            {
                return new[]
                {
                    new LamsConditionType("Answers all correct?", ConditionDTO.ConditionName.AllCorrect, ConditionDTO.ValueType.Bool),
                    new LamsConditionType("Total mark", ConditionDTO.ConditionName.Mark, ConditionDTO.ValueType.Long, 0, McQueContents.McQueContentMc.Count)
                };
            }
        }
    }
}
