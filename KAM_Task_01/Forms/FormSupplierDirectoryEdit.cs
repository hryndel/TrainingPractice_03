using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KAM_Task_01.Forms
{
    public partial class FormSupplierDirectoryEdit : Form
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AZS.mdf;Integrated Security = True";
        SqlConnection connection = new SqlConnection(connectionString);
        public FormSupplierDirectoryEdit()
        {
            InitializeComponent();
        }
        private void FormSupplierDirectoryEdit_Load(object sender, EventArgs e)
        {
            supplierDirectoryTableAdapter.Fill(azsDataSet.SupplierDirectory);
            StatusLabel.Text = "Количество записей: " + dataGridViewSupplierDirectory.RowCount.ToString();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Вы хотите удалить запись?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            //{
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [SupplierDirectory] WHERE IdSupplier = @pId", connection);
                command.Parameters.Add(new SqlParameter("@pId", dataGridViewSupplierDirectory.CurrentRow.Cells["ColumnID"].Value));
                command.ExecuteNonQuery();
                connection.Close();
                FormSupplierDirectoryEdit_Load(sender, e);
            //}
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (textBoxName.Text.Replace(" ", "") != "")
            {
                DataRow nRow = azsDataSet.Tables[1].NewRow();
                nRow[1] = textBoxName.Text;
                azsDataSet.Tables[1].Rows.Add(nRow);
                supplierDirectoryTableAdapter.Update(azsDataSet.SupplierDirectory);
                azsDataSet.Tables[1].AcceptChanges();
                FormSupplierDirectoryEdit_Load(sender, e);
                textBoxName.Text = "";
            }
            else MessageBox.Show("Название не может быть пустым!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxName.Clear();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Вы хотите сохранить изменения?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            //{
                bindingSource.EndEdit();
                supplierDirectoryTableAdapter.Update(azsDataSet);
                FormSupplierDirectoryEdit_Load(sender, e);
            //}
        }
        private void dataGridViewSupplierDirectory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }
        private void numericUpDownID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
