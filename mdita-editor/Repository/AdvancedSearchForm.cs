using System;
using System.Windows.Forms;

namespace mDitaEditor.Repository
{
    public partial class AdvancedSearchForm : Form
    {
        private static AdvancedSearchForm advancedSearch;

        public string Result { get; set; }
        protected AdvancedSearchForm()
        {
            InitializeComponent();
        }


        public static AdvancedSearchForm getIntance()
        {
            if(advancedSearch != null)
            {
                return advancedSearch;
            }
            else
            {
                advancedSearch = new AdvancedSearchForm();
                return advancedSearch;
            }
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbOneOrAll.SelectedIndex = 1;
            cmbCondition.SelectedIndex = 0;
            cmbField.SelectedIndex = 8;
            //DialogResult = DialogResult.No;
        }


        private void btnAddCondition_Click(object sender, EventArgs e)
        {
            if (cmbCondition.SelectedItem != null && cmbField.SelectedItem != null && txtValue.Text != "")
            {
                SearchCondition cond = new SearchCondition(cmbField.SelectedItem.ToString(), cmbCondition.SelectedItem.ToString(), txtValue.Text);
                listConditions.Items.Add(cond);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (listConditions.SelectedItem != null)
            {
                listConditions.Items.Remove(listConditions.SelectedItem);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string query = "";
            for (int i = 0; i < listConditions.Items.Count; i++)
            {
                SearchCondition condition = (SearchCondition)listConditions.Items[i];
                string signNot = (condition.ConditionType.Equals("Is not same as") || condition.ConditionType.Equals("Not contains")) ? "-" : "";
                string containsSign = (condition.ConditionType.Equals("Contains") || condition.ConditionType.Equals("contains")) ? "" : "\"";
                if (i == 0)
                {
                    query += signNot + condition.Field.ToLower() + ":" + containsSign + "" + condition.Match + "" + containsSign;
                }
                else {
                    if (cmbOneOrAll.SelectedItem.ToString().Equals("All"))
                    {
                        query += " AND " + signNot + condition.Field.ToLower() + ":" + containsSign + "" + condition.Match + "" + containsSign;
                    }
                    else
                    {
                        query += " OR " + signNot + condition.Field.ToLower() + ":" + containsSign + "" + condition.Match + "" + containsSign;
                    }
                }
            }
            Result = query;
            DialogResult = DialogResult.OK;
            this.Close();

        }
    }
}
