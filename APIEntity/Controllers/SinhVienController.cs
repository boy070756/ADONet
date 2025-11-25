using APIEntity.DataContext;
using APIEntity.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIEntity.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SinhVienController : ControllerBase
    {
        //_appDbContext → biến này sẽ lưu lại đối tượng DbContext để dùng trong các action.
        private readonly AppDBContext _appDBContext;

        //Đây là constructor của SinhVienController
        //ASP.NET Core sẽ tự động inject(tiêm) AppDBContext vào đây thông qua Dependency Injection(DI).
        //appDBContext(tham số) chính là đối tượng DbContext được tạo sẵn bởi hệ thống.
        public SinhVienController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        //Mỗi request HTTP sẽ được cấp một AppDbContext mới.
        //Khi request kết thúc → DbContext đó bị dispose(hủy) (giải phóng khỏi bộ nhớ).
        //Toàn bộ dữ liệu mà DbContext đang tracking trong bộ nhớ(các entity đã load) sẽ bị xóa.
        //Đây là cơ chế Scoped lifetime trong Dependency Injection của ASP.NET Core.
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var sinhViens = await _appDBContext.SinhVien.ToListAsync();// dữ liệu load vào RAM
                // xử lý trong RAM
                return Ok(new { success = true, data = sinhViens });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }// khi request kết thúc, DbContext dispose => dữ liệu trong RAM biến mất
        [HttpPost("{maSV}")]
        public async Task<IActionResult> GetByMaSV(int maSV)
        {
            var found = await _appDBContext.SinhVien
                                           .FirstOrDefaultAsync(x => x.MaSV == maSV);
            //kiem tra ton tai
            if (found == null) return NotFound("Sinh vien khong ton tai");

            return Ok(found);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SinhVien request)
        {
            _appDBContext.SinhVien.Add(request);
            //add object
            await _appDBContext.SaveChangesAsync();

            return Ok(request);
        }

        [HttpPut("{maSV}")]
        public async Task<IActionResult> Update(int maSV, SinhVien request)
        {
            var found = await _appDBContext.SinhVien
                                           .FirstOrDefaultAsync(x => x.MaSV == maSV);
            //kiem tra ton tai
            if (found == null) return BadRequest("Sinh vien khong ton tai");

            //update
            found.HoTen = request.HoTen;
            found.NgaySinh = request.NgaySinh;
            found.Lop = request.Lop;
            found.GioiTinh = request.GioiTinh;
            found.Email = request.Email;

            _appDBContext.SinhVien.Update(found);
            //add object
            await _appDBContext.SaveChangesAsync();

            return Ok(found);
        }

        [HttpDelete("{maSV}")]
        public async Task<IActionResult> Delete(int maSV)
        {
            var found = await _appDBContext.SinhVien
                                           .FirstOrDefaultAsync(x => x.MaSV == maSV);
            //kiem tra ton tai
            if (found == null) return NotFound("Sinh vien khong ton tai");

            _appDBContext.SinhVien.Remove(found);
            //add object
            await _appDBContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
