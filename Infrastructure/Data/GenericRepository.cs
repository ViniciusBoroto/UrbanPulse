using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    protected readonly StoreContext _context;

    public GenericRepository(StoreContext context)
    {
        _context = context;
    }
    //Gets
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }
    public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
    {
        return await ApplySpecification(spec).ToListAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        _context.SaveChanges();
        return entity;
    }
    public Task<T> UpdateAsync(int id, T entity)
    {
        throw new NotImplementedException();
    }

    public async Task<T> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
            _context.Remove(entity);
        _context.SaveChanges();
        return entity;
    }

    public Task<IReadOnlyList<T>> ListAllAsync()
    {
        throw new NotImplementedException();
    }


    private IQueryable<T> ApplySpecification(ISpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable() ,spec);
    }
}
