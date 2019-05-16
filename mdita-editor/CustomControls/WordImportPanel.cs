using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using mDitaEditor.Dita;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Project;
using mDitaEditor.Properties;
using mDitaEditor.Utils;

namespace mDitaEditor.CustomControls
{
    public partial class WordImportPanel : UserControl
    {
        Microsoft.Office.Interop.Word.Document doc = null;
        Microsoft.Office.Interop.Word.Application word = null;
        List<string> loadedTexts = new List<string>();
        int showParagraphs = 0;
        List<string> loadedPictures = new List<string>();
        int saverCount = 0;
        public WordImportPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            word = new Microsoft.Office.Interop.Word.Application();
            if (word == null)
            {
                MessageBox.Show("Word is not properly installed!!");
                return;
            }
            if (Util.IsOfficeInstalled())
            {
                showParagraphs = 0;
                DisposePanelControls(flowImages);
                DisposePanelControls(flowParagraphs);
                loadedTexts.Clear();
                loadedPictures.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                OpenFileDialog file = new OpenFileDialog();
                if (float.Parse(word.Version) < 15)
                {
                    file.Filter = " Word files|*.doc;*.docx|Word 97-2003 Documents (*.doc)|*.doc|Word 2007 Documents (*.docx)|*.docx";
                }
                else
                {
                    WordPdfImportWarningRemover remover = new WordPdfImportWarningRemover(word.Version);
                    file.Filter = " Word files|*.doc;*.docx;*.pdf|Word 97-2003 Documents (*.doc)|*.doc|Word 2007 Documents (*.docx)|*.docx|Word PDF Documents (*.pdf)|*.pdf";
                }
                if (file.ShowDialog() == DialogResult.OK)
                {
                    lblFileOpened.Text = Path.GetFileName(file.FileName);
                    picLoading.Visible = true;
                    OpenWord(file.FileName);
                }
            }
            else
            {
                MessageBox.Show("You must have Office package installed for using this option");
            }
        }
        public static void DisposePanelControls(Panel panelControler)
        {
            if (panelControler.InvokeRequired)
            {
                panelControler.Invoke(new MethodInvoker(delegate
                {
                    for (int ix = panelControler.Controls.Count - 1; ix >= 0; --ix)
                    {
                        var ctl = panelControler.Controls[ix];
                        if (ctl != panelControler) ctl.Dispose();
                    }
                }));

            }
            else
            {
                for (int ix = panelControler.Controls.Count - 1; ix >= 0; --ix)
                {
                    var ctl = panelControler.Controls[ix];
                    if (ctl != panelControler) ctl.Dispose();
                }
            }
        }
        private Microsoft.Office.Interop.Word.Application WordApp(string wordPath)
        {
            if (word == null)
            {
                MessageBox.Show("Word is not properly installed!!");
                return null;
            }
            word.Visible = false;
            word.ScreenUpdating = false;
            word.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
            object inputFile = wordPath;
            object confirmConversions = false;
            object readOnly = true;
            object visible = false;
            object missing = Type.Missing;
            doc = word.Documents.Open(
                ref inputFile, ref confirmConversions, ref readOnly, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref visible,
                ref missing, ref missing, ref missing, ref missing);
            return word;
        }

        public TextBox CreateTextBoxWithText(string text)
        {
            TextBox textBox = new TextBox();
            textBox.Text = text;
            textBox.Width = flowParagraphs.Width - 50;
            textBox.Multiline = true;
            textBox.Height = 80;
            textBox.BackColor = Color.White;
            textBox.ReadOnly = true;
            Size size = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            if (size.Height < 80)
            {
                using (Graphics g = CreateGraphics())
                {
                    textBox.Height = (int)g.MeasureString(textBox.Text, textBox.Font, textBox.Width).Height + 15;
                }
            }
            return textBox;
        }
        public Label CreateHeading1Label(string text)
        {
            Label textBox = new Label();
            textBox.Text = text;
            textBox.Width = flowParagraphs.Width - 25;
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.Black;
            textBox.Font = new Font(textBox.Font, FontStyle.Regular);
            textBox.Font = new Font(textBox.Font.FontFamily, 14);
            textBox.Dock = DockStyle.None;
            Size sz = new Size(textBox.Width, int.MaxValue);
            sz = TextRenderer.MeasureText(textBox.Text, textBox.Font, sz, TextFormatFlags.WordBreak);
            textBox.Height = sz.Height - 15;
            textBox.TextAlign = ContentAlignment.MiddleCenter;
            return textBox;
        }
        public Label CreateHeading2Label(string text)
        {
            Label textBox = new Label();
            textBox.Text = text;
            textBox.Width = flowParagraphs.Width - 25;
            textBox.BackColor = Color.White;
            textBox.ForeColor = Color.Black;
            textBox.Dock = DockStyle.None;
            textBox.Font = new Font(textBox.Font, FontStyle.Regular);
            textBox.Font = new Font(textBox.Font.FontFamily, 12);
            Size sz = new Size(textBox.Width, int.MaxValue);
            sz = TextRenderer.MeasureText(textBox.Text, textBox.Font, sz, TextFormatFlags.WordBreak);
            textBox.Height = sz.Height - 15;
            textBox.TextAlign = ContentAlignment.MiddleCenter;
            return textBox;
        }

