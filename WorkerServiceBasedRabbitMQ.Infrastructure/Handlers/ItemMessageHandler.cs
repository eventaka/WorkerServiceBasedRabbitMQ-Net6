using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using WorkerServiceBasedRabbitMQ.Core.Interfaces;
using WorkerServiceBasedRabbitMQ.Core.Models;
using WorkerServiceBasedRabbitMQ.Infrastructure.Interfaces;

namespace WorkerServiceBasedRabbitMQ.Infrastructure.Handlers
{
    public class ItemMessageHandler : IItemMessageHandler
    {
        private  ICreateItemRepository _createItemRepository;

        private readonly IServiceScopeFactory _serviceScopeFactory;

       

        public ItemMessageHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            using var scope = _serviceScopeFactory.CreateScope();
            _createItemRepository = scope.ServiceProvider.GetService<ICreateItemRepository>();

        }





        public void HandleItemMessage(string bodyString, string routingKey)
        {
            


            switch (routingKey)
            {
                case "item_queue": 
                    var item = JsonSerializer.Deserialize<Item>(bodyString);
                    _createItemRepository.CreateItemAsync(item);
                    break;

                default:
                    break;
            }

            

            
        }
    }
}
