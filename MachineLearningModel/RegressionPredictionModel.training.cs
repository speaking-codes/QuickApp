﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Microsoft.ML;

namespace MachineLearningModel
{
    public partial class RegressionPredictionModel
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(@"Gender", @"Gender", outputKind: OneHotEncodingEstimator.OutputKind.Indicator)      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"CustomerId", @"CustomerId"),new InputOutputColumnPair(@"Age", @"Age"),new InputOutputColumnPair(@"MaritalStatusId", @"MaritalStatusId"),new InputOutputColumnPair(@"FamilyTypeId", @"FamilyTypeId"),new InputOutputColumnPair(@"ChildrenNumbers", @"ChildrenNumbers"),new InputOutputColumnPair(@"IncomeTypeId", @"IncomeTypeId"),new InputOutputColumnPair(@"ProfessionTypeId", @"ProfessionTypeId"),new InputOutputColumnPair(@"Income", @"Income"),new InputOutputColumnPair(@"RegionId", @"RegionId"),new InputOutputColumnPair(@"InsurancePolicyCategoryId", @"InsurancePolicyCategoryId")}))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Gender",@"CustomerId",@"Age",@"MaritalStatusId",@"FamilyTypeId",@"ChildrenNumbers",@"IncomeTypeId",@"ProfessionTypeId",@"Income",@"RegionId",@"InsurancePolicyCategoryId"}))      
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))      
                                    .Append(mlContext.Regression.Trainers.LbfgsPoissonRegression(new LbfgsPoissonRegressionTrainer.Options(){L1Regularization=0.03125F,L2Regularization=0.04586596F,LabelColumnName=@"NormalizedRenewalNumber",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}