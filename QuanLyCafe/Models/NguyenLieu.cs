using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuanLyCafe.Models;

[Table("NGUYEN_LIEU")]
public class NguyenLieu
{
    [Key]
    [Column("MaNguyenLieu")]
    public string Id { get; set; }

    [Column("MaQuan")]
    public string MaQuan { get; set; }

    [Column("TenNguyenLieu")]
    public string TenNguyenLieu { get; set; }

    [Column("SoLuong")]
    public decimal SoLuong { get; set; } = 0;

    [Column("DonViTinh")]
    public string DonViTinh { get; set; }

    [Column("HanSuDung")]
    public DateTime? HanSuDung { get; set; }

    [Column("DonGia")]
    public decimal DonGia { get; set; } = 0;

    [Column("SoLuongToiThieu")]
    public decimal SoLuongToiThieu { get; set; } = 0;

    // Quan hệ với bảng QUAN_CAFE
    [ForeignKey("MaQuan")]
    public QuanCafe QuanCafe { get; set; }
}
