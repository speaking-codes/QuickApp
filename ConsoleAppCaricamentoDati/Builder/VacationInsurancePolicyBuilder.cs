using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Builder
{
    public class VacationInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private const string _basePath = @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\ConsoleAppCaricamentoDati\DatiBase\";
        private const string _addressStructurePath = $"{_basePath}Indirizzi.txt";
        private const string _structureNamePath = $"{_basePath}/StruttureAlberghiere.txt";

        private IList<string> _structureAddresses { get; set; }
        private IList<string> _structureNames { get; set; }

        private readonly IList<StructureType> _structureTypes;
        private readonly IList<Municipality> _municipalities;

        public VacationInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) :
            base(insurancePolicyCategory, customer, unitOfWork)
        {
            using (var reader = new StreamReader(_addressStructurePath))
                _structureAddresses = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < _structureAddresses.Count; i++)
                _structureAddresses[i] = _structureAddresses[i].Replace('\r', ' ').Trim();

            using (var reader = new StreamReader(_structureNamePath))
                _structureNames = reader.ReadToEnd().Split('\n').ToList();

            for (var i = 0; i < _structureNames.Count; i++)
                _structureNames[i] = _structureNames[i].Replace('\r', ' ').Trim();

            _structureTypes = UnitOfWork.StructureTypes.GetAll();
            _municipalities = unitOfWork.Municipalities.GetAll();
        }

        protected override InsurancePolicy NewInsurancePolicy()
        {
            InsurancePolicy = new InsurancePolicy();
            InsurancePolicy.Vacations = new List<Vacation>();
            return InsurancePolicy;
        }

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = false;
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            var vacation = new Vacation();
            var indexMunicipality = Random.Next(0, _municipalities.Count);
            var indexStructureType = Random.Next(0, _structureTypes.Count);
            var indexStructureName = Random.Next(0, _structureNames.Count);
            var indexStructureAddress=Random.Next(0, _structureAddresses.Count);

            vacation.PlaceStructure = _municipalities[indexMunicipality];
            vacation.StructureType = _structureTypes[indexStructureType];
            vacation.StructureName = _structureNames[indexStructureName];
            vacation.StructureAddres = _structureAddresses[indexStructureAddress];
            vacation.CheckInDate = InsurancePolicy.IssueDate.AddDays(Random.Next(20, 60));
            vacation.CheckOutDate = vacation.CheckInDate.AddDays(Random.Next(2, 15));
            
            InsurancePolicy.Vacations.Add(vacation);
            return this;
        }
    }
}
