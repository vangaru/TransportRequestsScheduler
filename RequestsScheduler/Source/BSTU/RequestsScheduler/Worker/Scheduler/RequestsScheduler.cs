using BSTU.RequestsScheduler.Interactor.Models;

namespace BSTU.RequestsScheduler.Worker.Scheduler
{
    public class RequestsScheduler : IRequestsScheduler
    {
        private readonly ILogger<RequestsScheduler> _logger;

        public RequestsScheduler(ILogger<RequestsScheduler> logger)
        {
            _logger = logger;
        }

        public void Schedule(Request request)
        {
            if (request.DateTime.TimeOfDay >= DateTime.Now.TimeOfDay)
            {
                TimeSpan callIn = request.DateTime.TimeOfDay - DateTime.Now.TimeOfDay;
                Thread.Sleep(callIn);
                SubmitRequest(request);
            }
        }

        private void SubmitRequest(object? request)
        {
            _logger.LogInformation(request?.ToString());
        }
    }
}