using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace Lesson1
{
    public partial class Form1 : Form
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["DBContext"].ConnectionString;

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
                dgvSinhVien.Rows.Add(
                    sqlReader["MaSV"],
                    sqlReader["HoTen"],
                    sqlReader["NgaySinh"],
                    sqlReader["GioiTinh"],
                    sqlReader["Lop"],
                    sqlReader["Email"]);
            }
            //dgvSinhVien.Columns["MaSV"].ReadOnly = true;
            sqlReader.Close();
            sqlConnect.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            //12 dòng dữ liệu thực sự, thì Rows.Count = 13 va index[0]
            
            var lastRow = dgvSinhVien.Rows[dgvSinhVien.Rows.Count - 2];

            if (!string.IsNullOrEmpty(lastRow.Cells["MaSV"].Value?.ToString()))
            {
                MessageBox.Show("Không có dữ liệu mới");
            }
            else 
            {
                var sqlConnect = new SqlConnection(connectionString);
                sqlConnect.Open();

                var queryInsert = @"Insert Into SinhVien(HoTen,Email,NgaySinh,Lop,GioiTinh)
                                    OutPut Inserted.MaSV
                                    Values (@HoTen,@Email,@NgaySinh,@Lop,@GioiTinh)";
                var cmd = new SqlCommand(queryInsert, sqlConnect);
                cmd.Parameters.AddWithValue("@HoTen", lastRow.Cells["HoTen"].Value?.ToString());
                cmd.Parameters.AddWithValue("@Email", lastRow.Cells["Email"].Value?.ToString());
                cmd.Parameters.AddWithValue("@NgaySinh", lastRow.Cells["NgaySinh"].Value?.ToString());
                cmd.Parameters.AddWithValue("@Lop", lastRow.Cells["Lop"].Value?.ToString());
                cmd.Parameters.AddWithValue("@GioiTinh", lastRow.Cells["GioiTinh"].Value?.ToString());

                try
                {
                    var maSinhVienMoi = cmd.ExecuteScalar();

                    // Gán ID mới vào DataRow
                    lastRow.Cells["MaSV"].Value = maSinhVienMoi;

                    MessageBox.Show($"Sinh Vien: {lastRow.Cells["HoTen"].Value?.ToString()}, tạo thành công");
                }
                catch (SqlException ex)
                {
                    MessageBox.Show($"Lỗi SQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi không xác định: {ex.Message}");
                }
                finally
                {
                    sqlConnect.Close();
                }
            }
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

            //dgvSinhVien.Columns["MaSV"].ReadOnly = true;
        }

        public void LoadDisconnected1()
        {
            var query = "Select * From SinhVien";

            conn = new SqlConnection(connectionString);

            adapter = new SqlDataAdapter(query, conn);

            DataTable table = new DataTable();
            adapter.Fill(table);// Lấy dữ liệu vào DataTable và ngắt kết nối

            dgvSinhVien.DataSource = table;
            //dgvSinhVien.Columns["MaSV"].ReadOnly = true;
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
            //dgvSinhVien.Columns["MaSV"].ReadOnly = true;
        }

        private void btnLoadDisconnected_Click(object sender, EventArgs e)
        {
            LoadDisconnected();
        }

        private void btnLoadDisconnected1_Click(object sender, EventArgs e)
        {
            LoadDisconnected2();
        }
        private (bool statusChanged, string message) CheckStatus(DataViewRowState request)
        {
            var modifiedRows = dataSet.Tables["SinhVien"].Select(null, null, request);

            if (modifiedRows.Length == 0)
                return (false, "Không có dữ liệu mới hoặc các dòng dữ liệu không có thay đổi");

            var list = string.Empty;
            foreach (var row in modifiedRows)
            {
                list = list + $"- {row["MaSV"]} | {row["HoTen"]}\n";
            }
            return (true, list);
            //MessageBox.Show("Các dòng dữ liệu không có thay đổi");
        }
        private void btnCreate1_Click(object sender, EventArgs e)
        {
            try
            {
                var (status, message) = CheckStatus(DataViewRowState.Added);
                if (!status)
                {
                    MessageBox.Show(message);
                }
                else
                {
                    var resultCofirm = MessageBox.Show(message, "Xác nhận tạo mới sinh viên", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (resultCofirm == DialogResult.Yes)
                    {
                        var rowsAffected = adapter.Update(dataSet, "SinhVien");
                        if (rowsAffected < 1) throw new ApplicationException("Khong Tao Duoc Sinh Vien");

                        // Sau khi đã cập nhật thành công xuống DB, reset trạng thái
                        //trạng thái Added, Modified, hoặc Deleted sẽ được chuyển sang trạng thái Unchanged
                        dataSet.AcceptChanges();

                        dataSet.Tables["SinhVien"].Clear(); // Xóa dữ liệu cũ
                        adapter.Fill(dataSet.Tables["SinhVien"]); // Load lại từ DB

                        MessageBox.Show("Tạo thành công");
                    }
                    else
                    {
                        dataSet.RejectChanges();
                        MessageBox.Show("Không Tạo");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #region test
        //private void btnUpdate1_Click(object sender, EventArgs e)
        //{
        //    adapter.Update(dataSet, "SinhVien");
        //    MessageBox.Show("Cập nhật thành công");
        //}

        //private void btnUpdate2_Click(object sender, EventArgs e)
        //{
        //    var result = MessageBox.Show("Bạn có chắc cập nhật",
        //        "Xác nhận update",
        //        MessageBoxButtons.YesNo,
        //        MessageBoxIcon.Question);
        //    if (result == DialogResult.Yes)
        //    {
        //        adapter.Update(dataSet, "SinhVien");
        //        MessageBox.Show("Cập nhật thành công");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không Cập nhật");
        //    }
        //}

        //private void btnUpdate3_Click(object sender, EventArgs e)
        //{
        //    var result = MessageBox.Show("Bạn có chắc cập nhật",
        //                                 "Xác nhận update",
        //                                 MessageBoxButtons.YesNo,
        //                                 MessageBoxIcon.Question);

        //    if (result == DialogResult.Yes)
        //    {
        //        if (dataSet.HasChanges() == true)
        //        {
        //            adapter.Update(dataSet, "SinhVien");
        //            MessageBox.Show("Cập nhật thành công");
        //        }
        //        else MessageBox.Show("Các dòng dữ liệu không có thay đổi");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Không Cập nhật");
        //    }
        //}

        //private void btnUpdate4_Click(object sender, EventArgs e)
        //{
        //    var modifiedRows = dataSet.Tables["SinhVien"].Select(null, null, DataViewRowState.ModifiedCurrent);
        //    if (modifiedRows.Length == 0)
        //    {
        //        MessageBox.Show("Các dòng dữ liệu không có thay đổi");
        //    }
        //    else
        //    {
        //        var list = string.Empty;
        //        foreach (var row in modifiedRows)
        //        {
        //            list = list + $"- {row["MaSV"]} | {row["HoTen"]}\n";
        //        }
        //        var resultCofirm = MessageBox.Show(
        //       list, "Xác nhận update",
        //       MessageBoxButtons.YesNo,
        //       MessageBoxIcon.Question);
        //        if (resultCofirm == DialogResult.Yes)
        //        {
        //            adapter.Update(dataSet, "SinhVien");
        //            MessageBox.Show("Cập nhật thành công");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Không Cập nhật");
        //        }
        //    }
        //}

        //private void btnCreate_Click(object sender, EventArgs e)
        //{
        //    var modifiedRows = dataSet.Tables["SinhVien"].Select(null, null, DataViewRowState.Added);
        //    if (modifiedRows.Length == 0)
        //    {
        //        MessageBox.Show("Không có dữ liệu mới");
        //    }
        //    else
        //    {
        //        var list = string.Empty;
        //        foreach (var row in modifiedRows)
        //        {
        //            list = list + $"- {row["HoTen"]}\n";
        //        }
        //        var resultCofirm = MessageBox.Show(
        //       list, "Xác nhận tạo mới sinh viên",
        //       MessageBoxButtons.YesNo,
        //       MessageBoxIcon.Question);
        //        if (resultCofirm == DialogResult.Yes)
        //        {
        //            adapter.Update(dataSet, "SinhVien");
        //            dataSet.AcceptChanges();
        //            dataSet.Tables["SinhVien"].Clear(); // Xóa dữ liệu cũ
        //            adapter.Fill(dataSet.Tables["SinhVien"]); // Load lại từ DB
        //            MessageBox.Show("Tạo thành công");
        //        }
        //        else
        //        {
        //            dataSet.RejectChanges();
        //            MessageBox.Show("Không Tạo");
        //        }
        //    }
        //}

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    var gvRowSelected = gvSinhVien.SelectedRows;
        //    if (gvRowSelected.Count == 0)
        //    {
        //        MessageBox.Show("Không có dữ liệu được chọn");
        //    }
        //    else
        //    {
        //        var list = string.Empty;
        //        var rowsToDelete = new List<DataRow>();
        //        foreach (DataGridViewRow row in gvRowSelected)
        //        {
        //            if (!row.IsNewRow)
        //            {
        //                list = list + $"- {row.Cells["MaSV"].Value} | {row.Cells["HoTen"].Value}\n";
        //                //dataSet.Tables["SinhVien"].Rows[?].Delete();

        //                // Lấy DataRow tương ứng var dgr = row.DataBoundItem as DataRowView;
        //                // if (dgr != null) 
        //                // Đánh dấu dòng là Deleted
        //                // dgr.Row.Delete();

        //                // Lấy DataRow tương ứng
        //                var dgr = row.DataBoundItem as DataRowView;
        //                if (dgr != null)
        //                    rowsToDelete.Add(dgr.Row);
        //            }
        //        }
        //        var resultCofirm = MessageBox.Show(
        //       list, "Xác nhận xóa sinh viên",
        //       MessageBoxButtons.YesNo,
        //       MessageBoxIcon.Question);
        //        if (resultCofirm == DialogResult.Yes)
        //        {
        //            foreach (var row in rowsToDelete)
        //                row.Delete();

        //            adapter.Update(dataSet, "SinhVien");
        //            dataSet.AcceptChanges();
        //            MessageBox.Show("Xóa thành công");
        //        }
        //        else
        //        {
        //            dataSet.RejectChanges();
        //            MessageBox.Show("Không Xóa");
        //        }
        //    }
        //}

        //private void btnSeeStatus_Click(object sender, EventArgs e)
        //{
        //    var modifiedRows = dataSet.Tables["SinhVien"].Select(null, null, DataViewRowState.ModifiedCurrent);
        //    if (modifiedRows.Length == 0)
        //    {
        //        MessageBox.Show("Các dòng dữ liệu không có thay đổi");
        //    }
        //    else
        //    {
        //        var list = string.Empty;
        //        foreach (var row in modifiedRows)
        //        {
        //            list = list + $"- {row["MaSV"]} | {row["HoTen"]}\n";
        //        }
        //        MessageBox.Show(list, "Trạng Thái");
        //    }
        //}
        #endregion


        #region form1
        //public class SinhVien
        //{
        //    public string HoTen { get; set; }
        //    public string Email { get; set; }
        //    public DateTime NgaySinh { get; set; }
        //    public string Lop { get; set; }
        //    public string GioiTinh { get; set; }
        //}
        //public void LoadDB()
        //{
        //    conn = new SqlConnection(connStr);
        //    var query = "Select * From SinhVien";
        //    adapter = new SqlDataAdapter(query, conn);
        //    var builder = new SqlCommandBuilder(adapter);
        //    ds = new DataSet();
        //    adapter.Fill(ds, "SinhVien");
        //    gvSinhVien.DataSource = ds.Tables["SinhVien"];

        //    //gvSinhVien.ReadOnly = true;
        //    gvSinhVien.SelectionMode = DataGridViewSelectionMode.CellSelect;
        //    gvSinhVien.MultiSelect = false;
        //    gvSinhVien.AllowUserToAddRows = false;
        //    gvSinhVien.AllowUserToDeleteRows = false;
        //    //gvSinhVien.AllowUserToOrderColumns = false;
        //    //gvSinhVien.AllowUserToResizeColumns = false;
        //    //gvSinhVien.AllowUserToResizeRows = false;
        //    gvSinhVien.RowHeadersVisible = false;

        //    foreach (DataGridViewColumn col in gvSinhVien.Columns)
        //    {
        //        if (col.Name == "cbxSelect")
        //            col.ReadOnly = false;
        //        else
        //            col.ReadOnly = true;
        //    }
        //    AddCheckboxColumn();
        //}
        //private void AddCheckboxColumn()
        //{
        //    if (!gvSinhVien.Columns.Contains("cbxSelect"))
        //    {
        //        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn();
        //        chk.Name = "cbxSelect";
        //        chk.HeaderText = "Chọn";
        //        chk.Width = 50;
        //        chk.TrueValue = true;
        //        chk.FalseValue = false;
        //        gvSinhVien.Columns.Insert(0, chk);
        //    }
        //}
        //private void Create1(SinhVien received)
        //{
        //    var newRow = ds.Tables["SinhVien"].NewRow();
        //    newRow["HoTen"] = received.HoTen;
        //    newRow["Email"] = received.Email;
        //    newRow["NgaySinh"] = received.NgaySinh.ToString("yyyy-MM-dd");
        //    newRow["Lop"] = received.Lop;
        //    newRow["GioiTinh"] = received.GioiTinh;
        //    ds.Tables["SinhVien"].Rows.Add(newRow);

        //    try
        //    {
        //        var rowsAffected = adapter.Update(ds, "SinhVien");
        //        if (rowsAffected < 1) throw new ApplicationException("Khong Tao Duoc Sinh Vien");

        //        ds.Tables["SinhVien"].Clear();
        //        adapter.Fill(ds, "SinhVien");
        //        gvSinhVien.DataSource = ds.Tables["SinhVien"];
        //        MessageBox.Show($"Sinh Vien: {received.HoTen}, tạo thành công");

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi: ${ex.Message}");
        //    }
        //}

        //private void Create2(SinhVien received)
        //{
        //    var queryInsert = @"Insert Into SinhVien(HoTen,Email,NgaySinh,Lop,GioiTinh)
        //                            OutPut Inserted.MaSV
        //                            Values (@HoTen,@Email,@NgaySinh,@Lop,@GioiTinh)";
        //    var cmd = new SqlCommand(queryInsert, conn);
        //    cmd.Parameters.AddWithValue("@HoTen", received.HoTen);
        //    cmd.Parameters.AddWithValue("@Email", received.Email);
        //    cmd.Parameters.AddWithValue("@NgaySinh", received.NgaySinh);
        //    cmd.Parameters.AddWithValue("@Lop", received.Lop);
        //    cmd.Parameters.AddWithValue("@GioiTinh", received.GioiTinh);

        //    if (conn.State != ConnectionState.Open)
        //        conn.Open();

        //    try
        //    {
        //        var maSinhVienMoi = cmd.ExecuteScalar();
        //        var newRow = ds.Tables["SinhVien"].NewRow();
        //        newRow["MaSV"] = maSinhVienMoi;
        //        newRow["HoTen"] = received.HoTen;
        //        newRow["Email"] = received.Email;
        //        newRow["NgaySinh"] = received.NgaySinh;
        //        newRow["Lop"] = received.Lop;
        //        newRow["GioiTinh"] = received.GioiTinh;
        //        ds.Tables["SinhVien"].Rows.Add(newRow);
        //        MessageBox.Show($"Sinh Vien: {received.HoTen}, tạo thành công");
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"Lỗi SQL: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi không xác định: {ex.Message}");
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //            conn.Close();
        //    }
        //}
        //private void btnCreate_Click(object sender, System.EventArgs e)
        //{
        //    // hide main
        //    this.Hide();
        //    var frmCreate = new FormCreate();
        //    var dialog = frmCreate.ShowDialog();
        //    this.Show();
        //    if (dialog == DialogResult.OK)
        //    {
        //        Create1(frmCreate.SV);
        //    }
        //}

        //private void btn2_Click(object sender, EventArgs e)
        //{
        //    // main is blur and form create is modal
        //    var frmCreate = new FormCreate();
        //    var dialog = frmCreate.ShowDialog();
        //    if (dialog == DialogResult.OK)
        //    {
        //        Create2(frmCreate.SV);
        //    }
        //}

        //private void btnXoa_Click(object sender, System.EventArgs e)
        //{
        //    var idDelete = new List<int>();
        //    var mess = string.Empty;
        //    foreach (DataGridViewRow row in gvSinhVien.Rows)
        //    {
        //        if (row.Cells["cbxSelect"].Value != null && (bool)row.Cells["cbxSelect"].Value)
        //        {
        //            idDelete.Add((int)row.Cells["MaSV"].Value);
        //            mess += $"{row.Cells["MaSV"].Value},\n";
        //            //lay gia tri cua 1 row
        //        }
        //    }
        //    if (idDelete.Count == 0)
        //    {
        //        MessageBox.Show("Vui lòng chọn sinh viên để xóa !");
        //        return;
        //    }

        //    DialogResult result = MessageBox.Show($"Bạn có chắc muốn xóa các sinh viên đã chọn?\n{mess}", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //    if (result != DialogResult.Yes) return;

        //    var parameters = idDelete.Select((id, index) => new SqlParameter($"@p{index}", id)).ToArray();
        //    var parameterNames = string.Join(", ", parameters.Select(p => p.ParameterName));
        //    var queryDelete = $@"Delete SinhVien where MaSV in ({parameterNames})";
        //    if (conn.State != ConnectionState.Open)
        //        conn.Open();

        //    var cmd = new SqlCommand(queryDelete, conn);
        //    cmd.Parameters.AddRange(parameters);
        //    try
        //    {
        //        cmd.ExecuteNonQuery();
        //        //foreach (DataRow row in ds.Tables["SinhVien"].Rows)
        //        //{
        //        //    var found = idDelete.FirstOrDefault(x => x == (int)row["MaSV"]);
        //        //    if(found!=0) row.Delete();
        //        //}
        //        ds.Tables["SinhVien"].Clear();
        //        adapter.Fill(ds, "SinhVien");
        //        MessageBox.Show("Đã xóa các sinh viên được chọn.");
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show($"Lỗi SQL: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Lỗi không xác định: {ex.Message}");
        //    }
        //    finally
        //    {
        //        if (conn.State == ConnectionState.Open)
        //            conn.Close();
        //    }
        //}
        #endregion
        #region From create
        //public partial class FormCreate : Form
        //{
        //    public SinhVien SV { get; set; }
        //    List<CboOption> genderList = null;
        //    List<CboOption> classList = null;
        //    public FormCreate()
        //    {
        //        InitializeComponent();
        //        txtHoTen.TabIndex = 0;
        //        dtpNgaySinh.Format = DateTimePickerFormat.Custom;
        //        dtpNgaySinh.CustomFormat = "dd/MM/yyyy";

        //        genderList = new List<CboOption>
        //                    {
        //                        new CboOption { Key = "", Display = "--Chọn--" },
        //                        new CboOption { Key = "M", Display = "Nam" },
        //                        new CboOption { Key = "F", Display = "Nữ" },
        //                        new CboOption { Key = "O", Display = "Khác" }
        //                    };
        //        classList = new List<CboOption>
        //                    {
        //                        new CboOption { Key = "", Display = "--Chọn--" },
        //                        new CboOption { Key = "CNTT1", Display = "CNTT1" },
        //                        new CboOption { Key = "CNTT2", Display = "CNTT2" },
        //                        new CboOption { Key = "CNTT3", Display = "CNTT3" }
        //                    };
        //        cboGioiTinh.DataSource = genderList;
        //        cboLop.DataSource = classList;
        //    }

        //    private void btnCreate_Click(object sender, EventArgs e)
        //    {
        //        if (string.IsNullOrEmpty(txtHoTen.Text))
        //        {
        //            MessageBox.Show("Vui lòng nhập họ tên");
        //            return;
        //        }
        //        if (string.IsNullOrEmpty(txtEmail.Text))
        //        {
        //            MessageBox.Show("Vui lòng nhập email");
        //            return;
        //        }
        //        if (cboLop.SelectedItem == null)
        //        {
        //            MessageBox.Show("Vui lòng chọn lớp");
        //            return;
        //        }
        //        if (dtpNgaySinh.Value == null)
        //        {
        //            MessageBox.Show("Vui lòng chọn ngày sinh");
        //            return;
        //        }
        //        if (cboGioiTinh.SelectedItem == null)
        //        {
        //            MessageBox.Show("Vui lòng chọn giới tính");
        //            return;
        //        }
        //        SV = new SinhVien
        //        {
        //            HoTen = txtHoTen.Text,
        //            Email = txtEmail.Text,
        //            Lop = ((CboOption)cboLop.SelectedItem).Key,
        //            NgaySinh = dtpNgaySinh.Value,
        //            GioiTinh = ((CboOption)cboGioiTinh.SelectedItem).Key
        //        };
        //        this.DialogResult = DialogResult.OK;
        //        this.Close();
        //    }


        //    public class CboOption
        //    {
        //        public string Key { get; set; }
        //        public string Display { get; set; }

        //        public override string ToString()
        //        {
        //            return Display;
        //        }
        //    }
        //}
        #endregion
    }
}
