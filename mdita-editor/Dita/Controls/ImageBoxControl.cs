using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using mDitaEditor.Dita.Forms;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using System.Diagnostics;

namespace mDitaEditor.Dita.Controls
{
    /// <summary>
    /// Klasa ImageBox predstavlja GUI kompoenentu koja sadrzi sliku (Picture)
    /// i opis slike (Caption- kao TextBox kontrola)
    /// </summary>
    class ImageBoxControl : FlowLayoutPanel, ResizeableControlImage.ISectiondivContainter
    {
        public PictureBox PicBox;
        public TextBoxMemory TxtCaption;
        private RichTextBox TxtDesc;
        public string PathSlike = null;
        public Sectiondiv rootSectionDiv;
        private string LastCaptionMode = "";
 
        public bool IsUrl { get; set; }

        /// <summary>
        /// Inicializuje Sectiondiv potreban za ImageBox
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static Sectiondiv InitSectionDiv(Sectiondiv root)
        {
            var div = new Sectiondiv("vp4");
            root.SectionDivs.Add(div);
            return div;
        }


        private ImageBoxControl()
        {
            Debug.WriteLine("slika empty");
            FlowDirection = FlowDirection.TopDown;
            WrapContents = false;
            AutoSize = false;
            Margin = new Padding(0);

            PicBox = new PictureBox();
            PicBox.Margin = new Padding(0);
            PicBox.BorderStyle = BorderStyle.Fixed3D;
            PicBox.SizeChanged += SubControl_SizeChanged;
            Controls.Add(PicBox);

           
            TxtCaption = new TextBoxMemory();
            TxtCaption.Multiline = true;
            TxtCaption.Margin = new Padding(0, 0, 0, 0);
            TxtCaption.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TxtCaption.Width = Width - TxtCaption.Margin.Horizontal;
            TxtCaption.SizeChanged += SubControl_SizeChanged;
          
             Controls.Add(TxtCaption);
 
            SizeChanged += ImageBox_SizeChanged;
        }

        /// <summary>
        /// Konstruktor za novu sliku
        /// </summary>
        /// <param name="panel"></param>
        public ImageBoxControl(SelectableFlowPanel panel, Sectiondiv div, string hasLocation = null) : this()
        {

            Debug.WriteLine("slika nova");
          
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Image Files|*.png;*.jpg;*.gif;*.JPG";
            // if (imageDialog.ShowDialog() == DialogResult.OK)
            //  {
            string imageLocation = "";
            if (hasLocation != null)
            {
                imageLocation = hasLocation;
            }
            else if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                imageLocation = imageDialog.FileName;
            }
            if (imageLocation != "")
            {
                Image image = Util.GetCopyImage(imageLocation);
                Image scaleImage = null;
                bool compress = ShouldICompress(image);
                PictureSaveForm pic = new PictureSaveForm(ProjectSingleton.SelectedContent);
                if (pic.ShowDialog() == DialogResult.OK)
                {
                    scaleImage = Util.ScaleImage(image, panel.Width, panel.HeightLeftPanel() - 40);
                    string copyPath = (!compress) ? ProjectSingleton.Project.ResourcesDir + pic.ImeSlike + Path.GetExtension(imageLocation).ToLower() :
                        ProjectSingleton.Project.ResourcesDir + pic.ImeSlike + ".jpg";
                    PathSlike = Path.GetFileName(copyPath);
                    try
                    {
                        if (!compress)
                        {
                            System.IO.File.Copy(imageLocation, copyPath, true);
                        }
                        else
                        {
                            compressAndSave(copyPath, image);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Fajl koji ste uneli se vec nalazi u projektu. ");
                    }
                    DefinePictureBox(image, scaleImage);
                    DefineCaption();
                    rootSectionDiv = div;
                    div.Content = GetXmlForElement();
                    DitaClipboard.ActiveSectiondiv = div;
                    this.TxtCaption.TextChanged += TxtCaption_TextChanged;
                    TxtCaption.GotFocus += TxtCaption_GotFocus;
                    TxtCaption.updateSaved();
                }
                else
                {
                    this.Dispose();
                }
            }
            else
            {
                this.Dispose();
            }

          
        }

