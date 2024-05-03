/****** Script for SelectTopNRows command from SSMS  ******/
use datSampleDataBase
;with	MatrixUsersItemsCte as
(
	select	mui.UserId Utente, InsurancePolicyCategoryName NomePolizza, Rating Valutazione
	from	AppMatrixUsersItems mui join AppInsurancePolicyCategories ipc
	on		mui.ItemId = ipc.Id		
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
select	top 20 *
from	PivotTableCte
where	[TUTELA GIUDIZIARIA] <> '0'
and		[TUTELA GIUDIZIARIA] > '0.82'
order	by Utente desc