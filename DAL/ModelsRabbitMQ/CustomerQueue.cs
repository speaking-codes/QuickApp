using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ModelsRabbitMQ
{
    public class CustomerQueue
    {
        public EnumPublishQueueType PublishQueueType { get; set; }
        public string CustomerCode { get; set; }

        public CustomerQueue(EnumPublishQueueType publishQueueType, string customerCode)
        {
            PublishQueueType = publishQueueType;
            CustomerCode = customerCode;
        }
    }
}
