using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Lesson1
{
    public partial class Form1 : Form
    {
        SqlConnection conn;
        SqlDataAdapter adapter;
        DataSet dataSet;

        string connectionString = @"data source=tinhnvt;user id= sa;password=boy100890;initial catalog=QuanLySinhVien;
                                     Persist Security Info=false;
                                     Encrypt=true;TrustServerCertificate=true;
                                     Connection Timeout=30";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLoadConnected_Click(object sender, EventArgs e)
        {
            var query = "Select * From SinhVien";

            var sqlConnect = new SqlConnection(connectionString);
            sqlConnect.Open();

            var sqlCommand = new SqlCommand(query, sqlConnect);
            var sqlReader = sqlCommand.ExecuteReader();

            // Tạo cột cho DataGridView (nếu chưa có)
            dgvSinhVien.Columns.Clear();
            dgvSinhVien.Columns.Add("MaSV", "Mã Sinh Viên");
            dgvSinhVien.Columns.Add("HoTen", "Họ và Tên");
            dgvSinhVien.Columns.Add("NgaySinh", "Ngày Sinh");
            dgvSinhVien.Columns.Add("GioiTinh", "Giới Tính");
            dgvSinhVien.Columns.Add("Lop", "Lớp");
            dgvSinhVien.Columns.Add("Email", "Email");

            // Đọc từng dòng và thêm vào DataGridView
            while (sqlReader.Read())
            {
                dgvSinhVien.Rows.Add(sqlReader["MaSV"],
                    sqlReader["HoTen"],
                    sqlReader["NgaySinh"],
                    sqlReader["GioiTinh"],
                    sqlReader["NgaySinh"],
                    sqlReader["Lop"],
                    sqlReader["Email"]);
            }

            sqlReader.Close();
            sqlConnect.Close();
        }

        public void LoadDisconnected()
        {
            var query = "Select * From SinhVien";

            conn = new SqlConnection(connectionString);

            adapter = new SqlDataAdapter(query, conn);
            dataSet = new DataSet();
            adapter.Fill(dataSet, "SinhVien");// Lấy dữ liệu vào DataSet và ngắt kết nối

            dgvSinhVien.DataSource = dataSet.Tables["SinhVien"];

            dgvSinhVien.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
            dgvSinhVien.Columns["HoTen"].HeaderText = "Họ và Tên";
            dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dgvSinhVien.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvSinhVien.Columns["Lop"].HeaderText = "Lớp";
            dgvSinhVien.Columns["Email"].HeaderText = "Email";
        }

        public void LoadDisconnected1()
        {
            var query = "Select * From SinhVien";

            conn = new SqlConnection(connectionString);

            adapter = new SqlDataAdapter(query, conn);

            DataTable table = new DataTable();
            adapter.Fill(table);// Lấy dữ liệu vào DataTable và ngắt kết nối

            dgvSinhVien.DataSource = table;
        }

        public void LoadDisconnected2()
        {
            var list = dataSet.Tables["SinhVien"].AsEnumerable()
                 .Select(x => new
                 {
                     MaSV = x.Field<int>("MaSV"),
                     HoTen = x.Field<string>("HoTen"),
                     NgaySinh = x.Field<DateTime>("NgaySinh") 
                 }).Take(2).ToList();

            dgvSinhVien.DataSource = list;

            dgvSinhVien.Columns["MaSV"].HeaderText = "Mã Sinh Viên";
            dgvSinhVien.Columns["HoTen"].HeaderText = "Họ và Tên";
            dgvSinhVien.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
        }

        private void btnLoadDisconnected_Click(object sender, EventArgs e)
        {
            LoadDisconnected();
        }

        private void btnLoadDisconnected1_Click(object sender, EventArgs e)
        {
            LoadDisconnected2();
        }
    }
}
