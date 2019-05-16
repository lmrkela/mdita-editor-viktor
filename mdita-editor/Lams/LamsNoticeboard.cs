using System;
using System.Drawing;
using System.Xml.Serialization;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Project;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    [Serializable]
    [XmlRoot(ElementName = "org.lamsfoundation.lams.tool.noticeboard.NoticeboardContent")]
    public class LamsNoticeboard : LamsTool
    {
        [XmlElement(ElementName = "nbContentId")]
        public long NbContentId { get; set; }

        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "content")]
        public string Content { get; set; }

        [XmlElement(ElementName = "defineLater")]
        public bool DefineLater { get; set; }

        [XmlElement(ElementName = "reflectOnActivity")]
        public bool ReflectOnActivity { get; set; }

        [XmlElement(ElementName = "reflectInstructions")]
        public string ReflectInstructions { get; set; }

        [XmlElement(ElementName = "contentInUse")]
        public bool ContentInUse { get; set; }

        [XmlElement(ElementName = "creatorUserId")]
        public long CreatorUserId { get; set; }

        [XmlElement(ElementName = "dateCreated")]
        public DateTimeDTO DateCreated { get; set; }

        [XmlElement(ElementName = "dateUpdated")]
        public DateTimeDTO DateUpdated { get; set; }

        private LearningBase _learningObject;
        [XmlIgnore]
        public LearningBase LearningObject
        {
            get { return _learningObject; }
            set
            {
                _learningObject = value;
                if (_learningObject != null)
                {
                    var project = ProjectSingleton.Project;
                    if (project == null)
                    {
                        throw new ArgumentException("Project not open.");
                    }
                    Title = _learningObject is LearningContent ? ((LearningContent)_learningObject).Title : _learningObject.TitleText;
                    Content = string.Format("<div><iframe height='850' src='http://mdita.metropolitan.ac.rs/qdita-temp/{0}/{1}/{0}-{1}-{2}.html' width='100%'></iframe></div>", project.CourseCode, project.LessonNumber, _learningObject.FileNamePpt);
                }
            }
        }

        public LamsNoticeboard()
        {
            NbContentId = 271836;
            Title = "";
            Content = "";
            DefineLater = false;
            ReflectOnActivity = false;
            ReflectInstructions = "";
            ContentInUse = false;
            CreatorUserId = 2992;
            DateCreated = new DateTimeDTO();
            DateUpdated = new DateTimeDTO();
        }

        public LamsNoticeboard(LearningBase learningObject) : this()
        {
            LearningObject = learningObject;
        }

        [XmlIgnore]
        public override string TitleText
        {
            get
            {
                if (LearningObject != null)
                {
                    return LearningObject.TitleText;
                }
                else
                {
                    return Title;
                }
            }
        }

        [XmlIgnore]
        public override Image Icon
        {
            get
            {
                if (LearningObject != null)
                {
                    return LearningObject.Icon;
                }
                else
                {
                    return Resources.noticeboard;
                }

            }
        }

        [XmlIgnore]
        public override string Description
        {
            get
            {
                return ReflectInstructions;
            }
        }

        [XmlIgnore]
        public override string ActivityTitle
        {
            get { return Title; }
        }

        [XmlIgnore]
        public override string HelpText
        {
            get { return "Displays formatted text and links to external sources on a read only page."; }
        }

        [XmlIgnore]
        public override string HelpURL
        {
            get { return "http://wiki.lamsfoundation.org/display/lamsdocs/lanb11"; }
        }

        [XmlIgnore]
        public override long LearningLibraryID
        {
            get { return 15; }
        }

        [XmlIgnore]
        public override string ToolSignature
        {
            get { return "lanb11"; }
        }

        [XmlIgnore]
        public override long ToolID
        {
            get { return 15; }
        }

        [XmlIgnore]
        public override long ToolContentID
        {
            get { return NbContentId; }
            set { NbContentId = value; }
        }

        [XmlIgnore]
        public override string ToolDisplayName
        {
            get { return "NoticeboardX"; }
        }

        [XmlIgnore]
        public override long ToolVersion
        {
            get { return 20140102; }
        }

        [XmlIgnore]
        public override string AuthoringURL
        {
            get { return "tool/lanb11/authoring.do"; }
        }

        [XmlIgnore]
        public override string MonitoringURL
        {
            get { return "tool/lanb11/monitoring.do"; }
        }

        [XmlIgnore]
        public override long ActivityCategoryID
        {
            get { return 4; }
        }

        [XmlIgnore]
        public override string LibraryActivityUIImage
        {
            get { return "tool/lanb11/images/icon_htmlnb.swf"; }
        }

        public override string ToString()
        {
            if (LearningObject != null)
            {
                return LearningObject.TitleText + " - " + LearningObject.TitleDescription;
            }
            else
            {
                return this.Title;
            }
        }
    }
}
