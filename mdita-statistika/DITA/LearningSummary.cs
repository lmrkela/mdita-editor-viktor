using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    [XmlRoot(ElementName = "learningSummary")]
    public class LearningSummary : LearningBase
    {
        [XmlElement(ElementName = "title")]
        public string Title { get; set; }

        [XmlElement(ElementName = "learningSummarybody")]
        public LearningBody LearningSummarybody
        {
            get
            {
                return LearningBody;
            }
            set { LearningBody = value; }

        }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        public LearningSummary()
        {
            Title = "Zaključak";
            LearningSummarybody = new LearningBody
            {
                Section = new ListSection()
            };
            AddSummarySection();
            Id = "LS-01";
        }

        public override string FileNamePpt
        {
            get
            {
                var objects = Project.LearningContents;
                int index = objects.Count + 1;
                foreach (var o in objects)
                {
                    index += o.SubObjects.Count;
                }
                return "pptls" + index;
            }
        }

        public void AddSummarySection()
        {
            var section = new Section(this, "");
            section.Title = "Zaključak";
            var subtitle = new Sectiondiv("subtitle");
            subtitle.Content = "";
            var zakljucakDiv = new Sectiondiv("columns1");
            section.Sectiondiv.Add(subtitle);
            section.Sectiondiv.Add(zakljucakDiv);
            var lmrc = new Sectiondiv("lmrc");
            zakljucakDiv.SectionDiv.Add(lmrc);
        }


        public override string TitleDescription
        {
            get { return "ZAKLJUČAK"; }
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
                return (LearningSummary)formatter.Deserialize(ms);
            }
        }

        public override string ToString()
        {
            return "Zaključak";
        }
    }
}
