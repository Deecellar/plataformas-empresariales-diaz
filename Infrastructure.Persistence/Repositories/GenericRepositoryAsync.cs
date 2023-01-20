using System.Dynamic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Common;
using SqlKata.Execution;
using System.Collections.ObjectModel;
using SqlKata;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<T,Tid> : IGenericRepositoryAsync<T,Tid> where T : class, IEntity<Tid> where Tid: struct
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly string _table;
        protected readonly string _IdColumn;
        private readonly IAuthenticatedUserService _authenticatedUser;

        protected GenericRepositoryAsync( IUnitOfWork unitOfWork, IAuthenticatedUserService authenticatedUser, string table, string idName = "Id")
        {
            _unitOfWork = unitOfWork;
            _authenticatedUser = authenticatedUser;
            _table = table;
            _IdColumn = idName;
        }

        public virtual async Task<T> GetByIdAsync(Tid id)
        {
            return (await _unitOfWork.Query(_table).Where(_IdColumn,id).GetAsync<T>()).FirstOrDefault();
        }

        public async Task<IReadOnlyList<T>> GetPagedResponsesAsync(int pageNumber, int pageSize)
        {
            return (await _unitOfWork.Query(_table)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .GetAsync<T>())
                .ToList();
        }

        public async Task<T> AddAsync(T entity)
        {
            IDictionary<string,object>  entityNoID =  new Dictionary<string,object>();

            if (entity is AuditableBaseEntity<Tid> baseEntity)
            {
                baseEntity.Created = DateTime.UtcNow;
                baseEntity.CreatedBy= _authenticatedUser.UserId ?? "Anonymous";
            } 
            if(typeof(Tid) == typeof(Guid) ){
                await _unitOfWork.Query(_table).InsertAsync(entity,_unitOfWork.Transaction);
            await _unitOfWork.Commit();
                return entity;
            }
            
            foreach(var e in entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)){
                if(e.Name != _IdColumn)
                entityNoID[e.Name] = e.GetValue(entity);
            }
            await _unitOfWork.Query(_table).InsertAsync(new ReadOnlyDictionary<string,object>(entityNoID),_unitOfWork.Transaction);
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity is AuditableBaseEntity<Tid> baseEntity)
            {
                baseEntity.LastModified = DateTime.UtcNow;
                baseEntity.LastModifiedBy = _authenticatedUser.UserId;
            } 
            await _unitOfWork.Query(_table).UpdateAsync(entity,_unitOfWork.Transaction);
        }

        public async Task DeleteAsync(T entity)
        {
            await _unitOfWork.Query(_table).Where(_IdColumn,entity.Id).DeleteAsync(_unitOfWork.Transaction);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return (await _unitOfWork.Query(_table).GetAsync<T>()).ToList();
        }
    }
}
