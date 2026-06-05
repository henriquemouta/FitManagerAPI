using System.Net;
using System.Text.Json;

namespace FitManagerAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.Unauthorized,
                    ex.Message);
            }
            catch (ArgumentException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.NotFound,
                    ex.Message);
            }
            catch (Exception)
            {
                await HandleException(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Erro interno do servidor.");
            }
        }

        private static async Task HandleException(
            HttpContext context,
            HttpStatusCode statusCode,
            string message)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var response = new
            {
                success = false,
                message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}