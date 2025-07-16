using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class MonAn
{
    public int MaMon { get; set; }

    public string? TenMon { get; set; }

    public decimal? Gia { get; set; }

    public int? MaLoaiMon { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<DonDatMon> DonDatMons { get; set; } = new List<DonDatMon>();

    public virtual LoaiMon? MaLoaiMonNavigation { get; set; }
}
