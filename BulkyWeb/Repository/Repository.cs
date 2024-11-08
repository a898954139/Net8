using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.Models.Models;
using BulkyWeb.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb.Repository;

public class Repository<T>(
    ApplicationDbContext db,
    DbSet<T> dbSet) : 
    IRepository<T> where T : class
{
    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet; // Start with the DbSet
        query = query.Where(filter); // Apply the filter
        return query.FirstOrDefault(); 
    }

    public void Add(T entity)
    {
        dbSet.Add(entity);
    }
    
    public void Delete(T entity)
    {
        dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}