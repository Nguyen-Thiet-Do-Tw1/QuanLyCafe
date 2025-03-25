using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCafe.Models
{
    [Table("NHAN_VIEN")]
    public class NhanVien
    {
        [Key]
        [Column("MaNhanVien")]
        public required string Id { get; set; }

        [Required]
        [Column("MaQuan")]
        public required string MaQuan { get; set; } // Mã quán cafe

        [ForeignKey("MaQuan")]
        public QuanCafe? QuanCafe { get; set; } // Liên kết với quán cafe

        [Required]
        [Column("HoTen")]
        public required string HoTen { get; set; }

        [Column("DiaChi")]
        public required string DiaChi { get; set; }

        [Column("NgaySinh")]
        public DateTime? NgaySinh { get; set; }

        [Column("GioiTinh")]
        [StringLength(10)]
        public required string GioiTinh { get; set; }

        [Required]
        [Column("ChucVu")]
        public required string ChucVu { get; set; }

        [Required]
        [Column("SDT")]
        [StringLength(20)]
        public required string SoDienThoai { get; set; }

        [Required]
        [Column("SoCCCD")]
        [StringLength(50)]
        public required string SoCCCD { get; set; } 

        [Column("Email")]
        public required string Email { get; set; }

        [Required]
        [Column("LuongCoBan")]
        [Range(0, double.MaxValue)]
        public decimal LuongCoBan { get; set; } = 0; // Lương cơ bản

        [Required]
        [Column("HeSoLuong")]
        [Range(0, double.MaxValue)]
        public decimal HeSoLuong { get; set; } = 1; // Hệ số lương
    }
}
