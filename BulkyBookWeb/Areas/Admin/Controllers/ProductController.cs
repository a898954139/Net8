using Bulky.DataAccess.Repository.Interfaces;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc;

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

    public IActionResult Create()
    {
        return View(); 
    }   
    
    [HttpPost]
    public IActionResult Create(Product obj)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("Error", "Please fill validate fields");
            return View();
        }
        
        try
        {
            dbFactory.ProductRepository.Add(obj);
            dbFactory.Save();
            TempData["success"] = "Category successfully created.";
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
        }
        
        return RedirectToAction("Index"); 
    }

    public IActionResult Edit(int? id)
    {
        if (id is null or 0)
        {
            return NotFound();
        }
        return View(dbFactory
            .ProductRepository
            .Get(u => u.Id == id));
    }

    [HttpPost]
    public IActionResult Edit(Product obj)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        try
        {
            dbFactory
                .ProductRepository
                .Update(obj);
            dbFactory.Save();
            TempData["success"] = "Product successfully updated.";
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
        }
        return RedirectToAction("Index");
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