using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;
using BC = BCrypt.Net.BCrypt;
namespace NhaCungCap
{
    public partial class frmLogin: Form
    {
        public static string Tendangnhap = "";
        public static string Vaitro = "";
        public static int MaRole = 0;

        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
            
            txtUser.Focus();
        }
        

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string userInput = txtUser.Text.Trim();
            string passwordInput = txtPass.Text;

            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập hoặc email!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
                return;
            }

            if (string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }

            // Ẩn chuỗi kết nối thật (nên đưa vào App.config hoặc appsettings.json sau)
            string connStr = @"Data Source=.;Initial Catalog=FashionShopDb;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // Lấy thông tin user theo Name hoặc Email
                    string query = @"
                SELECT Name, Password, Role 
                FROM Users 
                WHERE Name = @user OR Email = @user";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", userInput);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string fullName = reader["Name"].ToString();
                                string hashedPasswordFromDB = reader["Password"].ToString();
                                string role = reader["Role"].ToString();

                                // ĐÚNG: So sánh mật khẩu gốc với hash trong DB
                                bool passwordValid = BCrypt.Net.BCrypt.Verify(passwordInput, hashedPasswordFromDB);

                                if (passwordValid)
                                {
                                    // Đăng nhập thành công
                                    Tendangnhap = fullName;
                                    Vaitro = role;
                                    MaRole = role == "Admin" ? 1 : (role == "Employee" ? 2 : 3);

                                    MessageBox.Show($"Đăng nhập thành công!\nXin chào {role}: {fullName}",
                                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    this.Hide();
                                    frmNhaCungCap main = new frmNhaCungCap();
                                    main.ShowDialog();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("Mật khẩu không đúng!", "Lỗi đăng nhập",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    txtPass.SelectAll();
                                    txtPass.Focus();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Tài khoản không tồn tại!", "Lỗi đăng nhập",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtUser.Focus();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu:\n" + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        

public string HashPassword(string password)
    {
        return BC.HashPassword(password);
    }

    private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