        public PictureBox CreatePictureBox(string image)
        {
            PictureBox btn = new PictureBox();
            btn.Width = flowImages.Width - 50;
            btn.Height = flowImages.Width - 50;
            btn.Image = GetThumbnail(image);
            btn.BorderStyle = BorderStyle.FixedSingle;
            btn.SizeMode = PictureBoxSizeMode.Zoom;
            return btn;
        }
        public PictureBox CreateButtonForPicture(PictureBox control, string tag)
        {
            PictureBox btn = new PictureBox();
            btn.Width = 24;
            btn.Height = 24;
            btn.Margin = new Padding(0, (control.Height / 2) - 10, 0, 0);
            btn.Tag = tag;
            btn.Image = Resources.add;
            btn.MouseEnter += Btn_MouseEnter;
            btn.MouseLeave += Btn_MouseLeave;
            btn.Click += Btn_Click;
            return btn;
        }

        private void Btn_Click(object sender, EventArgs e)
        {
            PictureBox btn = (PictureBox)sender;
            var panel = MainForm.Instance.SelectedPanel;
            if (panel != null)
            {
                ImageBoxControl box = ControlFactory.getPictureBoxForPanel(panel, btn.Tag + "");
                if (box != null)
                {
                    DitaClipboard.ControlAddOrDeleteState(panel.Column, box.rootSectionDiv, true);
                }
            }
        }

