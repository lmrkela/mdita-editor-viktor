using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using mDitaEditor.Project;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Controls
{
    public class AudioControl : PictureBox
    {
        private Sectiondiv rootSectionDiv;
        private string audioType;
        private bool onLoadCheckSize = true;
        private string audioPath;

        /// <summary>
        /// Konstruktor koji prima Path do audio fajla, Panel na kome se
        /// nalazi sam Audio kao i sectiondiv od koga se ucitava Audio
        /// </summary>
        /// <param name="_audioPath"></param>
        /// <param name="panel"></param>
        /// <param name="div"></param>
        public AudioControl(string _audioPath, Panel panel, Sectiondiv div)
        {
            audioPath = _audioPath;
            GetTypeFromExtension();
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            SetupAudio();
            rootSectionDiv.Content = GetXmlForElement();
            this.Width = panel.Width;
            this.Height = 27;
        }

        /// <summary>
        /// Konstruktor kome se prosledjuje kreirani Sectiondiv
        /// </summary>
        /// <param name="div"></param>
        public AudioControl(Sectiondiv div)
        {
            rootSectionDiv = div;
            DitaClipboard.ActiveSectiondiv = rootSectionDiv;
            SetupAudio();
            GetWidthAndHeightFromSecitonDiv();
        }

        /// <summary>
        /// Metoda koja vraca tip formata zvuka u odnosu na ekstenziju
        /// </summary>
        public void GetTypeFromExtension()
        {
            string ext = Path.GetExtension(audioPath);
            switch (ext)
            {
                case ".mp3":
                    ext = "audio/mpeg";
                    break;
                case ".ogg":
                    ext = "audio/ogg";
                    break;
                case ".wav":
                    ext = "audio/wav";
                    break;
                default:
                    ext = "audio/mpeg";
                    break;
            }
            audioType = ext;
        }

        /// <summary>
        /// Metoda koja postavlja Pozadinu na Panel za audio 
        /// i podešava Paint metodu
        /// </summary>
        public void SetupAudio()
        {
            this.BackColor = Color.FromArgb(167, 5, 50);
            this.Paint += YouTubeVideo_Paint;
        }
        /// <summary>
        /// Paint metoda koja crta Path do Audio fajla po Panelu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YouTubeVideo_Paint(object sender, PaintEventArgs e)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            Font f = new Font(FontFamily.GenericSansSerif, 18);
            Rectangle rect = ClientRectangle;
            Rectangle rect1 = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
            e.Graphics.DrawString("Audio - " + Path.GetFileName(audioPath), f, Brushes.Black, rect1, sf);
        }

        /// <summary>
        /// Inicializuje prazan section div element za TextBox
        /// </summary>
        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            var div = new Sectiondiv("audio");
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
                    if(this.Parent != null && this.Parent.Parent != null)
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
        /// Metoda koja se poziva na promenu Parenta
        /// Stavlja automatski 16:9 velicinu 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent != null)
            {
                this.Width = this.Parent.Width;
            }
        }


        /// <summary>
        /// Getuje XML za element YouTube
        /// </summary>
        /// <returns></returns>
        public string GetXmlForElement()
        {
            return "<p><audio src=\"" + audioPath + "\" type=\"" + audioType + "\"></audio></p>";
        }

        /// <summary>
        /// Metoda koja parsira XML za audio kako bi učitala
        /// tip i path do audio fajla
        /// </summary>
        public void GetWidthAndHeightFromSecitonDiv()
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(rootSectionDiv.Content.Replace("\r\n", ""))))
            {
                string value = rootSectionDiv.Content;
                reader.ReadToFollowing("audio");
                //Odavde sam sklonio Decode HTML ne znam sto je bio
                value = Regex.Replace(value, @"(<audio\/?[^>]+>)", @"", RegexOptions.IgnoreCase).Replace("</audio>", "");
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
                reader.MoveToAttribute("src");
                string src = reader.Value;
                reader.MoveToAttribute("type");
                string type = reader.Value;
                audioPath = src;
                audioType = type;
                Height = 27;
                rootSectionDiv.Content = GetXmlForElement();
            }
        }
    }

}
