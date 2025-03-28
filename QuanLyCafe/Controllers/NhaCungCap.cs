using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCafe.Models;
using System.Diagnostics;

namespace QuanLyCafe.Controllers
{
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhaCungCapController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllNhaCungCap()
        {
            var nhaCungCaps = await _context.NhaCungCap.ToListAsync();
            return Json(nhaCungCaps);
        }
    }
}