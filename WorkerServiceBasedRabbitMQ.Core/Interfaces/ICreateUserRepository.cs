using WorkerServiceBasedRabbitMQ.Core.Dto;
using WorkerServiceBasedRabbitMQ.Core.Models;
using System;
using System.Threading.Tasks;

namespace WorkerServiceBasedRabbitMQ.Core.Interfaces
{
    public interface ICreateUserRepository
    {
         Task CreateUserAsync(User user);
        
    }
}
