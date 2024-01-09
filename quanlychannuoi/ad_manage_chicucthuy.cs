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
    public partial class ad_manage_chicucthuy : Form
    {
        private Database database;
        public ad_manage_chicucthuy()
        {
            InitializeComponent();
            database = new Database();
            ShowCosokhaonghiemData();
        }
        private void ShowCosokhaonghiemData()
        {
            try
            {
                // Lấy dữ liệu từ bảng cososanxuat
                DataTable cososanxuatData = database.GetchicucthuyData();

                // Hiển thị dữ liệu trên DataGridView
                GridViewAccounts.DataSource = cososanxuatData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ad_manage_sanpham form1 = new ad_manage_sanpham();
            form1.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void ad_manage_chicucthuy_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string ten = textBox1.Text;
            string nguoilienhe = textBox3.Text;
            string email = textBox2.Text;
            int phone = Convert.ToInt32(textBox4.Text);

            string diachi = textBox7.Text;

            // Thực hiện thêm dữ liệu và kiểm tra kết quả
            bool insertSuccess = database.Insertchicucthuy(ten, nguoilienhe, email, phone, diachi);

            if (insertSuccess)
            {
                // Cập nhật các TextBox trên giao diện với dữ liệu mới
                textBox2.Text = email;
                textBox3.Text = nguoilienhe;
                textBox1.Text = ten; // Giả sử 'ten' là tên biến tương ứng với tên của cơ sở sản xuất
                textBox4.Text = phone.ToString();

                textBox7.Text = diachi;

                // Cập nhật DataGridView để hiển thị dữ liệu mới
                ShowCosokhaonghiemData();

                MessageBox.Show("Data inserted successfully.");
            }
            else
            {
                MessageBox.Show("Insert failed.");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString(); // Giả sử textBoxID là TextBox chứa ID cần sửa
                string ten = textBox1.Text;
                string nguoilienhe = textBox3.Text;
                string email = textBox2.Text;
                int phone = Convert.ToInt32(textBox4.Text);

                string diachi = textBox7.Text;

                // Thực hiện sửa dữ liệu và kiểm tra kết quả
                bool updateSuccess = database.Updatechicucthuy(Convert.ToInt32(primaryKeyValue), ten, nguoilienhe, email, phone, diachi);

                if (updateSuccess)
                {
                    // Cập nhật các TextBox trên giao diện với dữ liệu mới
                    textBox2.Text = email;
                    textBox3.Text = nguoilienhe;
                    textBox1.Text = ten;
                    textBox4.Text = phone.ToString();

                    textBox7.Text = diachi;

                    // Cập nhật DataGridView để hiển thị dữ liệu mới
                    ShowCosokhaonghiemData();

                    MessageBox.Show("Data updated successfully.");
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                // Lấy chỉ số của hàng được chọn
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;

                // Lấy giá trị của cột khóa chính (nếu có)
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();

                // Thực hiện xóa dữ liệu từ cơ sở dữ liệu
                bool deleteSuccess = database.Deletechicucthuy(Convert.ToInt32(primaryKeyValue));

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    ShowCosokhaonghiemData(); //Gọi lại hàm tải dữ liệu
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

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string email = GridViewAccounts.Rows[selectedIndex].Cells["email"].Value.ToString();

                string name = GridViewAccounts.Rows[selectedIndex].Cells["ten"].Value.ToString();
                string nguoilienhe = GridViewAccounts.Rows[selectedIndex].Cells["nguoilienhe"].Value.ToString();

                string diachi = GridViewAccounts.Rows[selectedIndex].Cells["diachi"].Value.ToString();
                string phone = GridViewAccounts.Rows[selectedIndex].Cells["phone"].Value.ToString();
                textBox2.Text = email;
                textBox3.Text = nguoilienhe;
                textBox1.Text = name;
                textBox4.Text = phone;

                textBox7.Text = diachi;
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }
    }
}