using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using SqlKata;
using SqlKata.Execution;

namespace Infrastructure.Persistence.Repositories
{
    public class FileServiceRepositoryAsync : GenericRepositoryAsync<FileService, int>, IFileServiceRepositoryAsync
    {
        public FileServiceRepositoryAsync(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : base(unitOfWork, authenticatedUser, "FileServices")
        {
        }

        public async Task<bool> CheckIfServiceExist(int FileServiceId)
        {
            return (await GetByIdAsync(FileServiceId)) != null;
        }
    }
}
