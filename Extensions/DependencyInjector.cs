using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RefactorThis.Repositories;
using RefactorThis.Services;
using System.IO;
using System.Reflection;
using System;

namespace RefactorThis.Extensions
{
    /// <summary>
    /// public static class Extensions
    /// </summary>
    public static class DependencyInjector
    {        
        /// <summary>
        /// Extension method to add the dependencies
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration">Base configuration</param>
        /// <returns></returns>
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {            
            services.AddOptions();                     
            services.Configure<IConfiguration>(configuration);
            services.AddSingleton(configuration);
            services.InjectDependencies(configuration);
            return services;
        }
        /// <summary>
        /// Create a dependency injection
        /// </summary>
        /// <param name="services">Base services</param>
        /// <param name="configuration"></param>
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IProductService, ProductService>();            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddTransient<IProductOptionsService, ProductOptionsService>();
            services.AddScoped<IProductOptionRepository, ProductOptionRepository>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //sql connection
            services.AddDbContext<AppDbContext>(options => options.UseSqlite(configuration["ConnectionStrings:Products"]));
            // Register the Swagger generator, defining 1 or more Swagger documents
            // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Xero Products API", Version = "v1" });

                // Get/Create xml comments path
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);

                // Set xml path
                options.IncludeXmlComments(xmlPath);
            });
        }
        /// <summary>
        /// Add Azure App insights support
        /// </summary>
        /// <param name="loggerConfiguration">Logger Configuration </param>
        /// <param name="configuration">Base configuration</param>
        /// <returns></returns>
        //public static LoggerConfiguration AddApplicationInsightsLogging(this LoggerConfiguration loggerConfiguration, IConfiguration configuration)
        //{
        //    if (!string.IsNullOrWhiteSpace(configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY")))
        //    {
        //        loggerConfiguration.WriteTo.ApplicationInsights(
        //            new TelemetryConfiguration { InstrumentationKey = configuration.GetValue<string>("APPINSIGHTS_INSTRUMENTATIONKEY") }, TelemetryConverter.Traces);
        //    }

        //    return loggerConfiguration;
        //}
    }
}
