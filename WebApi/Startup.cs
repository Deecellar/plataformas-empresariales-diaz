using System.ComponentModel;
using Application;
using Application.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            services.AddHealthChecks();
            services.AddHealthUI(Config, type);
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
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCORS();
            app.UseSwaggerExtension();
            app.UseErrorHandlingMiddleware();
            //app.UseHealthChecks("/health");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
