using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("DO_UONG")]
public class DoUong
{
    [Key]
    [Column("MaDoUong")]
    public string Id { get; set; } 

    [Column("TenDoUong")]
    public string TenDoUong { get; set; }

    [Column("DonGia")]
    public decimal DonGia { get; set; }

    [Column("HinhAnh")]
    public string HinhAnh { get; set; }

    [Column("MoTa")]
    public string MoTa { get; set; }

    [Column("MaLoai")]
    public string MaLoai { get; set; }

    // Quan hệ với bảng Loại Đồ Uống
    [ForeignKey("MaLoai")]
    public LoaiDoUong LoaiDoUong { get; set; }
}
