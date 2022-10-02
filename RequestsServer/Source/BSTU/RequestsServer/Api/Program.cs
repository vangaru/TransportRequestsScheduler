using BSTU.RequestsServer.Api.Handlers;
using BSTU.RequestsServer.Domain.PostgreSQL;
using BSTU.RequestsServer.Domain.RabbitMQ;
using BSTU.RequestsServer.Domain.Transactions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BSTU.RequestsServer.Api
{
    public class Program
    {
        private const string RequestsSchedulerConnectionString = "RequestsScheduler";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(logger);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            string connectionString = builder.Configuration.GetConnectionString(RequestsSchedulerConnectionString);

            builder.Services.AddDbContext<RequestsContext>(options => options.UseNpgsql(connectionString));
            builder.Services.AddScoped<IRabbitMQClientWrapper, RabbitMQClientWrapper>();
            builder.Services.AddScoped<IRequestTransactionCommiter, RequestTransactionCommiter>();
            builder.Services.AddScoped<IRequestsHandler, RequestsHandler>();
            builder.Services.Configure<RabbitMQConfiguration>(builder.Configuration.GetSection(nameof(RabbitMQConfiguration)));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStatusCodePages();
            app.UseExceptionHandler("/api/requests/error");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}