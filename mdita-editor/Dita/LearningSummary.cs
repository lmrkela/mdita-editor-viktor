using System.Drawing;
using mDitaEditor.Project;
using System.Xml;
using System.Xml.Serialization;
using mDitaEditor.Properties;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace mDitaEditor.Dita
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
                Sections = new SectionList()
            };
            AddSummarySection();
            Id = "LS-01";

        }

        public void AddSummarySection()
        {
            Section section = new Section(this, "");
            section.Title = "Zaključak";
            Sectiondiv subtitle = new Sectiondiv("subtitle");
            subtitle.Content = "";
            Sectiondiv zakljucakDiv = new Sectiondiv("columns1");
            section.SectionDivs.Add(subtitle);
            section.SectionDivs.Add(zakljucakDiv);
            Sectiondiv lmrc = new Sectiondiv("lmrc");
            zakljucakDiv.SectionDivs.Add(lmrc);
        }

        private static Image _previewImage = Resources.zakljucak;

        public override string TitleDescription
        {
            get { return "ZAKLJUČAK"; }
        }

        public override string FileNamePpt
        {
            get
            {
                var objects = ProjectSingleton.Project.LearningContents;
                int index = objects.Count + 1;
                foreach (var o in objects)
                {
                    index += o.SubObjects.Count;
                }
                return "pptls" + index;
            }
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
                return (LearningSummary)formatter.Deserialize(ms);
            }
        }

        public override string ToString()
        {
            return "Zaključak";
        }
    }
}
