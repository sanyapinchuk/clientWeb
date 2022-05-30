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

        




        private static async void HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {       
            context.Response.Redirect("/Error/?statusCode=500");
        }
    }
}
