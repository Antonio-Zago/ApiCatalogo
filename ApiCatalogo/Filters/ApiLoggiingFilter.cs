using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters
{
    public class ApiLoggiingFilter : IActionFilter
    {
        private readonly ILogger<ApiLoggiingFilter> _logger;

        public ApiLoggiingFilter(ILogger<ApiLoggiingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context) 
        {
            _logger.LogInformation("OnActionExecuting");
        }
    }
}
