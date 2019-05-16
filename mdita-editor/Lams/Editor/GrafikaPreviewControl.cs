using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Dita;

namespace mDitaEditor.Lams.Editor
{
    public partial class GrafikaPreviewControl : PictureBox
    {
        private static readonly Size SizeLarge = new Size(176, 125);
        private static readonly Size SizeSmall = new Size(160, 114);

        public GrafikaListControl ParentList { get; private set; }

        private IGrafikaObject _grafikaObject;
        public IGrafikaObject GrafikaObject
        {
            get { return _grafikaObject; }
            set
            {
                _grafikaObject = value;
                if (_grafikaObject != null)
                {
                    Image = _grafikaObject.Icon;
                    if (_grafikaObject is LamsNoticeboard)
                    {
                        LamsNoticeboard notice = (LamsNoticeboard)_grafikaObject;
                        if (notice.LearningObject != null)
                        {
                            itemDelete.Visible = false;
                            Size = SizeLarge;
                            Margin = new Padding(2);
                        }
                        else
                        {
                            itemDelete.Visible = true;
                            Size = SizeSmall;
                            Margin = new Padding(10, 2, 2, 2);
                        }
                    }
                    else
                    {
                        itemDelete.Visible = true;
                        Size = SizeSmall;
                        Margin = new Padding(10, 2, 2, 2);
                    }
                }
                else
                {
                    Image = null;
                }
                if (_grafikaObject is LamsNoticeboard && ((LamsNoticeboard)_grafikaObject).LearningObject is LearningOverview)
                {
                    toolStripSeparator1.Visible = false;
                    itemAddAssessment.Visible = false;
                    itemAddChat.Visible = false;
                    itemAddForum.Visible = false;
                    itemAddMC.Visible = false;
                    itemAddQA.Visible = false;
                    itemAddShareResources.Visible = false;
                    itemAddSubmitFiles.Visible = false;
                    itemAddJavagrader.Visible = false;
                    itemAddNotebook.Visible = false;
                    itemAddNoticeboard.Visible = false;
                }
                else
                {
                    toolStripSeparator1.Visible = true;
                    itemAddAssessment.Visible = true;
                    itemAddChat.Visible = true;
                    itemAddForum.Visible = true;
                    itemAddMC.Visible = true;
                    itemAddQA.Visible = true;
                    itemAddShareResources.Visible = true;
                    itemAddSubmitFiles.Visible = true;
                    itemAddJavagrader.Visible = true;
                    itemAddNotebook.Visible = true;
                    itemAddNoticeboard.Visible = true;
                }
                Transparent = false;
                Selected = false;
                Visible = true;

        
                if ((_grafikaObject is LamsNoticeboard))
                {
                    var title = ((LamsNoticeboard)_grafikaObject).ActivityTitle;
                    if ( title.Equals( "MindMapMozak123" ) )
                    {
                        Visible = false;
                    }
                }

            }
        }

        private bool _transparent;

        public bool Transparent
        {
            get { return _transparent; }
            set
            {
                _transparent = value;
                Hover = false;
                _isDragging = false;
                if (GrafikaObject is LamsNoticeboard)
                {
                    Visible = ParentList.ShowTransparentObjects || !_transparent;
                }
                else
                {
                    Visible = ParentList.ShowTransparentTools || !_transparent;
                }
                Invalidate();
            }
        }

