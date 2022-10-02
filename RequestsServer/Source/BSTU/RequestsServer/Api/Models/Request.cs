using System.ComponentModel.DataAnnotations;
using BSTU.RequestsServer.Domain.Exceptions;
using BSTU.RequestsServer.Domain.Models;
using BSTU.RequestsServer.Api.Extensions;
using Newtonsoft.Json;

namespace BSTU.RequestsServer.Api.Models
{
    public class Request
    {
        private string? _sourceBusStopName;
        private string? _destinationBusStopName;

        [Required]
        public string SourceBusStopName
        {
            get => _sourceBusStopName ?? throw new RequestsServerException($"{nameof(SourceBusStopName)} is required.");
            set => _sourceBusStopName = value;
        }

        [Required]
        public string DestinationBusStopName
        {
            get => _destinationBusStopName ?? throw new RequestsServerException($"{nameof(DestinationBusStopName)} is required.");
            set => _destinationBusStopName = value;
        }

        [Required]
        public int SeatsCount { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public static implicit operator Domain.Models.Request(Request request)
        {
            return new Domain.Models.Request
            {
                Id = Guid.NewGuid().ToString().ToUpper(),
                SourceBusStopName = request.SourceBusStopName,
                DestinationBusStopName = request.DestinationBusStopName,
                SeatsCount = request.SeatsCount,
                DateTime = request.DateTime.SetKindUtc(),
                Status = RequestStatus.Received
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}