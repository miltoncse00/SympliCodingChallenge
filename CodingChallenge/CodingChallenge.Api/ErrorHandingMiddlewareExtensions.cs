using Microsoft.AspNetCore.Builder;
using CodingChallenge.Api;

namespace CodingChallenge.Api
{
    public static class ErrorHandingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
