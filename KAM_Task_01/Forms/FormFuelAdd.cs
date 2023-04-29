using System;
using System.Data;
using System.Windows.Forms;

namespace KAM_Task_01.Forms
{
    public partial class FormFuelAdd : Form
    {
        public FormFuelAdd()
        {
            InitializeComponent();
        }

        private void FormFuelAdd_Load(object sender, EventArgs e)
        {
            this.supplierDirectoryTableAdapter.Fill(this.azsDataSet.SupplierDirectory);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormFuelEdit main = this.Owner as FormFuelEdit;
            if (main != null && textBoxName.Text.Length != 0 && textBoxPrice.Text.Length != 0)
            {
                DataRow nRow = main.azsDataSet.Tables[2].NewRow();
                nRow[1] = textBoxName.Text;
                nRow[2] = textBoxPrice.Text;
                nRow[3] = comboBoxSupplier.SelectedValue;

                main.azsDataSet.Tables[2].Rows.Add(nRow);
                main.typesOfFuelTableAdapter.Update(main.azsDataSet.TypesOfFuel);
                main.azsDataSet.Tables[2].AcceptChanges();
                main.FormFuelEdit_Load(sender, e);

                textBoxName.Text = "";
                textBoxPrice.Text = "";
            } else MessageBox.Show("Название / цена не может быть пустым!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void textBoxPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
