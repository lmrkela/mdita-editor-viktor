using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using mDitaEditor.Project;
using Timer = System.Windows.Forms.Timer;

namespace mDitaEditor.Dita.Controls
{
    partial class SlideListControl : FlowLayoutPanel
    {

        public void referesh()
        {
            foreach (SlidePreviewControl s in _previewList)
            {
                SelectedControl = s;
            }
        }

        private readonly List<SlidePreviewControl> _previewList;
        private List<SlidePreviewControl> SlideList
        {
            get { return _previewList; }
        }

        private IDitaSlide _selectedSlide;
        public IDitaSlide SelectedSlide
        {
            get { return _selectedSlide; }
            set
            {
                if (_selectedSlide == value)
                {
                    return;
                }
                _selectedSlide = value;
                if (_selectedSlide == null)
                {
                    SelectedControl = null;
                }
                else
                {
                    foreach (var control in SlideList.Where(control => control.Slide == _selectedSlide))
                    {
                        if (_selectedControl != control)
                        {
                            SelectedControl = control;
                        }
                        return;
                    }
                }
            }
        }

        private SlidePreviewControl _selectedControl;
        private bool _opening = false;
        private SlidePreviewControl SelectedControl
        {
            get { return _selectedControl; }
            set
            {
                _opening = true;
                var prev = _selectedControl;
                _selectedControl = value;

                if (prev != null && !prev.IsDisposed)
                {
                    prev.Slide.GeneratePreviewImage();
                    prev.Image = prev.Slide.GetPreviewImage();
                    prev.Invalidate();
                }
                if (_selectedControl != null)
                {
                    Rectangle bounds = _selectedControl.Bounds;
                    bounds.Intersect(ClientRectangle);
                    if (bounds == Rectangle.Empty)
                    {
                        ShowSlide(_selectedControl.Slide);
                    }
                    else
                    {
                        _selectedControl.Invalidate();
                    }
                }
                if (_selectedControl != null)
                {
                    _selectedSlide = _selectedControl.Slide;
                    MainForm.Instance.OpenSlide(_selectedSlide);
                    _selectedControl.Invalidate();
                }
                else
                {
                    _selectedSlide = null;
                    MainForm.Instance.CloseSlide();
                }
                _opening = false;
            }
        }
        /// <summary>
        /// Ova metoda je napravljena jer kad se dodaju sekcije iz nekog razloga
        /// se ne setuje selectedSlide 
        /// </summary>
        public void setSectionActive()
        {
            SlidePreviewControl view =  _previewList[OpenSlideIndex];
            _selectedSlide = view.Slide;
            _selectedControl.Invalidate();
        }

        /// <summary>
        /// Designer fix mora biti private dodelice dizajner -1
        /// </summary>
        public int OpenSlideIndex
        {
            get { return _previewList.IndexOf(_selectedControl); }
            set
            {
                if (_opening)
                {
                    return;
                }
                if (DesignMode)
                {
                    _opening = false;
                    return;
                }
                if (value < 0)
                {
                    MainForm.Instance.CloseSlide();
                    return;
                }
                if (value >= _previewList.Count)
                {
                    value = _previewList.Count - 1;
                }
                if (_previewList.IndexOf(_selectedControl) != value)
                {
                    SelectedControl = _previewList[value];
                }
            }
        }


        public SlideListControl()
        {
            InitializeComponent();
            AllowDrop = true;

            _previewList = new List<SlidePreviewControl>();

            _scrollTimer = new Timer();
            _scrollTimer.Tick += scrollTimer_Tick;
            _scrollTimer.Interval = 20;
            PreviewKeyDown += SlideListControl_PreviewKeyDown;
            LostFocus += SlideListControl_LostFocus;
        }

        private bool _keyPressed = false;

        private void SlideListControl_LostFocus(object sender, EventArgs e)
        {
            if(_keyPressed)
            //if (ClientRectangle.Contains(PointToClient(Control.MousePosition)) && !MainForm.Instance.ribbonMenu.OrbDropDown.Visible)
            {
                Focus();
                _keyPressed = false;
            }
        }

