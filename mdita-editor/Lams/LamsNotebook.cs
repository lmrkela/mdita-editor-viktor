using System;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.notebook.model.Notebook")]
    public class LamsNotebook : LamsTool
    {
        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.notebook.model.NotebookCondition")]
        public class NotebookCondition
        {
            public NotebookCondition()
            {
                this.AllWords = "LAMS";
                this.ConditionsParsed = "false";
                this.OrderId = "1";
                this.Name = "user.entry.output.definition.notebook#16";
                this.DisplayName = "user entry output definition notebook default condition";
                this.Type = "OUTPUT_STRING";
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

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }
        [XmlElement(ElementName = "instructions")]
        public string Instructions { get; set; }
        [XmlElement(ElementName = "lockOnFinished")]
        public string LockOnFinished { get; set; }
        [XmlElement(ElementName = "allowRichEditor")]
        public string AllowRichEditor { get; set; }
        [XmlElement(ElementName = "contentInUse")]
        public string ContentInUse { get; set; }
        [XmlElement(ElementName = "defineLater")]
        public string DefineLater { get; set; }
        [XmlElement(ElementName = "toolContentId")]
        public string ToolContentId { get; set; }
        [XmlElement(ElementName = "conditions")]
        public Conditions ConditionList { get; set; }

        public LamsNotebook()
        {
            this.Title = "";
            this.Instructions = "";
            this.LockOnFinished = "false";
            this.AllowRichEditor = "true";
            this.ContentInUse = "false";
            this.DefineLater = "false";
            this.ToolContentId = "101";
            this.ConditionList = new Conditions();
        }

        public override string ToString()
        {
            return "Notebook - " + Title;
        }
        [XmlIgnore]
        public override string TitleText
        {
            get
            {
                return Title;
            }
        }
        [XmlIgnore]
        public override Image Icon
        {
            get
            {
                return Resources.notebook;
            }
        }
        [XmlIgnore]

        public override string Description
        {
            get
            {
                return Instructions;
            }
        }
        [XmlIgnore]
        public override string ActivityTitle
        {
            get
            {
                return Title;
            }
        }
        [XmlIgnore]
        public override string HelpText
        {
            get
            {
                return "Notebook for notes and reflections";
            }
        }
        [XmlIgnore]
        public override string HelpURL
        {
            get
            {
                return "http://wiki.lamsfoundation.org/display/lamsdocs/lantbk11";
            }
        }
        [XmlIgnore]
        public override long LearningLibraryID
        {
            get
            {
                return 16;
            }
        }
        [XmlIgnore]
        public override string ToolSignature
        {
            get
            {
                return "lantbk11";
            }
        }
        [XmlIgnore]
        public override long ToolID
        {
            get
            {
                return 16;
            }
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
            get
            {
                return "Notebook";
            }
        }
        [XmlIgnore]
        public override long ToolVersion
        {
            get
            {
                return 20140102;
            }
        }
        [XmlIgnore]
        public override string AuthoringURL
        {
            get
            {
                return "tool/lantbk11/authoring.do";
            }
        }
        [XmlIgnore]
        public override string MonitoringURL
        {
            get
            {
                return "tool/lantbk11/monitoring.do";
            }
        }
        [XmlIgnore]

        public override long ActivityCategoryID
        {
            get
            {
                return 6;
            }
        }
        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get
            {
                return "tool/lantbk11/images/icon_notebook.swf";
            }
        }

        [Serializable]
        [XmlRoot(ElementName = "conditions")]
        public class Conditions
        {
            public Conditions()
            {
                this.Class = "tree-set";
                this.Comparator = new Comparator("org.lamsfoundation.lams.learningdesign.TextSearchConditionComparator");
                this.NotebookCondition = new NotebookCondition();

            }
            [XmlElement(ElementName = "comparator")]
            public Comparator Comparator { get; set; }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.notebook.model.NotebookCondition")]
            public NotebookCondition NotebookCondition { get; set; }
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
            public Comparator(string comp)
            {
                this.Class = comp;
            }

            [XmlAttribute(AttributeName = "class")]
            public string Class { get; set; }
        }
    }
}
