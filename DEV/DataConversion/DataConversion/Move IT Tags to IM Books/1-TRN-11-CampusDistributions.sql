--insert into tblCampusDistribution (CampusID, ISBN, Code,Source,Amount, Copies, Notes,UserID, ModifiedDate)

select SiteID, ISBN, 'DIST', 'District', 0, count(ISBN), 'Hayes Move Items from TIPWeb-IT', 0, getdate()
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
where [action] = 'transfer'
group by SiteID, ISBN
order by siteID, ISBN

--2671

select *
--update tm set CampusDistributionID = cd.DistributionID
from Keller_IT2IM_Move.dbo.Keller_Transfer_IT2IM tm
join tblCampusDistribution cd on tm.SiteID = cd.CampusID and tm.ISBN = cd.ISBN
where [action] = 'transfer'
and cd.Notes = 'Hayes Move Items from TIPWeb-IT'
order by siteID, tm.ISBN
--22905


--insert into tblCampusDistribution_tx (DistributionID, CampusID, ISBN, Code, Source, Amount, Copies,Notes, UserID,ModifiedDate,ActionTaken)
select DistributionID, CampusID, ISBN, Code, Source,  Amount, Copies, Notes, UserID, ModifiedDate, 'INSERTED'
from tblCampusDistribution cd
where cd.Notes = 'Hayes Move Items from TIPWeb-IT'
--2671



