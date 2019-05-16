using System.Collections.Generic;

namespace mDitaEditor.Repository
{
    public class Params
    {
        public string indent { get; set; }
        public string q { get; set; }
        public string wt { get; set; }
    }

    public class ResponseHeader
    {
        public int status { get; set; }
        public int QTime { get; set; }
        public Params @params { get; set; }
    }

    public class Doc
    {
        public string id { get; set; }
        public List<string> content { get; set; }
        public object _version_ { get; set; }
    }

    public class Response
    {
        public int numFound { get; set; }
        public int start { get; set; }
        public List<Doc> docs { get; set; }
    }

    public class SolrRootObject
    {
        public ResponseHeader responseHeader { get; set; }
        public Response response { get; set; }
    }
}
