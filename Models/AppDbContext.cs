using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SP6.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BanBidum> BanBida { get; set; }

    public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }

    public virtual DbSet<DatCho> DatChos { get; set; }

    public virtual DbSet<DonDatMon> DonDatMons { get; set; }

    public virtual DbSet<HoaDon> HoaDons { get; set; }

    public virtual DbSet<KhungGio> KhungGios { get; set; }

    public virtual DbSet<LoaiBanBidum> LoaiBanBida { get; set; }

    public virtual DbSet<LoaiMon> LoaiMons { get; set; }

    public virtual DbSet<LoaiPhong> LoaiPhongs { get; set; }

    public virtual DbSet<MonAn> MonAns { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<PhongKaraoke> PhongKaraokes { get; set; }

    public virtual DbSet<VaiTro> VaiTros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=BIG-LULU\\SQLEXPRESS02;Database=DB_LittleFishStation;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BanBidum>(entity =>
        {
            entity.HasKey(e => e.MaBan).HasName("PK__Ban_Bida__03FFDF0DE9C326D4");

            entity.ToTable("Ban_Bida");

            entity.Property(e => e.MaBan).HasColumnName("ma_ban");
            entity.Property(e => e.MaLoaiBan).HasColumnName("ma_loai_ban");
            entity.Property(e => e.TenBan)
                .HasMaxLength(50)
                .HasColumnName("ten_ban");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Tr?ng")
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaLoaiBanNavigation).WithMany(p => p.BanBida)
                .HasForeignKey(d => d.MaLoaiBan)
                .HasConstraintName("FK__Ban_Bida__ma_loa__47DBAE45");
        });

        modelBuilder.Entity<ChiTietHoaDon>(entity =>
        {
            entity.HasKey(e => e.MaChiTiet).HasName("PK__Chi_Tiet__CD8DB51491BC1571");

            entity.ToTable("Chi_Tiet_Hoa_Don");

            entity.Property(e => e.MaChiTiet).HasColumnName("ma_chi_tiet");
            entity.Property(e => e.DonGia)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("don_gia");
            entity.Property(e => e.MaHoaDon).HasColumnName("ma_hoa_don");
            entity.Property(e => e.MoTa)
                .HasMaxLength(255)
                .HasColumnName("mo_ta");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");
            entity.Property(e => e.ThanhTien)
                .HasComputedColumnSql("([so_luong]*[don_gia])", true)
                .HasColumnType("decimal(21, 2)")
                .HasColumnName("thanh_tien");

            entity.HasOne(d => d.MaHoaDonNavigation).WithMany(p => p.ChiTietHoaDons)
                .HasForeignKey(d => d.MaHoaDon)
                .HasConstraintName("FK__Chi_Tiet___ma_ho__693CA210");
        });

        modelBuilder.Entity<DatCho>(entity =>
        {
            entity.HasKey(e => e.MaDatCho).HasName("PK__Dat_Cho__F08C9285773372B1");

            entity.ToTable("Dat_Cho");

            entity.Property(e => e.MaDatCho).HasColumnName("ma_dat_cho");
            entity.Property(e => e.DaDatCoc)
                .HasDefaultValue(false)
                .HasColumnName("da_dat_coc");
            entity.Property(e => e.DatCoc)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("dat_coc");
            entity.Property(e => e.LoaiDichVu)
                .HasMaxLength(20)
                .HasColumnName("loai_dich_vu");
            entity.Property(e => e.MaBan).HasColumnName("ma_ban");
            entity.Property(e => e.MaNguoiDung).HasColumnName("ma_nguoi_dung");
            entity.Property(e => e.MaPhong).HasColumnName("ma_phong");
            entity.Property(e => e.ThoiGianBatDau)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_bat_dau");
            entity.Property(e => e.ThoiGianKetThuc)
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_ket_thuc");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasDefaultValue("Ð?t tru?c")
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaBanNavigation).WithMany(p => p.DatChos)
                .HasForeignKey(d => d.MaBan)
                .HasConstraintName("FK__Dat_Cho__ma_ban__52593CB8");

            entity.HasOne(d => d.MaNguoiDungNavigation).WithMany(p => p.DatChos)
                .HasForeignKey(d => d.MaNguoiDung)
                .HasConstraintName("FK__Dat_Cho__ma_nguo__4F7CD00D");

            entity.HasOne(d => d.MaPhongNavigation).WithMany(p => p.DatChos)
                .HasForeignKey(d => d.MaPhong)
                .HasConstraintName("FK__Dat_Cho__ma_phon__5165187F");
        });

        modelBuilder.Entity<DonDatMon>(entity =>
        {
            entity.HasKey(e => e.MaDonMon).HasName("PK__Don_Dat___BADB62E6CBFB2702");

            entity.ToTable("Don_Dat_Mon");

            entity.Property(e => e.MaDonMon).HasColumnName("ma_don_mon");
            entity.Property(e => e.MaDatCho).HasColumnName("ma_dat_cho");
            entity.Property(e => e.MaMon).HasColumnName("ma_mon");
            entity.Property(e => e.MaNhanVien).HasColumnName("ma_nhan_vien");
            entity.Property(e => e.SoLuong).HasColumnName("so_luong");
            entity.Property(e => e.ThoiGianGoi)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_goi");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaDatChoNavigation).WithMany(p => p.DonDatMons)
                .HasForeignKey(d => d.MaDatCho)
                .HasConstraintName("FK__Don_Dat_M__ma_da__5EBF139D");

            entity.HasOne(d => d.MaMonNavigation).WithMany(p => p.DonDatMons)
                .HasForeignKey(d => d.MaMon)
                .HasConstraintName("FK__Don_Dat_M__ma_mo__5FB337D6");

            entity.HasOne(d => d.MaNhanVienNavigation).WithMany(p => p.DonDatMons)
                .HasForeignKey(d => d.MaNhanVien)
                .HasConstraintName("FK__Don_Dat_M__ma_nh__60A75C0F");
        });

        modelBuilder.Entity<HoaDon>(entity =>
        {
            entity.HasKey(e => e.MaHoaDon).HasName("PK__Hoa_Don__DBE2D9E3BFCBA87E");

            entity.ToTable("Hoa_Don");

            entity.Property(e => e.MaHoaDon).HasColumnName("ma_hoa_don");
            entity.Property(e => e.HinhThucThanhToan)
                .HasMaxLength(50)
                .HasColumnName("hinh_thuc_thanh_toan");
            entity.Property(e => e.MaDatCho).HasColumnName("ma_dat_cho");
            entity.Property(e => e.MaNhanVienThuNgan).HasColumnName("ma_nhan_vien_thu_ngan");
            entity.Property(e => e.ThoiGianTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("thoi_gian_tao");
            entity.Property(e => e.TongTien)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("tong_tien");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(50)
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaDatChoNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaDatCho)
                .HasConstraintName("FK__Hoa_Don__ma_dat___6477ECF3");

            entity.HasOne(d => d.MaNhanVienThuNganNavigation).WithMany(p => p.HoaDons)
                .HasForeignKey(d => d.MaNhanVienThuNgan)
                .HasConstraintName("FK__Hoa_Don__ma_nhan__656C112C");
        });

        modelBuilder.Entity<KhungGio>(entity =>
        {
            entity.HasKey(e => e.MaKhungGio).HasName("PK__Khung_Gi__B3CBD815E92E3144");

            entity.ToTable("Khung_Gio");

            entity.Property(e => e.MaKhungGio).HasColumnName("ma_khung_gio");
            entity.Property(e => e.GiaGio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gia_gio");
            entity.Property(e => e.GioBatDau).HasColumnName("gio_bat_dau");
            entity.Property(e => e.GioKetThuc).HasColumnName("gio_ket_thuc");
            entity.Property(e => e.LoaiDichVu)
                .HasMaxLength(20)
                .HasColumnName("loai_dich_vu");
            entity.Property(e => e.MaLoaiBan).HasColumnName("ma_loai_ban");
            entity.Property(e => e.MaLoaiPhong).HasColumnName("ma_loai_phong");

            entity.HasOne(d => d.MaLoaiBanNavigation).WithMany(p => p.KhungGios)
                .HasForeignKey(d => d.MaLoaiBan)
                .HasConstraintName("FK__Khung_Gio__ma_lo__4316F928");

            entity.HasOne(d => d.MaLoaiPhongNavigation).WithMany(p => p.KhungGios)
                .HasForeignKey(d => d.MaLoaiPhong)
                .HasConstraintName("FK__Khung_Gio__ma_lo__440B1D61");
        });

        modelBuilder.Entity<LoaiBanBidum>(entity =>
        {
            entity.HasKey(e => e.MaLoaiBan).HasName("PK__Loai_Ban__D42EBD15E2179132");

            entity.ToTable("Loai_Ban_Bida");

            entity.Property(e => e.MaLoaiBan).HasColumnName("ma_loai_ban");
            entity.Property(e => e.MoTa)
                .HasMaxLength(255)
                .HasColumnName("mo_ta");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .HasColumnName("ten_loai");
        });

        modelBuilder.Entity<LoaiMon>(entity =>
        {
            entity.HasKey(e => e.MaLoaiMon).HasName("PK__Loai_Mon__A3B18D95FFB8165B");

            entity.ToTable("Loai_Mon");

            entity.Property(e => e.MaLoaiMon).HasColumnName("ma_loai_mon");
            entity.Property(e => e.TenLoaiMon)
                .HasMaxLength(50)
                .HasColumnName("ten_loai_mon");
        });

        modelBuilder.Entity<LoaiPhong>(entity =>
        {
            entity.HasKey(e => e.MaLoaiPhong).HasName("PK__Loai_Pho__072698415EF45224");

            entity.ToTable("Loai_Phong");

            entity.Property(e => e.MaLoaiPhong).HasColumnName("ma_loai_phong");
            entity.Property(e => e.MoTa)
                .HasMaxLength(255)
                .HasColumnName("mo_ta");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(50)
                .HasColumnName("ten_loai");
        });

        modelBuilder.Entity<MonAn>(entity =>
        {
            entity.HasKey(e => e.MaMon).HasName("PK__Mon_An__0BA72B719E3DA16B");

            entity.ToTable("Mon_An");

            entity.Property(e => e.MaMon).HasColumnName("ma_mon");
            entity.Property(e => e.Gia)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("gia");
            entity.Property(e => e.MaLoaiMon).HasColumnName("ma_loai_mon");
            entity.Property(e => e.TenMon)
                .HasMaxLength(100)
                .HasColumnName("ten_mon");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaLoaiMonNavigation).WithMany(p => p.MonAns)
                .HasForeignKey(d => d.MaLoaiMon)
                .HasConstraintName("FK__Mon_An__ma_loai___5BE2A6F2");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoiDung).HasName("PK__Nguoi_Du__19C32CF76E4950DF");

            entity.ToTable("Nguoi_Dung");

            entity.HasIndex(e => e.Email, "UQ__Nguoi_Du__AB6E6164BD4A8CE6").IsUnique();

            entity.Property(e => e.MaNguoiDung).HasColumnName("ma_nguoi_dung");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("ho_ten");
            entity.Property(e => e.MaVaiTro).HasColumnName("ma_vai_tro");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .HasColumnName("mat_khau");
            entity.Property(e => e.NgaySinh).HasColumnName("ngay_sinh");
            entity.Property(e => e.NgayTao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("ngay_tao");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .HasColumnName("so_dien_thoai");

            entity.HasOne(d => d.MaVaiTroNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaVaiTro)
                .HasConstraintName("FK__Nguoi_Dun__ma_va__3A81B327");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNhanVien).HasName("PK__Nhan_Vie__6781B7B95C22B49C");

            entity.ToTable("Nhan_Vien");

            entity.Property(e => e.MaNhanVien).HasColumnName("ma_nhan_vien");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("ho_ten");
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(20)
                .HasColumnName("so_dien_thoai");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasColumnName("trang_thai");
            entity.Property(e => e.VaiTro)
                .HasMaxLength(50)
                .HasColumnName("vai_tro");
        });

        modelBuilder.Entity<PhongKaraoke>(entity =>
        {
            entity.HasKey(e => e.MaPhong).HasName("PK__Phong_Ka__1BD319C9EB543F5E");

            entity.ToTable("Phong_Karaoke");

            entity.Property(e => e.MaPhong).HasColumnName("ma_phong");
            entity.Property(e => e.MaLoaiPhong).HasColumnName("ma_loai_phong");
            entity.Property(e => e.TenPhong)
                .HasMaxLength(50)
                .HasColumnName("ten_phong");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .HasDefaultValue("Tr?ng")
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaLoaiPhongNavigation).WithMany(p => p.PhongKaraokes)
                .HasForeignKey(d => d.MaLoaiPhong)
                .HasConstraintName("FK__Phong_Kar__ma_lo__4BAC3F29");
        });

        modelBuilder.Entity<VaiTro>(entity =>
        {
            entity.HasKey(e => e.MaVaiTro).HasName("PK__Vai_Tro__4AE1754D17B745AF");

            entity.ToTable("Vai_Tro");

            entity.Property(e => e.MaVaiTro).HasColumnName("ma_vai_tro");
            entity.Property(e => e.TenVaiTro)
                .HasMaxLength(50)
                .HasColumnName("ten_vai_tro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
