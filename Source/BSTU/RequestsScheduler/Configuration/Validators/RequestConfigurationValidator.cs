﻿using BSTU.RequestsScheduler.Configuration.Exceptions;
using BSTU.RequestsScheduler.Interactor.Configuration;

namespace BSTU.RequestsScheduler.Configuration.Validators
{
    public class RequestConfigurationValidator : IRequestConfigurationValidator
    {
        private readonly IEnumerable<IRequestConfigurationValidator> _validators;

        public RequestConfigurationValidator(IEnumerable<IRequestConfigurationValidator> validators)
        {
            _validators = validators;
        }

        public RequestValidationException? Validate(IEnumerable<BusStopConfiguration> configuration, out bool success)
        {
            throw new NotImplementedException();
        }
    }
}
