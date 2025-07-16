using System;
using System.Collections.Generic;

namespace SP6.Models;

public partial class DatCho
{
    public int MaDatCho { get; set; }

    public int? MaNguoiDung { get; set; }

    public string? LoaiDichVu { get; set; }

    public int? MaPhong { get; set; }

    public int? MaBan { get; set; }

    public DateTime ThoiGianBatDau { get; set; }

    public DateTime ThoiGianKetThuc { get; set; }

    public string? TrangThai { get; set; }

    public decimal? DatCoc { get; set; }

    public bool? DaDatCoc { get; set; }

    public virtual ICollection<DonDatMon> DonDatMons { get; set; } = new List<DonDatMon>();

    public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();

    public virtual BanBidum? MaBanNavigation { get; set; }

    public virtual NguoiDung? MaNguoiDungNavigation { get; set; }

    public virtual PhongKaraoke? MaPhongNavigation { get; set; }
}
