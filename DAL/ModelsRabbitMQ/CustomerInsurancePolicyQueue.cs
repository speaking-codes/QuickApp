using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelsRabbitMQ
{
    public class CustomerInsurancePolicyQueue
    {
        public EnumPublishQueueType PublishQueueType { get; set; }
        public string CustomerCode { get; set; }
        public string InsurancePolicyCode { get; set; }

        public CustomerInsurancePolicyQueue(EnumPublishQueueType publishQueueType, string customerCode, string insurancePolicyCode)
        {
            PublishQueueType = publishQueueType;
            CustomerCode = customerCode;
            InsurancePolicyCode = insurancePolicyCode;
        }
    }
}
