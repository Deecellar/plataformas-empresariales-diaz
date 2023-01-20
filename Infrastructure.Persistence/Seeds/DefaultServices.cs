using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;

namespace Infrastructure.Persistence.Seeds
{
    public static class DefaultServices
    {
        public static async Task SeedAsync(IUnitOfWork repository){
            await repository.FileServiceRepository.AddAsync(new Domain.Entities.FileService{
                FileServiceName = "Azure Blob Storage"
            });
            await repository.Commit();
        }
    }
}