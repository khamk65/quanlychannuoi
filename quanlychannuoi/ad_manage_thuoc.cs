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
    public partial class ad_manage_thuoc : Form
    {
        private Database database;

        public ad_manage_thuoc()
        {
            InitializeComponent();
            database = new Database();
            ShowdailybanthuocData();
        }

        private void ShowdailybanthuocData()
        {
            try
            {
                // Lấy dữ liệu từ bảng dailybanthuoc
                DataTable dailybanthuocData = database.GetData("dailybanthuoc");

                // Hiển thị dữ liệu trên DataGridView
                GridViewAccounts.DataSource = dailybanthuocData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing dailybanthuoc data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private void button5_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string email = GridViewAccounts.Rows[selectedIndex].Cells["email"].Value.ToString();

                string name = GridViewAccounts.Rows[selectedIndex].Cells["ten"].Value.ToString();
                string nguoilienhe = GridViewAccounts.Rows[selectedIndex].Cells["nguoilienhe"].Value.ToString();

                string idchicuc = GridViewAccounts.Rows[selectedIndex].Cells["idchicuc"].Value.ToString();
                string phone = GridViewAccounts.Rows[selectedIndex].Cells["phone"].Value.ToString();
                textBox2.Text = email;
                textBox3.Text = nguoilienhe;
                textBox1.Text = name;
                textBox4.Text = phone;

                textBox7.Text = idchicuc;
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
                bool deleteSuccess = database.DeleteData("dailybanthuoc", Convert.ToInt32(primaryKeyValue));

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    ShowdailybanthuocData(); // Gọi lại hàm tải dữ liệu
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();

                string ten = textBox1.Text;
                string nguoilienhe = textBox3.Text;
                string email = textBox2.Text;
                int phone = Convert.ToInt32(textBox4.Text);

                int idchicuc = Convert.ToInt32(textBox7.Text);

                // Thực hiện sửa dữ liệu và kiểm tra kết quả
                bool updateSuccess = database.UpdateData("dailybanthuoc", Convert.ToInt32(primaryKeyValue), new Dictionary<string, object>
            {
                { "ten", ten },
                { "nguoilienhe", nguoilienhe },
                { "email", email },
                { "phone", phone },
                { "idchicuc", idchicuc }
            });

                if (updateSuccess)
                {
                    // Cập nhật các TextBox trên giao diện với dữ liệu mới
                    textBox2.Text = email;
                    textBox3.Text = nguoilienhe;
                    textBox1.Text = ten;
                    textBox4.Text = phone.ToString();

                    textBox7.Text = idchicuc.ToString();

                    // Cập nhật DataGridView để hiển thị dữ liệu mới
                    ShowdailybanthuocData();

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

        private void button3_Click_1(object sender, EventArgs e)
        {
            string ten = textBox1.Text;
            string nguoilienhe = textBox3.Text;
            string email = textBox2.Text;
            int phone = Convert.ToInt32(textBox4.Text);

            int idchicuc = Convert.ToInt32(textBox7.Text);

            // Thực hiện thêm dữ liệu và kiểm tra kết quả
            bool insertSuccess = database.InsertData("dailybanthuoc", new Dictionary<string, object>
        {
            { "ten", ten },
            { "nguoilienhe", nguoilienhe },
            { "email", email },
            { "phone", phone },
            { "idchicuc", idchicuc }
        });

            if (insertSuccess)
            {
                // Cập nhật các TextBox trên giao diện với dữ liệu mới
                textBox2.Text = email;
                textBox3.Text = nguoilienhe;
                textBox1.Text = ten;
                textBox4.Text = phone.ToString();

                textBox7.Text = idchicuc.ToString();

                // Cập nhật DataGridView để hiển thị dữ liệu mới
                ShowdailybanthuocData();

                MessageBox.Show("Data inserted successfully.");
            }
            else
            {
                MessageBox.Show("Insert failed.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ad_manage_thuy form1 = new ad_manage_thuy();
            form1.Show();
            this.Hide();
        }
    }
}
