using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;

namespace quanlychannuoi
{
    public class Database
    {
        private string connectionSTR = "Data Source=Stoner;Initial Catalog=quanlychannuoi;Integrated Security=True";
        private SqlConnection con;
        private string sql;
        private DataTable dt;
        private SqlCommand cmd;

        public Database()
        {
            con = new SqlConnection(connectionSTR);
            try
            {
                con.Open();
            }
            catch (Exception e)
            {
                MessageBox.Show("Connection failed: " + e.Message);
            }
        }

        public DataTable GetUserData(string email, string name)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    string sql = "SELECT * FROM taikhoan";

                    // Kiểm tra các trường hợp của tham số email và name
                    if (email != null && name != null)
                    {
                        sql += " WHERE gmail = @Email AND ten = @Name";
                    }
                    else if (email == null)
                    {
                        sql += " WHERE ten = @Name";
                    }
                    else if (name == null)
                    {
                        sql += " WHERE gmail = @Email";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số và gán giá trị từ biến
                        if (email != null)
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                        }
                        if (name != null)
                        {
                            cmd.Parameters.AddWithValue("@Name", name);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user data: " + e.Message);
                return null;
            }
        }

        public DataTable GetHoData(int trangthai)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    string sql = @"
                SELECT hochannuoi.id, hochannuoi.ten, hochannuoi.email, hochannuoi.phone, hochannuoi.dieukienchannuoi, vungchannuoi.trangthai
                FROM hochannuoi
                JOIN vungchannuoi ON hochannuoi.idvungchannuoi = vungchannuoi.id";

                    // Kiểm tra trường hợp của tham số trangthai
                    if (trangthai != null)
                    {
                        sql += " WHERE vungchannuoi.trangthai = @TrangThai";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số và gán giá trị từ biến
                        if (trangthai != null)
                        {
                            cmd.Parameters.AddWithValue("@TrangThai", trangthai);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user data: " + e.Message);
                return null;
            }
        }

