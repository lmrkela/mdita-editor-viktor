using System;
using System.Collections.Generic;
using System.Windows.Forms;
using mDitaEditor.Dita.Controls;
using mDitaEditor.Project;
using mDitaEditor.Utils;
using System.IO;
using System.Text;

namespace mDitaEditor.Dita.Forms
{
    /// <summary>
    /// Forma u kojoj korisnik moze uneti snippet za prikaz programskog koda.
    /// </summary>
    public partial class EditSnippetForm : Form
    {
        private SnippetCtrl controller;
        private SnippetControl snippetForUpdate;

        public EditSnippetForm()
        {
            InitializeComponent();
            cmbSelectLanguage.SelectedIndex = SnippetCtrl.LastSelectedIndex;
            chkShowLineNumber.Checked = true;
        }

        public EditSnippetForm(SnippetControl snippetForUpdate)
        {
            InitializeComponent();
            this.snippetForUpdate = snippetForUpdate;
            List<string> items = Util.CollectionToList<string>(cmbSelectLanguage.Items);
            int index = items.IndexOf(items.Find(x => x.ToUpper() == snippetForUpdate.Lang.ToUpper()));
            cmbSelectLanguage.SelectedIndex = index;
            chkShowLineNumber.Checked = snippetForUpdate.ShowLineNumbers;
            txtInsertCode.Text = snippetForUpdate.Text;
            numLineCount.Value = snippetForUpdate.Height / SnippetControl.LINE_HEIGHT;
            btnInsertSnippet.Text = "Update";

        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            try
            {
                txtInsertCode.Text = File.ReadAllText(fileDialog.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unablo to load file: \n" + ex.Message, "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Dodavanje snipeta u panel klikom na dugme insert snippet.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInsertSnippet_Click(object sender, EventArgs e)
        {
            string tempContent = "";
            if (snippetForUpdate != null) tempContent = snippetForUpdate.GetXmlForElement();
            SnippetCtrl.LastSelectedIndex = cmbSelectLanguage.SelectedIndex;            
            controller = new SnippetCtrl(txtInsertCode.Text, cmbSelectLanguage.SelectedItem.ToString(), numLineCount.Value.ToString(), chkShowLineNumber.Checked, snippetForUpdate, MainForm.Instance.SelectedPanel);
            controller.AddOrUpdateSnippet();
            if (snippetForUpdate != null) DitaClipboard.UpdateSnippetUnodState(snippetForUpdate.rootSectionDiv, tempContent, snippetForUpdate.GetXmlForElement()); 
            Close();
        }

        private void txtInsertCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case '\r':
                    {
                        var line = GetLine(txtInsertCode.Text, txtInsertCode.SelectionStart);
                        int indent = CountIndent(line);
                        var sb = new StringBuilder("\r\n");
                        for (int i = 0; i < indent; ++i)
                        {
                            sb.Append(' ');
                        }
                        InsertText(sb.ToString());
                        txtInsertCode.ScrollToCaret();
                        e.Handled = true;
                    }
                    break;
                case '\t':
                    {
                        InsertText("    ");
                        e.Handled = true;
                    }
                    break;
                case '}':
                    {
                        if (CountSpaces(txtInsertCode.Text, txtInsertCode.SelectionStart))
                        {
                            RemoveChars(4);
                        }
                    }
                    break;
            }
            
        }

        private static string GetLine(string text, int endIndex)
        {
            if (endIndex == 0)
            {
                return text;
            }
            int startIndex = endIndex - 1;
            while (startIndex > 0)
            {
                if (text[startIndex] == '\n')
                {
                    ++startIndex;
                    break;
                }
                --startIndex;
            }
            return text.Substring(startIndex, endIndex - startIndex);
        }

        private static int CountIndent(string line)
        {
            int i;
            for (i = 0; i < line.Length; ++i)
            {
                if (!char.IsWhiteSpace(line[i]))
                {
                    break;
                }
            }
            if (line.Trim().EndsWith("{"))
            {
                i += 4;
            }
            return i;
        }

        private static bool CountSpaces(string text, int endIndex)
        {
            if (endIndex < 4)
            {
                return false;
            }
            for (int i = 1; i <= 4; ++i)
            {
                if (text[endIndex - i] != ' ')
                {
                    return false;
                }
            }
            return true;
        }

        private void InsertText(string text)
        {
            var selectionIndex = txtInsertCode.SelectionStart;
            txtInsertCode.Text = txtInsertCode.Text.Insert(selectionIndex, text);
            txtInsertCode.SelectionStart = selectionIndex + text.Length;
        }

        private void RemoveChars(int count)
        {
            var selectionIndex = txtInsertCode.SelectionStart;
            txtInsertCode.Text = txtInsertCode.Text.Remove(selectionIndex - count, count);
            txtInsertCode.SelectionStart = selectionIndex - count;
        }
    }
}

