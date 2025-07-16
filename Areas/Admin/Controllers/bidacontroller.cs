using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LittleFishStation.Areas.Admin.Models;
using SP6.Models;

namespace LittleFishStation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BidaController : Controller
    {
        private readonly AppDbContext _db;
        public BidaController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Ban(int? maLoaiBan, string trangThai, string sortOrder)
        {
            var vm = new BidaViewModel(_db)
            {
                SelectedLoaiBan = maLoaiBan,
                SelectedTrangThai = trangThai,
                SortOrder = sortOrder
            };

            // Lọc dữ liệu theo bộ lọc
            var banList = vm.BanBidaList.AsQueryable();
            if (maLoaiBan.HasValue)
                banList = banList.Where(b => b.MaLoaiBan == maLoaiBan.Value);
            if (!string.IsNullOrEmpty(trangThai) && trangThai != "Tất cả")
                banList = banList.Where(b => b.TrangThai == trangThai);
            if (sortOrder == "desc")
                banList = banList.OrderByDescending(b => b.MaBan);
            else
                banList = banList.OrderBy(b => b.MaBan);

            ViewBag.LoaiBanBida = vm.LoaiBanBidaList;
            ViewBag.SelectedLoaiBan = vm.SelectedLoaiBan;
            ViewBag.SelectedTrangThai = vm.SelectedTrangThai;
            ViewBag.SortOrder = vm.SortOrder;
            ViewBag.GiaMacDinhDict = vm.GiaMacDinhDict;

            return View(banList.ToList());
        }

        public IActionResult Gia()
        {
            var vm = new BidaViewModel(_db);
            ViewBag.PriceList = vm.LoaiBanBidaList.Select(loai => new {
                loai.MaLoaiBan,
                loai.TenLoai,
                GiaGioThuong = _db.KhungGios
                    .Where(k => k.MaLoaiBan == loai.MaLoaiBan && k.LoaiDichVu == "Bida" && k.GioBatDau >= new TimeOnly(6,0) && k.GioKetThuc <= new TimeOnly(18,0))
                    .Select(k => k.GiaGio).FirstOrDefault(),
                MaKhungGioThuong = _db.KhungGios
                    .Where(k => k.MaLoaiBan == loai.MaLoaiBan && k.LoaiDichVu == "Bida" && k.GioBatDau >= new TimeOnly(6,0) && k.GioKetThuc <= new TimeOnly(18,0))
                    .Select(k => k.MaKhungGio).FirstOrDefault(),
                GiaGioDem = _db.KhungGios
                    .Where(k => k.MaLoaiBan == loai.MaLoaiBan && k.LoaiDichVu == "Bida" && (k.GioBatDau < new TimeOnly(6,0) || k.GioBatDau >= new TimeOnly(18,0)))
                    .Select(k => k.GiaGio).FirstOrDefault(),
                MaKhungGioDem = _db.KhungGios
                    .Where(k => k.MaLoaiBan == loai.MaLoaiBan && k.LoaiDichVu == "Bida" && (k.GioBatDau < new TimeOnly(6,0) || k.GioBatDau >= new TimeOnly(18,0)))
                    .Select(k => k.MaKhungGio).FirstOrDefault()
            }).ToList();
            return View();
        }

        public IActionResult LichSu()
        {
            // Tùy chỉnh truy vấn lịch sử sử dụng nếu cần
            return View();
        }

        public IActionResult TrangThai()
        {
            // Tùy chỉnh truy vấn trạng thái nếu cần
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Them(string TenBan, int? MaLoaiBan, string TenLoaiBan)
        {
            // Kiểm tra tên bàn đã tồn tại
            if (_db.BanBida.Any(b => b.TenBan == TenBan))
            {
                ModelState.AddModelError("TenBan", "Tên bàn đã tồn tại");
                return RedirectToAction("Ban");
            }

            int loaiBanId = 0;
            // Nếu chọn thêm loại bàn mới
            if (!string.IsNullOrEmpty(TenLoaiBan))
            {
                var loaiBan = new LoaiBanBidum
                {
                    TenLoai = TenLoaiBan
                };
                _db.LoaiBanBida.Add(loaiBan);
                _db.SaveChanges();
                loaiBanId = loaiBan.MaLoaiBan;
            }
            else if (MaLoaiBan.HasValue)
            {
                loaiBanId = MaLoaiBan.Value;
            }

            var ban = new BanBidum
            {
                TenBan = TenBan,
                MaLoaiBan = loaiBanId,
                TrangThai = "Hoạt động"
            };
            _db.BanBida.Add(ban);
            _db.SaveChanges();

            return RedirectToAction("Ban");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemGia(int MaLoaiBan, string KhungGio, decimal GiaGio, string GioBatDau = null, string GioKetThuc = null, bool? ConfirmOverwrite = null)
        {
            // Kiểm tra giá trị GiaGio hợp lệ
            if (GiaGio < 0 || GiaGio > 10000000)
            {
                ModelState.AddModelError("GiaGio", "Giá giờ phải từ 0 đến 10,000,000");
                return RedirectToAction("Gia");
            }

            string loaiDichVu = "Bida";
            TimeOnly gioBatDau, gioKetThuc;

            if (KhungGio == "Giờ thường (6h-18h)")
            {
                gioBatDau = new TimeOnly(6, 0);
                gioKetThuc = new TimeOnly(18, 0);
            }
            else if (KhungGio == "Giờ đêm (18h-6h)")
            {
                gioBatDau = new TimeOnly(18, 0);
                gioKetThuc = new TimeOnly(6, 0);
            }
            else if (KhungGio == "Khung giờ tùy chỉnh" && !string.IsNullOrEmpty(GioBatDau) && !string.IsNullOrEmpty(GioKetThuc))
            {
                gioBatDau = TimeOnly.Parse(GioBatDau);
                gioKetThuc = TimeOnly.Parse(GioKetThuc);
            }
            else
            {
                gioBatDau = new TimeOnly(0, 0);
                gioKetThuc = new TimeOnly(23, 59);
            }

            // Kiểm tra khung giờ đã tồn tại
            var existed = _db.KhungGios.FirstOrDefault(k =>
                k.MaLoaiBan == MaLoaiBan &&
                k.LoaiDichVu == loaiDichVu &&
                k.GioBatDau == gioBatDau &&
                k.GioKetThuc == gioKetThuc
            );

            if (existed != null && ConfirmOverwrite != true)
            {
                TempData["ConfirmOverwrite"] = true;
                TempData["MaLoaiBan"] = MaLoaiBan;
                TempData["KhungGio"] = KhungGio;
                TempData["GiaGio"] = GiaGio.ToString(); // Chuyển decimal sang string
                TempData["GioBatDau"] = GioBatDau;
                TempData["GioKetThuc"] = GioKetThuc;
                TempData["Message"] = "Giá cho khung giờ này đã tồn tại. Bạn có muốn ghi đè lên không?";
                return RedirectToAction("Gia");
            }

            if (existed != null)
            {
                existed.GiaGio = GiaGio;
                _db.SaveChanges();
            }
            else
            {
                var khungGio = new KhungGio
                {
                    MaLoaiBan = MaLoaiBan,
                    LoaiDichVu = loaiDichVu,
                    GioBatDau = gioBatDau,
                    GioKetThuc = gioKetThuc,
                    GiaGio = GiaGio
                };
                _db.KhungGios.Add(khungGio);
                _db.SaveChanges();
            }

            TempData["Message"] = "Cập nhật giá thành công!";
            return RedirectToAction("Gia");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sua(int MaBan, string TenBan, int MaLoaiBan, string TrangThai)
        {
            var ban = _db.BanBida.Include(b => b.MaLoaiBanNavigation).FirstOrDefault(b => b.MaBan == MaBan);
            if (ban == null)
            {
                return NotFound();
            }

            // Kiểm tra trùng tên bàn (trừ chính bàn đang sửa)
            if (_db.BanBida.Any(b => b.TenBan == TenBan && b.MaBan != MaBan))
            {
                ModelState.AddModelError("TenBan", "Tên bàn đã tồn tại");
                return RedirectToAction("Ban");
            }

            ban.TenBan = TenBan;
            ban.MaLoaiBan = MaLoaiBan;
            ban.TrangThai = TrangThai;
            _db.SaveChanges();

            return RedirectToAction("Ban");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Xoa(int id)
        {
            var ban = _db.BanBida.FirstOrDefault(b => b.MaBan == id);
            if (ban != null)
            {
                _db.BanBida.Remove(ban);
                _db.SaveChanges();
                TempData["Message"] = "Xóa bàn thành công!";
            }
            return RedirectToAction("Ban");
        }

        [HttpGet]
        public IActionResult DeleteGia(int id)
        {
            var khungGio = _db.KhungGios.FirstOrDefault(k => k.MaKhungGio == id);
            if (khungGio != null)
            {
                _db.KhungGios.Remove(khungGio);
                _db.SaveChanges();
                TempData["Message"] = "Xóa khung giá thành công!";
            }
            return RedirectToAction("Gia");
        }

        [HttpGet]
        public IActionResult DeleteLoai(int id)
        {
            // Xóa tất cả khung giờ liên quan đến loại bàn này
            var khungGios = _db.KhungGios.Where(k => k.MaLoaiBan == id).ToList();
            if (khungGios.Any())
            {
                _db.KhungGios.RemoveRange(khungGios);
            }

            // Xóa loại bàn
            var loaiBan = _db.LoaiBanBida.FirstOrDefault(l => l.MaLoaiBan == id);
            if (loaiBan != null)
            {
                _db.LoaiBanBida.Remove(loaiBan);
                _db.SaveChanges();
                TempData["Message"] = "Xóa loại bàn và các khung giờ liên quan thành công!";
            }
            return RedirectToAction("Gia");
        }
    }
}
