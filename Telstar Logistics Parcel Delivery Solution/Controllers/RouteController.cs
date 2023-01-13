using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telstar_Logistics_Parcel_Delivery_Solution.Calculations;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;
namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteRequestDtoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private RouteMapperService _service;

        public RouteRequestDtoController(ApplicationDbContext context, RouteMapperService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/Session
        [HttpGet]
        public List<string> ExecuteRouteRequest(String startLocation, String endDestination, String? transitLocation, bool? isSignatureChecked)
        {
            bool isTelstarRoutesOnly = isSignatureChecked == null ? true : (bool)isSignatureChecked;
            if (!isTelstarRoutesOnly) // Remove this if statement when city map is updated to none TelstarRoutes.
            {
                isTelstarRoutesOnly = true;
            }

            if (startLocation != null && endDestination != null && transitLocation != null)
            {
                List<(int, int, int)> filteredEdges = GetCityMap(isTelstarRoutesOnly);

                List<int> pathToTransit = _service.Execute(filteredEdges, GetLocationId(startLocation), GetLocationId(transitLocation));
                List<int> pathToEnd = _service.Execute(filteredEdges, GetLocationId(transitLocation), GetLocationId(endDestination));

                pathToEnd.RemoveAt(0);
                List<int> path = pathToTransit.Concat(pathToEnd).ToList();

                List<String> route = new List<String>();
                route = path
                    .Join(_context.CITY, id => id, city => city.ID, (id, city) => city.CityName)
                    .ToList();

                return route;
            }
            else if (startLocation != null && endDestination != null)
            {
                List<(int, int, int)> filteredEdges = GetCityMap(isTelstarRoutesOnly);
                List<int> path = new List<int>();

                path = _service.Execute(filteredEdges, GetLocationId(startLocation), GetLocationId(endDestination));

                List<String> route = new List<String>();
                route = path
                    .Join(_context.CITY, id => id, city => city.ID, (id, city) => city.CityName)
                    .ToList();

                return route;
            }
            else
            {
                return new List<string>();
            }
        }

        private int GetLocationId(String startLocation)
        {
            return _context.CITY.Where(c => c.CityName == startLocation)
                .Select(c => c.ID)
                .FirstOrDefault();
        }

        private List<(int, int, int)> GetCityMap(bool isTelstarRoutesOnly)
        {
            List<Edge> edges = _context.Edges.ToList();
            if (isTelstarRoutesOnly)
            {
                return edges.Where(e => e.Distance > 0)
                    .Select(e => (e.IdSource, e.IdTarget, e.Distance))
                    .ToList();
            }
            else
            {
                return new List<(int, int, int)>();
            }
            
        }
    }
}