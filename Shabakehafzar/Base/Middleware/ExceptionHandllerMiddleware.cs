using Newtonsoft.Json;
using System.Net;
using FluentValidation;
using Shabakehafzar.Application.Exception;

namespace Shabakehafzar.API.Base.Middleware
{
    public class ExceptionHandllerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandllerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception exception)
            {
                await ManageException(context, exception);
            }
        }

        private async Task ManageException(HttpContext context, Exception ex)
        {
            switch (ex)
            {
                case ManagedException managedException:
                    {
                        await ConfigureResponse(context, HttpStatusCode.BadRequest, managedException.Message);
                        break;
                    }
                case NotFoundException notFoundException:
                    {
                        await ConfigureResponse(context, HttpStatusCode.NotFound, notFoundException.Message);
                        break;
                    }
                default:
                    {
                        await ConfigureResponse(context, HttpStatusCode.InternalServerError, "InternalServerError");
                    }
                    break;
            }
        }

        private static async Task ConfigureResponse(HttpContext context, HttpStatusCode statusCode, string message)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                new FailedResponseMessage(message).ToString());
        }

        public class FailedResponseMessage
        {
            public FailedResponseMessage(string message)
            {
                this.message = message;
            }
            public string message { get; set; }
            public override string ToString()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
