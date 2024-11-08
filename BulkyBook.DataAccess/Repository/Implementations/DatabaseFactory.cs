using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;

namespace Bulky.DataAccess.Repository.Implementations;

public class DatabaseFactory(ApplicationDbContext db) : 
    IDatabaseFactory
{
    public ICategoryRepository CategoryRepository { get; } = 
        new CategoryRepository(db);
    
    public IProductRepository ProductRepository { get; } = 
        new ProductRepository(db);
    public void Save()
    {
        db.SaveChanges();
    }
}