using System.Data.SqlClient;
using System.Text.RegularExpressions;
using BulkyWeb.Data;
using BulkyWeb.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    private IOptionsMonitor<AppSettingsModel> _settings;

    public CategoryController(
        ApplicationDbContext db, 
        IOptionsMonitor<AppSettingsModel> settings)
    {
        _db = db;
        _settings = settings;
    }

    public IActionResult Index()
    {
        var category = _db.Categories.ToList();
        return View(category);
    }    
    public IActionResult Create()
    {
        return View();
    }  
    [HttpPost]
    public IActionResult Create(Category category)
    {
        if (ModelState.IsValid)
        {
            // EfCoreInsert(category);
            DapperInsert(category);
            return RedirectToAction("Index");
        }
        if (Regex.IsMatch(category.Name, @"(?i)fuck"))
            ModelState.AddModelError("name", "Contains sensitive keywords");
        return View();
    }

    private void DapperInsert(Category category)
    {
        using var con = new SqlConnection(_settings.CurrentValue.BulkyDB);
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

    private void EfCoreInsert(Category category)
    {
        _db.Add(category);
        _db.SaveChanges();
    }
}