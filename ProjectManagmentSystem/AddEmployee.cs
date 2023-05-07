using Bunifu.UI.WinForms;
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
    public partial class AddEmployee : Form
    {
        private MySqlConnection conn;
        private string server;
        private string database;
        private string uid;
        private string password;

        private string firstName;
        private string lastName;
        private string roleName;
        public AddEmployee()
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuTextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gender = "";
            if (comboBox1.SelectedIndex == 0)
            {
                gender = "Male";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                gender = "Female";
            }
            else
            {
                // Handle the case where no gender has been selected
            }

            int roleId=0;
            string roleText = comboBox2.SelectedIndex == 0 ? "Admin" : "Employee";
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    roleId = 1;
                    break;
                case 1:
                    roleId = 2;
                    break;
                default:
                    // Handle the case where no role has been selected
                    break;
            }

            string query = "INSERT INTO employee(FirstName,LastName,BirthDate,Gender,Email,PersonalNumber,PhoneNumber) VALUES (@FirstName,@LastName,@BirthDate,@Gender,@Email,@PersonalNumber,@PhoneNumber); SELECT LAST_INSERT_ID()";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FirstName", bunifuTextBox1.Text);
            cmd.Parameters.AddWithValue("@LastName", bunifuTextBox2.Text);
            cmd.Parameters.AddWithValue("@BirthDate", monthCalendar1.SelectionRange.Start);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@Email", bunifuTextBox3.Text);
            cmd.Parameters.AddWithValue("@PersonalNumber", bunifuTextBox4.Text);
            cmd.Parameters.AddWithValue("@PhoneNumber", bunifuTextBox5.Text);
            conn.Open();

            try
            {
                int newEmployeeId = Convert.ToInt32(cmd.ExecuteScalar());

                if (newEmployeeId > 0)
                {
                    // Insert a new user for the employee
                    string username = bunifuTextBox3.Text;
                    string password = "defaultpassword";
                    
                    query = "INSERT INTO user(Email, Password, RoleId, EmployeeId) VALUES (@Email, @Password, @Role, @EmployeeId)";
                    cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", username);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@Role", roleId);
                    cmd.Parameters.AddWithValue("@EmployeeId", newEmployeeId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 1)
                    {
                        AdminForm_Employee updateFunction = new AdminForm_Employee(firstName, lastName, roleName);
                        updateFunction.upDate();
                        MessageBox.Show("Employee and user added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Employee added successfully, but user not added!");
                    }
                    
                    this.Close();   
                    
                }
                else
                {
                    MessageBox.Show("Employee not added!");
                }
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

        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
