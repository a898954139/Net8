namespace Bulky.DataAccess.Repository.Interfaces;

public interface IDatabaseFactory
{
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }
    void Save();
}