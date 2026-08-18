using System;
using System.Collections.Generic;
using Digitial_Village_Api.Domain.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Digitial_Village_Api.Domain.Persistence;

public partial class VillageDbContext : DbContext
{
    public VillageDbContext()
    {
    }

    public VillageDbContext(DbContextOptions<VillageDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ViDistrict> ViDistricts { get; set; }

    public virtual DbSet<ViOrder> ViOrders { get; set; }

    public virtual DbSet<ViOrderDetail> ViOrderDetails { get; set; }

    public virtual DbSet<ViProduct> ViProducts { get; set; }

    public virtual DbSet<ViProductCategory> ViProductCategories { get; set; }

    public virtual DbSet<ViRegistration> ViRegistrations { get; set; }

    public virtual DbSet<ViState> ViStates { get; set; }

    public virtual DbSet<ViSubDistrict> ViSubDistricts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=172.16.10.30,1435;Database=MT_Training;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ViDistrict>(entity =>
        {
            entity.HasKey(e => e.DistrictId).HasName("PK__Vi_Distr__85FDA4C6F3E85D82");

            entity.ToTable("Vi_Districts");

            entity.Property(e => e.DistrictName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.State).WithMany(p => p.ViDistricts)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vi_Districts_States");
        });

        modelBuilder.Entity<ViOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Vi_Order__C3905BCF9BD09BFB");

            entity.ToTable("Vi_Orders");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Registration).WithMany(p => p.ViOrders)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vi_Orders_Registration");
        });

        modelBuilder.Entity<ViOrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__Vi_Order__D3B9D36C0D5EF26A");

            entity.ToTable("Vi_OrderDetails");

            entity.Property(e => e.DiscountAmount)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Order).WithMany(p => p.ViOrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vi_OrderDetails_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.ViOrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vi_OrderDetails_Product");
        });

        modelBuilder.Entity<ViProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Vi_Produ__B40CC6CDFA1913E8");

            entity.ToTable("Vi_Products");

            entity.Property(e => e.IsActive).HasDefaultValue(true, "DF_Vi_Products_IsActive");
            entity.Property(e => e.ProductImageUrl).HasMaxLength(500);
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(10, 3)");
            entity.Property(e => e.ProductUnit)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ProductUnitValue).HasColumnType("decimal(10, 3)");

            entity.HasOne(d => d.ProductCategoryNavigation).WithMany(p => p.ViProducts)
                .HasForeignKey(d => d.ProductCategory)
                .HasConstraintName("FK_Vi_Products_ProductCategory");

            entity.HasOne(d => d.Registration).WithMany(p => p.ViProducts)
                .HasForeignKey(d => d.RegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vi_Products_Registration");
        });

        modelBuilder.Entity<ViProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Vi_Produ__19093A0B4ADBFEB8");

            entity.ToTable("Vi_ProductCategories");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
        });

        modelBuilder.Entity<ViRegistration>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__VI_Regis__6EF588100B16D023");

            entity.ToTable("VI_Registration");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.ConfirmPassword).HasMaxLength(255);
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasDefaultValue("India");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.District).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Mobile).HasMaxLength(15);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Pincode).HasMaxLength(10);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.ShopGovtRegistrationId).HasMaxLength(100);
            entity.Property(e => e.ShopImage).HasMaxLength(500);
            entity.Property(e => e.ShopName).HasMaxLength(200);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.Subdistrict).HasMaxLength(100);
            entity.Property(e => e.VillageName).HasMaxLength(150);
        });

        modelBuilder.Entity<ViState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PK__Vi_State__C3BA3B3A900FCD70");

            entity.ToTable("Vi_States");

            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViSubDistrict>(entity =>
        {
            entity.HasKey(e => e.SubDistrictId).HasName("PK__Vi_SubDi__47CF01791CF7EB5D");

            entity.ToTable("Vi_SubDistricts");

            entity.Property(e => e.SubDistrictName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.District).WithMany(p => p.ViSubDistricts)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Vi_SubDistricts_Districts");
        });
        modelBuilder.HasSequence("SampleSequence").HasMin(1L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
