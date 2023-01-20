using System.Data;
using System;
using Application.Interfaces.Repositories;
using System.Threading.Tasks;
using SqlKata;

namespace Application.Interfaces
{
    public interface IUnitOfWork  : IDisposable
    {
        #region Repositories
        IFileRepositoryAsync FileRepository { get; }
        IProductRepositoryAsync ProductRepository { get; }
        IFileServiceRepositoryAsync FileServiceRepository { get; }
        #endregion
        IDbTransaction Transaction { get; }
        Task Commit();
        Query Query(string table);
        Query Query(Query query);
    }
}
