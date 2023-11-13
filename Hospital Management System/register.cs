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
    public partial class register : Form
    {
        private string database_server;
        private string database_user;
        private string database_password;
        private string database_name;

        public register()
        {
            database_server = "localhost";
            database_user = "root";
            database_password = "";
            database_name = "hospital_management";
            InitializeComponent();
        }
        private bool validateuser()
        {
            if(nametextbox.Text == string.Empty || phonetextbox.Text == string.Empty || usertextbox.Text == string.Empty || passtextbox.Text == string.Empty)
            {
                return false;
            }
            return true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!validateuser())
            {
                MessageBox.Show("Please ensure all details are entered");
            }
            else
            {
                string client_name = nametextbox.Text;
                string client_phone = phonetextbox.Text;
                string u_name = usertextbox.Text;
                string p_word = passtextbox.Text;
                string key = $"SERVER={database_server};USERNAME={database_user};;PASSWORD={database_password};DATABASE={database_name}";
                MySqlConnection connection_p = new MySqlConnection(key);
                string enter = $"INSERT INTO users(id,name,telephone,username,password) VALUES ('',@name,@telephone,@username,@password)";
                MySqlCommand cmd = new MySqlCommand(enter, connection_p);
                cmd.Parameters.AddWithValue("@name",client_name);
                cmd.Parameters.AddWithValue("@telephone", client_phone);
                cmd.Parameters.AddWithValue("@username", u_name);
                cmd.Parameters.AddWithValue("@password", p_word);
                try
                {
                    connection_p.Open();
                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Registered");
                        Close();
                        dashboard dashboard = new dashboard();
                        dashboard.Show();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
