using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace mDitaEditor.Dita
{
    public interface IDitaSlide
    {
        string GetTitle();

        Image GetPreviewImage();

        bool HasPreviewImage();

        void GeneratePreviewImage();

        bool CanMove(bool up);
    }
}
