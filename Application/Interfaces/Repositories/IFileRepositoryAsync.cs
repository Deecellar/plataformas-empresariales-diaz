using System;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IFileRepositoryAsync : IGenericRepositoryAsync<File,Guid>
    {
        
    }
}
