use datSampleDataBase
select	top 15
		Gender				Sesso,
		BirthMonth			[Mese Nascita],
		YearBirth			[Anno Nascita],
		MaritalStatus		[Stato Civile],
		ChildrenNumbers		[Numero Figli],
		ProfessionType		Professione,
		Income				[Reddito (R.A.L.)],
		Country				[Provincia Residenza],
		Region				[Regione Residenza],
		InsurancePolicyId	[Identificativo Polizza],
		InsurancePolicyCode	[Codice Polizza]
from	AppCustomerLearningFeaturesTraining
order	by NEWID()