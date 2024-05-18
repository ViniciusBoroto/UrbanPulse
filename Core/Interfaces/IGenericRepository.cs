using Core.Entities;
using Core.Specifications;
using System.Linq.Expressions;

namespace Core.Interfaces;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> GetEntityWithSpecAsync(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(int id, T entity);
    Task<T> DeleteAsync(int id);
}
