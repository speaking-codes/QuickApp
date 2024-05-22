use datSampleDataBase
GO
BEGIN TRAN
TRUNCATE TABLE AppCustomerLearningFeaturesCopy
TRUNCATE TABLE AppMatrixUsersItems
INSERT INTO [dbo].[AppCustomerLearningFeaturesCopy]
           ([Id]
           ,[CustomerId]
           ,[Gender]
           ,[BirthMonth]
           ,[YearBirth]
           ,[MaritalStatus]
           ,[IsSingle]
           ,[IsDependentSpouse]
           ,[ChildrenNumbers]
           ,[DependentChildrenNumber]
           ,[ProfessionType]
           ,[IsFreelancer]
           ,[IncomeClassType]
           ,[IncomeType]
           ,[Country]
           ,[Region]
           ,[InsurancePolicyId]
           ,[InsurancePolicyCode]
           ,[InsurancePolicyName]
           ,[InsurancePolicyDescription])
			SELECT TOP 90 PERCENT
				   [Id]
				  ,[CustomerId]
				  ,[Gender]
				  ,[BirthMonth]
				  ,[YearBirth]
				  ,[MaritalStatus]
				  ,[IsSingle]
				  ,[IsDependentSpouse]
				  ,[ChildrenNumbers]
				  ,[DependentChildrenNumber]
				  ,[ProfessionType]
				  ,[IsFreelancer]
				  ,[IncomeClassType]
				  ,[IncomeType]
				  ,[Country]
				  ,[Region]
				  ,[InsurancePolicyId]
				  ,[InsurancePolicyCode]
				  ,[InsurancePolicyName]
				  ,[InsurancePolicyDescription]
			  FROM [dbo].[AppCustomerLearningFeatures]
GO
GO
COMMIT TRAN
SELECT	InsurancePolicyId, InsurancePolicyName, COUNT(DISTINCT CustomerId) CountCustomer
FROM	AppCustomerLearningFeaturesCopy
GROUP	BY InsurancePolicyName, InsurancePolicyId
ORDER	BY InsurancePolicyId ASC
GO

