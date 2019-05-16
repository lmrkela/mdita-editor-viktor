using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Controls
{
    public partial class DivControl : UserControl
    {
        private bool IsPreview { get; set; }
        private Control _control;

        private ToolStripMenuItem redoButton;
        private ToolStripMenuItem undoButton;
        private ToolStripSeparator toolStripSeperator;
        public Control SubControl
        {
            get
            {
                return _control;
            }
            set
            {
                if (_control != null)
                {
                    UnregisterControl(_control);
                    _control.SizeChanged -= SubControl_Resize;
                    Controls.Remove(_control);
                    _control.Dispose();
                }

                _control = value;

                if (_control != null)
                {
                    RegisterControl(_control);
                    _control.SizeChanged += SubControl_Resize;
                    _control.Margin = new Padding(0);
                    _control.Location = new Point(0, 0);
                    _control.ContextMenuStripChanged += _control_ContextMenuChanged;
                    _control.ContextMenuStrip = contextMenuStrip1;
                    Height = _control.Height + 1;
                    Controls.Add(_control);
                }
            }
        }

        private void _control_ContextMenuChanged(object sender, EventArgs e)
        {
            contextMenuStrip1.Items.Remove(undoButton);
            contextMenuStrip1.Items.Remove(redoButton);
            contextMenuStrip1.Items.Remove(toolStripSeperator);
            if (_control is TextBoxControl || _control is NoteControl)
            {
                contextMenuStrip1.Items.Add(toolStripSeperator);
                contextMenuStrip1.Items.Add(undoButton);
                contextMenuStrip1.Items.Add(redoButton);
            }
        }


        /// <summary>
        /// Inicializuje DivControl za rootSectionDiv i prosleđenu kontrolu
        /// i definiše onFocus kontrole
        /// </summary>
        /// <param name="control"></param>
        /// <param name="rootSectiondiv"></param>
        public DivControl(Control control, Sectiondiv rootSectiondiv, bool isPreview = false)
        {
            undoButton = GuiUtil.UndoButton();
            redoButton = GuiUtil.RedoButton();
            toolStripSeperator = new ToolStripSeparator();
            undoButton.Click += UndoButton_Click;
            redoButton.Click += RedoButton_Click;
            IsPreview = isPreview;
            if (control == null)
            {
                Dispose();
                return;
            }
            InitializeComponent();

            GotFocus += DivControl_GotFocus;
            LostFocus += DivControl_LostFocus;
            Div = rootSectiondiv;
            SubControl = control;
        }

        private void RedoButton_Click(object sender, EventArgs e)
        {
            if(_control is NoteControl)
            {
                NoteControl note = (NoteControl)_control;
                note.Redo();
            }
            else if (_control is TextBoxControl)
            {
                TextBoxControl note = (TextBoxControl)_control;
                note.Redo();
            }
        }

        private void UndoButton_Click(object sender, EventArgs e)
        {
            if (_control is NoteControl)
            {
                NoteControl note = (NoteControl)_control;
                note.Undo();
            } else if (_control is TextBoxControl)
            {
                TextBoxControl note = (TextBoxControl)_control;
                note.Undo();
            }
        }

        /// <summary>
        /// Registruje Mouse Eventove za prosleđenu kontrolu
        /// kao i za kontrole koje njoj pripadaju
        /// </summary>
        /// <param name="control"></param>
        private void RegisterControl(Control control)
        {
            control.GotFocus += DivControl_GotFocus;
            control.MouseEnter += DivControl_MouseEnter;
            control.MouseLeave += DivControl_MouseLeave;
            control.MouseUp += DivControl_MouseUp;
            if (control is WebBrowser)
            {
                var doc = ((WebBrowser)control).Document;
                //doc.Focusing += DivControl_GotFocus;
                doc.LosingFocus += DivControl_LostFocus;
                doc.MouseOver += DivControl_MouseEnter;
                doc.MouseLeave += DivControl_MouseLeave;
                doc.MouseUp += DivControl_MouseUp;
            }
            else
            {
                if (control is PictureBox)
                {
                    control.MouseClick += DivControl_MouseClick;
                }
                control.LostFocus += DivControl_LostFocus;
                control.AllowDrop = false;
            }

            foreach (Control c in control.Controls)
            {
                RegisterControl(c);
            }
        }

        /// <summary>
        /// Briše Mouse Eventove za prosleđenu kontrolu
        /// kao i za kontrole koje njoj pripadaju
        /// </summary>
        /// <param name="control"></param>
        private void UnregisterControl(Control control)
        {
            control.GotFocus -= DivControl_GotFocus;
            control.MouseEnter -= DivControl_MouseEnter;
            control.MouseLeave -= DivControl_MouseLeave;
            control.MouseUp -= DivControl_MouseUp;
            if (control is WebBrowser)
            {
                var doc = ((WebBrowser) control).Document;
                doc.Focusing -= DivControl_GotFocus;
                doc.LosingFocus -= DivControl_LostFocus;
                doc.MouseOver -= DivControl_MouseEnter;
                doc.MouseLeave -= DivControl_MouseLeave;
                doc.MouseUp -= DivControl_MouseUp;
            }
            else
            {
                if (control is PictureBox)
                {
                    control.MouseClick -= DivControl_MouseClick;
                }
                control.LostFocus -= DivControl_LostFocus;
            }

            foreach (Control c in control.Controls)
            {
                UnregisterControl(c);
            }
        }

        private SelectableFlowPanel _parent;

        public Sectiondiv Div { get; private set; }

        /// <summary>
        /// Briše 1px jer TextBox ide preko ivice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivControl_ParentChanged(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                _parent = Parent as SelectableFlowPanel;
                Width = Parent.Width - Margin.Horizontal;
                if (SubControl is ResizeableControlImage.ISectiondivContainter)
                {
                    Control c = SubControl;
                    if (c is ImageBoxControl)
                    {
                        c = ((ImageBoxControl)SubControl).PicBox;
                    }
                    if (c.Width > Width)
                    {
                        c.Size = new Size(Width, c.Height * Width / c.Width);
                    }
                }
                else
                {
                    SubControl.Width = Width - 2;
                }
            }
            else
            {
                _parent = null;
            }
        }

        /// <summary>
        /// Dodaje na komponentu još jedan pixel veličine kako bi ukupna Margin-a bila 3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubControl_Resize(object sender, EventArgs e)
        {
            Height = SubControl.Height + 1;
        }

        private void DivControl_Layout(object sender, LayoutEventArgs e)
        {
            picMove.BringToFront();
        }

        /// <summary>
        /// Pomera dita kontrolu gore kao i samu komponentu na GUI-ju
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = _parent.Column.SectionDivs.IndexOf(Div) - 1;
            if (index < 0)
            {
                return;
            }
            _parent.MoveControl(this, _parent, index);
        }

        /// <summary>
        /// Pomera dita kontrolu dole kao i samu komponentu na GUI-ju
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moveDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = _parent.Column.SectionDivs.IndexOf(Div) + 1;
            if (index >= _parent.Column.SectionDivs.Count)
            {
                return;
            }
            _parent.MoveControl(this, _parent, index);
        }

        /// <summary>
        /// Briše DITA element, kreira stanje brisanja za element,
        /// ukoliko je slika u pitanju pravi stanje za brisanje slike
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            deleteControl();
        }

        public void deleteControl()
        {
            if (!IsPreview)
            {
                if (SubControl is ImageBoxControl)
                {
                    var img = (ImageBoxControl)SubControl;
                    DitaClipboard.ImageDeleteState(_parent.Column, Div, Util.GetCopyImage(img.PicBox.Image), img.PathSlike);
                }
                else
                {
                    DitaClipboard.ControlDelete(Div, Parent);
                }
                _parent.Column.SectionDivs.Remove(Div);
                _parent.Remove(this);
            }
        }

        /// <summary>
        /// Seče element, pravi stanje brisanja elementa i čuva RootSectionDiv kao i višinu
        /// kontrole radi budućeg Paste-a
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_control is TextBoxControl)
            {
                TextBoxControl con = (TextBoxControl)_control;
                if (con.CutSelectedText())
                {
                    return;
                }
            }
            else if (_control is NoteControl)
            {
                NoteControl con = (NoteControl)_control;
                if (con.CutSelectedText())
                {
                    return;
                }
            }
            DitaClipboard.CopiedControlHeight = Height;
            DitaClipboard.CopiedSectiondiv = Div;
            if (!IsPreview)
            {
                DitaClipboard.ControlAddOrDeleteState(_parent.Column, DitaClipboard.CopiedSectiondiv, false);
            }
            _parent.Column.SectionDivs.Remove(Div);
            _parent.Remove(this);
            Clipboard.Clear();
        }

        /// <summary>
        /// Postavljamo Root section div prosleđen kroz DivControl za kopiju
        /// i čuvamo njegovu visinu radi lakšeg određivanja prostora pri Paste-u
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_control is TextBoxControl)
            {
                TextBoxControl con = (TextBoxControl)_control;
                if (con.CopySelectedText())
                {
                    return;
                }
            }
            else if (_control is NoteControl)
            {
                NoteControl con = (NoteControl)_control;
                if (con.CopySelectedText())
                {
                    return;
                }
            }
            DitaClipboard.CopiedControlHeight = Height;
            DitaClipboard.CopiedSectiondiv = Div;
            Clipboard.Clear();
        }

        /// <summary>
        /// Na focus kontrole staviti da je Panel kontrole selektovan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DivControl_GotFocus(object sender, EventArgs e)
        {
            MainForm.Instance.SelectedPanel = _parent;
            MainForm.Instance.SelectedControl = SubControl;
        }

        private void DivControl_LostFocus(object sender, EventArgs e)
        {
            //MainForm.Instance.SelectedControl = null;
        }

        private void DivControl_MouseClick(object sender, MouseEventArgs e)
        {
            DivControl_GotFocus(sender, e);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_control is TextBoxControl)
            {
                TextBoxControl con = (TextBoxControl)_control;
                con.Paste();
            }
            else if (_control is NoteControl)
            {
                NoteControl con = (NoteControl)_control;
                con.Paste();
            } else if(this.Parent is SelectableFlowPanel)
            {
                SelectableFlowPanel flow = (SelectableFlowPanel)this.Parent;
                flow.Paste();

            }
        }
    }
}
