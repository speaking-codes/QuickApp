using DAL.Core.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleAppCaricamentoDati.Builder;
using DAL.Models;

namespace ConsoleAppCaricamentoDati.BuilderManager
{
    public abstract class InsurancePolicyBuilderManager //: BuilderManager
    {
        //private readonly IList<Customer> _customerList;

        //public InsurancePolicyBuilderManager(IUnitOfWork unitOfWork, ICustomerManager customerManager) : base(unitOfWork, customerManager)
        //{
        //    _customerList = CustomerManager.GetActiveCustomers();
        //}

        //public override Task Run()
        //{
        //    var random=new Random();
        //    for (var i = 0; i < _customerList.Count; i++)
        //    {
        //        var j = random.Next(0, _customerList.Count);

        //            //var insurancePolicy = insurancePolicyBuilder.SetInsurancePolicy()
        //            //                                              .SetIssueDate()
        //            //                                              .SetExpireDate()
        //            //                                              .SetInsurancePolicyCategory()
        //            //                                              .SetCustomer(customerList[j])
        //            //                                              .SetInsuredMaximum()
        //            //                                              .SetTotalPrize()
        //            //                                              .SetIsLuxuryPolicy()
        //            //                                              .Build();
        //            //switch (insurancePolicy.InsurancePolicyCategory.Id)
        //            //{
        //            //    case 1://Auto
        //            //    case 2://Moto
        //            //    case 3://Imbarcazioni
        //            //        vehicleInsurancePolicyBuilder = new VehicleInsurancePolicyBuilder(provider, insurancePolicy);
        //            //        var vehicleInsurancePolicy = vehicleInsurancePolicyBuilder.SetConfigurationModel()
        //            //                                                                 .SetLicensePlate()
        //            //                                                                 .SetCommercialValue()
        //            //                                                                 .SetInsuredValue()
        //            //                                                                 .SetRiskCategory()
        //            //                                                                 .Build();
        //            //        insurancePolicyList.Add(vehicleInsurancePolicy);
        //            //        break;
        //            //    default:
        //            //        insurancePolicyList.Add(insurancePolicy);
        //            //        break;
        //            //}

        //        }
            
        //    //using (var manager = provider.GetService<IInsurancePolicyManager>())
        //    //{
        //    //    manager.BeginTransaction();
        //    //    for (var i = 0; i < insurancePolicyList.Count; i++)
        //    //        manager.AddInsurancePolicy(insurancePolicyList[i]);
        //    //}
        //    return Task.CompletedTask;
        //}
    }
}
