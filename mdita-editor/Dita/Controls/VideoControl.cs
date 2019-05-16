using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using System.Diagnostics;

namespace mDitaEditor.Dita.Controls
{
    public class VideoControl : PictureBox, ResizeableControlImage.ISectiondivContainter
    {
        private Sectiondiv rootSectionDiv;
        private string videoPath;
        private bool onLoadCheckSize = true;
        private string videoType;

        public VideoControl(string _videoPath, Panel panel, Sectiondiv div)
        {
            Debug.WriteLine("Video created");
            videoPath = _videoPath;
            GetTypeFromExtension();
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            SetupVideo();
            rootSectionDiv.Content = GetXmlForElement();
            Point velicina = Util.ScaleVideo(panel.Width, ((SelectableFlowPanel)panel).HeightLeftPanel() - 40);
            this.Width = velicina.X;
            this.Height = velicina.Y;
        }

        public void GetTypeFromExtension()
        {
            string ext = Path.GetExtension(videoPath);
            switch (ext)
            {
                case ".mp4":
                    ext = "video/mp4";
                        break;
                case ".ogg":
                    ext = "video/ogg";
                    break;
                case ".webm":
                    ext = "video/webm";
                    break;
                default:
                    ext = ".mp4";
                    break;
            }
            videoType = ext;
        }

        public VideoControl(Sectiondiv div)
        {
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
            e.Graphics.DrawString("Video", f, Brushes.Black, rect1, sf);
            e.Graphics.DrawString(Path.GetFileName(videoPath), f, Brushes.Black, rect2, sf);
        }

        /// <summary>
        /// Inicializuje prazan section div element za TextBox
        /// </summary>
        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            var div = new Sectiondiv("video");
            root.SectionDivs.Add(div);
            return div;
        }

        /// <summary>
        /// Menjanje root section div-a za get xml-a
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            try
            {
                rootSectionDiv.Content = GetXmlForElement();
                if (!onLoadCheckSize)
                {
                    if(Parent != null && Parent.Parent != null)
                    if (!Util.CheckText((SelectableFlowPanel)this.Parent.Parent))
                    {
                        this.Height -= Util.CheckPanelOverlap(((SelectableFlowPanel)this.Parent.Parent));
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
            return "<p><video width=\"" + ((int)(Math.Round(this.Width * 1.1, 0))) + "\" height=\"" + ((int)(Math.Round(this.Height * 1.1, 0))) + "\" src=\"" + videoPath + "\" type=\"" + videoType + "\"></video></p>";
        }


        public void GetWidthAndHeightFromSecitonDiv()
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(rootSectionDiv.Content.Replace("\r\n", ""))))
            {
                try
                {
                    string value = rootSectionDiv.Content;
                    reader.ReadToFollowing("video");
                    //Odavde sam sklonio Decode HTML ne znam sto je bio
                    value = Regex.Replace(value, @"(<video\/?[^>]+>)", @"", RegexOptions.IgnoreCase).Replace("</video>", "");
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
                    reader.MoveToAttribute("type");
                    string type = reader.Value;
                    Width = (int)(Math.Round(int.Parse(width) / 1.1, 0));
                    Height = (int)(Math.Round(int.Parse(height) / 1.1, 0));
                    videoPath = src;
                    videoType = type;
                    rootSectionDiv.Content = GetXmlForElement();
                }catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
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
