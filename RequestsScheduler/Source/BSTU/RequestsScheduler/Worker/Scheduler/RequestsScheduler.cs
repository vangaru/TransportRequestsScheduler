using System.Text;
using BSTU.RequestsScheduler.Interactor.Models;
using BSTU.RequestsScheduler.Worker.Configuration;
using Microsoft.Extensions.Options;

namespace BSTU.RequestsScheduler.Worker.Scheduler
{
    public class RequestsScheduler : IRequestsScheduler
    {
        private const string JsonMediaType = "application/json";

        private readonly ILogger<RequestsScheduler> _logger;
        private readonly HttpClient _httpClient;
        private readonly RequestsServerConfiguration _requestsServerConfiguration;

        public RequestsScheduler(
            ILogger<RequestsScheduler> logger, 
            HttpClient httpClient, 
            IOptions<RequestsServerConfiguration> requestsServerOptions)
        {
            _logger = logger;
            _httpClient = httpClient;
            _requestsServerConfiguration = requestsServerOptions.Value;

            _httpClient.BaseAddress = new Uri(_requestsServerConfiguration.BaseUrl
                ?? throw new ApplicationException("Base Url of the Requests Server is not configured."));
        }

        public async Task ScheduleAsync(Request request)
        {
            if (request.DateTime.TimeOfDay >= DateTime.Now.TimeOfDay)
            {
                TimeSpan callIn = request.DateTime.TimeOfDay - DateTime.Now.TimeOfDay;
                Thread.Sleep(callIn);
                await SubmitRequest(request);
            }
        }

        private async Task SubmitRequest(object? requestObj)
        {
            if (requestObj is Request request)
            {
                var requestContent = new StringContent(request.ToString(), Encoding.UTF8, JsonMediaType);
                await _httpClient.PostAsync(_requestsServerConfiguration.RequestsEndpoint, requestContent);
                _logger.LogInformation(request.ToString());
            }
            else if (requestObj == null)
            {
                throw new ArgumentNullException(nameof(requestObj), "Request cannot be null.");
            }
            else
            {
                throw new ArgumentException($"Parameter is not {typeof(Request)} type.", nameof(requestObj));
            }
        }
    }
}