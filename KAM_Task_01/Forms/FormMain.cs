using KAM_Task_01.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KAM_Task_01
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            pictureBoxLogo.Parent = pictureBoxBg;
            labelName.Parent = pictureBoxBg;
            labelLozung.Parent = pictureBoxBg;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void supplierDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSupplierDirectoryEdit form = new FormSupplierDirectoryEdit();
            form.Owner = this;
            form.Show();
        }

        private void fuelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFuelEdit form = new FormFuelEdit();
            form.Owner = this;
            form.Show();
        }

        private void accountingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAccountingShow form = new FormAccountingShow();
            form.Owner = this;
            form.Show();
        }
    }
}
