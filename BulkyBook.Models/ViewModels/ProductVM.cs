using System.Reflection;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bulky.Models.ViewModels;

public record ProductVm(
    Product Product,
    IEnumerable<SelectListItem> CategoryList)
{
    public Product Product { get; set; } = Product;
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; } = CategoryList;

    public static IEnumerable<PropertyInfo> GetAllProperties()
    {
        var productVmProperties = typeof(ProductVm)
            .GetProperties()
            .Where(p => p.Name != "Product");
        var productProperties = (IEnumerable<PropertyInfo>)typeof(Product)
            .GetProperties();
        return productProperties.Concat(productVmProperties);
    }}