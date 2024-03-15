using DAL.BuilderModel.Interfaces;
using DAL.Enums;
using DAL.Helpers;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.BuilderModel
{
    public class DeliveryBuilder : IDeliveryBuilder
    {
        private Random _random;
        private Delivery _delivery;

        public DeliveryBuilder()
        {
            _random = new Random();
        }

        public IDeliveryBuilder SetDelivery(Delivery delivery)
        {
            delivery ??= new Delivery();
            _delivery= delivery;
            return this;
        }
        public IDeliveryBuilder SetIsPrimary(bool isPrimary)
        {
            _delivery.IsPrimary = isPrimary;
            return this;
        }
        public IDeliveryBuilder SetDeliveryType(EnumDeliveryType deliveryType)
        {
            _delivery.DeliveryType = deliveryType;
            return this;
        }

        public IDeliveryBuilder SetEmail(IList<string> providerMails, string lastName, string firstName)
        {
            var i = _random.Next(providerMails.Count);
            _delivery.Email = Utilities.GetEmail(providerMails[i], lastName, firstName, _random);
            return this;
        }

        public IDeliveryBuilder SetPhoneNumber()
        {
            _delivery.PhoneNumber = Utilities.GetPhoneNumber(_random);
            return this;
        }

        public Delivery Build() => _delivery;

        public void Dispose()
        {
            _random = null;
            _delivery = null;
        }
    }
}
