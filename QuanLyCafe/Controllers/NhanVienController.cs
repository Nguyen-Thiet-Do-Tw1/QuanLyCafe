using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCafe.Models;
using System.Linq;
using System.Threading.Tasks;

public class NhanVienController : Controller
{
    private readonly ApplicationDbContext _context;

    public NhanVienController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var quanCafes = await _context.QuanCafe.ToListAsync();
        return View(quanCafes);
    }

    [HttpGet]
    public async Task<IActionResult> GetNhanVienByQuan(string maQuan)
    {
        var nhanViens = await _context.NhanVien
            .Where(nv => nv.MaQuan == maQuan)
            .Select(nv => new {
                nv.HoTen,
                nv.DiaChi,
                nv.NgaySinh,
                nv.GioiTinh,
                nv.ChucVu,
                nv.SoDienThoai,
                nv.Email,
                
            })
            .ToListAsync();

        return Json(nhanViens);
    }
}
