using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("NHA_CUNG_CAP")]
public class NhaCungCap
{
    [Key]
    [Column("MaNhaCungCap")]
    public string MaNhaCungCap { get; set; }

    [Required]
    [Column("TenNhaCungCap")]
    [StringLength(255)]
    public string TenNhaCungCap { get; set; }

    [Column("DiaChi")]
    [StringLength(255)]
    public string DiaChi { get; set; }

    [Required]
    [Column("SDTNCC")]
    [StringLength(20)]
    public string SDTNCC { get; set; }

    [Column("STK")]
    [StringLength(50)]
    public string STK { get; set; }
}