        public bool AddUser(string gmail, string mk, int role, string ten)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để thêm người dùng vào bảng taikhoan
                    string sql = "INSERT INTO taikhoan (gmail, mk, role, ten) VALUES (@Gmail, @MK, @Role, @Ten)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm các tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Gmail", gmail);
                        cmd.Parameters.AddWithValue("@MK", mk);
                        cmd.Parameters.AddWithValue("@Role", role);
                        cmd.Parameters.AddWithValue("@Ten", ten);

                        // Thực hiện truy vấn
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error adding user: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool UpdateUser(int id, string newGmail, string newMK, string newTen)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để cập nhật người dùng trong bảng taikhoan
                    string sql = "UPDATE taikhoan SET gmail = @NewGmail, mk = @NewMK, ten = @NewTen WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm các tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@NewGmail", newGmail);
                        cmd.Parameters.AddWithValue("@NewMK", newMK);

                        cmd.Parameters.AddWithValue("@NewTen", newTen);
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Thực hiện truy vấn
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating user: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool DeleteUser(int userId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL DELETE
                    string sql = "DELETE FROM taikhoan WHERE id = @UserID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        // Thực hiện câu lệnh SQL DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting user: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool DeleteCososanxuat(int cososanxuatId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL DELETE cho bảng cososanxuat
                    string sql = "DELETE FROM cososanxuat WHERE id = @CososanxuatID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@CososanxuatID", cososanxuatId);

                        // Thực hiện câu lệnh SQL DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting cososanxuat: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool Deletecosochebiensanphamchannuoi(int cososanxuatId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL DELETE cho bảng cososanxuat
                    string sql = "DELETE FROM cosochebiensanphamchannuoi WHERE id = @CososanxuatID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@CososanxuatID", cososanxuatId);

                        // Thực hiện câu lệnh SQL DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting cososanxuat: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool DeleteCosokhaonghiem(int cososanxuatId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL DELETE cho bảng cososanxuat
                    string sql = "DELETE FROM cosokhaonghiem WHERE id = @CososanxuatID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@CososanxuatID", cososanxuatId);

                        // Thực hiện câu lệnh SQL DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting cososanxuat: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool Deletetochucchungnhan(int cososanxuatId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL DELETE cho bảng cososanxuat
                    string sql = "DELETE FROM tochucchungnhan WHERE id = @CososanxuatID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@CososanxuatID", cososanxuatId);

                        // Thực hiện câu lệnh SQL DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting cososanxuat: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool Deletechicucthuy(int cososanxuatId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL DELETE cho bảng cososanxuat
                    string sql = "DELETE FROM chicucthuy WHERE id = @CososanxuatID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@CososanxuatID", cososanxuatId);

                        // Thực hiện câu lệnh SQL DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting cososanxuat: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool Deletesanpham(int cososanxuatId)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL DELETE cho bảng cososanxuat
                    string sql = "DELETE FROM sanpham WHERE id = @CososanxuatID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@CososanxuatID", cososanxuatId);

                        // Thực hiện câu lệnh SQL DELETE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào bị ảnh hưởng hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error deleting cososanxuat: " + e.Message);
                // Thực hiện xử lý lỗi nếu cần thiết
                return false;
            }
        }
        public bool UpdateCososanxuat(int id, string ten, string nguoilienhe, string email, int phone, int quymo, string diachi)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL UPDATE
                    string sql = "UPDATE cososanxuat SET ten = @Ten, nguoilienhe = @Nguoilienhe, email = @Email, phone = @Phone, quymo = @Quymo, diachi = @Diachi WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Quymo", quymo);
                        cmd.Parameters.AddWithValue("@Diachi", diachi);
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Thực hiện câu lệnh SQL UPDATE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được sửa hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Updatecosochebiensanphamchannuoi(int id, string ten, string nguoilienhe, string email, int phone, int congsuat)        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL UPDATE
                    string sql = "UPDATE cosochebiensanphamchannuoi SET ten = @Ten, nguoilienhe = @Nguoilienhe, email = @Email, phone = @Phone,congsuat = @Quymo WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Quymo", congsuat);
                       
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Thực hiện câu lệnh SQL UPDATE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được sửa hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool UpdateCosokhaonghiem(int id, string ten, string nguoilienhe, string email, int phone, string diachi)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL UPDATE
                    string sql = "UPDATE cosokhaonghiem SET ten = @Ten, nguoilienhe = @Nguoilienhe, email = @Email, phone = @Phone, diachi = @Diachi WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        cmd.Parameters.AddWithValue("@Diachi", diachi);
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Thực hiện câu lệnh SQL UPDATE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được sửa hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Updatetochucchungnhan(int id, string ten, string nguoilienhe, string email, int phone)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL UPDATE
                    string sql = "UPDATE tochucchungnhan SET ten = @Ten, nguoilienhe = @Nguoilienhe, email = @Email, phone = @Phone WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Thực hiện câu lệnh SQL UPDATE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được sửa hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Updatechicucthuy(int id, string ten, string nguoilienhe, string email, int phone, string diachi)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL UPDATE
                    string sql = "UPDATE chicucthuy SET ten = @Ten, nguoilienhe = @Nguoilienhe, email = @Email, phone = @Phone, diachi = @Diachi WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        cmd.Parameters.AddWithValue("@Diachi", diachi);
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Thực hiện câu lệnh SQL UPDATE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được sửa hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Updatesanpham(int id, string ten, int idsanxuat, int idkhaonghiem)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL UPDATE
                    string sql = "UPDATE sanpham SET ten = @Ten, idsanxuat = @Nguoilienhe, idkhaonghiem = @Email WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", idsanxuat);
                        cmd.Parameters.AddWithValue("@Email", idkhaonghiem);
                        cmd.Parameters.AddWithValue("@ID", id);

                        // Thực hiện câu lệnh SQL UPDATE
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được sửa hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool InsertCososanxuat(string ten, string nguoilienhe, string email, int phone, int quymo, string diachi)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL INSERT
                    string sql = "INSERT INTO cososanxuat (ten, nguoilienhe, email, phone, quymo, diachi) VALUES (@Ten, @Nguoilienhe, @Email, @Phone, @Quymo, @Diachi)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Quymo", quymo);
                        cmd.Parameters.AddWithValue("@Diachi", diachi);

                        // Thực hiện câu lệnh SQL INSERT
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được thêm hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error inserting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Insertcosochebiensanphamchannuoi(string ten, string nguoilienhe, string email, int phone, int congsuat)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL INSERT
                    string sql = "INSERT INTO cosochebiensanphamchannuoi (ten, nguoilienhe, email, phone, congsuat) VALUES (@Ten, @Nguoilienhe, @Email, @Phone, @Quymo)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Quymo", congsuat);
                        

                        // Thực hiện câu lệnh SQL INSERT
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được thêm hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error inserting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool InsertCosokhaonghiem(string ten, string nguoilienhe, string email, int phone, string diachi)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL INSERT
                    string sql = "INSERT INTO cosokhaonghiem (ten, nguoilienhe, email, phone, diachi) VALUES (@Ten, @Nguoilienhe, @Email, @Phone, @Diachi)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        cmd.Parameters.AddWithValue("@Diachi", diachi);

                        // Thực hiện câu lệnh SQL INSERT
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được thêm hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error inserting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Inserttochucchungnhan(string ten, string nguoilienhe, string email, int phone)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL INSERT
                    string sql = "INSERT INTO tochucchungnhan (ten, nguoilienhe, email, phone) VALUES (@Ten, @Nguoilienhe, @Email, @Phone)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                       

                        // Thực hiện câu lệnh SQL INSERT
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được thêm hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error inserting tochucchungnhan data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Insertchicucthuy(string ten, string nguoilienhe, string email, int phone, string diachi)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL INSERT
                    string sql = "INSERT INTO chicucthuy (ten, nguoilienhe, email, phone, diachi) VALUES (@Ten, @Nguoilienhe, @Email, @Phone, @Diachi)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", nguoilienhe);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Phone", phone);

                        cmd.Parameters.AddWithValue("@Diachi", diachi);

                        // Thực hiện câu lệnh SQL INSERT
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được thêm hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error inserting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public bool Insertsanpham(string ten, int idsanxuat, int idkhaonghiem)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL INSERT
                    string sql = "INSERT INTO sanpham (ten, idsanxuat, idkhaonghiem) VALUES (@Ten, @Nguoilienhe, @Email)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Thêm tham số để tránh SQL Injection
                        cmd.Parameters.AddWithValue("@Ten", ten);
                        cmd.Parameters.AddWithValue("@Nguoilienhe", idsanxuat);
                        cmd.Parameters.AddWithValue("@Email", idkhaonghiem);


                        // Thực hiện câu lệnh SQL INSERT
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra xem có bản ghi nào được thêm hay không
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error inserting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về false
                return false;
            }
        }
        public DataTable GetCososanxuatData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL SELECT để lấy dữ liệu từ bảng cososanxuat
                    string sql = "SELECT * FROM cososanxuat";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Trả về DataTable chứa dữ liệu
                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về DataTable trống
                return new DataTable();
            }
        }
        public DataTable Getcosochebiensanphamchannuoi()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL SELECT để lấy dữ liệu từ bảng cososanxuat
                    string sql = "SELECT * FROM cosochebiensanphamchannuoi";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Trả về DataTable chứa dữ liệu
                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về DataTable trống
                return new DataTable();
            }
        }
        public DataTable GetCosokhaonghiemData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL SELECT để lấy dữ liệu từ bảng cososanxuat
                    string sql = "SELECT * FROM cosokhaonghiem";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Trả về DataTable chứa dữ liệu
                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về DataTable trống
                return new DataTable();
            }
        }
        public DataTable GettochucchungnhanData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL SELECT để lấy dữ liệu từ bảng cososanxuat
                    string sql = "SELECT * FROM tochucchungnhan";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Trả về DataTable chứa dữ liệu
                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về DataTable trống
                return new DataTable();
            }
        }
        public DataTable GetchicucthuyData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL SELECT để lấy dữ liệu từ bảng cososanxuat
                    string sql = "SELECT * FROM chicucthuy";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Trả về DataTable chứa dữ liệu
                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về DataTable trống
                return new DataTable();
            }
        }
        public DataTable GetsanphamData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện câu lệnh SQL SELECT để lấy dữ liệu từ bảng cososanxuat
                    string sql = "SELECT * FROM sanpham";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng SqlDataAdapter để lấy dữ liệu và đổ vào DataTable
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Trả về DataTable chứa dữ liệu
                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting cososanxuat data: " + e.Message);
                // Xử lý lỗi nếu cần thiết và trả về DataTable trống
                return new DataTable();
            }
        }
        public DataTable GetUser()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy dữ liệu người dùng từ bảng taikhoan
                    string sql = "SELECT * FROM taikhoan";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user data: " + e.Message);
                // Trả về null hoặc thực hiện xử lý lỗi phù hợp với yêu cầu của bạn
                return null;
            }
        }
        public DataTable GetProductData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy dữ liệu từ ba bảng: sanpham, cososanxuat, cosokhaonghiem
                    string sql = "SELECT sanpham.id AS ProductID, sanpham.ten AS ProductName, " +
                                 "cososanxuat.ten AS ManufacturerName, cosokhaonghiem.ten AS TestingFacilityName " +
                                 "FROM sanpham " +
                                 "INNER JOIN cososanxuat ON sanpham.idsanxuat = cososanxuat.id " +
                                 "INNER JOIN cosokhaonghiem ON sanpham.idkhaonghiem = cosokhaonghiem.id";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting product data: " + e.Message);
                // Trả về null hoặc thực hiện xử lý lỗi phù hợp với yêu cầu của bạn
                return null;
            }
        }
        public DataTable ShowData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Câu truy vấn SQL với INNER JOIN giữa các bảng
                    string sql = @"
                SELECT chungnhanchannuoi.id, chungnhanchannuoi.ten AS ChungNhanTen,
                       hochannuoi.ten AS HoChannuoiTen,
                       tochucchungnhan.ten AS ToChucChungNhanTen
                FROM chungnhanchannuoi
                JOIN hochannuoi ON chungnhanchannuoi.idhochannuoi = hochannuoi.id
                JOIN tochucchungnhan ON chungnhanchannuoi.idchungnhan = tochucchungnhan.id
            ";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable; // Trả về DataTable đã được điền dữ liệu
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error showing data: " + e.Message);
                // Xử lý lỗi nếu cần thiết
                return null;
            }
        }

        public DataTable GetHoVungData()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy dữ liệu từ ba bảng: sanpham, cososanxuat, cosokhaonghiem
                    string sql = @"
    SELECT hochannuoi.id, hochannuoi.ten, hochannuoi.email, hochannuoi.phone, hochannuoi.dieukienchannuoi, vungchannuoi.trangthai
    FROM hochannuoi
    JOIN vungchannuoi ON hochannuoi.idvungchannuoi = vungchannuoi.id;
