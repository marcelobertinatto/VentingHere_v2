using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using VentingHere.Domain.Repository.Interfaces;
using VentingHere.Domain.Repository.Services;

namespace VentingHere.Domain
{
    public class ServiceBase<TEntity> : IDisposable, IServiceBase<TEntity> where TEntity : class
    {
        private readonly IRepositoryBase<TEntity> _repositoryBase;
        public ServiceBase(IRepositoryBase<TEntity> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public void Add(TEntity entity) => _repositoryBase.Add(entity);

        public void AddRange(List<TEntity> entities) => _repositoryBase.AddRange(entities);

        public void Dispose() => _repositoryBase.Dispose();

        public List<TEntity> Find(Expression<Func<TEntity, bool>> expression) => _repositoryBase.Find(expression);
        public List<TEntity> GetAll() => _repositoryBase.GetAll();

        public TEntity FindById(int id) => _repositoryBase.FindById(id);

        public void Remove(TEntity entity) => _repositoryBase.Remove(entity);

        public void RemoveRange(List<TEntity> entities) => _repositoryBase.RemoveRange(entities);

        public void Update(TEntity entity) => _repositoryBase.Update(entity);

        public void UpdateRange(List<TEntity> entities) => _repositoryBase.UpdateRange(entities);
    }
}
