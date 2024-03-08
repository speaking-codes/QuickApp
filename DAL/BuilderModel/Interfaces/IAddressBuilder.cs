using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface IAddressBuilder : IDisposable
    {
        IAddressBuilder SetAddress(Address address);
        IAddressBuilder SetAddressType(EnumAddressType addressType);
        IAddressBuilder SetLocation(IList<string> streetNames);
        IAddressBuilder SetMunicipality(IList<Municipality> municipalities);
        Address Build();
    }
}
