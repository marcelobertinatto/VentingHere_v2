using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace VentingHere.Application.Interface
{
    public interface IServiceAppBase<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(List<TEntity> entities);
        TEntity FindById(int id);
        List<TEntity> Find(Expression<Func<TEntity, bool>> expression);
        List<TEntity> GetAll();
        void Dispose();
    }
}
