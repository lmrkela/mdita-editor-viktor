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
    public partial class LamsGateForm : Form
    {
        public LamsGate Gate { get; set; }

        public LamsGateForm(LamsGate gate)
        {
            InitializeComponent();
            Gate = gate;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
