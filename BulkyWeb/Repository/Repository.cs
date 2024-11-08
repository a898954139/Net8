using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Bulky.Models.Models;
using BulkyWeb.Repository.Interfaces;

namespace BulkyWeb.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    public IEnumerable<T> GetAll()
    {
        throw new NotImplementedException();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public void Add(T entity)
    {
        throw new NotImplementedException();
    }

    public void Update(T entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(IEnumerable<T> entity)
    {
        throw new NotImplementedException();
    }
}