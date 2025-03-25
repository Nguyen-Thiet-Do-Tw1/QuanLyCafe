// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Http;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;
// using QuanLyCafe.Models;

// public class AccountController : Controller
// {
//     private readonly ApplicationDbContext _context;

//     public AccountController(ApplicationDbContext context)
//     {
//         _context = context;
//     }

//     [HttpGet]
//     public IActionResult Login()
//     {
//         return View();
//     }

//     [HttpPost]
//     public IActionResult Login(string username, string password)
//     {
//         var userKH = _context.TaiKhoanKH
//             .FirstOrDefault(u => u.Username == username && u.Password == password);

//         var userNV = _context.TaiKhoan
//             .Include(u => u.Quyen) // Load thông tin quyền
//             .FirstOrDefault(u => u.Username == username && u.Password == password);

//         if (userKH != null)
//         {
//             HttpContext.Session.SetString("UserRole", "KhachHang");
//             return RedirectToAction("Index", "Home");
//         }
//         else if (userNV != null)
//         {
//             if (userNV.Quyen == null)
//             {
//                 ViewBag.Error = "Tài khoản không có quyền hợp lệ!";
//                 return View();
//             }

//             if (userNV.Quyen.TenQuyen == "Admin")
//             {
//                 HttpContext.Session.SetString("UserRole", "Admin");
//                 return RedirectToAction("Index", "Admin");
//             }
//             else
//             {
//                 HttpContext.Session.SetString("UserRole", "NhanVien");
//                 return RedirectToAction("Index", "Staff");
//             }
//         }

//         ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng!";
//         return View();
//     }

//     public IActionResult Logout()
//     {
//         HttpContext.Session.Clear();
//         return RedirectToAction("Login");
//     }
// }
