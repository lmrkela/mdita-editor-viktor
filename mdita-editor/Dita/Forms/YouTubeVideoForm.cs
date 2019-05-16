using System;
using System.Windows.Forms;
using mDitaEditor.Dita.Controls;

namespace mDitaEditor.Dita.Forms
{
    public partial class YouTubeVideoForm : Form
    {
        private SelectableFlowPanel panel;
        private YouTubeVideoControl control;
        public YouTubeVideoForm(SelectableFlowPanel _panel = null, YouTubeVideoControl _control = null)
        {
            control = _control;         
            panel = _panel;
            InitializeComponent();

            if (control != null)
            {
                txtInputLink.Text = control.getPath();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string tmp = "";
            if (control != null) tmp = control.GetXmlForElement();

            Uri uriResult;
            bool result = Uri.TryCreate(txtInputLink.Text, UriKind.Absolute, out uriResult)
                          && (uriResult.Scheme == Uri.UriSchemeHttp
                              || uriResult.Scheme == Uri.UriSchemeHttps);
                           

            if (uriResult != null)
            {


                DialogResult = DialogResult.OK;

                if (uriResult.ToString().Contains("youtube"))
                {
                    if (txtInputLink.Text.Contains("&"))
                    {
                        int indexOf = txtInputLink.Text.IndexOf('&');
                        txtInputLink.Text = txtInputLink.Text.Substring(0, indexOf);
                    }

                    if (panel!= null && control == null)
                    {
                        ControlFactory.getYouTubeVideoYouTube(panel, txtInputLink.Text);
                    }
                    else if(control != null)
                    {
                        //control.updatePath(txtInputLink.Text);
                        control.redefineControl(txtInputLink.Text);
                        Utils.DitaClipboard.UpdateYouTubeUndoState(control.getRootSectionDiv(), tmp, control.GetXmlForElement());

                    }
                    //DitaClipboard.AddUndoState(ProjectSingleton.SelectedSection);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Niste uneli validnu YouTube adresu");
                }
            }
            else
            {
                MessageBox.Show("Niste uneli validnu YouTube adresu");
            }
        }

        private void YouTubeVideoForm_Load(object sender, EventArgs e)
        {

        }

        private void txtInputLink_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }
    }
}
