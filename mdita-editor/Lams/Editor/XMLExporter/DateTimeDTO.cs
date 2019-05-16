using System;
using System.Xml.Serialization;

namespace mDitaEditor.Lams.Editor.XMLExporter
{
    [Serializable]
    public class DateTimeDTO
    {
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }

        [XmlText]
        public string Text { get; set; }

        public DateTimeDTO()
        {
            Class = "sql-timestamp";
            Text = "2016-04-20 04:20:00.0";
        }
    }
}
