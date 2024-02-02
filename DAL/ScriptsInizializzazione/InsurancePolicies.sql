USE datSampleDataBase
BEGIN TRAN

INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, SalesLineDescription, SalesLineTitle, BackGroundColorCssClass, IsActive) 
		SELECT	1, 'T01', 'Travels', 'Travels', 'Viaggi', '#CC0066', 1
INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, SalesLineDescription, SalesLineTitle, BackGroundColorCssClass, IsActive) 
		SELECT	2, 'W01', 'Work', 'Work', 'Attivita'' Lavorativa', '#00FF80', 1
INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, SalesLineDescription, SalesLineTitle, BackGroundColorCssClass, IsActive) 
		SELECT	3, 'V01', 'Veichle', 'Veichle', 'Veicoli', '#FF0000', 1
INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, SalesLineDescription, SalesLineTitle, BackGroundColorCssClass, IsActive) 
		SELECT	4, 'H01', 'Home', 'Home', 'Famiglia', '#FF00FF', 1
INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, SalesLineDescription, SalesLineTitle, BackGroundColorCssClass, IsActive) 
		SELECT	5, 'H02', 'Health', 'Health', 'Salute', '#00FFFF', 1
INSERT INTO AppSalesLineTypes (Id, SalesLineCode, SalesLineName, SalesLineDescription, SalesLineTitle, BackGroundColorCssClass, IsActive) 
		SELECT	6, 'H03', 'House', 'House', 'Casa', '#FF8000', 1

INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 1, 'TS001', 'Suitcase', NULL, 1
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 2, 'TF001', 'Flight', NULL, 1
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 3, 'TT001', 'Train', NULL, 1
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 4, 'TB001', 'Bus', NULL, 1
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 5, 'TC001', 'Cruise', NULL, 1
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 6, 'TS002', 'Stay', NULL, 1
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 7, 'AA001', 'Agriculture', NULL, 2
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 8, 'AF001', 'Farm', NULL, 2
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 9, 'AIP01', 'Industrial Plant', NULL, 2
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 10, 'APC01', 'Professional Capacity', NULL, 2
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 11, 'VC001', 'Car', NULL, 3
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 12, 'VB001', 'Byke', NULL, 3
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 13, 'VS001', 'Ship', NULL, 3
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 14, 'FF001', 'Familiar', NULL, 4
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 15, 'FP001', 'Pet', NULL, 4
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 16, 'WS001', 'Specialist', NULL, 5
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 17, 'WD001', 'Dentistry', NULL, 5
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 18, 'WMI01', 'Major Interventions', NULL, 5
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 19, 'WH001', 'Hospitalization', NULL, 5
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 20, 'HH001', 'House', NULL, 6
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 21, 'HV001', 'Villa', NULL, 6
INSERT INTO AppInsurancePolicyCategories(Id, InsurancePolicyCategoryCode, InsurancePolicyCategoryName, IconCssClass, SalesLineId) SELECT 22, 'HC001', 'Chalet', NULL, 6

INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 0, 'None', 'Non Specificato', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 1, 'Contratto_A_Termine', 'Contratto a Termine - (CDD)', 0.5, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 2, 'Contratto_Tempo_Indeterminato', 'Contratto a Tempo Determinato - (CDI)', 1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 3, 'Contratto_Apprendistato', 'Contratto di Apprendistato', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 4, 'Partita_Iva', 'Partita IVA', 0.25, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 5, 'Contratto_CO_CO_PRO', 'Contratto a Progetto - (Co.Co.Pro.)', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 6, 'Contratto_CO_CO_CO', 'Contratto di collaborazione coordinata e continuativa - (Co.Co.Co.)', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 7, 'Contratto_Somministrazione', 'Contratto di somministrazione', 0.1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 8, 'Contratto_Lavoro_Intermittente', 'Contratto di lavoro Intermittente', 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 9, 'Contratto_Lavoro_Part_Time', 'Contratto di lavoro Part Time', 0.1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppContractTypes(Id, ContractTypeDescription, ContractTypeTitle, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) 
	SELECT 10, 'Contratto_Lavoro_Domicilio', 'Contratto di lavoro a Domicilio', 0, NULL, NULL, GETDATE(), GETDATE()

INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 1, 0, 18000, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 2, 18001, 28000, 0.05, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 3, 28001, 38000, 0.1, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 4, 38001, 48000, 0.15, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 5, 48001, 50000, 0.2, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 6, 50001, 55000, 0.25, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 7, 55001, 60000, 0.3, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 8, 60001, 65000, 0.35, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 9, 65001, 70000, 0.4, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 10, 70001, 75000, 0.45, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 11, 75001, 80000, 0.5, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 12, 80001, 85000, 0.6, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 13, 85001, 90000, 0.7, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 14, 90001, 100000, 0.8, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppJobRalRatingCoefficients(Id, MinRal, MaxRal, RatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 15, 100001, NULL, 0.9, NULL, NULL, GETDATE(), GETDATE()

INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 1, 0, 17, 0.5, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 2, 18, 26, 0.15, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 3, 27, 35, 0.25, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 4, 36, 44, 0.5, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 5, 45, 53, 0.75, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 6, 54, 62, 0.75, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 7, 63, 71, 0.25, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 8, 72, 80, 0.15, 0, NULL, NULL, GETDATE(), GETDATE()
INSERT INTO AppAgeRatingCoefficients(Id, MinAge, MaxAge, RatingCoefficient, ChildrenNumberRatingCoefficient, CreatedBy, UpdatedBy, UpdatedDate, CreatedDate) SELECT 9, 81, NULL, 0, 0, NULL, NULL, GETDATE(), GETDATE()

commit TRAN