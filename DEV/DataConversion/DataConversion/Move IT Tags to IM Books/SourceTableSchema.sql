USE [Keller_IT2IM_Move]
GO

/****** Object:  Table [dbo].[Keller_Transfer_IT2IM]    Script Date: 4/2/2019 11:50:43 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Keller_Transfer_IT2IM](
	[ikey] [int] NOT NULL,
	[Tag] [varchar](50) NULL,
	[ProductName] [varchar](150) NULL,
	[Location] [varchar](150) NULL,
	[Department] [varchar](150) NULL,
	[OrderNumber] [varchar](50) NULL,
	[SiteName] [varchar](150) NULL,
	[Status] [varchar](50) NULL,
	[SiteID] [varchar](50) NULL,
	[LocationID] [varchar](50) NULL,
	[Action] [varchar](50) NULL,
	[InventoryUID] [int] NULL,
	[ItemUID] [int] NULL,
	[VendorID] [int] NULL,
	[VendorName] [varchar](100) NULL,
	[TechDepartmentUID] [int] NULL,
	[SiteUID] [int] NULL,
	[StatusID] [int] NULL,
	[EntityTypeUID] [int] NULL,
	[EntityUID] [int] NULL,
	[PurchaseUID] [int] NULL,
	[PurchaseItemDetailUID] [int] NULL,
	[PurchaseItemShipmentUID] [int] NULL,
	[PurchaseDate] [date] NULL,
	[TransferInventoryUID] [int] NULL,
	[PublisherID] [int] NULL,
	[PublisherName] [varchar](100) NULL,
	[BookInventoryUID] [int] NULL,
	[FundingSource] [varchar](100) NULL,
	[FundingSourceUID] [int] NULL,
	[VendorOrderUID] [int] NULL,
	[VendorOrderDetailsUID] [int] NULL,
	[DistributionID] [int] NULL,
	[CampusDistributionID] [int] NULL,
	[Title] [varchar](150) NULL,
	[ISBN] [varchar](50) NULL,
	[ItemName] [varchar](100) NULL,
	[ItemNumber] [varchar](100) NULL,
	[ItemType] [varchar](50) NULL,
	[PurchasePrice] [varchar](50) NULL,
	[RequisitionUID] [int] NULL,
	[TeacherDistributionID] [int] NULL,
	[StudentDistributionID] [int] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


