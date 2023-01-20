using Application.Interfaces;
using Domain.Settings;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storage.Net.Blobs;
using Storage.Net;

namespace Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<MailSettings>(_config.GetSection("MailSettings"));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<Storage.Net.Blobs.IBlobStorage>(x => StorageFactory.Blobs.AzureBlobStorageWithSharedKey(_config.GetSection("Azure").GetValue<string>("BlobStorageAccount"),_config.GetSection("Azure").GetValue<string>("BlobStorageKey")).WithGzipCompression());
            services.AddTransient<IFileService,FileService>();
        }
    }
}
