using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace VentingHere.Domain.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        TEntity FindById(int id);
        List<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        void Dispose();
    }
}
