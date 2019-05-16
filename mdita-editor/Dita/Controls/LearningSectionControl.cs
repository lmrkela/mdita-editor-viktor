using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mDitaEditor.Utils;
using System.Diagnostics;
using mDitaEditor.CustomControls;
using mDitaEditor.Project;

namespace mDitaEditor.Dita.Controls
{
    public partial class LearningSectionControl : UserControl
    {
        public SelectableFlowPanel[] _panels = new SelectableFlowPanel[3];
           
 
        private Section _section;
        
        private Sectiondiv _subtitleDiv;
        private bool IsPreview { get; set; }

        /// <summary>
        /// Definiše setter koji učitava subttitle kreira ako ga nema. Setuje vrednosti TextBox-ova. 
        /// I pravi layout
        /// </summary>
        public Section Section
        {
            get { return _section; }
            set
            {

          
                if (_section == value)
                {
                    // return;
                }
              

                SuspendLayout();
                _section = value;
                _subtitleDiv = null;
                
                foreach (var pan in _panels)
                {
                    pan.Column = null;
                }

                if (_section != null)
                {
                    foreach (var div in _section.SectionDivs)
                    {
                        if (div.Outputclass == "subtitle")
                        {
                            _subtitleDiv = div;
                            break;
                        }
                    }
                    if (_subtitleDiv == null)
                    {
                        _subtitleDiv = new Sectiondiv()
                        {
                            Outputclass = "subtitle"
                        };
                        _section.SectionDivs.Insert(0, _subtitleDiv);
                    }

                    //Removes focus from textbox

                    // txbNaslov.Enabled = false;
                   //  txbNaslov.Enabled = true;

                   //  txbCilj.Enabled = false;
                   //  txbCilj.Enabled = true;

                    this.Focus();                  

                    SectionTitle = _section.Title != null ? Util.UnEscapeXml(_section.Title.Trim()) : "";
                    SectionGoal = _subtitleDiv.Content != null ? Util.UnEscapeXml(_subtitleDiv.Content.Trim()) : "";

                    Debug.WriteLine("Section set");

                    txbNaslov.saveCurrentText();
                    txbCilj.saveCurrentText();

                    CreateLayout();
                    foreach (Sectiondiv div in Section.SectionDivs)
                    {
                        for (int i = 0; i < div.SectionDivs.Count; ++i)
                        {
                            if (i >= _panels.Length)
                            {
                                break;
                            }
                            _panels[i].IsPreview = IsPreview;
                            _panels[i].Column = div.SectionDivs[i];
                        }
                    }
                    FixColumns();
                    //FindWebBrowsers();

                    if (tableColumns.Controls.Count > 0)
                    {
                        tableColumns.Controls[0].Focus();
                    }
                    Visible = true;
                }
                else
                {
                    Visible = false;
                }
                ResumeLayout();
            }
        }

        //public List<WebBrowser> Browsers { get; private set; } = new List<WebBrowser>();

        //private void FindWebBrowsers()
        //{
        //    Browsers.Clear();
        //    foreach (var panel in _panels)
        //    {
        //        foreach (var control in panel.Controls)
        //        {
        //            var div = control as DivControl;
        //            var browser = div?.SubControl as WebBrowser;
        //            if (browser != null)
        //            {
        //                browser.Height = 0;
        //                Browsers.Add(browser);
        //                browser.SizeChanged += Browser_SizeChanged;
        //            }
        //        }
        //    }
        //}

        //private void Browser_SizeChanged(object sender, EventArgs e)
        //{
        //    var browser = (WebBrowser) sender;
        //    browser.SizeChanged -= Browser_SizeChanged;
        //    Browsers.Remove(browser);
        //}

        public LearningSectionControl() : this(null, false)
        { }

        public LearningSectionControl(Section section, bool isPreview = false)
        {
            InitializeComponent();
            IsPreview = isPreview;
            for (int i = 0; i < _panels.Length; ++i)
            {
                _panels[i] = new SelectableFlowPanel();
            }

            Section = section;
      

            txbNaslov.saveCurrentText();
            txbCilj.saveCurrentText();


        }

