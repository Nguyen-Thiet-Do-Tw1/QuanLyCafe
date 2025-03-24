using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCafe.Models;


namespace QuanLyCafe.Controllers {
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
    }
}