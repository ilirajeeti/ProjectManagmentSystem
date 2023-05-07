using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectManagmentSystem
{
    public partial class Login : Form
    {
        MySqlConnection conn;
        string server;
        string database;
        string uid;
        string password;

        public Login()
        {
            server = "localhost";
            database = "projectmanagmentsystem";
            uid = "root";
            password = "";
            string connString;
            connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            conn = new MySqlConnection(connString);


            try
            {
                conn.Open();
                MessageBox.Show("Connection successful.");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection failed: " + ex.Message);
            }
            InitializeComponent();
        }

        private void bunifuPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuLabel2_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = Color.White;
            bunifuPanel3.BackgroundColor = Color.White;
            textBox2.BackColor = SystemColors.Menu;
            bunifuPanel4.BackgroundColor = Color.Transparent;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = SystemColors.Menu;
            bunifuPanel3.BackgroundColor = Color.Transparent;
            textBox2.BackColor = Color.White;
            bunifuPanel4.BackgroundColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String email, password;
            email = textBox1.Text;
            password = textBox2.Text;
            try
            {
                String query = "SELECT user.*, employee.FirstName, employee.LastName,role.roleName FROM user JOIN role ON user.RoleId = role.Id JOIN employee ON user.EmployeeId = employee.Id WHERE user.Email = '" + textBox1.Text + "' AND user.Password = '" + textBox2.Text + "' AND role.RoleName = 'admin' LIMIT 1";
                MySqlDataAdapter sqlData = new MySqlDataAdapter(query, conn);
                DataTable dtable = new DataTable();
                sqlData.Fill(dtable);
                if (dtable.Rows.Count > 0)
                {
                    // Get the first name and last name of the authenticated user
                    string firstName = dtable.Rows[0]["FirstName"].ToString();
                    string lastName = dtable.Rows[0]["LastName"].ToString();
                    string roleName = dtable.Rows[0]["RoleName"].ToString();

                    email = textBox1.Text;
                    password = textBox2.Text;

                    // Pass the first name and last name as parameters when initializing the AdminForm_Projects form
                    AdminForm_Projects admin = new AdminForm_Projects(firstName, lastName,roleName); 
                    admin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Login Denied");
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
            finally
            {
                conn.Close();
            }
        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuLabel1_Click(object sender, EventArgs e)
        {

        }
    }
}
