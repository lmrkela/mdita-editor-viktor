using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mDitaEditor.Utils;

namespace mDitaEditor.Dita.Forms
{
    public partial class PictureSaveForm : Form
    {
        private LearningBase trenutniObjekat;
        public string ImeSlike { get; private set; }
        public string Klasifikacija { get; set; }
        Random r = new Random();
        /// <summary>
        /// Konstruktor koji prima objekat kako bi zakljucio koja je klasifikacija istog
        /// </summary>
        /// <param name="objekat"></param>
        public PictureSaveForm(LearningBase objekat, bool isGallery = false)
        {
            InitializeComponent();
            trenutniObjekat = objekat;
            if (trenutniObjekat is LearningContent)
            {
                LearningContent content = (LearningContent)trenutniObjekat;

                if (trenutniObjekat != null && content.Shortdesc.Draftcomment[2].Text != null)
                {
                    Klasifikacija = content.Shortdesc.Draftcomment[2].Text;
                }
                else
                {
                    Klasifikacija = "";
                }
            } else if(trenutniObjekat is LearningSummary)
            {
                Klasifikacija = "Zakljucak";
            }
            else if(trenutniObjekat is LearningOverview)
            {
                Klasifikacija = "Uvod";
            }
            if (isGallery)
            {
                Klasifikacija = "Galerija-" + r.Next(1000);
            }
            GetClassificationForPictureSave();
        }
        /// <summary>
        /// Podešava klasifikaciju za sliku
        /// </summary>
        private void GetClassificationForPictureSave()
        {
            textBoxPictureSave.Text = Klasifikacija + "-Slika";
            textBoxPictureSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        }

        private void PictureSave_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// Čuva ime slike kao paramtar koji kasnije može da se koristi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            ImeSlike = textBoxPictureSave.Text.Replace(" ","");
            ImeSlike = Util.FixStringForPath(ImeSlike);
            if (ImeSlike != Regex.Replace(ImeSlike, @"[^\u0020-\u007E]", string.Empty))
            {
                MessageBox.Show("U imenu slike ne možete koristiti naša slova.Ovo takođe važi i za klasifikaciju!");
            }
            else {
                if (Util.CheckIfFileExistsWithAnyExtensionInResourcesFolder(ImeSlike))
                {
                    DialogResult dialog = MessageBox.Show("TitleDescription slike koji ste upisali je već iskorišćen.\nPrebrisati postojeću sliku?", "Upozorenje", MessageBoxButtons.YesNo);
                    if (dialog == DialogResult.Yes)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
        /// <summary>
        /// Na promenu teksta proverava da li je korisnik obrisao klasifikaciju
        /// i vraća je ako jeste
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPictureSave_TextChanged(object sender, EventArgs e)
        {
            textBoxPictureSave = sender as TextBox;
            if (trenutniObjekat != null)
                if (!textBoxPictureSave.Text.StartsWith(Klasifikacija + "-Slika"))
                {
                    textBoxPictureSave.Text = Klasifikacija + "-Slika";
                    textBoxPictureSave.Select(textBoxPictureSave.Text.Length, 0);
                }
        }
        /// <summary>
        /// Ako korisnik lupi enter pritiska se dugme Save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxPictureSave_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnSave.PerformClick();
            }
        }
    }
}
