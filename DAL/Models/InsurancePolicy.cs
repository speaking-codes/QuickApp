﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class InsurancePolicy : AuditableEntity
    {
        public int Id { get; set; }
        public string InsurancePolicyCode { get; set; }
        public short Progressive { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public double InsuredMaximum { get; set; }
        public double TotalPrize { get; set; }
        public bool IsLuxuryPolicy { get; set; }

        public virtual InsurancePolicyCategory InsurancePolicyCategory { get; set; }
        public virtual Customer Customer { get; set; }
    }
}