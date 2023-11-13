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
    public partial class patient : Form
    {
        private MySqlConnection connection_p;
        private string server_name;
        private string database_name;
        private string database_username;
        private string database_password;
    
        public patient()
        {
            server_name = "localhost";
            database_name = "hospital_management";
            database_username = "root";
            database_password = "";
            InitializeComponent();
        }
        private bool validate_patient_entry()
        {
            if (nametextbox.Text == string.Empty || addresstextbox.Text == string.Empty || telephonetextbox.Text == string.Empty || illnesstextbox.Text == string.Empty)
            {
                return false;
            }
            return true;
        }
        private void telephonetextbox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validate_patient_entry())
            {
                MessageBox.Show("Please fill in all space");
            }
            else
            {
                string patient_name = nametextbox.Text;
                string patient_telephone = telephonetextbox.Text;
                string patient_address = addresstextbox.Text;
                string patient_illness = illnesstextbox.Text;
                if (!Yesbutton.Checked && !Nobutton.Checked)
                {
                    MessageBox.Show("Please select admitted status");
                }
                if (Yesbutton.Checked)
                {
                    string admitted_status = Yesbutton.Text.ToString();
                    string connectionkey = $"SERVER={server_name};USERNAME={database_username};PASSWORD={database_password};DATABASE={database_name}";
                    connection_p = new MySqlConnection(connectionkey);
                    string addto = $"INSERT INTO patient(id,name,telephone,address,Illness,Admitted) VALUES ('',@name,@telephone,@address,@illness,@admitted)";
                    MySqlCommand cmd = new MySqlCommand(addto, connection_p);
                    cmd.Parameters.AddWithValue("@name", patient_name);
                    cmd.Parameters.AddWithValue("@telephone", patient_telephone);
                    cmd.Parameters.AddWithValue("@address", patient_address);
                    cmd.Parameters.AddWithValue("illness", patient_illness);
                    cmd.Parameters.AddWithValue("admitted", admitted_status);
                    try
                    {
                        connection_p.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Patient information has been added to database");
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
                if (Nobutton.Checked)
                {
                    string admitted = Nobutton.Text.ToString();
                    string connectionstring = $"SERVER={server_name};USERNAME={database_username};PASSWORD={database_password};DATABASE={database_name}";
                    connection_p = new MySqlConnection(connectionstring);
                    string add = $"INSERT INTO patient(id,name,telephone,address,Illness,Admitted) VALUES ('',@name,@telephone,@address,@illness,@admitted)";
                    MySqlCommand cmd = new MySqlCommand(add, connection_p);
                    cmd.Parameters.AddWithValue("@name", patient_name);
                    cmd.Parameters.AddWithValue("@telephone", patient_telephone);
                    cmd.Parameters.AddWithValue("@address", patient_address);
                    cmd.Parameters.AddWithValue("illness", patient_illness);
                    cmd.Parameters.AddWithValue("admitted", admitted);
                    try
                    {
                        connection_p.Open();
                        if (cmd.ExecuteNonQuery() > 0)
                        {
                            MessageBox.Show("Patient information has been Added Successfully");
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

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            dashboard dashboard = new dashboard();
            dashboard.Show();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        private void viewgrid()
        {
            string connectionkey = $"SERVER={server_name};USERNAME={database_username};PASSWORD={database_password};DATABASE={database_name}";
            connection_p = new MySqlConnection(connectionkey);
            string show = $"SELECT * FROM patient";
            MySqlDataAdapter adapter = new MySqlDataAdapter(show, connection_p);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            staffDGV.DataSource = dataTable;
        }
        private void staffDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (staffDGV.SelectedRows.Count > 0)
            {
                itextbox.Text = staffDGV.SelectedRows[0].Cells[0].Value.ToString();
                ntextbox.Text = staffDGV.SelectedRows[0].Cells[1].Value.ToString();
                ttextbox.Text = staffDGV.SelectedRows[0].Cells[2].Value.ToString();
                atextbox.Text = staffDGV.SelectedRows[0].Cells[3].Value.ToString();
                ptextbox.Text = staffDGV.SelectedRows[0].Cells[4].Value.ToString();
                adtextbox.Text = staffDGV.SelectedRows[0].Cells[5].Value.ToString();
            }
        }

        private void patient_Load(object sender, EventArgs e)
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
            string admitted1 = adtextbox.Text = staffDGV.SelectedRows[0].Cells[5].Value.ToString();
            string connectionkey = $"SERVER={server_name};USERNAME={database_username};PASSWORD={database_password};DATABASE={database_name}";
            connection_p = new MySqlConnection(connectionkey);
            string update = $"UPDATE patient SET name = @name, telephone = @telephone, address = @address, illness = @position, admitted = @admitted WHERE id = {id}";
            MySqlCommand cmd = new MySqlCommand(update, connection_p);
            cmd.Parameters.AddWithValue("@name", name1);
            cmd.Parameters.AddWithValue("@telephone", phone1);
            cmd.Parameters.AddWithValue("@address", address1);
            cmd.Parameters.AddWithValue("@position", position1);
            cmd.Parameters.AddWithValue("@admitted", admitted1);




            try
            {
                connection_p.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Patient has been updated");
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
            string connectionstring = $"SERVER={server_name};USERNAME={database_username};PASSWORD={database_password};DATABASE={database_name}";
            connection_p = new MySqlConnection(connectionstring);
            string delete = $"DELETE FROM patient where id={id}";
            MySqlCommand cmd = new MySqlCommand(delete, connection_p);
            try
            {
                connection_p.Open();

                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Patient has been deleted");
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
