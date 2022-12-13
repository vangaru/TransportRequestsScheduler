using BSTU.RequestsServer.Api.Handlers;
using BSTU.RequestsServer.Api.ResponseModels;
using BSTU.RequestsServer.Domain.Exceptions;
using BSTU.RequestsServer.Domain.Models;
using BSTU.RequestsServer.Domain.Providers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BSTU.RequestsServer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RequestsController : ControllerBase
    {
        private readonly IRequestsHandler _requestsHandler;
        private readonly ILogger<RequestsController> _logger;
        private readonly IBusStopNamesProvider _busStopNamesProvider;
        private readonly IReasonsForTravelProvider _reasonsForTravelProvider;

        public RequestsController(
            IRequestsHandler requestsHandler, 
            ILogger<RequestsController> logger,
            IBusStopNamesProvider busStopNamesProvider, 
            IReasonsForTravelProvider reasonsForTravelProvider)
        {
            _requestsHandler = requestsHandler;
            _logger = logger;
            _busStopNamesProvider = busStopNamesProvider;
            _reasonsForTravelProvider = reasonsForTravelProvider;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Request), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public ActionResult<Request> PostRequest(Request request)
        {
            _requestsHandler.HandleRequest(request);
            return Ok(request);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Route("busstops")]
        public ActionResult<List<string>> GetBusStops()
        {
            return Ok(_busStopNamesProvider.BusStopNames);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [Route("reasonsfortravel")]
        public ActionResult<List<string>> GetReasonsForTravel()
        {
            return Ok(_reasonsForTravelProvider.ReasonsForTravel);
        }

        [Route("error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult<ErrorResponse> HandleError()
        {
            IExceptionHandlerFeature? context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            RequestsServerException exception = context == null
                ? new RequestsServerException("Unknown exception.", StatusCodes.Status500InternalServerError)
                : context.Error is RequestsServerException requestsServerException
                    ? requestsServerException
                    : new RequestsServerException(context.Error, StatusCodes.Status500InternalServerError);

            return HandleError(exception);
        }

        private ActionResult<ErrorResponse> HandleError(RequestsServerException exception)
        {
            _logger.LogError(exception, $"Status code: {exception.StatusCode}, Message: {exception.Message}");
            var errorResponse = new ErrorResponse(exception.Message, exception.StatusCode);
            return StatusCode(errorResponse.StatusCode, errorResponse);
        }
    }
}