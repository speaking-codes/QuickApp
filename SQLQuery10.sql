USE datSampleDataBase
SELECT	InsurancePolicyId, InsurancePolicyName, COUNT(CustomerId) CountCustomer
FROM	AppCustomerLearningFeaturesCopy
GROUP	BY InsurancePolicyId, InsurancePolicyName
ORDER	BY InsurancePolicyId ASC
