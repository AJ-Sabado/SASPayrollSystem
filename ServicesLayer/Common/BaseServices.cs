using DomainLayer.Models.Role;
using InfrastructureLayer.DataAccess.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Common
{
    public class BaseServices<T> : IBaseServices<T> where T : class
    {
        private IBaseRepository<T> _repository;
        private IModelDataAnnotationsCheck _modelDataAnnotationsCheck;

        public BaseServices(IBaseRepository<T> repository, IModelDataAnnotationsCheck modelDataAnnotatoinsCheck)
        {
            _repository = repository;
            _modelDataAnnotationsCheck = modelDataAnnotatoinsCheck;
        }

        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public async Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> filter = null, string? includeProperties = null)
        {
            return await _repository.GetManyAsync(filter, includeProperties);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            return await _repository.GetAsync(filter, includeProperties);
        }

        public async Task RemoveAsync(T entity)
        {
            await _repository.RemoveAsync(entity);
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            await _repository.RemoveRangeAsync(entities);
        }

        public async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            await _repository.UpdateRangeAsync(entities);
        }

        public void ValidateModelDataAnnotations(T domainModel)
        {
            _modelDataAnnotationsCheck.ValidateModelDataAnnotations(domainModel);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
