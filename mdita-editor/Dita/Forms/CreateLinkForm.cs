using System;
using System.Windows.Forms;

namespace mDitaEditor.Dita.Forms
{
    public partial class CreateLinkForm : Form
    {
        public CreateLinkForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(txtInputLink.Text, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp
                              || uriResult.Scheme == Uri.UriSchemeHttps);

            if (uriResult != null)
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Niste uneli validnu adresu");
            }
        }
    }
}
