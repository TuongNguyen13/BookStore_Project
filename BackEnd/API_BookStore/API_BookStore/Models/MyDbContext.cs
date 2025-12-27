using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_BookStore.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptDetail> ReceiptDetails { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-OMLIN6S;Database=Book_Manage;User Id=sa;Password=123;TrustServerCertificate=True;MultipleActiveResultSets=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserCode).HasName("PK__Account__1DF52D0D5F8C90A4");

            entity.HasOne(d => d.EmployeeCodeNavigation).WithOne(p => p.Account)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Account_Employee");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerCode).HasName("PK__Customer__06678520DF5C85C4");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeCode).HasName("PK__Employee__1F6425490FB26F33");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderCode).HasName("PK__Orders__999B5228FCA19CA7");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Payment).HasDefaultValue("Tiền mặt");

            entity.HasOne(d => d.CustomerCodeNavigation).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Order_Customer");

            entity.HasOne(d => d.EmployeeCodeNavigation).WithMany(p => p.Orders).HasConstraintName("fk_Order_Employee");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderCode, e.ProductCode }).HasName("PK__OrderDet__6B6FB20CB5B43B55");

            entity.HasOne(d => d.OrderCodeNavigation).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderDetail_Orders");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_OrderDetail_Products");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductCode).HasName("PK__Products__2F4E024E8C4D7EF8");

            entity.Property(e => e.ProductType).HasDefaultValue("Khác");
            entity.Property(e => e.StockQuantity).HasDefaultValue(0);
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.HasKey(e => e.ReceiptCode).HasName("PK__Receipt__1AB76D019B242E68");

            entity.Property(e => e.DateCreated).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.EmployeeCodeNavigation).WithMany(p => p.Receipts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Receipt_Employee");

            entity.HasOne(d => d.SupplierCodeNavigation).WithMany(p => p.Receipts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Receipt_Supplier");
        });

        modelBuilder.Entity<ReceiptDetail>(entity =>
        {
            entity.HasKey(e => new { e.ReceiptCode, e.ProductCode }).HasName("PK__ReceiptD__E8438D25BC375CD7");

            entity.HasOne(d => d.ProductCodeNavigation).WithMany(p => p.ReceiptDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ReceiptDetail_Product");

            entity.HasOne(d => d.ReceiptCodeNavigation).WithMany(p => p.ReceiptDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ReceiptDetail_Receipt");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierCode).HasName("PK__Supplier__44BE981A28544C55");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
