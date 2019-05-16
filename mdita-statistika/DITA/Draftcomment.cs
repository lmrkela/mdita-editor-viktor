using System;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace StatistikaProjekata.DITA
{
    [Serializable]
    [XmlRoot(ElementName = "draft-comment")]
    public class Draftcomment
    {
        [XmlAttribute(AttributeName = "disposition")]
        public string Disposition { get; set; }


        private string text;

        [XmlText]
        public string Text
        {
            get { return text; }
            set {
                if (value == null) value = "";
                text = Regex.Replace(value, @"\s+", " "); }
        }



        public Draftcomment()
        {
            Text = "";
        }

        public Draftcomment(string disposition, string text)
        {
            Disposition = disposition;
            Text = text;
        }

        public Draftcomment Clone()
        {
            var d = new Draftcomment();
            d.text = text;
            d.Disposition = Disposition;
            return d;
        }
    }
}