using ConsoleAppCaricamentoDati.Models;
using DAL;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Builder
{
    public class GreatInterventionsInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private const string _basePath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\ConsoleAppCaricamentoDati\DatiBase\";
        private const string _lastNamePath = $"{_basePath}Cognomi.txt";
        private const string _firstNameMalePath = $"{_basePath}NomiMaschili.txt";
        private const string _firstNameFemalePath = $"{_basePath}NomiFemminili.txt";

        private IList<string> _lastNames { get; set; }
        private IList<FirstNameTemplate> _firstNameTemplates { get; set; }

        private readonly IList<KinshipRelationshipType> _kinshipRelationshipTypes;

        public GreatInterventionsInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) : 
            base(insurancePolicyCategory, customer, unitOfWork)
        {
            using (var reader = new StreamReader(_lastNamePath))
                _lastNames = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < _lastNames.Count; i++)
                _lastNames[i] = _lastNames[i].Replace('\r', ' ').Trim();

            using (var reader = new StreamReader(_firstNameMalePath))
                _firstNameTemplates = reader.ReadToEnd()
                                            .Split('\n').Select(x =>
                                            new FirstNameTemplate
                                            {
                                                FirstName = x.Replace('\r', ' ').Trim(),
                                                IsMan = true
                                            }
                                            ).ToList();

            using (var reader = new StreamReader(_firstNameFemalePath))
                ((List<FirstNameTemplate>)_firstNameTemplates).AddRange(
                                    reader.ReadToEnd()
                                            .Split('\n').Select(x =>
                                            new FirstNameTemplate
                                            {
                                                FirstName = x.Replace('\r', ' ').Trim(),
                                                IsMan = false
                                            }
                                            ).ToList());

            _kinshipRelationshipTypes = unitOfWork.KinshipRelationshipTypes.GetAll();
        }

        protected override InsurancePolicy NewInsurancePolicy() => new HealthInsurancePolicy();

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = false;
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            var indexLastName = Random.Next(0, _lastNames.Count);
            var indexFirstName = Random.Next(0, _firstNameTemplates.Count);
            var indexKinshipRelationship = Random.Next(0, _kinshipRelationshipTypes.Count);
            var age = Random.Next(0, 20);
            var days = Random.Next(1, 365);
            var birthDate = DateTime.Now.AddYears(-age).AddDays(-days);

            ((HealthInsurancePolicy)InsurancePolicy).LastName = _lastNames[indexLastName];
            ((HealthInsurancePolicy)InsurancePolicy).FirstName = _firstNameTemplates[indexFirstName].FirstName;
            ((HealthInsurancePolicy)InsurancePolicy).Gender = _firstNameTemplates[indexFirstName].IsMan ? EnumGender.Uomo : EnumGender.Donna;
            ((HealthInsurancePolicy)InsurancePolicy).KinshipRelationshipType = _kinshipRelationshipTypes[indexKinshipRelationship];
            ((HealthInsurancePolicy)InsurancePolicy).BirthDate = birthDate;
            ((HealthInsurancePolicy)InsurancePolicy).TipologiaPrestazione = Random.NextDouble().ToString();

            return this;
        }
    }
}
