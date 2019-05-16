using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mDitaEditor.Project
{
    class ImageDeleted
    {
        private Image image;

        public Image Image
        {
            get { return image; }
            set { image = value; }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; }
        }
    }
}
