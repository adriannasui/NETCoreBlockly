using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetCore2Blockly;
using TestBlocklyHtml.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace TestBlocklyHtml
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddBlockly();
            services.AddDbContext<testsContext>(options => options
              
              .UseInMemoryDatabase(databaseName: "MyDB"));
            //this is not necessary to be added
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "Please see blockly.html", 
                    Version = "v1" ,
                    Description= " Please see https://github.com/ignatandrei/netcoreblockly"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //just developer testing! do not use in production
            app.UseFileServer(enableDirectoryBrowsing: true);
            //this is not necessary to be added
            app.UseSwagger();

            //this is not necessary to be added
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseBlockly(); 
        }
    }
}
