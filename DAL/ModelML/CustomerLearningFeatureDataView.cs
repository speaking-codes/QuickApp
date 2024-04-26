using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelML
{
    public class CustomerLearningFeatureDataView
    {
        [ColumnName("CustomerId"), LoadColumn(0)]
        public float CustomerId { get; set; }

        [ColumnName("Gender"), LoadColumn(1)]
        public string Gender { get; set; }

        [ColumnName("BirthMonth"), LoadColumn(2)]
        public string BirthMonth { get; set; }

        [ColumnName("YearBirth"), LoadColumn(3)]
        public string YearBirth { get; set; }

        [ColumnName("MaritalStatus"), LoadColumn(4)]
        public string MaritalStatus { get; set; }

        [ColumnName("IsSingle"), LoadColumn(5)]
        public bool IsSingle { get; set; }

        [ColumnName("IsDependentSpouse"), LoadColumn(6)]
        public bool IsDependentSpouse { get; set; }

        [ColumnName("ChildrenNumbers"), LoadColumn(7)]
        public float ChildrenNumbers { get; set; }

        [ColumnName("DependentChildrenNumber"), LoadColumn(8)]
        public float DependentChildrenNumber { get; set; }

        //[ColumnName("ProfessionType"), LoadColumn(9)]
        //public string ProfessionType { get; set; }

        [ColumnName("IsFreelancer"), LoadColumn(9)]
        public bool IsFreelancer { get; set; }

        //[ColumnName("IncomeClassType"), LoadColumn(11)]
        //public string IncomeClassType { get; set; }

        //[ColumnName("IncomeType"), LoadColumn(12)]
        //public string IncomeType { get; set; }

        //[ColumnName("Country"), LoadColumn(13)]
        //public string Country { get; set; }

        //[ColumnName("Region"), LoadColumn(14)]
        //public string Region { get; set; }

        [ColumnName("InsurancePolicyId"), LoadColumn(10)]
        public byte InsurancePolicyId { get; set; }

        //[ColumnName("InsurancePolicyCode"), LoadColumn(16)]
        //public string InsurancePolicyCode { get; set; }

        //[ColumnName("InsurancePolicyName"), LoadColumn(17)]
        //public string InsurancePolicyName { get; set; }

        //[ColumnName("InsurancePolicyDescription"), LoadColumn(18)]
        //public string InsurancePolicyDescription { get; set; }

        //[ColumnName("WarrantyAvaibles"), LoadColumn(19)]
        //public string WarrantyAvaibles { get; set; }
    }

    public class InsurancePrediction
    {
        [ColumnName("CustomerId"), LoadColumn(0)]
        public float CustomerId { get; set; }

        [ColumnName("GenderEncoded"), LoadColumn(1)]
        public float[] Gender { get; set; }

        [ColumnName("BirthMonthEncoded"), LoadColumn(2)]
        public float[] BirthMonth { get; set; }

        [ColumnName("YearBirthEncoded"), LoadColumn(3)]
        public float[] YearBirth { get; set; }

        [ColumnName("MaritalStatusEncoded"), LoadColumn(4)]
        public float[] MaritalStatus { get; set; }

        [ColumnName("IsSingleEncoded"), LoadColumn(5)]
        public float[] IsSingle { get; set; }

        [ColumnName("IsDependentSpouseEncoded"), LoadColumn(6)]
        public float[] IsDependentSpouse { get; set; }

        [ColumnName("ChildrenNumbers"), LoadColumn(7)]
        public float ChildrenNumbers { get; set; }

        [ColumnName("DependentChildrenNumber"), LoadColumn(8)]
        public float DependentChildrenNumber { get; set; }

        [ColumnName("IsFreelancerEncoded"), LoadColumn(9)]
        public float[] IsFreelancer { get; set; }

        [ColumnName("InsurancePolicyId"), LoadColumn(10)]
        public byte InsurancePolicyId { get; set; }

        [ColumnName("PredictedInsurancePolicyId")]
        public byte PredictedInsurancePolicyId { get; set; }

        [ColumnName("Probability")]
        public float Probability { get; set; }

        [ColumnName("Score")]
        public float Score { get; set; }
    }
}
