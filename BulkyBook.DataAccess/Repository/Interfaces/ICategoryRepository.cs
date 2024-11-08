using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category obj);
}