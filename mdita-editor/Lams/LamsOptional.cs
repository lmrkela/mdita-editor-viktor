using System.Collections.Generic;
using System.Drawing;
using mDitaEditor.Lams.Editor;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams
{
    public class LamsOptional : IGrafikaObject
    {
        public string TitleText { get; set; }

        public Image Icon { get { return Resources.additional_activity; } }

        public List<IGrafikaObject> SubObjects { get; private set; } 

        public LamsOptional()
        {
            TitleText = "Optional Activity";
            SubObjects = new List<IGrafikaObject>();
        }
    }
}
