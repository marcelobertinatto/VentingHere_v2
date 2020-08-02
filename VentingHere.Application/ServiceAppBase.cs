using System;
using System.Collections.Generic;
using System.Text;
using VentingHere.Application.Interface;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Application
{
    public class ServiceAppBase<TEntity> : IDisposable, IServiceAppBase<TEntity> where TEntity : class
    {
        private readonly IServiceBase<TEntity> _serviceAppBase;
        public ServiceAppBase(IServiceBase<TEntity> serviceAppBase)
        {
            _serviceAppBase = serviceAppBase;
        }
        public void Add(TEntity entity) => _serviceAppBase.Add(entity);

        public void AddRange(List<TEntity> entities) => _serviceAppBase.AddRange(entities);

        public void Dispose() => _serviceAppBase.Dispose();

        public List<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> expression) => _serviceAppBase.Find(expression);

        public List<TEntity> GetAll() => _serviceAppBase.GetAll();

        public TEntity FindById(int id) => _serviceAppBase.FindById(id);

        public void Remove(TEntity entity) => _serviceAppBase.Remove(entity);

        public void RemoveRange(List<TEntity> entities) => _serviceAppBase.RemoveRange(entities);

        public void Update(TEntity entity) => _serviceAppBase.Update(entity);

        public void UpdateRange(List<TEntity> entities) => _serviceAppBase.UpdateRange(entities);
    }
}
