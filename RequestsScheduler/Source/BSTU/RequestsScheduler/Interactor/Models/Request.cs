using Newtonsoft.Json;

namespace BSTU.RequestsScheduler.Interactor.Models
{
    public class Request : IComparable<Request>, IComparable
    {
        private string? _id;
        private string? _sourceBusStopName;
        private string? _destinationBusStopName;

        public string Id 
        { 
            get => _id ?? throw new ApplicationException($"{nameof(Id)} is required."); 
            set => _id = value; 
        }

        public string SourceBusStopName
        {
            get => _sourceBusStopName ?? throw new ApplicationException($"{nameof(SourceBusStopName)} is required.");
            set => _sourceBusStopName = value;
        }

        public string DestinationBusStopName
        {
            get => _destinationBusStopName ?? throw new ApplicationException($"{nameof(DestinationBusStopName)} is required.");
            set => _destinationBusStopName = value;
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