using DAL.Enums;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati.Models
{
    public class CustomerBuilder
    {
        private Customer _customer;
        private Random _random;
        private readonly IList<Municipality> _municipalityList;
        private readonly CustomerBaseTemplate _template;
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private const string charsNumber = "0123456789";

        public CustomerBuilder(ServiceProvider provider, CustomerBaseTemplate template)
        {
            _random = new Random();
            using (var scope = provider.CreateScope())
            {
                var repository = provider.GetService<IRepositoryMunicipality>();
                _municipalityList = repository.GetMunicipalities().ToList();
            }
            _template = template;
        }


        private string generateRandomCode(int length)
        {
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string generatePhoneNumber()
        {
            return new string(Enumerable.Repeat(charsNumber, 10)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private void setFirstNameDefault()
        {
            var i = _random.Next(0, _template.FirstNameTemplates.Count - 1);
            _customer.FirstName = _template.FirstNameTemplates[i].FirstName;
            _customer.Gender = _template.FirstNameTemplates[i].IsMan ? DAL.Enums.EnumGender.Uomo : DAL.Enums.EnumGender.Donna;
        }

        private void setLastNameDefault()
        {
            var i = _random.Next(0, _template.LastNames.Count - 1);
            _customer.LastName = _template.LastNames[i];
        }

        public CustomerBuilder SetCustomer()
        {
            _customer = new Customer() { IsActive = true };
            return this;
        }

        public CustomerBuilder SetLastName()
        {
            setLastNameDefault();
            return this;
        }

        public CustomerBuilder SetFirstName()
        {
            setFirstNameDefault();
            return this;
        }

        public CustomerBuilder SetBirthDate()
        {
            var age = _random.Next(18, 90);
            var days = _random.Next(1, 365);
            _customer.BirthDate = DateTime.Now.AddYears(-age).AddDays(-days);
            return this;
        }

        public CustomerBuilder SetBirthPlace()
        {
            var i = _random.Next(0, _municipalityList.Count - 1);
            _customer.BirthMunicipality = _municipalityList[i];
            return this;
        }

        public CustomerBuilder SetMaritalStatus()
        {
            _customer.MaritalStatus = (EnumMaritalStatus)_random.Next(0, 4);
            return this;
        }

        public CustomerBuilder SetChildrenNumber()
        {
            _customer.ChildrenNumber = (byte)_random.Next(0, 7);
            return this;
        }

        public CustomerBuilder SetJob()
        {
            var i = _random.Next(0, _template.JobTemplates.Count - 1);
            _customer.JobTitle = _template.JobTemplates[i].JobTitle;
            _customer.Income = _template.JobTemplates[i].Ral * (1 + _random.NextDouble());

            i = _random.Next(0, _template.JobContractType.Count - 1);
            _customer.ContractType = (EnumContractType)_template.JobContractType[i];
            return this;
        }

        public CustomerBuilder SetAddress()
        {
            var i = _random.Next(0, _template.AddressTemplates.Count - 1);
            var address = new Address() { AddressType = EnumAddressType.Residenza };
            address.Location = _template.AddressTemplates[i];

            i = _random.Next(0, _municipalityList.Count - 1);
            address.Municipality = _municipalityList[i];

            _customer.Addresses = new List<Address>();
            _customer.Addresses.Add(address);

            return this;
        }

        public CustomerBuilder SetDelivery()
        {
            if (string.IsNullOrEmpty(_customer.LastName))
                setLastNameDefault();

            if (string.IsNullOrEmpty(_customer.FirstName))
                setFirstNameDefault();

            var i = _random.Next(0, _template.ProviderMailTemplates.Count - 1);
            var delivery = new Delivery() { DeliveryType = EnumDeliveryType.Privato };
            delivery.Email = $"{_customer.FirstName.Substring(0, 1)}.{_customer.LastName}{_template.ProviderMailTemplates[i]}";
            delivery.PhoneNumber = generatePhoneNumber();

            _customer.Deliveries = new List<Delivery>();
            _customer.Deliveries.Add(delivery);
            return this;
        }

        public Customer Build() => _customer;
    }
}
