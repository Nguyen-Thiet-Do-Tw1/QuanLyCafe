using System.ComponentModel.DataAnnotations.Schema;

[Table("TAI_KHOAN")]
public class TaiKhoan
{
    [Column("MaTaiKhoan")]
    public string Id { get; set; }  // MaTaiKhoan là VARCHAR(50) nên dùng string

    [Column("TenTaiKhoan")]
    public string Username { get; set; }

    [Column("MatKhau")]
    public string Password { get; set; }

    [Column("MaQuyen")]
    public string QuyenId { get; set; } // MaQuyen cũng là VARCHAR(50), phải dùng string

    // Khai báo quan hệ với bảng QUYEN
    [ForeignKey("QuyenId")]
    public Quyen Quyen { get; set; }
}
