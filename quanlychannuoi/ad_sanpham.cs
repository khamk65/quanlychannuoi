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
    public partial class ad_sanpham : Form
    {
        private Database database;
        public ad_sanpham()
        {
            InitializeComponent();
            database = new Database();
            ShowsanphamData();
            ShowsanphamthamchieuData();
        }
        private void ShowsanphamData()
        {
            try
            {
                // Lấy dữ liệu từ bảng cososanxuat
                DataTable cososanxuatData = database.GetsanphamData();

                // Hiển thị dữ liệu trên DataGridView
                GridViewAccounts.DataSource = cososanxuatData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }
        private void ShowsanphamthamchieuData()
        {
            try
            {
                // Lấy dữ liệu từ bảng cososanxuat
                DataTable cososanxuatData = database.GetProductData();

                // Hiển thị dữ liệu trên DataGridView
                dataGridView1.DataSource = cososanxuatData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }

        private void ad_sanpham_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ad_manage_sanpham form1 = new ad_manage_sanpham();
            form1.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string name = GridViewAccounts.Rows[selectedIndex].Cells["ten"].Value.ToString();
                string idsanxuat = GridViewAccounts.Rows[selectedIndex].Cells["idsanxuat"].Value.ToString();
                string idkhaonghiem = GridViewAccounts.Rows[selectedIndex].Cells["idkhaonghiem"].Value.ToString();
                textBox2.Text = idsanxuat;
                textBox1.Text = name;
                textBox3.Text = idkhaonghiem;
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ten = textBox1.Text;
            int idkhaonghiem = Convert.ToInt32(textBox3.Text);
            int idsanxuat = Convert.ToInt32(textBox2.Text);

            // Thực hiện thêm dữ liệu và kiểm tra kết quả
            bool insertSuccess = database.Insertsanpham(ten, idsanxuat, idkhaonghiem);

            if (insertSuccess)
            {
                // Cập nhật các TextBox trên giao diện với dữ liệu mới
                textBox2.Text = idsanxuat.ToString();
                textBox3.Text = idkhaonghiem.ToString();
                textBox1.Text = ten; // Giả sử 'ten' là tên biến tương ứng với tên của cơ sở sản xuất


                // Cập nhật DataGridView để hiển thị dữ liệu mới
                ShowsanphamData();
                ShowsanphamthamchieuData();
                MessageBox.Show("Data inserted successfully.");
            }
            else
            {
                MessageBox.Show("Insert failed.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
               
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;

                // Lấy giá trị của cột khóa chính (nếu có)
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString();

                // Thực hiện xóa dữ liệu từ cơ sở dữ liệu
                bool deleteSuccess = database.Deletesanpham(Convert.ToInt32(primaryKeyValue));

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    ShowsanphamData();
                    ShowsanphamthamchieuData();//Gọi lại hàm tải dữ liệu
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
                string primaryKeyValue = GridViewAccounts.Rows[selectedIndex].Cells["id"].Value.ToString(); // Giả sử textBoxID là TextBox chứa ID cần sửa
                string ten = textBox1.Text;
                int idkhaonghiem = Convert.ToInt32(textBox3.Text);
                int idsanxuat = Convert.ToInt32(textBox2.Text);


                // Thực hiện sửa dữ liệu và kiểm tra kết quả
                bool updateSuccess = database.Updatesanpham(Convert.ToInt32(primaryKeyValue), ten, idsanxuat,idkhaonghiem);

                if (updateSuccess)
                {
                    // Cập nhật các TextBox trên giao diện với dữ liệu mới
                    textBox2.Text = idsanxuat.ToString();
                    textBox3.Text = idkhaonghiem.ToString();
                    textBox1.Text = ten;


                    // Cập nhật DataGridView để hiển thị dữ liệu mới
                    ShowsanphamData();
                    ShowsanphamthamchieuData();
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
    }
}
