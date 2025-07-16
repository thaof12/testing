using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class LoaiBanBidum
{
    public int MaLoaiBan { get; set; }

    public string? TenLoai { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<BanBidum> BanBida { get; set; } = new List<BanBidum>();

    public virtual ICollection<KhungGio> KhungGios { get; set; } = new List<KhungGio>();
}

