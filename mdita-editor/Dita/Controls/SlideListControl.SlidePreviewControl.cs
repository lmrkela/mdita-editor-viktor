using System;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Lams.Editor;

namespace mDitaEditor.Dita.Controls
{
    partial class SlideListControl
    {
        private static readonly Size SizeLarge = new Size(176, 125);
        private static readonly Size SizeSmall = new Size(160, 114);

        public partial class SlidePreviewControl : PictureBox
        {
            private IDitaSlide _slide;
            public IDitaSlide Slide
            {
                get { return _slide; }
                set
                {
                    _slide = value;
                    if (_slide == null)
                    {
                        Image = null;
                        ContextMenuStrip = null;
                        _allowDrag = false;
                    }
                    else
                    {
                        Image = _slide.GetPreviewImage();
                        if (_slide is Section)
                        {
                            Section section = (Section)_slide;
                            if (section.Parent is LearningOverview || section.Parent is LearningSummary)
                            {
                                ContextMenuStrip = MenuSectionReduced;
                                _allowDrag = false;
                            }
                            else
                            {
                                ContextMenuStrip = MenuSection;
                                _allowDrag = true;
                            }
                        }
                        else if (_slide is LearningContent)
                        {
                            ContextMenuStrip = MenuObject;
                            _allowDrag = true;
                        }
                        else
                        {
                            ContextMenuStrip = MenuObjectReduced;
                            _allowDrag = false;
                        }

                        LearningContent learningContent = null;
                        if (_slide is LearningContent)
                        {
                            learningContent = (LearningContent)_slide;
                        }
                        else if (_slide is Section)
                        {
                            learningContent = ((Section)_slide).Parent as LearningContent;
                        }
                        Size = (learningContent != null && learningContent.Parent != null) ? SizeSmall : SizeLarge;
                    }
                }
            }

            private SlideListControl _parentList;

            public SlidePreviewControl(IDitaSlide slide, SlideListControl parentList)
            {
                InitializeComponent();
                _parentList = parentList;
                _parentList.SlideList.Add(this);
                Slide = slide;
            }

            private void SlidePreviewControl_Paint(object sender, PaintEventArgs e)
            {
                var g = e.Graphics;
                var font = Slide is LearningBase ? GrafikaUtils.TitleFontBold : GrafikaUtils.TitleFont;
                var textColor = Color.Black;
                var outlineColor = Color.White;
                if (_parentList.SelectedSlide == Slide)
                {
                    var style = ButtonBorderStyle.Solid;
                    ControlPaint.DrawBorder(g, DisplayRectangle,
                        GrafikaUtils.HighlightYellow, 3, style,
                        GrafikaUtils.HighlightYellow, 3, style,
                        GrafikaUtils.HighlightYellow, 3, style,
                        GrafikaUtils.HighlightYellow, 3, style);
                }
                if (_hover || _isDragging || ContextMenuStrip.Visible)
                {
                    var thickness = _parentList.SelectedSlide == Slide ? 1 : 2;
                    ControlPaint.DrawBorder(g, DisplayRectangle,
                        Color.Black, thickness, ButtonBorderStyle.Solid,
                        Color.Black, thickness, ButtonBorderStyle.Solid,
                        Color.Black, thickness, ButtonBorderStyle.Solid,
                        Color.Black, thickness, ButtonBorderStyle.Solid);
                    if (Slide is LearningBase)
                    {
                        textColor = Color.White;
                        outlineColor = Color.Black;
                    }
                    else
                    {
                        font = GrafikaUtils.TitleFontBold;
                    }
                }
                GrafikaUtils.DrawTitleText(g, Slide.GetTitle(), new Rectangle(3, 3, Width - 6, Height - 6), textColor, outlineColor, font);
            }

            private void SlidePreviewControl_Disposed(object sender, EventArgs e)
            {
                _parentList.SlideList.Remove(this);
                _parentList.Controls.Remove(this);
            }

            private bool _hover;

            private void SlidePreviewControl_MouseEnter(object sender, EventArgs e)
            {
                _hover = true;
                Invalidate();
            }

            private void SlidePreviewControl_MouseLeave(object sender, EventArgs e)
            {
                _hover = false;
                Invalidate();
            }

            private bool _loading;

            private void SlidePreviewControl_Click(object sender, EventArgs e)
            {
                if (_loading)
                {
                    return;
                }
                _loading = true;
                _parentList.SelectedControl = this;
                _loading = false;
            }

            public int SubControlCount
            {
                get
                {
                    if (Slide is LearningBase)
                    {
                        int count = ((LearningBase)Slide).LearningBody.Sections.Count;
                        if (Slide is LearningContent)
                        {
                            var slide = (LearningContent)Slide;
                            foreach (var content in slide.SubObjects)
                            {
                                count += content.LearningContentBody.Sections.Count + 1;
                            }
                        }
                        return count;
                    }
                    return 0;
                }
            }

            public void Delete()
            {
                int count = SubControlCount;

                var list = new SlidePreviewControl[count + 1];

                var parentList = _parentList.SlideList;
                int index = parentList.IndexOf(this);
                for (int i = 0; i < list.Length; ++i)
                {
                    list[i] = parentList[index + i];
                }

                foreach (var control in list)
                {
                    control.Dispose();
                }
            }
        }
    }
}
