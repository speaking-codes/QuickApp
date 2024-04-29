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
        public static string GetVehicleDescription(this Vehicle vehicle)
        {
            var sb = new StringBuilder(string.Empty);
            sb.Append($"Targa: {vehicle.LicensePlate}");
            sb.Append($" - {vehicle.ConfigurationModel.Model.Brand.BrandName}");
            sb.Append($" - {vehicle.ConfigurationModel.Model.ModelName}");
            sb.Append($"  - {vehicle.ConfigurationModel.ConfigurationDescription}");
            return sb.ToString();
        }
        public static IList<string> GetVehicleDescriptions(this IEnumerable<Vehicle> vehicles)
        {
            var vehicleDescriptions=new List<string>();
            foreach(var item in vehicles)
                vehicleDescriptions.Add(item.GetVehicleDescription());
            return vehicleDescriptions;
        }
    }
}
