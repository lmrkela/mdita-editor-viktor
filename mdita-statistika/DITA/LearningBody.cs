
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    public class LearningBody
    {
        [XmlElement(ElementName = "lcObjectives")]
        public LcObjectives LcObjectives { get; set; }

        [XmlElement(ElementName = "section")]
        public ListSection Section { get; set; }

        public LearningBody Clone()
        {
            var body = new LearningBody();
            body.LcObjectives = LcObjectives.Clone();
            body.Section = Section.Clone();
            return body;
        }
    }
}
