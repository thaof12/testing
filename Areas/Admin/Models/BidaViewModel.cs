using System.Collections.Generic;
using System.Linq;
using SP6.Models;

namespace LittleFishStation.Areas.Admin.Models
{
    public class BidaViewModel
    {
        public List<BanBidum> BanBidaList { get; set; } = new();
        public List<LoaiBanBidum> LoaiBanBidaList { get; set; } = new();
        public int? SelectedLoaiBan { get; set; }
        public string SelectedTrangThai { get; set; }
        public string SortOrder { get; set; }
        public Dictionary<int, decimal> GiaMacDinhDict { get; set; } = new();

        // Constructor lấy dữ liệu từ DbContext
        public BidaViewModel(AppDbContext db)
        {
            BanBidaList = db.BanBida.ToList();
            LoaiBanBidaList = db.LoaiBanBida.ToList();
            // Lấy giá mặc định cho từng bàn theo khung giờ 8-16h
            GiaMacDinhDict = BanBidaList.ToDictionary(
                b => b.MaBan,
                b => db.KhungGios
                        .Where(k => k.MaLoaiBan == b.MaLoaiBan
                            && k.GioBatDau <= new TimeOnly(8,0)
                            && k.GioKetThuc >= new TimeOnly(16,0))
                        .OrderBy(k => k.GioBatDau)
                        .Select(k => k.GiaGio)
                        .FirstOrDefault()
            );
        }
    }
}

// ViewModel này KHÔNG truy vấn trực tiếp từ database.
// Bạn cần truy vấn dữ liệu ở Controller, sau đó gán vào ViewModel như sau:

/*
var viewModel = new BidaViewModel
{
    BanBidaList = dbContext.BanBidums.ToList(),
    LoaiBanBidaList = dbContext.LoaiBanBidums.ToList(),
    // Các thuộc tính khác...
};
return View(viewModel);
*/
