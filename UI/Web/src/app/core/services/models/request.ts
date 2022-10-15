export class Request {
    constructor(
        public sourceBusStopName: string,
        public destinationBusStopName: string,
        public seatsCount: number
    ) {}
}