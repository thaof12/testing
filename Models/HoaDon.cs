using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class HoaDon
{
    public int MaHoaDon { get; set; }

    public int? MaDatCho { get; set; }

    public int? MaNhanVienThuNgan { get; set; }

    public DateTime? ThoiGianTao { get; set; }

    public decimal? TongTien { get; set; }

    public string? HinhThucThanhToan { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    public virtual DatCho? MaDatChoNavigation { get; set; }

    public virtual NhanVien? MaNhanVienThuNganNavigation { get; set; }
}
