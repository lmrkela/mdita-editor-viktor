using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace mDitaEditor.LAMS
{   
    [Serializable]
    class LamsDate
    {

        public LamsDate()
        {
            this.Class = "sql-timestamp";
            //this.Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.f");
            this.Date = DateTime.Now;
        }
        [XmlAttribute(AttributeName = "class")]
        public string Class { get; set; }
        [XmlText]
        public DateTime Date { get; set; }

    }
}
