use datSampleDataBase
begin	tran
	truncate	table appmatrixusersitemsv1
	insert		into appmatrixusersitemsv1 (UserId, ItemId, Rating)
	select		UserId, ItemId, Rating
	from		AppMatrixUsersItems
	where		UserId in
	(
				select	top 60 percent UserId
				from	AppMatrixUsersItems
				group	by UserId
				order	by NEWID()
	)
commit tran
--select	top 30 percent Count(UserId) CountUser
--from	AppMatrixUsersItems
--group	by UserId
--order	by NEWID()
select	top 60 percent UserId
from	AppMatrixUsersItems
group	by UserId
order	by NEWID()
--select	COUNT(distinct UserId)
--from	AppMatrixUsersItems