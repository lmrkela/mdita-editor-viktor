using System.Drawing;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using mDitaEditor.Properties;
using mDitaEditor.Project;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace mDitaEditor.Dita
{
    [Serializable]
    [XmlRoot(ElementName = "learningOverview")]
    public class LearningOverview : LearningBase
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "learningOverviewbody")]
        public LearningBody LearningOverviewbody
        {
            get
            {
                return LearningBody;
            }
            set { LearningBody = value; }
        }

        public LearningOverview()
        {
            LearningOverviewbody = new LearningBody
            {
                Sections = new SectionList()
            };
        }
      

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }


        private static Image _previewImage = Resources.uvod;

        public override string TitleDescription
        {
            get { return "UVOD"; }
        }

        public override string FileNamePpt
        {
            get { return "pptlo"; }
        }

        public override Image GetPreviewImage()
        {
            return _previewImage;
        }

        public override bool HasPreviewImage()
        {
            return _previewImage != null;
        }

        public override void GeneratePreviewImage()
        { }

        public override bool CanMove(bool up)
        {
            return false;
        }

        /// <summary>
        /// Klonira sekciju preko serializacije
        /// </summary>
        /// <returns></returns>
        public override LearningBase Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (LearningOverview)formatter.Deserialize(ms);
            }
        }
    }
}