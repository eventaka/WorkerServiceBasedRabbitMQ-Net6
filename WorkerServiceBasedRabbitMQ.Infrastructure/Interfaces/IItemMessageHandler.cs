using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using WorkerServiceBasedRabbitMQ.Infrastructure.DbContexts;


namespace WorkerServiceBasedRabbitMQ.Infrastructure.Interfaces
{
    public interface IItemMessageHandler
    {
        

        void HandleItemMessage(string bodyString, string routingKey);
        
    }
}
