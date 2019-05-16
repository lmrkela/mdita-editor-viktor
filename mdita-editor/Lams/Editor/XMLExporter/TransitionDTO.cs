using System;
using System.Xml.Serialization;

namespace mDitaEditor.Lams.Editor.XMLExporter
{

    [XmlRoot(ElementName = "org.lamsfoundation.lams.learningdesign.dto.TransitionDTO")]
    public class TransitionDTO
    {
        [XmlElement(ElementName = "transitionID")]
        public long TransitionID { get; set; }

        [XmlElement(ElementName = "transitionUIID")]
        public long TransitionUIID { get; set; }

        [XmlElement(ElementName = "toUIID")]
        public long ToUIID { get; set; }

        [XmlElement(ElementName = "fromUIID")]
        public long FromUIID { get; set; }

        [XmlElement(ElementName = "createDateTime")]
        public DateTimeDTO CreateDateTime { get; set; }

        [XmlElement(ElementName = "toActivityID")]
        public long ToActivityID { get; set; }

        [XmlElement(ElementName = "fromActivityID")]
        public long FromActivityID { get; set; }

        [XmlElement(ElementName = "learningDesignID")]
        public long LearningDesignID { get; set; }

        [XmlElement(ElementName = "transitionType")]
        public int TransitionType { get; set; }

        [XmlElement(ElementName = "dataFlowObjects")]
        public string DataFlowObjects { get; set; }

        public TransitionDTO()
        {
            CreateDateTime = new DateTimeDTO();
            LearningDesignID = 32295;
            TransitionType = 0;
            DataFlowObjects = "";
        }

        public TransitionDTO(GrafikaConnection connection, LearningDesignDTO learningDesign) : this()
        {
            var from = learningDesign.Activities.List.Get_(connection.StartItem);
            if (from == null)
            {
                throw new ArgumentException("Nepostojeca pocetna aktivnost. " + connection.StartItem.GrafikaObject.TitleText + " " + connection.EndItem.GrafikaObject.TitleText);
            }
            var to = learningDesign.Activities.List.Get_(connection.EndItem);
            if (to == null)
            {
                throw new ArgumentException("Nepostojeca zavrsna aktivnost. " + connection.StartItem.GrafikaObject.TitleText);
            }
            LearningDesignID = learningDesign.LearningDesignID;
            FromUIID = from.ActivityUIID;
            FromActivityID = from.ActivityID;
            ToUIID = to.ActivityUIID;
            ToActivityID = to.ActivityID;
        }
    }
}