using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class BanBidum
{
    public int MaBan { get; set; }

    public string? TenBan { get; set; }

    public int? MaLoaiBan { get; set; }

    public string? TrangThai { get; set; }

    public virtual ICollection<DatCho> DatChos { get; set; } = new List<DatCho>();

    public virtual LoaiBanBidum? MaLoaiBanNavigation { get; set; }
}
