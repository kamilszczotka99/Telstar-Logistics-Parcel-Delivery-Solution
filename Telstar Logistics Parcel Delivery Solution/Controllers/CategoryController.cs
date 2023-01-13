using Microsoft.AspNetCore.Mvc;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers;


[ApiController]
[Route("")]
public class CategoryController : Controller
{
    private ApplicationDbContext applicationDbContext;

    private List<Category> categories;
    public CategoryController(ApplicationDbContext applicationDbContext)
    {
        categories = applicationDbContext.Category.ToList();
    }
    
    // GET
    [Route("/categories")]
    public ActionResult GetCities()
    {
        return Ok(categories);
    }
}