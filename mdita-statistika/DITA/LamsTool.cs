using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using StatistikaProjekata.LAMS;

namespace StatistikaProjekata.DITA
{

    [XmlInclude(typeof(LamsNoticeboard))]
    [XmlType(Namespace = "")]
    [Serializable]
    public abstract class LamsTool : IGrafikaObject
    {
        public int DesignerParentIndex { get; set; }

        public void UpdateParentIndex()
        {
            if (Parent == null || Parent is LearningOverview)
            {
                DesignerParentIndex = -2;
            }
            else if (Parent is LearningSummary)
            {
                DesignerParentIndex = -1;
            }
            else
            {
                var content = Parent as LearningContent;
                if (content?.Parent != null)
                {
                    content = content.Parent;
                }
            }
        }

        [XmlIgnore]
        public LearningBase Parent { get; set; }

        [XmlIgnore]
        public abstract string TitleText { get; }

        [XmlIgnore]
        public abstract Image Icon { get; }

        [XmlIgnore]
        public abstract string Description { get; }

        [XmlIgnore]
        public abstract string ActivityTitle { get; }

        [XmlIgnore]
        public abstract string HelpText { get; }

        [XmlIgnore]
        public abstract string HelpURL { get; }

        [XmlIgnore]
        public abstract long LearningLibraryID { get; }

        [XmlIgnore]
        public abstract string ToolSignature { get; }

        [XmlIgnore]
        public abstract long ToolID { get; }

        [XmlIgnore]
        public abstract long ToolContentID { get; set; }

        [XmlIgnore]
        public abstract string ToolDisplayName { get; }

        [XmlIgnore]
        public abstract long ToolVersion { get; }

        [XmlIgnore]
        public abstract string AuthoringURL { get; }

        [XmlIgnore]
        public abstract string MonitoringURL { get; }

        [XmlIgnore]
        public abstract long ActivityCategoryID { get; }

        [XmlIgnore]
        public abstract string LibraryActivityUIImage { get; }

        public LamsTool()
        {
            DesignerParentIndex = -2;
        }
    }
}
