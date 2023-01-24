using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Data.SqlClient;
using FirebirdSql.Data.FirebirdClient;
using Infrastructure.Shared;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using SqlKata.Compilers;
using SqlKata.Execution;
using Application.Interfaces;
using SqlKata;

namespace Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration, ConnectionType connectionType = ConnectionType.Sqlite)
        {

            services.AddScoped(x => new QueryFactory(GetConnection(connectionType, configuration), GetCompiler(connectionType)));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            Dapper.SqlMapper.AddTypeHandler(new GuidDapperHandler());
            Dapper.SqlMapper.RemoveTypeMap(typeof(Guid));
            Dapper.SqlMapper.RemoveTypeMap(typeof(Guid?));

        }

        private static IDbConnection GetConnection(ConnectionType type, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                new SqliteConnection("Data Source= =>memory =>;Version=3;New=True;");
            }

            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            IDbConnection connection = type switch
            {
                ConnectionType.SqlServer =>
                      new SqlConnection(connectionString),
                ConnectionType.Sqlite =>
                      new SqliteConnection(connectionString),
                ConnectionType.MySql =>
                      new MySqlConnection(connectionString),
                ConnectionType.FireBird =>
                      new FbConnection(connectionString),
                ConnectionType.OracleDb =>
                      new OracleConnection(connectionString),
                ConnectionType.PostgresSql =>
                      new NpgsqlConnection(connectionString),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return connection;
        }

        private static Compiler GetCompiler(ConnectionType type)
        {
            Compiler compiler = type switch
            {
                ConnectionType.SqlServer => new SqlServerCompiler(),
                ConnectionType.Sqlite => new SqliteCompiler(),
                ConnectionType.MySql => new MySqlCompiler(),
                ConnectionType.FireBird => new FirebirdCompiler(),
                ConnectionType.OracleDb => new OracleCompiler(),
                ConnectionType.PostgresSql => new PostgresCompiler(),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };
            return compiler;
        }
    }
}
