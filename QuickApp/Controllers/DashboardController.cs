using AutoMapper;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickApp.Helpers;
using QuickApp.ViewModels;
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
            var allCustomers = _dashboardManager.GetCustomerHeader(customerCode);
            return Ok(allCustomers);
        }

        [HttpGet("CustomerDetail/{customerCode}")]
        public IActionResult GetCustomerDetail(string customerCode)
        {
            var allCustomers = _dashboardManager.GetCustomerDetail(customerCode);
            return Ok(allCustomers);
        }
    }
}
