using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exstensions
{
    public static class VehicleExstension
    {
        public static Vehicle SetConfigurationModel(this Vehicle vehicle, Random random, IList<ConfigurationModel> configurationModels)
        {
            var i = random.Next(0, configurationModels.Count);
            vehicle.ConfigurationModel = configurationModels[i];
            return vehicle;
        }

        public static Vehicle SetLicensePlate(this Vehicle vehicle, Random random)
        {
            vehicle.LicensePlate = Utilities.GenerateLicensePlate(random);
            return vehicle;
        }

        public static Vehicle SetCommercialValue(this Vehicle vehicle, Random random)
        {
            vehicle.CommercialValue = 25000 * (1 + random.NextDouble());
            return vehicle;
        }

        public static Vehicle SetInsuredValue(this Vehicle vehicle)
        {
            vehicle.InsuredValue = vehicle.CommercialValue * 1.15;
            return vehicle;
        }

        public static Vehicle SetRiskCategory(this Vehicle vehicle, Random random)
        {
            vehicle.RiskCategory = (byte)random.Next(1, 14);
            return vehicle;
        }
    }

}
