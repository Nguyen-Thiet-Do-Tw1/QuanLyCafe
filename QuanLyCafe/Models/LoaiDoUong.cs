using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("LOAI_DO_UONG")]
public class LoaiDoUong
{
    [Key]
    [Column("MaLoai")]
    public string MaLoai { get; set; }

    [Column("TenLoai")]
    public string TenLoai { get; set; }

    // Danh sách đồ uống thuộc loại này
    public List<DoUong> DoUongs { get; set; }
}
