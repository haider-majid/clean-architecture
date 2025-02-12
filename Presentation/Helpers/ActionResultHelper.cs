using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace clean_architecture.Helpers
{
    public static class ActionResultHelper
    {
        public static IActionResult HandleSuccess<T>(ILogger logger, string message, T data)
        {
            logger.LogInformation(message);
            return new OkObjectResult(data);
        }

        public static IActionResult HandleNotFound(ILogger logger, string message)
        {
            logger.LogWarning(message);
            return new NotFoundObjectResult(message);
        }

        public static IActionResult HandleError(ILogger logger, Exception ex, string message)
        {
            logger.LogError(ex, message);
            return new ObjectResult("An internal server error occurred.") { StatusCode = 500 };
        }
    }
}