using Core.Entities.Identity;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using skyNetApp.Extentions;
using skyNetApp.Helpers;
using skyNetApp.MiddleWare;
using StackExchange.Redis;

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

            //new dattabase for identity
            services.AddDbContext<AppIdentityDbContext>(x =>
            {
                x.UseSqlite(_configuration.GetConnectionString("IdentityConnection"));
                }
            );
                //redis configurattio
                //redis connection is designed to shared and reused between between callers,fully thread save 

            services.AddSingleton<IConnectionMultiplexer>(x=> {
                var configration = ConfigurationOptions.Parse(_configuration.GetConnectionString("Redis"),true);
                return ConnectionMultiplexer.Connect(configration);

            });

            //three extention methods
            services.AddServices();
            services.AddIdentityServices(_configuration);
            services.AddSwaggerDocumentaion();

            services.AddCors(opt=>opt.AddPolicy("corsPolicy",Policy=> {
                Policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("https://localhost:4200");
            
            }));
            //services.AddIdentityCore<AppUser>();
         
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
            app.UseCors("corsPolicy");
            app.UseAuthentication();
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
