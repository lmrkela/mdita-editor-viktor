using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Xml.Serialization;

namespace StatistikaProjekata.LAMS
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
