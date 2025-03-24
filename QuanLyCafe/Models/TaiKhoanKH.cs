using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuanLyCafe.Models;

[Table("TAI_KHOAN_KH")]
public class TaiKhoanKH
{
    [Key]
    [Column("MaTaiKhoan")]
    public string Id { get; set; }
    [Column("TenTaiKhoan")]
    public string Username { get; set; }

    [Column("MatKhau")]
    public string Password { get; set; }

    [Column("MaQuyen")]
    public string QuyenId { get; set; }

    [ForeignKey("QuyenId")]
    public Quyen Quyen { get; set; }

    [Column("MaKhachHang")]
    public string MaKhachHang { get; set; }

    [ForeignKey("MaKhachHang")]
    public KhachHang KhachHang { get; set; } // Liên kết với bảng KHACH_HANG
}
