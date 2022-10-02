namespace BSTU.RequestsServer.Domain.Exceptions
{
    public class RequestsServerException : Exception
    {
        private const int Status400BadRequest = 400;

        public int StatusCode { get; }

        public RequestsServerException() : this(Status400BadRequest)
        {
        }

        public RequestsServerException(int statusCode) : base()
        {
            StatusCode = statusCode;
        }

        public RequestsServerException(string message) : this(message, Status400BadRequest)
        {
        }

        public RequestsServerException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }

        public RequestsServerException(Exception innerException, int statusCode) : base(innerException.Message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}