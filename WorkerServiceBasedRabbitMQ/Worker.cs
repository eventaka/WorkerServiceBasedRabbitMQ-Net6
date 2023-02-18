using ServiceBasedRabbit.Core.Interfaces.MessageBroker;

namespace WorkerServiceBasedRabbitMQ
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;


        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }




        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                //await Task.Delay(1000, stoppingToken);


                //Using the scoped service
                using var scope = _serviceScopeFactory.CreateScope();
                var anyScopeService = scope.ServiceProvider.GetService<IRabbitMQSubscriber>();
                anyScopeService!.Subscribe();



            }
        }
    }
}