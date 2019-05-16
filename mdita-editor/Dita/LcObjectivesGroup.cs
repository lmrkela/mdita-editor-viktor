using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace mDitaEditor.Dita
{
    [Serializable]
    [XmlRoot(ElementName = "lcObjectivesGroup")]
    public class LcObjectivesGroup
    {
        [XmlElement(ElementName = "lcObjective")]
        public List<string> LcObjective { get; set; }

        public LcObjectivesGroup Clone()
        {
            LcObjectivesGroup grp = new LcObjectivesGroup();
            grp.LcObjective = new List<string>(LcObjective.Count);
            grp.LcObjective.AddRange(LcObjective);
            return grp;
        }
    }
}