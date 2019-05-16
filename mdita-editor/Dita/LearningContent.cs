using mDitaEditor.Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using mDitaEditor.Properties;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita
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

        public override string FileNamePpt
        {
            get
            {
                var objects = ProjectSingleton.Project.LearningContents;
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

        private Image _previewImage;

        private bool _generating;

        /// <summary>
        /// Daje Preview sliku za objekat
        /// </summary>
        /// <returns></returns>
        public override Image GetPreviewImage()
        {
            if (_generating)
            {
                return null;
            }
            return _previewImage;
        }


        /// <summary>
        /// Proverava da li objekat trenutno ima sliku za levi meni
        /// </summary>
        /// <returns></returns>
        public override bool HasPreviewImage()
        {
            return !_changed;
        }

        private static readonly LearningContentControl PreviewControl = new LearningContentControl(null);

        /// <summary>
        /// Metoda koja generiše Preview sliku za meni
        /// </summary>
        public override void GeneratePreviewImage()
        {
            _generating = true;

            _previewImage?.Dispose();
            PreviewControl.Content = this;
            _previewImage = GuiUtil.DrawControlToImage(PreviewControl);

            _changed = false;

            _generating = false;
        }



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
        public LearningContent(LearningContent parent, int index = -1) : this()
        {

            if (parent == null)
            {
                Id = LastIdIncrementOrSame(true);
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
            LearningContentBody.Sections = new SectionList();
            Shortdesc = new Shortdesc
            {
                Draftcomment = new List<Draftcomment>()
            };
            for (int i = 0; i < DraftDispositions.Length; i++)
            {
                Draftcomment draf = new Draftcomment
                {
                    Text = "",
                    Disposition = DraftDispositions[i]
                };
                Shortdesc.Draftcomment.Add(draf);
            }
            if (parent == null)
            {
                if (index < 0 || index >= Project.LearningContents.Count)
                {
                    Project.LearningContents.Add(this);
                }
                else
                {
                    Project.LearningContents.Insert(index, this);

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
            LearningContentList.FixDraftComments(this);
        }

        public void Dispose()
        {
            if (_previewImage != null)
            {
                _previewImage.Dispose();
                _previewImage = null;
            }
            foreach (var section in LearningContentBody.Sections)
            {
                section.Dispose();
            }

            foreach (var learningContent in SubObjects)
            {
                learningContent.Dispose();
            }
        }

        public void Delete()
        {
            if (Parent != null)
            {
                Parent.SubObjects.Remove(this);
            }
            else
            {
                Project.LearningContents.Remove(this);
            }
            Dispose();
        }


        /// <summary>
        /// Radi decrement Id-ja
        /// </summary>
        public void DecrementId()
        {
            int i = int.Parse(this.Id.Split('-')[1]) - 1;
            this.Id = string.Format("LC-{0:D2}", i);
        }

        /// <summary>
        /// Radi decrement Id-ja
        /// </summary>
        public void IncrementId()
        {
            int i = int.Parse(this.Id.Split('-')[1]) + 1;
            this.Id = string.Format("LC-{0:D2}", i);
        }


        /// <summary>
        /// Radi increment zadnjeg ID-ja ili ukoliko je u pitanju podobjekat
        /// stavlja isti ID kao zadnji
        /// </summary>
        /// <param name="isObject"></param>
        /// <returns></returns>
        public static string LastIdIncrementOrSame(bool isObject)
        {
            List<LearningContent> lista = ProjectSingleton.Project.LearningContents;
            if (lista.Count > 0)
            {
                LearningContent poslednji = lista[lista.Count - 1];
                int poslednjiId = int.Parse(poslednji.Id.Split('-')[1]);
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
            var content = new LearningContent
            {
                Project = Project,
                Title = Title,
                Shortdesc = Shortdesc.Clone(),
                LearningContentBody = LearningContentBody.Clone(),
                lesson = lesson,
                url = url,
                Parent = null,
                Id = Id,
                SubObjects = new List<LearningContent>(SubObjects.Count)
            };
            foreach (var learningContent in SubObjects)
            {
                LearningContent sub = (LearningContent)learningContent.Clone();
                sub.Parent = content;
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

        public override bool CanMove(bool up)
        {
            List<LearningContent> list;
            if (Parent == null)
            {
                list = Project.LearningContents;
            }
            else
            {
                list = Parent.SubObjects;
            }
            if (up)
            {
                return list.IndexOf(this) > 0;
            }
            else
            {
                return list.IndexOf(this) < list.Count - 1;
            }
        }

        public void MoveTo(int index)
        {
            var list = Project.LearningContents;

            list.Remove(this);
            list.Insert(index, this);

            for (int i = 0; i < list.Count; ++i)
            {
                list[i].Id = Util.GetLearningContentIdForLesson(i + 1);
            }
        }
    }
}
