using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using FluentMigrator.Runner;
using Infrastructure.Persistence.Migrations;
using Microsoft.Extensions.Configuration;
using SqlKata.Compilers;
using SqlKata.Execution;
using Infrastructure.Shared;

namespace WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LongLongApi.Template.xml"));
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Clean Architecture - WebApi",
                    Description = "This Api will be responsible for overall data distribution and authorization.",
                    Contact = new OpenApiContact
                    {
                        Name = "LongLongDragon",
                        Email = "adming@lldragon.net",
                        Url = new Uri("https://lldragon.net/contact"),
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                    },
                });
            });
        }
        public static void AddApiVersioningExtension(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
        }
        public static void AddDatabaseEngineServices(this IServiceCollection services,
            IConfiguration configuration, ConnectionType type)
        {
            var databaseConnectionString = configuration.GetConnectionString("DefaultConnectionString");

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddContextualMigration(type)
                    .WithGlobalConnectionString(databaseConnectionString)
                    .ScanIn(typeof(InitialMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);

        }

        public static void AddHealth(this IHealthChecksBuilder healthChecks, IConfiguration configuration, ConnectionType connection)
        {
            healthChecks.AddPingHealthCheck(x => x.AddHost(configuration.GetSection("PingHealth").GetValue<string>("Host"), configuration.GetSection("Health").GetValue<int>("TimeOut")))
                        .AddAzureBlobStorage($"DefaultEndpointsProtocol=https;AccountName={configuration.GetSection("Azure")["BlobStorageAccount"]};AccountKey={configuration.GetSection("Azure")["BlobStorageKey"]}")
                        .AddHealthDatabase(configuration, connection)
                        .AddDiskStorageHealthCheck(x => x.AddDrive(DriveInfo.GetDrives().First().Name, 120));
        }

        public static IHealthChecksBuilder AddHealthDatabase(this IHealthChecksBuilder healthChecks, IConfiguration configuration, ConnectionType type)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            switch (type)
            {
                case ConnectionType.SqlServer:
                    healthChecks.AddSqlServer(connectionString);
                    break;
                case ConnectionType.Sqlite:
                    healthChecks.AddSqlite(connectionString);
                    break;
                case ConnectionType.MySql:
                    healthChecks.AddMySql(connectionString);
                    break;
                case ConnectionType.OracleDb:
                    healthChecks.AddOracle(connectionString);
                    break;
                case ConnectionType.PostgresSql:
                    healthChecks.AddNpgSql(connectionString);
                    break;
            }
            return healthChecks;
        }
        public static void AddHealthUI(this IServiceCollection services, IConfiguration configuration, ConnectionType type)
        {
            switch (type)
            {
                case ConnectionType.SqlServer:
                    services.AddHealthChecksUI()
                            .AddSqlServerStorage(configuration.GetConnectionString("HealthDatabase"));
                    break;
                case ConnectionType.Sqlite:
                    services.AddHealthChecksUI()
            .AddSqliteStorage(configuration.GetConnectionString("HealthDatabase"));
                    break;
                case ConnectionType.MySql:
                    services.AddHealthChecksUI()
            .AddMySqlStorage(configuration.GetConnectionString("HealthDatabase"));
                    break;
                case ConnectionType.PostgresSql:
                    services.AddHealthChecksUI()
            .AddPostgreSqlStorage(configuration.GetConnectionString("HealthDatabase"));
                    break;
            }

        }

        public static void AddCORS(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                                builder =>
                                {
                                    builder.AllowAnyOrigin()
                                    .AllowAnyMethod()
                                    .AllowAnyHeader();
                                });
            });
        }

        public static IMigrationRunnerBuilder AddContextualMigration(this IMigrationRunnerBuilder migrationRunner, ConnectionType type)
        {
            switch (type)
            {
                case ConnectionType.SqlServer:
                    migrationRunner.AddSqlServer();
                    break;
                case ConnectionType.Sqlite:
                    migrationRunner.AddSQLite();
                    break;
                case ConnectionType.MySql:
                    migrationRunner.AddMySql5();
                    break;
                case ConnectionType.FireBird:
                    migrationRunner.AddFirebird();
                    break;
                case ConnectionType.OracleDb:
                    migrationRunner.AddOracleManaged();
                    break;
                case ConnectionType.PostgresSql:
                    migrationRunner.AddPostgres();
                    break;
            }

            return migrationRunner;
        }
    }
}
