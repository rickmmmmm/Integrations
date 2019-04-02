



--insert into tblCampusApproved(CampusID, ISBN, UserID)
select distinct SiteID, tm.ISBN, 0
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
left outer join tblCampusApproved ca on tm.SiteID = ca.CampusID and tm.ISBN = ca.ISBN
where [action] = 'transfer'
and ca.ISBN is null

--2665


select CampusID, ISBN
from tblCampusApproved 
group by CampusID, ISBN having count(*) > 1
