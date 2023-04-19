using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        SqlConnection conn = new SqlConnection("server=316-11\\SQLEXPRESS;database=Northwind;UID=sa;PWD=I$kur2022#!");

        private void Form1_Load(object sender, EventArgs e)
        {
           

            SqlDataAdapter dap = new SqlDataAdapter("Select CustomerId,CompanyName from Customers; select shipperId,CompanyName from Shippers", conn);

            //dap.SelectCommand = new SqlCommand("insert ....." , conn);
            // dap.SelectCommand
            // dap.InsertCommand
            // dap.UpdateCommand
            // dap.DeleteCommand

            DataSet ds = new DataSet();
            dap.Fill(ds);
            ds.Tables[0].TableName = "Musteriler";
            ds.Tables[1].TableName = "Kargocular";

            listBox1.DataSource = ds.Tables[0];
            listBox1.DisplayMember = "CompanyName";
            listBox1.ValueMember = "CustomerId";

            comboBox2.DataSource = ds.Tables["Kargocular"];
            comboBox2.DisplayMember = "CompanyName";
            comboBox2.ValueMember = "ShipperId";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dap = new SqlDataAdapter("Select (convert(nvarchar(20),OrderId) + '>>' + convert(nvarchar(11), OrderDate))as erp from Orders where CustomerId=@cid and ShipVia=@sid", conn);
            dap.SelectCommand.Parameters.AddWithValue("@cid", listBox1.SelectedValue);
            dap.SelectCommand.Parameters.AddWithValue("@sid", comboBox2.SelectedValue);

            DataTable dt = new DataTable();
            dap.Fill(dt);
            listBox2.DataSource = dt;
            listBox2.DisplayMember = "erp";

        }
    }
}
