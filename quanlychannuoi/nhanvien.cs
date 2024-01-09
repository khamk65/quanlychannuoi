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
    public partial class nhanvien : Form
    {
        private User currentUser;
        private Database database;
        public nhanvien(User user)
        {
            InitializeComponent();
            database = new Database();
            currentUser = user;
            label7.Text = currentUser.Username;

            label6.Text = currentUser.gmail;
        }

        private void nhanvien_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {
            ad_manage_sanpham form1 = new ad_manage_sanpham();
            form1.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ad_manage_ho form1 = new ad_manage_ho();
            form1.Show();
           
        }

        private void label2_Click(object sender, EventArgs e)
        {
            ad_manage_thuy form1 = new ad_manage_thuy();
            form1.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login form1 = new Login();
            form1.Show();
            this.Hide();
        }
    }
}
