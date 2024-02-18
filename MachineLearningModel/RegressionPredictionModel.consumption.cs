﻿// This file was auto-generated by ML.NET Model Builder. 
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
namespace MachineLearningModel
{
    public partial class RegressionPredictionModel
    {
        /// <summary>
        /// model input class for RegressionPredictionModel.
        /// </summary>
        #region model input class
        public class ModelInput
        {
            [ColumnName(@"Id")]
            public float Id { get; set; }

            [ColumnName(@"CustomerId")]
            public float CustomerId { get; set; }

            [ColumnName(@"Gender")]
            public string Gender { get; set; }

            [ColumnName(@"Age")]
            public float Age { get; set; }

            [ColumnName(@"MaritalStatusId")]
            public float MaritalStatusId { get; set; }

            [ColumnName(@"FamilyTypeId")]
            public float FamilyTypeId { get; set; }

            [ColumnName(@"ChildrenNumbers")]
            public float ChildrenNumbers { get; set; }

            [ColumnName(@"IncomeTypeId")]
            public float IncomeTypeId { get; set; }

            [ColumnName(@"ProfessionTypeId")]
            public float ProfessionTypeId { get; set; }

            [ColumnName(@"Income")]
            public float Income { get; set; }

            [ColumnName(@"RegionId")]
            public float RegionId { get; set; }

            [ColumnName(@"InsurancePolicyCategoryId")]
            public float InsurancePolicyCategoryId { get; set; }

            [ColumnName(@"RenewalNumber")]
            public float RenewalNumber { get; set; }

            [ColumnName(@"NormalizedRenewalNumber")]
            public float NormalizedRenewalNumber { get; set; }

        }

        #endregion

        /// <summary>
        /// model output class for RegressionPredictionModel.
        /// </summary>
        #region model output class
        public class ModelOutput
        {
            [ColumnName(@"Id")]
            public float Id { get; set; }

            [ColumnName(@"CustomerId")]
            public float CustomerId { get; set; }

            [ColumnName(@"Gender")]
            public float[] Gender { get; set; }

            [ColumnName(@"Age")]
            public float Age { get; set; }

            [ColumnName(@"MaritalStatusId")]
            public float MaritalStatusId { get; set; }

            [ColumnName(@"FamilyTypeId")]
            public float FamilyTypeId { get; set; }

            [ColumnName(@"ChildrenNumbers")]
            public float ChildrenNumbers { get; set; }

            [ColumnName(@"IncomeTypeId")]
            public float IncomeTypeId { get; set; }

            [ColumnName(@"ProfessionTypeId")]
            public float ProfessionTypeId { get; set; }

            [ColumnName(@"Income")]
            public float Income { get; set; }

            [ColumnName(@"RegionId")]
            public float RegionId { get; set; }

            [ColumnName(@"InsurancePolicyCategoryId")]
            public float InsurancePolicyCategoryId { get; set; }

            [ColumnName(@"RenewalNumber")]
            public float RenewalNumber { get; set; }

            [ColumnName(@"NormalizedRenewalNumber")]
            public float NormalizedRenewalNumber { get; set; }

            [ColumnName(@"Features")]
            public float[] Features { get; set; }

            [ColumnName(@"Score")]
            public float Score { get; set; }

        }

        #endregion

        private static string MLNetModelPath = Path.GetFullPath("RegressionPredictionModel.zip");

        public static readonly Lazy<PredictionEngine<ModelInput, ModelOutput>> PredictEngine = new Lazy<PredictionEngine<ModelInput, ModelOutput>>(() => CreatePredictEngine(), true);

        /// <summary>
        /// Use this method to predict on <see cref="ModelInput"/>.
        /// </summary>
        /// <param name="input">model input.</param>
        /// <returns><seealso cref=" ModelOutput"/></returns>
        public static ModelOutput Predict(ModelInput input)
        {
            var predEngine = PredictEngine.Value;
            return predEngine.Predict(input);
        }

        private static PredictionEngine<ModelInput, ModelOutput> CreatePredictEngine()
        {
            var mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(MLNetModelPath, out var _);
            return mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
        }
    }
}