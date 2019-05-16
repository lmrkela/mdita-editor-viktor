using System;
using System.Drawing;
using System.Xml.Serialization;
using StatistikaProjekata.DITA;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName = "updateDate")]
    public class UpdateDateNotebook
    {
        public UpdateDateNotebook()
        {
            Class = "sql-timestamp";
            Text = "2016-06-29 10:47:39.0";
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlText]
        public string Text { get; set; }
    }
  
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.notebook.model.NotebookCondition")]
    public class NotebookCondition
    {
        public NotebookCondition()
        {
            AllWords = "LAMS";
            ConditionsParsed = "false";
            OrderId = "1";
            Name = "user.entry.output.definition.notebook#16";
            DisplayName = "user entry output definition notebook default condition";
            Type = "OUTPUT_STRING";
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
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.notebook.model.Notebook")]
    public class LamsNotebook : LamsTool
    {
        [XmlElement(ElementName = "createDate")]
        public string CreateDate { get; set; }
        [XmlElement(ElementName = "updateDate")]
        public UpdateDateNotebook UpdateDate { get; set; }
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
        public Conditions Conditions { get; set; }

        public LamsNotebook()
        {
            CreateDate = "2016-03-10 11:13:38.5 CET";
            UpdateDate = new UpdateDateNotebook();
            Title = "";
            Instructions = "";
            LockOnFinished = "false";
            AllowRichEditor = "true";
            ContentInUse = "false";
            DefineLater = "false";
            ToolContentId = "101";
            Conditions = new Conditions();
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
                return null;
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
      
    }


}
