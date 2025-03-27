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
            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return Json(new { success = false, message = "Khách hàng không tồn tại!" });
            }

            _context.KhachHang.Remove(khachHang);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Xóa khách hàng thành công!" });
        }

        [HttpPut]
        public async Task<IActionResult> EditKhachHang(string id, [FromBody] KhachHang updatedKhachHang)
        {
            var khachHang = await _context.KhachHang.FindAsync(id);
            if (khachHang == null)
            {
                return Json(new { success = false, message = "Khách hàng không tồn tại!" });
            }

            khachHang.TenKhachHang = updatedKhachHang.TenKhachHang;
            khachHang.SoDienThoai = updatedKhachHang.SoDienThoai;
            khachHang.Email = updatedKhachHang.Email;
            khachHang.DiaChiGiaoHang = updatedKhachHang.DiaChiGiaoHang;

            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Cập nhật khách hàng thành công!" });
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
                QuyenId= "4",
                MaKhachHang = MaKhachHang
            };

            _context.TaiKhoanKH.Add(newAccount);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Thêm tài khoản thành công!" });
        }










    }
}