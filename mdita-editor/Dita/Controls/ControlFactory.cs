using System;
using System.Windows.Forms;
using mDitaEditor.Project;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Drawing;

namespace mDitaEditor.Dita.Controls
{
    static class ControlFactory
    {
        /// <summary>
        /// Stavlja equation control na panel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="latex"></param>
        public static void getEquationForPanel(SelectableFlowPanel panel, string latex = null, Sectiondiv div = null)
        {
            if (panel.HeightLeftPanel() < 50)
            {
                MessageBox.Show("Nema više mesta na odabranoj sekciji");
                return;
            }
            if (div == null)
            {
                div = LatexControl.InitSectionDiv(panel.Column);
            }
            panel.Add(new LatexControl(div, latex), div);
        }

        /// <summary>
        /// Stavlja equation control na panel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="latex"></param>
        public static void getYouTubeVideoYouTube(SelectableFlowPanel panel, string link = null, Sectiondiv div = null)
        {
            if (panel.HeightLeftPanel() < 150)
            {
                MessageBox.Show("Potrebno je osloboditi više prostora, kako bi postavili video.");
                return;
            }
            if (div == null)
            {
                div = YouTubeVideoControl.InitSectionDiv(panel.Column);
                panel.Add(new YouTubeVideoControl(link, panel, div), div);
            }
            else
            {
                panel.Add(new YouTubeVideoControl(div), div);
            }
        }

        /// <summary>
        /// Stavlja equation control na panel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="latex"></param>
        public static void getVideo(SelectableFlowPanel panel, string link = null, Sectiondiv div = null)
        {
            if (panel.HeightLeftPanel() < 150)
            {
                MessageBox.Show("Potrebno je osloboditi više prostora, kako bi postavili video.");
                return;
            }
            if (div == null)
            {
                div = VideoControl.InitSectionDiv(panel.Column);
                panel.Add(new VideoControl(link, panel, div), div);
            }
            else
            {
                panel.Add(new VideoControl(div), div);
            }
        }

        /// <summary>
        /// Stavlja equation control na panel
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="latex"></param>
        public static void getAudio(SelectableFlowPanel panel, string link = null, Sectiondiv div = null)
        {
            if (panel.HeightLeftPanel() < 35)
            {
                MessageBox.Show("Potrebno je osloboditi više prostora, kako bi postavili video.");
                return;
            }
            if (div == null)
            {
                div = AudioControl.InitSectionDiv(panel.Column);
                panel.Add(new AudioControl(link, panel, div), div);
            }
            else
            {
                panel.Add(new AudioControl(div), div);
            }
        }

        /// <summary>
        /// Metoda koja dodaje ImageBox komponentu
        /// </summary>
        /// <param name="panel1">Paramatar za Panel</param>
        public static ImageBoxControl getPictureBoxForPanel(SelectableFlowPanel panel, string hasUrl = null)
        {
            Sectiondiv div = null;
            if (ProjectSingleton.Project == null)
            {
                MessageBox.Show("Niste otvorili projekat");
                return null;
            }
            if (panel == null)
            {
                MessageBox.Show("Slike mozete dodavati samo u okviru sekcije. ");
                return null;
            }
            var selected = ProjectSingleton.SelectedContent;
            if(selected != null && selected is LearningContent)
            {
                var learning = (LearningContent)selected;
                if (learning.Shortdesc.Draftcomment[2].Text.Equals("") && div == null)
                {
                    MessageBox.Show("Morate definisati klasifikaciju objekta da biste uneli sliku. ");
                    return null;
                }
            }
            if (panel.HeightLeftPanel() < 150)
            {
                MessageBox.Show("Nema više mesta na odabranoj sekciji");
                return null;
            }
            ImageBoxControl imgBox = null;
            if (div == null)
            {
                div = ImageBoxControl.InitSectionDiv(panel.Column);
            }
            if (hasUrl != null)
            {
                imgBox = new ImageBoxControl(panel, div, hasUrl);
            }
            else {
                imgBox = new ImageBoxControl(panel, div);
            }
            if (!imgBox.IsDisposed)
            {
                panel.Add(imgBox, div);
            }
            return imgBox;
        }

      
        /// <summary>
        /// Dodaje div field za unos na panel
        /// </summary>
        /// <param name="panel1">Panel na koji se dodaje TextField</param>
        public static void getTextFieldForPanel(SelectableFlowPanel panel, Sectiondiv div = null)
        {
            if (panel.HeightLeftPanel() > 30)
            {
                if (div == null)
                {
                    div = TextBoxControl.InitSectionDiv(panel.Column);

                }               
                
                TextBoxControl textBox = new TextBoxControl(div);
                panel.Add(textBox, div);
            
            }
            else
            {
                MessageBox.Show("Nema više mesta na odabranoj sekciji");
            }
        }

