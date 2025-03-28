using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace QuanLyCafe.Models
{
    [Table("KHACH_HANG")]
    public class KhachHang
    {
        [Key]
        [Column("MaKhachHang")]
        public  string Id { get; set; }

        [Column("TenKhachHang")]
        public required string TenKhachHang { get; set; }

        [Column("SDT")]
        public  required string SoDienThoai { get; set; }

        [Column("Email")]
        public required string Email { get; set; }

        [Column("DiaChiGiaoHang")]
        public required string DiaChiGiaoHang { get; set; }

        // Danh sách tài khoản khách hàng thuộc về khách hàng này
        public List<TaiKhoanKH>? TaiKhoanKHs { get; set; }
    }
}