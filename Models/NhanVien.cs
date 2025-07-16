using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class NhanVien
{
    public int MaNhanVien { get; set; }

    public string? HoTen { get; set; }

    public string? SoDienThoai { get; set; }

    public string? Email { get; set; }

    public string? VaiTro { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<DonDatMon> DonDatMons { get; set; } = new List<DonDatMon>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
}
