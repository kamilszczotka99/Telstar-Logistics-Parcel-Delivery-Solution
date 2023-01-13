using Azure;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers;


[ApiController]
[Route("/cities")]
public class CitiesController : Controller
{
    private ApplicationDbContext applicationDbContext;

    private List<City> cities;
    public CitiesController(ApplicationDbContext applicationDbContext)
    {
        cities = applicationDbContext.CITY.ToList();
    }

    [HttpGet]
    [Route("/cities")]
    public ActionResult GetExtCities()
    {
        List<String> citiesList = new List<String>();
        foreach (var city in cities)
        {
            citiesList.Add(city.CityName);
        }
        return Ok(citiesList);
    }
}