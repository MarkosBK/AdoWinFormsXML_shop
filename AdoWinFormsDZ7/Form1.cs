using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AdoWinFormsDZ7
{
    public partial class Form1 : Form
    {
        DataSet shop = new DataSet("shop");
        DataTable products = new DataTable("products");
        DataTable employees = new DataTable("employees");
        DataTable customers = new DataTable("customers");
        DataTable check = new DataTable("Check");
        List<Check> last10Checks = new List<Check>();
        XmlSerializer xs = new XmlSerializer(typeof(List<Check>));


        public Form1()
        {
            InitializeComponent();
            createShopSchema();
            fillShop();
            comboBox1.DataSource = shop.Tables["employees"];
            comboBox1.DisplayMember = "employeeFIO";
            comboBox2.DataSource = shop.Tables["customers"];
            comboBox2.DisplayMember = "customerFIO";
        }



        private void createShopSchema()
        {
            DataColumn productIdColumn = new DataColumn("id", Type.GetType("System.Int32"));
            productIdColumn.Unique = true;
            productIdColumn.AllowDBNull = false;
            productIdColumn.AutoIncrement = true;
            productIdColumn.AutoIncrementSeed = 1;
            productIdColumn.AutoIncrementStep = 1;   
            DataColumn productNameColumn = new DataColumn("productName", Type.GetType("System.String"));
            DataColumn productPriceColumn = new DataColumn("price", Type.GetType("System.Double"));
            products.Columns.Add(productIdColumn);
            products.Columns.Add(productNameColumn);
            products.Columns.Add(productPriceColumn);
            shop.Tables.Add(products);


            DataColumn employeeIdColumn = new DataColumn("id", Type.GetType("System.Int32"));
            employeeIdColumn.Unique = true;
            employeeIdColumn.AllowDBNull = false;
            employeeIdColumn.AutoIncrement = true;
            employeeIdColumn.AutoIncrementSeed = 1;
            employeeIdColumn.AutoIncrementStep = 1;
            DataColumn employeeFIOColumn = new DataColumn("employeeFIO", Type.GetType("System.String"));
            DataColumn employeePhoneColumn = new DataColumn("employeePhone", Type.GetType("System.String"));
            employeePhoneColumn.Unique = true;
            DataColumn employeeAgeColumn = new DataColumn("age", Type.GetType("System.Int32"));
            employees.Columns.Add(employeeIdColumn);
            employees.Columns.Add(employeeFIOColumn);
            employees.Columns.Add(employeePhoneColumn);
            employees.Columns.Add(employeeAgeColumn);
            shop.Tables.Add(employees);


            DataColumn customerIdColumn = new DataColumn("id", Type.GetType("System.Int32"));
            customerIdColumn.Unique = true;
            customerIdColumn.AllowDBNull = false;
            customerIdColumn.AutoIncrement = true;
            customerIdColumn.AutoIncrementSeed = 1;
            customerIdColumn.AutoIncrementStep = 1;
            DataColumn customerFIOColumn = new DataColumn("customerFIO", Type.GetType("System.String"));
            DataColumn customerPhoneColumn = new DataColumn("customerPhone", Type.GetType("System.String"));
            customerPhoneColumn.Unique = true;
            customers.Columns.Add(customerIdColumn);
            customers.Columns.Add(customerFIOColumn);
            customers.Columns.Add(customerPhoneColumn);
            shop.Tables.Add(customers);


        }
        private void fillShop()
        {
            //DataRow product1 = products.NewRow();
            //product1.ItemArray = new object[] { null, "fridge1", 25000};
            //products.Rows.Add(product1);

            //DataRow product2 = products.NewRow();
            //product2.ItemArray = new object[] { null, "fridge2", 15000 };
            //products.Rows.Add(product2);

            //DataRow product3 = products.NewRow();
            //product3.ItemArray = new object[] { null, "fridge3", 10000 };
            //products.Rows.Add(product3);

            //DataRow product4 = products.NewRow();
            //product4.ItemArray = new object[] { null, "fridge4", 3000 };
            //products.Rows.Add(product4);

            //DataRow product5 = products.NewRow();
            //product5.ItemArray = new object[] { null, "fridge5", 5000 };
            //products.Rows.Add(product5);

            //DataRow product6 = products.NewRow();
            //product6.ItemArray = new object[] { null, "fridge6", 4000 };
            //products.Rows.Add(product6);



            //DataRow employee1 = employees.NewRow();
            //employee1.ItemArray = new object[] { null, "employeeFIO1", "0973231156", 20 };
            //employees.Rows.Add(employee1);

            //DataRow employee2 = employees.NewRow();
            //employee2.ItemArray = new object[] { null, "employeeFIO2", "0970962314", 25 };
            //employees.Rows.Add(employee2);

            //DataRow employee3 = employees.NewRow();
            //employee3.ItemArray = new object[] { null, "employeeFIO3", "0960122576", 23 };
            //employees.Rows.Add(employee3);


            //DataRow customer1 = customers.NewRow();
            //customer1.ItemArray = new object[] { null, "customerFIO1", "0960152271"};
            //customers.Rows.Add(customer1);

            //DataRow customer2 = customers.NewRow();
            //customer2.ItemArray = new object[] { null, "customerFIO2", "0971102895" };
            //customers.Rows.Add(customer2);

            //DataRow customer3 = customers.NewRow();
            //customer3.ItemArray = new object[] { null, "customerFIO3", "0930121254" };
            //customers.Rows.Add(customer3);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //shop.WriteXml("shop.xml");
            shop.ReadXml("shop.xml");    // xml file in the project folder       
            using (FileStream fs = new FileStream("last10Cheks.xml", FileMode.OpenOrCreate))
            {
                last10Checks = (List<Check>)xs.Deserialize(fs);
            }
            listView1.View = View.Details;
            listView1.Columns.Add("dateOfSale");
            listView1.Columns.Add("employeeFIO");
            listView1.Columns.Add("customerFIO");
            listView1.Columns.Add("total");
            foreach (Check check in last10Checks)
            {
                ListViewItem item = new ListViewItem(new string[] {check.buyDate.ToShortDateString(),check.employeeFIO,check.customerFIO,check.total.ToString()});
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Check check = new Check();
            addProduct childForm = new addProduct(shop, check);
            if(childForm.ShowDialog()==DialogResult.OK)
            {
                check.buyDate = DateTime.Now.Date;
                check.employeeFIO = comboBox1.Text;
                check.customerFIO = comboBox2.Text;
                if (last10Checks.Count < 10)
                    last10Checks.Add(check);
                else
                {
                    listView1.Items.RemoveAt(0);
                    last10Checks.RemoveAt(0);
                    last10Checks.Add(check);
                }
                ListViewItem item = new ListViewItem(new string[] { check.buyDate.ToShortDateString(), check.employeeFIO, check.customerFIO, check.total.ToString() });
                listView1.Items.Add(item);

                using (FileStream fs = new FileStream("last10Cheks.xml", FileMode.OpenOrCreate))
                {
                    xs.Serialize(fs, last10Checks);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            addCustomer childForm = new addCustomer(shop.Tables["customers"]);
            if (childForm.ShowDialog()==DialogResult.OK)
            {
                shop.WriteXml("shop.xml");
            }
        }
    }
}
