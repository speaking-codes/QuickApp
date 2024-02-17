﻿using DAL.Core.Interfaces;
using DAL.Mapping;
using DAL.Models;
using DAL.ModelsNoSql;
using DAL.RepositoryNoSql.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DAL.Core
{
    public class DashboardManager : Manager, IDashboardManager
    {
        private readonly ICustomerHeaderRepository _customerHeaderRepository;
        private readonly ICustomerDetailRepository _customerDetailRepository;
        private readonly IInsuranceCategoryPolicyDashboardCardRepository _insuranceCategoryPolicyDashboardCardRepository;
        private readonly IInsuranceCategoryPolicyTopSellingRepository _insuranceCategoryPolicyTopSellingRepository;
        private readonly IInsuranceCoverageSummaryRepository _insuranceCoverageSummaryRepository;
        private readonly IInsuranceCoverageChartRepository _insuranceCoverageChartRepository;

        public DashboardManager(IUnitOfWork unitOfWork,
                                ICustomerHeaderRepository customerHeaderRepository,
                                ICustomerDetailRepository customerDetailRepository,
                                IInsuranceCategoryPolicyDashboardCardRepository insuranceCategoryPolicyDashboardCardRepository,
                                IInsuranceCategoryPolicyTopSellingRepository insuranceCategoryPolicyTopSellingRepository,
                                IInsuranceCoverageSummaryRepository insuranceCoverageSummaryRepository,
                                IInsuranceCoverageChartRepository insuranceCoverageChartRepository) : base(unitOfWork)
        {
            _customerHeaderRepository = customerHeaderRepository;
            _customerDetailRepository = customerDetailRepository;
            _insuranceCategoryPolicyDashboardCardRepository = insuranceCategoryPolicyDashboardCardRepository;
            _insuranceCategoryPolicyTopSellingRepository = insuranceCategoryPolicyTopSellingRepository;
            _insuranceCoverageSummaryRepository = insuranceCoverageSummaryRepository;
            _insuranceCoverageChartRepository = insuranceCoverageChartRepository;
        }

        public CustomerHeader GetCustomerHeader(string customerCode)
        {
            var customerHeader = _customerHeaderRepository.GetCustomer(customerCode);
            if (customerHeader != null)
                return customerHeader;

            var customer = UnitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();
            if (customer == null)
                return new CustomerHeader();

            customerHeader = customer.ToNoSqlHeaderEntity();
            _customerHeaderRepository.InsertOne(customerHeader);

            return customerHeader;
        }

        public CustomerDetail GetCustomerDetail(string customerCode)
        {
            var customerDetail = _customerDetailRepository.GetCustomer(customerCode);
            if (customerDetail != null)
                return customerDetail;

            var customer = UnitOfWork.Customers.GetCustomer(customerCode).FirstOrDefault();
            if (customer == null)
                return new CustomerDetail();

            customerDetail = customer.ToNoSqlDetailEntity();
            _customerDetailRepository.InsertOne(customerDetail);

            return customerDetail;
        }

        public IList<SalesLineChart> GetSalesLineChart(string customerCode)
        {
            var insuranceCoverageChart = _insuranceCoverageChartRepository.GetInsuranceCoverageChart(customerCode);
            if (insuranceCoverageChart != null)
                return insuranceCoverageChart.SalesLineCharts;

            insuranceCoverageChart = new InsuranceCoverageChart();
            insuranceCoverageChart.CustomerCode = customerCode;
            insuranceCoverageChart.SalesLineCharts = new List<SalesLineChart>();

            var salesLines = UnitOfWork.SalesLines.GetSalesLineTypes(customerCode).ToList();
            foreach (var item in salesLines)
            {
                var insurancePolicies = UnitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode, item.Id).ToList();
                insuranceCoverageChart.SalesLineCharts.Add(new SalesLineChart
                {
                    SalesLineCode = item.SalesLineCode,
                    SalesLineName = item.SalesLineName,
                    BackGroundColor = item.BackGroundColor,
                    TotalCount = insurancePolicies.Count,
                    TotalPrice=insurancePolicies.Sum(x => x.TotalPrize)
                }) ;
            }

            _insuranceCoverageChartRepository.InsertOne(insuranceCoverageChart);
            return insuranceCoverageChart.SalesLineCharts;
        }

        public IList<InsuranceCoverageGrid> GetInsuranceCoverageGridSummaries(string customerCode)
        {
            var insuranceCoverageSummary = _insuranceCoverageSummaryRepository.GetInsuranceCoverageSummary(customerCode);
            if (insuranceCoverageSummary != null)
                return insuranceCoverageSummary.InsuranceCoverageGrids;

            var insuranceCoverages = UnitOfWork.InsurancePolicies.GetInsurancePolicies(customerCode)
                                                                 .Select(x => new
                                                                 {
                                                                     Id = x.Id,
                                                                     CategoryId = x.InsurancePolicyCategory.Id,
                                                                     CategoryName = x.InsurancePolicyCategory.InsurancePolicyCategoryName
                                                                 })
                                                                 .ToList();
            insuranceCoverageSummary = new InsuranceCoverageSummary();
            insuranceCoverageSummary.CustomerCode = customerCode;
            InsuranceCoverageGrid insuranceCoverageGrid = null;

            foreach (var item in insuranceCoverages)
            {
                switch (item.CategoryId)
                {
                    case 1://Auto
                    case 2://Moto
                    case 3://Imbarcazioni
                        var vehicleInsurancePolicy = (VehicleInsurancePolicy)UnitOfWork.InsurancePolicies.Get(item.Id);
                        var configurationModel = UnitOfWork.ConfigurationModels.GetConfigurationsByInsurancePolicyVehicle(vehicleInsurancePolicy.Id).FirstOrDefault();
                        insuranceCoverageGrid = new InsuranceCoverageGrid
                        {
                            Code = vehicleInsurancePolicy.InsurancePolicyCode,
                            CategoryType = item.CategoryName,
                            ItemDescription = $"{vehicleInsurancePolicy.LicensePlate} - {configurationModel.Model.Brand.BrandName} - {configurationModel.Model.ModelName} - {configurationModel.ConfigurationDescription}",
                            IssueDate = vehicleInsurancePolicy.IssueDate.ToString("dd/MM/yyyy"),
                            ExpiryDate = vehicleInsurancePolicy.ExpiryDate.ToString("dd/MM/yyyy"),
                            TotalPrice = $"{vehicleInsurancePolicy.TotalPrize.ToString("#,##0.00")} €"
                        };
                        insuranceCoverageSummary.InsuranceCoverageGrids.Add(insuranceCoverageGrid);
                        break;
                }
            }

            _insuranceCoverageSummaryRepository.InsertOne(insuranceCoverageSummary);
            return insuranceCoverageSummary.InsuranceCoverageGrids;
        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetTopSellingInsuranceCategoryPolicyDashboardCards(int year, int top, IEnumerable<string> incuranceCoverageCodes)
        {
            var insuranceCategoryPolicyTopSelling = _insuranceCategoryPolicyTopSellingRepository.GetInsuranceCategoryPolicyTopSelling(year);
            if (insuranceCategoryPolicyTopSelling != null)
                return insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies
                                                        .Where(x => !incuranceCoverageCodes.Any(y => y == x.Code))
                                                        .OrderByDescending(x => x.Total)
                                                        .Take(top)
                                                        .ToList();

            var insurancePolicyCategories = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategoryStatistics(year)
                                                                                     .GroupBy(x => x.Id)
                                                                                     .ToList();
            insuranceCategoryPolicyTopSelling = new InsuranceCategoryPolicyTopSelling();
            insuranceCategoryPolicyTopSelling.Year = year;
            insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies = new List<InsuranceCategoryPolicyDashboardCard>();

            foreach (var item in insurancePolicyCategories)
            {
                var subItem = item.FirstOrDefault();
                insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies.Add(
                    new InsuranceCategoryPolicyDashboardCard
                    {
                        Code = subItem.InsurancePolicyCategoryCode,
                        Name = subItem.InsurancePolicyCategoryName,
                        Abstract = subItem.InsurancePolicyCategoryDescription.Length > 170 ? subItem.InsurancePolicyCategoryDescription.Substring(0, 170) + "..." : subItem.InsurancePolicyCategoryDescription,
                        IconCssClass = subItem.IconCssClass,
                        SalesLineBackgroundColor = subItem.SalesLine.BackGroundColor,
                        SalesLineBackgroundCssClass = subItem.SalesLine.BackGroundColorCssClass,
                        SalesLineCode = subItem.SalesLine.SalesLineCode,
                        SalesLineName = subItem.SalesLine.SalesLineName,
                        Total = subItem.InsurancePolicyCategoryStatics.Sum(x => x.TotalCount)
                    });
            }

            if (insuranceCategoryPolicyTopSelling == null)
                return new List<InsuranceCategoryPolicyDashboardCard>();

            if (insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies.Count == 0)
                return insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies;

            _insuranceCategoryPolicyTopSellingRepository.InsertOne(insuranceCategoryPolicyTopSelling, false);

            return insuranceCategoryPolicyTopSelling.InsuranceCategoryPolicies
                                                    .Where(x => !incuranceCoverageCodes.Any(y => y == x.Code))
                                                    .OrderByDescending(x => x.Total)
                                                    .Take(top)
                                                    .ToList();
        }

        public IList<InsuranceCategoryPolicyDashboardCard> GetOtherInsuranceCategoryPolicyDashboardCards(IEnumerable<string> insuranceCategoryPolicyCodes)
        {
            var insuranceCategoryPolicyDashboardCards = _insuranceCategoryPolicyDashboardCardRepository.GetAll();
            if (insuranceCategoryPolicyDashboardCards != null && insuranceCategoryPolicyDashboardCards.Count != 0)
                return insuranceCategoryPolicyDashboardCards
                       .Where(x => !insuranceCategoryPolicyCodes.Any(y => y == x.Code))
                       .ToList();

            insuranceCategoryPolicyDashboardCards = UnitOfWork.InsurancePolicyCategories.GetInsurancePolicyCategories()
                                                                  .Select(x => new InsuranceCategoryPolicyDashboardCard
                                                                  {
                                                                      Code = x.InsurancePolicyCategoryCode,
                                                                      Name = x.InsurancePolicyCategoryName,
                                                                      Abstract = x.InsurancePolicyCategoryDescription.Length > 170 ? x.InsurancePolicyCategoryDescription.Substring(0, 170) : x.InsurancePolicyCategoryDescription,
                                                                      IconCssClass = x.IconCssClass,
                                                                      SalesLineBackgroundColor = x.SalesLine.BackGroundColor,
                                                                      SalesLineBackgroundCssClass = x.SalesLine.BackGroundColorCssClass,
                                                                      SalesLineCode = x.SalesLine.SalesLineCode,
                                                                      SalesLineName = x.SalesLine.SalesLineName
                                                                  })
                                                                  .ToList();

            _insuranceCategoryPolicyDashboardCardRepository.InsertMany(insuranceCategoryPolicyDashboardCards);

            return insuranceCategoryPolicyDashboardCards
                  .Where(x => !insuranceCategoryPolicyCodes.Any(y => y == x.Code))
                  .ToList();
        }

        public override void Dispose()
        {
            if (IsMassiveWriter && UnitOfWork.IsTransactionOpened)
            {
                if (_countError > 0)
                    UnitOfWork.RollbackTransaction();
                else
                    UnitOfWork.CommitTransaction();
            }

            UnitOfWork.Dispose();
        }
    }
}
