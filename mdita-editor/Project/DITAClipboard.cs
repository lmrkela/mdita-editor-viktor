using mDitaEditor.Dita;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Project;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace mDitaEditor.Utils
{
    static partial class DitaClipboard
    {
        public static int CopiedControlHeight = 0;
        private static Section _copiedSection;
        public static Section CopiedSection
        {
            get
            {
                return _copiedSection;
            }
            set
            {
                if (_copiedSection != null)
                {
                    _copiedSection.Dispose();
                }

                if (value == null)
                {
                    _copiedSection = null;
                    return;
                }
                _copiedSection = value.Clone();
            }
        }

        public static Sectiondiv CopiedSectiondiv { get; set; }

        private static string clipboardFormat(string text)
        {
            for (int i = 57344; i <= 63743; i++)
            {
                text = text.Replace((char)i, ' ');
            }
            text = text.Replace("&shy;", "");
            text = Regex.Replace(text, @"[\u00AD]", string.Empty);
            return text;
        }

        public static void Paste(SelectableFlowPanel panel, bool isPreview = false)
        {
           

            //Text
            string clipboardText = Clipboard.GetText(TextDataFormat.UnicodeText);
            if (pasteText(panel, clipboardText, isPreview))
            {
                return;
            }

            //Image
            IDataObject clipboardData = Clipboard.GetDataObject();
            if (clipboardData != null && pasteImage(panel, clipboardData)) {
                MainForm.Instance.enumerateFigures();
                return;
            }


            //Section Div
            if (CopiedSectiondiv == null)
            {
                MessageBox.Show("Niste prethodno kopirali komponentu");
                return;
            }
            if (CopiedControlHeight > panel.HeightLeftPanel())
            {
                MessageBox.Show("Nemate dovoljno prostora da biste iskopirali kontrolu. ");
                return;
            }
            
            Sectiondiv copiedSectiondiv = CopiedSectiondiv.Clone();
            panel.Column.SectionDivs.Add(copiedSectiondiv);
            if (!isPreview)
            {
                ControlAddOrDeleteState(panel.Column, copiedSectiondiv, true);
            }
            MainForm.Instance.enumerateFigures();

        }

        public static bool pasteText(SelectableFlowPanel panel, string clipboardText, bool isPreview)
        {
            clipboardText = clipboardFormat(clipboardText);
            if (clipboardText.Length != 0)
            {

                if (ControlFactory.getTextFieldForPanel(panel, clipboardText) && !isPreview)
                {
                    ControlAddOrDeleteState(panel.Column, ActiveSectiondiv, true);
                }

                return true;
            }
            return false;
        }

        public static bool pasteImage(SelectableFlowPanel destination, IDataObject data)
        {          
                string[] files = (string[])data.GetData(DataFormats.FileDrop);
                if (files != null)
                {
                    if (files.Length > 0)
                    {
                        string file = files[0];
                        if (Path.GetExtension(file) == ".png" || Path.GetExtension(file) == ".jpg" || Path.GetExtension(file) == ".JPG" || Path.GetExtension(file) == ".PNG" || Path.GetExtension(file) == "gif")
                        {
                            ImageBoxControl box = ControlFactory.getPictureBoxForPanel(destination, file);
                            if (box != null)
                            {
                                ControlAddOrDeleteState(destination.Column, box.rootSectionDiv, true);
                            }
                        }
                        return true;
                    }
                }
                if (data.GetDataPresent(DataFormats.EnhancedMetafile) & data.GetDataPresent(DataFormats.MetafilePict))
                {
                    Metafile meta =  GetMetafile(data);
                    Image image = meta;
                    string dir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "mDitaEditor");
                    string file = Path.Combine(dir, "temp.png");
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    image.Save(file, ImageFormat.Png);
                    ImageBoxControl box = ControlFactory.getPictureBoxForPanel(destination, file);
                    if (box != null)
                    {
                        ControlAddOrDeleteState(destination.Column, box.rootSectionDiv, true);
                    }
                    image = null;
                    File.Delete(file);
                    return true;
                }

            return false;       
                     
        }

        static Metafile GetMetafile(System.Windows.Forms.IDataObject obj)
        {
            var iobj = (System.Runtime.InteropServices.ComTypes.IDataObject)obj;
            var etc = iobj.EnumFormatEtc(System.Runtime.InteropServices.ComTypes.DATADIR.DATADIR_GET);
            var pceltFetched = new int[1];
            var fmtetc = new System.Runtime.InteropServices.ComTypes.FORMATETC[1];
            while (0 == etc.Next(1, fmtetc, pceltFetched) && pceltFetched[0] == 1)
            {
                var et = fmtetc[0];
                var fmt = DataFormats.GetFormat(et.cfFormat);
                if (fmt.Name != "EnhancedMetafile")
                {
                    continue;
                }
                System.Runtime.InteropServices.ComTypes.STGMEDIUM medium;
                iobj.GetData(ref et, out medium);
                return new Metafile(medium.unionmember, true);
            }
            return null;
        }

        public static Section Paste(LearningBase learningObject)
        {
            if (CopiedSection == null)
            {
                MessageBox.Show("Niste prethodno kopirali komponentu");
                return null;
            }

            Section copiedSection = CopiedSection.Clone();
            Section preSection = ProjectSingleton.SelectedSection;

            if (preSection != null)
            {
                learningObject.LearningBody.Sections.InsertAfter(preSection, copiedSection);
            }
            else
            {
                learningObject.LearningBody.Sections.Add(copiedSection);
            }
            copiedSection.Parent = learningObject;
            return copiedSection;
        }
    }
}
