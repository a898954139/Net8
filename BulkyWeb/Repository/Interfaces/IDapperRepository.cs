using System.Diagnostics.CodeAnalysis;
using Bulky.Models.Models;

namespace BulkyWeb.Repository.Interfaces;

public partial interface IDapperRepository<T> where T : class
{
    Category GetCategoryByIdDapper([DisallowNull] int? id);
    void DapperInsert(Category category);
    void DeleteCategoryByIdDapper(int? id);
}