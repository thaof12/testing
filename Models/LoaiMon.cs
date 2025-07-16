using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class LoaiMon
{
    public int MaLoaiMon { get; set; }

    public string? TenLoaiMon { get; set; }

    public virtual ICollection<MonAn> MonAns { get; set; } = new List<MonAn>();
}
