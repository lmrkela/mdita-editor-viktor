using System;
using System.IO;
using System.Windows.Forms;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Utils;
using System.Diagnostics;

namespace mDitaEditor.Dita.Forms
{
    public partial class InsertLatexForm : Form
    {
        public SelectableFlowPanel Panel { get; set; }
        public bool isEdit = false;
        public LatexControl Eq { get; set; }
        public InsertLatexForm()
        {
            InitializeComponent();
        }
        public string oldLatex { get; set; }
        private Timer t = new Timer();

        public bool isFail = false;
        public InsertLatexForm(string latex, LatexControl eqControl, bool isFail = false)
        {
            InitializeComponent();
            txtInsertFormula.Text = latex;
            oldLatex = latex;
            LoadPreview();
            isEdit = true;
            this.isFail = isFail;
            Eq = eqControl;
            InitTimer();
        }

        public void InitTimer()
        {

            t.Tick += t_Tick;
            t.Interval = 250;
        }

        void t_Tick(object sender, EventArgs e)
        {
            t.Stop();
            LoadPreview();            
        }

        public InsertLatexForm(SelectableFlowPanel panel1, string LaTeX = null)
        {
            InitializeComponent();
            Panel = panel1;
            InitTimer();
            if(LaTeX != null)
            {
                txtInsertFormula.Text = LaTeX;
                LoadPreview();
            }
        }
        public void LoadPreview()
        {

            var uri = new Uri(Path.GetFullPath("style.css"));
            var converted = uri.AbsoluteUri;
            string doc = Util.LatexWebForCode(txtInsertFormula.Text);
                      
            webPreviewLatex.DocumentText = doc;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPreview();
        }

        private void InsertLatex_Load(object sender, EventArgs e)
        {
        }

        private void btnInsertFormula_Click(object sender, EventArgs e)
        {
            // TODO: ADD STATE
            if (!isEdit)
            {
                ControlFactory.getEquationForPanel(Panel, txtInsertFormula.Text);
                Close();
            }
            else
            {
                Eq.DocumentText = Util.LatexWebForCode(txtInsertFormula.Text);
                string xmlBefore = Eq.GetXmlForElement();
                Eq.LaTeX = txtInsertFormula.Text;
                if (!isFail)
                {
                    Eq.oldLatex = oldLatex;
                }
                Eq.showMessageUpdate = true;
                Eq.UpdateLaTex();
                string xmlAfter = Eq.GetXmlForElement();
                DitaClipboard.UpdateLatexUndoState(Eq.rootSectionDiv, xmlBefore, xmlAfter);
                Close();
            }
            webPreviewLatex.Dispose();
            this.Dispose();
        }

        private void opstiButton_Click(object sender, EventArgs e)
        {
            RibbonButton btnKliknut = (RibbonButton)sender;
            string text = (string)btnKliknut.Tag;
            txtInsertFormula.SelectedText += text;
        }


        //Razmak izmedju dva dogadjaja manji od 250 msec nece da zahteva generisanje preview-a.
        private void txtInsertFormula_TextChanged(object sender, EventArgs e)
        {
            //Resets timer
            t.Stop();
            t.Start();
           
        }

        private void ribbonButton123_Click(object sender, EventArgs e)
        {
            if (!txtInsertFormula.Text.Contains("=\\gbreak")) {
                txtInsertFormula.Text = txtInsertFormula.Text.Replace("=", "=\\gbreak ");
            }
            MessageBox.Show("Na svaki znak jednakosti je dodat \\gbreak parametar");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadPreview();
        }
    }
}
