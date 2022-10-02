﻿using BSTU.RequestsServer.Domain.Exceptions;
using Newtonsoft.Json;

namespace BSTU.RequestsServer.Domain.Models
{
    public class Request
    {
        private string? _id;
        private string? _sourceBusStopName;
        private string? _destinationBusStopName;

        public string Id
        {
            get => _id ?? throw new RequestsServerException($"{nameof(Id)} is required.");
            set => _id = value;
        }

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

        public int SeatsCount { get; set; }

        public DateTime DateTime { get; set; }

        public RequestStatus Status { get; set;}

        public string? ErrorMessage { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}