using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoWinFormsDZ7
{
    public partial class addCustomer : Form
    {
        DataTable table;
        public addCustomer(DataTable table)
        {
            InitializeComponent();
            this.table = table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool temp = false;
            foreach (DataRow item in table.Rows)
            {
                if (item.ItemArray[2].ToString() == textBox2.Text)
                {
                    temp = true;
                    MessageBox.Show("This phone number is already registered");
                    break;
                }
            }
            if(!temp)
            {
                DataRow row = table.NewRow();
                row.ItemArray = new object[] { null, textBox1.Text, textBox2.Text };
                table.Rows.Add(row);
                this.DialogResult = DialogResult.OK;
            }         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
