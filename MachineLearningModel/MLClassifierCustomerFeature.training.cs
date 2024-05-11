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
    public partial class MLClassifierCustomerFeature
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
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new []{new InputOutputColumnPair(@"Gender", @"Gender"),new InputOutputColumnPair(@"BirthMonth", @"BirthMonth"),new InputOutputColumnPair(@"YearBirth", @"YearBirth"),new InputOutputColumnPair(@"MaritalStatus", @"MaritalStatus"),new InputOutputColumnPair(@"IsSingle", @"IsSingle"),new InputOutputColumnPair(@"IsDependentSpouse", @"IsDependentSpouse"),new InputOutputColumnPair(@"ProfessionType", @"ProfessionType"),new InputOutputColumnPair(@"IsFreelancer", @"IsFreelancer"),new InputOutputColumnPair(@"IncomeClassType", @"IncomeClassType"),new InputOutputColumnPair(@"IncomeType", @"IncomeType"),new InputOutputColumnPair(@"Country", @"Country"),new InputOutputColumnPair(@"Region", @"Region"),new InputOutputColumnPair(@"InsurancePolicyCode", @"InsurancePolicyCode"),new InputOutputColumnPair(@"InsurancePolicyName", @"InsurancePolicyName")}, outputKind: OneHotEncodingEstimator.OutputKind.Indicator)      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"ChildrenNumbers", @"ChildrenNumbers"),new InputOutputColumnPair(@"DependentChildrenNumber", @"DependentChildrenNumber")}))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Gender",@"BirthMonth",@"YearBirth",@"MaritalStatus",@"IsSingle",@"IsDependentSpouse",@"ProfessionType",@"IsFreelancer",@"IncomeClassType",@"IncomeType",@"Country",@"Region",@"InsurancePolicyCode",@"InsurancePolicyName",@"ChildrenNumbers",@"DependentChildrenNumber"}))      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName:@"InsurancePolicyId",inputColumnName:@"InsurancePolicyId"))      
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))      
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator: mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression(new LbfgsLogisticRegressionBinaryTrainer.Options(){L1Regularization=1F,L2Regularization=1F,LabelColumnName=@"InsurancePolicyId",FeatureColumnName=@"Features"}), labelColumnName:@"InsurancePolicyId"))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName:@"PredictedLabel",inputColumnName:@"PredictedLabel"));

            return pipeline;
        }
    }
}