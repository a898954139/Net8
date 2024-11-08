namespace Bulky.DataAccess.Repository.Interfaces;

public interface IDatabaseFactory
{
    ICategoryRepository CategoryRepository { get; }
    void Save();
}