
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    [XmlRoot(ElementName = "learningContent")]
    public class LearningContent : LearningBase
    {
        private bool _changed = true;


        public static readonly string[] DraftDispositions = { "Author", "SchoolYear", "Classification", "Difficulty level", "Keywords", "Audience", "Learning duration" };

        public override string TitleDescription
        {
            get { return Id + ((Parent != null) ? "/" + (Parent.SubObjects.IndexOf(this) + 1) : ""); }
        }

     
        private Image _previewImage;

        private bool _generating;

      


        private string _title;
        [XmlElement(ElementName = "title")]
        public string Title
        {
            get { return _title; }
            set
            {
                if (value == _title)
                {
                    return;
                }
                _title = value;

                _changed = true;
            }
        }

        public override string FileNamePpt
        {
            get
            {
                var objects = Project.LearningContents;
                int index;
                int endIndex;
                if (Parent != null)
                {
                    endIndex = objects.IndexOf(Parent);
                    index = endIndex + Parent.SubObjects.IndexOf(this) + 2;
                }
                else
                {
                    endIndex = objects.IndexOf(this);
                    index = endIndex + 1;
                }
                for (int i = 0; i < endIndex; ++i)
                {
                    var o = objects[i];
                    index += o.SubObjects.Count;
                }
                return "pptlc" + index;
            }
        }
        public override string GetTitle()
        {
            return TitleDescription + " - " + Title;
        }

        [XmlElement(ElementName = "shortdesc")]
        public Shortdesc Shortdesc { get; set; }

        [XmlElement(ElementName = "learningContentbody")]
        public LearningBody LearningContentBody
        {
            get { return LearningBody; }
            set { LearningBody = value; }
        }

        [XmlIgnore]
        public string lesson
        {
            get; set;
        }

        [XmlIgnore]
        public string url
        {
            get; set;
        }

        [XmlIgnore]
        public LearningContent Parent { get; set; }

        [XmlIgnore]
        private string _id;
        [XmlAttribute(AttributeName = "id")]
        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id)
                {
                    return;
                }

                _id = value;
                foreach (var learningContent in SubObjects)
                {
                    learningContent.Id = value;
                }

                _changed = true;
            }
        }

        [XmlIgnore]
        public List<LearningContent> SubObjects { get; private set; }


        public LearningContent()
        {
            SubObjects = new List<LearningContent>();
            Title = "";
        }

        /// <summary>
        /// Kreira prazan LearningContent sa upisanim neophodnim informacijama za prikaz slajda
        /// </summary>
        /// <param name="isObject"></param>
        public LearningContent(LearningContent parent,ProjectFile project, int index = -1) : this()
        {

            if (parent == null)
            {
                Id = LastIdIncrementOrSame(true, project);
            }
            else
            {
                if (parent.Parent != null)
                {
                    parent = parent.Parent;
                }
                Parent = parent;
                Id = Parent.Id;
            }

            LearningContentBody = new LearningBody
            {
                LcObjectives = new LcObjectives
                {
                    LcObjectivesGroup = new LcObjectivesGroup
                    {
                        LcObjective = new List<string>()
                    }
                }
            };
            LearningContentBody.Section = new ListSection();
            Shortdesc = new Shortdesc
            {
                Draftcomment = new List<Draftcomment>()
            };
            for (var i = 0; i < DraftDispositions.Length; i++)
            {
                var draf = new Draftcomment
                {
                    Text = "",
                    Disposition = DraftDispositions[i]
                };
                Shortdesc.Draftcomment.Add(draf);
            }
            if (parent == null)
            {
                if (index < 0 || index >= project.LearningContents.Count)
                {
                    project.LearningContents.Add(this);
                }
                else
                {
                    project.LearningContents.Insert(index, this);

                }
            }
            else
            {
                if (index < 0 || index >= parent.SubObjects.Count)
                {
                    parent.SubObjects.Add(this);
                }
                else
                {
                    parent.SubObjects.Insert(index, this);
                }
            }
        }

        public void Dispose()
        {
            if (_previewImage != null)
            {
                _previewImage.Dispose();
                _previewImage = null;
            }
            foreach (var section in LearningContentBody.Section)
            {
                section.Dispose();
            }

            foreach (var learningContent in SubObjects)
            {
                learningContent.Dispose();
            }
        }

        public void Delete(ProjectFile file)
        {
            if (Parent != null)
            {
                Parent.SubObjects.Remove(this);
            }
            else
            {
                file.LearningContents.Remove(this);
            }
            Dispose();
        }


        /// <summary>
        /// Radi decrement Id-ja
        /// </summary>
        public void DecrementId()
        {
            var i = int.Parse(Id.Split('-')[1]) - 1;
            Id = string.Format("LC-{0:D2}", i);
        }

        /// <summary>
        /// Radi decrement Id-ja
        /// </summary>
        public void IncrementId()
        {
            var i = int.Parse(Id.Split('-')[1]) + 1;
            Id = string.Format("LC-{0:D2}", i);
        }


        /// <summary>
        /// Radi increment zadnjeg ID-ja ili ukoliko je u pitanju podobjekat
        /// stavlja isti ID kao zadnji
        /// </summary>
        /// <param name="isObject"></param>
        /// <returns></returns>
        public static string LastIdIncrementOrSame(bool isObject, ProjectFile file)
        {
            List<LearningContent> lista = file.LearningContents;
            if (lista.Count > 0)
            {
                var poslednji = lista[lista.Count - 1];
                var poslednjiId = int.Parse(poslednji.Id.Split('-')[1]);
                if (isObject)
                {
                    poslednjiId++;
                }
                return string.Format("LC-{0:D2}", poslednjiId);
            }
            return "LC-01";
        }

        /// <summary>
        /// Klonira sekciju preko serializacije
        /// </summary>
        /// <returns></returns>
        public override LearningBase Clone()
        {
            var content = new LearningContent();
            content.Title = Title;
            content.Shortdesc = Shortdesc.Clone();
            content.LearningContentBody = LearningContentBody.Clone();
            content.lesson = lesson;
            content.url = url;
            content.Parent = null;
            content.Id = Id;
            content.SubObjects = new List<LearningContent>(SubObjects.Count);
            foreach (var learningContent in SubObjects)
            {
                var sub = (LearningContent)learningContent.Clone();
                sub.Parent = this;
                content.SubObjects.Add(sub);
            }
            return content;
        }

        public LearningBase DeepClone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return (LearningBase)formatter.Deserialize(ms);
            }
        }

        /// <summary>
        /// Vraca Sifru objekta + njegov title ako je dostupan
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Parent == null)
            {
                return Id + " - " + Title;
            }
            else
            {
                return Id + " - Sub - " + Title;
            }
        }

        public void MoveTo(LearningContent destination, int index)
        {
            Parent.SubObjects.Remove(this);
            Parent = destination;
            Id = destination.Id;
            destination.SubObjects.Insert(index, this);
        }

   
        public void MoveTo(int index, ProjectFile file)
        {
            var list = file.LearningContents;

            list.Remove(this);
            list.Insert(index, this);

            for (var i = 0; i < list.Count; ++i)
            {
                list[i].Id = Util.GetLearningContentIdForLesson(i + 1);
            }
        }
    }
}
