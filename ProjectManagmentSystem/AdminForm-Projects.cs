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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuDataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AdminForm_Projects_Load(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand employeeId = new MySqlCommand("SELECT Id,FirstName,LastName FROM employee", conn);
            MySqlDataReader readerEmployeeId = employeeId.ExecuteReader();

            while (readerEmployeeId.Read())
            {
                string id = readerEmployeeId.GetString("Id");
                string name = readerEmployeeId.GetString("FirstName");
                string lastname = readerEmployeeId.GetString("LastName");
                comboBox2.Items.Add(id + " " + name + " " + lastname); ;
            }
            readerEmployeeId.Close();

            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;

            MySqlCommand statusName = new MySqlCommand("SELECT StatusName FROM status", conn);
            MySqlDataReader readerStatusName = statusName.ExecuteReader();

            while (readerStatusName.Read())
            {
                string statusNameId = readerStatusName.GetString("StatusName");
                comboBox1.Items.Add(statusNameId);
            }
            readerStatusName.Close();

            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedEmployee = comboBox2.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedEmployee))
            {
                string employeeId = selectedEmployee.Split(' ')[0];

                
                DataView dataView = ((DataTable)bunifuDataGridView1.DataSource).DefaultView;
                dataView.RowFilter = $"Id = '{employeeId}'";
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedStatusName = comboBox2.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedStatusName))
            {
                string stausId = selectedStatusName.Split(' ')[0];


                DataView dataView = ((DataTable)bunifuDataGridView1.DataSource).DefaultView;
                dataView.RowFilter = $"Id = '{stausId}'";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (roleName == "admin")
            {
                var addProjects = new AddProjects();
                addProjects.Show();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            if (bunifuDataGridView1.SelectedRows.Count > 0 && roleName == "admin")
            {

                int id = (int)bunifuDataGridView1.SelectedRows[0].Cells["id"].Value;



                string query = $"DELETE FROM projects WHERE Id = {id}";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                try
                {

                    cmd.ExecuteNonQuery();
                    conn.Close();

                    MessageBox.Show("Projekti u fshi me sukses");

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
