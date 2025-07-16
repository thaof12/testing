using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class LoaiPhong
{
    public int MaLoaiPhong { get; set; }

    public string? TenLoai { get; set; }

    public string? MoTa { get; set; }

    public virtual ICollection<KhungGio> KhungGios { get; set; } = new List<KhungGio>();

    public virtual ICollection<PhongKaraoke> PhongKaraokes { get; set; } = new List<PhongKaraoke>();
}
