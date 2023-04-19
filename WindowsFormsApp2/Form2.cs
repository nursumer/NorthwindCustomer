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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("server=316-11\\SQLEXPRESS;database=Northwind;UID=sa;PWD=I$kur2022#!");
        SqlDataAdapter dap;
        private void Form2_Load(object sender, EventArgs e)
        {
    dap = new SqlDataAdapter("Select CustomerId,CompanyName from Customers", conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            listBox1.DataSource = ds.Tables[0];
          
            listBox1.DisplayMember = "CompanyName";
            listBox1.ValueMember = "CustomerId";

           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //SqlDataAdapter dap = new SqlDataAdapter("Update Customers set CompanyName= @name where CustomerId=@id",conn);
            SqlCommand cmd = new SqlCommand("Update Customers set CompanyName= @name where CustomerId=@id", conn);
            dap.UpdateCommand = cmd;
            dap.UpdateCommand.Parameters.AddWithValue("@id", textBox1.Text);
            dap.UpdateCommand.Parameters.AddWithValue("@name", textBox2.Text);
            conn.Open();
            dap.UpdateCommand.ExecuteNonQuery();
            MessageBox.Show("Güncelleme başarılı");
            conn.Close();




        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                textBox1.Text = listBox1.SelectedValue.ToString();
                //textBox2.Text = listBox1.GetItemText(listBox1.SelectedItem);
                DataRowView drv = (DataRowView)listBox1.SelectedItem;
                textBox2.Text = drv["CompanyName"].ToString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dap = new SqlDataAdapter("Select CustomerId,CompanyName from Customers", conn);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            listBox2.DataSource = ds.Tables[0];

            listBox2.DisplayMember = "CompanyName";
            listBox2.ValueMember = "CustomerId";

        }
    }
}
