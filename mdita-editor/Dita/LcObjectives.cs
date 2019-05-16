using System;
using System.Xml;
using System.Xml.Serialization;

namespace mDitaEditor.Dita
{
    [Serializable]
    [XmlRoot(ElementName = "lcObjectives")]
    public class LcObjectives
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "lcObjectivesStem")]
        public string LcDescription { get; set; }

        [XmlElement(ElementName = "lcObjectivesGroup")]
        public LcObjectivesGroup LcObjectivesGroup { get; set; }

        public LcObjectives Clone()
        {
            LcObjectives obj = new LcObjectives();
            obj.Title = Title;
            obj.LcDescription = LcDescription;
            obj.LcObjectivesGroup = LcObjectivesGroup.Clone();
            return obj;
        }
    }
}