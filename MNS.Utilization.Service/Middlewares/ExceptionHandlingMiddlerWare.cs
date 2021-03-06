using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MNS.Services.Utilization.Core.DomainExceptions;
using System;
using System.Threading.Tasks;

namespace MNS.Utilization.Service.Api.Middlewares
{
    /// <summary>
    /// Exception Middleware
    /// </summary>
    public class ExceptionHandlingMiddlerWare : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddlerWare> _logger;
        /// <summary>
        /// Constructor of Middleware
        /// </summary>
        /// <param name="logger">Logger to log details</param>
        public ExceptionHandlingMiddlerWare(ILogger<ExceptionHandlingMiddlerWare> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Interface method to Invoke the RequestDelegate for HttpPipeline.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogError($"Utilization Service Exception: {ex.Message}");
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = ex.Message
                }.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught {ex.Message}");
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Internal Server Error. Please try again later."
                }.ToString());
            }
        }
    }
}
