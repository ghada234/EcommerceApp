using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Extentions
{
    public static class SwaggerSrevicesExtention
    {

        public static IServiceCollection AddSwaggerDocumentaion(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "skyNetApp", Version = "v1" });
            });

            return services;


        }

        public static IApplicationBuilder UserSwaggerDocumentation(this IApplicationBuilder app) {

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "skyNetApp v1"));
            return app;
        }
    }
}
