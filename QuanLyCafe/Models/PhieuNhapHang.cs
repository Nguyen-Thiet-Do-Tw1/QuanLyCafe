using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using QuanLyCafe.Models;

[Table("PHIEU_NHAP_HANG")]
public class PhieuNhapHang
{
    [Key]
    [Column("MaPhieuNhap")]
    public string Id { get; set; }

    [Column("MaQuan")]
    public string MaQuan { get; set; }

    [Column("NgayLap")]
    public DateTime NgayLap { get; set; } = DateTime.Now;

    [Column("NgayNhanHang")]
    public DateTime? NgayNhanHang { get; set; }

    [Column("MaNhanVien")]
    public string MaNhanVien { get; set; }

    [Column("MaNhaCungCap")]
    public string MaNhaCungCap { get; set; }

    [Column("TrangThai")]
    public byte  TrangThai { get; set; } = 0; // 0: Chờ duyệt, 1: Đã duyệt, 2: Đã nhận, -1: Hủy

    [Column("TongTien")]
    public decimal? TongTien { get; set; }

    [Column("GhiChu")]
    public string GhiChu { get; set; }

    // Quan hệ với bảng QUAN_CAFE
    [ForeignKey("MaQuan")]
    public QuanCafe QuanCafe { get; set; }

    // Quan hệ với bảng NHAN_VIEN
    [ForeignKey("MaNhanVien")]
    public NhanVien NhanVien { get; set; }

    // Quan hệ với bảng NHA_CUNG_CAP
    [ForeignKey("MaNhaCungCap")]
    public NhaCungCap NhaCungCap { get; set; }

    // Danh sách chi tiết phiếu nhập
    public ICollection<PhieuNhapChiTiet> ChiTietPhieuNhap { get; set; }
}
