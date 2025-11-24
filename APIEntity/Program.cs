using APIEntity.DataContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// connectionstring
var conString = @"data source=.;user id= sa;password=boy100890;initial catalog=QuanLySinhVien;Persist Security Info=false;Encrypt=false;TrustServerCertificate=false;integrated security=false;MultipleActiveResultSets=True;Pooling=True;Min Pool Size=10;Max Pool Size=200;Connection Timeout=30";
//var conString = builder.Configuration.GetConnectionString("DatabaseContext");

// Đăng ký DbContext với SQL Server
builder.Services.AddDbContext<AppDBContext>(options =>options.UseSqlServer(conString));

//tùy chỉnh hành vi IIS => không cần viết, ứng dụng vẫn chạy tốt với cấu hình mặc định
builder.Services.Configure<IISOptions>(options => { });

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthorization();

//ánh xạ route từ các attribute [Route], [HttpGet], [HttpPost] trong Controller
app.MapControllers();

app.MapGet("/", () => "Hello World v191125");
app.Run();
