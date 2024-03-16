﻿using DAL.ModelsNoSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.RepositoryNoSql.Interfaces
{
    public interface IInsuranceCoverageChartRepository : IRepositoryNoSql<InsuranceCoverageChart>
    {
        InsuranceCoverageChart GetInsuranceCoverageChart(string customerCode);
        bool DeleteInsuranceCoverageChart(string customerCode);
        bool UpdateInsuranceCoverageChart(string customerCode, InsuranceCoverageChart insuranceCoverageChart);
    }
}
