using BSTU.RequestsProcessor.Domain.PostgreSQL;
using BSTU.RequestsProcessor.Domain.Processors;
using BSTU.RequestsProcessor.Domain.RabbitMQ;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Core;

namespace BSTU.RequestsProcessor.Processor
{
    public class Program
    {
        private const string ConnectionString = "RequestsProcessor";

        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureLogging((hostContext, loggingBuilder) =>
                {
                    Logger? logger = new LoggerConfiguration().ReadFrom.Configuration(hostContext.Configuration).CreateLogger();
                    loggingBuilder.ClearProviders();
                    loggingBuilder.AddSerilog(logger);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    string connectionString = hostContext.Configuration.GetConnectionString(ConnectionString);
                    services.AddDbContext<RequestsProcessorContext>(options 
                        => options.UseNpgsql(connectionString, opts => opts.MigrationsAssembly("Domain")), ServiceLifetime.Singleton);
                    services.AddSingleton<IRequestsRepository, RequestsRepository>();
                    services.AddSingleton<IRequestsProcessor, Domain.Processors.RequestsProcessor>();
                    services.AddSingleton<IMessageJobsHandler, MessageJobsHandler>();
                    services.AddSingleton<IFaultedRequestsCountLogger, FaultedRequestsCountLogger>();
                    services.Configure<RabbitMQConfiguration>(hostContext.Configuration.GetSection(nameof(RabbitMQConfiguration)));
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}