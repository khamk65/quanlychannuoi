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
    public partial class ad_manage_vungchannuoi : Form
    {
        private Database database;
        public ad_manage_vungchannuoi()
        {
            InitializeComponent();
            database = new Database();
            label14.Text = database.GetCountvung().ToString();
            ShowvungData();
        }
        private void ShowvungData()
        {
            try
            {
                // Lấy dữ liệu từ bảng cosogietmo
                DataTable cosogietmoData = database.GetData("vungchannuoi");

                // Hiển thị dữ liệu trên DataGridView
              dataGridViewAccounts.DataSource = cosogietmoData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing cosogietmo data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
          
            string diadiem = nameText.Text;
            int trangthai = Convert.ToInt32(emailText.Text);

            // Thực hiện thêm dữ liệu và kiểm tra kết quả
            bool insertSuccess = database.InsertData("vungchannuoi", new Dictionary<string, object>
        {
            { "diadiem", diadiem },
            { "trangthai", trangthai },
            
        });

            if (insertSuccess)
            {
                // Cập nhật các TextBox trên giao diện với dữ liệu mới
                nameText.Text = diadiem;
                emailText.Text = trangthai.ToString();


                // Cập nhật DataGridView để hiển thị dữ liệu mới
                ShowvungData();

                MessageBox.Show("Data inserted successfully.");
            }
            else
            {
                MessageBox.Show("Insert failed.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridViewAccounts.SelectedRows[0].Index;
                string diadiem = dataGridViewAccounts.Rows[selectedIndex].Cells["diadiem"].Value.ToString();

                string trangthai = dataGridViewAccounts.Rows[selectedIndex].Cells["trangthai"].Value.ToString();

                nameText.Text = diadiem;
                emailText.Text = trangthai;
              
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count > 0)
            {
                // Lấy chỉ số của hàng được chọn
                int selectedIndex = dataGridViewAccounts.SelectedRows[0].Index;

                // Lấy giá trị của cột khóa chính (nếu có)
                string primaryKeyValue = dataGridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();

                // Thực hiện xóa dữ liệu từ cơ sở dữ liệu
                bool deleteSuccess = database.DeleteData("vungchannuoi", Convert.ToInt32(primaryKeyValue));

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    ShowvungData(); // Gọi lại hàm tải dữ liệu
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
            if (dataGridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = dataGridViewAccounts.SelectedRows[0].Index;
                string primaryKeyValue = dataGridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();
                string diadiem = nameText.Text;
                int trangthai = Convert.ToInt32(emailText.Text);




                // Thực hiện sửa dữ liệu và kiểm tra kết quả
                bool updateSuccess = database.UpdateData("cosogietmo", Convert.ToInt32(primaryKeyValue), new Dictionary<string, object>
            {
                { "diadiem", diadiem },
                { "trangthai", trangthai },
               
            });

                if (updateSuccess)
                {
                    // Cập nhật các TextBox trên giao diện với dữ liệu mới
                    nameText.Text = diadiem;
                    emailText.Text = trangthai.ToString();

                    // Cập nhật DataGridView để hiển thị dữ liệu mới
                    ShowvungData();

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

        private void emailText_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
           ad_manage_ho form1 = new ad_manage_ho();
            form1.Show();
            this.Hide();
        }
    }
}
