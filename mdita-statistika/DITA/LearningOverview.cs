using System.Drawing;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StatistikaProjekata.DITA
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
                Section = new ListSection()
            };
        }
      

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        public override string FileNamePpt
        {
            get { return "pptlo"; }
        }



        public override string TitleDescription
        {
            get { return "UVOD"; }
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