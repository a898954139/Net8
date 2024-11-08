using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    public void Update(Product obj);
}