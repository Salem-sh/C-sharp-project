using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Final_Csharp
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Omar\Documents\Storagedb.mdf;Integrated Security=True;Connect Timeout=30");
        private void Customer_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Add_Click(object sender, EventArgs e)
        {



            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into customerTbl values('" + nameBox.Text + "','" + storageBox.Text + "','" + phoneBox.Text + "','" + durationBox.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {
                MessageBox.Show("Not Working, you may have inserted wrong data type");
                Con.Close();
            }





        }
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from customerTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                customerGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }
        private void customerGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Clear_Click(object sender, EventArgs e)
        {
            nameBox.Text = "";
            storageBox.Text = "";
            phoneBox.Text = "";
            durationBox.Text = "";
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (storageBox.Text == "")
            {
                MessageBox.Show("Please Enter the Storage ID");
            }
            else
            {
                try
                {
                    Con.Open();
                    string myquery = "delete from customerTbl where StorageID ='" + storageBox.Text + "'";
                    SqlCommand cmd = new SqlCommand(myquery, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer Successfully Deleted");
                    Con.Close();
                    populate();
                }
                catch
                {
                    MessageBox.Show("Make sure Storage ID in the list");
                    Con.Close();
                }
            }
        }

        private void customerGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            customerGV.CurrentRow.Selected= true;
            nameBox.Text = customerGV.SelectedRows[0].Cells[0].Value.ToString();
            storageBox.Text = customerGV.SelectedRows[0].Cells[1].Value.ToString();
            phoneBox.Text = customerGV.SelectedRows[0].Cells[2].Value.ToString();
            durationBox.Text = customerGV.SelectedRows[0].Cells[3].Value.ToString();

        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update customerTbl set Name='" + nameBox.Text + "',StorageID='" + storageBox.Text + "',Phone='" + phoneBox.Text + "',Duration='" + durationBox.Text + "' where StorageID='"+storageBox.Text+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {
                MessageBox.Show("Not Working");
                Con.Close();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home h1 = new Home();
            h1.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
