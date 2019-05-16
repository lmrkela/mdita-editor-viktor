using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mDitaEditor.Project
{
    public class DitaObjectCannotBeLoadedException : Exception
    {
        public DitaObjectCannotBeLoadedException()
        {
        }
        public DitaObjectCannotBeLoadedException(string message) : base(message) { }
        public DitaObjectCannotBeLoadedException(string message, System.Exception inner) : base(message, inner) { }
    }
}
