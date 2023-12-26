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
using System.Data.Sql;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Final_Csharp
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Omar\Documents\Storagedb.mdf;Integrated Security=True;Connect Timeout=30");
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from productTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                productGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void customerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void Add_Click(object sender, EventArgs e)
        {



            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into productTbl values('" + nameBox.Text + "','" + quantBox.Text + "','" + storageBox.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Product Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {
                MessageBox.Show("Not Working\n1-Wrong data type\n2-duplicat name\nIf none of the above then the storage does not exist, pls go to Manage customers and add a new one");
                Con.Close();
            }




        }

        private void Products_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (ItemName.Text == "")
            {
                MessageBox.Show("Please Enter the Item Name");
            }
            else
            {
                try
                {
                    Con.Open();
                    string myquery = "delete from productTbl where ItemName='" + nameBox.Text + "'";
                    SqlCommand cmd = new SqlCommand(myquery, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product Successfully Deleted");
                    Con.Close();
                    populate();
                }
                catch
                {
                    MessageBox.Show("Make sure Item Name in the list");
                    Con.Close();
                }
                
              
            }
        }

        private void productGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            productGV.CurrentRow.Selected = true;
            nameBox.Text = productGV.SelectedRows[0].Cells[0].Value.ToString();
            quantBox.Text = productGV.SelectedRows[0].Cells[1].Value.ToString();
            storageBox.Text = productGV.SelectedRows[0].Cells[2].Value.ToString();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update productTbl set ItemName='" + nameBox.Text + "',Quantity='" + quantBox.Text + "',Storage_ID='" + storageBox.Text + "' where ItemName='" + nameBox.Text + "'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("product Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {
                MessageBox.Show("Not Working\n1-Wrong data type\n2-duplicat name\nIf none of the above then the storage does not exist, pls go to Manage customers and add a new one");
                Con.Close();
            }
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            nameBox.Text = "";
            quantBox.Text = "";
            storageBox.Text = "";
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home h1=new Home();
            h1.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Con.Open();
            string output = "";
            string sql;
            sql = "SELECT * FROM productTbl";
            SqlCommand cmd = new SqlCommand(sql, Con); ;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                output += "item name : " + reader["itemName"] + " quan : " + reader["Quantity"] + "storage ID : " + reader["storage_ID"] + "\n";

            }
            using (StreamWriter writer = File.CreateText("newfile.txt"))
            {
                await writer.WriteLineAsync(output);

            }
            Con.Close();
        }
    }
}
