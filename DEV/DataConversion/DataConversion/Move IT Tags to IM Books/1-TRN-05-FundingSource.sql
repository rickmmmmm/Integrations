--insert tblFundingSources (FundingSource, Active, Applicationuid, CreatedByUserID, createddate, LastModifiedByUserID, LastModifiedDate)

select distinct fundingsource, 1, 1, 0, getdate(), 0, getdate() 
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm 
where [action] = 'transfer' 
and fundingsource not in (select fundingSource from tblFundingSources where applicationuid = 1)
order by 1

select * from tblFundingSources where applicationuid = 1 order by fundingsource

select *
--update tm set FundingSourceUID = f.FundingSourceUID
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm 
join tblFundingSources f on tm.FundingSource = f.FundingSource
where [action] = 'transfer' 
and applicationuid = 1 

select * 
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm 
where [action] = 'transfer' 
and fundingSourceUID is null


