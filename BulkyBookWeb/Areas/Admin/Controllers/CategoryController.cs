using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController(
    ApplicationDbContext db,
    IDapperRepository<Category> dapperRepository,
    ICategoryRepository categoryRepo)
    : Controller
{
    public IActionResult Index()
    {
        return View(categoryRepo
            .GetAll()
            .ToList());
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

        TryInsertValue_v2(category);
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
            return NotFound();
        return View(categoryRepo.Get(u => u.Id == id));
    }

    [HttpPost]
    public IActionResult Edit(Category category)
    {
        if (!ModelState.IsValid)
            return View();
        TryUpdateCategory_v2(category);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
            return NotFound();
        return View(dapperRepository.GetCategoryByIdDapper(id));
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
            DeleteCategoryByIdEfCore_v2(id);
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
        db.Categories.Remove(db.Categories.Find(id) ?? throw new Exception("Category not found"));
        db.SaveChanges();
    }  
    
    private void DeleteCategoryByIdEfCore_v2(int? id)
    {
        var obj = categoryRepo.Get(u => u.Id == id);
        categoryRepo.Delete(obj);
        categoryRepo.Save();
    }

    private void TryInsertValue(Category category)
    {
        try
        {
            // EfCoreInsert(category);
            dapperRepository.DapperInsert(category);
            TempData["success"] = "Category successfully created.";
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            throw;
        }
    }  
    
    private void TryInsertValue_v2(Category category)
    {
        try
        {
            categoryRepo.Add(category);
            categoryRepo.Save();
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
            db.Update(category);
            TempData["success"] = "Category updated successfully.";
            db.SaveChanges();
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            throw;
        }
    }
    private void TryUpdateCategory_v2(Category category)
    {
        try
        {
            categoryRepo.Update(category);
            categoryRepo.Save();
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            throw;
        }
    }

    private void InsertCategoryEfCore(Category category)
    {
        db.Add(category);
        db.SaveChanges();
    }

    private Category GetCategoryByIdEfCore([DisallowNull] int? id)
    {
        return db.Categories.FirstOrDefault(x => x.Id == id)
               ?? throw new Exception();
    }

    # endregion
}