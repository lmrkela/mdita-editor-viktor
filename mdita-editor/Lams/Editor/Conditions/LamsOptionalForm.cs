using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Editor.Conditions
{
    public partial class LamsOptionalForm : Form
    {
        public LamsOptional Optional { get; private set; }

        public LamsOptionalForm(LamsOptional optional)
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Resources.additional_activity24.GetHicon());

            Optional = optional;
            foreach (var control in MainForm.Instance.grafikaPanel.ListControl.PreviewControls)
            {
                if (!control.Transparent)
                {
                    lbAvailable.Items.Add(control.GrafikaObject);
                }
            }

            txbName.Text = Optional.TitleText;
            lbSubObjects.Items.AddRange(Optional.SubObjects.ToArray());
        }

        private void txbName_TextChanged(object sender, System.EventArgs e)
        {
            Optional.TitleText = txbName.Text;
        }

        private void lbSubItems_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            btnRemove.Enabled = lbSubObjects.SelectedItem != null;
        }

        private void btnRemove_Click(object sender, System.EventArgs e)
        {
            var obj = lbSubObjects.SelectedItem as IGrafikaObject;
            if (obj != null)
            {
                Optional.SubObjects.Remove(obj);
                lbSubObjects.Items.Remove(obj);
                MainForm.Instance.grafikaPanel.ListControl.ShowObject(obj);
            }
        }

        private void lbAvailable_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            btnAdd.Enabled = lbAvailable.SelectedItem != null;
        }

        private void btnAdd_Click(object sender, System.EventArgs e)
        {
            var obj = lbAvailable.SelectedItem as IGrafikaObject;
            if (obj != null)
            {
                lbAvailable.Items.Remove(obj);
                Optional.SubObjects.Add(obj);
                lbSubObjects.Items.Add(obj);
                MainForm.Instance.grafikaPanel.ListControl.HideObject(obj);
            }
        }
    }
}
