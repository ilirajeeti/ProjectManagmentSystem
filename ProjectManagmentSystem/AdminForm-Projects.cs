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
    public partial class AdminForm_Projects : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;


        private string firstName;
        private string lastName;
        private string roleName;
        public AdminForm_Projects(string firstName,string lastName, string roleName)
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

        private void button3_Click(object sender, EventArgs e)
        {

            AdminForm_Tasks adminTasks = new AdminForm_Tasks(firstName, lastName, roleName);
            adminTasks.Location = this.Location;
            adminTasks.StartPosition = FormStartPosition.Manual;
            adminTasks.Show();
            this.Hide();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminForm_Employee adminEmployee = new AdminForm_Employee(firstName, lastName, roleName);
            adminEmployee.Location = this.Location;
            adminEmployee.StartPosition = FormStartPosition.Manual;
            adminEmployee.Show();
            this.Hide();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AdminForm_User adminUser = new AdminForm_User(firstName, lastName, roleName);
            adminUser.Location = this.Location;
            adminUser.StartPosition = FormStartPosition.Manual;
            adminUser.Show();
            this.Hide();
        }

        public void upDate()
        {
            conn.Open();
            string query = "select * from projects";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();
            DataTable table = new DataTable();
            table.Load(reader);
            //bunifuDataGridView1.DataSource = table;
            bunifuDataGridView1.DataSource = table;
            conn.Close();
        }

        private void bunifuDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
