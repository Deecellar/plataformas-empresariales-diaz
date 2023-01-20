using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IFileServiceRepositoryAsync : IGenericRepositoryAsync<Domain.Entities.FileService, int>
    {
        Task<bool> CheckIfServiceExist(int FileServiceId);

        
    }
}
