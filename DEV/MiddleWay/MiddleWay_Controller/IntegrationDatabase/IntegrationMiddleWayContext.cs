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
                entity.HasKey(e => e._ETL_DetailUid)
                    .HasName("PK__ETL_Details");

                entity.Property(e => e.OrderNumber) // .HasColumnName("")
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.SiteID)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.FundingSource)
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.FundingSourceDescription)
                    .HasColumnType("varchar(500)");

                entity.Property(e => e.ProductName)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.ProductDescription)
                    .HasColumnType("varchar(1000)");

                entity.Property(e => e.ProductTypeName)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.ProductTypeDescription)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.AccountCode)
                    .HasColumnType("varchar(100)");

                entity.Property(e => e.DepartmentName)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.DepartmentID)
                    .HasColumnType("varchar(50)");

                entity.Property(e => e.CFDA)
                    .HasColumnType("varchar(50)");

            });

            modelBuilder.Entity<_ETL_Headers>(entity =>
            {
                entity.HasKey(e => e._ETL_HeaderUid).HasName("PK__ETL_Headers"); // { get; set; } //INT IDENTITY(1,1) NOT NULL,

                entity.Property(e => e.ProcessUid).HasColumnType("INT"); // { get; set; } //INT NOT NULL,

                entity.Property(e => e.PurchaseUid).HasColumnType("INT"); //  { get; set; } //INT NOT NULL

                entity.Property(e => e.OrderNumber).HasColumnType("VARCHAR(50)"); //  { get; set; } //VARCHAR(50) NOT NULL,

                entity.Property(e => e.StatusID).HasColumnType("DATETIME"); // { get; set; } //INT NOT NULL,

                entity.Property(e => e.Status).HasColumnType("VARCHAR(50)"); //  { get; set; } //VARCHAR(50) NULL,

                entity.Property(e => e.VendorUid).HasColumnType("DATETIME"); //  { get; set; } //INT NOT NULL,

                entity.Property(e => e.VendorName).HasColumnType("VARCHAR(100)"); //  { get; set; } //VARCHAR(100) NULL,

                entity.Property(e => e.VendorAccountNumber).HasColumnType("VARCHAR(50)"); // { get; set; } //VARCHAR(50) NULL,

                entity.Property(e => e.SiteUid).HasColumnType("DATETIME"); // { get; set; } //INT NOT NULL,

                entity.Property(e => e.SiteID).HasColumnType("VARCHAR(100)"); // { get; set; } //VARCHAR(100) NULL,

                entity.Property(e => e.PurchaseDate).HasColumnType("DATETIME"); //  { get; set; } //DATETIME NULL,

                entity.Property(e => e.EstimatedDeliveryDate).HasColumnType("DATETIME"); //  { get; set; } //DATETIME NULL,

                entity.Property(e => e.Notes).HasColumnType("VARCHAR(1000)"); //  { get; set; } //VARCHAR(1000) NULL,

                entity.Property(e => e.Other1).HasColumnType("VARCHAR(100)"); //  { get; set; } //VARCHAR(100) NULL,

                entity.Property(e => e.FRN).HasColumnType("VARCHAR(50)"); // { get; set; } //VARCHAR(50)
            });

            modelBuilder.Entity<_ETL_Inventory>(entity =>
            {
                entity.HasKey(e => e._ETL_InventoryUid).HasName("PK__ETL_InventoryUid");

                entity.Property(e => e.ProcessUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.InventoryUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.AssetID).HasColumnType("varchar( )");//VARCHAR(100) NULL,

                entity.Property(e => e.Tag).HasColumnType("varchar( )");//VARCHAR(50) NOT NULL,



                entity.Property(e => e.Serial).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.InventoryTypeUid).HasColumnType("INT"); //INT NOT NULL,


                entity.Property(e => e.InventoryTypeName).HasColumnType("varchar( )"); //VARCHAR(100) NOT NULL,

                entity.Property(e => e.ItemUid).HasColumnType("INT").IsRequired(); //INT NOT NULL,


                entity.Property(e => e.ProductName).HasColumnType("varchar( )"); //VARCHAR(100) NULL,

                entity.Property(e => e.ProductDescription).HasColumnType("varchar( )"); //VARCHAR(1000) NULL,


                entity.Property(e => e.ProductByNumber).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.ItemTypeUid).HasColumnType("INT");//INT NOT NULL,

                entity.Property(e => e.ProductTypeName).HasColumnType("varchar( )");//VARCHAR(50) NOT NULL,


                entity.Property(e => e.ProductTypeDescription).HasColumnType("varchar( )"); //VARCHAR(1000) NULL,

                entity.Property(e => e.ModelNumber).HasColumnType("varchar( )");//VARCHAR(100) NULL,

                entity.Property(e => e.ManufacturerUid).HasColumnType("INT");//INT NULL,


                entity.Property(e => e.ManufacturerName).HasColumnType("varchar( )"); //VARCHAR(100) NULL,

                entity.Property(e => e.AreaUid).HasColumnType("INT"); //INT NULL,

                entity.Property(e => e.AreaName).HasColumnType("varchar( )"); //VARCHAR(100) NULL,

                entity.Property(e => e.SiteUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.SiteID).HasColumnType("varchar( )");//VARCHAR(100) NULL,

                entity.Property(e => e.SiteName).HasColumnType("varchar( )"); //VARCHAR(100) NULL,

                entity.Property(e => e.EntityUid).HasColumnType("INT");//INT NOT NULL,

                entity.Property(e => e.EntityName).HasColumnType("varchar( )"); //VARCHAR(50) NOT NULL,

                entity.Property(e => e.EntityTypeUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.EntityTypeName).HasColumnType("varchar( )");//VARCHAR(100) NOT NULL,

                entity.Property(e => e.StatusID).HasColumnType("INT");//INT NOT NULL,

                entity.Property(e => e.Status).HasColumnType("varchar( )"); //VARCHAR(50) NULL,

                entity.Property(e => e.TechDepartmentUid).HasColumnType("INT"); //INT NULL,

                entity.Property(e => e.DepartmentName).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.DepartmentID).HasColumnType("varchar( )"); //VARCHAR(50) NULL,

                entity.Property(e => e.FundingSourceUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.FundingSource).HasColumnType("varchar( )");//VARCHAR(500) NULL,

                entity.Property(e => e.FundingSourceDescription).HasColumnType("varchar( )"); //VARCHAR(500) NULL,

                entity.Property(e => e.PurchasePrice).HasColumnType("varchar( )"); //DECIMAL NULL,

                entity.Property(e => e.PurchaseDate).HasColumnType("varchar( )"); //DATETIME NULL,

                entity.Property(e => e.ExpirationDate).HasColumnType("varchar( )"); //DATETIME NULL,

                entity.Property(e => e.InventoryNotes).HasColumnType("varchar( )"); //VARCHAR(3000) NULL,

                entity.Property(e => e.ParentInventoryUid).HasColumnType("INT"); //INT NULL,

                entity.Property(e => e.ParentTag).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.InventorySourceUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.InventorySourceName).HasColumnType("varchar( )"); //VARCHAR(100),

                entity.Property(e => e.PurchaseUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.OrderNumber).HasColumnType("varchar( )"); //VARCHAR(50) NOT NULL,

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.LineNumber).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.AccountCode).HasColumnType("varchar( )"); //VARCHAR(100) NULL,

                entity.Property(e => e.VendorUid).HasColumnType("INT");//INT NOT NULL,

                entity.Property(e => e.VendorName).HasColumnType("varchar( )");//VARCHAR(100) NULL,

                entity.Property(e => e.VendorAccountNumber).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnType("INT");//INT NOT NULL,

                entity.Property(e => e.InvoiceNumber).HasColumnType("varchar( )"); //VARCHAR(25) NULL,

                entity.Property(e => e.InvoiceDate).HasColumnType("varchar( )");//DATE NULL,

                entity.Property(e => e.InventoryExt1Uid).HasColumnType("INT"); //INT NULL,

                entity.Property(e => e.InventoryMeta1Uid).HasColumnType("INT"); //INT NULL,

                entity.Property(e => e.CustomField1Label).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.CustomField1Value).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.InventoryExt2Uid).HasColumnType("INT"); //INT NULL,

                entity.Property(e => e.InventoryMeta2Uid).HasColumnType("INT"); //INT NULL,

                entity.Property(e => e.CustomField2Label).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.CustomField2Value).HasColumnType("varchar( )");//VARCHAR(50) NULL,

                entity.Property(e => e.InventoryExt3Uid).HasColumnType("INT");// //INT NULL,

                entity.Property(e => e.InventoryMeta3Uid).HasColumnType("INT");// //INT NULL,

                entity.Property(e => e.CustomField3Label).HasColumnType("varchar( )");////VARCHAR(50) NULL,

                entity.Property(e => e.CustomField3Value).HasColumnType("varchar( )");// //VARCHAR(50) NULL,

                entity.Property(e => e.InventoryExt4Uid).HasColumnType("INT");// //INT NULL,

                entity.Property(e => e.InventoryMeta4Uid).HasColumnType("INT");// //INT NULL,

                entity.Property(e => e.CustomField4Label).HasColumnType("varchar( )");////VARCHAR(50) NULL,

                entity.Property(e => e.CustomField4Value).HasColumnType("varchar( )");// //VARCHAR(50) NULL
            });

            modelBuilder.Entity<_ETL_Products>(entity =>
            {
                entity.HasKey(e => e._ETL_ProductsUid).HasName("PK__ETL_Products"); //INT IDENTITY(1,1) NOT NULL,

                entity.Property(e => e.ProcessUid).HasColumnType("INT"); //INT NOT NULL,

                entity.Property(e => e.ProductUid).HasColumnType("INT");  //INT NOT NULL,


                entity.Property(e => e.ProductNumber).HasColumnType("varchar( )");  //VARCHAR(50) NOT NULL,



                entity.Property(e => e.ProductName).HasColumnType("varchar( )"); //VARCHAR(100) NOT NULL,


                entity.Property(e => e.ProductDescription).HasColumnType("varchar( )");//VARCHAR(1000) NOT NULL,

                entity.Property(e => e.ItemTypeUid).HasColumnType("INT"); //INT NOT NULL,


                entity.Property(e => e.ProductTypeName).HasColumnType("varchar( )"); //VARCHAR(50) NOT NULL,



                entity.Property(e => e.ProductTypeDescription).HasColumnType("varchar( )");  //VARCHAR(1000) NULL,


                entity.Property(e => e.ModelNumber).HasColumnType("varchar( )"); //VARCHAR(100) NULL,

                entity.Property(e => e.ManufacturerUid).HasColumnType("INT");  //INT NOT NULL,


                entity.Property(e => e.ManufacturerName).HasColumnType("varchar( )"); //VARCHAR(100) NOT NULL,

                entity.Property(e => e.SuggestedPrice).HasColumnType("DECIMAL(18)"); //DECIMAL NULL,

                entity.Property(e => e.AreaUid).HasColumnType("INT");//INT NOT NULL,



                entity.Property(e => e.AreaName).HasColumnType("varchar( )"); //VARCHAR(100) NULL,


                entity.Property(e => e.ItemNotes).HasColumnType("varchar( )"); //VARCHAR(8000) NULL,



                entity.Property(e => e.SKU).HasColumnType("varchar( )");  //VARCHAR(50) NULL,

                entity.Property(e => e.SerialRequired).HasColumnType("varchar( )");  //BIT NOT NULL,

                entity.Property(e => e.ProjectedLife).HasColumnType("INT"); //INT NOT NULL,


                entity.Property(e => e.CustomField1).HasColumnType("varchar( )");  //VARCHAR(1000) NULL,



                entity.Property(e => e.CustomField2).HasColumnType("varchar( )"); //VARCHAR(1000) NULL,


                entity.Property(e => e.CustomField3).HasColumnType("varchar( )"); //VARCHAR(1000) NULL,

                entity.Property(e => e.Active).HasColumnType("BIT"); //BIT NOT NULL,

                entity.Property(e => e.AllowUntagged).HasColumnType("BIT"); //BIT NOT NULL

            });

            modelBuilder.Entity<_ETL_Shipments>(entity =>
            {
                entity.HasKey(e => e._ETL_ShipmentsUid).HasName("PK__ETL_ShipmentsUid"); //INT IDENTITY(1,1) NOT NULL,

                entity.Property(e => e.ProcessUid).HasColumnType("varchar( )"); //INT NOT NULL,

                entity.Property(e => e.PurchaseItemShipmentUid).HasColumnType("varchar( )"); // INT NOT NULL,

                entity.Property(e => e.PurchaseItemDetailUid).HasColumnType("varchar( )"); //INT NOT NULL,

                entity.Property(e => e.OrderNumber).HasColumnType("varchar( )"); //VARCHAR(50) NOT NULL,

                entity.Property(e => e.LineNumber).HasColumnType("varchar( )"); //INT NOT NULL,

                entity.Property(e => e.ShippedToSiteUid).HasColumnType("varchar( )"); // INT NOT NULL,

                entity.Property(e => e.SiteID).HasColumnType("varchar( )"); //VARCHAR(100) NULL,

                entity.Property(e => e.TicketNumber).HasColumnType("varchar( )"); // INT NULL,

                entity.Property(e => e.QuantityShipped).HasColumnType("varchar( )"); //INT NOT NULL,

                entity.Property(e => e.TicketedByUserID).HasColumnType("varchar( )"); //INT NULL,

                entity.Property(e => e.TicketedBy).HasColumnType("varchar( )"); //VARCHAR(50) NULL,

                entity.Property(e => e.TicketedDate).HasColumnType("varchar( )"); //DATETIME NULL,

                entity.Property(e => e.StatusID).HasColumnType("varchar( )"); // INT NOT NULL,

                entity.Property(e => e.Status).HasColumnType("varchar( )"); //VARCHAR(50) NULL,

                entity.Property(e => e.InvoiceNumber).HasColumnType("varchar( )"); //VARCHAR(25) NULL,

                entity.Property(e => e.InvoiceDate).HasColumnType("varchar( )"); //DATE NULL

            });

            modelBuilder.Entity<Configurations>(entity =>
            {
                entity.HasKey(e => e.ConfigurationUid).HasName("PK_Configurations");

                entity.Property(e => e.ProcessUid).HasColumnType("varchar( )"); // INT NOT NULL,

                entity.Property(e => e.ConfigurationName).HasColumnType("varchar( )"); // VARCHAR(100) NOT NULL,

                entity.Property(e => e.ConfigurationValue).HasColumnType("varchar( )"); // VARCHAR(250) NOT NULL,

                entity.Property(e => e.Enabled).HasColumnType("varchar( )"); // BIT NOT NULL,

            });

            modelBuilder.Entity<InventoryFlatData>(entity =>
            {
                entity.HasKey(e => e.InventoryFlatDataUid).HasName("PK_Configurations"); // INT IDENTITY(1,1) NOT NULL,

                entity.Property(e => e.ProcessUid).HasColumnType("varchar( )"); // INT NOT NULL,


                entity.Property(e => e.AssetID).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.Tag).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.Serial).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.SiteID).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.SiteName).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.Location).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.Status).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.DepartmentName).HasColumnType("varchar( )"); //VARCHAR(50) NULL,


                entity.Property(e => e.DepartmentID).HasColumnType("varchar( )"); //VARCHAR(50) NULL,


                entity.Property(e => e.FundingSource).HasColumnType("varchar( )"); //VARCHAR(500) NULL,


                entity.Property(e => e.FundingSourceDescription).HasColumnType("varchar( )"); //VARCHAR(500) NULL,

                entity.Property(e => e.PurchasePrice).HasColumnType("varchar( )"); //DECIMAL NULL,

                entity.Property(e => e.PurchaseDate).HasColumnType("varchar( )"); //DATETIME NULL,

                entity.Property(e => e.ExpirationDate).HasColumnType("varchar( )"); // DATETIME NULL,


                entity.Property(e => e.InventoryNotes).HasColumnType("varchar( )"); // VARCHAR(3000) NULL,


                entity.Property(e => e.OrderNumber).HasColumnType("varchar( )"); // VARCHAR(50) NOT NULL,


                entity.Property(e => e.VendorName).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.VendorAccountNumber).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.ParentTag).HasColumnType("varchar( )"); //VARCHAR(50) NULL,


                entity.Property(e => e.ProductName).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.ProductDescription).HasColumnType("varchar( )"); // VARCHAR(1000) NULL,


                entity.Property(e => e.ProductByNumber).HasColumnType("varchar( )"); //VARCHAR(50) NULL,


                entity.Property(e => e.ProductTypeName).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.ProductTypeDescription).HasColumnType("varchar( )"); // VARCHAR(1000) NULL,


                entity.Property(e => e.ModelNumber).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.ManufacturerName).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.AreaName).HasColumnType("varchar( )"); // VARCHAR(100) NULL,


                entity.Property(e => e.CustomField1Value).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.CustomField1Label).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.CustomField2Value).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.CustomField2Label).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.CustomField3Value).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.CustomField3Label).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.CustomField4Value).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.CustomField4Label).HasColumnType("varchar( )"); // VARCHAR(50) NULL,


                entity.Property(e => e.InvoiceNumber).HasColumnType("varchar( )"); // VARCHAR(25) NULL,

                entity.Property(e => e.InvoiceDate).HasColumnType("varchar( )"); //DATE NULL
            });

            modelBuilder.Entity<Mappings>(entity =>
            {
                entity.HasKey(e => e.MappingsUid).HasName("PK_Mappings"); // INT IDENTITY(1, 1) NOT NULL,

                entity.Property(e => e.ProcessUid).HasColumnType("varchar( )"); // INT NOT NULL,

                entity.Property(e => e.SourceColumn).HasColumnType("varchar( )"); // VARCHAR(100) NOT NULL,

                entity.Property(e => e.DestinationColumn).HasColumnType("varchar( )"); // VARCHAR(100) NOT NULL,

                entity.Property(e => e.Enabled).HasColumnType("varchar( )"); // BIT
            });

            modelBuilder.Entity<ProcessErrors>(entity =>
            {
                entity.HasKey(e => e.ProcessErrorUid).HasName("PK_ProcessErrors"); // INT IDENTITY(1, 1) NOT NULL,

                entity.Property(e => e.ProcessUid).HasColumnType("varchar( )"); // INT NOT NULL,

                entity.Property(e => e.ErrorNumber).HasColumnType("varchar( )"); // INT NULL,

                entity.Property(e => e.ErrorDescription).HasColumnType("varchar( )"); // VARCHAR(250)  NOT NULL,

                entity.Property(e => e.ErrorField).HasColumnType("varchar( )"); // VARCHAR(100) NULL,

                entity.Property(e => e.CreatedDate).HasColumnType("varchar( )"); // DATE

            });

            modelBuilder.Entity<Processes>(entity =>
            {
                entity.HasKey(e => e.)
                      .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });

            modelBuilder.Entity<ProductsFlatData>(entity =>
            {
                entity.HasKey(e => e.)
                      .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });

            modelBuilder.Entity<PurchaseInvoiceFlatData>(entity =>
            {
                entity.HasKey(e => e.)
                       .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });

            modelBuilder.Entity<PurchaseOrderDetailShipmentFlatData>(entity =>
            {
                entity.HasKey(e => e.)
                        .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });

            modelBuilder.Entity<PurchaseOrderFlatData>(entity =>
            {
                entity.HasKey(e => e.)
                        .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });

            modelBuilder.Entity<PurchaseOrderShellFlatData>(entity =>
            {
                entity.HasKey(e => e.)
                       .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });

            modelBuilder.Entity<PurchaseShipmentFlatData>(entity =>
            {
                entity.HasKey(e => e.)
                      .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });

            modelBuilder.Entity<Transformations>(entity =>
            {
                entity.HasKey(e => e.)
                       .HasName("PK_");

                entity.Property(e => e.)
                                   .HasColumnName("")
                                   .HasColumnType("varchar( )")
                                   .HasDefaultValueSql("");
            });
        }
    }
}
