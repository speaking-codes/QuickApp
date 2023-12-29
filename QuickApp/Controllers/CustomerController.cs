// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using AutoMapper;
using DAL;
using DAL.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.ViewModels;
using QuickApp.Helpers;
using QuickApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        //[HttpGet("throw")]
        //public IEnumerable<CustomerViewModel> Throw()
        //{
        //    throw new InvalidOperationException($"This is a test exception: {DateTime.Now}");
        //}

        //[HttpGet("email")]
        //public async Task<string> Email()
        //{
        //    var recipientName = "QickApp Tester"; //         <===== Put the recipient's name here
        //    var recipientEmail = "test@ebenmonney.com"; //   <===== Put the recipient's email here

        //    var message = EmailTemplates.GetTestEmail(recipientName, DateTime.UtcNow);

        //    (var success, var errorMsg) = await _emailSender.SendEmailAsync(recipientName, recipientEmail, "Test Email from QuickApp", message);

        //    if (success)
        //        return "Success";

        //    return $"Error: {errorMsg}";
        //}

        // GET api/values/5
        [HttpGet("{taxIdCode}")]
        public IActionResult Get(string taxIdCode)
        {
            var customer = _customerManager.GetCustomer(taxIdCode);
            return Ok(_mapper.Map<CustomerDetailHeaderViewModel>(customer));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{taxIdCode}")]
        public void Delete(string taxIdCode)
        {
            _customerManager.DeleteCustomer(taxIdCode);
        }
    }
}
