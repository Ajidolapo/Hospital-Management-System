using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital_Management_System
{
    public partial class dashboard : Form
    {
        public dashboard()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            patient patient = new patient();
            patient.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 staff = new Form1();
            staff.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            inventory inventory = new inventory();
            inventory.Show();
        }
    }
}
