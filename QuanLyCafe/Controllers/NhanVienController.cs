using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            .Select(nv => new
            {
                nv.Id,
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateNhanVien(
    [FromForm] string HoTen,
    [FromForm] string MaQuan,
    [FromForm] string DiaChi,
    [FromForm] DateTime? NgaySinh,
    [FromForm] string GioiTinh,
    [FromForm] string ChucVu,
    [FromForm] string SoDienThoai,
    [FromForm] string SoCCCD,
    [FromForm] string Email,
    [FromForm] decimal LuongCoBan,
    [FromForm] decimal HeSoLuong)
    {
        // Validate required fields
        if (string.IsNullOrEmpty(HoTen))
            return Json(new { success = false, message = "Họ tên là bắt buộc" });

        if (string.IsNullOrEmpty(MaQuan))
            return Json(new { success = false, message = "Mã quán là bắt buộc" });

        // Generate unique ID
        string newId;
        do
        {
            newId = Guid.NewGuid().ToString("N");
        }
        while (_context.NhanVien.Any(n => n.Id == newId));

        try
        {
            var newNhanVien = new NhanVien
            {
                Id = newId,
                HoTen = HoTen,
                MaQuan = MaQuan,
                DiaChi = DiaChi,
                NgaySinh = NgaySinh,
                GioiTinh = GioiTinh,
                ChucVu = ChucVu,
                SoDienThoai = SoDienThoai,
                SoCCCD = SoCCCD,
                Email = Email,
                LuongCoBan = LuongCoBan,
                HeSoLuong = HeSoLuong
            };

            _context.NhanVien.Add(newNhanVien);
            await _context.SaveChangesAsync();

            return Json(new
            {
                success = true,
                message = "Thêm nhân viên thành công!",
                id = newId
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                success = false,
                message = $"Lỗi server: {ex.Message}"
            });
        }
    }
    [HttpDelete]
    public async Task<IActionResult> DeleteNhanVien(string id)
    {
        var nhanVien = await _context.NhanVien.FindAsync(id);
        if (nhanVien == null)
        {
            return Json(new { success = false, message = "Nhân viên không tồn tại!" });
        }

        try
        {
            _context.NhanVien.Remove(nhanVien);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Xóa nhân viên thành công!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = $"Lỗi: {ex.Message}" });
        }
    }



}
