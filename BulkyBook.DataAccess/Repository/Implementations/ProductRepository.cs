using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.Implementations;

public class ProductRepository(ApplicationDbContext db) : 
    Repository<Product>(db), IProductRepository
{
    public void Update(Product obj)
    {
        db.Products.Update(obj);
    }
}