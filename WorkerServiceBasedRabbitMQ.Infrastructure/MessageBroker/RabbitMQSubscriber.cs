using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceBasedRabbit.Core.Interfaces.MessageBroker;
using System;
using System.Text;
using WorkerServiceBasedRabbitMQ.Infrastructure.Interfaces;

namespace ServiceBasedRabbit.Infrastructure.MessageBroker
{



    public class RabbitMQSubscriber : IRabbitMQSubscriber
    {
        
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public RabbitMQSubscriber(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }




        public void Subscribe()
        {

            using var scope = _serviceScopeFactory.CreateScope();
            var _userMessageHandler = scope.ServiceProvider.GetService<IUserMessageHandler>();
            var _itemMessageHandler = scope.ServiceProvider.GetService<IItemMessageHandler>();


            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "user_queue", 
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueDeclare(queue: "item_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.QueueDeclare(queue: "userItem_queue",
                                   durable: false,
                                   exclusive: false,
                                   autoDelete: false,
                                   arguments: null);


                // Waiting for messages...
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, eventArgs) =>
                {
                    try
                    {

                        var bodyArray = eventArgs.Body.ToArray();
                        var bodyString = Encoding.UTF8.GetString(bodyArray);

                        
                        //var message = JsonSerializer.Deserialize<RabbitMQMessageType>(bodyString);


                        Console.WriteLine($" [x] Received {bodyString}");

                        channel.BasicAck(eventArgs.DeliveryTag, false);
                        Console.WriteLine($" [x] ea.DeliveryTag {eventArgs.DeliveryTag}");

                                             

                        switch (eventArgs.RoutingKey)
                        {
                            case "user_queue":// "user.create":
                                _userMessageHandler.HandleUserMessage(bodyString, eventArgs.RoutingKey);
                                break;

                            case "item_queue":// "item.create":
                                _itemMessageHandler.HandleItemMessage(bodyString, eventArgs.RoutingKey);
                                break;

                            case "userItem_queue": // "userItem.create" or "userItem.delete":
                                //_userItemMessageHandler.HandleUserItemMessage(bodyString, eventArgs.RoutingKey);
                                break;
                        }




                    }
                    catch (Exception ex)
                    {
                        //throw;
                        channel.BasicNack(eventArgs.DeliveryTag, false, true);

                    }
                };
                channel.BasicConsume(queue: "user_queue",//"UserItemQueue",
                                     autoAck: false,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();


            }

        }

       
    }
}
