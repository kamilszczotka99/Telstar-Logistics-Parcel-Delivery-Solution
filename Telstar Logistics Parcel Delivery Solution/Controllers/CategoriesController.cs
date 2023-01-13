using Azure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers;


[ApiController]
[Route("/categories")]
public class CategoryController : Controller
{
    private ApplicationDbContext applicationDbContext;

    private List<Category> categories;
    public CategoryController(ApplicationDbContext applicationDbContext)
    {
        categories = applicationDbContext.Categories.ToList();
    }

    [HttpGet]
    public ActionResult GetExtCities()
    {
        List<String> extCities = new List<String>();
        foreach (var category in categories)
        {
            extCities.Add(category.CategoryType.ToString() + " (+$" + category.FeePercent.ToString() + ")");
        }
        return Ok(extCities);
    }
}