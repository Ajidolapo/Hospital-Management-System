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

    public partial class Form1 : Form
    {
        private MySqlConnection the_connection_p;
        private string Server_name;
        private string database_name;
        private string database_user_username;
        private string database_user_password;
        public Form1()
        {
            Server_name = "localhost";
            database_name = "hospital_management";
            database_user_username = "root";
            database_user_password = "";
            InitializeComponent();
        }
        private bool validate_staff_data_entry()
        {
            if (nametextbox.Text == string.Empty || addresstextbox.Text == string.Empty || telephonetextbox.Text == string.Empty || positiontextbox.Text == string.Empty)
            {
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validate_staff_data_entry())
            {
                MessageBox.Show("Please fill in every field");
            }
            else
            {
                string Staffname = nametextbox.Text;
                string Stafftelephone = telephonetextbox.Text;
                string Staffaddress = addresstextbox.Text;
                string Staffposition = positiontextbox.Text;
                string keystring = $"SERVER={Server_name};USERNAME={database_user_username};PASSWORD={database_user_password};DATABASE={database_name}";
                the_connection_p = new MySqlConnection(keystring);
                string involve = $"INSERT INTO staff(id,name,telephone,address,position) VALUES ('',@name,@telephone,@address,@position)";
                
                MySqlCommand cmd = new MySqlCommand(involve, the_connection_p);
                cmd.Parameters.AddWithValue("@name", Staffname);
                cmd.Parameters.AddWithValue("@telephone", Stafftelephone);
                cmd.Parameters.AddWithValue("@address", Staffaddress);
                cmd.Parameters.AddWithValue("position", Staffposition);
                

                try
                {
                    the_connection_p.Open();
                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Staff has been added to data");
                        Close();
                        dashboard dashboard = new dashboard();
                        dashboard.Show();
                    }

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            dashboard dashboard = new dashboard();
            dashboard.Show();
        }
        private void viewgrid()
        {
            string keystring = $"SERVER={Server_name};USERNAME={database_user_username};PASSWORD={database_user_password};DATABASE={database_name}";
            the_connection_p = new MySqlConnection(keystring);
            string show = $"SELECT * FROM staff";
            MySqlDataAdapter adapter = new MySqlDataAdapter(show, the_connection_p);
            DataTable data_stuff = new DataTable();
            adapter.Fill(data_stuff);
            staffDGV.DataSource = data_stuff;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(staffDGV.SelectedRows.Count > 0)
            {
                itextbox.Text = staffDGV.SelectedRows[0].Cells[0].Value.ToString();
                ntextbox.Text = staffDGV.SelectedRows[0].Cells[1].Value.ToString();
                ttextbox.Text = staffDGV.SelectedRows[0].Cells[2].Value.ToString();
                atextbox.Text = staffDGV.SelectedRows[0].Cells[3].Value.ToString();
                ptextbox.Text = staffDGV.SelectedRows[0].Cells[4].Value.ToString();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            viewgrid();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            string id = staffDGV.SelectedRows[0].Cells[0].Value.ToString();
            string name1 = ntextbox.Text = staffDGV.SelectedRows[0].Cells[1].Value.ToString();
            string phone1 = ttextbox.Text = staffDGV.SelectedRows[0].Cells[2].Value.ToString();
            string address1 = atextbox.Text = staffDGV.SelectedRows[0].Cells[3].Value.ToString();
            string position1 = ptextbox.Text = staffDGV.SelectedRows[0].Cells[4].Value.ToString();
            string keystring = $"SERVER={Server_name};USERNAME={database_user_username};PASSWORD={database_user_password};DATABASE={database_name}";
            the_connection_p = new MySqlConnection(keystring);
            string update = $"UPDATE staff SET name = @name, telephone = @telephone, address = @address, position = @position WHERE id = {id}";
            MySqlCommand cmd = new MySqlCommand(update, the_connection_p);            
            cmd.Parameters.AddWithValue("@name", name1);
            cmd.Parameters.AddWithValue("@telephone", phone1);
            cmd.Parameters.AddWithValue("@address", address1);
            cmd.Parameters.AddWithValue("@position", position1);
            



            try
                {
                    the_connection_p.Open();
                    
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Staff has been updated");
                        viewgrid();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }



            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = staffDGV.SelectedRows[0].Cells[0].Value.ToString();
            string keystring = $"SERVER={Server_name};USERNAME={database_user_username};PASSWORD={database_user_password};DATABASE={database_name}";
            the_connection_p = new MySqlConnection(keystring);
            string delete = $"DELETE FROM staff where id={id}";
            MySqlCommand cmd = new MySqlCommand(delete, the_connection_p);
            try
            {
                the_connection_p.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Staff has been deleted");
                    viewgrid();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
