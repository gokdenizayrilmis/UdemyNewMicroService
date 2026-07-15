using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UdemyNewMicroService.Order.Application.Contracts
{
    public interface IGenericRepository<TId,TEntity> where TEntity : class where TId : struct
    {
        public Task<bool> AnyAsync(TId id);
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllPagedAsync(int pageNumber, int pageSize);
        ValueTask<TEntity> GetByIdAsync(TId id);
        ValueTask AddAsync(TEntity entity);
        void AddR(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
