using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WorkerServiceBasedRabbitMQ.Core.Interfaces;
using WorkerServiceBasedRabbitMQ.Core.Models;
using WorkerServiceBasedRabbitMQ.Infrastructure.Interfaces;

namespace WorkerServiceBasedRabbitMQ.Infrastructure.Handlers
{
    public class UserMessageHandler : IUserMessageHandler
    {
        private  ICreateUserRepository _createUserRepository;

        private readonly IServiceScopeFactory _serviceScopeFactory;

       

        public UserMessageHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            using var scope = _serviceScopeFactory.CreateScope();
            _createUserRepository = scope.ServiceProvider.GetService<ICreateUserRepository>();

        }





        public void HandleUserMessage(string bodyString, string routingKey)
        {
            


            switch (routingKey)
            {
                case "user_queue": // "user.create":
                    var user = JsonSerializer.Deserialize<User>(bodyString);
                    _createUserRepository.CreateUserAsync(user);
                    break;

                default:
                    break;
            }

            

            
        }
    }
}
