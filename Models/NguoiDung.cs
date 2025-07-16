using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class NguoiDung
{
    public int MaNguoiDung { get; set; }

    public string Email { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? SoDienThoai { get; set; }

    public DateOnly? NgaySinh { get; set; }

    public int? MaVaiTro { get; set; }

    public DateTime? NgayTao { get; set; }

    public virtual ICollection<DatCho> DatChos { get; set; } = new List<DatCho>();

    public virtual VaiTro? MaVaiTroNavigation { get; set; }
}
