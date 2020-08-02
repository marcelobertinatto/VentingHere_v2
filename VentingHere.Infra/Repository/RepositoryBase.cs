using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using VentingHere.Domain.Repository.Interfaces;

namespace VentingHere.Infra.Repository
{
    public class RepositoryBase<TEntity> : IDisposable,IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly VentingContext _ventingContext;
        public RepositoryBase(VentingContext ventingContext) : base() => _ventingContext = ventingContext;
        public void Add(TEntity entity)
        {
            _ventingContext.Add(entity);
            _ventingContext.SaveChanges();
        }

        public void AddRange(List<TEntity> entities)
        {
            _ventingContext.AddRange(entities);
            _ventingContext.SaveChanges();
        }

        public void Dispose() => _ventingContext.Dispose();

        public List<TEntity> Find(Expression<Func<TEntity, bool>> expression) => _ventingContext.Set<TEntity>().Where(expression).ToList();

        public List<TEntity> GetAll() => _ventingContext.Set<TEntity>().ToList();

        public TEntity FindById(int id) => _ventingContext.Set<TEntity>().Find(id);

        public void Remove(TEntity entity)
        {
            _ventingContext.Entry(entity).State = EntityState.Detached;
            _ventingContext.Set<TEntity>().Remove(entity);
            _ventingContext.SaveChanges();
        }

        public void RemoveRange(List<TEntity> entities)
        {
            _ventingContext.Entry(entities).State = EntityState.Detached;
            _ventingContext.Set<TEntity>().RemoveRange(entities);
            _ventingContext.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            _ventingContext.Entry(entity).State = EntityState.Detached;
            //_ventingContext.Entry(entity).State = EntityState.Modified;
            _ventingContext.Set<TEntity>().Update(entity);
            _ventingContext.SaveChanges();
        }

        public void UpdateRange(List<TEntity> entities)
        {
            _ventingContext.Entry(entities).State = EntityState.Detached;
            _ventingContext.Set<TEntity>().UpdateRange(entities);
            _ventingContext.SaveChanges();
        }

        public virtual void DetachLocal(Func<TEntity,bool> predicate)
        {
            var local = _ventingContext.Set<TEntity>().Local.Where(predicate).FirstOrDefault();
            if (local != null)
            {
                _ventingContext.Entry(local).State = EntityState.Detached;
            }
        }
    }
}
