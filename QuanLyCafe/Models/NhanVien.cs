using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCafe.Models
{
    [Table("NHAN_VIEN")]
    public class NhanVien
    {
        [Key]
        [Column("MaNhanVien")]
        public string Id { get; set; }

        [Required]
        [Column("HoTen")]
        public string HoTen { get; set; }

        [Column("DiaChi")]
        public string DiaChi { get; set; }

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        [Column("GioiTinh")]
        public string GioiTinh { get; set; }

        [Required]
        [Column("ChucVu")]
        public string ChucVu { get; set; }

        [Required]
        [Column("SDT")]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        [Required]
        [Column("MaQuan")]
        public string MaQuan { get; set; } // Mã quán cafe

        [ForeignKey("MaQuan")]
        public QuanCafe QuanCafe { get; set; } // Liên kết với quán cafe
    }
}
