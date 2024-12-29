using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab04_01
{
    public partial class FrmFalculty : Form
    {

        public FrmFalculty()
        {
            InitializeComponent();
            this.dvgKhoa = new System.Windows.Forms.DataGridView();
            this.txtMa = new System.Windows.Forms.TextBox();
            this.txtTen = new System.Windows.Forms.TextBox();
            this.txtTong = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // Cấu hình DataGridView
            this.dvgKhoa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgKhoa.Location = new System.Drawing.Point(12, 12);
            this.dvgKhoa.Name = "dvgKhoa";
            this.dvgKhoa.Size = new System.Drawing.Size(776, 426);
            this.dvgKhoa.TabIndex = 0;
            this.dvgKhoa.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgKhoa_CellContentClick);

            // Cấu hình các TextBox
            this.txtMa.Location = new System.Drawing.Point(12, 450);
            this.txtTen.Location = new System.Drawing.Point(12, 480);
            this.txtTong.Location = new System.Drawing.Point(12, 510);

            // Thêm các điều khiển vào Form
            this.Controls.Add(this.dvgKhoa);
            this.Controls.Add(this.txtMa);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.txtTong);

            this.ResumeLayout(false);
        }


        public class Model2 : DbContext
        {
            public Model2() : base("name=Model2")
            {
            }

            public virtual DbSet<Khoa> Khoas { get; set; }
        }


        public class Khoa
        {
            public string MaKhoa { get; set; }
            public string TenKhoa { get; set; }
            public int TongSoGS { get; set; }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThemSua_Click(object sender, EventArgs e)
        {
            string maKhoa = txtMa.Text;
            string tenKhoa = txtTen.Text;

            if (string.IsNullOrEmpty(maKhoa) || string.IsNullOrEmpty(tenKhoa))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            using (var context = new Model2())
            {
                // Kiểm tra nếu khoa đã tồn tại
                var khoa = context.Khoas.SingleOrDefault(k => k.MaKhoa == maKhoa);

                if (khoa == null)
                {
                    // Thêm mới
                    khoa = new Khoa
                    {
                        MaKhoa = maKhoa,
                        TenKhoa = tenKhoa
                    };
                    context.Khoas.Add(khoa);
                    MessageBox.Show("Thêm khoa thành công!");
                }
                else
                {
                    // Sửa khoa
                    khoa.TenKhoa = tenKhoa;
                    MessageBox.Show("Sửa khoa thành công!");
                }

                context.SaveChanges();
                LoadData();  // Tải lại dữ liệu lên DataGridView
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maKhoa = txtMa.Text;

            if (string.IsNullOrEmpty(maKhoa))
            {
                MessageBox.Show("Vui lòng nhập mã khoa cần xóa.");
                return;
            }

            using (var context = new Model2())
            {
                var khoa = context.Khoas.SingleOrDefault(k => k.MaKhoa == maKhoa);

                if (khoa != null)
                {
                    context.Khoas.Remove(khoa);
                    context.SaveChanges();
                    MessageBox.Show("Xoá khoa thành công!");
                    LoadData();  // Tải lại dữ liệu lên DataGridView
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khoa có mã này.");
                }
            }
        }

        private void dvgKhoa_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dvgKhoa.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ các cột của dòng đã chọn vào các TextBox
                txtMa.Text = row.Cells["MaKhoa"].Value.ToString();
                txtTen.Text = row.Cells["TenKhoa"].Value.ToString();
                txtTong.Text = row.Cells["TongSoGS"].Value.ToString();
            }
        }
        private void LoadData()
        {
            using (var context = new Model2())
            {
                // Lấy tất cả các khoa từ cơ sở dữ liệu
                var khoas = context.Khoas.ToList();

                // Cập nhật nguồn dữ liệu của DataGridView
                dvgKhoa.DataSource = khoas;

                // Đảm bảo DataGridView hiển thị các cột chính xác
                dvgKhoa.AutoGenerateColumns = true; // Tự động sinh các cột từ thuộc tính của lớp Khoa
            }
            using (var context = new Model2())
            {
                var khoas = context.Khoas.ToList();

                // Đảm bảo tự động sinh các cột từ lớp Khoa
                dvgKhoa.DataSource = khoas;

                // Nếu muốn kiểm soát cột hiển thị cụ thể, bạn có thể sử dụng đoạn sau:
                dvgKhoa.Columns["MaKhoa"].HeaderText = "Mã Khoa";
                dvgKhoa.Columns["TenKhoa"].HeaderText = "Tên Khoa";
                dvgKhoa.Columns["TongSoGS"].HeaderText = "Tổng số Giảng Viên";
            }
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTen_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTong_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmFalculty_Load(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
