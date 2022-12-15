using System.ComponentModel.DataAnnotations;
using BSTU.RequestsServer.Domain.Exceptions;
using Newtonsoft.Json;

namespace BSTU.RequestsServer.Domain.Models
{
    public class Request
    {
        private string? _sourceBusStopName;
        private string? _destinationBusStopName;
        private string? _reasonForTravel;

        [Required]
        public string SourceBusStopName
        {
            get => _sourceBusStopName ?? throw new RequestsServerException($"{nameof(SourceBusStopName)} is required.");
            set => _sourceBusStopName = value;
        }

        public string DestinationBusStopName
        {
            get => _destinationBusStopName ?? throw new RequestsServerException($"{nameof(DestinationBusStopName)} is required.");
            set => _destinationBusStopName = value;
        }

        [Required]
        public int SeatsCount { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public string ReasonForTravel 
        {
            get => _reasonForTravel ?? throw new RequestsServerException($"{nameof(ReasonForTravel)} is required."); 
            set => _reasonForTravel = value;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}