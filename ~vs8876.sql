/****** Script for SelectTopNRows command from SSMS  ******/
use datSampleDataBase
SELECT	MAX(Rating) RatingMax, MIN(Rating) RatingMin, AVG(Rating) RatingAverage, ipc.Id, ipc.InsurancePolicyCategoryCode, ipc.InsurancePolicyCategoryName
FROM	AppMatrixUsersItems mui join AppInsurancePolicyCategories ipc
on		mui.ItemId = ipc.Id
group	by ipc.Id, ipc.InsurancePolicyCategoryCode, ipc.InsurancePolicyCategoryName
order	by ipc.Id asc
select	*
from	AppMatrixUsersItems
where	ItemId in (8, 9)
and		Rating > '3'
select	MAX(Rating) RatingMax, MIN(Rating) RatingMin
from	AppMatrixUsersItems