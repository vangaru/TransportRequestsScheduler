using BSTU.RequestsScheduler.Configuration.Configuration;
using BSTU.RequestsScheduler.Configuration.Validators;
using BSTU.RequestsScheduler.Interactor.Configuration;
using BSTU.RequestsScheduler.Interactor.Factories;
using BSTU.RequestsScheduler.Interactor.Interactors;
using BSTU.RequestsScheduler.Interactor.Presentators;
using BSTU.RequestsScheduler.Interactor.Providers;
using BSTU.RequestsScheduler.Worker.Configuration;
using BSTU.RequestsScheduler.Worker.Scheduler;
using Serilog;
using Serilog.Core;

namespace BSTU.RequestsScheduler.Worker
{
    internal static class Program
    {
        private static void Main(string[] args)
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
                    services.AddSingleton<IRequestFactory, RequestFactory>();
                    services.AddSingleton<IRequestsPresentator, RequestsPresentator>();
                    services.AddSingleton<IRequestsInteractor, RequestsInteractor>();
                    services.AddSingleton<IRequestsCountProvider, RequestsCountProvider>();
                    var validators = new List<IRequestConfigurationValidator>
                    {
                        // TODO: define validators from config.
                        new BusStopCrossTimePeriodsValidator(),
                        new BusStopDailyRequestsCountValidator(),
                        new BusStopNamesValidator(),
                        new BusStopRequestsCountCoefficientValidator(),
                        new BusStopsCountValidator(),
                        new BusStopTimePeriodBoundsValidator(),
                        new BusStopTimePeriodsCoverageValidator()
                    };
                    services.AddSingleton<IEnumerable<IRequestConfigurationValidator>>(validators);
                    services.AddSingleton<IRequestConfigurationValidator, RequestConfigurationValidator>();
                    services.AddSingleton<IRequestConfigurationProxy, RequestConfiguration>();
                    services.AddSingleton<IReasonsForTravelProxy, ReasonsForTravelProvider>();
                    services.AddHttpClient<IRequestsScheduler, Scheduler.RequestsScheduler>();
                    services.Configure<RequestsServerConfiguration>(hostContext.Configuration.GetSection(nameof(RequestsServerConfiguration)));
                    services.AddHostedService<Worker>();
                })
                .Build();

            host.Run();
        }
    }
}