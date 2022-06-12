using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skyNetApp.Helpers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CashedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSecond;

        public CashedAttribute()
        {
        }

        //itt seems like interceptor
        //filer allow code to run before or afterspecific stage and requestt processing pipeline
        // we have opportunity to run code immediately before or after action metthod
        // we have to do both because before action method we have to see if we have the thing they are asked
        //inside our cache and if we don't have inside our cach we will go to execute request
        //and then store the reult in the cash

        public CashedAttribute(int timeToLiveSecond)
        {
            _timeToLiveSecond = timeToLiveSecond;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //get cahservice
            var cahceService = context.HttpContext.RequestServices.GetRequiredService<IResponeCacheService>();
            var cachKey = GenerateCashKeyFromRequest(context.HttpContext.Request);
            //see if we have cachkey in redis
            //before executing action
            var cashresponse = await cahceService.GetCashedResponseAsync(cachKey);
            if (!String.IsNullOrEmpty(cashresponse))
            {
                //we get reponseform memory not form control
                var ConentResult = new ContentResult()
                {
                    Content = cashresponse,
                    ContentType = "application/json",
                    StatusCode = 200,

                };
                context.Result = ConentResult;
                return;
            }
            //aftter executing action
            var executedContext = await next();  //move to cionttroller
            if (executedContext.Result is OkObjectResult okObjectResult)
            {
                await cahceService.CachResponseAsync(cachKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSecond));

            }




        }

        private string GenerateCashKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");
            //
            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {

                keyBuilder.Append($"|{key}-{value}");

            }
            return keyBuilder.ToString();
        }
    }
}
