using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using mDitaEditor.Lams.Editor.XMLExporter;
using mDitaEditor.Properties;

namespace mDitaEditor.Lams.Editor.Conditions
{
    public partial class LamsBranchForm : Form
    {
        public ToolOutputBranchActivityEntryDTO SelectedCondition
        {
            get
            {
                if (lvConditions.SelectedIndices.Count != 0)
                {
                    return Branch.Entries[lvConditions.SelectedIndices[0]];
                }
                return null;
            }
        }

        public int SelectedConditionIndex
        {
            get
            {
                if (lvConditions.SelectedIndices.Count != 0)
                {
                    return lvConditions.SelectedIndices[0];
                }
                return -1;
            }
        }

        private LamsBranch _branch;

        public LamsBranch Branch
        {
            get { return _branch; }
            set
            {
                _branch = value;
            }
        }

        public LamsBranchForm(LamsBranch branch, GrafikaItem item)
        {
            InitializeComponent();
            Icon = Icon.FromHandle(Resources.branch24.GetHicon());

            var tools = new List<LamsTool>();
            var prev = item.Previous;
            while (prev != null)
            {
                if (prev.GrafikaObject is IHasConditions)
                {
                    tools.Add(prev.GrafikaObject as LamsTool);
                }
                prev = prev.Previous;
            }
            
            if (tools.Count == 0)
            {
                labEmpty.Visible = true;
            }
            else for (int i = tools.Count - 1; i >= 0; --i)
            {
                cmbInputTool.Items.Add(tools[i]);
            }

            Branch = branch;
            txbName.Text = Branch.TitleText;
            if (Branch.InputTool != null && !cmbInputTool.Items.Contains(Branch.InputTool))
            {
                if (Branch.Entries.Count > 0)
                {
                    cmbInputTool.Items.Insert(0, Branch.InputTool);
                }
                else
                {
                    Branch.InputTool = null;
                }
            }
            cmbInputTool.SelectedItem = Branch.InputTool;
            foreach (var entry in Branch.Entries)
            {
                lvConditions.Items.Add(new ListViewItem(new[] { entry.Condition.DisplayName, entry.BranchPath?.Title }));
            }
            foreach (var branchConnection in Branch.Branches)
            {
                cmbBranch.Items.Add(branchConnection);
                cmbDefaultBranch.Items.Add(branchConnection);
            }
            cmbDefaultBranch.SelectedItem = Branch.DefaultBranch;
        }

        private void txbName_TextChanged(object sender, EventArgs e)
        {
            Branch.TitleText = txbName.Text;
        }

        private IHasConditions _selectedTool;

        private void cmbInputTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbInputTool.SelectedItem == _selectedTool)
            {
                return;
            }
            if (_selectedTool != null && Branch.Entries.Count != 0)
            {
                var result = MessageBox.Show("Ako zamenite objekat obrisaćete sve zahteve.", "Zameniti objekat?", MessageBoxButtons.YesNo);
                if (result != DialogResult.Yes)
                {
                    cmbInputTool.SelectedItem = _selectedTool;
                    return;
                }
                Branch.Entries.Clear();
                lvConditions.Items.Clear();
            }

