

 --Insert into tblBookOrders (RequisitionUID, ISBN, FundingSource, Status, UserID, Price,  Ordered, Received, CopiesOnHand, CopiesSent,  copiesapproved)

select RequisitionUID, ISBN, FundingSource, 'Complete', 0, PurchasePrice, count(*), count(*), count(*), count(*), count(*)
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
where [action] = 'transfer'
group by RequisitionUID, ISBN, FundingSource, PurchasePrice

--2738 

--insert into tblbookordershistorydist (RequisitionUID, ISBN, CopiesSent, DateCopiesSent, TicketDate, CopiesToShip, CopiesReceived, UserID, RandomReq, ModifiedDate)

select bo.requisitionuid, isbn, ordered as copiessent, bo.modifieddate as datecopiessent, bo.modifieddate as ticketdate,
ordered as copiestoship, ordered as copiesreceived, 0 as userid, bo.requisitionuid AS randomreq, getdate() as modifieddate
from tblbookorders bo
join tblRequisitions rq on bo.RequisitionUID = rq.RequisitionUID
where rq.Notes = 'Hayes Move Items from TIPWeb-IT'
