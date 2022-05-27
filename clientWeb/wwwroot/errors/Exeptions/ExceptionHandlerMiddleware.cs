using Newtonsoft.Json;
using System.Net;
using System.Web.Mvc;

namespace ClientWeb.errors.Exeptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {

                await Task.Run(() => HandleExceptionMessageAsync(context, ex));
            }
        }

        




        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            //context.Response.ContentType = "text/HTML";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string result;

            switch (statusCode)
            {
                case 404:
                    {
                        context.Response.Redirect("errors/404.html");
                        return Task.CompletedTask;
                    }
                case 403:
                    {
                        context.Response.Redirect("errors/403.html");
                        return Task.CompletedTask;
                    }
                case 500:
                    {
                        context.Response.Redirect("errors/500.html");
                        return Task.CompletedTask;
                    }
                default:
                    {
                        var stringMesasge = JsonConvert.SerializeObject(new
                        {
                            ErrorMessage = exception.Message
                        });
                        result = $"<h1>{statusCode}</h1>" +
                            $"<h2>{stringMesasge}</h2>";
                        break;
                    }
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}
