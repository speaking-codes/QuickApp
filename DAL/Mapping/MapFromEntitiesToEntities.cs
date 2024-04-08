using DAL.Models;
using MachineLearningModel;
using System.Collections.Generic;

namespace DAL.Mapping
{
    public static class MapFromEntitiesToEntities
    {
        //public static RegressionModel ToRegressionPredictionModel(this LearningCustomerPreferences learningCustomerPreferences)
        //{
        //    var model = new RegressionModel();
        //    model.UserId = learningCustomerPreferences.UserId;
        //    model.Gender = learningCustomerPreferences.Gender;
        //    model.Age = learningCustomerPreferences.Age;
        //    model.MaritalStatusId = learningCustomerPreferences.MaritalStatusId;
        //    model.FamilyTypeId = learningCustomerPreferences.FamilyTypeId;
        //    model.ChildrenNumbers = learningCustomerPreferences.ChildrenNumbers;
        //    model.IncomeTypeId = learningCustomerPreferences.IncomeTypeId;
        //    model.ProfessionTypeId = learningCustomerPreferences.ProfessionTypeId;
        //    model.Income = (float)learningCustomerPreferences.Income;
        //    model.RegionId = learningCustomerPreferences.RegionId;
        //    model.InsurancePolicyCategoryId = learningCustomerPreferences.InsurancePolicyCategoryId;

        //    return model;
        //}

        //public static IList<RegressionModel> ToRegressionPredictionModel(this IEnumerable<LearningCustomerPreferences> learningCustomerPreferences)
        //{
        //    var models = new List<RegressionModel>();
        //    foreach(var item in learningCustomerPreferences)
        //        models.Add(item.ToRegressionPredictionModel());

        //    return models;
        //}

        public static ClassificationLearningModel.ModelInput ToClassificationModelInput(this LearningCustomerPreferences learningCustomerPreferences)
        {
            var model = new ClassificationLearningModel.ModelInput();
            model.UserId = learningCustomerPreferences.UserId;
            model.Gender = learningCustomerPreferences.Gender;
            model.Age = learningCustomerPreferences.Age ?? 0;
            model.MaritalStatus = learningCustomerPreferences.MaritalStatus;
            model.FamilyType = learningCustomerPreferences.FamilyType;
            model.ChildrenNumbers = learningCustomerPreferences.ChildrenNumbers ?? 0;
            model.IncomeType = learningCustomerPreferences.IncomeType;
            model.ProfessionType = learningCustomerPreferences.ProfessionType;
            model.Income = (float)(learningCustomerPreferences.Income ?? 0);
            model.Region = learningCustomerPreferences.Region;
            model.InsurancePolicyCategory = learningCustomerPreferences.InsurancePolicyCategory;

            return model;
        }

        public static IList<ClassificationLearningModel.ModelInput> ToClassificationModelInputCollection(this IEnumerable<LearningCustomerPreferences> learningCustomerPreferences)
        {
            var models = new List<ClassificationLearningModel.ModelInput>();
            foreach (var item in learningCustomerPreferences)
                models.Add(item.ToClassificationModelInput());

            return models;
        }
    }
}
