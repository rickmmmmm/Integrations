--insert into tblRequisitions (RequisitionID, ReqStatus, DistrictCreatedCampusReq, CampusID,  DateCreatedOrSent, CampusReq, UserID, ModifiedDate, Notes)
  
 select distinct 'Hayes Move Items from TIPWeb-IT ' + campusid as Requisitionid, 'Complete', 1, tblcampusdistribution.CampusID, 
 GETDATE() as datecretedorsent,  1 as campusreq, 0 as userid, GETDATE() as modifieddate, 'Hayes Move Items from TIPWeb-IT'
 from tblcampusdistribution
 where notes = 'Hayes Move Items from TIPWeb-IT'

 --42



select *
--update tm set requisitionuid = rq.requisitionuid
from tblRequisitions rq
join Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm on tm.SiteID = rq.CampusID
where rq.RequisitionID like 'Hayes Move Items from TIPWeb-IT %'
and [action] = 'transfer'
