using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class IntegrationMiddleWayContext : DbContext
    {
        public virtual DbSet<_ETL_Details> _ETL_Details { get; set; }
        public virtual DbSet<_ETL_Headers> _ETL_Headers { get; set; }
        public virtual DbSet<_ETL_Inventory> _ETL_Inventory { get; set; }
        public virtual DbSet<_ETL_Products> _ETL_Products { get; set; }
        public virtual DbSet<_ETL_Shipments> _ETL_Shipments { get; set; }
        public virtual DbSet<Configurations> Configurations { get; set; }
        public virtual DbSet<InventoryFlatData> FlatData { get; set; }
        public virtual DbSet<Mappings> Mappings { get; set; }
        public virtual DbSet<ProcessErrors> ProcessErrors { get; set; }
        public virtual DbSet<Processes> Processes { get; set; }
        public virtual DbSet<ProductsFlatData> ProductsFlatData { get; set; }
        public virtual DbSet<PurchaseInvoiceFlatData> PurchaseInvoiceFlatData { get; set; }
        public virtual DbSet<PurchaseOrderDetailShipmentFlatData> PurchaseOrderDetailShipmentFlatData { get; set; }
        public virtual DbSet<PurchaseOrderFlatData> PurchaseOrderFlatData { get; set; }
        public virtual DbSet<PurchaseOrderShellFlatData> PurchaseOrderShellFlatData { get; set; }
        public virtual DbSet<PurchaseShipmentFlatData> PurchaseShipmentFlatData { get; set; }
        public virtual DbSet<Transformations> Transformations { get; set; }

        public IntegrationMiddleWayContext(DbContextOptions<IntegrationMiddleWayContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<_ETL_Details>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<_ETL_Headers>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<_ETL_Inventory>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<_ETL_Products>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<_ETL_Shipments>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Configurations>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<InventoryFlatData>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Mappings>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<ProcessErrors>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Processes>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<ProductsFlatData>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<PurchaseInvoiceFlatData>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<PurchaseOrderDetailShipmentFlatData>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<PurchaseOrderFlatData>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<PurchaseOrderShellFlatData>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<PurchaseShipmentFlatData>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });

            modelBuilder.Entity<Transformations>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_");

                //entity.Property(e => e.)
                //    .HasColumnName("")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.)
                //    .HasColumnType("")
                //    .HasDefaultValueSql("");

                //entity.Property(e => e.).HasDefaultValueSql("1");
            });
        }
    }
}
