using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.Implementations;

public class ProductRepository(ApplicationDbContext db) : 
    Repository<Product>(db), IProductRepository
{
    private readonly ApplicationDbContext _db = db;

    public void Update(Product obj)
    {
        _db.Products.Update(obj);
    }
}