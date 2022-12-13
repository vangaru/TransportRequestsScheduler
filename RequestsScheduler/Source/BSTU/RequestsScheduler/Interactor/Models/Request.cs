using Newtonsoft.Json;

namespace BSTU.RequestsScheduler.Interactor.Models
{
    public class Request : IComparable<Request>, IComparable
    {
        private string? _sourceBusStopName;
        private string? _destinationBusStopName;
        private string? _reasonForTravel;

        public string SourceBusStopName
        {
            get => _sourceBusStopName ?? throw new ApplicationException($"{nameof(SourceBusStopName)} is required.");
            internal set => _sourceBusStopName = value;
        }

        public string DestinationBusStopName
        {
            get => _destinationBusStopName ?? throw new ApplicationException($"{nameof(DestinationBusStopName)} is required.");
            internal set => _destinationBusStopName = value;
        }

        public string ReasonForTravel
        {
            get => _reasonForTravel ?? throw new ApplicationException($"{nameof(ReasonForTravel)} is required.");
            internal set => _reasonForTravel = value;
        }

        public int SeatsCount { get; set; }

        public DateTime DateTime { get; set; }

        public int CompareTo(object? obj)
        {
            return obj is Request request 
                ? CompareTo(request) 
                : throw new ArgumentException(nameof(obj));
        }

        public int CompareTo(Request? other)
        {
            return other != null
                ? DateTime.TimeOfDay.CompareTo(other.DateTime.TimeOfDay)
                : throw new ArgumentNullException(nameof(other));
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}