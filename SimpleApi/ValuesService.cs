using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApi
{
    public interface IValuesService
    {
        string GetTraceIdentifier();
    }
    public class ValuesService : IValuesService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ValuesService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetTraceIdentifier()
        {
            return  httpContextAccessor.HttpContext.TraceIdentifier;
        }
    }
}
