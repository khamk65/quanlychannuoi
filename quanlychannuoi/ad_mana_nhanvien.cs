using Microsoft.Azure.Documents;
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
    public partial class ad_mana_nhanvien : Form
    {
        private Database database;
        public ad_mana_nhanvien()
        {
            InitializeComponent();
            database = new Database();
            label14.Text = database.GetCountUsers().ToString();
            label15.Text = database.GetCountAdmin().ToString();
            label16.Text = database.GetCountnhanvien().ToString();
            ShowUserData();
        }

        private void ad_mana_nhanvien_Load(object sender, EventArgs e)
        {

        }
        private void ShowUserData()
        {
            
        }
        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String name = nameText.Text;
            String email = emailText.Text;

            DataTable userData;

            // Kiểm tra nếu cả hai trường đều rỗng
            if (string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(email))
            {
                // Trường hợp cả hai trường đều rỗng, lấy toàn bộ dữ liệu từ bảng
                userData = database.GetUserData(null, null);
            }
            else
            {
                // Trường hợp một trong hai trường có giá trị hoặc cả hai trường đều có giá trị
                userData = database.GetUserData(email, name);
            }

            // Hiển thị dữ liệu trong DataGridView
            dataGridViewAccounts.DataSource = userData;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ad_chitietNhanvien form1 = new ad_chitietNhanvien();
            form1.Show();
            this.Hide();
        }
    }
}
