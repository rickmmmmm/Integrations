
select * 
--update s set status = 1
from tblStudents s
where status = 0
and StudentsUID in (
	select EntityUID
	from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
	where [action] = 'transfer'
	and entitytypeuid = 4
	)
--25

--INSERT INTO	 tblStudentsDistribution (StudentsUID, ISBN, Accession, Code, SourceType, Source, Amount, Copies, Notes, UserID, ModifiedDate, Reconciled)
select EntityUID, ISBN, Tag, 'DIST', 'Campus', SiteID, null, 1, 'Hayes Move Items from TIPWeb-IT', 0, getdate(), 0
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
where [action] = 'transfer'
and entitytypeuid = 4
--27

--INSERT INTO tblStudentsDistribution_tx (DistributionID, StudentsUID, ISBN, Accession, Code, SourceType, Source, Amount, Copies, Notes, UserID, ModifiedDate, ActionTaken)
SELECT DistributionID, StudentsUID, ISBN, Accession, Code, SourceType, Source, Amount, Copies, Notes, UserID, ModifiedDate,  'INSERTED' 
FROM tblStudentsDistribution
where Notes = 'Hayes Move Items from TIPWeb-IT'
--27


select *
--update tm set StudentDistributionID = d.DistributionID
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
join tblStudentsDistribution d on tm.EntityUID = d.StudentsUID and tm.ISBN = d.ISBN and tm.Tag = d.Accession
where [action] = 'transfer'
and entitytypeuid = 4
and d.Notes = 'Hayes Move Items from TIPWeb-IT'
