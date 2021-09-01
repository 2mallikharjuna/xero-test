using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using RefactorThis.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace RefactorThis
{
    public class Startup
    {
        /// <summary>
        /// Get the Configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// startup class constructor
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddDependencies(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Add serilog support for logging
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
           
            object p = app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xero Products API V1");
            });
            app.Map("/HealthCheck", a =>
            {
                a.Run(async context =>
                {
                    await context.Response.WriteAsync($"{env.ApplicationName} is alive in {env.EnvironmentName}");
                });
            });
            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}