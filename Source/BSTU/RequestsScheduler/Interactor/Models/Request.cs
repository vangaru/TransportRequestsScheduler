﻿namespace BSTU.RequestsScheduler.Interactor.Models
{
    public class Request
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
    }
}