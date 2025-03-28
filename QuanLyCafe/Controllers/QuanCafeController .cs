using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCafe.Models;

[Route("QuanCafe")]
public class QuanCafeController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuanCafeController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("ChiTietCoSo/{id}")]
    public IActionResult ChiTietCoSo(string id)
    {
        var coSo = _context.QuanCafe.Find(id);
        if (coSo == null) return Json(new { error = "Không tìm thấy cơ sở" });
        return Json(coSo);
    }

    [HttpPost("ThemCoSo")]
    public async Task<IActionResult> ThemCoSo([FromBody] QuanCafe model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = "Dữ liệu không hợp lệ" });

        try
        {
            if (await _context.QuanCafe.AnyAsync(q => q.Id == model.Id))
                return BadRequest(new { error = "Mã cơ sở đã tồn tại" });

            _context.Add(model);
            await _context.SaveChangesAsync();

            // Trả về dữ liệu mới tạo kèm HTTP 201
            return CreatedAtAction(nameof(ChiTietCoSo), new { id = model.Id }, model);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Lỗi khi thêm: {ex.Message}" });
        }
    }

    [HttpPost("EditCoSo")]
    public async Task<IActionResult> EditCoSo([FromBody] QuanCafe model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { error = "Dữ liệu không hợp lệ" });

        try
        {
            // Tìm cơ sở cần chỉnh sửa theo Id gốc (không phải Id từ model)
            var existing = await _context.QuanCafe.FindAsync(model.Id);
            if (existing == null)
                return NotFound(new { error = "Không tìm thấy cơ sở" });

            // Kiểm tra nếu Id mới khác Id cũ và đã tồn tại
            if (existing.Id != model.Id && await _context.QuanCafe.AnyAsync(q => q.Id == model.Id))
                return BadRequest(new { error = "Mã cơ sở mới đã tồn tại" });

            // Cập nhật từng trường cụ thể
            existing.TenQuan = model.TenQuan;
            existing.DiaChi = model.DiaChi;
            existing.SoDienThoai = model.SoDienThoai;
            existing.Email = model.Email;

            await _context.SaveChangesAsync();

            // Trả về dữ liệu đã cập nhật
            return Ok(new { success = true, data = existing });
        }
        catch (Exception ex)
        {
            
            return StatusCode(500, new { error = $"Lỗi khi cập nhật: {ex.Message}" });
        }
    }

    [HttpPost("XoaCoSo/{id}")]
    public async Task<IActionResult> XoaCoSo(string id)
    {
        try
        {
            var coSo = await _context.QuanCafe
                .Include(q => q.NhanViens)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (coSo == null)
                return NotFound(new { error = "Không tìm thấy cơ sở" });

            // Kiểm tra tất cả các quan hệ
            var hasRelations = await _context.NguyenLieu.AnyAsync(n => n.MaQuan == id)
                || await _context.PhieuNhapHang.AnyAsync(p => p.MaQuan == id);
            // || await _context.HoaDon.AnyAsync(h => h.MaQuan == id);

            if (hasRelations)
                return BadRequest(new { error = "Không thể xóa do tồn tại dữ liệu liên quan" });

            _context.QuanCafe.Remove(coSo);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = $"Lỗi khi xóa: {ex.Message}" });
        }
    }

}