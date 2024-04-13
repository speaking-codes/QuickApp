use NewAge
Declare @registryCustomer Table
(	
	Id bigint identity(1, 1),
	Gender char(1) null,
	BirthDate Datetime null,
	MaritalStatusCode nvarchar(10) null,
	FamilyTypeCode nvarchar(10) null,
	ChildrenNumbers int null,
	IncomeTypeCode nvarchar(10) null,
	ProfessionTypeCode nvarchar(10) null,
	ProfessionTypeCode_1 nvarchar(10) null,
	ProfessionTypeCode_2 nvarchar(10) null,
	IncomeBracket nvarchar(3) null,
	Income float null,
	CountryAbbreviation nvarchar(10),
	RegionCode nvarchar(10) null,
	InsurancePolicyCategoryCode nvarchar(max) not null
)
insert	into @registryCustomer(Gender, BirthDate, MaritalStatusCode, FamilyTypeCode, ChildrenNumbers, IncomeTypeCode, ProfessionTypeCode, ProfessionTypeCode_1, ProfessionTypeCode_2,
		Income, IncomeBracket, CountryAbbreviation, RegionCode, InsurancePolicyCategoryCode)
select	
		p.sesso, p.data_di_nascita, p.cod_tipo_stato_civile, p.cod_tipo_famiglia, p.num_figli, p.cod_tipo_reddito, p.cod_libera_professione, p.cod_professione, p.cod_professione_gaas, 
		p.reddito, cl.cod_fascia_reddito, pr.sigla_provincia, pr.cod_regione, 'C01'
from	PERSONA p with (nolock) join CLIENTE cl with (nolock) 
on		p.codice_cliente = p.codice_cliente left join PROVINCIA pr with (nolock)
on		cl.provincia = pr.sigla_provincia join MATRICE_POLIZZE_CLIENTI mpc with (nolock)
on		cl.codice_cliente = mpc.codice_cliente and mpc.cod_tipo_legame = 'C' join DATI_AMMINISTRATIVI_POLIZZA dap with (nolock) 
on		mpc.cod_agenzia = dap.cod_agenzia and mpc.cod_ramo = dap.cod_ramo and mpc.numero_polizza = dap.numero_polizza join POLIZZA_AUTO pa
on		dap.cod_agenzia = pa.cod_agenzia and dap.cod_ramo = pa.cod_ramo and mpc.numero_polizza = pa.numero_polizza
where	(pa.cod_settore_tariffario = '8' or pa.cod_settore_tariffario = '9') 
and		(DATEPART(YEAR, dap.data_emissione_contratto) >= '2023' or DATEPART(YEAR, dap.data_inizio_copertura) >= '2023')
select	'insert	into Temp(Gender, BirthDate, MaritalStatusCode, FamilyTypeCode, ChildrenNumbers, IncomeTypeCode, ProfessioneTypeCode, ProfessioneTypeCode_1, ProfessioneTypeCode_2,
		Income, IncomeBracket, CountryAbbreviation, RegionCode, InsurancePolicyCategoryCode) select ' +
		cast(Id as nvarchar(max)) + ', ' + 
		case when MaritalStatusCode is null then 'null, ' else '''' + MaritalStatusCode + ''', ' end +
		case when FamilyTypeCode is null then 'null, ' else '''' + FamilyTypeCode + ''', ' end +
		case when ChildrenNumbers is null then 'null, ' else '' + CAST(ChildrenNumbers as nvarchar(max)) + '' end + 
		case when IncomeTypeCode is null then 'null, ' else '''' + IncomeTypeCode + ''', ' end +
		case when ProfessionTypeCode is null then 'null, ' else '''' + ProfessionTypeCode + ''', ' end +
		case when ProfessionTypeCode_1 is null then 'null, ' else '''' + ProfessionTypeCode_1 + ''', ' end +
		case when ProfessionTypeCode_2 is null then 'null, ' else '''' + ProfessionTypeCode_2 + ''', ' end +
		case when Income is null then 'null, ' else '' + CAST(Income as nvarchar(max)) + ', ' end + 
		case when IncomeBracket is null then 'null, ' else '''' + IncomeBracket + ''', ' end +
		case when CountryAbbreviation is null then 'null, ' else '''' + CountryAbbreviation + ''', ' end +
		case when RegionCode is null then 'null, ' else '''' + RegionCode + ''', ' end + 'C01'
from	@registryCustomer

select	*
from	@registryCustomer