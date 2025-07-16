using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class DonDatMon
{
    public int MaDonMon { get; set; }

    public int? MaDatCho { get; set; }

    public int? MaMon { get; set; }

    public int? MaNhanVien { get; set; }

    public int? SoLuong { get; set; }

    public string? TrangThai { get; set; }

    public DateTime? ThoiGianGoi { get; set; }

    public virtual DatCho? MaDatChoNavigation { get; set; }

    public virtual MonAn? MaMonNavigation { get; set; }

    public virtual NhanVien? MaNhanVienNavigation { get; set; }
}
