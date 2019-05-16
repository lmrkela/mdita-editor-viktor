using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mDitaEditor.Dita.Controls
{
    class TextBoxMemory : TextBox
    {

        string savedText = "Slika-1 ";

        public void updateSaved()
        {
            if (Text.Trim() != "")
            {
                savedText = Text;
            }
        }

        public string SavedText 
        {
            get
            {
                return savedText;
            }

            set
            {
                savedText = value;
            }
        }

    }
}
