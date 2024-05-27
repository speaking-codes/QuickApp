/****** Script for SelectTopNRows command from SSMS  ******/
SELECT  TOP 15
		CustomerId				[Customer Id],
		Gender					Sesso,
		BirthMonth				[Mese Nascita],
		YearBirth				[Anno Nascita],
		MaritalStatus			[Stato Civile],
		[IsSingle]				[Is Single],
        [IsDependentSpouse]		[Coniuge Carico],
        ChildrenNumbers			[Numero Figli],		
		DependentChildrenNumber [Numero Figli Carico],
		ProfessionType			Professione,
		IsFreelancer			[Is Freelancer],
		Income					[Reddito (R.A.L.)],
		IncomeType				[Tipo Reddito],
		Country					[Provincia Residenza],
		Region					[Regione Residenza],
		InsurancePolicyId		[Identificativo Polizza],
		InsurancePolicyCode		[Codice Polizza]      
FROM	[AppCustomerLearningFeatures]
ORDER	BY NEWID()