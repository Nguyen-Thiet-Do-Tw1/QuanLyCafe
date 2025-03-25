using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyCafe.Models
{
    [Table("QUAN_CAFE")]
    public class QuanCafe
    {
        [Key]
        [Column("MaQuan")]
        public string Id { get; set; }

        [Required]
        [Column("TenQuan")]
        public string TenQuan { get; set; }

        [Required]
        [Column("DiaChi")]
        public string DiaChi { get; set; }

        [Required]
        [Column("SDT")]
        [StringLength(20)]
        public string SoDienThoai { get; set; }

        [Column("Email")]
        public string Email { get; set; }

        // Quan hệ một quán cafe có nhiều nhân viên
        public virtual List<NhanVien> NhanViens { get; set; }
    }
}