        /// <summary>
        /// Dodaje div field za unos na panel sa opcionim pocetnim tekstom
        /// </summary>
        /// <param name="panel1">Panel na koji se dodaje TextField</param>
        /// <param name="text"> Pocetni tekst koji se stavlja u Textbox </param>a
        public static bool getTextFieldForPanel(SelectableFlowPanel panel, string text)
        {
                                                          

            int height = 0;
            using (Graphics g = panel.CreateGraphics())
            {
                Font textBoxFont = TextBoxControl.defaultFont;
                height = (int)g.MeasureString(text,
                    new Font(textBoxFont.FontFamily, textBoxFont.Size + 2.7F, textBoxFont.Style, textBoxFont.Unit, textBoxFont.GdiCharSet)                
                    , panel.Width-2).Height + 15;                           
            }
               
            var TabLength = 4;
            var TabSpace = new String(' ', TabLength);
            text = text.Replace("<br>", "&lt;br&gt;");
            text = text.Replace("\t", TabSpace);
            text = text.Replace(Environment.NewLine, "<br>");
            text = text.Replace(" ", "&nbsp;");

           
            if (panel.HeightLeftPanel() > height)
            {
                Sectiondiv div = TextBoxControl.InitSectionDiv(panel.Column);
                if (text.Length != 0)
                {
                   div.SectionDivs[0].Content = text;
                }                
                TextBoxControl textBox = new TextBoxControl(div);
                   
                panel.Add(textBox, div);

                return true;
             
                
            }
            else
            {
                MessageBox.Show("Nema više mesta na odabranoj sekciji");
                return false;
            }          
       

        }

        /// <summary>
        /// Dodaje div field za unos na panel
        /// </summary>
        /// <param name="panel1">Panel na koji se dodaje TextField</param>
        public static void getNonEditableTextFieldForPanel(SelectableFlowPanel panel, Sectiondiv div = null)
        {
            if (panel.HeightLeftPanel() > 30)
            {
                if (div == null)
                {
                    div = MathMlLoader.InitSectionDiv(panel.Column);
                }
                MathMlLoader textBox = new MathMlLoader(div);
                panel.Add(textBox, div);
            }
            else
            {
                MessageBox.Show("Nema više mesta na odabranoj sekciji");
            }
        }

        /// <summary>
        /// Dodaje snipet sa kodom na panel koji je selektovan. 
        /// </summary>
        public static void getSnippetForPanel(SelectableFlowPanel panel, FastColoredTextBoxNS.FastColoredTextBox snippet, Sectiondiv div)
        {
            if (panel.HeightLeftPanel() < 50)
            {
                MessageBox.Show("Nema više mesta na odabranoj sekciji");
                return;
            }
            panel.Add(snippet, div);
        }

        /// <summary>
        /// Generiše Note polje na Panelu
        /// </summary>
        public static void getNoteForPanel(SelectableFlowPanel panel, Sectiondiv div = null)
        {
            if (panel.HeightLeftPanel() > 50)
            {
                if (div == null)
                {
                    div = NoteControl.InitSectionDiv(panel.Column);
                }
                NoteControl note = new NoteControl(div);
                panel.Add(note, div);
                //  return note;
            }
            else
            {
                MessageBox.Show("Nema više mesta na odabranoj sekciji");
            }
        }
    }
}
