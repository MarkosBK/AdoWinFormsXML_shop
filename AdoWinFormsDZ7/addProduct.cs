using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdoWinFormsDZ7
{
    public partial class addProduct : Form
    {
        DataSet shop;
        Check check;
        ObservableCollection<Product> listProducts = new ObservableCollection<Product>();
        public addProduct(DataSet shop, Check check)
        {
            InitializeComponent();
            this.check = check;
            this.shop = shop;
            comboBox1.DataSource = shop.Tables["products"];
            comboBox1.DisplayMember = "productName";
            comboBox1.ValueMember = "id";
            listProducts.CollectionChanged += ListProducts_CollectionChanged;
        }

        private void ListProducts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            double total=0;
            foreach (Product item in listProducts)
            {
                total += item.price * item.count;
            }
            textBox1.Text = total.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void addProduct_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = 1;
            textBox1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Product product = new Product();
            bool temp = false;
            foreach (DataRow row in shop.Tables["Products"].Rows)
            {
                if((int)row.ItemArray[0]==(int)comboBox1.SelectedValue)
                {
                    product.productName = row.ItemArray[1].ToString();
                    product.price = (double)row.ItemArray[2];
                    product.count = (int)numericUpDown1.Value;
                    product.total = product.price * product.count;
                }
            }
            for (int i = 0; i < listProducts.Count; i++)
            {
                if (listProducts[i].productName == product.productName)
                {
                    listProducts[i].count += (int)numericUpDown1.Value;
                    listBox1.Items[i] = listProducts[i];
                    temp = true;
                    double total = 0;
                    foreach (Product item in listProducts)
                    {
                        total += item.price * item.count;
                    }
                    textBox1.Text = total.ToString();
                    break;
                }
            }          
                
            if(!temp)
            {
                listBox1.Items.Add(product);
                listProducts.Add(product);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            listProducts.RemoveAt(listBox1.SelectedIndex);
            listBox1.Items.RemoveAt(listBox1.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            check.listProducts = listProducts;
            check.total = double.Parse(textBox1.Text);
            this.DialogResult = DialogResult.OK;
        }
   
    }
}
