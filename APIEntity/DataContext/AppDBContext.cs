using APIEntity.Entities;
using Microsoft.EntityFrameworkCore;

namespace APIEntity.DataContext
{
    public class AppDBContext : DbContext
    {
        public DbSet<SinhVien> SinhVien { get; set; }

        //constructor có tham số của AppDbContext.
        //Nó nhận DbContextOptions từ hệ thống Dependency Injection để cấu hình kết nối database.
        // base(options) gọi constructor của lớp cha (DbContext) để khởi tạo đúng cách.
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        /*
         OnModelCreating là nơi  cấu hình mapping giữa entity và database.
         ModelBuilder cung cấp API để định nghĩa bảng, cột, quan hệ.
         base.OnModelCreating(modelBuilder) đảm bảo giữ lại các cấu hình mặc định.
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Đặt tên bảng cho entity Product
            //modelBuilder.Entity<SinhVien>().ToTable("SinhViens");
        }
    }
}
