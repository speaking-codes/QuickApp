using AutoMapper;
using DAL.Core.Interfaces;
using DAL.ModelsNoSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using System;
using System.Collections.Generic;

namespace QuickApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashboardManager _dashboardManager;
        private readonly ILogger _logger;

        public DashboardController(IMapper mapper, ILogger<CustomerController> logger, IDashboardManager dashboardManager)
        {
            _mapper = mapper;
            _dashboardManager = dashboardManager;
            _logger = logger;
        }

        [HttpGet("CustomerHeader/{customerCode}")]
        public IActionResult GetCustomerHeader(string customerCode)
        {
            var customer = _dashboardManager.GetCustomerHeader(customerCode.ToUpper());
            if (customer == null) customer = new CustomerHeader();

            return Ok(_mapper.Map<CustomerHeaderViewModel>(customer));
        }

        [HttpGet("CustomerDetail/{customerCode}")]
        public IActionResult GetCustomerDetail(string customerCode)
        {
            var customer = _dashboardManager.GetCustomerDetail(customerCode.ToUpper());
            if (customer == null) customer = new CustomerDetail();

            return Ok(_mapper.Map<CustomerDetailViewModel>(customer));
        }

        [HttpGet("InsuranceCoverageChart/{customerCode}")]
        public IActionResult GetInsuranceCoverageChart(string customerCode)
        {
            var salesLineCharts = _dashboardManager.GetSalesLineChart(customerCode.ToUpper());
            return Ok(_mapper.Map<IList<SalesLineChartViewModel>>(salesLineCharts));
        }

        [HttpGet("InsuranceCoverageSummary/{customerCode}")]
        public IActionResult GetInsuranceCoverageSummary(string customerCode)
        {
            var insuranceCoverageSummaries = _dashboardManager.GetInsuranceCoverageGridSummaries(customerCode);
            return Ok(_mapper.Map<IList<InsuranceCoverageSummaryViewModel>>(insuranceCoverageSummaries));
        }

        [HttpGet("insurancecoveragerecommended/{customerCode}")]
        public IActionResult GetInsuranceCoverageRecommended(string customerCode)
        {
            var currentYear = DateTime.Now.Year - 1;
            var top = 6;
            var insuranceCategoryPolicyDashboardCards = _dashboardManager.GetTopSellingInsuranceCategoryPolicyDashboardCards(currentYear, top, new List<string>());
            return Ok(_mapper.Map<IList<InsuranceCategoryPolicyDashboardCardViewModel>>(insuranceCategoryPolicyDashboardCards));
        }

        [HttpGet("insurancecoveragetopselling/{top}")]
        public IActionResult GetInsuranceCoverageTopSelling(int top, [FromQuery] string insuranceCategoryPolicyCodes)
        {
            var currentYear = DateTime.Now.Year - 1;
            var insuranceCategoryPolicyDashboardCards = _dashboardManager.GetTopSellingInsuranceCategoryPolicyDashboardCards(currentYear, top, insuranceCategoryPolicyCodes.Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries));
            return Ok(_mapper.Map<IList<InsuranceCategoryPolicyDashboardCardViewModel>>(insuranceCategoryPolicyDashboardCards));
        }

        [HttpGet("insurancecoverageother")]
        public IActionResult GetInsuranceCoverageOther([FromQuery] string insuranceCategoryPolicyCodes)
        {
            var insuranceCategoryPolicyDashboardCard = _dashboardManager.GetOtherInsuranceCategoryPolicyDashboardCards(insuranceCategoryPolicyCodes.Split(new string[] { ";" }, System.StringSplitOptions.RemoveEmptyEntries));
            return Ok(_mapper.Map<IList<InsuranceCategoryPolicyDashboardCardViewModel>>(insuranceCategoryPolicyDashboardCard));
        }
    }
}
