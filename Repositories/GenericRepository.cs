using LibraryManagementNet9.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryManagementNet9.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly LibraryContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(LibraryContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "",
        int? skip = null,
        int? take = null)
    {
        IQueryable<T> query = _dbSet.AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProp.Trim());
        }

        if (orderBy != null)
            query = orderBy(query);

        if (skip.HasValue) query = query.Skip(skip.Value);
        if (take.HasValue) query = query.Take(take.Value);

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate != null) return await _dbSet.CountAsync(predicate);
        return await _dbSet.CountAsync();
    }

    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is null) throw new KeyNotFoundException($"{typeof(T).Name} not found");
        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
