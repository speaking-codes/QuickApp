using DAL.BuilderModel.Interfaces;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DAL.BuilderModel
{
    public class AddressBuilder : IAddressBuilder
    {
        private Random _random;
        private Address _address;

        public AddressBuilder()
        {
            _random = new Random();
        }

        public IAddressBuilder SetAddress(Address address)
        {
            address ??= new Address();
            _address = address;
            return this;
        }

        public IAddressBuilder SetAddressType(EnumAddressType addressType)
        {
            _address.AddressType = addressType;
            return this;
        }

        public IAddressBuilder SetLocation(IList<string> streetNames)
        {
            var i = _random.Next(streetNames.Count);
            var houseNumber = _random.Next(1, 5999);
            _address.Location = $"{streetNames[i]}, {houseNumber}";
            return this;
        }

        public IAddressBuilder SetMunicipality(IList<Municipality> municipalities)
        {
            var i = _random.Next(municipalities.Count);
            _address.MunicipalityId = municipalities[i].Id;
            return this;
        }

        public Address Build() => _address;

        public void Dispose()
        {
            _random = null;
            _address = null;
        }
    }
}