        private void ImageBox_SizeChanged(object sender, EventArgs e)
        {
            TxtCaption.Width = Width - TxtCaption.Margin.Horizontal;
            if (TxtDesc != null)
            {
                TxtDesc.Width = Width - TxtDesc.Margin.Horizontal;
            }
        }

        /// <summary>
        /// Konstruktor koji prima postojecu sliku
        /// </summary>
        /// <param name="parent"></param>
        public ImageBoxControl(Sectiondiv div, bool fromWeb = false) : this()
        {
            Debug.WriteLine("slika postojeca");
            rootSectionDiv = div;
            ParseXmlFromSectionDiv(div, fromWeb);
            DitaClipboard.ActiveSectiondiv = div;
        }
        /// <summary>
        /// Metoda koja parsira postojeći XML iz SectionDiv-a i ponovno učitava sliku
        /// </summary>
        /// <param name="div"></param>
        /// <param name="isUrl"></param>
        public void ParseXmlFromSectionDiv(Sectiondiv div, bool isUrl = false)
        {
            IsUrl = isUrl;
            string replacedContent = div.Content.Replace("\r\n", "");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(replacedContent);
            XmlNodeList nodesImage = doc.DocumentElement.SelectNodes("image");
            if (nodesImage.Count > 0)
            {
                XmlNodeList nodesTitle = doc.DocumentElement.SelectNodes("title");
                XmlNodeList nodesDesc = doc.DocumentElement.SelectNodes("desc");
                try
                {
                    XmlAttributeCollection href2 = nodesImage[0].Attributes;
                    string href = href2[0].Value;
                    int width = (int)(Math.Round(int.Parse(href2[1].Value.Replace("px", "")) / 1.1, 0));
                    int height = (int)(Math.Round(int.Parse(href2[2].Value.Replace("px", "")) / 1.1, 0));

                    string titlePic = "";
                    string descPic = null;
                    if (nodesTitle.Count > 0)
                    {
                        string titlePic2 = Util.UnEscapeXml(nodesTitle[0].InnerText);
                        titlePic = Regex.Replace(titlePic2, @"<[^>]*>", string.Empty);
                        titlePic = Regex.Replace(titlePic, @"\s+", " ");
                    }
                    if (nodesDesc.Count > 0)
                    {
                        descPic = Util.UnEscapeXml(nodesDesc[0].InnerText);
                    }
                    PathSlike = Util.PictureNameForWithoutDITA(href);
                    PicBox.Width = width;
                    PicBox.Height = height;
                    TxtCaption.TextChanged += TxtCaption_TextChanged;
                    TxtCaption.GotFocus += TxtCaption_GotFocus;
                    TxtCaption.SavedText = "Slika-1 "+titlePic;
                    TxtCaption.Text = titlePic;

                   
                    if (descPic != null)
                    {
                        descPic = Regex.Replace(descPic, @"\s+", " ");
                        InsertDescription(descPic);
                    }
                    DefinePictureBoxWithSize(width, height);
                    InitPictureWithHref(isUrl);
                }
                catch { }
            }
            else
            {
               
            }

        }

        private void TxtCaption_GotFocus(object sender, EventArgs e)
        {
            TxtCaption.SavedText = TxtCaption.Text;
        }

        /// <summary>
        /// Metoda koja učitava sliku sa Web-a ili iz resource foldera u zavisnosti od parametra
        /// isUrl
        /// </summary>
        /// <param name="isUrl"></param>
        public void InitPictureWithHref(bool isUrl)
        {
            if (ProjectSingleton.Project == null)
            {
                return;
            }
            if (isUrl == false)
            {
                string fileName = ProjectSingleton.Project.ResourcesDir + PathSlike;
                if (File.Exists(fileName))
                    PicBox.Image = Util.GetCopyImage(ProjectSingleton.Project.ResourcesDir + PathSlike);
            }
            else
            {
                string pathWithoutSourceAndFile = rootSectionDiv.UrlTo.Substring(0, rootSectionDiv.UrlTo.LastIndexOf("/"));
                try
                {
                    PicBox.Load("http://mdita.metropolitan.ac.rs/qdita-temp/" + pathWithoutSourceAndFile + "/resources/" + PathSlike);
                }
                catch
                {
                    Console.WriteLine("Cannnot load file : " + "http://mdita.metropolitan.ac.rs/qdita-temp/" + pathWithoutSourceAndFile + "/resources/" + PathSlike);
                }
            }
        }
        
