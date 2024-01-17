// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using AutoMapper;
using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using System.Collections.Generic;

namespace QuickApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerManager _customerManager;
        private readonly ILogger _logger;
        private readonly IEmailSender _emailSender;

        public CustomerController(IMapper mapper, ICustomerManager customerManager, ILogger<CustomerController> logger, IEmailSender emailSender)
        {
            _mapper = mapper;
            _customerManager = customerManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var allCustomers = _customerManager.GetCustomers();
            return Ok(_mapper.Map<IEnumerable<CustomerGridViewModel>>(allCustomers));
        }

        //[HttpGet("active")]
        //public IActionResult GetActive()
        //{
        //    var activeCustomers = _customerManager.GetActiveCustomers();
        //    return Ok(_mapper.Map<IEnumerable<CustomerGridViewModel>>(activeCustomers));
        //}

        [HttpGet("{customerCode}")]
        public IActionResult Get(string customerCode)
        {
            var customer = _customerManager.GetCustomer(customerCode);
            var customerViewModel = _mapper.Map<CustomerEditViewModel>(customer);
            return Ok(customerViewModel);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] CustomerEditViewModel customerViewModel)
        {
            var customer = _mapper.Map<Customer>(customerViewModel);
            customerViewModel.CustomerCode = _customerManager.AddCustomer(customer);
            return Ok(customerViewModel);
        }

        // PUT api/values/5
        [HttpPut("{customerCode}")]
        public IActionResult Put(string customerCode, [FromBody] CustomerEditViewModel customerViewModel)
        {
            var customer = _mapper.Map<Customer>(customerViewModel);
            customerViewModel.CustomerCode = _customerManager.UpdateCustomer(customerCode, customer);
            return Ok(customerViewModel);
        }

        // DELETE api/values/5
        [HttpDelete("{customerCode}")]
        public void Delete(string customerCode)
        {
            _customerManager.DeleteCustomer(customerCode);
        }
    }
}
