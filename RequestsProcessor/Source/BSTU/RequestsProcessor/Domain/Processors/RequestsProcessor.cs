using BSTU.RequestsProcessor.Domain.Extensions;
using BSTU.RequestsProcessor.Domain.Models;
using BSTU.RequestsProcessor.Domain.PostgreSQL;
using Newtonsoft.Json;

namespace BSTU.RequestsProcessor.Domain.Processors
{
    public class RequestsProcessor : IRequestsProcessor
    {
        private readonly IRequestsRepository _requestsRepository;

        public RequestsProcessor(IRequestsRepository requestsRepository)
        {
            _requestsRepository = requestsRepository;
        }

        public async Task ProcessAsync(string requestContent)
        {
            Request? request = JsonConvert.DeserializeObject<Request>(requestContent);
            
            if (request == null)
            {
                throw new JsonSerializationException($"Cannot deserialize contents of{requestContent}.");
            }

            request.Id = Guid.NewGuid().ToString();
            request.DateTime = request.DateTime.SetKindUtc();
            await _requestsRepository.AddAsync(request);
        }
    }
}