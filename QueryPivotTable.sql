/****** Script for SelectTopNRows command from SSMS  ******/
use datSampleDataBase
;with	MatrixUsersItemsCte as
(
	select	mui.UserId Utente, InsurancePolicyCategoryName NomePolizza, Rating Valutazione
	from	AppMatrixUsersItems mui with (nolock) join AppInsurancePolicyCategories ipc with (nolock)
	on		mui.ItemId = ipc.Id		
	where	UserId in (select UserId from AppMatrixUsersItems group by UserId having SUM(Rating) > 1)
	--and		ItemId not in (8, 9)
),
PivotTableCte as
(
	select	*
	from	(
				select	*
				from	MatrixUsersItemsCte
			)	as SourceTable
	pivot(SUM(Valutazione) for NomePolizza in (
						[R.C.A.],
						[A.R.D.],
						[R.C. DIVERSI],
						[MULTIGARANZIA ABITAZIONE],
						[GLOBALE FABBRICATI],
						[INFORTUNI],
						[MALATTIA],
						[INCENDIO/FURTO],
						[TUTELA GIUDIZIARIA]
						)) as PivotTable
)
select	*--top 25 *
from	PivotTableCte
--where	Utente >= '99940'
--where	Utente > '365018'
--where	[R.C. DIVERSI] = '0'
--		([INCENDIO/FURTO] <> '0'
--and		[TUTELA GIUDIZIARIA] <> '0')
--select	*
--from	AppCustomerLearningFeatures
--where	CustomerId in 
--(
--	select	CustomerId
--	from	AppCustomerLearningFeatures
--	where	InsurancePolicyId in (2, 3, 5, 8, 9)
--)
--order	by CustomerId asc
----select	*
----from	AppMatrixUsersItems
----where	UserId = '96267'
--order	by NEWID()
order	by Utente desc
