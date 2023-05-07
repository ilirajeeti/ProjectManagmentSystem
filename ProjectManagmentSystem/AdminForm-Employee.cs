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
    public partial class AdminForm_Employee : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;

        private string firstName;
        private string lastName;
        private string roleName;

        public AdminForm_Employee(string firstName,string lastName, string roleName)
        {
            server = "localhost";
            database = "projectmanagmentsystem";
            uid = "root";
            password = "";
            string connString;
            connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            conn = new MySqlConnection(connString);
            

            InitializeComponent();
            this.firstName = firstName;
            this.lastName = lastName;
            this.roleName = roleName;

            label1.Text = firstName + " "+ lastName;
            label2.Text = roleName;

            upDate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            AdminForm_User adminUser = new AdminForm_User(firstName, lastName, roleName);
            adminUser.Location = this.Location;
            adminUser.StartPosition = FormStartPosition.Manual;
            adminUser.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AdminForm_Tasks adminTasks = new AdminForm_Tasks(firstName, lastName, roleName);
            adminTasks.Location = this.Location;
            adminTasks.StartPosition = FormStartPosition.Manual;
            adminTasks.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminForm_Projects adminProjects = new AdminForm_Projects(firstName, lastName, roleName);
            adminProjects.Location = this.Location;
            adminProjects.StartPosition = FormStartPosition.Manual;
            adminProjects.Show();
            this.Hide();
        }

        public void upDate()
        {
            conn.Open();
            string query = "select * from employee";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            bunifuDataGridView1.DataSource = table;
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddEmployee insertEmployee = new AddEmployee();
            insertEmployee.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            if (bunifuDataGridView1.SelectedRows.Count > 0)
            {
                
                int id = (int)bunifuDataGridView1.SelectedRows[0].Cells["id"].Value;

                conn.Open();
                string query2 = $"DELETE FROM user WHERE EmployeeId = {id}";
                MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                string query = $"DELETE FROM employee WHERE Id = {id}";               
                MySqlCommand cmd = new MySqlCommand(query, conn);
                
                try
                {
                    cmd2.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    conn.Close();
           
                    MessageBox.Show("Puntori u fshi me sukses");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conn.Close();

                
                bunifuDataGridView1.Rows.RemoveAt(bunifuDataGridView1.SelectedRows[0].Index);
                
            }
        }
    }
}
