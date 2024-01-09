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
    public partial class ad_chitietNhanvien : Form
    {
        private Database database;
        public ad_chitietNhanvien()
        {
       
            InitializeComponent(); 
            database = new Database();
            ShowUserData();
        }
        private void ShowUserData()
        {
            DataTable userData = database.GetUser();
            GridViewAccounts.DataSource = userData;
        }


        private void dataGridViewAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
      

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy dữ liệu từ các ô TextBox hoặc các điều khiển khác tùy vào thiết kế của bạn
                string gmail = textBox5.Text;
                string mk = textBox4.Text;
                int role = 2; // Đặt quyền mặc định hoặc lấy từ điều khiển phù hợp
                string ten = textBox6.Text;

                // Thêm người dùng
                bool success = database.AddUser(gmail, mk, role, ten);

                if (success)
                {
                    MessageBox.Show("User added successfully.");
                    DataTable userData = database.GetUser();
                    GridViewAccounts.DataSource = userData;
                }
                else
                {
                    MessageBox.Show("Failed to add user.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ad_mana_nhanvien form1 = new ad_mana_nhanvien();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                // Lấy chỉ số của hàng được chọn
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;

                // Lấy giá trị của cột khóa chính (nếu có)
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();

                // Thực hiện xóa dữ liệu từ cơ sở dữ liệu
                bool deleteSuccess = database.DeleteUser(Convert.ToInt32(primaryKeyValue));

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    DataTable userData = database.GetUser();
                    GridViewAccounts.DataSource = userData; //Gọi lại hàm tải dữ liệu
                    MessageBox.Show("Row deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Delete failed.");
                }
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();

                string newEmail = textBox2.Text;
                string newPassword = textBox3.Text;
                
                string newName = textBox1.Text;

                // Gọi hàm UpdateUser để cập nhật thông tin người dùng
                bool updateSuccess = database.UpdateUser(Convert.ToInt32(primaryKeyValue), newEmail, newPassword, newName);

                if (updateSuccess)
                {
                    MessageBox.Show("User updated successfully.");
                    // Nếu cần, làm mới DataGridView để hiển thị thông tin đã cập nhật
                    // GridViewAccounts.Refresh();
                    DataTable userData = database.GetUser();
                    GridViewAccounts.DataSource = userData;
                }
                else
                {
                    MessageBox.Show("Update failed.");
                }
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string email = GridViewAccounts.Rows[selectedIndex].Cells["gmail"].Value.ToString();
                string password = GridViewAccounts.Rows[selectedIndex].Cells["mk"].Value.ToString();
                string name = GridViewAccounts.Rows[selectedIndex].Cells["ten"].Value.ToString();
                textBox2.Text = email;
                textBox3.Text = password;
                textBox1.Text = name;
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
            }

        private void ad_chitietNhanvien_Load(object sender, EventArgs e)
        {

        }
    }
}
