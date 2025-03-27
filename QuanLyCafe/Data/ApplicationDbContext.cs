using Microsoft.EntityFrameworkCore;
using QuanLyCafe.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<TaiKhoanKH> TaiKhoanKH { get; set; }
    public DbSet<TaiKhoan> TaiKhoan { get; set; }
    public DbSet<Quyen> Quyen { get; set; }
    public DbSet<KhachHang> KhachHang { get; set; }
    public DbSet<DoUong> DoUong { get; set; }
    public DbSet<LoaiDoUong> LoaiDoUong { get; set; }
    public DbSet<NhanVien> NhanVien { get; set; }
    public DbSet<QuanCafe> QuanCafe { get; set; }
    public DbSet<NguyenLieu> NguyenLieu { get; set; }
    public DbSet<PhieuNhapHang> PhieuNhapHang { get; set; }
    public DbSet<PhieuNhapChiTiet> ChiTietPhieuNhap { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cấu hình quan hệ
        modelBuilder.Entity<DoUong>()
            .HasOne(d => d.LoaiDoUong)
            .WithMany(l => l.DoUongs)
            .HasForeignKey(d => d.MaLoai);
    }
}
