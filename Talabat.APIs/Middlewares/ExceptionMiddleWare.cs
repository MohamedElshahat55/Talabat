using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middlewares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleWare(RequestDelegate Next , ILogger<ExceptionMiddleWare> logger , IWebHostEnvironment env)
        {
            _next = Next;
            _logger = logger;
            _env = env;
        }

        // InvokAsync

        public async Task InvokeAsync(HttpContext content)
        {
            try
            {
                await _next.Invoke(content);
            }catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                // In Production Case => Log Ex in database
                content.Response.ContentType = "application/json";
                content.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //if(_env.IsDevelopment())
                //{
                //    var Response = new ApiExceptionResponse(500,ex.Message,ex.StackTrace.ToString());
                //}
                //else
                //{
                //    var Response = new ApiExceptionResponse(500);
                //}
               var Response =  _env.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse(500);
                // Send the response
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var JsonResponse = JsonSerializer.Serialize(Response, options);
                content.Response.WriteAsync(JsonResponse);
            }
        }
    }
}
