select * 
--update t set status = 1
from tblTeachers t
where status = 0
and TeachersUID in (
	select EntityUID
	from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
	where [action] = 'transfer'
	and entitytypeuid = 3
	)
--35

--INSERT INTO	 tblTeachersDistribution (TeachersUID, ISBN, Accession, Code, SourceType, Source, Amount, Copies, Notes, UserID, ModifiedDate)
select EntityUID, ISBN, Tag, 'DIST', 'Campus', SiteID, null, 1,  'Hayes Move Items from TIPWeb-IT', 1, getdate()
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
where [action] = 'transfer'
and entitytypeuid = 3
--6994


--INSERT INTO tblTeachersDistribution_tx (DistributionID, TeachersUID, ISBN, Accession, Code, SourceType, Source, Amount, Copies, Notes, UserID, ModifiedDate, ActionTaken)
select DistributionID, TeachersUID, ISBN, Accession, Code, SourceType, Source, Amount, Copies, Notes, UserID, ModifiedDate, 'INSERTED'
from tblTeachersDistribution d
where Notes = 'Hayes Move Items from TIPWeb-IT'
--6994


select *
--update tm set TeacherDistributionID = d.DistributionID
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
join tblTeachersDistribution d on tm.EntityUID = d.TeachersUID and tm.ISBN = d.ISBN and tm.Tag = d.Accession
where [action] = 'transfer'
and entitytypeuid = 3
and d.Notes = 'Hayes Move Items from TIPWeb-IT'

