using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace StatistikaProjekata.LAMS
{
    [Serializable]
    [XmlRoot(ElementName = "tool")]
    public class Tool
    {
        public Tool()
        {}
        
        public Tool(string toolFile)
        {
            ToolFile = toolFile;
        }

        [XmlElement(ElementName = "toolFile")]
        public string ToolFile { get; set; }
    }
  
    [Serializable, XmlRoot(ElementName = "object")]
    public class LamsToolFileInfo
    {
        public LamsToolFileInfo()
        {
            Tool = new List<Tool>();
        }

        [XmlElement(ElementName = "tool")]
        public List<Tool> Tool { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }
    }

    [Serializable]
    [XmlRoot(ElementName = "objects")]
    public class LamsToolFileList
    {
        public LamsToolFileList()
        {
            ToolFileInfo = new List<LamsToolFileInfo>();
        }

        [XmlElement(ElementName = "object")]
        public List<LamsToolFileInfo> ToolFileInfo { get; set; }
    }
}
