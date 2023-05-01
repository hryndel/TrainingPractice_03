using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KAM_Task_01.Forms
{
    public partial class FormFuelEdit : Form
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AZS.mdf;Integrated Security = True";
        SqlConnection connection = new SqlConnection(connectionString);
        public FormFuelEdit()
        {
            InitializeComponent();
        }

        public void FormFuelEdit_Load(object sender, EventArgs e)
        {
            string query =
            "SELECT " +
            "[TypesOfFuel].[IdFuel], " +
            "[TypesOfFuel].[Name], " +
            "[TypesOfFuel].[Price], " +
            "[SupplierDirectory].[Name] " +
            "FROM " +
            "[TypesOfFuel], " +
            "[SupplierDirectory] " +
            "WHERE " +
            "([TypesOfFuel].[IdSupplier]=[SupplierDirectory].[IdSupplier]) ";
            Zapros(query);
        }
        private void Zapros(string query)
        {
            dataGridViewFuel.Rows.Clear();
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[4]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
            {
                dataGridViewFuel.Rows.Add(s);
            }
            StatusLabel.Text = "Количество записей: " + dataGridViewFuel.RowCount.ToString();
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

        private void buttonAccept_Click(object sender, EventArgs e)
        {
            string value = " < ";
            if (!radioButtonDown.Checked && !radioButtonUp.Checked || textBoxPrice.Text.Length == 0)
                return;
            if (radioButtonUp.Checked) 
                value = " > ";
            string query =
            "SELECT " +
            "[TypesOfFuel].[IdFuel], " +
            "[TypesOfFuel].[Name], " +
            "[TypesOfFuel].[Price], " +
            "[SupplierDirectory].[Name] " +
            "FROM " +
            "[TypesOfFuel], " +
            "[SupplierDirectory] " +
            "WHERE " +
            "([TypesOfFuel].[IdSupplier]=[SupplierDirectory].[IdSupplier]) AND " +
            "([TypesOfFuel].[Price]" + value + textBoxPrice.Text + ") ";
            Zapros(query);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            radioButtonDown.Checked = false;
            radioButtonUp.Checked = false;
            textBoxPrice.Clear();
            FormFuelEdit_Load(sender, e);
        }

        private void dataGridViewFuel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Вы хотите удалить запись?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            //{
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM [TypesOfFuel] WHERE IdFuel = @pId", connection);
                command.Parameters.Add(new SqlParameter("@pId", dataGridViewFuel.CurrentRow.Cells["ColumnID"].Value));
                command.ExecuteNonQuery();
                connection.Close();
                FormFuelEdit_Load(sender, e);
            //}
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Вы хотите сохранить изменения?", "Информация", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.OK)
            //{
                bindingSource.EndEdit();
                typesOfFuelTableAdapter.Update(azsDataSet);
                FormFuelEdit_Load(sender, e);
            //}
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormFuelAdd form = new FormFuelAdd();
            form.Owner = this;
            form.Show();
        }
    }
}
