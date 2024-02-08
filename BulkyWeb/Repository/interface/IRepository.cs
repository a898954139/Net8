using System.Diagnostics.CodeAnalysis;
using BulkyWeb.Models;

namespace BulkyWeb.Repository;

public interface IRepository
{
    Category GetCategoryByIdDapper([DisallowNull] int? id);
    void DapperInsert(Category category);
    void DeleteCategoryByIdDapper(int? id);
}