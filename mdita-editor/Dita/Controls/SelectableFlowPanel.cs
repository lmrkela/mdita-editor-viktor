using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using System.Diagnostics;

namespace mDitaEditor.Dita.Controls
{
    public partial class SelectableFlowPanel : FlowLayoutPanel
    {
        public readonly List<DivControl> _controls;

        private Sectiondiv _column;
        public Sectiondiv Column
        {
            get { return _column; }
            set
            {
                _column = value;

                foreach (var divControl in _controls)
                {
                    divControl.Dispose();
                }
                Util.DisposePanelControls(this);
                _controls.Clear();

                if (_column == null)
                {
                    return;
                }

                Debug.WriteLine("set");
                foreach (Sectiondiv divSekSek in _column.SectionDivs.ToArray())
                {
                    // Brise glupe irvasove Sectiondivove koji su potpuno prazni
                    if (divSekSek.Content == null && divSekSek.SectionDivs.Count == 0)
                    {
                        _column.SectionDivs.Remove(divSekSek);
                    }

                    if (divSekSek.Outputclass == "latex")
                    {
                        Add(new LatexControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.Outputclass == "youtube")
                    {
                        Add(new YouTubeVideoControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.Outputclass == "video")
                    {
                        Add(new VideoControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.Outputclass == "audio")
                    {
                        Add(new AudioControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.Outputclass == "flexslider")
                    {
                        Add(new GalleryControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.Outputclass.Substring(0, 1) == "v" &&
                             (divSekSek.Content == "" || divSekSek.Content == null) && divSekSek.SectionDivs.Count == 0)
                    {
                        _column.SectionDivs.Remove(divSekSek);
                    }
                    else if (divSekSek.SectionDivs.Count > 0 &&
                             divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                             divSekSek.SectionDivs[0].SectionDivs.Count == 1 &&
                             divSekSek.SectionDivs[0].SectionDivs[0].Outputclass != null &&
                             divSekSek.SectionDivs[0].SectionDivs[0].Outputclass.Contains("note"))
                    {
                        Add(new NoteControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.SectionDivs.Count > 0 &&
                             divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                             divSekSek.SectionDivs[0].Content != null &&
                             divSekSek.SectionDivs[0].Content.Contains("d4p_eqn_inline"))
                    {
                        Add(new MathMlLoader(divSekSek), divSekSek);
                    }
                    else if (divSekSek.SectionDivs.Count > 0 &&
                             divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                             divSekSek.SectionDivs[0].SectionDivs.Count == 0 &&
                             !divSekSek.SectionDivs[0].Content.Contains("<pre"))
                    {
                        Add(new TextBoxControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.SectionDivs.Count > 0 &&
                             divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                             divSekSek.SectionDivs[0].SectionDivs.Count == 1 &&
                             !divSekSek.SectionDivs[0].Content.Contains("<pre"))
                    {
                        divSekSek.SectionDivs[0].SectionDivs.RemoveAt(0);
                        if (divSekSek.Content == null || divSekSek.Content == "")
                        {
                            divSekSek.Content = "<p></p>";
                        }
                        Add(new TextBoxControl(divSekSek), divSekSek);
                    }
                    else if (divSekSek.SectionDivs.Count > 0 &&
                             divSekSek.SectionDivs[0].Outputclass.Substring(0, 1) == "f" &&
                             divSekSek.SectionDivs[0].SectionDivs.Count == 0 &&
                             divSekSek.SectionDivs[0].Content.Contains("<pre"))
                    {
                        using (
                            XmlReader reader =
                                XmlReader.Create(
                                    new StringReader(
                                        divSekSek.SectionDivs[0].Content.Replace("\r\n", "")))
                            )
                        {
                            string s = divSekSek.SectionDivs[0].Content;
                            try {
                                reader.ReadToFollowing("pre");
                                //Odavde sam sklonio Decode HTML ne znam sto je bio
                                s =
                                    Regex.Replace(s, @"(<pre\/?[^>]+>)", @"",
                                        RegexOptions.IgnoreCase).Replace("</pre>", "");
                                reader.MoveToAttribute("outputclass");
                                if (s.StartsWith("\r\n"))
                                {
                                    s = s.Substring(2, s.Length - 2);
                                }
                                if (s.EndsWith("\r\n"))
                                    s = s.Substring(0, s.Length - 2);
                                if (s.StartsWith("  "))
                                {
                                    s = s.Substring(2, s.Length - 2);
                                }
                                string outputclass = reader.Value;
                                string[] classes = outputclass.Split(' ');
                                bool haveLines = classes.Contains("linenums");
                                string lang = "";
                                string lineN = "0";
                                try
                                {
                                    string lineNumS =
                                        classes.Where(x => x.Contains("noflines")).ToArray()[0];
                                    lineN = lineNumS.Replace("noflines", "");
                                    lang = classes.Where(x => x.Contains("lang-")).ToArray()[0];
                                    lang = lang.Replace("lang-", "");
                                }
                                catch
                                {
                                    MessageBox.Show("Pukao sam");
                                }
                                SnippetCtrl controller = new SnippetCtrl(s.TrimStart(), lang, lineN,
                                    haveLines, null, this, divSekSek);
                                controller.AddOrUpdateSnippet();
                                Console.ReadLine();
                            }
                            catch
                            {
                                divSekSek.SectionDivs[0].Content = "";
                            }
                        }
                    }
                    else if (divSekSek.Content != null && divSekSek.Content.Contains("<fig>"))
                    {
                        Add(new ImageBoxControl(divSekSek, IsPreview), divSekSek);
                    }
                    else
                    {
                        _column.SectionDivs.Remove(divSekSek);
                    }
                }
            }
        }

        public bool IsPreview
        {
            get; set;
        }

        private ContextMenuStrip _menuDitaObject;


        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SelectableFlowPanel
            // 
            this.AllowDrop = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.Size = new System.Drawing.Size(2000, 800);
            this.TabStop = true;
            this.WrapContents = false;
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.SelectableFlowPanel_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.SelectableFlowPanel_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.SelectableFlowPanel_DragOver);
            this.DragLeave += new System.EventHandler(this.SelectableFlowPanel_DragLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SelectableFlowPanel_Paint);
            this.Enter += new System.EventHandler(this.SelectableFlowPanel_Enter);
            this.Leave += new System.EventHandler(this.SelectableFlowPanel_Leave);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectablePanel_MouseDown);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Konstruktor koji omogucuje da je panel selektabilan
        /// </summary>
        public SelectableFlowPanel(Sectiondiv root = null)
        {
            InitializeComponent();
            SetStyle(ControlStyles.Selectable, true);

            _controls = new List<DivControl>();
            Column = root;

            GotFocus += SelectableFlowPanel_GotFocus;
            
            

            _menuDitaObject = GuiUtil.createSelectablePanelRightClickMenu();
            _menuDitaObject.Items[0].Click += Paste_Click;
            this.ContextMenuStrip = _menuDitaObject;
        }


       


        private void SelectableFlowPanel_GotFocus(object sender, EventArgs e)
        {
            MainForm.Instance.SelectedPanel = this;
        }

        private void SelectableFlowPanel_Enter(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void SelectableFlowPanel_Leave(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void SelectableFlowPanel_Paint(object sender, PaintEventArgs e)
        {
            if (Focused)
            {
                var rc = ClientRectangle;
                rc.Inflate(-2, -2);
                ControlPaint.DrawFocusRectangle(e.Graphics, rc);
            }
        }

        private void SelectablePanel_MouseDown(object sender, MouseEventArgs e)
        {
            Focus();
        }

        public void Paste()
        {
           
            DitaClipboard.Paste(this, IsPreview);
            MainForm.Instance.OpenSlide(ProjectSingleton.SelectedSection);
        }

        private void Paste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        /// <summary>
        /// Metoda koja sortira komponente i stavlja ih u pravilan polozaj
        /// </summary>
        private void RelocateControls()
        {
            SuspendLayout();

            Controls.Clear();
            foreach (var control in _controls)
            {
                Controls.Add(control);
            }

            ResumeLayout();
        }

        public void Add(Control control, Sectiondiv div)
        {
            if (!Column.SectionDivs.Contains(div))
            {
                Column.SectionDivs.Add(div);
            }

            if (control is GalleryControl)
            {
                Controls.Add(control);
            }
            else
            {
                _controls.Add(new DivControl(control, div, IsPreview));
                RelocateControls();
            }
        }

        public void Remove(Control control)
        {
            if (control is DivControl)
            {
                var c = (DivControl)control;
                if (_controls.Remove(c))
                {
                    RelocateControls();
                    c.Dispose();
                }
            }
            else
            {
                foreach (var div in _controls)
                {
                    if (div.SubControl == control)
                    {
                        _controls.Remove(div);
                        RelocateControls();
                        div.Dispose();
                        return;
                    }
                }
            }
            MainForm.Instance.enumerateFigures();
            MainForm.Instance.OpenSlide(ProjectSingleton.SelectedSection);
        }

        public int HeightLeftPanel()
        {
            int height = Height;
            foreach (Control con in Controls)
            {
                height -= con.Height + con.Margin.Vertical;
            }
            return height;
        }
    }
}
