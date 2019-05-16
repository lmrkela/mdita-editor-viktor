using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using mDitaEditor.Properties;
using System.Web.Script.Serialization;
using mDitaEditor.Dita.Controls;

namespace mDitaEditor.Dita
{
    [Serializable]
    [XmlRoot(ElementName = "section")]
    public class Section : IDitaSlide
    {
        private string title;

        [XmlElement(ElementName = "title")]
        public string Title
        {
            get { return title; }
            set
            {
                title = Regex.Replace(value, @"\s+", " ");
                title = Regex.Replace(title, @"<[^>]*>", string.Empty);
            }
        }


        [XmlElement(ElementName = "sectiondiv")]
        public List<Sectiondiv> SectionDivs { get; set; }

        [XmlAttribute(AttributeName = "outputclass")]
        public string Outputclass { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        private string content;
        [XmlText]
        public string Content
        {
            get { return content; }
            set { content = value.Normalize(); }
        }
        [ScriptIgnore]
        [XmlIgnore]
        public LearningBase Parent { get; set; }

        //[XmlIgnore]
        //public int UndoIndex { get; set; }

        //[XmlIgnore]
        //public int UndoDelete { get; set; }

        public Section()
        {
            SectionDivs = new List<Sectiondiv>();
        }

        public Section(LearningBase parent, string outputclass, int index = -1) : this()
        {
            Parent = parent;
            Title = "";
            Outputclass = outputclass;
            if (index >= 0)
            {
                Parent.LearningBody.Sections.Insert(index, this);
            }
            else
            {
                Parent.LearningBody.Sections.Add(this);
            }
        }

        /// <summary>
        /// Klonira sekciju preko serializacije
        /// </summary>
        /// <returns></returns>
        public Section Clone()
        {
            Section section = new Section();
            section.title = title;
            section.SectionDivs = new List<Sectiondiv>(SectionDivs.Count);
            foreach (var sectiondiv in SectionDivs)
            {
                section.SectionDivs.Add(sectiondiv.Clone());
            }
            section.Outputclass = Outputclass;
            section.Id = Id;
            section.content = content;
            return section;
            //using (var ms = new MemoryStream())
            //{
            //    var formatter = new BinaryFormatter();
            //    formatter.Serialize(ms, this);
            //    ms.Position = 0;
            //    return (Section)formatter.Deserialize(ms);
            //}
        }

        public bool ChangeLayout(string layout)
        {
            int indexObject = (SectionDivs.Count == 1) ? 0 : 1;
            var sectiondiv = SectionDivs[indexObject];

            var previousOutputclass = sectiondiv.Outputclass;
            if (previousOutputclass == layout)
            {
                return false;
            }

            Outputclass = "section-" + layout;
            sectiondiv.Outputclass = layout;

            var oldSections = sectiondiv.SectionDivs;
            sectiondiv.SectionDivs = new List<Sectiondiv>();

            sectiondiv.AddSections();
            for (int i = 0, j = 0; i < oldSections.Count; i++)
            {
                var newSectiondiv = sectiondiv.SectionDivs[j].SectionDivs;
                foreach (var oldSectiondiv in oldSections[i].SectionDivs)
                {
                    newSectiondiv.Add(oldSectiondiv);
                }
                if (++j >= sectiondiv.SectionDivs.Count)
                {
                    j = sectiondiv.SectionDivs.Count - 1;
                }
            }

            LearningSectionControl control = new LearningSectionControl(this);
            foreach (var selectablePanel in control._panels)
            {
                if (selectablePanel != null && selectablePanel.HeightLeftPanel() < 0)
                {
                    Outputclass = "section-" + previousOutputclass;
                    sectiondiv.Outputclass = previousOutputclass;
                    sectiondiv.SectionDivs = oldSections;
                    control.Dispose();
                    return false;
                }
            }
            control.Dispose();
            return true;
        }

        public string GetTitle()
        {
            return Title;
        }

        private Image _previewImage;

        private bool _generating;


        public Image GetPreviewImage()
        {
            if (_generating)
            {
                return null;
            }
            return _previewImage;
        }

        public bool HasPreviewImage()
        {
            return _previewImage != null;
        }

        private Bitmap getBitmapForSection()
        {
            Bitmap bit = null;
            int indexObject = SectionDivs.Count - 1;
            SectionDivs[indexObject].AddSections();
            switch (SectionDivs[indexObject].Outputclass)
            {
                case "columns1":
                    // Provera da li je galerija
                    if (SectionDivs[indexObject].SectionDivs[0].SectionDivs.Count == 1 &&
                        SectionDivs[indexObject].SectionDivs[0].SectionDivs[0].Outputclass == "flexslider")
                    {
                        bit = Resources.columnsGallery;
                    }
                    else
                    {
                        bit = Resources.columns1;
                    }
                    break;
                case "columns2":
                    bit = Resources.columns2;
                    break;
                case "columns3":
                    bit = Resources.columns3;
                    break;
                case "columns2-2-1":
                    bit = Resources.columns21;
                    break;
                case "columns2-1-2":
                    bit = Resources.columns12;
                    break;
            }
            return bit;
        }

        //private static readonly LearningSectionControl PreviewControl = new LearningSectionControl();

        public void GeneratePreviewImage()
        {
            _generating = true;

            _previewImage = getBitmapForSection();

            //var control = MainForm.Instance.sectionControl;
            //if (control.Section != this)
            //{
            //    _previewImage?.Dispose();
            //    _previewImage = getBitmapForSection();
            //    //control = PreviewControl;
            //    //control.Section = this;
            //    //foreach (var browser in control.Browsers)
            //    //{
            //    //    while (browser.ReadyState != WebBrowserReadyState.Complete)
            //    //    {
            //    //        Application.DoEvents();
            //    //    }
            //    //}
            //    //for (int i = 0; i < 10; ++i)
            //    //{
            //    //    Application.DoEvents();
            //    //    Thread.Sleep(10);
            //    //}
            //}
            //else
            //{
            //    if (control.Browsers.Count == 0)
            //    {
            //        _previewImage?.Dispose();
            //        _previewImage = GuiUtil.DrawControlToImage(control);
            //    }
            //    else
            //    {
            //        Debug.WriteLine("BROWSERS " + control.Browsers.Count);
            //    }
            //}

            //var buffer = getBitmapForSection();
            //using (Graphics g = Graphics.FromImage(buffer))
            //{
            //    Font f = new Font("Arial Narrow", 16f);
            //    Color c = Color.FromArgb(167, 5, 50);
            //    Brush brush = new SolidBrush(c);
            //    Rectangle rect = new Rectangle(3, 3, buffer.Width - 6, buffer.Height - 6);
            //    StringFormat sf = new StringFormat
            //    {
            //        LineAlignment = StringAlignment.Center,
            //        Alignment = StringAlignment.Center
            //    };
            //    g.DrawString(Title.Trim(), f, brush, rect, sf);
            //}
            //_previewImage = buffer;

            _generating = false;
        }

        public bool CanMove(bool up)
        {
            if (up)
            {
                return Parent.LearningBody.Sections.IndexOf(this) > 0;
            }
            else
            {
                return Parent.LearningBody.Sections.IndexOf(this) < Parent.LearningBody.Sections.Count - 1;
            }
        }

        public void Dispose()
        {
            if (_previewImage != null)
            {
                _previewImage.Dispose();
                _previewImage = null;
            }
        }

        public void Delete()
        {
            Parent.LearningBody.Sections.Remove(this);
            Dispose();
        }

        public void MoveTo(LearningBase destination, int index)
        {
            Parent.LearningBody.Sections.Remove(this);
            Parent = destination;
            destination.LearningBody.Sections.Insert(index, this);
        }
    }
}