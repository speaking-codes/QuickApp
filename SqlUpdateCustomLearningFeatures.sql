USE [datSampleDataBase]
GO
begin tran
		DROP TABLE IF EXISTS #AppCustomerLearningFeatures
		CREATE TABLE #AppCustomerLearningFeatures
		(
			[CustomerId] [bigint] NOT NULL IDENTITY(1,1),
			[Gender] [nvarchar](max) NULL,
			[BirthMonth] [nvarchar](max) NULL,
			[YearBirth] [nvarchar](max) NULL,
			[MaritalStatus] [nvarchar](max) NULL,
			[IsSingle] [bit] NOT NULL,
			[ChildrenNumbers] [int] NOT NULL,
			[DependentChildrenNumber] [int] NOT NULL,
			[ProfessionType] [nvarchar](max) NULL,
			[IsFreelancer] [bit] NOT NULL,
			[Income] [float] NOT NULL,
			[IncomeType] [nvarchar](max) NULL,
			[Country] [nvarchar](max) NULL,
			[Region] [nvarchar](max) NULL,
			[InsurancePolicyId] [tinyint] NOT NULL,
			[InsurancePolicyCode] [nvarchar](max) NULL,
			[InsurancePolicyName] [nvarchar](max) NULL
		)
		INSERT INTO #AppCustomerLearningFeatures
				   ([Gender]
				   ,[BirthMonth]
				   ,[YearBirth]
				   ,[MaritalStatus]
				   ,[IsSingle]
				   ,[ChildrenNumbers]
				   ,[DependentChildrenNumber]
				   ,[ProfessionType]
				   ,[IsFreelancer]
				   ,[Income]
				   ,[IncomeType]
				   ,[Country]
				   ,[Region]
				   ,[InsurancePolicyId]
				   ,[InsurancePolicyCode]
				   ,[InsurancePolicyName])
			SELECT [Gender]
				  ,[BirthMonth]
				  ,[YearBirth]
				  ,[MaritalStatus]
				  ,[IsSingle]
				  ,[ChildrenNumbers]
				  ,[DependentChildrenNumber]
				  ,[ProfessionType]
				  ,[IsFreelancer]
				  ,[Income]
				  ,[IncomeType]
				  ,[Country]
				  ,[Region]
				  ,[InsurancePolicyId]
				  ,[InsurancePolicyCode]
				  ,[InsurancePolicyName]
		  FROM [AppCustomerLearningFeatures]
		  where CustomerId is null
		  group by [Gender]
			  ,[BirthMonth]
			  ,[YearBirth]
			  ,[MaritalStatus]
			  ,[IsSingle]
			  ,[IsDependentSpouse]
			  ,[ChildrenNumbers]
			  ,[DependentChildrenNumber]
			  ,[ProfessionType]
			  ,[IsFreelancer]
			  ,[Income]
			  ,[IncomeType]
			  ,[Country]
			  ,[Region]
			  ,[InsurancePolicyId]
			  ,[InsurancePolicyCode]
			  ,[InsurancePolicyName]
		Update	AppCustomerLearningFeatures
		set		CustomerId = clfTemp.CustomerId
		from	AppCustomerLearningFeatures clf join #AppCustomerLearningFeatures clfTemp
		on		clf.Gender = clfTemp.Gender
		and		clf.BirthMonth = clfTemp.BirthMonth
		and		clf.YearBirth = clfTemp.YearBirth
		and		clf.MaritalStatus = clfTemp.MaritalStatus
		and		clf.IsSingle = clfTemp.IsSingle
		and		clf.ChildrenNumbers = clfTemp.ChildrenNumbers
		and		clf.DependentChildrenNumber = clfTemp.DependentChildrenNumber
		and		clf.ProfessionType = clfTemp.ProfessionType
		and		clf.IsFreelancer = clfTemp.IsFreelancer
		and		clf.Income = clfTemp.Income
		and		clf.IncomeType = clfTemp.IncomeType
		and		clf.Country = clfTemp.Country
		and		clf.Region = clfTemp.Region
		and		clf.InsurancePolicyId = clfTemp.InsurancePolicyId
		and		clf.InsurancePolicyCode = clfTemp.InsurancePolicyCode
		and		clf.InsurancePolicyName = clfTemp.InsurancePolicyName
		select	*
		from	#AppCustomerLearningFeatures
		order	by CustomerId asc
commit tran
GO
select	*
from	AppCustomerLearningFeatures
where	CustomerId is null

