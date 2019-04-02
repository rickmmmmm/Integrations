
--select *
--from tblBookInventory bi
--join (
--	select distinct ISBN, Title, PurchasePrice, PublisherID 
--	from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM 
--	where [action] = 'transfer'
--	) tm  on bi.ISBN = tm.ISBN


update Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM set PurchasePrice = '130.64' where ISBN = 'C2-KISDK5ST509-K'

select distinct ISBN, Title, PurchasePrice, PublisherID 
	from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM 
	where [action] = 'transfer' 
	and ISBN in (

	select ISBN from (
		select distinct ISBN, Title, PurchasePrice, PublisherID 
		from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM 
		where [action] = 'transfer'
		) dup
	group by ISBN having count(*) > 1
	)
order by ISBN