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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Xml.Linq;

namespace quanlychannuoi
{
    public partial class ad_manage_Hochannuoi : Form
    {
        private Database database;
        public ad_manage_Hochannuoi()
        {
            InitializeComponent();
            database = new Database();
            ShowCosogietmoData();
        }
        private void ShowCosogietmoData()
        {
            try
            {
                // Lấy dữ liệu từ bảng cosogietmo
                DataTable cosogietmoData = database.GetData("hochannuoi");

                // Hiển thị dữ liệu trên DataGridView
                GridViewAccounts.DataSource = cosogietmoData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing cosogietmo data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int trangthai = Convert.ToInt32(textBox6.Text);
            DataTable userData;

            // Kiểm tra nếu cả hai trường đều rỗng
            if (String.IsNullOrWhiteSpace(trangthai.ToString() ))
            {
                // Trường hợp cả hai trường đều rỗng, lấy toàn bộ dữ liệu từ bảng
                userData = database.GetHoVungData();
            }
            else
            {
                // Trường hợp một trong hai trường có giá trị hoặc cả hai trường đều có giá trị
                userData = database.GetHoData(trangthai);
            }

            // Hiển thị dữ liệu trong DataGridView
            GridViewAccounts.DataSource = userData;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string email = GridViewAccounts.Rows[selectedIndex].Cells["email"].Value.ToString();

                string name = GridViewAccounts.Rows[selectedIndex].Cells["ten"].Value.ToString();
                string dieukienchannuoi = GridViewAccounts.Rows[selectedIndex].Cells["dieukienchannuoi"].Value.ToString();

                string idvungchannuoi = GridViewAccounts.Rows[selectedIndex].Cells["idvungchannuoi"].Value.ToString();
                string phone = GridViewAccounts.Rows[selectedIndex].Cells["phone"].Value.ToString();
                textBox2.Text = email;
                textBox3.Text = dieukienchannuoi;
                textBox1.Text = name;
                textBox4.Text = phone;

                textBox7.Text = idvungchannuoi;
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
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
                bool deleteSuccess = database.DeleteData("hochannuoi", Convert.ToInt32(primaryKeyValue));

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    ShowCosogietmoData(); // Gọi lại hàm tải dữ liệu
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
            string ten = textBox1.Text;
            string dieukienchannuoi = textBox3.Text;
            string email = textBox2.Text;
            int phone = Convert.ToInt32(textBox4.Text);

            int idvungchannuoi = Convert.ToInt32(textBox7.Text);

            // Thực hiện thêm dữ liệu và kiểm tra kết quả
            bool insertSuccess = database.InsertData("hochannuoi", new Dictionary<string, object>
        {
            { "ten", ten },
            { "dieukienchannuoi", dieukienchannuoi },
            { "email", email },
            { "phone", phone },
            { "idvungchannuoi", idvungchannuoi }
        });

            if (insertSuccess)
            {
                // Cập nhật các TextBox trên giao diện với dữ liệu mới
                textBox2.Text = email;
                textBox3.Text = dieukienchannuoi;
                textBox1.Text = ten;
                textBox4.Text = phone.ToString();

                textBox7.Text = idvungchannuoi.ToString();

                // Cập nhật DataGridView để hiển thị dữ liệu mới
                ShowCosogietmoData();

                MessageBox.Show("Data inserted successfully.");
            }
            else
            {
                MessageBox.Show("Insert failed.");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();

                string ten = textBox1.Text;
                string dieukienchannuoi = textBox3.Text;
                string email = textBox2.Text;
                int phone = Convert.ToInt32(textBox4.Text);

                int idvungchannuoi = Convert.ToInt32(textBox7.Text);

                // Thực hiện sửa dữ liệu và kiểm tra kết quả
                bool updateSuccess = database.UpdateData("hochannuoi", Convert.ToInt32(primaryKeyValue), new Dictionary<string, object>
            {
                { "ten", ten },
                { "dieukienchannuoi", dieukienchannuoi },
                { "email", email },
                { "phone", phone },
                { "idvungchannuoi", idvungchannuoi }
            });

                if (updateSuccess)
                {
                    // Cập nhật các TextBox trên giao diện với dữ liệu mới
                    textBox2.Text = email;
                    textBox3.Text = dieukienchannuoi;
                    textBox1.Text = ten;
                    textBox4.Text = phone.ToString();

                    textBox7.Text = idvungchannuoi.ToString();

                    // Cập nhật DataGridView để hiển thị dữ liệu mới
                    ShowCosogietmoData();

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
            ad_manage_ho form1 = new ad_manage_ho();
            form1.Show();
            this.Hide();
        }
    }
}
