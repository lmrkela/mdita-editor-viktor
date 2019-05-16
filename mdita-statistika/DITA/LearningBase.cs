
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    public abstract partial class LearningBase : IDitaSlide
    {
        [XmlIgnore]
        public ProjectFile Project { get; set; }

        [XmlIgnore]
        public LearningBody LearningBody { get; set; }

        [XmlIgnore]
        public abstract string TitleDescription { get; }
       
        [XmlIgnore]
        public LamsObjectList ToolList { get; private set; }

        [XmlIgnore]
        public abstract string FileNamePpt { get; }


        protected LearningBase()
        {
            ToolList = new LamsObjectList(this);
        }


        public virtual string GetTitle()
        {
            return TitleDescription;
        }

        public abstract LearningBase Clone();

        [XmlIgnore]
        public string TitleText
        {
            get { return TitleDescription; }
        }

     
    }
}