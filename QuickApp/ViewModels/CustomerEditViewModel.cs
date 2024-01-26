﻿using DAL.Enums;
using System;

namespace QuickApp.ViewModels
{
    public class CustomerEditViewModel
    {
        public string CustomerCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string BirthCounty { get; set; }
        public EnumGender Gender { get; set; }
        public string Profession { get; set; }
        public EnumContractType ContractType { get; set; }
        public double? RAL { get; set; }

        public string Location { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
