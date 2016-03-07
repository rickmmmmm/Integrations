/****** Object:  Database [TIPWEB_ETL_TestCamNu]    Script Date: 03/07/2016 16:03:20 ******/
CREATE DATABASE [TIPWEB_ETL_TestCamNu] ON  PRIMARY 
( NAME = N'TIPWEB_ETL', FILENAME = N'E:\SQL\DATA\TIPWEB_ETL_TestCamNu.mdf' , SIZE = 113920KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TIPWEB_ETL_log', FILENAME = N'E:\SQL\DATA\TIPWEB_ETL_TestCamNu_1.LDF' , SIZE = 112384KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TIPWEB_ETL_TestCamNu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET ANSI_NULLS OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET ANSI_PADDING OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET ARITHABORT OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET  DISABLE_BROKER
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET  READ_WRITE
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET RECOVERY FULL
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET  MULTI_USER
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [TIPWEB_ETL_TestCamNu] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'TIPWEB_ETL_TestCamNu', N'ON'
GO
/****** Object:  User [TIPWeb]    Script Date: 03/07/2016 16:03:20 ******/
CREATE USER [TIPWeb] FOR LOGIN [TIPWeb] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[CustomFieldMeta_Pivot]    Script Date: 03/07/2016 16:03:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFieldMeta_Pivot](
	[product_type] [nvarchar](255) NULL,
	[name] [nvarchar](255) NULL,
	[datatype] [nvarchar](255) NULL,
	[required] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomFieldMeta]    Script Date: 03/07/2016 16:03:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomFieldMeta](
	[Product_Type] [nvarchar](255) NULL,
	[Custom_Field1] [nvarchar](255) NULL,
	[Data_Type1] [nvarchar](255) NULL,
	[Required_1] [nvarchar](255) NULL,
	[Custom_Field2] [nvarchar](255) NULL,
	[Data_Type2] [nvarchar](255) NULL,
	[Required_2] [nvarchar](255) NULL,
	[Custom_Field3] [nvarchar](255) NULL,
	[Data_Type3] [nvarchar](255) NULL,
	[Required_3] [nvarchar](255) NULL,
	[Custom_Field4] [nvarchar](255) NULL,
	[Data_Type4] [nvarchar](255) NULL,
	[Required_4] [nvarchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients_ETL_Orig]    Script Date: 03/07/2016 16:03:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients_ETL_Orig](
	[iKey] [int] IDENTITY(1,1) NOT NULL,
	[ProductNumber] [int] NULL,
	[ProductName] [varchar](1000) NULL,
	[ManufacturerName] [varchar](100) NULL,
	[ModelNumber] [varchar](100) NULL,
	[ItemTypeName] [varchar](50) NULL,
	[ItemSuggestedPrice] [varchar](50) NULL,
	[DepartmentName] [varchar](100) NULL,
	[SKU] [varchar](50) NULL,
	[serial_required] [char](10) NULL,
	[Serial] [varchar](250) NULL,
	[ProjectedLife] [varchar](50) NULL,
	[ProductInfo1] [varchar](1000) NULL,
	[ProductInfo2] [varchar](1000) NULL,
	[ProductInfo3] [varchar](1000) NULL,
	[SiteID] [varchar](100) NULL,
	[SiteName] [varchar](200) NULL,
	[RoomNumber] [varchar](50) NULL,
	[RoomDesc] [varchar](50) NULL,
	[RoomType] [varchar](50) NULL,
	[Tag] [varchar](50) NULL,
	[ParentTag] [varchar](50) NULL,
	[TagNotes] [varchar](1000) NULL,
	[ExpirationDate] [varchar](50) NULL,
	[EntityID] [varchar](50) NULL,
	[EntityLastName] [varchar](50) NULL,
	[EntityFirstName] [varchar](50) NULL,
	[EntityType] [varchar](50) NULL,
	[PONumber] [varchar](50) NULL,
	[Fundingsource] [varchar](500) NULL,
	[PurchaseDate] [varchar](50) NULL,
	[PurchasePrice] [varchar](50) NULL,
	[AccountCode] [varchar](50) NULL,
	[VendorName] [varchar](300) NULL,
	[PO_Notes] [varchar](max) NULL,
	[newPONumber] [varchar](50) NULL,
	[Accessory1Included] [varchar](50) NULL,
	[Accessory2Included] [varchar](50) NULL,
	[Accessory3Included] [varchar](50) NULL,
	[CustomUDEF1_Value] [varchar](250) NULL,
	[CustomUDEF2_Value] [varchar](250) NULL,
	[CustomUDEF3_Value] [varchar](250) NULL,
	[CustomUDEF4_Value] [varchar](250) NULL,
	[CustomUDEF1] [varchar](50) NULL,
	[CustomUDEF2] [varchar](50) NULL,
	[CustomUDEF3] [varchar](50) NULL,
	[CustomUDEF4] [varchar](50) NULL,
	[CustomUDEF1DataType] [varchar](50) NULL,
	[CustomUDEF2DataType] [varchar](50) NULL,
	[CustomUDEF3DataType] [varchar](50) NULL,
	[CustomUDEF4DataType] [varchar](50) NULL,
	[AccessoryName1] [varchar](150) NULL,
	[AccessoryPrice1] [varchar](50) NULL,
	[AccessoryQuantity1] [varchar](50) NULL,
	[AccessoryConsumable1] [varchar](50) NULL,
	[AccessoryName2] [varchar](150) NULL,
	[AccessoryPrice2] [varchar](50) NULL,
	[AccessoryQuantity2] [varchar](50) NULL,
	[AccessoryConsumable2] [varchar](50) NULL,
	[AccessoryName3] [varchar](150) NULL,
	[AccessoryPrice3] [varchar](50) NULL,
	[AccessoryQuantity3] [varchar](50) NULL,
	[AccessoryConsumable3] [varchar](50) NULL,
	[oldproductname] [varchar](255) NULL,
	[Other2] [varchar](200) NULL,
	[Other1] [varchar](200) NULL,
	[Status] [varchar](50) NULL,
	[Date_Loaded] [datetime] NULL,
	[DataSource] [varchar](200) NULL,
	[ReviewFlag] [bit] NULL,
	[ReviewNotes] [varchar](200) NULL,
	[import] [bit] NOT NULL,
	[RejectNotes] [varchar](1000) NULL,
	[ManufacturerUID] [int] NULL,
	[VendorUID] [int] NULL,
	[ItemTypeUID] [int] NULL,
	[ItemUID] [int] NULL,
	[InventoryUID] [int] NULL,
	[SiteUID] [int] NULL,
	[RoomUID] [int] NULL,
	[RoomTypeUID] [int] NULL,
	[EntityTypeUID] [int] NULL,
	[EntityUID] [int] NULL,
	[FundingSourceUID] [int] NULL,
	[PurchaseUID] [int] NULL,
	[PurchaseItemDetailUID] [int] NULL,
	[PurchaseItemShipmentUID] [int] NULL,
 CONSTRAINT [PK_Clients_ETL_Orig] PRIMARY KEY CLUSTERED 
(
	[iKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Clients_ETL]    Script Date: 03/07/2016 16:03:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clients_ETL](
	[iKey] [int] IDENTITY(1,1) NOT NULL,
	[ProductNumber] [int] NULL,
	[ProductName] [varchar](1000) NULL,
	[ManufacturerName] [varchar](100) NULL,
	[ModelNumber] [varchar](100) NULL,
	[ItemTypeName] [varchar](50) NULL,
	[ItemSuggestedPrice] [varchar](50) NULL,
	[DepartmentName] [varchar](100) NULL,
	[SKU] [varchar](50) NULL,
	[serial_required] [char](10) NULL,
	[Serial] [varchar](250) NULL,
	[ProjectedLife] [varchar](50) NULL,
	[ProductInfo1] [varchar](1000) NULL,
	[ProductInfo2] [varchar](1000) NULL,
	[ProductInfo3] [varchar](1000) NULL,
	[SiteID] [varchar](100) NULL,
	[SiteName] [varchar](200) NULL,
	[RoomNumber] [varchar](50) NULL,
	[RoomDesc] [varchar](50) NULL,
	[RoomType] [varchar](50) NULL,
	[Tag] [varchar](50) NULL,
	[ParentTag] [varchar](50) NULL,
	[TagNotes] [varchar](1000) NULL,
	[ExpirationDate] [varchar](50) NULL,
	[EntityID] [varchar](50) NULL,
	[EntityLastName] [varchar](50) NULL,
	[EntityFirstName] [varchar](50) NULL,
	[EntityType] [varchar](50) NULL,
	[PONumber] [varchar](50) NULL,
	[Fundingsource] [varchar](500) NULL,
	[PurchaseDate] [varchar](50) NULL,
	[PurchasePrice] [varchar](50) NULL,
	[AccountCode] [varchar](50) NULL,
	[VendorName] [varchar](300) NULL,
	[PO_Notes] [varchar](max) NULL,
	[newPONumber] [varchar](50) NULL,
	[Accessory1Included] [varchar](50) NULL,
	[Accessory2Included] [varchar](50) NULL,
	[Accessory3Included] [varchar](50) NULL,
	[CustomUDEF1_Value] [varchar](250) NULL,
	[CustomUDEF2_Value] [varchar](250) NULL,
	[CustomUDEF3_Value] [varchar](250) NULL,
	[CustomUDEF4_Value] [varchar](250) NULL,
	[CustomUDEF1] [varchar](50) NULL,
	[CustomUDEF2] [varchar](50) NULL,
	[CustomUDEF3] [varchar](50) NULL,
	[CustomUDEF4] [varchar](50) NULL,
	[CustomUDEF1DataType] [varchar](50) NULL,
	[CustomUDEF2DataType] [varchar](50) NULL,
	[CustomUDEF3DataType] [varchar](50) NULL,
	[CustomUDEF4DataType] [varchar](50) NULL,
	[AccessoryName1] [varchar](150) NULL,
	[AccessoryPrice1] [varchar](50) NULL,
	[AccessoryQuantity1] [varchar](50) NULL,
	[AccessoryConsumable1] [varchar](50) NULL,
	[AccessoryName2] [varchar](150) NULL,
	[AccessoryPrice2] [varchar](50) NULL,
	[AccessoryQuantity2] [varchar](50) NULL,
	[AccessoryConsumable2] [varchar](50) NULL,
	[AccessoryName3] [varchar](150) NULL,
	[AccessoryPrice3] [varchar](50) NULL,
	[AccessoryQuantity3] [varchar](50) NULL,
	[AccessoryConsumable3] [varchar](50) NULL,
	[oldproductname] [varchar](255) NULL,
	[Other2] [varchar](200) NULL,
	[Other1] [varchar](200) NULL,
	[Status] [varchar](50) NULL,
	[Date_Loaded] [datetime] NULL,
	[DataSource] [varchar](200) NULL,
	[ReviewFlag] [bit] NULL,
	[ReviewNotes] [varchar](200) NULL,
	[import] [bit] NOT NULL,
	[RejectNotes] [varchar](1000) NULL,
	[ManufacturerUID] [int] NULL,
	[VendorUID] [int] NULL,
	[ItemTypeUID] [int] NULL,
	[ItemUID] [int] NULL,
	[InventoryUID] [int] NULL,
	[SiteUID] [int] NULL,
	[RoomUID] [int] NULL,
	[RoomTypeUID] [int] NULL,
	[EntityTypeUID] [int] NULL,
	[EntityUID] [int] NULL,
	[FundingSourceUID] [int] NULL,
	[PurchaseUID] [int] NULL,
	[PurchaseItemDetailUID] [int] NULL,
	[PurchaseItemShipmentUID] [int] NULL,
 CONSTRAINT [PK_Clients_ETL] PRIMARY KEY CLUSTERED 
(
	[iKey] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_Clients_ETL_Orig_ReviewFlag]    Script Date: 03/07/2016 16:03:21 ******/
ALTER TABLE [dbo].[Clients_ETL_Orig] ADD  CONSTRAINT [DF_Clients_ETL_Orig_ReviewFlag]  DEFAULT ((1)) FOR [ReviewFlag]
GO
/****** Object:  Default [DF_Clients_ETL_Orig_import]    Script Date: 03/07/2016 16:03:21 ******/
ALTER TABLE [dbo].[Clients_ETL_Orig] ADD  CONSTRAINT [DF_Clients_ETL_Orig_import]  DEFAULT ((1)) FOR [import]
GO
/****** Object:  Default [DF_Clients_ETL_ReviewFlag]    Script Date: 03/07/2016 16:03:21 ******/
ALTER TABLE [dbo].[Clients_ETL] ADD  CONSTRAINT [DF_Clients_ETL_ReviewFlag]  DEFAULT ((1)) FOR [ReviewFlag]
GO
