using VendingMachine.Application.Exceptions;
using VendingMachine.Application.Models;
using VendingMachine.Core.Enums;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;

namespace VendingMachine.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }catch(BusinessRuleException ex)
            {
                _logger.LogError("validation rule broked :" + ex.Message);
                _logger.LogError(ex?.InnerException?.Message);
                await HandleBusinessRuleException(ex, context);
            }catch(Exception ex)
            {
                _logger.LogError("Server Error :" + ex.Message);
                _logger.LogError(ex?.InnerException?.Message);
                await HandleException(ex, context);
            }
        }

        private Task HandleBusinessRuleException(BusinessRuleException exception,HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            string Reponse = JsonConvert.SerializeObject(new MessageModel()
            {
                Code = exception.ErrorCode,
                Message = exception.Message

            }); 
            return context.Response.WriteAsync(Reponse);
        }
        private Task HandleException(Exception exception, HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            string Reponse = JsonConvert.SerializeObject(new MessageModel()
            {
                Code = ApplicationCode.ServerError,
                Message = exception.Message

            });
            return context.Response.WriteAsync(Reponse);
        }
    }
}
