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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace quanlychannuoi
{
    public partial class Login : Form
    {
        private Database database;

        public Login()
        {
            InitializeComponent();
           database = new Database();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usermail= email.Text;
            
string password1 = password.Text;
            // Kiểm tra đăng nhập
            if (database.AuthenticateUser(usermail, password1))

            {
                // Lấy vai trò từ cơ sở dữ liệu
                string role = database.GetUserRole(usermail);
                string name = database.GetUserName(usermail);

                User currentUser = new User { gmail = usermail, Role = role , Username = name };

                // Chuyển hướng đến các màn hình tương ứng với vai trò
                switch (role)
                {
                    case "1":
                        // Chuyển đến màn hình 1
                       
                        admin   form1 = new admin(currentUser);
                        form1.Show();
                        break;
                    case "2":
                        // Chuyển đến màn hình 2
                        nhanvien form2 = new nhanvien(currentUser);
                        form2.Show();
                        break;
                    default:
                        MessageBox.Show("Vai trò không hợp lệ.");
                        break;
                }

                // Đóng form đăng nhập
                this.Hide();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công. Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.");
            }
        }
    }
}

// Tạo một lớp User chứa thông tin người dùng
public class User
{
    public string Username { get; set; }
    public string Role { get; set; }
    public string gmail { get; set; }
    // Thêm các thuộc tính khác của người dùng nếu cần
}

