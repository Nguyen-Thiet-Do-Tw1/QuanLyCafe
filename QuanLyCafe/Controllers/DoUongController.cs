using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

public class DoUongController : Controller
{
    private readonly ApplicationDbContext _context;

    public DoUongController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string search, int page = 1, int pageSize = 8)
    {
        ViewData["CurrentFilter"] = search;

        var doUongs = _context.DoUong.Include(d => d.LoaiDoUong).AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            doUongs = doUongs.Where(s => s.TenDoUong.Contains(search));
        }

        ViewBag.LoaiDoUongList = await _context.LoaiDoUong
            .Select(l => new SelectListItem { Value = l.MaLoai, Text = l.TenLoai })
            .ToListAsync();

        return View(await PaginatedList<DoUong>.CreateAsync(doUongs.AsNoTracking(), page, pageSize));
    }


    [HttpPost]
    public async Task<IActionResult> CreateDoUong([FromForm] IFormFile HinhAnh, [FromForm] string TenDoUong, [FromForm] decimal DonGia, [FromForm] string MoTa, [FromForm] string MaLoai)
    {
        if (string.IsNullOrEmpty(TenDoUong) || DonGia <= 0 || string.IsNullOrEmpty(MaLoai))
        {
            return Json(new { success = false, message = "Vui lòng nhập đầy đủ thông tin!" });
        }

        // Tạo ID ngẫu nhiên và kiểm tra không trùng
        string newId;
        do
        {
            newId = Guid.NewGuid().ToString("N"); // Chuỗi không dấu gạch ngang, 32 ký tự
        }
        while (_context.DoUong.Any(d => d.Id == newId));

        string fileName = null;
        if (HinhAnh != null && HinhAnh.Length > 0)
        {
            fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnh.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await HinhAnh.CopyToAsync(stream);
            }
        }

        var newDrink = new DoUong
        {
            Id = newId,
            TenDoUong = TenDoUong,
            DonGia = DonGia,
            MoTa = MoTa,
            MaLoai = MaLoai,
            HinhAnh = fileName
        };

        _context.DoUong.Add(newDrink);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Thêm đồ uống thành công!", id = newId, imageUrl = "/img/products/" + fileName });
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        var doUong = await _context.DoUong.FindAsync(id);
        if (doUong == null)
        {
            return Json(new { success = false, message = "Không tìm thấy đồ uống!" });
        }

        _context.DoUong.Remove(doUong);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Xóa đồ uống thành công!" });
    }

    [HttpGet]
    public async Task<IActionResult> GetDrink(string id)
    {
        var doUong = await _context.DoUong.FindAsync(id);
        if (doUong == null)
        {
            return Json(new { success = false, message = "Không tìm thấy đồ uống!" });
        }

        return Json(new
        {
            success = true,
            drink = new
            {
                id = doUong.Id,
                tenDoUong = doUong.TenDoUong,
                donGia = doUong.DonGia,
                moTa = doUong.MoTa,
                maLoai = doUong.MaLoai,
                hinhAnh = string.IsNullOrEmpty(doUong.HinhAnh) ? null : Url.Content("~/img/products/" + doUong.HinhAnh)
            }
        });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateDrink([FromForm] string Id, [FromForm] IFormFile HinhAnh, [FromForm] string TenDoUong, [FromForm] decimal DonGia, [FromForm] string MoTa, [FromForm] string MaLoai)
    {
        var doUong = await _context.DoUong.FindAsync(Id);
        if (doUong == null)
        {
            return Json(new { success = false, message = "Không tìm thấy đồ uống!" });
        }

        doUong.TenDoUong = TenDoUong;
        doUong.DonGia = DonGia;
        doUong.MoTa = MoTa;
        doUong.MaLoai = MaLoai;

        // Nếu có ảnh mới, cập nhật ảnh
        if (HinhAnh != null && HinhAnh.Length > 0)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(HinhAnh.FileName);
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await HinhAnh.CopyToAsync(stream);
            }

            // Xóa ảnh cũ nếu có
            if (!string.IsNullOrEmpty(doUong.HinhAnh))
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/products", doUong.HinhAnh);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            doUong.HinhAnh = fileName;
        }

        _context.DoUong.Update(doUong);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Cập nhật đồ uống thành công!" });
    }





}
