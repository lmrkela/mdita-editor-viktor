using System.Collections.Generic;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Lams.Editor.XMLExporter;

namespace mDitaEditor.Lams.Editor
{
    public partial class GrafikaListControl : FlowLayoutPanel
    {
        public GrafikaPanel ParentPanel { get; private set; }
        
        public List<GrafikaPreviewControl> PreviewControls { get; private set; }

        private GrafikaPreviewControl _selectedControl;
        public GrafikaPreviewControl SelectedControl
        {
            get { return _selectedControl; }
            set
            {
                if (_selectedControl != null)
                {
                    _selectedControl.Selected = false;
                }
                _selectedControl = value;
                if (_selectedControl != null)
                {
                    _selectedControl.Selected = true;
                    MainForm.Instance.btnAddAditionalActivity.Enabled = !(SelectedControl.GrafikaObject is LearningOverview);
                }
                else
                {
                    MainForm.Instance.btnAddAditionalActivity.Enabled = false;
                }
            }
        }

        public LearningBase SelectedBase
        {
            get
            {
                var noticeboard = SelectedObject as LamsNoticeboard;
                if (noticeboard != null)
                {
                    return noticeboard.LearningObject;
                }
                var lams = SelectedObject as LamsTool;
                if (lams != null)
                {
                    return lams.Parent;
                }
                return null;
            }
        }

        public IGrafikaObject SelectedObject
        {
            get
            {
                if (SelectedControl == null)
                {
                    return null;
                }
                return SelectedControl.GrafikaObject;
            }
        }

        private bool _showTransparentObjects;
        public bool ShowTransparentObjects
        {
            get { return _showTransparentObjects; }
            set
            {
                if (_showTransparentObjects == value)
                {
                    return;
                }
                _showTransparentObjects = value;
                foreach (var control in PreviewControls)
                {
                    if (control.GrafikaObject is LamsNoticeboard)
                    {
                        control.Visible = !control.Transparent || _showTransparentObjects;
                    }
                }
            }
        }

        private bool _showTransparentTools;
        public bool ShowTransparentTools
        {
            get { return _showTransparentTools; }
            set
            {
                if (_showTransparentTools == value)
                {
                    return;
                }
                _showTransparentTools = value;
                foreach (var control in PreviewControls)
                {
                    if (!(control.GrafikaObject is LamsNoticeboard))
                    {
                        control.Visible = !control.Transparent || _showTransparentTools;
                    }
                }
            }
        }

        public GrafikaListControl(GrafikaPanel parent)
        {
            InitializeComponent();
            ParentPanel = parent;
            PreviewControls = new List<GrafikaPreviewControl>();
        }

        public void ReloadControls()
        {
            SuspendLayout();
            if (ParentPanel.Objects.Count == 0)
            {
                while (Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                PreviewControls.Clear();
                ResumeLayout();
                return;
            }
            Controls.Clear();

            int lastControlIndex = 0;
            foreach (var obj in ParentPanel.Objects)
            {
                if (lastControlIndex < PreviewControls.Count)
                {
                    PreviewControls[lastControlIndex].GrafikaObject = obj;
                }
                else
                {
                    PreviewControls.Add(new GrafikaPreviewControl(this, obj));
                }
                if (ParentPanel.Canvas.Items.Contains_(obj))
                {
                    PreviewControls[lastControlIndex].Transparent = true;
                }
                ++lastControlIndex;
            }

            while (lastControlIndex < PreviewControls.Count)
            {
                PreviewControls[lastControlIndex].Dispose();
                PreviewControls.RemoveAt(lastControlIndex);
            }
            foreach (var c in PreviewControls)
            {
                Controls.Add(c);
            }
            ResumeLayout();
        }

        public GrafikaPreviewControl FindControl(IGrafikaObject obj)
        {
            foreach (var control in PreviewControls)
            {
                if (control.GrafikaObject == obj)
                {
                    return control;
                }
                var noticeboardGrafika = control.GrafikaObject as LamsNoticeboard;
                var noticeboardObj = obj as LamsNoticeboard;
                if (noticeboardGrafika != null && noticeboardObj != null && noticeboardObj.LearningObject == noticeboardGrafika.LearningObject && noticeboardObj.LearningObject != null)
                {
                    return control;
                }
            }
            return null;
        }

        public bool ShowObject(IGrafikaObject obj)
        {
            var control = FindControl(obj);
            if (control == null)
            {
                return false;
            }
            control.Transparent = false;
            return false;
        }

        public bool HideObject(IGrafikaObject obj)
        {
            var control = FindControl(obj);
            if (control == null)
            {
                return false;
            }
            control.Transparent = true;
            return true;
        }

        private void GrafikaListControl_Layout(object sender, LayoutEventArgs e)
        {
            ParentPanel.vScrollBar.Visible = !VerticalScroll.Visible;
        }

        private bool AllObjectsHidden
        {
            get
            {
                if (PreviewControls.Count == 0)
                {
                    return false;
                }
                foreach (var control in PreviewControls)
                {
                    if (control.Visible)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
