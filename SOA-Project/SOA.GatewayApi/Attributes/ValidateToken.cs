using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SOA.GatewayApi.Services;
using System;
using System.Threading.Tasks;

namespace SOA.GatewayApi.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ValidateToken : Attribute, IAsyncActionFilter
    {
        private const string authorization = "Authorization";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var forbiddenResult =new ContentResult()
            {
                StatusCode = 401,
                Content = "Unauthorized"
            };
            if (context.HttpContext.Request.Headers.ContainsKey(authorization) &&
                context.HttpContext.Request.Headers[authorization][0].StartsWith("Bearer "))
            {
                var token = context.HttpContext.Request.Headers[authorization][0]
                    .Substring("Bearer ".Length);
                if (string.IsNullOrWhiteSpace(token))
                {
                    context.Result = forbiddenResult;
                    return;
                }
                var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                var serviceResult = await userService.GetUserFromToken(token);
                if (string.IsNullOrWhiteSpace(serviceResult))
                {
                    context.Result = forbiddenResult;
                    return;
                }
                context.HttpContext.TraceIdentifier = serviceResult;
                await next();

            }
            context.Result = forbiddenResult;
            return;
        }
    }
}
