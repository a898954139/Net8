using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository.Implementations;

public class CategoryRepository(
    ApplicationDbContext db) : 
    Repository<Category>(db), ICategoryRepository
{
    private readonly ApplicationDbContext _db = db;

    public void Update(Category obj)
    {
       _db.Categories.Update(obj);
    }

    public void Save()
    {
        _db.SaveChanges();
    }
}