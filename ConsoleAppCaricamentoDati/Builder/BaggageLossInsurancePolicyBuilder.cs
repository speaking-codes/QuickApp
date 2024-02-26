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
    public class BaggageLossInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private readonly IList<BaggageType> _baggageTypes;

        public BaggageLossInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) :
            base(insurancePolicyCategory, customer, unitOfWork)
        {
            _baggageTypes = UnitOfWork.BaggageTypes.GetAll();
        }

        protected override InsurancePolicy NewInsurancePolicy()
        {
            InsurancePolicy = new InsurancePolicy();
            InsurancePolicy.BaggageLosses = new List<BaggageLoss>();
            return InsurancePolicy;
        }

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = false;
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            var itemCount = Random.Next(1, 3);
            for (var i = 0; i < itemCount; i++)
            {

                var index = Random.Next(0, _baggageTypes.Count);
                var enumBaggaheType = (EnumBaggageType)_baggageTypes[index].Id;
                var baggageLoss = new BaggageLoss
                {
                    BaggageType = _baggageTypes[index]
                };

                switch (enumBaggaheType)
                {
                    case EnumBaggageType.Zainodaviaggiopiccolo:
                        baggageLoss.HeightMetres = 0.5;
                        baggageLoss.LengthMetres = 0.35;
                        baggageLoss.WidthMetres = 0.25;
                        baggageLoss.WeightKg = 1.5;
                        break;
                    case EnumBaggageType.Zainodaviaggiomedio:
                        baggageLoss.HeightMetres = 0.6;
                        baggageLoss.LengthMetres = 0.4;
                        baggageLoss.HeightMetres = 0.3;
                        baggageLoss.WeightKg = 2.5;
                        break;
                    case EnumBaggageType.Zainodaviaggiogrande:
                        baggageLoss.HeightMetres = 0.8;
                        baggageLoss.LengthMetres = 0.6;
                        baggageLoss.HeightMetres = 0.4;
                        baggageLoss.WeightKg = 5;
                        break;
                    case EnumBaggageType.Zainodacampeggio:
                        baggageLoss.HeightMetres = 0.8;
                        baggageLoss.LengthMetres = 0.45;
                        baggageLoss.HeightMetres = 0.35;
                        baggageLoss.WeightKg = 3.5;
                        break;
                    case EnumBaggageType.Valigia:
                        baggageLoss.HeightMetres = 0.8;
                        baggageLoss.LengthMetres = 0.6;
                        baggageLoss.HeightMetres = 0.4;
                        baggageLoss.WeightKg = 8;
                        break;
                    case EnumBaggageType.Trolley:
                        baggageLoss.HeightMetres = 0.7;
                        baggageLoss.LengthMetres = 0.5;
                        baggageLoss.HeightMetres = 0.35;
                        baggageLoss.WeightKg = 6;
                        break;
                    case EnumBaggageType.Borsone:
                        baggageLoss.HeightMetres = 0.4;
                        baggageLoss.LengthMetres = 0.6;
                        baggageLoss.HeightMetres = 0.3;
                        baggageLoss.WeightKg = 2.5;
                        break;
                }

                InsurancePolicy.BaggageLosses.Add(baggageLoss);
            }
            return this;
        }
    }
}
