using DAL.Enums;
using DAL.ModelML;
using DAL.Models;
using System.Collections.Generic;

namespace DAL.Mapping
{
    public static class MapFromEntitiesToEntities
    {
        //public static CustomerLearningFeature ToCustomerLearningFeature(this Customer customer)
        //{
        //    var learningFeature = new CustomerLearningFeature();
        //    learningFeature.Gender = customer.Gender.GetCode();
        //    learningFeature.BirthMonth = customer.BirthDate.m;
        //    learningFeature.YearBirth = customer.YearBirth;
        //    learningFeature.MaritalStatus = customer.MaritalStatus;
        //    learningFeature.IsSingle = customer.IsSingle;
        //    learningFeature.IsDependentSpouse = customer.IsDependentSpouse;
        //    learningFeature.ChildrenNumbers = customer.ChildrenNumbers;
        //    learningFeature.DependentChildrenNumber = customer.DependentChildrenNumber;
        //    //model.ProfessionType = customerLearningFeature.ProfessionType;
        //    learningFeature.IsFreelancer = customer.IsFreelancer;
        //    //model.IncomeClassType = customerLearningFeature.IncomeClassType;
        //    //model.IncomeType = customerLearningFeature.IncomeType;
        //    //model.Country = customerLearningFeature.Country;
        //    //model.Region = customerLearningFeature.Region;
        //    learningFeature.InsurancePolicyId = customer.InsurancePolicyId;
        //    //model.InsurancePolicyCode = customerLearningFeature.InsurancePolicyCode;
        //    //model.InsurancePolicyName = customerLearningFeature.InsurancePolicyName;
        //    //model.InsurancePolicyDescription = customerLearningFeature.InsurancePolicyDescription;
        //    //model.WarrantyAvaibles = customerLearningFeature.WarrantyAvaibles;

        //    return learningFeature;
        //}
        
        //public static CustomerLearningFeatureDataView ToDataViewModel(this CustomerLearningFeature customerLearningFeature)
        //{
        //    var model = new CustomerLearningFeatureDataView();
        //    model.CustomerId = customerLearningFeature.CustomerId ?? 0;
        //    model.Gender = customerLearningFeature.Gender;
        //    model.BirthMonth = customerLearningFeature.BirthMonth;
        //    model.YearBirth = customerLearningFeature.YearBirth;
        //    model.MaritalStatus = customerLearningFeature.MaritalStatus;
        //    model.IsSingle = customerLearningFeature.IsSingle;
        //    model.IsDependentSpouse = customerLearningFeature.IsDependentSpouse;
        //    model.ChildrenNumbers = customerLearningFeature.ChildrenNumbers;
        //    model.DependentChildrenNumber = customerLearningFeature.DependentChildrenNumber;
        //    //model.ProfessionType = customerLearningFeature.ProfessionType;
        //    model.IsFreelancer = customerLearningFeature.IsFreelancer;
        //    //model.IncomeClassType = customerLearningFeature.IncomeClassType;
        //    //model.IncomeType = customerLearningFeature.IncomeType;
        //    //model.Country = customerLearningFeature.Country;
        //    //model.Region = customerLearningFeature.Region;
        //    model.InsurancePolicyId = customerLearningFeature.InsurancePolicyId;
        //    //model.InsurancePolicyCode = customerLearningFeature.InsurancePolicyCode;
        //    //model.InsurancePolicyName = customerLearningFeature.InsurancePolicyName;
        //    //model.InsurancePolicyDescription = customerLearningFeature.InsurancePolicyDescription;
        //    //model.WarrantyAvaibles = customerLearningFeature.WarrantyAvaibles;

        //    return model;
        //}

        //public static IList<CustomerLearningFeatureDataView> ToDataViewModels(this IEnumerable<CustomerLearningFeature> customerLearningFeatures)
        //{
        //    var models = new List<CustomerLearningFeatureDataView>();
        //    foreach (var item in customerLearningFeatures)
        //        models.Add(item.ToDataViewModel());

        //    return models;
        //}

        //public static ClassificationLearningModel.ModelInput ToClassificationModelInput(this LearningCustomerPreferences learningCustomerPreferences)
        //{
        //    var model = new ClassificationLearningModel.ModelInput();
        //    model.UserId = learningCustomerPreferences.UserId;
        //    model.Gender = learningCustomerPreferences.Gender;
        //    model.Age = learningCustomerPreferences.Age ?? 0;
        //    model.MaritalStatus = learningCustomerPreferences.MaritalStatus;
        //    model.FamilyType = learningCustomerPreferences.FamilyType;
        //    model.ChildrenNumbers = learningCustomerPreferences.ChildrenNumbers ?? 0;
        //    model.IncomeType = learningCustomerPreferences.IncomeType;
        //    model.ProfessionType = learningCustomerPreferences.ProfessionType;
        //    model.Income = (float)(learningCustomerPreferences.Income ?? 0);
        //    model.Region = learningCustomerPreferences.Region;
        //    model.InsurancePolicyCategory = learningCustomerPreferences.InsurancePolicyCategory;

        //    return model;
        //}

        //public static IList<ClassificationLearningModel.ModelInput> ToClassificationModelInputCollection(this IEnumerable<LearningCustomerPreferences> learningCustomerPreferences)
        //{
        //    var models = new List<ClassificationLearningModel.ModelInput>();
        //    foreach (var item in learningCustomerPreferences)
        //        models.Add(item.ToClassificationModelInput());

        //    return models;
        //}
    }
}
