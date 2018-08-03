using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public partial class IntegrationMiddleWayContext : DbContext
    {
        private string _connectionString;

        public IntegrationMiddleWayContext() : base()
        {
            _connectionString = string.Empty;
        }
        public IntegrationMiddleWayContext(string connectionString) : base()
        {
            _connectionString = connectionString;
        }

        public IntegrationMiddleWayContext(DbContextOptions<IntegrationMiddleWayContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (!string.IsNullOrEmpty(this._connectionString))
                {
                    optionsBuilder.UseSqlServer(this._connectionString);
                }
                else
                {
                    //    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                    //    optionsBuilder.UseSqlServer(@"Server=.;Database=HelpDesk;Trusted_Connection=True;");
                    throw new ArgumentNullException("ConnectionString", "The Connection string is null or empty");
                }
            }
        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer("Server=.;Database=IntegrationMiddleWay;Trusted_Connection=True;");
        //            }
        //        }

        public virtual DbSet<Configurations> Configurations { get; set; }
        public virtual DbSet<EtlDetails> EtlDetails { get; set; }
        public virtual DbSet<EtlHeaders> EtlHeaders { get; set; }
        public virtual DbSet<EtlInventory> EtlInventory { get; set; }
        public virtual DbSet<EtlProducts> EtlProducts { get; set; }
        public virtual DbSet<EtlRawFile> EtlRawFile { get; set; }
        public virtual DbSet<EtlShipments> EtlShipments { get; set; }
        public virtual DbSet<InventoryFlatData> InventoryFlatData { get; set; }
        public virtual DbSet<Mappings> Mappings { get; set; }
        public virtual DbSet<Processes> Processes { get; set; }
        public virtual DbSet<ProcessSource> ProcessSource { get; set; }
        public virtual DbSet<ProcessTasks> ProcessTasks { get; set; }
        public virtual DbSet<ProcessTasksErrors> ProcessTasksErrors { get; set; }
        public virtual DbSet<ProcessTaskSteps> ProcessTaskSteps { get; set; }
        public virtual DbSet<ProductsFlatData> ProductsFlatData { get; set; }
        public virtual DbSet<PurchaseInvoiceFlatData> PurchaseInvoiceFlatData { get; set; }
        public virtual DbSet<PurchaseOrderDetailShipmentFlatData> PurchaseOrderDetailShipmentFlatData { get; set; }
        public virtual DbSet<PurchaseOrderFlatData> PurchaseOrderFlatData { get; set; }
        public virtual DbSet<PurchaseOrderShellFlatData> PurchaseOrderShellFlatData { get; set; }
        public virtual DbSet<PurchaseShipmentFlatData> PurchaseShipmentFlatData { get; set; }
        public virtual DbSet<TransformationLookup> TransformationLookup { get; set; }
        public virtual DbSet<Transformations> Transformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Configurations>(entity =>
            {
                entity.HasKey(e => e.ConfigurationUid);

                entity.Property(e => e.ConfigurationName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ConfigurationValue)
                    .IsRequired()
                    .IsUnicode(false);

                entity.HasOne(d => d.ProcessU)
                    .WithMany(p => p.Configurations)
                    .HasForeignKey(d => d.ProcessUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Configurations_Processes");
            });

            modelBuilder.Entity<EtlDetails>(entity =>
            {
                entity.HasKey(e => e.EtlDetailUid);

                entity.ToTable("_ETL_Details");

                entity.Property(e => e.EtlDetailUid).HasColumnName("_ETL_DetailUID");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Cfda)
                    .HasColumnName("CFDA")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.SiteAddedSiteId)
                    .HasColumnName("SiteAddedSiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteUid).HasColumnName("SiteAddedSiteUID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.EtlDetails)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ETL_Details_ProcessTasks");
            });

            modelBuilder.Entity<EtlHeaders>(entity =>
            {
                entity.HasKey(e => e.EtlHeaderUid);

                entity.ToTable("_ETL_Headers");

                entity.Property(e => e.EtlHeaderUid).HasColumnName("_ETL_HeaderUID");

                entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.Frn)
                    .HasColumnName("FRN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Other1)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.VendorAccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VendorUid).HasColumnName("VendorUID");

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.EtlHeaders)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ETL_Headers_ProcessTasks");
            });

            modelBuilder.Entity<EtlInventory>(entity =>
            {
                entity.HasKey(e => e.EtlInventoryUid);

                entity.ToTable("_ETL_Inventory");

                entity.Property(e => e.EtlInventoryUid).HasColumnName("_ETL_InventoryUID");

                entity.Property(e => e.AccountCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaUid).HasColumnName("AreaUID");

                entity.Property(e => e.AssetId)
                    .HasColumnName("AssetID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ContainerUid).HasColumnName("ContainerUID");

                entity.Property(e => e.CustomField1Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField1Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField2Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField2Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField3Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField3Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField4Label)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField4Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntityId)
                    .IsRequired()
                    .HasColumnName("EntityID")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EntityTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EntityTypeUid).HasColumnName("EntityTypeUID");

                entity.Property(e => e.EntityUid).HasColumnName("EntityUID");

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.FundingSource)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FundingSourceUid).HasColumnName("FundingSourceUID");

                entity.Property(e => e.InventoryExt1Uid).HasColumnName("InventoryExt1UID");

                entity.Property(e => e.InventoryExt2Uid).HasColumnName("InventoryExt2UID");

                entity.Property(e => e.InventoryExt3Uid).HasColumnName("InventoryExt3UID");

                entity.Property(e => e.InventoryExt4Uid).HasColumnName("InventoryExt4UID");

                entity.Property(e => e.InventoryMeta1Uid).HasColumnName("InventoryMeta1UID");

                entity.Property(e => e.InventoryMeta2Uid).HasColumnName("InventoryMeta2UID");

                entity.Property(e => e.InventoryMeta3Uid).HasColumnName("InventoryMeta3UID");

                entity.Property(e => e.InventoryMeta4Uid).HasColumnName("InventoryMeta4UID");

                entity.Property(e => e.InventoryNotes)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.InventorySourceName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InventorySourceUid).HasColumnName("InventorySourceUID");

                entity.Property(e => e.InventoryTypeName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.InventoryTypeUid).HasColumnName("InventoryTypeUID");

                entity.Property(e => e.InventoryUid).HasColumnName("InventoryUID");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.ManufacturerName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerUid).HasColumnName("ManufacturerUID");

                entity.Property(e => e.ModelNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentInventoryUid).HasColumnName("ParentInventoryUID");

                entity.Property(e => e.ParentTag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductByNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseDate).HasColumnType("datetime");

                entity.Property(e => e.PurchaseInventoryUid).HasColumnName("PurchaseInventoryUID");

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.Property(e => e.PurchasePrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PurchaseSiteId)
                    .HasColumnName("PurchaseSiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseSiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseSiteUid).HasColumnName("PurchaseSiteUID");

                entity.Property(e => e.PurchaseUid).HasColumnName("PurchaseUID");

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.Serial)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteId)
                    .HasColumnName("ShippedToSiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteUid).HasColumnName("ShippedToSiteUID");

                entity.Property(e => e.SiteAddedSiteId)
                    .HasColumnName("SiteAddedSiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteUid).HasColumnName("SiteAddedSiteUID");

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SiteUid).HasColumnName("SiteUID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TechDepartmentUid).HasColumnName("TechDepartmentUID");

                entity.Property(e => e.VendorAccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.VendorName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.VendorUid).HasColumnName("VendorUID");

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.EtlInventory)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ETL_Inventory_ProcessTasks");
            });

            modelBuilder.Entity<EtlProducts>(entity =>
            {
                entity.HasKey(e => e.EtlProductsUid);

                entity.ToTable("_ETL_Products");

                entity.Property(e => e.EtlProductsUid).HasColumnName("_ETL_ProductsUID");

                entity.Property(e => e.AreaName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AreaUid).HasColumnName("AreaUID");

                entity.Property(e => e.CustomField1)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField2)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.CustomField3)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ItemNotes)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.ItemTypeUid).HasColumnName("ItemTypeUID");

                entity.Property(e => e.ItemUid).HasColumnName("ItemUID");

                entity.Property(e => e.ManufacturerName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ManufacturerUid).HasColumnName("ManufacturerUID");

                entity.Property(e => e.ModelNumber)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductDescription)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProductNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ProductTypeName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SuggestedPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.EtlProducts)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ETL_Products_ProcessTasks");
            });

            modelBuilder.Entity<EtlRawFile>(entity =>
            {
                entity.HasKey(e => e.RowId);

                entity.ToTable("_ETL_RawFile");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.RawData).IsUnicode(false);

                entity.Property(e => e.RawDataModified).IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.EtlRawFile)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ETL_RawFile_ProcessTasks");
            });

            modelBuilder.Entity<EtlShipments>(entity =>
            {
                entity.HasKey(e => e.EtlShipmentsUid);

                entity.ToTable("_ETL_Shipments");

                entity.Property(e => e.EtlShipmentsUid).HasColumnName("_ETL_ShipmentsUID");

                entity.Property(e => e.InvoiceDate).HasColumnType("date");

                entity.Property(e => e.InvoiceNumber)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OrderNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnName("PurchaseItemDetailUID");

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnName("PurchaseItemShipmentUID");

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.ShippedToSiteId)
                    .HasColumnName("ShippedToSiteID")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteUid).HasColumnName("ShippedToSiteUID");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TicketedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TicketedByUserId).HasColumnName("TicketedByUserID");

                entity.Property(e => e.TicketedDate).HasColumnType("datetime");

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.EtlShipments)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ETL_Shipments_ProcessTasks");
            });

            modelBuilder.Entity<InventoryFlatData>(entity =>
            {
                entity.HasKey(e => e.InventoryFlatDataUid);

                entity.Property(e => e.InventoryFlatDataUid).HasColumnName("InventoryFlatDataUID");

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.AreaName).IsUnicode(false);

                entity.Property(e => e.AssetId)
                    .HasColumnName("AssetID")
                    .IsUnicode(false);

                entity.Property(e => e.ContainerNumber).IsUnicode(false);

                entity.Property(e => e.CustomField1Label).IsUnicode(false);

                entity.Property(e => e.CustomField1Value).IsUnicode(false);

                entity.Property(e => e.CustomField2Label).IsUnicode(false);

                entity.Property(e => e.CustomField2Value).IsUnicode(false);

                entity.Property(e => e.CustomField3Label).IsUnicode(false);

                entity.Property(e => e.CustomField3Value).IsUnicode(false);

                entity.Property(e => e.CustomField4Label).IsUnicode(false);

                entity.Property(e => e.CustomField4Value).IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName).IsUnicode(false);

                entity.Property(e => e.ExpirationDate).IsUnicode(false);

                entity.Property(e => e.FundingSource).IsUnicode(false);

                entity.Property(e => e.FundingSourceDescription).IsUnicode(false);

                entity.Property(e => e.InventoryNotes).IsUnicode(false);

                entity.Property(e => e.InvoiceDate).IsUnicode(false);

                entity.Property(e => e.InvoiceNumber).IsUnicode(false);

                entity.Property(e => e.LineNumber).IsUnicode(false);

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .IsUnicode(false);

                entity.Property(e => e.LocationName).IsUnicode(false);

                entity.Property(e => e.LocationTypeName).IsUnicode(false);

                entity.Property(e => e.ManufacturerName).IsUnicode(false);

                entity.Property(e => e.ModelNumber).IsUnicode(false);

                entity.Property(e => e.OrderNumber).IsUnicode(false);

                entity.Property(e => e.ParentTag).IsUnicode(false);

                entity.Property(e => e.ProductByNumber).IsUnicode(false);

                entity.Property(e => e.ProductDescription).IsUnicode(false);

                entity.Property(e => e.ProductName).IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription).IsUnicode(false);

                entity.Property(e => e.ProductTypeName).IsUnicode(false);

                entity.Property(e => e.PurchaseDate).IsUnicode(false);

                entity.Property(e => e.PurchasePrice).IsUnicode(false);

                entity.Property(e => e.PurchaseSiteId)
                    .HasColumnName("PurchaseSiteID")
                    .IsUnicode(false);

                entity.Property(e => e.PurchaseSiteName).IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.Serial).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteId)
                    .HasColumnName("ShippedToSiteID")
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteName).IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteId)
                    .HasColumnName("SiteAddedSiteID")
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteName).IsUnicode(false);

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteID")
                    .IsUnicode(false);

                entity.Property(e => e.SiteName).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.Tag).IsUnicode(false);

                entity.Property(e => e.VendorAccountNumber).IsUnicode(false);

                entity.Property(e => e.VendorName).IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.InventoryFlatData)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryFlatData_ProcessTasks");
            });

            modelBuilder.Entity<Mappings>(entity =>
            {
                entity.HasKey(e => e.MappingsUid);

                entity.Property(e => e.DestinationColumn)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.SourceColumn)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProcessU)
                    .WithMany(p => p.Mappings)
                    .HasForeignKey(d => d.ProcessUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Mappings_Processes");
            });

            modelBuilder.Entity<Processes>(entity =>
            {
                entity.HasKey(e => e.ProcessUid);

                entity.Property(e => e.Client)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProcessSource>(entity =>
            {
                entity.HasKey(e => e.ProcessSourceUid);

                entity.Property(e => e.ProcessSourceUid).ValueGeneratedNever();

                entity.Property(e => e.ProcessSourceDescription)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessSourceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessSourceTable)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProcessTasks>(entity =>
            {
                entity.HasKey(e => e.ProcessTaskUid);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Parameters)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.ProcessU)
                    .WithMany(p => p.ProcessTasks)
                    .HasForeignKey(d => d.ProcessUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessTasks_Processes");
            });

            modelBuilder.Entity<ProcessTasksErrors>(entity =>
            {
                entity.HasKey(e => e.ProcessTaskErrorUid);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ErrorDescription)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ErrorField)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.ProcessTasksErrors)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessTasksErrors_Processes");
            });

            modelBuilder.Entity<ProcessTaskSteps>(entity =>
            {
                entity.HasKey(e => e.ProcessTaskStepsUid);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.ProcessTaskSteps)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProcessTaskSteps_ProcessTasks");
            });

            modelBuilder.Entity<ProductsFlatData>(entity =>
            {
                entity.HasKey(e => e.ProductsFlatDataUid);

                entity.Property(e => e.ProductsFlatDataUid).HasColumnName("ProductsFlatDataUID");

                entity.Property(e => e.AllowUntagged).IsUnicode(false);

                entity.Property(e => e.AreaName).IsUnicode(false);

                entity.Property(e => e.ManufacturerName).IsUnicode(false);

                entity.Property(e => e.ModelNumber).IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.OtherField1).IsUnicode(false);

                entity.Property(e => e.OtherField2).IsUnicode(false);

                entity.Property(e => e.OtherField3).IsUnicode(false);

                entity.Property(e => e.ProductDescription).IsUnicode(false);

                entity.Property(e => e.ProductName).IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription).IsUnicode(false);

                entity.Property(e => e.ProductTypeName).IsUnicode(false);

                entity.Property(e => e.ProjectedLife).IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.SerialRequired).IsUnicode(false);

                entity.Property(e => e.Sku)
                    .HasColumnName("SKU")
                    .IsUnicode(false);

                entity.Property(e => e.SuggestedPrice).IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.ProductsFlatData)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductsFlatData_ProcessTasks");
            });

            modelBuilder.Entity<PurchaseInvoiceFlatData>(entity =>
            {
                entity.HasKey(e => e.PurchaseInvoiceFlatDataUid);

                entity.Property(e => e.PurchaseInvoiceFlatDataUid).HasColumnName("PurchaseInvoiceFlatDataUID");

                entity.Property(e => e.AccountingDate).IsUnicode(false);

                entity.Property(e => e.AssetPrice).IsUnicode(false);

                entity.Property(e => e.AuthorizationStatus).IsUnicode(false);

                entity.Property(e => e.InvoiceDate).IsUnicode(false);

                entity.Property(e => e.InvoiceNumber).IsUnicode(false);

                entity.Property(e => e.InvoicePrice).IsUnicode(false);

                entity.Property(e => e.InvoiceStatus).IsUnicode(false);

                entity.Property(e => e.LineAmount).IsUnicode(false);

                entity.Property(e => e.LineDescription).IsUnicode(false);

                entity.Property(e => e.LineNumber).IsUnicode(false);

                entity.Property(e => e.OrderNumber).IsUnicode(false);

                entity.Property(e => e.Quantity).IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.PurchaseInvoiceFlatData)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseInvoiceFlatData_ProcessTasks");
            });

            modelBuilder.Entity<PurchaseOrderDetailShipmentFlatData>(entity =>
            {
                entity.HasKey(e => e.PurchaseOrderDetailShipmentFlatDataUid);

                entity.Property(e => e.PurchaseOrderDetailShipmentFlatDataUid).HasColumnName("PurchaseOrderDetailShipmentFlatDataUID");

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.Cfda)
                    .HasColumnName("CFDA")
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName).IsUnicode(false);

                entity.Property(e => e.FundingSource).IsUnicode(false);

                entity.Property(e => e.FundingSourceDescription).IsUnicode(false);

                entity.Property(e => e.InvoiceDate).IsUnicode(false);

                entity.Property(e => e.InvoiceNumber).IsUnicode(false);

                entity.Property(e => e.IsAssociated).IsUnicode(false);

                entity.Property(e => e.LineNumber).IsUnicode(false);

                entity.Property(e => e.OrderNumber).IsUnicode(false);

                entity.Property(e => e.ProductDescription).IsUnicode(false);

                entity.Property(e => e.ProductName).IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription).IsUnicode(false);

                entity.Property(e => e.ProductTypeName).IsUnicode(false);

                entity.Property(e => e.PurchaseDate).IsUnicode(false);

                entity.Property(e => e.PurchasePrice).IsUnicode(false);

                entity.Property(e => e.QuantityOrdered).IsUnicode(false);

                entity.Property(e => e.QuantityReceived).IsUnicode(false);

                entity.Property(e => e.QuantityShipped).IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.ShippedToSiteAddress).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteCity).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteId)
                    .HasColumnName("ShippedToSiteID")
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteName).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteState).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteZip).IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteId)
                    .HasColumnName("SiteAddedSiteID")
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteName).IsUnicode(false);

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteID")
                    .IsUnicode(false);

                entity.Property(e => e.SiteName).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.TicketNumber).IsUnicode(false);

                entity.Property(e => e.TicketedBy).IsUnicode(false);

                entity.Property(e => e.TicketedDate).IsUnicode(false);

                entity.Property(e => e.VendorAccountNumber).IsUnicode(false);

                entity.Property(e => e.VendorName).IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.PurchaseOrderDetailShipmentFlatData)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrderDetailShipmentFlatData_ProcessTasks");
            });

            modelBuilder.Entity<PurchaseOrderFlatData>(entity =>
            {
                entity.HasKey(e => e.PurchaseOrderFlatDataUid);

                entity.Property(e => e.PurchaseOrderFlatDataUid).HasColumnName("PurchaseOrderFlatDataUID");

                entity.Property(e => e.AccountCode).IsUnicode(false);

                entity.Property(e => e.Cfda)
                    .HasColumnName("CFDA")
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName).IsUnicode(false);

                entity.Property(e => e.FundingSource).IsUnicode(false);

                entity.Property(e => e.FundingSourceDescription).IsUnicode(false);

                entity.Property(e => e.IsAssociated).IsUnicode(false);

                entity.Property(e => e.LineNumber).IsUnicode(false);

                entity.Property(e => e.OrderNumber).IsUnicode(false);

                entity.Property(e => e.ProductDescription).IsUnicode(false);

                entity.Property(e => e.ProductName).IsUnicode(false);

                entity.Property(e => e.ProductTypeDescription).IsUnicode(false);

                entity.Property(e => e.ProductTypeName).IsUnicode(false);

                entity.Property(e => e.PurchaseDate).IsUnicode(false);

                entity.Property(e => e.PurchasePrice).IsUnicode(false);

                entity.Property(e => e.QuantityOrdered).IsUnicode(false);

                entity.Property(e => e.QuantityReceived).IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.SiteAddedSiteId)
                    .HasColumnName("SiteAddedSiteID")
                    .IsUnicode(false);

                entity.Property(e => e.SiteAddedSiteName).IsUnicode(false);

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteID")
                    .IsUnicode(false);

                entity.Property(e => e.SiteName).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.VendorAccountNumber).IsUnicode(false);

                entity.Property(e => e.VendorName).IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.PurchaseOrderFlatData)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrderFlatData_ProcessTasks");
            });

            modelBuilder.Entity<PurchaseOrderShellFlatData>(entity =>
            {
                entity.HasKey(e => e.PurchaseOrderShellFlatDataUid);

                entity.Property(e => e.PurchaseOrderShellFlatDataUid).HasColumnName("PurchaseOrderShellFlatDataUID");

                entity.Property(e => e.EstimatedDeliveryDate).IsUnicode(false);

                entity.Property(e => e.Frn)
                    .HasColumnName("FRN")
                    .IsUnicode(false);

                entity.Property(e => e.Notes).IsUnicode(false);

                entity.Property(e => e.OrderNumber).IsUnicode(false);

                entity.Property(e => e.Other1).IsUnicode(false);

                entity.Property(e => e.PurchaseDate).IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.SiteId)
                    .HasColumnName("SiteID")
                    .IsUnicode(false);

                entity.Property(e => e.SiteName).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.VendorAccountNumber).IsUnicode(false);

                entity.Property(e => e.VendorName).IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.PurchaseOrderShellFlatData)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOrderShellFlatData_ProcessTasks");
            });

            modelBuilder.Entity<PurchaseShipmentFlatData>(entity =>
            {
                entity.HasKey(e => e.PurchaseShipmentFlatDataUid);

                entity.Property(e => e.PurchaseShipmentFlatDataUid).HasColumnName("PurchaseShipmentFlatDataUID");

                entity.Property(e => e.InvoiceDate).IsUnicode(false);

                entity.Property(e => e.InvoiceNumber).IsUnicode(false);

                entity.Property(e => e.LineNumber).IsUnicode(false);

                entity.Property(e => e.OrderNumber).IsUnicode(false);

                entity.Property(e => e.QuantityShipped).IsUnicode(false);

                entity.Property(e => e.RejectedNotes).HasColumnType("text");

                entity.Property(e => e.RowId).HasColumnName("RowID");

                entity.Property(e => e.ShippedToSiteAddress).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteCity).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteId)
                    .HasColumnName("ShippedToSiteID")
                    .IsUnicode(false);

                entity.Property(e => e.ShippedToSiteName).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteState).IsUnicode(false);

                entity.Property(e => e.ShippedToSiteZip).IsUnicode(false);

                entity.Property(e => e.Status).IsUnicode(false);

                entity.Property(e => e.TicketNumber).IsUnicode(false);

                entity.Property(e => e.TicketedBy).IsUnicode(false);

                entity.Property(e => e.TicketedDate).IsUnicode(false);

                entity.HasOne(d => d.ProcessTaskU)
                    .WithMany(p => p.PurchaseShipmentFlatData)
                    .HasForeignKey(d => d.ProcessTaskUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseShipmentFlatData_ProcessTasks");
            });

            modelBuilder.Entity<TransformationLookup>(entity =>
            {
                entity.HasKey(e => e.TransformationLookupUid);

                entity.Property(e => e.TransformationLookupUid).HasColumnName("TransformationLookupUID");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.TransformationLookupKey)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProcessU)
                    .WithMany(p => p.TransformationLookup)
                    .HasForeignKey(d => d.ProcessUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TransformationLookup_Processes");
            });

            modelBuilder.Entity<Transformations>(entity =>
            {
                entity.HasKey(e => e.TransformationUid);

                entity.Property(e => e.DestinationColumn)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Function)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Parameters)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.SourceColumn)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StepName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProcessU)
                    .WithMany(p => p.Transformations)
                    .HasForeignKey(d => d.ProcessUid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transformations_Processes");
            });
        }
    }
}