        public CheckBox CreateButtonForText(TextBox tag)
        {
            CheckBox btn = new CheckBox();
            btn.Width = 24;
            btn.Height = 24;
            btn.Margin = new Padding(0, (tag.Height / 2) - 10, 0, 0);
            btn.Tag = tag;
            return btn;
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            box.Image = Resources.add;
        }

        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            PictureBox box = (PictureBox)sender;
            box.Image = Resources.add;
        }

        private void AddHeadingText(string text)
        {
            if (flowParagraphs.InvokeRequired)
            {
                flowParagraphs.Invoke(new MethodInvoker(delegate
                {
                    flowParagraphs.Controls.Add(CreateHeading1Label(text));
                }));
            }
            else
            {
                flowParagraphs.Controls.Add(CreateHeading1Label(text));
            }
        }

        private void AddHeading2Text(string text)
        {
            if (flowParagraphs.InvokeRequired)
            {
                flowParagraphs.Invoke(new MethodInvoker(delegate
                {
                    flowParagraphs.Controls.Add(CreateHeading2Label(text));
                }));
            }
            else
            {
                flowParagraphs.Controls.Add(CreateHeading2Label(text));
            }
        }

        private void AddTextBox(string text)
        {
            TextBox box = CreateTextBoxWithText(text.Replace("\a", ""));
            CheckBox checkBox = CreateButtonForText(box);
            if (flowParagraphs.InvokeRequired)
            {
                flowParagraphs.Invoke(new MethodInvoker(delegate
                {
                    flowParagraphs.Controls.Add(box);
                    flowParagraphs.Controls.Add(checkBox);
                }));
            }
            else
            {
                flowParagraphs.Controls.Add(box);
                flowParagraphs.Controls.Add(checkBox);
            }
        }

        public void LoadParagraphs()
        {

            int totalPages = (int)Math.Ceiling(float.Parse(loadedTexts.Count + "") / 50);
            int page = (showParagraphs / 50) + 1;
            if (lblPages.InvokeRequired)
            {
                lblPages.Invoke(new MethodInvoker(delegate
                {
                    lblPages.Text = "Page " + page + " of " + totalPages;
                }));
            }
            else
            {
                lblPages.Text = "Page " + page + " of " + totalPages;
            }
            if (flowParagraphs.InvokeRequired)
            {
                flowParagraphs.Invoke(new MethodInvoker(delegate
            {
                flowParagraphs.SuspendLayout();
                DisposePanelControls(flowParagraphs);
            }));
            }
            else
            {
                flowParagraphs.SuspendLayout();
                DisposePanelControls(flowParagraphs);
            }
            for (int i = showParagraphs; i < (50 + showParagraphs); i++)
            {
                if (i > (loadedTexts.Count - 1) || i < 0)
                {
                    break;
                }
                if (loadedTexts[i].StartsWith("heading1-"))
                {
                    AddHeadingText(loadedTexts[i].Replace("heading1-", ""));
                }
                else if (loadedTexts[i].StartsWith("heading2-"))
                {
                    AddHeading2Text(loadedTexts[i].Replace("heading2-", ""));
                }
                else
                {
                    AddTextBox(loadedTexts[i]);
                }
            }
            if (flowParagraphs.InvokeRequired)
            {
                flowParagraphs.Invoke(new MethodInvoker(delegate
            {
                flowParagraphs.ResumeLayout();
            }));
            }
            else
            {
                flowParagraphs.ResumeLayout();

            }
        }
        private Image GetThumbnail(string filename)
        {
            Image.GetThumbnailImageAbort callback = new Image.GetThumbnailImageAbort(ThumbCallback);
            Image image = new Bitmap(filename);



            double ratio = Convert.ToDouble((double)image.Width / image.Height);

            int w = 100;
            int h = 100;
            if (ratio > 1)
            {
                w = 100;
                h = Convert.ToInt32(100 / ratio);
            }
            else if (ratio < 1)
            {
                w = Convert.ToInt32(100 / ratio);
                h = 100;
            }

            Image thumb = image.GetThumbnailImage(w, h, callback, new IntPtr());
            return thumb;
        }
        public bool ThumbCallback()
        {
            return true;
        }
        private void OpenWord(string wordPath)
        {
            Thread t = new Thread(() =>
            {
                try
                {
                    Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
                    Thread.CurrentThread.IsBackground = true;
                    word = WordApp(wordPath);
                    if (word == null)
                    {
                        return;
                    }
                    doc.Activate();
                    foreach (Microsoft.Office.Interop.Word.Paragraph paragraph in doc.Paragraphs)
                    {
                        string text = paragraph.Range.Text;
                        Microsoft.Office.Interop.Word.Style style = paragraph.get_Style() as Microsoft.Office.Interop.Word.Style;
                        if (style != null)
                        {
                            string styleName = style.NameLocal;
                            if (styleName == "Heading 1")
                            {
                                loadedTexts.Add("heading1-" + text);
                                // AddHeadingText(text);
                            }
                            else if (styleName == "Heading 2")
                            {
                                loadedTexts.Add("heading2-" + text);
                                //  AddHeading2Text(text);
                            }
                            else
                            {
                                if (paragraph.Range.Tables.Count == 0)
                                {
                                    string trimmed = text.Trim();
                                    if (trimmed != "" && trimmed != "/" && !trimmed.Equals("\u0001") && !trimmed.Equals("\a"))
                                    {
                                        loadedTexts.Add(text);
                                        //AddTextBox(text);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (paragraph.Range.Tables.Count == 0)
                            {
                                string trimmed = text.Trim();
                                if (trimmed != "" && trimmed != "/" && !trimmed.Equals("\u0001") && !trimmed.Equals("\a"))
                                {
                                    loadedTexts.Add(text);
                                    //AddTextBox(text);
                                }
                            }
                        }
                    }
                    foreach (Microsoft.Office.Interop.Word.InlineShape ils in doc.InlineShapes)
                    {
                        if (ils != null)
                        {
                            if (ils.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapePicture || ils.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapeLinkedPicture || ils.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapeLinkedPictureHorizontalLine || ils.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapePictureBullet || ils.Type == Microsoft.Office.Interop.Word.WdInlineShapeType.wdInlineShapePictureHorizontalLine)
                            {
                                ils.Select();
                                SaveInlineShapeToFile(word);
                            }
                        }
                        if (ils.HasChart == Microsoft.Office.Core.MsoTriState.msoTrue)
                        {
                            ils.Select();
                            word.Selection.Copy();
                            SaveFromClipboard();
                        }
                    }
                    foreach (Microsoft.Office.Interop.Word.Shape ils in doc.Shapes)
                    {
                        if (ils != null)
                        {
                            if (ils.Type == Microsoft.Office.Core.MsoShapeType.msoPicture || ils.Type == Microsoft.Office.Core.MsoShapeType.msoLinkedPicture)
                            {
                                ils.Select();
                                SaveInlineShapeToFile(word);
                            }
                        }
                        if (ils.HasChart == Microsoft.Office.Core.MsoTriState.msoTrue)
                        {
                            ils.Select();
                            word.Selection.Copy();
                            SaveFromClipboard();
                        }
                    }

                    CopyTableToExcelAndExport();
                    LoadParagraphs();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
                catch { }
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }





        public void ExcelAppOpenAnSaveImage(Microsoft.Office.Interop.Word.Table table)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("Excel is not properly installed!!");
                    return;
                }

                try
                {
                    table.Select();
                    word.Selection.Copy();
                    xlApp.Visible = false;
                    xlApp.DisplayAlerts = false;
                    Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                    Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;
                    xlWorkBook = xlApp.Workbooks.Add(misValue);
                    xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    Microsoft.Office.Interop.Excel.Range excelCell = (Microsoft.Office.Interop.Excel.Range)xlWorkSheet.get_Range("A1", "A1");
                    xlWorkSheet.Paste();
                    xlApp.CutCopyMode = 0;
                    for (int i = 1; i <= table.Columns.Count; i++)
                    {
                        try
                        {
                            xlWorkSheet.Columns[i].ColumnWidth = 8.43 / 48 * (table.Columns[i].Cells[1].Width);
                        }
                        catch { }
                    }
                    xlApp.Selection.Copy();
                    SaveFromClipboard();
                    xlWorkBook.Close(false);
                    xlApp.Quit();
                }
                catch
                {

                }
            }
            catch
            {
                MessageBox.Show("Excel is not properly installed!!");
            }
        }
        public void CopyTableToExcelAndExport()
        {
            Thread.CurrentThread.IsBackground = true;
            foreach (Microsoft.Office.Interop.Word.Table table in doc.Tables)
            {
                ExcelAppOpenAnSaveImage(table);
            }
            object save = false;
            doc.Close(save);
            word.Quit();
            if (picLoading.InvokeRequired)
            {
                picLoading.Invoke(new MethodInvoker(delegate
                {
                    picLoading.Visible = false;
                }));
            }
        }
        public void SaveFromClipboard()
        {
            try
            {
                if (Clipboard.GetDataObject() != null)
                {
                    var data = Clipboard.GetDataObject();
                    if (data != null && data.GetDataPresent(DataFormats.Bitmap))
                    {
                        var image = (Image)data.GetData(DataFormats.Bitmap, true);
                        var currentBitmap = new Bitmap(image);
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                        string filePath = Path.Combine(path, "tempmdita");
                        if (!Directory.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string pathSave = Path.Combine(filePath, string.Format("img_{0}.png", saverCount));
                        currentBitmap.Save(pathSave);
                        if (File.Exists(pathSave))
                        {
                            PictureBox box = CreatePictureBox(pathSave);
                            if (flowImages.InvokeRequired)
                            {
                                flowImages.Invoke(new MethodInvoker(delegate
                                {
                                    PictureBox btn = CreateButtonForPicture(box, pathSave);
                                    flowImages.Controls.Add(box);
                                    flowImages.Controls.Add(btn);
                                }));
                            }
                            saverCount++;
                        }
                    }
                }
                Clipboard.Clear();
            }
            catch { }
        }
        protected void SaveInlineShapeToFile(Microsoft.Office.Interop.Word.Application wordApplication)
        {
            wordApplication.Selection.Copy();
            SaveFromClipboard();
        }

        private void WordImport_Load(object sender, EventArgs e)
        {
        }

        private void tablControl_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tablControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tablControl.SelectedIndex == 0)
            {
                btnAddText.Visible = true;
            }
            else {
                btnAddText.Visible = false;
            }
        }

        private void btnAddText_Click(object sender, EventArgs e)
        {
            string entireText = "";
            foreach (Control c in flowParagraphs.Controls)
            {
                if (c is CheckBox)
                {
                    CheckBox check = (CheckBox)c;

                    if (check.Checked)
                    {
                        TextBox text = c.Tag as TextBox;
                        entireText += "<p>" + text.Text + "</p>";
                    }
                }
            }
            if (entireText != "")
            {
                var panel = MainForm.Instance.SelectedPanel;
                if (panel != null)
                {
                    if (panel.HeightLeftPanel() > 30)
                    {
                        Sectiondiv div = TextBoxControl.InitSectionDiv(panel.Column);
                        div.SectionDivs[0].Content = entireText;
                        TextBoxControl textBox = new TextBoxControl(div);
                        panel.Add(textBox, div);
                        DitaClipboard.ControlAddOrDeleteState(panel.Column, DitaClipboard.ActiveSectiondiv, true);
                    }
                    else
                    {
                        MessageBox.Show("Nema više mesta na odabranoj sekciji");
                    }
                }
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            showParagraphs += 50;
            if (showParagraphs > loadedTexts.Count)
            {
                showParagraphs -= 50;
                return;
            }
            LoadParagraphs();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (showParagraphs == 0)
            {
                return;
            }
            showParagraphs -= 50;
            LoadParagraphs();
        }
    }
}
