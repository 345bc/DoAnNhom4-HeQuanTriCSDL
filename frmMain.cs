using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NhaCungCap
{
    public partial class frmMain: Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            lblHoten.Text = frmLogin.Tendangnhap;
            lblVaitro.Text = frmLogin.Vaitro;
            pictureBox16.Click += pictureBox16_Click;
        }
        private Form currentFormChild;

        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null)
                currentFormChild.Close();
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            mainContent.Controls.Add(childForm);
            mainContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void mainContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhaCungCap());
        }

        private void picLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc muốn đăng xuất không?",
        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();

                frmLogin login = new frmLogin();
                login.ShowDialog();

                this.Close();
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {
            if (currentFormChild != null)
            {
                currentFormChild.Close();
                currentFormChild = null;
            }
            this.Text = "Hệ thống quản lý bán quần áo";
        }
    }
}
