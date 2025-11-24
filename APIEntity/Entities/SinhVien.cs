using System.ComponentModel.DataAnnotations;

namespace APIEntity.Entities
{
    public class SinhVien
    {
        [Key]
        public int MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string GioiTinh { get; set; }
        public string Lop { get; set; }
        public string Email { get; set; }
    }
}
