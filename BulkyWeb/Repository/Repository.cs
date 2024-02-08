using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Dapper;
using Microsoft.Extensions.Options;

namespace BulkyWeb.Repository;

public class Repository : IRepository
{
    private string _connectionString;

    public Repository(
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
        var query = @"SELECT * FROM [dbo].[categories] WHERE Id = @id";
        using var conn = new SqlConnection(_connectionString);
        conn.Open();
        return conn.Query<Category>(query, new { Id = id }).FirstOrDefault() ?? throw new ArgumentException();
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
}