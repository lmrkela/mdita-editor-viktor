using System;

namespace mdita_update
{
    public class MditaVersion
    {
        public long Id { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
        public string Changelog { get; set; }
    }
}
