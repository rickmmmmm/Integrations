select *
--update tm set VendorName = PublisherName
  FROM [Keller_IT2IM_Move].[dbo].[Keller_Transfer_IT2IM] tm
  where [action] = 'transfer' and vendorname is null