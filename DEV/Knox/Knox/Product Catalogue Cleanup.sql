
USE TipWebHostedKnoxCountySD
GO

SET ANSI_NULLS, QUOTED_IDENTIFIER ON;
GO

CREATE TABLE #temp_ETL_ProductCatalogueCleanup(ItemName VARCHAR(100))

INSERT INTO #temp_ETL_ProductCatalogueCleanup select ItemName from TipWebHostedKnoxCountySD..tblTechItems group by ItemName having count(ItemName) > 1

DECLARE @v_ItemName varchar(100)

WHILE (Select Count(*) From #temp_ETL_ProductCatalogueCleanup) > 0

BEGIN

	SELECT TOP 1 @v_ItemName = ItemName from #temp_ETL_ProductCatalogueCleanup

		update tblTechUntaggedInventory set ItemUID = (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
			where ItemUID in (select itemuid from tblTechItems where ItemName = @v_ItemName)

		update tblTechItemAccessories set ItemUID = (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
			where ItemUID in (select itemuid from tblTechItems where ItemName = @v_ItemName)

		update tblTechItemImages set ItemUID = (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
			where ItemUID in (select itemuid from tblTechItems where ItemName = @v_ItemName)

		update tblTechInventory set ItemUID = (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
			where ItemUID in (select itemuid from tblTechItems where ItemName = @v_ItemName)

		update tblTechPurchaseItemDetails set ItemUID = (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
			where ItemUID in (select itemuid from tblTechItems where ItemName = @v_ItemName)

		update tblTechAuditDetailInventoryCounts set ItemUID = (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
			where ItemUID in (select itemuid from tblTechItems where ItemName = @v_ItemName)

		update tblTechTransferRequestDetails set ItemUID = (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
			where ItemUID in (select itemuid from tblTechItems where ItemName = @v_ItemName)

		delete from tblTechItems where ItemName = @v_ItemName and itemuid not in (Select top 1 itemuid from tblTechItems where ItemName = @v_ItemName order by ItemUID asc)
	
	DELETE FROM #temp_ETL_ProductCatalogueCleanup WHERE ItemName = @v_ItemName

END

DROP TABLE #temp_ETL_ProductCatalogueCleanup