            cmbInputType.Items.Clear();
            cmbInputType_SelectedIndexChanged(sender, e);
            _selectedTool = cmbInputTool.SelectedItem as IHasConditions;
            if (_selectedTool != null)
            {
                Branch.InputTool = (LamsTool)_selectedTool;
                cmbInputType.Items.AddRange(_selectedTool.ConditionsAvailable);
            }
        }

        private void cmbInputType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var input = cmbInputType.SelectedItem as LamsConditionType;
            if (input != null)
            {
                if (input.Type == ConditionDTO.ValueType.Long)
                {
                    panNumerical.Visible = true;
                    numStartValue.Minimum = numEndValue.Minimum = input.MinValue;
                    numStartValue.Maximum = numEndValue.Maximum = input.MaxValue;
                }
                else
                {
                    panNumerical.Visible = false;
                }
                btnAdd.Enabled = true;
            }
            else
            {
                panNumerical.Visible = false;
                btnAdd.Enabled = false;
            }
        }

        private void chbLessThan_CheckedChanged(object sender, EventArgs e)
        {
            numStartValue.Enabled = chbStartValue.Checked;
            btnAdd.Enabled = chbStartValue.Checked || chbEndValue.Checked;
        }

        private void chbGreaterThan_CheckedChanged(object sender, EventArgs e)
        {
            numEndValue.Enabled = chbEndValue.Checked;
            btnAdd.Enabled = chbStartValue.Checked || chbEndValue.Checked;
        }

        private void lvConditions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = SelectedCondition;
            if (selected != null)
            {
                grbBranchOptions.Enabled = true;
                cmbBranch.SelectedItem = selected.BranchPath;
            }
            else
            {
                grbBranchOptions.Enabled = false;
                cmbBranch.SelectedItem = null;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var input = cmbInputType.SelectedItem as LamsConditionType;
            switch (input.Type)
            {
                case ConditionDTO.ValueType.Bool:
                    AddBoolEntry(input);
                    break;
                case ConditionDTO.ValueType.Long:
                    AddLongEntry(input);
                    break;
            }
        }

        private void AddBoolEntry(LamsConditionType input)
        {
            var entry = new ToolOutputBranchActivityEntryDTO();
            var condition = entry.Condition;
            condition.Name = input.Name;
            condition.Type = input.Type;
            condition.DisplayName = input.Description;
            condition.ExactMatchValue = "true";
            condition.DisplayName = condition.DisplayName + ' ' + condition.ExactMatchValue.ToUpper();
            Branch.Entries.Add(entry);
            lvConditions.Items.Add(new ListViewItem(new[] { condition.DisplayName, entry.BranchPath?.Title }));

            entry = new ToolOutputBranchActivityEntryDTO();
            condition = entry.Condition;
            condition.Name = input.Name;
            condition.Type = input.Type;
            condition.DisplayName = input.Description;
            condition.ExactMatchValue = "false";
            condition.DisplayName = condition.DisplayName + ' ' + condition.ExactMatchValue.ToUpper();
            Branch.Entries.Add(entry);
            lvConditions.Items.Add(new ListViewItem(new[] { condition.DisplayName, entry.BranchPath?.Title }));
        }

        private void AddLongEntry(LamsConditionType input)
        {
            if (chbStartValue.Checked && chbEndValue.Checked && numStartValue.Value > numEndValue.Value)
            {
                MessageBox.Show("Početna vrednost ne može biti veća od krajnje vrednosti.", "Greška!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var entry = new ToolOutputBranchActivityEntryDTO();
            var condition = entry.Condition;
            condition.Name = input.Name;
            condition.Type = input.Type;
            condition.DisplayName = input.Description;
            if (chbStartValue.Checked && chbEndValue.Checked && numStartValue.Value == numEndValue.Value)
            {
                condition.StartValue = condition.EndValue = (long) numStartValue.Value;
                condition.DisplayName = condition.DisplayName + " = " + condition.StartValue;
            }
            else
            {
                if (chbStartValue.Checked)
                {
                    condition.StartValue = (long) numStartValue.Value;
                    condition.DisplayName = condition.StartValue + " <= " + condition.DisplayName;
                }
                if (chbEndValue.Checked)
                {
                    condition.EndValue = (long) numEndValue.Value;
                    condition.DisplayName = condition.DisplayName + " <= " + condition.EndValue;
                }
            }
            Branch.Entries.Add(entry);
            lvConditions.Items.Add(new ListViewItem(new[] { condition.DisplayName, entry.BranchPath?.Title }));
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var index = SelectedConditionIndex;
            Branch.Entries.RemoveAt(index);
            lvConditions.Items.RemoveAt(index);
        }

        private void cmbDefaultBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            Branch.DefaultBranch = cmbDefaultBranch.SelectedItem as GrafikaBranchConnection;
        }

        private void cmbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = SelectedCondition;
            if (selected == null)
            {
                return;
            }
            selected.BranchPath = Branch.Branches[cmbBranch.SelectedIndex];
            lvConditions.Items[lvConditions.SelectedIndices[0]].SubItems[1].Text = selected.BranchPath.Title;
        }
    }
}
