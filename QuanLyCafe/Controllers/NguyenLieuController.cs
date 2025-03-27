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
    [HttpPost]
    // public IActionResult ThemCoSo(QuanCafe model)
    // {
    //     if (ModelState.IsValid)
    //     {
    //         try
    //         {
    //             _context.QuanCafe.Add(model);
    //             _context.SaveChanges();
    //             return RedirectToAction("Index");
    //         }
    //         catch (Exception ex)
    //         {
    //             ModelState.AddModelError("", "Lỗi khi thêm cơ sở: " + ex.Message);
    //         }
    //     }
    //     return View("Index", _context.QuanCafe.ToList());
    // }
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

    // GET: NguyenLieu/ChiTietPhieuNhap/{id}
    [HttpGet]
    public IActionResult ChiTietPhieuNhap(string id)
    {
        var phieuNhap = _context.PhieuNhapHang
            .Include(p => p.ChiTietPhieuNhap)
                .ThenInclude(ct => ct.NguyenLieu)
            .Include(p => p.QuanCafe)
            .Include(p => p.NhanVien)
            .Include(p => p.NhaCungCap)
            .FirstOrDefault(p => p.Id == id);

        if (phieuNhap == null)
        {
            return NotFound();
        }

        return View(phieuNhap);
    }
    
}