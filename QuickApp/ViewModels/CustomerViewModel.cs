// ---------------------------------------
// Email: quickapp@ebenmonney.com
// Templates: www.ebenmonney.com/templates
// (c) 2023 www.ebenmonney.com/mit-license
// ---------------------------------------

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickApp.ViewModels
{
    public class CustomerViewModel
    {
        public string FullName { get; set; }
        public string TaxIdCode { get; set; }
        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCounty { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string Gender { get; set; }
    }

    public class CustomerViewModelValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerViewModelValidator()
        {
            RuleFor(register => register.FullName).NotEmpty().WithMessage("Customer name cannot be empty");
            RuleFor(register => register.Gender).NotEmpty().WithMessage("Gender cannot be empty");
        }
    }
}
