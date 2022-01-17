﻿using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using skyNetApp.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Extentions
{


 //clean startup file 
    public static class ApplicationServicesExtention
    {



        public static IServiceCollection AddServices(this IServiceCollection services) {

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepository<>)));
            //apibehaviouroptions is used to configure behaviour of types annotated with Apicontroller attrubitue
            services.Configure<ApiBehaviorOptions>(op => {
                op.InvalidModelStateResponseFactory = ActionContext =>
                {
                    //model state is dictionary and we use select many to flattenthis to one list
                    var errors = ActionContext.ModelState.Where(e => e.Value.Errors.Count > 0).SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToArray();

                    var errorresponse = new ApiValidaionErrorResponse
                    {
                        Errors = errors,
                    };
                    return new BadRequestObjectResult(errorresponse);
                };
            });

            return services;



        }
    }
}
