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
    public partial class inventory : Form
    {
        private MySqlConnection ensure_connection;
        private string server_name;
        private string database_user_username;
        private string database_user_password;
        private string database_name;
        public inventory()
        {
            server_name = "localhost";
            database_user_username = "root";
            database_user_password = "";
            database_name = "hospital_management";
            InitializeComponent();
        }
        private bool test()
        {
            if(nametextbox.Text.Trim() == string.Empty || producttextbox.Text.Trim() == string.Empty || descriptiontextbox.Text.Trim() == string.Empty)
            {
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!test())
            {
                MessageBox.Show("No field should be left empty");
            }
            else
            {
                
                string name_of_product = nametextbox.Text;
                string product_code = producttextbox.Text;
                string description_of_product = descriptiontextbox.Text;
                if (!Yesbutton.Checked && !Nobutton.Checked)
                {
                    MessageBox.Show("You need to select if available or not");
                }
                if (Yesbutton.Checked)
                {
                    string Product_availability = Yesbutton.Text.ToString();
                    string admitted = Yesbutton.Text.ToString();
                    string keyconnection = $"SERVER={server_name};USERNAME={database_user_username};PASSWORD={database_user_password};DATABASE={database_name}";
                    ensure_connection = new MySqlConnection(keyconnection);
                    string include = "INSERT INTO inventory(id,name,product_code,Description,availability) VALUES ('',@name,@product_code,@description,@availability)";
                    MySqlCommand cmd = new MySqlCommand(include, ensure_connection);
                    cmd.Parameters.AddWithValue("@name",name_of_product);
                    cmd.Parameters.AddWithValue("@product_code", product_code);
                    cmd.Parameters.AddWithValue("@description", description_of_product);
                    cmd.Parameters.AddWithValue("@availability", Product_availability);
                    try
                    {
                        ensure_connection.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Drug has been added to data");
                            this.Close();
                            dashboard dashboard = new dashboard();
                            dashboard.Show();
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Second:" + ex.Message);
                    }

                }
                if (Nobutton.Checked)
                {
                    string product_availability = Nobutton.Text.ToString();
                    string keyconnection = $"SERVER={server_name};USERNAME={database_user_username};PASSWORD={database_user_password};DATABASE={database_name}";
                    ensure_connection = new MySqlConnection(keyconnection);
                    string include = "INSERT INTO inventory(id,name,product_code,Description,availability) VALUES ('',@name,@product_code,@description,@availability)";
                    MySqlCommand cmd = new MySqlCommand(include, ensure_connection);
                    
                    cmd.Parameters.AddWithValue("@name", name_of_product);
                    cmd.Parameters.AddWithValue("@product_code", product_code);
                    cmd.Parameters.AddWithValue("@description", description_of_product);
                    cmd.Parameters.AddWithValue("@availability", product_availability);
                    try
                    {
                        ensure_connection.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Drug has been added to data");
                            this.Close();
                            
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Last" + ex.Message);
                    }
                }



            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            dashboard dashboard = new dashboard();
            dashboard.Show();
        }
    }
}
