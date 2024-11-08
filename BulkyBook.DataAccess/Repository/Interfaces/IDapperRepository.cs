using System.Diagnostics.CodeAnalysis;
using Bulky.Models.Models;

namespace Bulky.DataAccess.Repository.Interfaces;

public partial interface IDapperRepository<T> where T : class
{
    Category GetCategoryByIdDapper([DisallowNull] int? id);
    void DapperInsert(Category category);
    void DeleteCategoryByIdDapper(int? id);
}