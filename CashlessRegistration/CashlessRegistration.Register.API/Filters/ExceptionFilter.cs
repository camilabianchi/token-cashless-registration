using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CashlessRegistration.Register.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            context.Result = new JsonResult(new
            {
                type = context.Exception.GetType().Name,
                context.Exception.Message,
            });

            _logger.LogError(context.Exception.ToString());

            context.ExceptionHandled = true;
        }
    }
}
