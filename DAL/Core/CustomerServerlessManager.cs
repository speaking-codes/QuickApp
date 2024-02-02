using DAL.Core.Interfaces;
using DAL.Mapping;
using DAL.Models;
using DAL.ModelsRabbitMQ;
using DAL.Repositories.Interfaces;
using DAL.RepositoryNoSql.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class CustomerServerlessManager : ICustomerServerlessManager
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IInsurancePolicyCategoryRepository _insurancePolicyCategory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICustomerHeaderRepository _customerHeaderRepositoryNoSql;
        private readonly ICustomerDetailRepository _customerDetailRepositoryNoSql;

        public CustomerServerlessManager(ICustomerRepository customerRepository,
                                         IInsurancePolicyCategoryRepository insurancePolicyCategory,
                                         IUnitOfWork unitOfWork,
                                         ICustomerHeaderRepository customerRepositoryNoSql,
                                         ICustomerDetailRepository customerDetailRepositoryNoSql)
        {
            _customerRepository = customerRepository;
            _insurancePolicyCategory = insurancePolicyCategory;
            _unitOfWork = unitOfWork;

            _customerHeaderRepositoryNoSql = customerRepositoryNoSql;
            _customerDetailRepositoryNoSql = customerDetailRepositoryNoSql;
        }

        public void ManageCustomer(CustomerQueue customerQueue)
        {
            try
            {
                CustomerNoSqlManager customerNoSqlManager = null;
                switch (customerQueue.PublishQueueType)
                {
                    case Enums.EnumPublishQueueType.Created:
                        customerNoSqlManager = new CustomerNoSqlManagerAdded(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                        break;
                    case Enums.EnumPublishQueueType.Updated:
                        customerNoSqlManager = new CustomerNoSqlManagerUpdated(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                        break;
                    case Enums.EnumPublishQueueType.Deleted:
                        customerNoSqlManager = new CustomerNoSqlManagerDeleted(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                        break;
                    default:
                        customerNoSqlManager = new CustomerNoSqlManagerNoObject(_customerRepository, _customerHeaderRepositoryNoSql, _customerDetailRepositoryNoSql, customerQueue);
                        break;
                }
                customerNoSqlManager.Execute();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public void ManageInsurancePolicy(CustomerInsurancePolicyQueue customerInsurancePolicyQueue)
        {
            try
            {
                var customer = _customerRepository.GetCustomer(customerInsurancePolicyQueue.CustomerCode).SingleOrDefault();
                var insurancePolicyCategory = _insurancePolicyCategory.GetInsurancePolicyCategory(customerInsurancePolicyQueue.InsurancePolicyCategoryCode).SingleOrDefault();

                _unitOfWork.BeginTransaction();

                var customerInsurancePolicyRating = _unitOfWork.CustomerInsuranceCategoryPolicyRatings.GetCustomerInsuranceCategoryPolicyRatings(customer.CustomerCode, insurancePolicyCategory.InsurancePolicyCategoryCode).SingleOrDefault();
                if (customerInsurancePolicyRating != null)
                {
                    customerInsurancePolicyRating.Age = customer.BirthDate.HasValue ? (int)(DateTime.Now.Subtract(customer.BirthDate.Value).Days / 365) : 0;
                    customerInsurancePolicyRating.ChildrenNumbers = customer.ChildrenNumber ?? 0;
                    customerInsurancePolicyRating.ContractType = customer.ContractType.HasValue ? (byte)customer.ContractType : (byte)0;
                    customerInsurancePolicyRating.Gender = (byte)customer.Gender;
                    customerInsurancePolicyRating.JobTitle = customer.JobTitle;
                    customerInsurancePolicyRating.MaritalStatus = customer.MaritalStatus.HasValue ? (byte)customer.MaritalStatus : (byte)0;
                    customerInsurancePolicyRating.Ral = customer.Income ?? 0;
                    customerInsurancePolicyRating.Rating += 1;
                    _unitOfWork.CustomerInsuranceCategoryPolicyRatings.Update(customerInsurancePolicyRating);
                }
                else
                {
                    customerInsurancePolicyRating = new CustomerInsuranceCategoryPolicyRating
                    {
                        CustomerCode = customer.CustomerCode,
                        Age = customer.BirthDate.HasValue ? (int)(DateTime.Now.Subtract(customer.BirthDate.Value).Days / 365) : 0,
                        ChildrenNumbers = customer.ChildrenNumber ?? 0,
                        ContractType = customer.ContractType.HasValue ? (byte)customer.ContractType : (byte)0,
                        Gender = (byte)customer.Gender,
                        JobTitle = customer.JobTitle,
                        MaritalStatus = customer.MaritalStatus.HasValue ? (byte)customer.MaritalStatus : (byte)0,
                        Ral = customer.Income ?? 0,
                        InsuranceCategoryPolicyCode = insurancePolicyCategory.InsurancePolicyCategoryCode,
                        Rating = 1
                    };
                    _unitOfWork.CustomerInsuranceCategoryPolicyRatings.Add(customerInsurancePolicyRating);
                }

                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
