using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Mime;
using Application;
using Application.Interfaces;
using HealthChecks.UI.Client;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using WebApi.Extensions;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Config { get; }
        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var type = ConnectionType.Sqlite;
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(Config);
            services.AddDatabaseEngineServices(Config, type);
            services.AddPersistenceInfrastructure(Config, type);
            services.AddSharedInfrastructure(Config);
            services.AddSwaggerExtension();
            services.AddCORS(Config);
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddHealthUI(Config, type);
            WebApi.Extensions.ServiceExtensions.AddHealth(services.AddHealthChecks(), Config, type);
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCORS();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter =  UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecksUI();
            });
        }
    }
}