        /// <summary>
        /// Defniše Insert Description, Remove Description i Change Image opcije u meniju
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageBox_ContextMenuStripChanged(object sender, EventArgs e)
        {
            if (ContextMenuStrip != null)
            {
                ContextMenuStrip.Items[2].Click += OnDelete;
                InitChangeCaptionMenu();
                TxtCaption.ContextMenuStrip = ContextMenuStrip;
                ContextMenuStrip.Opening += ContextMenuStrip_Opening;
            }
        }

        private void ContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var menu = sender as ContextMenuStrip;
            ContextMenuStrip.Items[0].Visible = ContextMenuStrip.Items[1].Visible = menu?.SourceControl == TxtCaption;
        }

        /// <summary>
        /// Učitava meni za promenu početka Caption-a
        /// </summary>
        public void InitChangeCaptionMenu()
        {
            ToolStripMenuItem toolStripImage = new ToolStripMenuItem("Slika");
            toolStripImage.Click += changeCaptionClick;
            ToolStripMenuItem toolStripExample = new ToolStripMenuItem("Primer");
            toolStripExample.Click += changeCaptionClick;
            ToolStripMenuItem toolStripTabela = new ToolStripMenuItem("Tabela");
            toolStripTabela.Click += changeCaptionClick;
            ToolStripMenuItem menuItemChangeCaption = new ToolStripMenuItem("Change caption");
            menuItemChangeCaption.DropDownItems.Add(toolStripImage);
            menuItemChangeCaption.DropDownItems.Add(toolStripExample);
            menuItemChangeCaption.DropDownItems.Add(toolStripTabela);
            menuItemChangeCaption.Image = Properties.Resources.edit;
            ContextMenuStrip.Items.Insert(0, new ToolStripSeparator());
            ContextMenuStrip.Items.Insert(0, menuItemChangeCaption);
        }

        /// <summary>
        /// Menja Caption u zavinosti od izabrane stavke iz menija
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void changeCaptionClick(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            LastCaptionMode = menuItem.Text;
            int indexOf = TxtCaption.Text.IndexOf('-');
            if (indexOf != -1)
            {
                string doCrte = TxtCaption.Text.Substring(0, indexOf + 1);
                TxtCaption.Text = TxtCaption.Text.Replace(doCrte, LastCaptionMode);
            }
            else
            {
                TxtCaption.Text = LastCaptionMode;
                if (LastCaptionMode == "")
                {
                    TxtCaption.Text = "Slika- ";
                }
            }
            TxtCaption.Select(TxtCaption.Text.Length, 0);
        }

        public bool ShowDescription(bool visible)
        {
      
            if (visible)
            {
                SelectableFlowPanel panel = (SelectableFlowPanel) Parent.Parent;
                if (panel.HeightLeftPanel() < 25)
                {
                    return false;
                }
                InsertDescription();
            }
            else
            {
                RemoveDescription();
            }
            return true;
        }

        /// <summary>
        /// Daje description field za ImageBox
        /// </summary>
        private void InsertDescription(string text = "")
        {
            TxtDesc = new RichTextBox();
            TxtDesc.Margin = new Padding(0, 0, 0, 0);
            TxtDesc.Text = text;
            TxtDesc.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            TxtDesc.Width = Width - TxtDesc.Margin.Horizontal;
            TxtDesc.BackColor = Color.LightGray;
            TxtDesc.ForeColor = Color.Black;
            TxtDesc.ScrollBars = RichTextBoxScrollBars.None;

            TxtDesc.TextChanged += TextBox_TextChanged;
            TxtDesc.ContentsResized += TextBox_ContentsResized;
            TxtDesc.SizeChanged += SubControl_SizeChanged;

            Controls.Add(TxtDesc);
            Controls.Add(TxtCaption);

            rootSectionDiv.Content = GetXmlForElement();
        }
        /// <summary>
        /// Metoda koja briše Description sa TextBox-a
        /// </summary>
        private void RemoveDescription()
        {
            if (TxtDesc == null)
            {
                return;
            }

            Controls.Remove(TxtDesc);
            TxtDesc.Dispose();
            TxtDesc = null;
            SubControl_SizeChanged(null, null);
            rootSectionDiv.Content = GetXmlForElement();
        }

