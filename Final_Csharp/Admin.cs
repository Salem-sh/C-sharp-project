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

namespace Final_Csharp
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Omar\Documents\Storagedb.mdf;Integrated Security=True;Connect Timeout=30");
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Home h1 = new Home();
            h1.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        void populate()
        {
            try
            {
                Con.Open();
                string Myquery = "select * from AdminTbl";
                SqlDataAdapter da = new SqlDataAdapter(Myquery, Con);
                SqlCommandBuilder builder = new SqlCommandBuilder(da);
                var ds = new DataSet();
                da.Fill(ds);
                adminGV.DataSource = ds.Tables[0];
                Con.Close();
            }
            catch
            {

            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("insert into AdminTbl values('" + nameBox.Text + "','" + passbox.Text + "')", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Admin Successfully Added");
                Con.Close();
                populate();
            }
            catch
            {
                MessageBox.Show("Not Working, you may have inserted wrong data type");
                Con.Close();
            }
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (nameBox.Text == "")
            {
                MessageBox.Show("Please Enter a Username");
            }
            else
            {
                try
                {
                    Con.Open();
                    string myquery = "delete from AdminTbl where Username ='" + nameBox.Text + "'";
                    SqlCommand cmd = new SqlCommand(myquery, Con);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Admin Successfully Deleted");
                    Con.Close();
                    populate();
                }
                catch
                {
                    MessageBox.Show("Make sure Admin in the list");
                    Con.Close();
                }
            }
        }

        private void adminGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            adminGV.CurrentRow.Selected = true;
            nameBox.Text = adminGV.SelectedRows[0].Cells[0].Value.ToString();
            passbox.Text = adminGV.SelectedRows[0].Cells[1].Value.ToString();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                SqlCommand cmd = new SqlCommand("update AdminTbl set Username='" + nameBox.Text + "',Password='" + passbox.Text + "' where Username='"+nameBox.Text+"'", Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Admin Successfully Updated");
                Con.Close();
                populate();
            }
            catch
            {
                MessageBox.Show("Not Working");
                Con.Close();
            }
        }
    }
}
