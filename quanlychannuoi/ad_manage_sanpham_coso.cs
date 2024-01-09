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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace quanlychannuoi
{
    public partial class ad_manage_sanpham_coso : Form
    {
        private Database database;
        public ad_manage_sanpham_coso()
        {
            InitializeComponent();

            database = new Database();
            ShowCososanxuatData();
        }
        private void ShowCososanxuatData()
        {
            try
            {
                // Lấy dữ liệu từ bảng cososanxuat
                DataTable cososanxuatData = database.GetCososanxuatData();

                // Hiển thị dữ liệu trên DataGridView
                GridViewAccounts.DataSource = cososanxuatData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string email = GridViewAccounts.Rows[selectedIndex].Cells["email"].Value.ToString();
               
                string name = GridViewAccounts.Rows[selectedIndex].Cells["ten"].Value.ToString();
                string nguoilienhe = GridViewAccounts.Rows[selectedIndex].Cells["nguoilienhe"].Value.ToString();
                string quymo = GridViewAccounts.Rows[selectedIndex].Cells["quymo"].Value.ToString();
                string diachi = GridViewAccounts.Rows[selectedIndex].Cells["diachi"].Value.ToString();
                string phone = GridViewAccounts.Rows[selectedIndex].Cells["phone"].Value.ToString();
                textBox2.Text = email;
                textBox3.Text = nguoilienhe;
                textBox1.Text = name;
                textBox4.Text = phone;
                textBox5.Text = quymo;
                textBox7.Text = diachi;
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

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
                bool deleteSuccess = database.DeleteCososanxuat(Convert.ToInt32(primaryKeyValue));

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    ShowCososanxuatData(); //Gọi lại hàm tải dữ liệu
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

        private void button3_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox trên giao diện
            string ten = textBox1.Text;
            string nguoilienhe = textBox3.Text;
            string email = textBox2.Text;
            int phone = Convert.ToInt32(textBox4.Text);
            int quymo = Convert.ToInt32(textBox5.Text);
            string diachi = textBox7.Text;

            // Thực hiện thêm dữ liệu và kiểm tra kết quả
            bool insertSuccess = database.InsertCososanxuat(ten, nguoilienhe, email, phone, quymo, diachi);

            if (insertSuccess)
            {
                // Cập nhật các TextBox trên giao diện với dữ liệu mới
                textBox2.Text = email;
                textBox3.Text = nguoilienhe;
                textBox1.Text = ten; // Giả sử 'ten' là tên biến tương ứng với tên của cơ sở sản xuất
                textBox4.Text = phone.ToString();
                textBox5.Text = quymo.ToString();
                textBox7.Text = diachi;

                // Cập nhật DataGridView để hiển thị dữ liệu mới
                ShowCososanxuatData();

                MessageBox.Show("Data inserted successfully.");
            }
            else
            {
                MessageBox.Show("Insert failed.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString(); // Giả sử textBoxID là TextBox chứa ID cần sửa
                string ten = textBox1.Text;
                string nguoilienhe = textBox3.Text;
                string email = textBox2.Text;
                int phone = Convert.ToInt32(textBox4.Text);
                int quymo = Convert.ToInt32(textBox5.Text);
                string diachi = textBox7.Text;

                // Thực hiện sửa dữ liệu và kiểm tra kết quả
                bool updateSuccess = database.UpdateCososanxuat(Convert.ToInt32(primaryKeyValue), ten, nguoilienhe, email, phone, quymo, diachi);

                if (updateSuccess)
                {
                    // Cập nhật các TextBox trên giao diện với dữ liệu mới
                    textBox2.Text = email;
                    textBox3.Text = nguoilienhe;
                    textBox1.Text = ten;
                    textBox4.Text = phone.ToString();
                    textBox5.Text = quymo.ToString();
                    textBox7.Text = diachi;

                    // Cập nhật DataGridView để hiển thị dữ liệu mới
                    ShowCososanxuatData();

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

        private void button4_Click(object sender, EventArgs e)
        {
            ad_manage_sanpham form1 = new ad_manage_sanpham();
            form1.Show();
            this.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void GridViewAccounts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
