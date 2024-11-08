using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace Bulky.DataAccess.Repository.Implementations;

public class DapperRepository : IDapperRepository<Category>
{
    private string _connectionString;

    public DapperRepository(
        IOptionsMonitor<AppSettingsModel> settings)
    {
        _connectionString = settings.CurrentValue.BulkyDB;
        settings.OnChange(x =>
        {
            _connectionString = settings.CurrentValue.BulkyDB;
        });
    }

    public Category GetCategoryByIdDapper([DisallowNull] int? id)
    {
        var sql = @"SELECT * FROM [dbo].[categories] WHERE Id = @id";
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn.Query<Category>(sql, new { Id = id }).FirstOrDefault() ?? throw new Exception("Cannot find category");
    }

    public void DapperInsert(Category category)
    {
        using var con = new SqlConnection(_connectionString);
        var sql = 
        @"
          INSERT INTO [dbo].[Categories] (Name, DisplayOrder)
          VALUES (@Name, @DisplayOrder)
        ";
        con.Execute(sql, new
        {
            category.Name,
            category.DisplayOrder
        });
    }

    public void DeleteCategoryByIdDapper(int? id)
    {
        using var conn = new SqlConnection(_connectionString);
        var sql = @"DELETE FROM [dbo].[categories] WHERE Id = @id";
        conn.Execute(sql, new { id });
    }

    public IEnumerable<Category> GetAll()
    {
        throw new NotImplementedException();
    }

    public Category Get(Expression<Func<Category, bool>> filter)
    {
        throw new NotImplementedException();
    }

    public void Add(Category entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Category entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Category entity)
    {
        throw new NotImplementedException();
    }

    public void DeleteRange(IEnumerable<Category> entity)
    {
        throw new NotImplementedException();
    }
}