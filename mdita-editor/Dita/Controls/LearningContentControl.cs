using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mDitaEditor.Utils;
using mDitaEditor.CustomControls;
using System.Diagnostics;
using mDitaEditor.Project;

namespace mDitaEditor.Dita.Controls
{
    public partial class LearningContentControl : UserControl
    {
        private LearningContent _content;
  
        /// <summary>
        /// Na setovanje objekt-a automatski se učitavaju svi meta podaci kao i slika
        /// za objekat. Dok ako je objekat null to jest u pitanju je novi objekat podešavaju
        /// se Default vrednosti
        /// </summary>
        public LearningContent Content
        {
            get { return _content; }
            set
            {
        
                if (_content == value)
                {
                    //return;
                }
                _content = null;
                cmbDifficulty.SelectedItem = null;
                _content = value;
                if (Content != null)
                {
                    SuspendLayout();
                    txbTitle.Text = Content.Title.Replace("\t", "");
                    txbDescription.Text = Content.LearningContentBody.LcObjectives.LcDescription;
                    txbClassification.Text = Content.Shortdesc.Draftcomment[2].Text;
                    cmbDifficulty.SelectedItem = Util.GetFormatedDraftComment(Content.Shortdesc.Draftcomment[3].Text);
                    txbKeywords.Text = Content.Shortdesc.Draftcomment[4].Text;
                    txbDuration.Text = Content.Shortdesc.Draftcomment[6].Text;
                    BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject("_" + Content.Id.Split('-')[1], Properties.Resources.Culture);
                    ResumeLayout();
                    Visible = true;
                }
                else
                {
                    Visible = false;
                    txbTitle.Text = "";
                    txbDescription.Text = "";
                    txbClassification.Text = "";
                    cmbDifficulty.SelectedItem = null;
                    txbKeywords.Text = "";
                    txbDuration.Text = "";
                    BackgroundImage = null;
                }

                txbTitle.saveCurrentText();
                txbDescription.saveCurrentText();
                txbClassification.saveCurrentText();
                txbKeywords.saveCurrentText();
                txbDuration.saveCurrentText();
                cmbDifficulty.saveCurrentIndex();
            }
        }


        public LearningContentControl() : this(null)
        { }

        public LearningContentControl(LearningContent content)
        {
            InitializeComponent();
            Content = content;
        }

        /// <summary>
        /// Naslov se odma ažurira u objekat pri izmeni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbTitle_TextChanged(object sender, EventArgs e)
        {
            if (Content != null)
            {
                Content.Title = txbTitle.Text;
            }
        }
        /// <summary>
        /// Klasfikacija se odma ažurira u objekat pri izmeni
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbClassification_TextChanged(object sender, EventArgs e)
        {
            string[] character = { "Š", "Đ", "Č", "Ć", "Ž", "š", "đ", "č", "ć", "ž" };
            string[] characterReplace = { "s", "d", "c", "c", "z", "s", "d", "c", "c", "z" };
            if (txbClassification.Text != null && txbClassification.Text != "")
            {
                if (Regex.IsMatch(txbClassification.Text, @"\p{IsCyrillic}"))
                {
                    txbClassification.Text = Regex.Replace(txbClassification.Text, @"\p{IsCyrillic}", "");
                    txbClassification.SelectionStart = txbClassification.Text.Length;
                    MessageBox.Show("Klasifikacija mora biti na latinici");
                }
                for (int i = 0; i < character.Length; i++)
                {
                    if (txbClassification.Text.Contains(character[i]))
                    {
                        txbClassification.Text = txbClassification.Text.Replace(character[i], characterReplace[i]);
                        txbClassification.SelectionStart = txbClassification.Text.Length;
                    }
                }
            }
            if (Content != null)
            {
                Content.Shortdesc.Draftcomment[2].Text = txbClassification.Text;
            }
        }
        /// <summary>
        /// Ključne reči se odma ažuriraju u objekat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbKeywords_TextChanged(object sender, EventArgs e)
        {
            if (Content != null)
            {
                Content.Shortdesc.Draftcomment[4].Text = txbKeywords.Text;
            }
        }
        /// <summary>
        /// Dužina trajanja se odma čuva u objekat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDuration_TextChanged(object sender, EventArgs e)
        {
            if (Content != null)
            {
                Content.Shortdesc.Draftcomment[6].Text = txbDuration.Text;
            }
        }
        /// <summary>
        /// Promena opisa se odma čuva u objekat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txbDescription_TextChanged(object sender, EventArgs e)
        {
            if (Content != null)
            {
                Content.LearningContentBody.LcObjectives.LcDescription = txbDescription.Text;
            }
        }
        /// <summary>
        /// Menjanje težine objekta učenja se odma čuva u objekat znanja
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Content != null)
            {
                

                if (!cmbDifficulty.UndoRedoEvent && Content.Shortdesc.Draftcomment[3].Text != cmbDifficulty.SelectedItem + "")
                {
                    Debug.WriteLine("changed " + cmbDifficulty.SelectedIndex + " " + cmbDifficulty.OldIndex);
                    DitaClipboard.UpdateObjectDifficultyUndoState(cmbDifficulty);
                    cmbDifficulty.saveCurrentIndex();
                    
                }

                Content.Shortdesc.Draftcomment[3].Text = cmbDifficulty.SelectedItem + "";


            }
        }
       /// <summary>
       /// Kada se pomeri korisnik sa Naslova odma radi proveru grešaka
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void txbField_Leave(object sender, EventArgs e)
        {
            MainForm.Instance.CheckErrorsAndStatistics();
        }
               
        //ako se ide sa sekcije na objekat ne radi undo
        private void txbField_Validated(object sender, EventArgs e)
        {
           DitaClipboard.UpdateSlideTextUndoState(ProjectSingleton.SelectedContent, 
               (CueTextBox)sender, ((CueTextBox)sender).OldText, ((CueTextBox)sender).Text);
        }

        private void txbField_Enter(object sender, EventArgs e)
        {
            ((CueTextBox)sender).saveCurrentText();
        }

      
    }
}
