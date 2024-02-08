using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using BulkyWeb.Data;
using BulkyWeb.Models;
using BulkyWeb.Repository;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly IRepository _repository;

    public CategoryController(ApplicationDbContext db, IRepository repository)
    {
        _db = db;
        _repository = repository;
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
            _repository.DapperInsert(category);
            return RedirectToAction("Index");
        }
        if (Regex.IsMatch(category.Name, @"(?i)fuck"))
            ModelState.AddModelError("name", "Contains sensitive keywords");
        return View();
    }
    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
            return NotFound();
        return View(_repository.GetCategoryByIdDapper(id));
    }
    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid) 
            return View();
        _db.Update(category);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
            return NotFound();
        return View(_repository.GetCategoryByIdDapper(id));
    }    
    [HttpPost, ActionName("Delete")]
    public IActionResult PostDelete(int? id)
    {
        if (id is null or 0)
            return NotFound();
        // _repository.DeleteCategoryByIdDapper(id);
        DeleteCategoryByIdEfCore(id);
        return RedirectToAction("Index");
    }

    private void DeleteCategoryByIdEfCore(int? id)
    {
        _db.Categories.Remove(_db.Categories.Find(id) ?? throw new Exception("Category not found"));
        _db.SaveChanges();
    }

    private void InsertCategoryEfCore(Category category)
    {
        _db.Add(category);
        _db.SaveChanges();
    }
    private Category GetCategoryByIdEfCore([DisallowNull] int? id)
    {
        return _db.Categories.FirstOrDefault(x => x.Id == id)
            ?? throw new Exception();
    }
}