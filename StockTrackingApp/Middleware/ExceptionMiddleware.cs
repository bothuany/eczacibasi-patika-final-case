using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;

namespace StockTrackingApp.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
               await _next(httpContext);
            }
            catch (System.Exception ex)
            {
                httpContext.Response.ContentType = "application/json";

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                if (ex is HttpResponseException httpResponseException)
                {
                    httpContext.Response.StatusCode = (int)httpResponseException.Response.StatusCode;
                }
                else if (ex is WebException webException && webException.Response is HttpWebResponse httpWebResponse)
                {
                    httpContext.Response.StatusCode = (int)httpWebResponse.StatusCode;
                }

                var result = Newtonsoft.Json.JsonConvert.SerializeObject(new { code= (int)HttpStatusCode.InternalServerError, message= "An error occurred", url=httpContext.Request.Path.Value });

               await httpContext.Response.WriteAsync(result);

            }
        }
    }
}