        /// <summary>
        /// Metoda koja kreira potrebne kolone u Table Layout-u u zavisnosti
        /// od potrebnog Layout-a za sekciju
        /// </summary>
        private void CreateLayout()
        {
            tableColumns.Controls.Clear();
            int indexObject = Section.SectionDivs.Count - 1;
            Section.SectionDivs[indexObject].AddSections();
            switch (Section.SectionDivs[indexObject].Outputclass)
            {
                case "columns1":
                    tableColumns.Controls.Add(_panels[0], 0, 0);
                    tableColumns.SetColumnSpan(_panels[0], 6);

                    break;
                case "columns2":
                    tableColumns.Controls.Add(_panels[0], 0, 0);
                    tableColumns.SetColumnSpan(_panels[0], 3);

                    tableColumns.Controls.Add(_panels[1], 3, 0);
                    tableColumns.SetColumnSpan(_panels[1], 3);

                    break;
                case "columns3":
                    tableColumns.Controls.Add(_panels[0], 0, 0);
                    tableColumns.SetColumnSpan(_panels[0], 2);

                    tableColumns.Controls.Add(_panels[1], 2, 0);
                    tableColumns.SetColumnSpan(_panels[1], 2);

                    tableColumns.Controls.Add(_panels[2], 4, 0);
                    tableColumns.SetColumnSpan(_panels[2], 2);

                    break;
                case "columns2-1-2":
                    tableColumns.Controls.Add(_panels[0], 0, 0);
                    tableColumns.SetColumnSpan(_panels[0], 2);

                    tableColumns.Controls.Add(_panels[1], 2, 0);
                    tableColumns.SetColumnSpan(_panels[1], 4);

                    break;
                case "columns2-2-1":
                    tableColumns.Controls.Add(_panels[0], 0, 0);
                    tableColumns.SetColumnSpan(_panels[0], 4);

                    tableColumns.Controls.Add(_panels[1], 4, 0);
                    tableColumns.SetColumnSpan(_panels[1], 2);

                    break;
            }
        }

        private void FixColumns()
        {
            switch (Section.SectionDivs[Section.SectionDivs.Count - 1].Outputclass)
            {
                case "columns1":
                    _panels[0].Column.Outputclass = "lmrc";

                    break;
                case "columns2":
                    _panels[0].Column.Outputclass = "lmc2";
                    _panels[1].Column.Outputclass = "mrc2";

                    break;
                case "columns3":
                    _panels[0].Column.Outputclass = "lc";
                    _panels[1].Column.Outputclass = "mc";
                    _panels[2].Column.Outputclass = "rc";

                    break;
                case "columns2-1-2":
                    _panels[0].Column.Outputclass = "lc";
                    _panels[1].Column.Outputclass = "mrc";

                    break;
                case "columns2-2-1":
                    _panels[0].Column.Outputclass = "lmc";
                    _panels[1].Column.Outputclass = "rc";

                    break;
            }
        }

       

        /// <summary>
        /// Učitava vrednost unetu kroz TextBox za naslov
        /// </summary>
        public string SectionTitle
        {
            get { return txbNaslov.Text; }
            set { txbNaslov.Text = value; }
        }

        /// <summary>
        /// Učitava vrednost unetu kroz TextBox za cilj
        /// </summary>
        public string SectionGoal
        {
            get { return txbCilj.Text; }
            set { txbCilj.Text = value; TextChangedCilj(); }
        }

        /// <summary>
        /// Promena naslova automatski ažurira sam naslov u objketu. Ako tekst
        /// prelazi jednu linije automatski se dodaje jos jedna linija ispod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbNaslov_TextChanged(object sender, EventArgs e)
        {
            var oldText = txbNaslov.Text;
            var text = oldText.Replace('\n', ' ');
            text = Regex.Replace(text, @"\s+", " ");
            if (text != oldText)
            {
                var oldCursor = txbNaslov.SelectionStart;
                txbNaslov.Text = text;
                txbNaslov.SelectionStart = oldCursor;
                return;
            }

            var g = CreateGraphics();
            var textSize = g.MeasureString(text, txbNaslov.Font).Width;

            if (textSize >= txbNaslov.Width)
            {
                if (CalculateWidth(g, text, txbNaslov.Font, txbNaslov.Width))
                {
                    var oldCursor = txbNaslov.SelectionStart;
                    txbNaslov.Text = text.Substring(0, text.Length - 1);
                    txbNaslov.SelectionStart = oldCursor;
                    return;
                }
                txbNaslov.Size = new Size(txbNaslov.Size.Width, 73);
            }
            else
            {
                txbNaslov.Size = new Size(txbNaslov.Size.Width, 37);
            }

            _section.Title = text;

        }

