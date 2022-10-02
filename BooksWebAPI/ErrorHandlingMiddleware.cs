using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace BooksWebAPI
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {//before end point
                await _next(context);
            }//after end point
            catch (ArgumentException)
            {
                context.Response.StatusCode = 405;
            }
        }
    }
}