        private bool _selected;

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected != value)
                {
                    _selected = value;
                    Invalidate();
                }
            }
        }

        public bool Hover { get; set; }

        public GrafikaPreviewControl(GrafikaListControl parent, IGrafikaObject obj)
        {
            InitializeComponent();
            InitializeMenu();
            ParentList = parent;
            GrafikaObject = obj;
        }

        private void GrafikaPreviewControl_MouseEnter(object sender, EventArgs e)
        {
            ParentList.ParentPanel.toolTip.SetToolTip(this, null);
            _isDragging = false;
            Hover = true;
            Invalidate();
        }

        private void GrafikaPreviewControl_MouseLeave(object sender, EventArgs e)
        {
            Hover = false;
            Invalidate();
        }

        private bool _isDragging = false;
        
        private Point _mouseLocation;

        private void GrafikaPreviewControl_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseLocation = new Point(e.X, e.Y);
        }

        private void GrafikaPreviewControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                int x = _mouseLocation.X - e.X;
                int y = _mouseLocation.Y - e.Y;
                if (x * x + y * y > 40)
                {
                    if (Transparent)
                    {
                        var toolTip = ParentList.ParentPanel.toolTip;
                        if (string.IsNullOrEmpty(toolTip.GetToolTip(MainForm.Instance)))
                        {
                            var p = MainForm.Instance.PointToClient(PointToScreen(e.Location));
                            p = new Point(p.X - 8, p.Y - 40);
                            toolTip.UseAnimation = false;
                            toolTip.ToolTipTitle = "Slajd je već dodat na platno.";
                            toolTip.Show("Ne možete dodati dva ista slajda na platno.", MainForm.Instance, p);
                        }
                        return;
                    }
                    _isDragging = true;
                    DoDragDrop(GrafikaObject, DragDropEffects.All);
                    Invalidate();
                }
            }
        }

        private void GrafikaPreviewControl_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
            ParentList.ParentPanel.toolTip.Hide(MainForm.Instance);
            Invalidate();
        }

        private static readonly Brush TransparencyBrush =
            new SolidBrush(Color.FromArgb(200, SystemColors.ControlLightLight));

        private void GrafikaPreviewControl_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            var font = (GrafikaObject as LamsNoticeboard)?.LearningObject != null ? GrafikaUtils.TitleFontBold : GrafikaUtils.TitleFont;
            var textColor = Color.Black;
            var outlineColor = Color.White;
            if (Transparent)
            {
                g.FillRectangle(TransparencyBrush, 0, 0, Width, Height);
            }
            if (Selected)
            {
                var style = ButtonBorderStyle.Solid;
                ControlPaint.DrawBorder(e.Graphics, DisplayRectangle,
                    GrafikaUtils.HighlightYellow, 3, style,
                    GrafikaUtils.HighlightYellow, 3, style,
                    GrafikaUtils.HighlightYellow, 3, style,
                    GrafikaUtils.HighlightYellow, 3, style);
            }
            if (Hover || _isDragging || contextMenuPreview.Visible)
            {
                var thickness = Selected ? 1 : 2;
                ControlPaint.DrawBorder(g, DisplayRectangle,
                    Color.Black, thickness, ButtonBorderStyle.Solid,
                    Color.Black, thickness, ButtonBorderStyle.Solid,
                    Color.Black, thickness, ButtonBorderStyle.Solid,
                    Color.Black, thickness, ButtonBorderStyle.Solid);
                if ((GrafikaObject as LamsNoticeboard)?.LearningObject != null)
                {
                    textColor = Color.White;
                    outlineColor = Color.Black;
                }
                else
                {
                    font = GrafikaUtils.TitleFontBold;
                }
            }
            GrafikaUtils.DrawTitleText(g, GrafikaObject.TitleText, new Rectangle(3, 3, Width - 6, Height - 6), textColor, outlineColor, font);
        }

        private void GrafikaPreviewControl_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (!Transparent)
                    {
                        var toolTip = ParentList.ParentPanel.toolTip;
                        toolTip.UseAnimation = true;
                        toolTip.ToolTipTitle = "Dodavanje slajda";
                        toolTip.SetToolTip(this, "Prevucite mišem slajd da biste ga dodali na platno.");
                    }
                    ParentList.SelectedControl = this;
                    break;
            }
        }

        private void GrafikaPreviewControl_DoubleClick(object sender, EventArgs e)
        {
            ItemEdit_Click(sender, e);
        }
    }
}
