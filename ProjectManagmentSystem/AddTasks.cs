using Bunifu.UI.WinForms;
using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using Mysqlx;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms.VisualStyles;
using TheArtOfDev.HtmlRenderer.Adapters.Entities;

namespace ProjectManagmentSystem
{
    public partial class AddTasks : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;

        private string firstName;
        private string lastName;
        private string roleName;
        public AddTasks()
        {
            server = "localhost";
            database = "projectmanagmentsystem";
            uid = "root";
            password = "";
            string connString;
            connString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            conn = new MySqlConnection(connString);
            InitializeComponent();
        }

        private void AddTasks_Load(object sender, EventArgs e)
        {
            conn.Open();
            MySqlCommand employeeId = new MySqlCommand("SELECT Id,FirstName,LastName FROM employee", conn);
            MySqlDataReader readerEmployeeId = employeeId.ExecuteReader();

            while (readerEmployeeId.Read())
            {
                string id = readerEmployeeId.GetString("Id");
                string name = readerEmployeeId.GetString("FirstName");
                string lastname = readerEmployeeId.GetString("LastName");
                comboBox2.Items.Add(id + " " + name + " " +lastname); ;
            }
            readerEmployeeId.Close();

            MySqlCommand projectId = new MySqlCommand("SELECT Id,FileName FROM projects", conn);
            MySqlDataReader readerProjectId = projectId.ExecuteReader();

            while (readerProjectId.Read())
            {
                string id = readerProjectId.GetString("Id");
                string name = readerProjectId.GetString("FileName");
                comboBox3.Items.Add(id + " " + name); ;
            }
            readerProjectId.Close();

            MySqlCommand statusId = new MySqlCommand("SELECT StatusName FROM status", conn);
            MySqlDataReader readerStatusId = statusId.ExecuteReader();

            while (readerStatusId.Read())
            {
                string name = readerStatusId.GetString("StatusName");
                comboBox1.Items.Add(name); ;
            }
            readerStatusId.Close();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            string query = "INSERT INTO tasks(TaskName,Description,StartDate,CompletedDate,DateAdded,EmployeeId,ProjectId,StatusId) VALUES (@TaskName,@Description,@StartDate,@CompletedDate,@DateAdded,@EmployeeId,@ProjectId,@StatusId); SELECT LAST_INSERT_ID()";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TaskName", bunifuTextBox1.Text);
            cmd.Parameters.AddWithValue("@Description", richTextBox1.Text);
            cmd.Parameters.AddWithValue("@StartDate", monthCalendar1.SelectionRange.Start);
            cmd.Parameters.AddWithValue("@CompletedDate", monthCalendar2.SelectionRange.Start);
            cmd.Parameters.AddWithValue("@DateAdded", DateTime.Now);
            string employeeId = string.Empty;
            if (comboBox2.SelectedItem is string selectedEmployee)
            {
                int spaceIndex = selectedEmployee.IndexOf(' ');
                if (spaceIndex != -1)
                {
                    employeeId = selectedEmployee.Substring(0, spaceIndex);
                }
            }

            cmd.Parameters.AddWithValue("@EmployeeId", employeeId);

            string projectId = string.Empty;
            if (comboBox3.SelectedItem is string selectedProject)
            {
                int spaceIndex = selectedProject.IndexOf(' ');
                if (spaceIndex != -1)
                {
                    projectId = selectedProject.Substring(0, spaceIndex);
                }
            }

            cmd.Parameters.AddWithValue("@ProjectId", projectId);


            string statusId = string.Empty;
            if (comboBox1.SelectedItem is string selectedStatus)
            {
                int spaceIndex = selectedStatus.IndexOf(' ');
                if (spaceIndex != -1)
                {
                    statusId = selectedStatus.Substring(0, spaceIndex);
                }
            }

            cmd.Parameters.AddWithValue("@StatusId", statusId);


            try
            {
                conn.Open();
                int insertedId = Convert.ToInt32(cmd.ExecuteScalar());

                // Use the insertedId or perform any other required operations
                MessageBox.Show("Task inserted with ID: " + insertedId);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
