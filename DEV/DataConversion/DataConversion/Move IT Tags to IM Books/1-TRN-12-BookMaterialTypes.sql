select * from tblMaterialTypes order by 2
select distinct ItemType from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm where [action] = 'transfer'


--insert into tblbooksmaterialtypes (ISBN, MaterialTypeID, LastModifiedDate)
select distinct ISBN, 15, getDate()
from  Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm 
where  [action] = 'transfer'
and ItemType like '%Ancillary%'
and ISBN not in (select ISBN from tblbooksmaterialtypes)




--insert into tblbooksmaterialtypes (ISBN, MaterialTypeID, LastModifiedDate)

select distinct ISBN, 16, getDate()
from  Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm 
where  [action] = 'transfer'
and (ItemType  = 'Student Materials' or ItemType = 'Calculators')
and ISBN not in (select ISBN from tblbooksmaterialtypes)
