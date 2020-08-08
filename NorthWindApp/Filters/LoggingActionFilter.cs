using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NorthWindApp.Configuration;
using System;

namespace NorthWindApp.Filters
{
    public class LoggingActionFilter: IActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;
        private readonly FilterOptions _options;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger, IOptions<FilterOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_options.ActionLogging)
                _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} started at {DateTime.Now}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_options.ActionLogging)
                _logger.LogInformation($"Action {context.ActionDescriptor.DisplayName} stoped at {DateTime.Now}");
        }


    }
}
