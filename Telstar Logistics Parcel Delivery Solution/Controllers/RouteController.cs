using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Models;
using Telstar_Logistics_Parcel_Delivery_Solution.Calculations;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;
using Microsoft.EntityFrameworkCore;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteRequestDtoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private RouteMapperService _service;
        private PriceService _priceService;

        public RouteRequestDtoController(ApplicationDbContext context, RouteMapperService service, PriceService priceService)
        {
            _context = context;
            _service = service;
            _priceService = priceService;
        }
        
        // GET: api/Session
        [HttpPost]
        public RouteResponseDTO ExecuteRouteRequest([FromBody]RouteRequestDto routeRequestDto)
        {
            List<string> cityNames = new List<string>();
            {
                var cities = _context.CITY.ToList();
                foreach (var city in cities)
                {
                    cityNames.Add(city.CityName);
                }
            }

            bool containsStartExists = cityNames.Exists(c => c.Equals(routeRequestDto.startLocation, StringComparison.OrdinalIgnoreCase));
            bool containsEndExists = cityNames.Exists(c => c.Equals(routeRequestDto.endDestination, StringComparison.OrdinalIgnoreCase));
            bool containsTransitExists = cityNames.Exists(c => c.Equals(routeRequestDto.transitLocation, StringComparison.OrdinalIgnoreCase));

            if (routeRequestDto.parcel.Category.Equals("Weapon") || routeRequestDto.parcel.Weight > 40)
            {
                RouteResponseDTO responseDto =
                    new RouteResponseDTO(new List<string>(), 0,
                        0);
                return responseDto;
            }

            
            bool isSignatureChecked = routeRequestDto.parcel.Signature;
            bool isTelstarRoutesOnly = isSignatureChecked == null ? true : (bool)isSignatureChecked;
            if (!isTelstarRoutesOnly) // Remove this if statement when city map is updated to none TelstarRoutes.
            {
                isTelstarRoutesOnly = true;
            }

            if (routeRequestDto.startLocation != null 
                && containsStartExists
                && routeRequestDto.endDestination != null 
                && containsEndExists
                && routeRequestDto.transitLocation != null
                && containsTransitExists)
            {
                List<(int, int, int)> filteredEdges = GetCityMap(isTelstarRoutesOnly);

                List<int> pathToTransit = _service.Execute(filteredEdges, GetLocationId(routeRequestDto.startLocation), GetLocationId(routeRequestDto.transitLocation));
                List<int> pathToEnd = _service.Execute(filteredEdges, GetLocationId(routeRequestDto.transitLocation), GetLocationId(routeRequestDto.endDestination));

                pathToEnd.RemoveAt(0);
                List<int> path = pathToTransit.Concat(pathToEnd).ToList();

                List<String> route = new List<String>();
                route = path
                    .Join(_context.CITY, id => id, city => city.ID, (id, city) => city.CityName)
                    .ToList();
                RouteResponseDTO responseDto =
                    new RouteResponseDTO(route, _priceService.CalculatePrice(routeRequestDto.parcel, path),
                        _priceService.CalculateDuration(path));
                return responseDto;
            }
            else if (routeRequestDto.startLocation != null
                     && containsStartExists
                     && routeRequestDto.endDestination != null
                     && containsEndExists)
            {
                List<(int, int, int)> filteredEdges = GetCityMap(isTelstarRoutesOnly);
                List<int> path = new List<int>();

                path = _service.Execute(filteredEdges, GetLocationId(routeRequestDto.startLocation), GetLocationId(routeRequestDto.endDestination));

                List<String> route = new List<String>();
                route = path
                    .Join(_context.CITY, id => id, city => city.ID, (id, city) => city.CityName)
                    .ToList();

                RouteResponseDTO responseDto =
                    new RouteResponseDTO(route, _priceService.CalculatePrice(routeRequestDto.parcel, path),
                        _priceService.CalculateDuration(path));
                return responseDto;
            }
            else
            {
                RouteResponseDTO responseDto =
                    new RouteResponseDTO(new List<string>(), 0,
                        0);
                return responseDto;
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