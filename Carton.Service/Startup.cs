using System;
using Carton.Service.Storage;
using Carton.Service.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Carton.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add middleware
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                // Enable the swagger documentation endpoints
                c.SwaggerDoc("v1", new Info { Title = "Carton API", Version = "v1", Description = "Carton's RESTful web api" });
                // Enable the swagger documentation xml comment parser
                c.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory, "Carton.xml"));
            });

            // Bind dependencies
            services.AddTransient<ITime, Time>();
            services.AddSingleton<ICartStore, InMemoryCartStore>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Carton API V1");
            });
        }
    }
}
