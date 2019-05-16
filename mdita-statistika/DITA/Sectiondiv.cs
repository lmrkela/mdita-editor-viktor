using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    [XmlRoot(ElementName = "sectiondiv")]
    public class Sectiondiv 
    {
        [XmlAttribute(AttributeName = "outputclass")]
        public string Outputclass { get; set; }

        private string content;
        [XmlText]
        public string Content
        {
            get { return content; }
            set
            {
                content = (Outputclass != null && Outputclass == "subtitle") ? Regex.Replace(value.Normalize(), @"<[^>]*>", string.Empty) : value.Normalize();
           }
        }

        [XmlElement(ElementName = "sectiondiv")]
        public List<Sectiondiv> SectionDiv { get; set; }


        public Sectiondiv()
        {
            SectionDiv = new List<Sectiondiv>();
            Content = "";
        }

        public Sectiondiv(string _outputclass) : this()
        {
            Outputclass = _outputclass;
        }


        /// <summary>
        /// Klonira objekat preko serializacije
        /// </summary>
        /// <returns></returns>
        public Sectiondiv Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;
                return (Sectiondiv)formatter.Deserialize(ms);
            }
        }

        [XmlIgnore]
        public string UrlTo { get; set; }

        /// <summary>
        /// Proverava da li element vec postoji u listi
        /// </summary>
        /// <param name="sectionDivList"></param>
        /// <param name="outputclass"></param>
        /// <returns></returns>
        private bool IsELementInList(string outputclass)
        {
            var div = SectionDiv.Find(x => x.Outputclass == outputclass);
            return div != null;
        }

        /// <summary>
        /// Dodaje sekcije na panel po prosledjenoj output klasi
        /// </summary>
        public void AddSections()
        {
            switch (Outputclass)
            {
                case "columns1":
                    if (!IsELementInList( "lmrc"))
                        SectionDiv.Add(new Sectiondiv("lmrc"));
                    break;
                case "columns2":
                    if (!IsELementInList("lmc2"))
                        SectionDiv.Add(new Sectiondiv("lmc2"));
                    if (!IsELementInList("mrc2"))
                        SectionDiv.Add(new Sectiondiv("mrc2"));
                    break;
                case "columns2-2-1":
                    if (!IsELementInList("lmc"))
                        SectionDiv.Add(new Sectiondiv("lmc"));
                    if (!IsELementInList("rc"))
                        SectionDiv.Add(new Sectiondiv("rc"));
                    break;
                case "columns2-1-2":
                    if (!IsELementInList("lc"))
                        SectionDiv.Add(new Sectiondiv("lc"));
                    if (!IsELementInList("mrc"))
                        SectionDiv.Add(new Sectiondiv("mrc"));
                    break;
                case "columns3":
                    if (!IsELementInList("lc"))
                        SectionDiv.Add(new Sectiondiv("lc"));
                    if (!IsELementInList("mc"))
                        SectionDiv.Add(new Sectiondiv("mc"));
                    if (!IsELementInList("rc"))
                        SectionDiv.Add(new Sectiondiv("rc"));
                    break;
            }
        }
    }
}