        /// <summary>
        /// Na promenu Caption-a ažurira XML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            rootSectionDiv.Content = GetXmlForElement();
        }

        /// <summary>
        /// Reakcija na promenu velicine sadrzaja descriptiona slike
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            var richTextBox = (RichTextBox)sender;
            richTextBox.Height = e.NewRectangle.Height + 10;
        }

        public bool ShouldICompress(Image image)
        {
            if (image.Width > 1200 || image.Height > 1200)
            {
                image = Util.ScaleImage(image, 1200, 1200);
                return true;
            }
            return false;
        }

        public void ChangeImage()
        {
            Image tempImg = Util.GetCopyImage(PicBox.Image);
            string pre = GetXmlForElement();
            int trenutniWidth = PicBox.Width;
            int trenutniHeight = PicBox.Height;

            int n = getCountFigureInProject();
            OpenFileDialog imageDialog = new OpenFileDialog();
            imageDialog.Filter = "Image Files|*.png;*.jpg;*.gif;*.JPG";
            if (imageDialog.ShowDialog() == DialogResult.OK)
            {
                Image image = Util.GetCopyImage(imageDialog.FileName);
                SelectableFlowPanel parent = (SelectableFlowPanel)Parent.Parent;
                Image scaleImage = Util.ScaleImage(image, parent.Width, PicBox.Height);
                PicBox.Height = scaleImage.Height;
                PicBox.Width = scaleImage.Width;
                PicBox.Image = image;
                bool compress = ShouldICompress(image);
                if (n > 1)
                {
                    PictureSaveForm pic = new PictureSaveForm(ProjectSingleton.SelectedContent);
                    if (pic.ShowDialog() == DialogResult.OK)
                    {

                        string copyPath = (!compress) ? ProjectSingleton.Project.ResourcesDir + pic.ImeSlike + Path.GetExtension(imageDialog.FileName).ToLower() :
                            ProjectSingleton.Project.ResourcesDir + pic.ImeSlike + ".jpg";
                        PathSlike = Path.GetFileName(copyPath);
                        try
                        {
                            if (!compress)
                            {
                                System.IO.File.Copy(imageDialog.FileName, copyPath, true);
                            }
                            else
                            {
                                compressAndSave(copyPath, image);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("Fajl koji ste uneli se vec nalazi u projektu. ");
                        }
                    }
                }
                else
                {
                    string copyPath = ProjectSingleton.Project.ResourcesDir + PathSlike;
                    string copyTemp = copyPath;
                    string extOrg = Path.GetExtension(imageDialog.FileName);
                    string extOld = Path.GetExtension(copyPath).ToLower();
                    copyPath = copyPath.Replace(extOld, extOrg);
                    PathSlike = Path.GetFileName(copyPath);
                    try
                    {
                        if (!copyPath.Equals(copyTemp))
                        {
                            File.Delete(copyTemp);
                        }
                        if (!compress)
                        {
                            System.IO.File.Copy(imageDialog.FileName, copyPath, true);
                        }
                        else
                        {
                            compressAndSave(copyPath, image);
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Nemate privilegije da ovaj fajl prepisete.");
                    }
                }

                string posle = GetXmlForElement();
                DitaClipboard.ChangeImageUndoState(rootSectionDiv, pre, posle, PathSlike, tempImg, Util.GetCopyImage(PicBox.Image));
                rootSectionDiv.Content = GetXmlForElement();
            }
        }
        /// <summary>
        /// Metoda koja kompresijue sliku u slučaju da je slika prevelika
        /// </summary>
        /// <param name="imageFile"></param>
        /// <param name="image"></param>
        public void compressAndSave(string imageFile, Image image)
        {

            ImageCodecInfo jpgEncoder = Util.GetEncoder(ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            //Fix white pozadina ako je PNG
            using (var b = new Bitmap(image.Width, image.Height))
            {
                b.SetResolution(image.HorizontalResolution, image.VerticalResolution);

                using (var g = Graphics.FromImage(b))
                {
                    g.Clear(Color.White);
                    g.DrawImageUnscaled(image, 0, 0);
                }
                b.Save(imageFile, jpgEncoder, myEncoderParameters);
            }
        }

        /// <summary>
        /// Cekira da li slika vec postoji u LearningContentu projekta
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public int checkIfImageIsInLearningContent(LearningContent content)
        {
            int i = 0;
            foreach (Section sec in content.LearningBody.Sections)
            {
                foreach (Sectiondiv div in sec.SectionDivs)
                {
                    foreach (Sectiondiv subdiv in div.SectionDivs)
                    {
                        foreach (Sectiondiv subsubdiv in subdiv.SectionDivs)
                        {
                            if (subsubdiv.Content != null && subsubdiv.Content.Contains(PathSlike))
                            {
                                i++;
                            }
                        }
                    }
                }
            }
            return i;
        }

        /// <summary>
        /// Dobija broj ponavljanja slike u projektu
        /// </summary>
        /// <returns></returns>
        public int getCountFigureInProject()
        {
            int countPic = 0;
            foreach (LearningContent content in ProjectSingleton.Project.LearningContents)
            {
                countPic += checkIfImageIsInLearningContent(content);
                foreach (LearningContent subContent in content.SubObjects)
                {
                    countPic += checkIfImageIsInLearningContent(subContent);
                }
            }
            return countPic;
        }

        /// <summary>
        /// Definise klik na dugme delete u konteksnom meniju
        /// Ukoliko panel ne sadrzi komponentu to znaci da komponenta
        /// pripada vec nekom panelu pa radimo brisanje Parent komponente
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDelete(object sender, EventArgs e)
        {
            if (!IsUrl)
                if (getCountFigureInProject() == 0)
                {
                    Image tempImg = Util.GetCopyImage(PicBox.Image);
                    ImageDeleted deletedImg = new ImageDeleted();
                    deletedImg.Path = ProjectSingleton.Project.ResourcesDir + PathSlike;
                    deletedImg.Image = tempImg;
                    ProjectSingleton.ImagesToSaveOnClose.Add(deletedImg);
                    File.Delete(ProjectSingleton.Project.ResourcesDir + PathSlike);
                }
        }

        /// <summary>
        /// Metoda koja inicijalizuje (definise) odgovarajuce atribute PictureBox pod komponente.
        /// </summary>
        /// <param name="image"></param>
        public void DefinePictureBox(Image image, Image scaleImage)
        {
            PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PicBox.SizeChanged += PicBox_Resize;
            PicBox.Height = scaleImage.Height;
            PicBox.Width = scaleImage.Width;
            Size = new Size(image.Width, PicBox.Height + 40);
            PicBox.Image = image;
            PicBox.BorderStyle = BorderStyle.FixedSingle;
            ResizeableControlImage.Create(PicBox, this);
        }

        /// <summary>
        /// Metoda koja inicijalizuje (definise) odgovarajuce atribute PictureBox pod komponente.
        /// </summary>
        /// <param name="image"></param>
        public void DefinePictureBoxWithSize(int width, int height)
        {
            PicBox.SizeMode = PictureBoxSizeMode.StretchImage;
            PicBox.SizeChanged += PicBox_Resize;
            PicBox.Height = height;
            PicBox.Width = width;
            Size = new Size(width, PicBox.Height + 40);
            PicBox.BorderStyle = BorderStyle.FixedSingle;
            ResizeableControlImage.Create(PicBox, this);
        }

        /// <summary>
        /// Metoda koja inicijalizuje (definise) odgovarajuce atribute Caption (TextBox) pod komponente.
        /// </summary>
        public void DefineCaption()
        {
            TxtCaption.Text = "Slika-1 ";
            TxtCaption.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
        }


        /// <summary>
        /// Metoda koja validira uneti Caption i ažurira XML za element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TxtCaption_TextChanged(object sender, EventArgs e)
        {
            TxtCaption = sender as TextBoxMemory;
             
            

            Regex r = new Regex(@"^Slika-[0-9]* ");


            Match m = r.Match(TxtCaption.Text);
            if (!m.Success)
            {
                Debug.WriteLine("current " + TxtCaption.Text);
                Debug.WriteLine("saved " + TxtCaption.SavedText);
                if (TxtCaption.SavedText.Trim() == "")
                {
                    TxtCaption.SavedText = "Slika-1 ";
                }
                TxtCaption.Text = TxtCaption.SavedText;
               
            }


            if (rootSectionDiv != null && rootSectionDiv.Content != null)
                rootSectionDiv.Content = GetXmlForElement();

            if (Parent != null)
            {
                TxtCaption.Width = this.Parent.Width;
            }
            ResizeCaption();
        }

        public void ResizeCaption()
        {

            const int padding = 5;
            int numLines = this.TxtCaption.GetLineFromCharIndex(this.TxtCaption.TextLength) + 1;
            int border = this.TxtCaption.Height - this.TxtCaption.ClientSize.Height;
            this.TxtCaption.Height = this.TxtCaption.Font.Height * numLines + padding + border;
        }
        /// <summary>
        /// Na promenu Parent-a postavlja da je širina kontrole jednaka
        /// maksimalnoj širini Parent-a
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            try
            {
                if (Parent != null)
                {
                    Width = Parent.Width;
                    TxtCaption.Width = Width;
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                }
            }
            catch { }
        }

        /// <summary>
        /// Metoda koja reaguje na promeni velicine PictureBox-a
        /// Text mora da se pomeri zajedno sa slikom
        /// Ukoliko postoji Description slike on se takodje mora pomeriti sa slikom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicBox_Resize(object sender, EventArgs e)
        {
            if (rootSectionDiv != null)
            {
                if (!IsUrl)
                {
                    rootSectionDiv.Content = GetXmlForElement();
                }
            }
        }

        /// <summary>
        /// Pri promeni veličine kontrole menja veličine i ostalih kontrola automatski
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubControl_SizeChanged(object sender, EventArgs e)
        {
            ResizeCaption();
            int h = PicBox.Height + PicBox.Margin.Vertical + TxtCaption.Height + TxtCaption.Margin.Vertical;
            if (TxtDesc != null)
            {
                h += TxtDesc.Height + TxtDesc.Margin.Vertical;
            }
            Height = h;
        }

        /// <summary>
        /// Vraća XML za sliku
        /// </summary>
        /// <returns></returns>
        public string GetXmlForElement()
        {
            if (TxtDesc != null)
            {
                return "<fig>    <title>" + Util.EscapeXml(TxtCaption.Text) + "</title>    <desc>" + Util.EscapeXml(TxtDesc.Text) + "</desc>    <image href=\"" + ProjectSingleton.Project.CourseCode + "-" + PathSlike + "\"  width=\"" + ((int)(Math.Round(PicBox.Width * 1.1, 0))) + "px\" height=\"" + ((int)(Math.Round(PicBox.Height * 1.1, 0))) + "px\">    </image>    </fig>";
            }
            else
            {
                return "<fig>    <title>" + Util.EscapeXml(TxtCaption.Text) + "</title>    <image href=\"" + ProjectSingleton.Project.CourseCode + "-" + PathSlike + "\" width=\"" + ((int)(Math.Round(PicBox.Width * 1.1, 0))) + "px\" height=\"" + ((int)(Math.Round(PicBox.Height * 1.1, 0))) + "px\">    </image>     </fig>";
            }
        }

        private string _stateContent;

        /// <summary>
        /// Definiše sadržaj stateContent-a
        /// </summary>
        public void PrepareState()
        {
            if (rootSectionDiv == null)
            {
                return;
            }
            _stateContent = rootSectionDiv.Content;
        }

        /// <summary>
        /// Dodaje novo stanje promene Sectiondiv-a
        /// </summary>
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
