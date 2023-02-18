
using System;
using System.Collections.Generic;
using System.Text;
using WorkerServiceBasedRabbitMQ.Core.Models;

namespace ServiceBasedRabbit.Core.Interfaces.MessageBroker
{
    public interface IRabbitMQSubscriber 
    {
        void Subscribe();

    }
}
