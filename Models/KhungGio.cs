using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class KhungGio
{
    public int MaKhungGio { get; set; }

    public string LoaiDichVu { get; set; } = null!;

    public int? MaLoaiBan { get; set; }

    public int? MaLoaiPhong { get; set; }

    public TimeOnly GioBatDau { get; set; }

    public TimeOnly GioKetThuc { get; set; }

    public decimal GiaGio { get; set; }

    public virtual LoaiBanBidum? MaLoaiBanNavigation { get; set; }

    public virtual LoaiPhong? MaLoaiPhongNavigation { get; set; }
}
