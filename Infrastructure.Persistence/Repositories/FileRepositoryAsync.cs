using System;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Domain.Entities;
using SqlKata;
using SqlKata.Execution;

namespace Infrastructure.Persistence.Repositories
{
    public class FileRepositoryAsync : GenericRepositoryAsync<File,Guid>, IFileRepositoryAsync
    {
        public FileRepositoryAsync(IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser) : base(unitOfWork, authenticatedUser, "Files")
        {
        }

    }
}
