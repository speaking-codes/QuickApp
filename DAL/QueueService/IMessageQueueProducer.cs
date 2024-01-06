using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.QueueService
{
    public interface IMessageQueueProducer
    {
        void Send<T>(string queue, T message);
    }
}
