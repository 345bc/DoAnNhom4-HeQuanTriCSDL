using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text;

            if (string.IsNullOrEmpty(user))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUser.Focus();
                return;
            }

            if (string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPass.Focus();
                return;
            }

            string connStr = @"Data Source=.;Initial Catalog=HQTCSDL1;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT Name, Email, Role 
                FROM Users 
                WHERE (Email = @user OR Name = @user) 
                  AND Password = HASHBYTES('SHA2_256', @pass)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", user);
                        cmd.Parameters.AddWithValue("@pass", pass);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {


                                // Đăng nhập thành công
                                Tendangnhap = reader["Name"].ToString();
                                Vaitro = reader["Role"].ToString(); // Admin / Customer / Employee

                                MessageBox.Show($"Đăng nhập thành công!\nChào mừng {Vaitro} {Tendangnhap}",
                                    "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                this.Hide();
                                frmMain main = new frmMain();
                                main.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!",
                                    "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                txtPass.SelectAll();
                                txtPass.Focus();
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

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