";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting product data: " + e.Message);
                // Trả về null hoặc thực hiện xử lý lỗi phù hợp với yêu cầu của bạn
                return null;
            }
        }
        public int GetCountUsers()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy tổng số người dùng từ bảng taikhoan
                    string sql = "SELECT COUNT(*) FROM taikhoan";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng ExecuteScalar để lấy giá trị số lượng
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user count: " + e.Message);
                return -1; // Trả về giá trị âm để chỉ ra lỗi, bạn có thể thay đổi cách xử lý lỗi nếu cần thiết
            }
        }
        public int GetCountvung()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy tổng số người dùng từ bảng taikhoan
                    string sql = "SELECT COUNT(*) FROM vungchannuoi";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng ExecuteScalar để lấy giá trị số lượng
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user count: " + e.Message);
                return -1; // Trả về giá trị âm để chỉ ra lỗi, bạn có thể thay đổi cách xử lý lỗi nếu cần thiết
            }
        }
        public int GetCountAdmin()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy tổng số người dùng từ bảng taikhoan
                    string sql = "SELECT COUNT(*) FROM taikhoan where role=1";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng ExecuteScalar để lấy giá trị số lượng
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user count: " + e.Message);
                return -1; // Trả về giá trị âm để chỉ ra lỗi, bạn có thể thay đổi cách xử lý lỗi nếu cần thiết
            }
        }
        public int GetCountnhanvien()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    // Thực hiện truy vấn để lấy tổng số người dùng từ bảng taikhoan
                    string sql = "SELECT COUNT(*) FROM taikhoan where role=2";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        // Sử dụng ExecuteScalar để lấy giá trị số lượng
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user count: " + e.Message);
                return -1; // Trả về giá trị âm để chỉ ra lỗi, bạn có thể thay đổi cách xử lý lỗi nếu cần thiết
            }
        }
        public bool AuthenticateUser(string username, string password)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Thực hiện truy vấn để kiểm tra đăng nhập
                sql = "SELECT COUNT(*) FROM taikhoan WHERE gmail = @Username AND mk = @Password";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Password", password);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error authenticating user: " + e.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public string GetUserRole(string username)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Thực hiện truy vấn để lấy vai trò của người dùng
                sql = "SELECT role FROM taikhoan WHERE gmail = @Username";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Username", username);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user role: " + e.Message);
                return string.Empty;
            }
            finally
            {
                con.Close();
            }
        }
        public string GetUserName(string username)
        {
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }

                // Thực hiện truy vấn để lấy vai trò của người dùng
                sql = "SELECT ten FROM taikhoan WHERE gmail = @Username";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Username", username);

                object result = cmd.ExecuteScalar();
                return result != null ? result.ToString() : string.Empty;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error getting user role: " + e.Message);
                return string.Empty;
            }
            finally
            {
                con.Close();
            }
        }


        // thuy
        public DataTable GetData(string tableName)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    string sql = $"SELECT * FROM {tableName}";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        return dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error getting data from {tableName}: {e.Message}");
                return null;
            }
        }

        public bool DeleteData(string tableName, int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    string sql = $"DELETE FROM {tableName} WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error deleting data from {tableName}: {e.Message}");
                return false;
            }
        }

        public bool InsertData(string tableName, Dictionary<string, object> data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    string columns = string.Join(", ", data.Keys);
                    string values = string.Join(", ", data.Keys.Select(key => $"@{key}"));

                    string sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        foreach (var entry in data)
                        {
                            cmd.Parameters.AddWithValue($"@{entry.Key}", entry.Value);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error inserting data into {tableName}: {e.Message}");
                return false;
            }
        }

        public bool UpdateData(string tableName, int id, Dictionary<string, object> data)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionSTR))
                {
                    con.Open();

                    string setClause = string.Join(", ", data.Keys.Select(key => $"{key} = @{key}"));

                    string sql = $"UPDATE {tableName} SET {setClause} WHERE id = @ID";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@ID", id);

                        foreach (var entry in data)
                        {
                            cmd.Parameters.AddWithValue($"@{entry.Key}", entry.Value);
                        }

                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Error updating data in {tableName}: {e.Message}");
                return false;
       
    }
}
    }
}
