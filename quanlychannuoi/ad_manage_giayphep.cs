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
    public partial class ad_manage_giayphep : Form
    {
        private Database database;

        public ad_manage_giayphep()
        {
            InitializeComponent();
            database = new Database();
            ShowChungNhanChannuoiData();
            ShowChungNhanChannuoithamchieuData();
        }

        private void ShowChungNhanChannuoiData()
        {
            try
            {
                // Lấy dữ liệu từ bảng chungnhanchannuoi
                DataTable chungNhanChannuoiData = database.GetData("chungnhanchannuoi");

                // Hiển thị dữ liệu trên DataGridView
                GridViewAccounts.DataSource = chungNhanChannuoiData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing chungnhanchannuoi data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }
        private void ShowChungNhanChannuoithamchieuData()
        {
            try
            {
                // Lấy dữ liệu từ bảng chungnhanchannuoi
                DataTable chungNhanChannuoiData = database.ShowData();

                // Hiển thị dữ liệu trên DataGridView
                dataGridView1.DataSource = chungNhanChannuoiData;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing chungnhanchannuoi data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                string ten = GridViewAccounts.Rows[selectedIndex].Cells["ten"].Value.ToString();
                int idHocChannuoi = Convert.ToInt32(GridViewAccounts.Rows[selectedIndex].Cells["idhochannuoi"].Value);
                int idChungNhan = Convert.ToInt32(GridViewAccounts.Rows[selectedIndex].Cells["idchungnhan"].Value);

                // Thực hiện các hành động cần thiết với dữ liệu
                // Ví dụ: Hiển thị thông tin trong các TextBox hoặc thực hiện các xử lý khác
                textBox1.Text = ten;
                textBox2.Text = idHocChannuoi.ToString();
                textBox3.Text = idChungNhan.ToString();
            }
            else
            {
                MessageBox.Show("No row selected.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Thực hiện các hành động khi nhấn nút "Xóa"
            if (GridViewAccounts.SelectedRows.Count > 0)
            {
                int selectedIndex = GridViewAccounts.SelectedRows[0].Index;
                int primaryKeyValue = Convert.ToInt32(GridViewAccounts.Rows[selectedIndex].Cells["id"].Value);

                // Thực hiện xóa dữ liệu từ cơ sở dữ liệu
                bool deleteSuccess = database.DeleteData("chungnhanchannuoi", primaryKeyValue);

                if (deleteSuccess)
                {
                    // Refresh DataGridView để hiển thị dữ liệu mới
                    ShowChungNhanChannuoiData();
                    ShowChungNhanChannuoithamchieuData();
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
            // Thực hiện các hành động khi nhấn nút "Cập nhật"
            string ten = textBox1.Text;
            int idHocChannuoi = Convert.ToInt32(textBox2.Text);
            int idChungNhan = Convert.ToInt32(textBox3.Text);

            // Thực hiện thêm dữ liệu và kiểm tra kết quả
            bool insertSuccess = database.InsertData("chungnhanchannuoi", new Dictionary<string, object>
            {
                { "ten", ten },
                { "idhochannuoi", idHocChannuoi },
                { "idchungnhan", idChungNhan }
            });

            if (insertSuccess)
            {
                // Cập nhật DataGridView để hiển thị dữ liệu mới
                ShowChungNhanChannuoiData();
                ShowChungNhanChannuoithamchieuData();
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
                int primaryKeyValue = Convert.ToInt32(GridViewAccounts.Rows[selectedIndex].Cells["id"].Value);

                string ten = textBox1.Text;
                int idHocChannuoi = Convert.ToInt32(textBox2.Text);
                int idChungNhan = Convert.ToInt32(textBox3.Text);

                // Thực hiện cập nhật dữ liệu và kiểm tra kết quả
                bool updateSuccess = database.UpdateData("chungnhanchannuoi", primaryKeyValue, new Dictionary<string, object>
                {
                    { "ten", ten },
                    { "idhochannuoi", idHocChannuoi },
                    { "idchungnhan", idChungNhan }
                });

                if (updateSuccess)
                {
                    // Cập nhật DataGridView để hiển thị dữ liệu mới
                    ShowChungNhanChannuoiData();
                    ShowChungNhanChannuoithamchieuData();
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
            // Thực hiện các hành động khi nhấn nút "Thêm"
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ad_manage_ho form1 = new ad_manage_ho();
            form1.Show();
            this.Hide();
        }
    }
}