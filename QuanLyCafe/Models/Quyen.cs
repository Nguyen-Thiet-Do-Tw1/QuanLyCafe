using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("QUYEN")]
public class Quyen
{
    [Key]
    [Column("MaQuyen")]
    public string Id { get; set; } 

    [Column("TenQuyen")]
    public string TenQuyen { get; set; }

    // Liên kết đến danh sách tài khoản có quyền này
    public List<TaiKhoan> TaiKhoans { get; set; }
}
