using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class PhongKaraoke
{
    public int MaPhong { get; set; }

    public string? TenPhong { get; set; }

    public int? MaLoaiPhong { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<DatCho> DatChos { get; set; } = new List<DatCho>();

    public virtual LoaiPhong? MaLoaiPhongNavigation { get; set; }
}
