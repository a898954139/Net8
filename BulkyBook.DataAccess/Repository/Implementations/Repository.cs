using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository.Implementations;

public class Repository<T>(
    ApplicationDbContext db) : 
    IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = db.Set<T>();
    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = _dbSet;
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = _dbSet; // Start with the DbSet
        query = query.Where(filter); // Apply the filter
        return query.FirstOrDefault(); 
    }

    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }
    
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entity)
    {
        _dbSet.RemoveRange(entity);
    }
}