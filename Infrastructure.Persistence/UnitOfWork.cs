using System.Threading.Tasks;
using System.Data;
using System;
using Application.Interfaces;
using SqlKata;
using SqlKata.Execution;
using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;

namespace Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly QueryFactory _factory;
        private IDbTransaction _transaction;
        private readonly IAuthenticatedUserService _authenticatedUserService;

        #region Repositories
        public IProductRepositoryAsync ProductRepository
        {
            get
            {
                return new ProductRepositoryAsync(this, _authenticatedUserService);
            }
        }

        public IFileServiceRepositoryAsync FileServiceRepository => new FileServiceRepositoryAsync(this, _authenticatedUserService);
        public IFileRepositoryAsync FileRepository => new FileRepositoryAsync(this, _authenticatedUserService);

        #endregion
        public IDbTransaction Transaction => _transaction;


        public UnitOfWork(QueryFactory factory, IAuthenticatedUserService authenticatedUserService)
        {
            _factory = factory;
            _factory.Connection.Open();
            _transaction = _factory.Connection.BeginTransaction();
            _authenticatedUserService = authenticatedUserService;
        }
        public async Task Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                await Restart();
            }
        }

        private Task Restart()
        {
            if (_factory.Connection.State != ConnectionState.Open)
                _factory.Connection.Open();
            _transaction = _factory.Connection.BeginTransaction();
            return Task.CompletedTask;
        }

        public Query Query(string table)
        {
            return _factory.Query(table);
        }
        public Query Query(Query query)
        {
            return _factory.FromQuery(query);
        }

        public void Dispose()
        {
            _factory.Connection.Close();
            _factory.Dispose();
            _transaction = null;
        }



    }
}
