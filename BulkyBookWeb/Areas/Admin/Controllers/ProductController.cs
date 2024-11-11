using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyWeb.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController(IDatabaseFactory dbFactory) 
    : Controller
{
    public IActionResult Index()
    {
        return View(dbFactory
            .ProductRepository
            .GetAll()
            .ToList());
    }
    
    public IActionResult Upsert(int? id)
    {
        var isCreateView = id is null or 0;
        if (isCreateView)
        {
            return View();
        }

        try
        {
            var (categoryList, product) = TryGetProductVm(id);
            return View(new ProductVm(product, categoryList));
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            return View();
        }
    }
    
    [HttpPost]
    public IActionResult Upsert(ProductVm obj, IFormFile? file)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Please fill validate fields");
                return View();
            }
        
            try
            {
                dbFactory
                    .ProductRepository
                    .Add(obj.Product);
                dbFactory.Save();
                TempData["success"] = "Category successfully created.";
            }
            catch (Exception e)
            {
                TempData["error"] = e.Message;
                return View();
            }
        
            return RedirectToAction("Index");             
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            return View();
        }
    }

    private (List<SelectListItem> categoryList, Product product) TryGetProductVm(int? id)
    {
        var categoryList = dbFactory
            .CategoryRepository
            .GetAll()
            .Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString()
            })
            .ToList();

        var product = dbFactory
            .ProductRepository
            .Get(u => u.Id == id);

        if (product == null)
        {
            throw new Exception("Didn't find any product with this id.");
        }
        return (categoryList, product);
    }

    public IActionResult Delete(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        return View(dbFactory
            .ProductRepository
            .Get( u => u.Id == id));
    }   
    
    [HttpPost]
    public IActionResult Delete(Product obj)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Error", "Delete got unexpected error");
            return View();
        }

        try
        {
            dbFactory
                .ProductRepository
                .Delete(obj);
            dbFactory.Save();
            TempData["success"] = "Product successfully deleted.";
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
        }
        return RedirectToAction("Index");
    }
}