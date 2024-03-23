﻿using DAL.ModelsRabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IInsurancePolicyServerlessManager
    {
        Task Manage(CustomerInsurancePolicyQueue customerInsurancePolicyQueue);
    }
}
