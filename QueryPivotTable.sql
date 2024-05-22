/****** Script for SelectTopNRows command from SSMS  ******/
use datSampleDataBase
;with	MatrixUsersItemsCte as
(
	select	mui.UserId Utente, InsurancePolicyCategoryName NomePolizza, Rating Valutazione
	from	AppMatrixUsersItemsV7 mui with (nolock) join AppInsurancePolicyCategories ipc with (nolock)
	on		mui.ItemId = ipc.Id		
	where	UserId in (select UserId from AppMatrixUsersItems group by UserId having SUM(Rating) > 1)
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
						[TUTELA GIUDIZIARIA])) as PivotTable
)
select	*
from	PivotTableCte
--where	Utente <= '607734'
--where	Utente > '365018'
--where	[R.C. DIVERSI] = '0'
--		([INCENDIO/FURTO] <> '0'
--and		[TUTELA GIUDIZIARIA] <> '0')
order	by Utente desc
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