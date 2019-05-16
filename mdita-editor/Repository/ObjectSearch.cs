using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using mDitaEditor.Dita;
using mDitaEditor.Utils;

namespace mDitaEditor.Repository
{
    public class ObjectSearch
    {
        /// <summary>
        /// Getuje listu objekata sa interneta
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static List<LearningContent> GetListOfObjects(string query)
        {
            List<LearningContent> listOfObjects = new List<LearningContent>();
            string json = "";
            using (WebClient wc = new WebClient())
            {
                wc.UseDefaultCredentials = true;
                wc.Credentials = new NetworkCredential("admin", "caija833dajdfewf77");
                var queryMsg = Uri.EscapeDataString(query);
                try
                {
                    json =
                        wc.DownloadString("http://mdita.metropolitan.ac.rs:8983/solr/objekti_znanja/select?q=" +
                                          queryMsg + "&wt=json&indent=true&rows=10");
                }
                catch
                {
                    MessageBox.Show("Niste uneli validan  ili nemate internet konekciju");
                    return listOfObjects;
                }
            }
            SolrRootObject x = Newtonsoft.Json.JsonConvert.DeserializeObject<SolrRootObject>(json);
            if (x?.response.docs == null)
            {
                return listOfObjects;
            }
            foreach (Doc doc in x.response.docs)
            {
                string newid = doc.id.Replace("\\", "/");
                string url = "http://mdita.metropolitan.ac.rs/qdita-temp/" + newid;
                try
                {
                    AddObjectData(url, listOfObjects, newid);
                }
                catch
                {
                }
            }
            return listOfObjects;
        }

        /// <summary>
        /// Dodaje na listu objekata ucitan objekat sa neta
        /// </summary>
        /// <param name="url"></param>
        /// <param name="objectsData"></param>
        public static void AddObjectData(string url, List<LearningContent> objectsData, string newid)
        {
            string xml = "";
            using (WebClient wc = new WebClient())
            {
                xml = wc.DownloadString(url);
                XDocument doc = null;
                var settings = new XmlReaderSettings();
                settings.NameTable = new NameTable();
                settings.DtdProcessing = DtdProcessing.Ignore;
                var manager = new MyXmlNamespaceManager(settings.NameTable);
                XmlParserContext context = new XmlParserContext(null, manager, null, XmlSpace.Default);
                Uri uri = new Uri(url);
                string filename = "";
                filename = System.IO.Path.GetFileName(uri.AbsolutePath);
                string lessonnum = filename.Split('-')[1];
                using (XmlReader reader = XmlReader.Create(new StringReader(xml), settings, context))
                {
                    doc = XDocument.Load(reader);
                }
                var root = doc.Root;
                foreach (XElement element in root.Descendants("sectiondiv"))
                {
                    if (Util.isLastLevel(element))
                    {
                        string content = Util.RemoveSectionDivTag(element.ToString());
                        if (content.Length > 0)
                        {
                            element.Value = content;
                        }
                    }
                }
                if (doc.Root.Name.LocalName == "learningContent")
                {
                    LearningContent learning = (LearningContent) Util.FromXml(doc.ToString(), typeof (LearningContent));
                    learning.url = url;
                    if (learning.Shortdesc.Draftcomment.Count < 7)
                    {
                        learning.Shortdesc.Draftcomment.Insert(1, new Draftcomment("SchoolYear", "Undefined"));
                    }
                    foreach (Section sec in learning.LearningBody.Sections)
                    {
                        sec.Parent = learning;
                        foreach (Sectiondiv div in sec.SectionDivs)
                        {
                            div.UrlTo = newid;
                            foreach (Sectiondiv divdiv in div.SectionDivs)
                            {
                                divdiv.UrlTo = newid;

                                foreach (Sectiondiv divdivdiv in divdiv.SectionDivs)
                                {
                                    divdivdiv.UrlTo = newid;
                                }
                            }
                        }
                    }
                    learning.lesson = lessonnum;
                    objectsData.Add(learning);
                }
            }
        }
    }
}
