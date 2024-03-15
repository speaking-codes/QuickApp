using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel.Interfaces
{
    public interface IDeliveryBuilder : IDisposable
    {
        IDeliveryBuilder SetDelivery(Delivery delivery);
        IDeliveryBuilder SetIsPrimary(bool isPrimary);
        IDeliveryBuilder SetDeliveryType(EnumDeliveryType deliveryType);
        IDeliveryBuilder SetEmail(IList<string> providerMails, string lastName, string firstName);
        IDeliveryBuilder SetPhoneNumber();

        Delivery Build();
    }
}
