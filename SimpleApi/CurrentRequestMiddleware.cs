using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi
{
    public class CurrentRequestMiddleware
    {
        private const string CorrelationIdHeader = "X-Correlation-ID";

        private readonly RequestDelegate next;

        public CurrentRequestMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
            {
                // update trace identifier with the correlation id found in headers
                context.TraceIdentifier = correlationId;
            }

            // apply the correlation id to the response header
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CorrelationIdHeader, new[] { context.TraceIdentifier });
                return Task.CompletedTask;
            });

            await next.Invoke(context);
        }
    }
}
