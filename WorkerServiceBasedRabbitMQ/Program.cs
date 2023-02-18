
using Microsoft.EntityFrameworkCore;
using ServiceBasedRabbit.Core.Interfaces.MessageBroker;
using ServiceBasedRabbit.Infrastructure.MessageBroker;
using WorkerServiceBasedRabbitMQ;
using WorkerServiceBasedRabbitMQ.Core.Interfaces;
using WorkerServiceBasedRabbitMQ.Infrastructure.DbContexts;
using WorkerServiceBasedRabbitMQ.Infrastructure.Handlers;
using WorkerServiceBasedRabbitMQ.Infrastructure.Interfaces;
using WorkerServiceBasedRabbitMQ.Infrastructure.Repositories;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var connectionString = @"Server=(localdb)\local;Database=RabbitMQ;Trusted_Connection=True;TrustServerCertificate=True;";

        

        services.AddScoped<IRabbitMQSubscriber, RabbitMQSubscriber>();
        services.AddScoped<IUserMessageHandler, UserMessageHandler>();
        services.AddScoped<IItemMessageHandler, ItemMessageHandler>();
        services.AddScoped<ICreateUserRepository, CreateUserRepository>();
        services.AddScoped<ICreateItemRepository, CreateItemRepository>();


        services.AddDbContextFactory<AppDbContext>(
        options => options.UseSqlServer(connectionString));


        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
