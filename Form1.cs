using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;


namespace NhaCungCap
{
    public partial class frmNhaCungCap : Form
    {
        //Khai báo kết nối đến sql
        SqlConnection kn = new SqlConnection(@"Data Source=.;Initial Catalog=FashionShopDb3;Integrated Security=True;TrustServerCertificate=True");
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        bool them = false;
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void ThucHienDataBinding()
        {
            try
            {
                adapter = new SqlDataAdapter("SELECT * FROM BRAND", kn);
                ds.Clear();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu nhà cung cấp!");
                    return;
                }

                bs.DataSource = ds.Tables[0];

                // XÓA HẾT binding cũ
                txtMaNCC.DataBindings.Clear();
                txtTenNCC.DataBindings.Clear();
                txtSĐT.DataBindings.Clear();
                txtDiaChi.DataBindings.Clear();
                txtXuatXu.DataBindings.Clear();
                txtEmail.DataBindings.Clear();

                // BIND ĐÚNG TÊN CỘT TRONG SQL
                txtMaNCC.DataBindings.Add("Text", bs, "Brand_Id", true);
                txtTenNCC.DataBindings.Add("Text", bs, "Name", true);
                txtSĐT.DataBindings.Add("Text", bs, "Phone", true);
                txtDiaChi.DataBindings.Add("Text", bs, "Address", true);
                txtXuatXu.DataBindings.Add("Text", bs, "CountryOfOrigin", true);
                txtEmail.DataBindings.Add("Text", bs, "Email", true);

                bindingNavigator1.BindingSource = bs;
                dataGridViewNCC.DataSource = bs;

                // Tự động điều chỉnh cột
                dataGridViewNCC.AutoResizeColumns();
                dataGridViewNCC.Columns["Brand_Id"].HeaderText = "Mã NCC";
                dataGridViewNCC.Columns["Name"].HeaderText = "Tên NCC";
                dataGridViewNCC.Columns["Phone"].HeaderText = "Số điện thoại";
                dataGridViewNCC.Columns["Address"].HeaderText = "Địa chỉ";
                dataGridViewNCC.Columns["CountryOfOrigin"].HeaderText = "Xuất xứ";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load dữ liệu: " + ex.Message);
            }
        }
        private void KhoaNutLenh(bool Khoa)
        {
            bindingNavigatorAddNewItem.Enabled = Khoa;
            btnLuu.Enabled = !Khoa;
            btnHuybo.Enabled = !Khoa;
            bindingNavigatorDeleteItem.Enabled = Khoa;

        }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ThucHienDataBinding();
            
            // Tắt hành vi xóa mặc định của BindingNavigator
            this.bindingNavigator1.DeleteItem = null;

