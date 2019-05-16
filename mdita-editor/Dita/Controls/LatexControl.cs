using System;
using System.Windows.Forms;
using mDitaEditor.Dita.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Controls
{
    public class LatexControl : WebBrowser
    {
        public string LaTeX { get; set; }
        public Sectiondiv rootSectionDiv;
        public bool showMessageUpdate { get; set; }
        public string oldLatex
        {
            get; set;
        }
        Timer t;
        bool isCreated = true;

        /// <summary>
        /// Inicializuje prazan section div element za TextBox
        /// </summary>
        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            var div = new Sectiondiv("latex");
            root.SectionDivs.Add(div);
            return div;
        }
        /// <summary>
        /// Metoda koja učitava ponovo zadati kod u LaTeX
        /// </summary>
        public void UpdateLaTex()
        {
                NavigateAndLoadCode("<p>" + LaTeX + "</p>");
            rootSectionDiv.Content = GetXmlForElement();
        }
        /// <summary>
        /// Konstruktor koji učitava SectionDiv za postojeći LaTeX ili LaTeX ako se dodaje
        /// nova kontrola
        /// </summary>
        /// <param name="div"></param>
        /// <param name="latex"></param>
        public LatexControl(Sectiondiv div, string latex = null)
        {
            InitializeComponent();
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            InitTimer();
            ContextMenuStripChanged += EquationControl_ContextMenuStripChanged;
            this.IsWebBrowserContextMenuEnabled = false;
            if (latex != null)
            {
                LaTeX = "<p>" + latex + "</p>";
                rootSectionDiv.Content = GetXmlForElement();
            }
            else
            {
                LaTeX = Util.UnEscapeXml(rootSectionDiv.Content);
                rootSectionDiv.Content = Util.UnEscapeXml(rootSectionDiv.Content);
                isCreated = false;
            }
            NavigateAndLoadCode(LaTeX);
            ScriptErrorsSuppressed = true;
        }

        /// <summary>
        /// Inicializuje meni i timer
        /// </summary>
        public void InitTimer()
        {
            ScrollBarsEnabled = false;
            t = new Timer();
            t.Interval = 1000;
            t.Tick += T_Tick;
            t.Start();

        }
        private ToolStripMenuItem editEquation;
        /// <summary>
        /// Dodaje opciju Edit content u LaTeX meni na desni klik
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EquationControl_ContextMenuStripChanged(object sender, EventArgs e)
        {
            if (ContextMenuStrip != null)
            {
                ContextMenuStrip.Items.Insert(0, new ToolStripSeparator());
                editEquation = GuiUtil.EditButtonWithText("Edit");
                editEquation.Click += EditEquation_Click;
                ContextMenuStrip.Items.Insert(0, editEquation);
            }
        }
        /// <summary>
        /// Metoda koja učitava preview LaTeX-a ispočetka
        /// </summary>
        /// <param name="latex"></param>
        public void NavigateAndLoadCode(string latex)
        {
            try
            {
                Application.DoEvents();
                Navigate("about:blank");
                Application.DoEvents();
                LaTeX = latex;
                string doc = Util.LatexWebForCode(latex);
                DocumentText = doc;
                Document.MouseDown += Doc_MouseDown;
            }
            catch { }
        }

        /// <summary>
        /// Metoda koja se pokreće na klik dugmeta za edit formule
        /// Otvara formu za izmenu LaTeX koda
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditEquation_Click(object sender, EventArgs e)
        {
            editEquationMain(LaTeX);
        }

        public void editEquationMain(string LaTeX, bool fail=false)
        {
            string updateLatex = "";
            try
            {
                updateLatex = Util.UnEscapeXml(LaTeX.Replace("<p>", "").Replace("</p>", ""));
            }
            catch
            {
                updateLatex = Util.UnEscapeXml(LaTeX.Replace("<p>", "").Replace("</p>", ""));
            }
            InsertLatexForm latex = new InsertLatexForm(updateLatex, this, fail);
            latex.ShowDialog();
        }

        /// <summary>
        /// MouseDown metoda za dokument web browsera
        /// Napravljeno jer MouseDown event nad WebBrowserom ne radi (U Focus-u je dokument) 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Doc_MouseDown(object sender, HtmlElementEventArgs e)
        {
           
        }
        /// <summary>
        /// Metoda koja briše LaTeX kontrolu preko desnog klika
        /// </summary>
        public void Delete()
        {
            DitaClipboard.ControlDelete(rootSectionDiv, Parent.Parent);
            Sectiondiv divParent = ((SelectableFlowPanel)this.Parent.Parent).Column;
            ((SelectableFlowPanel)this.Parent.Parent).Remove(this);
            divParent.SectionDivs.Remove(rootSectionDiv);
        }
        /// <summary>
        /// Klik na delete dugme u kontektsnom meniju 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_MouseDown(object sender, EventArgs e)
        {
            // TODO: ADD STATE
            Delete();
        }
        /// <summary>
        /// Timer koji proverava da li je ubačeni Equestion veliki
        /// Ako je prevelik Equation skida ga i korisniku izbacuje poruku
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void T_Tick(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsDisposed && Document != null && Document.Body != null)
                    Height = Document.Body.ScrollRectangle.Height;

                if (Parent != null)
                    if (!Util.CheckText((SelectableFlowPanel)this.Parent.Parent))
                    {
                        if (isCreated == true)
                        {
                            t.Stop();
                            string LaTexBack = "";
                            try
                            {
                                LaTexBack = Util.UnEscapeXml(LaTeX.Replace("<p>", "").Replace("</p>", ""));
                            }
                            catch
                            {
                                LaTexBack = Util.UnEscapeXml(LaTeX.Replace("<p>", "").Replace("</p>", ""));
                            }
                            Delete();
                            MessageBox.Show("Upozorenje: Ubacili ste preveliki Equation");
                            InsertLatexForm attach = new InsertLatexForm(MainForm.Instance.SelectedPanel, LaTexBack);
                            attach.ShowDialog();

                        }
                        else
                        {
                            if (showMessageUpdate == true)
                            {
                                showMessageUpdate = false;
                                MessageBox.Show("Uneti LaTeX ne može stati");
                                string backup = LaTeX;
                                LaTeX = oldLatex;
                                UpdateLaTex();
                                editEquationMain(backup,true);
                            }
                        }
                    }
                isCreated = false;

            }
            catch { t.Dispose(); }
        }

        /// <summary>
        /// Vraća LaTeX za Save 
        /// </summary>
        /// <returns></returns>
        public string GetXmlForElement()
        {
            var escaped = LaTeX;
            return escaped;
        }
        /// <summary>
        /// Metoda koja inicijalno podešava veličinu LaTex kontrole
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.Size = new System.Drawing.Size(200, 20);
            this.ResumeLayout(false);

        }
    }
}
