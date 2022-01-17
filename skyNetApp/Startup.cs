using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using skyNetApp.Extentions;
using skyNetApp.Helpers;
using skyNetApp.MiddleWare;

namespace skyNetApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
         
            _configuration = configuration;
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //addscoped life until  http request finish or periode of it is the htttp request 

            //add auto mapper  ===> profile assembly iss location of profile mapping
            services.AddAutoMapper(typeof(MappingProfile));


            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_configuration.GetConnectionString("DefaultConnection")));

            //two extention methods
            services.AddServices();
            services.AddSwaggerDocumentaion();
            //
            //overide the behaviour of [ApiController ]attrubitue to get valifdation errors list as this attrbuite is responsible for get the value of parameter rout



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //use excetoption middle ware
            app.UseMiddleware<ExceptionMiddleWare>();
            
            //if (env.IsDevelopment())
            //{
            //    //app.UseDeveloperExceptionPage();
                
            //}

            // when send request to apu . if don't  have end point that match particular request so rexecute it to the path errors/{code}
            app.UseStatusCodePagesWithReExecute("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles();

            app.UseAuthorization();
            //extention method
            app.UserSwaggerDocumentation();
            //

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
