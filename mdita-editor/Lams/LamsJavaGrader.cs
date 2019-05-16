using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.javagrader.model.Javagrader")]
    public class LamsJavaGrader : LamsTool
    {
        public LamsJavaGrader()
        {
            this.Name = "";
            this.ToolContentId = "3";

            this.JavagraderQuestions = new JavagraderQuestionsClass();
        }

        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "toolContentId")]
        public string ToolContentId { get; set; }
        [XmlElement(ElementName = "javagraderQuestions")]
        public JavagraderQuestionsClass JavagraderQuestions { get; set; }

        [XmlIgnore]
        public override string TitleText { get { return Name; } }

        public override string ToString()
        {
            return "JavaGrader - " + Name;
        }
        [XmlIgnore]
        public override Image Icon { get { return Resources.java; } }

        [XmlIgnore]
        public override string Description
        {
            get { return "JavaGrader Tool"; }
        }

        [XmlIgnore]
        public override string ActivityTitle
        {
            get { return "javagrader"; }
        }

        [XmlIgnore]
        public override string HelpText
        {
            get { return "JavaGrader for notes and reflections"; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/lajavg20"; }
        }

        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 1; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "lajavg20"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 1; }
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
            get { return "Javagrader"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20071100; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/lajavg20/authoring.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/lajavg20/monitoring.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 6; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/lajavg20/images/icon_javagrader.swf"; }
        }
        [Serializable]
        [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.javagrader.model.JavagraderQuestion")]
        public class JavagraderQuestion
        {
            public JavagraderQuestion()
            {
                this.MethodName = "";
                this.Text = "";
                this.MethodHead = "";
                this.Method = "";
                this.Params1 = "";
                this.Params2 = "";
                this.Params3 = "";
                this.Params4 = "";
                this.Params5 = "";
                this.Params6 = "";
                this.Params7 = "";
                this.Params8 = "";
                this.Params9 = "";
                this.Params10 = "";
                this.Returns1 = "";
                this.Returns2 = "";
                this.Returns3 = "";
                this.Returns4 = "";
                this.Returns5 = "";
                this.Returns6 = "";
                this.Returns7 = "";
                this.Returns8 = "";
                this.Returns9 = "";
                this.Returns10 = "";
                this.OrderId = 1;
            }

            public override string ToString()
            {
                return Text;
            }

            [XmlElement(ElementName = "methodName")]
            public string MethodName { get; set; }
            [XmlElement(ElementName = "text")]
            public string Text { get; set; }
            [XmlElement(ElementName = "methodHead")]
            public string MethodHead { get; set; }
            [XmlElement(ElementName = "method")]
            public string Method { get; set; }
            [XmlElement(ElementName = "params1")]
            public string Params1 { get; set; }
            [XmlElement(ElementName = "params2")]
            public string Params2 { get; set; }
            [XmlElement(ElementName = "params3")]
            public string Params3 { get; set; }
            [XmlElement(ElementName = "params4")]
            public string Params4 { get; set; }
            [XmlElement(ElementName = "params5")]
            public string Params5 { get; set; }
            [XmlElement(ElementName = "params6")]
            public string Params6 { get; set; }
            [XmlElement(ElementName = "params7")]
            public string Params7 { get; set; }
            [XmlElement(ElementName = "params8")]
            public string Params8 { get; set; }
            [XmlElement(ElementName = "params9")]
            public string Params9 { get; set; }
            [XmlElement(ElementName = "params10")]
            public string Params10 { get; set; }
            [XmlElement(ElementName = "returns1")]
            public string Returns1 { get; set; }
            [XmlElement(ElementName = "returns2")]
            public string Returns2 { get; set; }
            [XmlElement(ElementName = "returns3")]
            public string Returns3 { get; set; }
            [XmlElement(ElementName = "returns4")]
            public string Returns4 { get; set; }
            [XmlElement(ElementName = "returns5")]
            public string Returns5 { get; set; }
            [XmlElement(ElementName = "returns6")]
            public string Returns6 { get; set; }
            [XmlElement(ElementName = "returns7")]
            public string Returns7 { get; set; }
            [XmlElement(ElementName = "returns8")]
            public string Returns8 { get; set; }
            [XmlElement(ElementName = "returns9")]
            public string Returns9 { get; set; }
            [XmlElement(ElementName = "returns10")]
            public string Returns10 { get; set; }
            [XmlElement(ElementName = "orderId")]
            public int OrderId { get; set; }
        }

        [Serializable]
        [XmlRoot(ElementName = "javagraderQuestions")]
        public class JavagraderQuestionsClass
        {
            public JavagraderQuestionsClass()
            {
                this.JavagraderQuestion = new List<JavagraderQuestion>();
            }
            [XmlElement(ElementName = "org.lamsfoundation.lams.tool.javagrader.model.JavagraderQuestion")]
            public List<JavagraderQuestion> JavagraderQuestion { get; set; }
        }
    }

}
