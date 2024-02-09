using System.Configuration;
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

    # region Constructer

    public CategoryController(ApplicationDbContext db, IRepository repository)
    {
        _db = db;
        _repository = repository;
    }

    # endregion

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
        if (!ModelState.IsValid)
        {
            if (Regex.IsMatch(category.Name, @"(?i)fuck"))
                ModelState.AddModelError("name", "Contains sensitive keywords");
            return View();
        }

        TryInsertValue(category);
        return RedirectToAction("Index");
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
        TryUpdateCategory(category);
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
        if (IsInvalidId(id))
            return NotFound();
        TryDeleteCategory(id);
        return RedirectToAction("Index");
    }


    # region private methods

    private void TryDeleteCategory(int? id)
    {
        try
        {
            // _repository.DeleteCategoryByIdDapper(id);
            DeleteCategoryByIdEfCore(id);
            TempData["success"] = "Category deleted successfully";
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            throw;
        }
    }

    private static bool IsInvalidId(int? id)
    {
        return id is null or 0;
    }

    private void DeleteCategoryByIdEfCore(int? id)
    {
        _db.Categories.Remove(_db.Categories.Find(id) ?? throw new Exception("Category not found"));
        _db.SaveChanges();
    }

    private void TryInsertValue(Category category)
    {
        try
        {
            // EfCoreInsert(category);
            _repository.DapperInsert(category);
            TempData["success"] = "Category successfully created.";
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            throw;
        }
    }

    private void TryUpdateCategory(Category category)
    {
        try
        {
            _db.Update(category);
            TempData["success"] = "Category updated successfully.";
            _db.SaveChanges();
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            throw;
        }
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

    # endregion
}