        private void SlideListControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    ++OpenSlideIndex;
                    break;
                case Keys.Up:
                    if (OpenSlideIndex > 0)
                    {
                        --OpenSlideIndex;
                    }
                    break;
                case Keys.Delete:
                    if (OpenSlideIndex != -1)
                    {
                        int open = OpenSlideIndex;
                        SlidePreviewControl control = _previewList[OpenSlideIndex];
                        if (control.Slide is Section)
                        {
                            Section content = (Section) control.Slide;
                            SlidePreviewControl.DeleteSection(content, control);
                        }
                        else if (control.Slide is LearningContent)
                        {
                            LearningContent content = (LearningContent) control.Slide;
                            SlidePreviewControl.DeleteObject(content, control);
                        }
                        OpenSlideIndex = open;
                    }
                    break;
            }
            Focus();
            _keyPressed = true;
        }

        private void CreatePreviewImagesInBackground()
        {
            var controls = new List<SlidePreviewControl>();
            foreach (var control in _previewList)
            {
                if (!control.Slide.HasPreviewImage())
                {
                    controls.Add(control);
                }
            }

            var slides = new List<IDitaSlide>(controls.Count);
            foreach (var c in controls)
            {
                slides.Add(c.Slide);
            }

            //var t = new Thread(() =>
            //{
            try
            {
                for (int i = 0; i < slides.Count; ++i)
                {
                    var slide = slides[i];
                    var control = controls[i];

                    if (!slide.HasPreviewImage())
                    {
                        slide.GeneratePreviewImage();
                    }
                    if (control.IsDisposed || !control.Visible)
                    {
                        continue;
                    }
                    control.Image = slide.GetPreviewImage();
                }
            }
            catch
            { }
            //});
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();
        }



        public void RecreateMenu()
        {
            SuspendLayout();

            ProjectFile project = ProjectSingleton.Project;
            if (project == null)
            {
                ResumeLayout();
                _previewList.Clear();
                RelocateControls();
                MainForm.Instance.CloseSlide();
                MainForm.Instance.Text = "mDitaEditor";
                return;
            }

            List<LearningBase> objects = new List<LearningBase>();
            objects.Add(project.LearningOverview);
            objects.AddRange(project.LearningContents);
            objects.Add(project.LearningSummary);

            int i = 0;
            foreach (var learningBase in objects)
            {
                if (i < _previewList.Count)
                {
                    _previewList[i].Slide = learningBase;
                }
                else
                {
                    new SlidePreviewControl(learningBase, this);
                }
                ++i;

                foreach (var section in learningBase.LearningBody.Sections)
                {
                    if (i < _previewList.Count)
                    {
                        _previewList[i].Slide = section;
                    }
                    else
                    {
                        new SlidePreviewControl(section, this);
                    }
                    ++i;
                }

                LearningContent learningContent = learningBase as LearningContent;
                if (learningContent != null)
                {
                    foreach (var subObject in learningContent.SubObjects)
                    {
                        if (i < _previewList.Count)
                        {
                            _previewList[i].Slide = subObject;
                        }
                        else
                        {
                            new SlidePreviewControl(subObject, this);
                        }
                        ++i;

                        foreach (var section in subObject.LearningBody.Sections)
                        {
                            if (i < _previewList.Count)
                            {
                                _previewList[i].Slide = section;
                            }
                            else
                            {
                                new SlidePreviewControl(section, this);
                            }
                            ++i;
                        }
                    }
                }
            }
            
            while (_previewList.Count > i)
            {
                var p = _previewList[i];
               _previewList.Remove(p);
                p.Dispose();
            }

            RelocateControls();
            CreatePreviewImagesInBackground();

            ResumeLayout();
            MainForm.Instance.ScrollVisible = !VerticalScroll.Visible;
        }

        private void RelocateControls()
        {
            Controls.Clear();
            foreach (var control in SlideList)
            {
                Controls.Add(control);
            }
            MainForm.Instance.ScrollVisible = !VerticalScroll.Visible;
        }

        private void DeleteSlide(SlidePreviewControl control)
        {
            SuspendLayout();

            if (SelectedSlide == control.Slide)
            {
                MainForm.Instance.CloseSlide();
            }
            control.Delete();

            CreatePreviewImagesInBackground();
            ResumeLayout();
        }

        private void ShowSlide(IDitaSlide slide, bool centerToMouse = false)
        {
            var control = SlideList.Find(c => c.Slide == slide);
            int y;
            if (centerToMouse)
            {
                var mouse = PointToClient(Cursor.Position);
                var c = GetChildAtPoint(mouse) as SlidePreviewControl;
                if (c != null && c.Slide == slide)
                {
                    return;
                }
                y = control.Location.Y + control.Height/2 - mouse.Y;
            }
            else
            {
                y = control.Location.Y + control.Height - Height/2;
            }
            Point p = AutoScrollPosition;
            AutoScrollPosition = new Point(-p.X, -p.Y + y);
        }


        private void SlideListControl_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                foreach (var control in SlideList)
                {
                    control.Slide = control.Slide;
                }
            }
        }
    }
}
