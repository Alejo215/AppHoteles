using AppHotel.ApplicationService.Exceptions;
using AppHotel.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace AppHotel.Infraestructure.Middlewares
{
    class ExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandlerException(context, ex);
            }
        }

        private async Task HandlerException(HttpContext context, Exception ex)
        {
            string message = string.Empty;
            int code;
            switch (ex)
            {
                case BadRequestApplicationExeption badRequestApplicationExeption:
                    message = ex.Message;
                    code = (int)HttpStatusCode.BadRequest;
                    break;

                case NotFoundApplicationException notFoundApplicationException:
                    message = ex.Message;
                    code = (int)HttpStatusCode.NotFound;
                    break;

                case System.FormatException formatException:
                    message = ex.Message;
                    code = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    message = "Error interno del sistema";
                    code = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            Response response = new Response()
            {
                mensaje = message,
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
