using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Common.Models;

namespace WebApi.Infrastructure.Filters
{
    public class RequestScopeFilter : Attribute, IResourceFilter
    {
        RequestScope requestScope;

        public RequestScopeFilter(RequestScope requestScope)
        {
            this.requestScope = requestScope;
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            LogContext.PushProperty("RequestScope", requestScope.Guid.ToString());
        }
    }
}
