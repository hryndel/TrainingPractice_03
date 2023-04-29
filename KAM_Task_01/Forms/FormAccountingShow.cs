using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace KAM_Task_01.Forms
{
    public partial class FormAccountingShow : Form
    {
        static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\AZS.mdf;Integrated Security = True";
        SqlConnection connection = new SqlConnection(connectionString);
        public FormAccountingShow()
        {
            InitializeComponent();
            comboBoxSort.SelectedIndex = 0;
        }

        private void FormAccountingShow_Load(object sender, EventArgs e)
        {
            string query =
            "SELECT " +
            "[Accounting].[IdAccounting], " +
            "[TypesOfFuel].[Name], " +
            "[TypesOfFuel].[Price], " +
            "[SupplierDirectory].[Name], " +
            "[Accounting].[Date], " +
            "[Accounting].[VolumeStart], " +
            "[Accounting].[VolumeSell] " +
            "FROM " +
            "[Accounting], " +
            "[TypesOfFuel], " +
            "[SupplierDirectory] " +
            "WHERE " +
            "([Accounting].[IdFuel]=[TypesOfFuel].[IdFuel]) AND " +
            "([TypesOfFuel].[IdSupplier]=[SupplierDirectory].[IdSupplier])";
            Zapros(query);
        }
        private void Zapros(string query)
        {
            dataGridViewAccounting.Rows.Clear();

            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (reader.Read())
            {
                data.Add(new string[7]);
                data[data.Count - 1][0] = reader[0].ToString();
                data[data.Count - 1][1] = reader[1].ToString();
                data[data.Count - 1][2] = reader[2].ToString();
                data[data.Count - 1][3] = reader[3].ToString();
                data[data.Count - 1][4] = reader[4].ToString();
                data[data.Count - 1][5] = reader[5].ToString();
                data[data.Count - 1][6] = reader[6].ToString();
            }
            reader.Close();
            connection.Close();
            foreach (string[] s in data)
            {
                dataGridViewAccounting.Rows.Add(s);
            }
            StatusLabel.Text = "Количество записей: " + dataGridViewAccounting.RowCount.ToString();
        }

        private void dataGridViewAccounting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            switch (comboBoxSort.SelectedIndex)
            {
                case 0:
                    FormAccountingShow_Load(sender, e);
                    break;
                case 1:
                    dataGridViewAccounting.Sort(ColumnFuel, System.ComponentModel.ListSortDirection.Ascending);
                    break;
                case 2:
                    dataGridViewAccounting.Sort(ColumnFuel, System.ComponentModel.ListSortDirection.Descending);
                    break;
                case 3:
                    dataGridViewAccounting.Sort(ColumnSupplier, System.ComponentModel.ListSortDirection.Ascending);
                    break;
                case 4:
                    dataGridViewAccounting.Sort(ColumnSupplier, System.ComponentModel.ListSortDirection.Descending);
                    break;
                case 5:
                    dataGridViewAccounting.Sort(ColumnDate, System.ComponentModel.ListSortDirection.Ascending);
                    break;
                case 6:
                    dataGridViewAccounting.Sort(ColumnDate, System.ComponentModel.ListSortDirection.Descending);
                    break;
            }
        }
        private void buttonExport_Click(object sender, EventArgs e)
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.Visible = true;
            //Книга
            Microsoft.Office.Interop.Excel.Workbook wBook;
            Microsoft.Office.Interop.Excel.Worksheet xlSheet;
            wBook = xlApp.Workbooks.Add();
            xlApp.Columns.ColumnWidth = 30;
            //Лист
            xlSheet = (Microsoft.Office.Interop.Excel.Worksheet)wBook.Sheets[1];
            //Присвоепние имени листа
            xlSheet.Name = "Таблица учета остатка";
            //Наименование ккаждого столбца
            xlSheet.Cells[1, 1] = "ID";
            xlSheet.Cells[1, 2] = "Топливо";
            xlSheet.Cells[1, 3] = "Цена топлива";
            xlSheet.Cells[1, 4] = "Поставщик";
            xlSheet.Cells[1, 5] = "Дата";
            xlSheet.Cells[1, 6] = "Начальный объем (л.)";
            xlSheet.Cells[1, 7] = "Проданный объем (л.)";
            for (int i = 0; i < dataGridViewAccounting.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridViewAccounting.Columns.Count; j++)
                {
                    xlApp.Cells[i + 2, j + 1] = dataGridViewAccounting.Rows[i].Cells[j].Value.ToString().Replace(",",".");
                }
            }
            xlSheet.Cells.HorizontalAlignment = 3;
            xlApp.Visible = true;
        }
    }
}
