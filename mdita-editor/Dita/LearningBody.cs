using mDitaEditor.Project;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace mDitaEditor.Dita
{
    [Serializable]
    public class LearningBody
    {
        [XmlElement(ElementName = "lcObjectives")]
        public LcObjectives LcObjectives { get; set; }

        [XmlElement(ElementName = "section")]
        public SectionList Sections { get; set; }

        public LearningBody Clone()
        {
            LearningBody body = new LearningBody();
            body.LcObjectives = LcObjectives.Clone();
            body.Sections = Sections.Clone();
            return body;
        }
    }
}
