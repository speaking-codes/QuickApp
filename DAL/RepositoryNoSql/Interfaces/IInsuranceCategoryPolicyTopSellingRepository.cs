﻿using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql.Interfaces
{
    public interface IInsuranceCategoryPolicyTopSellingRepository:IRepositoryNoSql<InsuranceCategoryPolicyTopSelling>
    {
        InsuranceCategoryPolicyTopSelling GetInsuranceCategoryPolicyTopSelling(int year);
    }
}