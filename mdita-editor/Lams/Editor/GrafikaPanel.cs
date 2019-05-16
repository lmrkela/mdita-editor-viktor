using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Project;

namespace mDitaEditor.Lams.Editor
{
    public partial class GrafikaPanel : UserControl
    {
        private bool _autoArrange;
        public bool AutoArrange
        {
            get
            {
                return _autoArrange;
            }
            set
            {
                _autoArrange = value;
                if (_autoArrange)
                {
                    LoadProject();
                }
            }
        }

        public List<LamsTool> Objects { get; private set; }

        public GrafikaListControl ListControl { get; private set; }

        public GrafikaCanvas Canvas { get; private set; }

        public GrafikaCanvas.SortType CanvasSortType { get; set; }

        public GrafikaPanel()
        {
            InitializeComponent();
            Objects = new List<LamsTool>();
            ListControl = new GrafikaListControl(this)
            {
                Bounds = new Rectangle(0, 0, 198, Height),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                ShowTransparentObjects = true,
                ShowTransparentTools = true
            };
            Controls.Add(ListControl);
            Canvas = new GrafikaCanvas(this)
            {
                Bounds = new Rectangle(ListControl.Width, 0, Width - ListControl.Width, Height),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            Controls.Add(Canvas);
            Canvas.SendToBack();
            panelDelete.BringToFront();
        }

        public void LoadProject()
        {
           
            Objects.Clear();
            var project = ProjectSingleton.Project;
            if (project != null)
            {
                Objects.Add(new LamsNoticeboard(project.LearningOverview));
                Objects.AddRange(project.LearningOverview.ToolList);
                foreach (var slide in ProjectSingleton.Project.LearningContents)
                {
                    Objects.Add(new LamsNoticeboard(slide));
                    foreach (var subObject in slide.SubObjects)
                    {
                        Objects.Add(new LamsNoticeboard(subObject));
                        // Objects.AddRange(subObject.ToolList);
                    }
                    Objects.AddRange(slide.ToolList);
                }
                Objects.Add(new LamsNoticeboard(project.LearningSummary));
                Objects.AddRange(project.LearningSummary.ToolList);
            }
            ListControl.ReloadControls();

    

            if (AutoArrange)
            {
                Canvas.AutoArrange(CanvasSortType);
            }
            else
            {
                Canvas.ValidateObjects();
            }
            Canvas.Invalidate();
         
        }

        public void ArrangeCanvas()
        {
            Canvas.AutoArrange(CanvasSortType, true);
        }

        private void panelDelete_VisibleChanged(object sender, System.EventArgs e)
        {
            labDelete.Visible = panelDelete.Visible;
            labDelete.BringToFront();
        }
    }
}
