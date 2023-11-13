using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Hospital_Management_System
{
    public partial class login : Form
    {
        private string database_server;
        private string database_user;
        private string database_password;
        private string database_name;
        public login()
        {
            database_server = "localhost";
            database_user = "root";
            database_password = "";
            database_name = "hospital_management";
            InitializeComponent();
        }
        private bool validate_data_entry()
        {
            if (usertextbox.Text == string.Empty || passtextbox.Text == string.Empty)
            {
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!validate_data_entry())
            {
                MessageBox.Show("please enter username and password");
            }
            else
            {
                string client_user_name = usertextbox.Text;
                string client_password = passtextbox.Text;
                string key = $"SERVER={database_server};USERNAME={database_user};;PASSWORD={database_password};DATABASE={database_name}";
                MySqlConnection connection_p = new MySqlConnection(key);
                string confirm_details = $"SELECT * FROM users WHERE username='{client_user_name}' AND password='{client_password}' ";
                MySqlCommand check = new MySqlCommand(confirm_details, connection_p);
                
                MySqlDataReader check_detail;
                try
                {
                    connection_p.Open();
                    check_detail = check.ExecuteReader();
                    if (check_detail.Read())
                    {
                        this.Hide();
                        dashboard dashboard = new dashboard();
                        dashboard.Show();
                    }
                    else
                    {
                        MessageBox.Show("Check Login Details");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            register register = new register();
            register.Show();
        }
    }
}
