using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Text;
using WorkerServiceBasedRabbitMQ.Infrastructure.DbContexts;


namespace WorkerServiceBasedRabbitMQ.Infrastructure.Interfaces
{
    public interface IUserMessageHandler
    {
        

        void HandleUserMessage(string bodyString, string routingKey);
        
    }
}
