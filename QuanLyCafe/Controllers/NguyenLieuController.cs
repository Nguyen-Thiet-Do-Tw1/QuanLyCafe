using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCafe.Models;


public class NguyenLieuController : Controller
{
    private readonly ApplicationDbContext _context;

    public NguyenLieuController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var quanCafes = await _context.QuanCafe.ToListAsync();
        return View(quanCafes);
    }


    [HttpGet]
    public async Task<IActionResult> GetNguyenLieunByQuan(string maQuan)
    {
        var nguyenLieus = await _context.NguyenLieu
            .Where(nl => nl.MaQuan == maQuan)
            .Select(nl => new
            {
                nl.Id,
                nl.TenNguyenLieu,
                nl.SoLuong,
                nl.DonViTinh,
                nl.DonGia,
                nl.HanSuDung,
                nl.SoLuongToiThieu

            })
            .ToListAsync();

        return Json(nguyenLieus);
    }

    public IActionResult LichSuNhapHang()
    {
        var phieuNhapList = _context.PhieuNhapHang
            .Include(p => p.QuanCafe)
            .Include(p => p.NhanVien)
            .Include(p => p.NhaCungCap)
            .OrderByDescending(p => p.NgayLap)
            .ToList();

        return View(phieuNhapList);
    }

    // // GET: NguyenLieu/ChiTietPhieuNhap/{id}
    // [HttpGet]
    // public IActionResult ChiTietPhieuNhap(string id)
    // {
    //     var phieuNhap = _context.PhieuNhapHang
    //         .Include(p => p.ChiTietPhieuNhap)
    //             .ThenInclude(ct => ct.NguyenLieu)
    //         .Include(p => p.QuanCafe)
    //         .Include(p => p.NhanVien)
    //         .Include(p => p.NhaCungCap)
    //         .FirstOrDefault(p => p.Id == id);

    //     if (phieuNhap == null)
    //     {
    //         return NotFound();
    //     }

    //     return View(phieuNhap);
    // }
    // NguyenLieuController.cs
    [HttpGet]
    public async Task<IActionResult> DanhSachPhieuNhap(string maQuan)
    {
        var phieuNhaps = await _context.PhieuNhapHang
            .Include(p => p.NhaCungCap)
            .Include(p => p.NhanVien)
            .Include(p => p.ChiTietPhieuNhap)
            .Where(p => p.MaQuan == maQuan)
            .OrderByDescending(p => p.NgayLap)
            .ToListAsync();

        return View(phieuNhaps);
    }
    // NguyenLieuController.cs
    [HttpGet]
    public async Task<IActionResult> ChiTietPhieuNhap(string id)
    {
        var phieuNhap = await _context.PhieuNhapHang
            .Include(p => p.ChiTietPhieuNhap)
                .ThenInclude(ct => ct.NguyenLieu)
            .Include(p => p.NhaCungCap)
            .Include(p => p.NhanVien)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (phieuNhap == null) return NotFound();

        return View(phieuNhap);
    }



}