        private static readonly char[] SplitChars = new[] { ' ', '-' };

        /// <summary>
        /// Računa širinu u odnosu na prosleđeni text i font a potom vraća da li 
        /// veličina prelazi MaxHeight ili ne
        /// </summary>
        /// <param name="g"></param>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <param name="maxWidth"></param>
        /// <returns></returns>
        private static bool CalculateWidth(Graphics g, string text, Font font, int maxWidth)
        {
            var words = text.Split(SplitChars);
            float width = 0;
            float spaceWidth = 1;//g.MeasureString(" ", font).Width;

            int i = 0;
            for (string word; i < words.Length; i++)
            {
                word = words[i];

                var w = g.MeasureString(word, font).Width;
                width += w;
                if (width > maxWidth)
                {
                    width = w;
                    i++;
                    break;
                }
                width += spaceWidth;
            }
            for (string word; i < words.Length; i++)
            {
                word = words[i];

                var w = g.MeasureString(word, font).Width;
                width += w;
                if (width > maxWidth)
                {
                    return true;
                }
                width += spaceWidth;
            }

            return false;
        }

        public void TextChangedCilj()
        {
            var oldText = txbCilj.Text;
            var text = oldText.Replace('\n', ' ');
            text = Regex.Replace(text, @"\s+", " ");
            if (text != oldText)
            {
                var oldCursor = txbCilj.SelectionStart;
                txbCilj.Text = text;
                txbCilj.SelectionStart = oldCursor;
                return;
            }

            var g = CreateGraphics();
            var textSize = g.MeasureString(text, txbCilj.Font).Width;

            if (textSize >= txbCilj.Width)
            {
                if (CalculateWidth(g, text, txbCilj.Font, txbCilj.Width))
                {
                    var oldCursor = txbCilj.SelectionStart;
                    txbCilj.Text = text.Substring(0, text.Length - 1);
                    txbCilj.SelectionStart = oldCursor;
                    return;
                }
                txbCilj.Size = new Size(txbCilj.Size.Width, 55);
            }
            else
            {
                txbCilj.Size = new Size(txbCilj.Size.Width, 28);
            }
            if (_subtitleDiv != null)
            {
                _subtitleDiv.Content = text;
            }
        }
        /// <summary>
        /// Promena cilja automatski ažurira sam cilj u objketu. Ako tekst
        /// prelazi jednu linije automatski se dodaje jos jedna linija ispod
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbCilj_TextChanged(object sender, EventArgs e)
        {
            TextChangedCilj();
        }

        /// <summary>
        /// Na leaven sa polja za naslov se automatski čekiraju greške
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbNaslov_Leave(object sender, EventArgs e)
        {
            MainForm.Instance.CheckErrorsAndStatistics();            
        }

        private void txbNaslov_Enter(object sender, EventArgs e)
        {
            MainForm.Instance.SelectedControl = txbNaslov;
            txbNaslov.saveCurrentText();
        }

        private void txbCilj_Enter(object sender, EventArgs e)
        {
            MainForm.Instance.SelectedControl = txbCilj;
            txbCilj.saveCurrentText();
        }

        private void txbNaslov_Validated(object sender, EventArgs e)
        {
            
           DitaClipboard.UpdateSlideTextUndoState(ProjectSingleton.SelectedSection,
                txbNaslov, txbNaslov.OldText,  txbNaslov.Text );
        
        }

        private void txbCilj_Validated(object sender, EventArgs e)
        {
            DitaClipboard.UpdateSlideTextUndoState(ProjectSingleton.SelectedSection, txbCilj,
                txbCilj.OldText, txbCilj.Text);
         
        }

         
    }
}