            // Đảm bảo chỉ gắn 1 lần
            this.bindingNavigatorDeleteItem.Click -= bindingNavigatorDeleteItem_Click;
            this.bindingNavigatorDeleteItem.Click += bindingNavigatorDeleteItem_Click;
        
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            KhoaNutLenh(false);
            them = true;
        }

        private void btnHuybo_Click(object sender, EventArgs e)
        {
            bs.CancelEdit();
            KhoaNutLenh(true);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            KhoaNutLenh(true);
            if (txtMaNCC.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập mã nhà cung cấp", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaNCC.Focus();
                return;
            }

            if (txtTenNCC.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập tên nhà cung cấp", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNCC.Focus();
                return;
            }

            if (txtSĐT.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập số điện thoại nhà cung cấp", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSĐT.Focus();
                return;
            }
            if (txtDiaChi.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập địa chỉ nhà cung cấp", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChi.Focus();
                return;
            }
            if (txtXuatXu.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập xuất xứ nhà cung cấp", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtXuatXu.Focus();
                return;
            }
            if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Hãy nhập email nhà cung cấp", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            string sql;
            if (them == true)
            {
                //if (Kiemtratrungmavaten() == true)
                //{
                //    MessageBox.Show("Mã hoặc tên nhà cung cấp bị trùng", "Chú ý", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                SqlCommand cmd = new SqlCommand("AddNewBrand", kn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Brand_Id", txtMaNCC.Text);
                cmd.Parameters.AddWithValue("@Name", txtTenNCC.Text);
                cmd.Parameters.AddWithValue("@Phone", txtSĐT.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@CountryOfOrigin", txtXuatXu.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                try
                {
                    kn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lưu nhà cung cấp thành công!");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    kn.Close();
                }
            }
            else
            {
                SqlCommand cmd = new SqlCommand("UpdateBrandWithTransaction", kn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Brand_Id", txtMaNCC.Text);
                cmd.Parameters.AddWithValue("@Name", txtTenNCC.Text);
                cmd.Parameters.AddWithValue("@Phone", txtSĐT.Text);
                cmd.Parameters.AddWithValue("@Address", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@CountryOfOrigin", txtXuatXu.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);

                try
                {
                    if (kn.State == ConnectionState.Closed) kn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật nhà cung cấp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Form1_Load(sender, e);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    kn.Close();
                }
            }


           

            KhoaNutLenh(true);



        }
        private bool Kiemtratrungmavaten()
        {
            string sql = "SELECT * FROM Brand WHERE Brand_Id = '" + txtMaNCC.Text + "' OR Name =N'" + txtTenNCC.Text + "'";
            adapter = new SqlDataAdapter(sql, kn);
            DataTable dtbKT = new DataTable();
            adapter.Fill(dtbKT);
            if (dtbKT.Rows.Count > 0) return true;
            return false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            KhoaNutLenh(false);
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (bs.Current == null)
            {
                MessageBox.Show("Vui lòng chọn một thương hiệu để xóa!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataRowView drv = (DataRowView)bs.Current;
            int brandId = Convert.ToInt32(drv["Brand_Id"]);
            string tenBrand = drv["Name"]?.ToString() ?? "Không xác định";

           
            if (MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa thương hiệu:\n\n" +
                $"   Tên: {tenBrand}\n" +
                $"   Mã: {brandId}\n\n" +
                $"⚠️ Thao tác này không thể hoàn tác!",
                "XÁC NHẬN XÓA THƯƠNG HIỆU",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand("DeleteBrand", kn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Brand_Id", brandId);

                    kn.Open();
                    cmd.ExecuteNonQuery(); 
                }

                bs.RemoveCurrent();

                MessageBox.Show($"Đã xóa thành công thương hiệu:\n{tenBrand}", "Thành công",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                // Bắt lỗi từ RAISERROR trong Stored Procedure
                string msg = ex.Message.Trim();

                if (ex.Number == 50000 || msg.Contains("không thể xóa") || msg.Contains("sản phẩm"))
                {
                    MessageBox.Show("Không thể xóa thương hiệu này!\n\n" +
                                    "Lý do: " + msg,
                                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else if (msg.Contains("không tìm thấy"))
                {
                    MessageBox.Show("Không tìm thấy thương hiệu để xóa!", "Thông báo",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bs.RemoveCurrent(); 
                }
                else
                {
                    MessageBox.Show("Lỗi cơ sở dữ liệu:\n" + ex.Message, "Lỗi nghiêm trọng",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định:\n" + ex.Message, "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (kn.State == ConnectionState.Open)
                    kn.Close();
            }
        }
        private void TimKiemNhaCungCap()
        {
            try
            {
                // Nếu chưa có dữ liệu thì bỏ qua
                if (bs == null || bs.DataSource == null) return;

                string keyword = txtTimKiem.Text.Trim();

                if (string.IsNullOrEmpty(keyword))
                {
                    bs.Filter = null;
                    lblTongNCC.Text = $"Tổng: {bs.Count} nhà cung cấp";
                    return;
                }

                keyword = keyword.ToLower();
                DataTable dt = (bs.DataSource as DataTable);
                if (dt == null) return;

                // Tạo điều kiện lọc linh hoạt trên mọi cột
                StringBuilder filter = new StringBuilder();
                bool first = true;

                foreach (DataColumn col in dt.Columns)
                {
                    // Chỉ tìm trên các cột chữ, số, email...
                    if (col.DataType == typeof(string) ||
                        col.DataType == typeof(int) ||
                        col.DataType == typeof(decimal))
                    {
                        if (!first) filter.Append(" OR ");
                        filter.Append($"Convert({col.ColumnName}, 'System.String') LIKE '%{keyword}%'");
                        first = false;
                    }
                }

                if (first) return; // Không có cột nào để tìm

                bs.Filter = filter.ToString();
                lblTongNCC.Text = $"Tìm thấy: {bs.Count} nhà cung cấp";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            TimKiemNhaCungCap();
            txtTimKiem.Focus();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemNhaCungCap();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            TimKiemNhaCungCap();
        }
    }
}

