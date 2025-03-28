using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCafe.Models;


namespace QuanLyCafe.Controllers
{
    public class QlyKhachController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QlyKhachController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách khách hàng
        public async Task<IActionResult> Index()
        {
            var khachHangs = await _context.KhachHang.ToListAsync();
            return View(khachHangs);
        }

        [HttpPost]
        public async Task<IActionResult> CreateKhachHang([FromForm] string TenKhachHang, [FromForm] string SoDienThoai, [FromForm] string Email, [FromForm] string DiaChiGiaoHang)
        {
            if (string.IsNullOrEmpty(TenKhachHang) || string.IsNullOrEmpty(SoDienThoai) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(DiaChiGiaoHang))
            {
                return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin!" });
            }

            // Tạo ID ngẫu nhiên
            string newId;
            do
            {
                newId = Guid.NewGuid().ToString("N");
            }
            while (_context.KhachHang.Any(kh => kh.Id == newId));



            var newKhachHang = new KhachHang
            {
                Id = newId,
                TenKhachHang = TenKhachHang,
                SoDienThoai = SoDienThoai,
                Email = Email,
                DiaChiGiaoHang = DiaChiGiaoHang,
            };

            _context.KhachHang.Add(newKhachHang);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Thêm khách hàng thành công!", id = newId });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteKhachHang(string id)
        {
            try
            {
                var khachHang = await _context.KhachHang
                    .Include(kh => kh.TaiKhoanKHs) // Thêm include để xử lý các liên kết
                    .FirstOrDefaultAsync(kh => kh.Id == id);

                if (khachHang == null)
                {
                    return Json(new { success = false, message = "Khách hàng không tồn tại!" });
                }

                // Xóa tất cả tài khoản liên quan trước
                if (khachHang.TaiKhoanKHs.Any())
                {
                    _context.TaiKhoanKH.RemoveRange(khachHang.TaiKhoanKHs);
                }

                _context.KhachHang.Remove(khachHang);
                await _context.SaveChangesAsync();

                return new JsonResult(new { success = true, message = "Xóa thành công!" })
                {
                    StatusCode = 200 // Explicitly set status code
                };
            }
            catch (DbUpdateException ex)
            {
                // Xử lý lỗi ràng buộc cơ sở dữ liệu
                return new JsonResult(new
                {
                    success = false,
                    message = "Không thể xóa do tồn tại dữ liệu liên quan!",
                    details = ex.InnerException?.Message
                })
                {
                    StatusCode = 500
                };
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    success = false,
                    message = "Lỗi server: " + ex.Message
                })
                {
                    StatusCode = 500
                };
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditKhachHang(
    [FromForm] string Id,
    [FromForm] string TenKhachHang,
    [FromForm] string SoDienThoai,
    [FromForm] string Email,
    [FromForm] string DiaChiGiaoHang)
        {
            try
            {
                Console.WriteLine($"ID: {Id}");
                Console.WriteLine($"Tên: {TenKhachHang}");
                Console.WriteLine($"SĐT: {SoDienThoai}");

                var khachHang = await _context.KhachHang.FindAsync(Id);
                if (khachHang == null)
                {
                    return Json(new { success = false, message = "Khách hàng không tồn tại!" });
                }

                // Validate
                var validationErrors = new List<string>();
                if (string.IsNullOrWhiteSpace(TenKhachHang)) validationErrors.Add("Tên là bắt buộc");
                if (string.IsNullOrWhiteSpace(SoDienThoai)) validationErrors.Add("SĐT là bắt buộc");

                if (validationErrors.Any())
                {
                    return Json(new
                    {
                        success = false,
                        message = "Lỗi validation",
                        errors = validationErrors
                    });
                }

                // Cập nhật
                khachHang.TenKhachHang = TenKhachHang.Trim();
                khachHang.SoDienThoai = SoDienThoai.Trim();
                khachHang.Email = Email?.Trim() ?? "";
                khachHang.DiaChiGiaoHang = DiaChiGiaoHang?.Trim() ?? "";

                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Cập nhật thành công!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex}");
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi server: " + ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetKhachHangById(string id)
        {
            try
            {
                var khachHang = await _context.KhachHang
                    .FirstOrDefaultAsync(kh => kh.Id == id);

                if (khachHang == null)
                {
                    return NotFound(new { success = false, message = "Không tìm thấy khách hàng" });
                }

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        id = khachHang.Id,
                        ten = khachHang.TenKhachHang,
                        sdt = khachHang.SoDienThoai,
                        email = khachHang.Email,
                        diachi = khachHang.DiaChiGiaoHang
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Lỗi server: " + ex.Message
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTaiKhoanKhachHang(string id)
        {
            var taiKhoans = await _context.TaiKhoanKH
                .Where(tk => tk.MaKhachHang == id)
                .Select(tk => new
                {
                    tk.Username,
                    tk.Password
                })
                .ToListAsync();

            if (taiKhoans.Count == 0)
            {
                return Json(new { success = true, noAccount = true, message = "Chưa có tài khoản cho khách hàng này." });
            }

            return Json(new { success = true, noAccount = false, data = taiKhoans });
        }

        [HttpPost]
        public async Task<IActionResult> ThemTaiKhoan([FromForm] string Username, [FromForm] string Password, [FromForm] string MaKhachHang)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(MaKhachHang))
            {
                return Json(new { success = false, message = "Thông tin tài khoản không hợp lệ!" });
            }

            bool exists = await _context.TaiKhoanKH.AnyAsync(tk => tk.Username == Username);
            if (exists)
            {
                return Json(new { success = false, message = "Tên tài khoản đã tồn tại!" });
            }

            string newId;
            do
            {
                newId = Guid.NewGuid().ToString("N");
            }
            while (await _context.TaiKhoanKH.AnyAsync(tk => tk.Id == newId));

            var newAccount = new TaiKhoanKH
            {
                Id = newId,
                Username = Username,
                Password = Password,
                QuyenId = "4",
                MaKhachHang = MaKhachHang
            };

            _context.TaiKhoanKH.Add(newAccount);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Thêm tài khoản thành công!" });
        }
    }
}