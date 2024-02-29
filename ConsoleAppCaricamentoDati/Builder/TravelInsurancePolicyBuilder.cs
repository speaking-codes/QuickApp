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
    public class TravelInsurancePolicyBuilder : InsurancePolicyBuilder
    {
        private readonly IList<TravelMeansType> _travelMeansTypes;
        private readonly IList<Municipality> _municipalities;
        private readonly IList<Municipality> _airportMunicipalities;
        private readonly IList<ConfigurationModel> _configurationModels;

        public TravelInsurancePolicyBuilder(InsurancePolicyCategory insurancePolicyCategory, Customer customer, IUnitOfWork unitOfWork) :
            base(insurancePolicyCategory, customer, unitOfWork)
        {
            _travelMeansTypes = UnitOfWork.TravelMeansTypes
                                          .GetTravelMeansTypes()
                                          .ToList();

            _municipalities = UnitOfWork.Municipalities
                                        .GetMunicipalities()
                                        .ToList();

            _airportMunicipalities = _municipalities.Where(x => x.HasAirport).ToList();

            _configurationModels = UnitOfWork.ConfigurationModels
                                             .GetConfigurationModels()
                                             .Where(x => !x.ModelType.IsByke && !x.Model.Brand.BrandType.IsByke)
                                             .ToList();
        }

        protected override InsurancePolicy NewInsurancePolicy()
        {
            InsurancePolicy = new InsurancePolicy();
            InsurancePolicy.Travels = new List<Travel>();
            return InsurancePolicy;
        }

        public override InsurancePolicyBuilder SetIsLuxuryPolicy()
        {
            InsurancePolicy.IsLuxuryPolicy = InsurancePolicy.Travels.Any(x => x.TravelClassType.IsTravelClassPremium);
            return this;
        }

        public override InsurancePolicyBuilder SetDetailItem()
        {
            var itemCount = Random.Next(1, 3);

            for (var i = 0; i < itemCount; i++)
            {
                var indexMeansType = Random.Next(0, _travelMeansTypes.Count);
                var indexClassType = Random.Next(0, _travelMeansTypes[indexMeansType].TravelClassTypes.Count);
                var indexConfigurationModel = Random.Next(0, _configurationModels.Count);

                var indexDepartureMunicipality = Random.Next(0, _municipalities.Count);
                var indexArrivalMunicipality = Random.Next(0, _municipalities.Count);
                while (indexDepartureMunicipality == indexArrivalMunicipality)
                    indexArrivalMunicipality = Random.Next(0, _municipalities.Count);

                var indexDepartureAirportMunicipality = Random.Next(0, _airportMunicipalities.Count);
                var indexArrivalAirportMunicipality = Random.Next(0, _airportMunicipalities.Count);
                while (indexDepartureAirportMunicipality == indexArrivalAirportMunicipality)
                    indexArrivalAirportMunicipality = Random.Next(0, _airportMunicipalities.Count);

                var travel = new Travel
                {
                    TravelMeansType = _travelMeansTypes[indexMeansType],
                };

                if (travel.TravelMeansType.Id == (byte)EnumTravelMeansType.CarSharing)
                {
                    travel.ConfigurationModel = _configurationModels[indexConfigurationModel];

                    switch (travel.ConfigurationModel.Model.Brand.BrandType.Id)
                    {
                        case 1: //BASE
                            travel.TravelClassType = _travelMeansTypes[indexMeansType].TravelClassTypes.Where(x => x.Id == 13).FirstOrDefault();//Classe Standard
                            break;
                        case 3: //PLUS
                            travel.TravelClassType = _travelMeansTypes[indexMeansType].TravelClassTypes.Where(x => x.Id == 14).FirstOrDefault();//Classe Plus
                            break;
                        case 2: //LUSSO
                        case 4: //SUPER LUSSO
                            travel.TravelClassType = _travelMeansTypes[indexMeansType].TravelClassTypes.Where(x => x.Id == 15).FirstOrDefault();//Classe Premium
                            break;
                        default:
                            travel.TravelClassType = _travelMeansTypes[indexMeansType].TravelClassTypes.Where(x => x.Id == 13).FirstOrDefault();//Classe Standard
                            break;
                    }
                }
                else
                {
                    travel.TravelClassType = _travelMeansTypes[indexMeansType].TravelClassTypes[indexClassType];
                }

                if (travel.TravelMeansType.Id == (byte)EnumMeansTransportType.Airplane)
                {
                    travel.DepartureMunicipality = _airportMunicipalities[indexDepartureAirportMunicipality];
                    travel.ArrivalMunicipality = _airportMunicipalities[indexArrivalAirportMunicipality];
                }
                else
                {
                    travel.DepartureMunicipality = _municipalities[indexDepartureMunicipality];
                    travel.ArrivalMunicipality = _municipalities[indexArrivalMunicipality];
                }

                var totalDays = (int)InsurancePolicy.IssueDate.Subtract(DateTime.Now).TotalDays;
                travel.DepartureDate = DateTime.Now.AddDays(Random.Next(-totalDays + 5, totalDays + 60));
                travel.ArrivalDate = travel.DepartureDate.AddDays(2);
                InsurancePolicy.Travels.Add(travel);
            }
            return this;
        }
    }
}
