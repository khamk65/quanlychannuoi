using Microsoft.Azure.Documents;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlychannuoi
{
    public partial class admin : Form
    {
        private User currentUser;
        private Database database;
        public admin(User user)
        {
            InitializeComponent();
            database = new Database();
            currentUser = user;
            label7.Text = currentUser.Username;

            label6.Text = currentUser.gmail;
          
        }
      


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            Login form1 = new Login();
            form1.Show();
           
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            Login form1 = new Login();
            form1.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
          
        }

        private void label5_Click_1(object sender, EventArgs e)
        {

            ad_mana_nhanvien form1 = new ad_mana_nhanvien();
            form1.Show();

        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox7.BringToFront();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
         
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            ad_manage_sanpham form1 = new ad_manage_sanpham();
            form1.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ad_manage_thuy form1 = new ad_manage_thuy();
            form1.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ad_manage_ho form1 = new ad_manage_ho();
            form1.Show();
         
        }
    }  
}
