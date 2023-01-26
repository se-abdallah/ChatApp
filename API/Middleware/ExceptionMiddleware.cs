using System.Net;
using System.Text.Json;
using API.Errors;

namespace API.Middleware
{
 public class ExceptionMiddleware
 {
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionMiddleware> _logger;
  private readonly IHostEnvironment _env;

  public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
  {
   _env = env;
   _next = next;
   _logger = logger;
  }
  public async Task InvokeAsync(HttpContext context)
  {
<<<<<<< HEAD

=======
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
   try
   {
    await _next(context);
   }
   catch (Exception ex)
   {
<<<<<<< HEAD
    // Console.WriteLine("Error Message : " + ex);
    _logger.LogError(ex, ex.Message);
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server Error!");
=======
    _logger.LogError(ex, ex.Message);
    context.Response.ContentType = "Application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    var response = _env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) : new ApiException(context.Response.StatusCode, "Internal Server Error!");
>>>>>>> 929b681754124ca02c81088fac73c6ff8f2352f6
    var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    var jsonResponse = JsonSerializer.Serialize(response, jsonOptions);
    await context.Response.WriteAsync(jsonResponse);
   }
  }
 }
}