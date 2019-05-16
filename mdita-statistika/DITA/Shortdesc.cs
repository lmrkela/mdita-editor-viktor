﻿using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    [XmlRoot(ElementName = "shortdesc")]
    public class Shortdesc
    {
        [XmlElement(ElementName = "draft-comment")]
        public List<Draftcomment> Draftcomment { get; set; }

        public Shortdesc Clone()
        {
            var s = new Shortdesc();
            s.Draftcomment = new List<Draftcomment>(Draftcomment.Count);
            foreach (var d in Draftcomment)
            {
                s.Draftcomment.Add(d.Clone());
            }
            return s;
        }
    }
}
