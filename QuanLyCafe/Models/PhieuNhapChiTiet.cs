using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

[Table("PHIEU_NHAP_CHI_TIET")]
[PrimaryKey(nameof(MaPhieuNhap), nameof(MaNguyenLieu))]
public class PhieuNhapChiTiet
{
    [Column("MaPhieuNhap")]
    public string MaPhieuNhap { get; set; }

    [Column("MaNguyenLieu")]
    public string MaNguyenLieu { get; set; }

    [Column("SoLuong")]
    public decimal SoLuong { get; set; }

    [Column("DonGia")]
    public decimal DonGia { get; set; }

    [NotMapped]
    public decimal ThanhTien => SoLuong * DonGia;

    [ForeignKey("MaPhieuNhap")]
    public PhieuNhapHang PhieuNhapHang { get; set; }

    [ForeignKey("MaNguyenLieu")]
    public NguyenLieu NguyenLieu { get; set; }
}
