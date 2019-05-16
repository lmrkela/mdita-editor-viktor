using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using System.Diagnostics;
using mDitaEditor.Dita.Forms;

namespace mDitaEditor.Dita.Controls
{
    public class YouTubeVideoControl : PictureBox, ResizeableControlImage.ISectiondivContainter
    {
        private Sectiondiv rootSectionDiv;
        private string videoPath;
        private bool onLoadCheckSize = true;

        public void updatePath(String path)
        {
            videoPath = path;
        }
        public string getPath()
        {
            return videoPath;
        }

        public Sectiondiv getRootSectionDiv()
        {
            return rootSectionDiv;
        }

        
        public YouTubeVideoControl(string _videoPath, Panel panel, Sectiondiv div)
        {
            Debug.WriteLine("Youtube created");
            ContextMenuStripChanged += YouTubeVideo_ContextMenuStripChanged;
            videoPath = _videoPath;
            videoPath = videoPath.Replace("watch?v=", "embed/");
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            SetupVideo();
            rootSectionDiv.Content = GetXmlForElement();
            Point velicina = Util.ScaleVideo(panel.Width, ((SelectableFlowPanel)panel).HeightLeftPanel() - 40);
            this.Width = velicina.X;
            this.Height = velicina.Y;
            
        }

        public void redefineControl(string Text)
        {
            videoPath = Text;
            rootSectionDiv.Content = GetXmlForElement();
        }

        public YouTubeVideoControl(Sectiondiv div)
        {
            ContextMenuStripChanged += YouTubeVideo_ContextMenuStripChanged;
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            SetupVideo();
            GetWidthAndHeightFromSecitonDiv();
        }

        public void SetupVideo()
        {
            this.BackColor = Color.FromArgb(167, 5, 50);
            this.Paint += YouTubeVideo_Paint;
            ResizeableControlImage.Create(this, this);
        }

        private void YouTubeVideo_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font f = new Font(FontFamily.GenericSansSerif, 18);
            Rectangle rect = ClientRectangle;
            Rectangle rect1 = new Rectangle(rect.X, rect.Y - 40, rect.Width, rect.Height);
            Rectangle rect2 = new Rectangle(rect.X, rect.Y + 40, rect.Width, rect.Height);
            e.Graphics.DrawString("YouTube Video", f, Brushes.Black, rect1, sf);
            e.Graphics.DrawString(videoPath, f, Brushes.Black, rect2, sf);
        }

        /// <summary>
        /// Inicializuje prazan section div element za TextBox
        /// </summary>
        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            var div = new Sectiondiv("youtube");
            root.SectionDivs.Add(div);
            return div;
        }

        private ToolStripMenuItem editLink;

        private void YouTubeVideo_ContextMenuStripChanged(object sender, EventArgs e)
        { 
            Debug.WriteLine("null");
            if (ContextMenuStrip != null)
            {
                Debug.WriteLine("ok1");
                ContextMenuStrip.Items.Insert(0, new ToolStripSeparator());
                editLink = GuiUtil.EditButtonWithText("Edit");
                editLink.Click += editLink_click;
                ContextMenuStrip.Items.Insert(0, editLink);
            }
        }

        private void editLink_click(object sender, EventArgs e)
        {
            YouTubeVideoForm form = new YouTubeVideoForm(null,this);
            form.ShowDialog();
        }

        
        /// <summary>
        /// Menjanje root section div-a za get xml-a
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            try
            {
                rootSectionDiv.Content = GetXmlForElement();
                if (!onLoadCheckSize)
                {
                    if(Parent != null && Parent.Parent != null)
                    if (!Util.CheckText((SelectableFlowPanel)Parent.Parent))
                    {
                        Height -= Util.CheckPanelOverlap(((SelectableFlowPanel)Parent.Parent));
                    }
                }
                else
                {
                    onLoadCheckSize = false;
                }
            }
            catch { }
        }

        /// <summary>
        /// Getuje XML za element YouTube
        /// </summary>
        /// <returns></returns>
        public string GetXmlForElement()
        {
            return "<p><youtube width=\"" + ((int)(Math.Round(this.Width * 1.1,0))) + "\" height=\"" + ((int)(Math.Round(this.Height * 1.1,0))) + "\" src=\"" + videoPath + "\"></youtube></p>";
        }


        public void GetWidthAndHeightFromSecitonDiv()
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(rootSectionDiv.Content.Replace("\r\n", ""))))
            {
                string value = rootSectionDiv.Content;
                value = WebUtility.HtmlDecode(value);
                reader.ReadToFollowing("youtube");
                //Odavde sam sklonio Decode HTML ne znam sto je bio
                value = Regex.Replace(value, @"(<youtube\/?[^>]+>)", @"", RegexOptions.IgnoreCase).Replace("</youtube>", "");
                reader.MoveToAttribute("width");
                if (value.Substring(0, 2) == "\r\n")
                {
                    value = value.Substring(2, value.Length - 2);
                }
                if (value.EndsWith("\r\n"))
                    value = value.Substring(0, value.Length - 2);
                if (value.Substring(0, 2) == "  ")
                {
                    value = value.Substring(2, value.Length - 2);
                }
                string width = reader.Value;
                reader.MoveToAttribute("height");
                string height = reader.Value;
                reader.MoveToAttribute("src");
                string src = reader.Value;
                videoPath = src;
                Width = (int)(Math.Round(int.Parse(width) / 1.1,0));
                Height = (int)(Math.Round(int.Parse(height) / 1.1, 0));
            }
        }

        private string _stateContent = null;

        public void PrepareState()
        {
            if (rootSectionDiv == null)
            {
                return;
            }
            _stateContent = rootSectionDiv.Content;
        }

        public void AddState()
        {
            if (rootSectionDiv == null || _stateContent == null)
            {
                return;
            }
            if (rootSectionDiv.Content == _stateContent)
            {
                return;
            }
            DitaClipboard.AddSectiondivChangedState(ProjectSingleton.SelectedSection, rootSectionDiv, _stateContent);
        }
    }

}
