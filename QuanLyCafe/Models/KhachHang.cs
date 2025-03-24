using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("KHACH_HANG")]
public class KhachHang
{
    [Key]
    [Column("MaKhachHang")]
    public string Id { get; set; } 

    [Column("TenKhachHang")]
    public string TenKhachHang { get; set; }

    [Column("SDT")]
    public string SoDienThoai { get; set; }

    [Column("Email")]
    public string Email { get; set; }

    [Column("DiaChiGiaoHang")]
    public string DiaChiGiaoHang { get; set; }

    // Danh sách tài khoản khách hàng thuộc về khách hàng này
    public List<TaiKhoanKH> TaiKhoanKHs { get; set; }
}
