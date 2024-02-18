using ConsoleAppCaricamentoDati.Models;
using DAL.Core.Interfaces;
using DAL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppCaricamentoDati
{
    internal class CustomerBuilderManager : IDisposable
    {
        private string _basePath => @"C:\Users\mauro.diliddo\source\repos\QuickApp\QuickAppGitHub\QuickApp\ConsoleAppCaricamentoDati\DatiBase\";
        private string _lastNamePath => $"{_basePath}Cognomi.txt";
        private string _firstNameMalePath => $"{_basePath}NomiMaschili.txt";
        private string _firstNameFemalePath => $"{_basePath}NomiFemminili.txt";
        private string _addressPath => $"{_basePath}Indirizzi.txt";
        private string _jobPath => $"{_basePath}Professioni.txt";

        private readonly int _customerCount;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerManager _customerManager;

        public CustomerBuilderManager(IUnitOfWork unitOfWork, ICustomerManager customerManager, int customerCount)
        {
            _unitOfWork = unitOfWork;
            _customerManager = customerManager;
            _customerCount = customerCount;
        }

        public void Run()
        {
            var customerTemplateBuilder = new CustomerBaseTemplateBuilder();
            var customerTemplate = customerTemplateBuilder.SetLastName(_lastNamePath)
                                                          .SetFirstName(_firstNameMalePath, _firstNameFemalePath)
                                                          .SetAddress(_addressPath)
                                                          .SetProviderMail()
                                                          .SetIncomesBase()
                                                          .Build();

            var customBuilder = new CustomerBuilder(_unitOfWork, customerTemplate);

            IList<Customer> customerList = new List<Customer>();

            for (var i = 0; i < _customerCount; i++)
            {
                customerList.Add(customBuilder.SetCustomer()
                                                .SetFirstName()
                                                .SetLastName()
                                                .SetMaritalStatus()
                                                .SetFamilyType()
                                                .SetChildrenNumber()
                                                .SetBirthDate()
                                                .SetBirthPlace()
                                                .SetAddress()
                                                .SetDelivery()
                                                .SetContractType()
                                                .SetJob()
                                                .SetIncome()
                                                .Build());
            }

            _customerManager.BeginTransaction();
            for (var i = 0; i < customerList.Count; i++)
                _customerManager.AddCustomer(customerList[i]);

        }

        public void Dispose()
        {
            _customerManager.Dispose();
            _unitOfWork.Dispose();
        }
    }
}
