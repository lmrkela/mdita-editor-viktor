using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using mDitaEditor.Project;

namespace mDitaEditor.Dita
{
    [Serializable]
    public abstract partial class LearningBase : IDitaSlide
    {
        [XmlIgnore, NonSerialized]
        private ProjectFile _project;
        [XmlIgnore]
        public ProjectFile Project { get { return _project; } set { _project = value; } }

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
            Project = ProjectSingleton.Project;
            ToolList = new LamsObjectList(this);
        }


        public virtual string GetTitle()
        {
            return TitleDescription;
        }

        public abstract Image GetPreviewImage();
        public abstract bool HasPreviewImage();
        public abstract void GeneratePreviewImage();
        public abstract bool CanMove(bool up);

        public abstract LearningBase Clone();

        [XmlIgnore]
        public string TitleText
        {
            get { return TitleDescription; }
        }

        [XmlIgnore]
        public Image Icon
        {
            get
            {
                var img = GetPreviewImage();
                if (img == null)
                {
                    GeneratePreviewImage();
                    img = GetPreviewImage();
                }
                return img;
            }
        }
    }
}