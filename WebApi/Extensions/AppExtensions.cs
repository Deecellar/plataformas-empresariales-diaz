using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LongLongApi.Template");
            });
        }
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
        public static void InitializeDatabase( IMigrationRunner runner)
        {
            runner.MigrateUp();
        }
        public static void UseCORS(this IApplicationBuilder app)
        {
            app.UseCors();
        }
    }
}
