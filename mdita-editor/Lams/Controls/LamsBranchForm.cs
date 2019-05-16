using mDitaEditor.Lams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mDitaEditor.LAMS.Controls
{
    public partial class LamsBranchForm : Form
    {
        public LamsBranch Branch { get; set; }

        public LamsBranchForm(LamsBranch branch)
        {
            InitializeComponent();
            Branch = branch;
        }
    }
}
