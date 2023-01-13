using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using Telstar_Logistics_Parcel_Delivery_Solution.Data;
using Telstar_Logistics_Parcel_Delivery_Solution.Models;

namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers
{
    [ApiController]
    public class OrderInformationController : Controller
    {
        private const double maxWeight = 40.0;
        private ApplicationDbContext applicationDbContext;
        private List<Category> categories;
        private List<City> citys;

        public OrderInformationController(ApplicationDbContext applicationDbContext)
        {
            categories = applicationDbContext.Categories.ToList();
            citys = applicationDbContext.CITY.ToList();
        }

        [HttpPost]
        [Route("/integration/india")]
        public ActionResult PostIndia([FromBody] ExternalOrderRequestDto externalOrder)
        {
            var client = new EastIndiaRestClient();
            var response = client.Post(externalOrder);
            if(response == null)
            {
                return BadRequest(new ErrorResponseDto("East India has rejected our request"));
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("/integration/oceanic")]
        public ActionResult PostOceanic([FromBody] ExternalOrderRequestDto externalOrder)
        {
            var client = new OceanicRestClient();
            var response = client.Post(externalOrder);
            if (response == null)
            {
                return BadRequest(new ErrorResponseDto("Oceanic has rejected our request"));
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("/route/information")]
        public ActionResult Post([FromBody] ExternalOrderRequestDto externalOrder)
        {
            sanitize(externalOrder);
            if (!validate(externalOrder))
            {
                return BadRequest(new ErrorResponseDto("This went bad, sorry"));
            }

            return Ok(createResponse());

        }

        private void sanitize(ExternalOrderRequestDto externalOrderDto)
        {
            externalOrderDto.cityTo=externalOrderDto.cityTo.ToUpper();
            externalOrderDto.cityFrom=externalOrderDto.cityFrom.ToUpper();

            for(int i = 0; i<externalOrderDto.categories.Count; i++) {
                externalOrderDto.categories[i]= externalOrderDto.categories[i].ToUpper();
            }
        }

        private bool validate(ExternalOrderRequestDto externalOrderDto)
        {
            if (externalOrderDto == null) { return false; }
            if (!IsNumber(externalOrderDto.dimensions.width)) { return false; }
            if (!IsNumber(externalOrderDto.dimensions.height)) { return false; }
            if (!IsNumber(externalOrderDto.dimensions.length)) { return false; }
            if (!IsNumber(externalOrderDto.weight)) { return false; }
            double weight_double = double.Parse(externalOrderDto.weight);
            if (weight_double > maxWeight) { return false; }
            if (externalOrderDto.categories.Contains("weapon")) { return false; }


            
            List<String> normalised_categories = new List<String>();
            foreach (var category in categories)
            {
                normalised_categories.Add(category.NormalizedCategoryType.ToUpper());
            }

            
            List<String> normalised_cities = new List<String>();
            foreach (var city in citys)
            {
                normalised_cities.Add(city.NormalizedName.ToUpper());
            }

            if (!CompareLists(externalOrderDto.categories, normalised_categories)) { return false; }

            if (!IsInList(externalOrderDto.cityFrom, normalised_cities)) { return false; }
            if (!IsInList(externalOrderDto.cityTo, normalised_cities)) { return false; }
            
            return true;
        }

        private static bool IsNumber(string input)
        {
            int result1;
            double result2;
            return int.TryParse(input, out result1) || double.TryParse(input, out result2);
        }

        private static bool IsInList(string input, List<string> list)
        {
            return list.Contains(input);
        }

        private static bool CompareLists(List<string> incomingList, List<string> referenceList)
        {
            // Use the All method to check if all elements in the incoming list exist in the reference list
            return incomingList.All(referenceList.Contains);
        }


        private ExternalOrderResponseDto createResponse()
        {
            ExternalOrderResponseDto response = new ExternalOrderResponseDto("4", "4");
            return response;
        }

        private ExternalOrderRequestDto createRequestDto()
        {
            DimensionsDto dimensions = new DimensionsDto("20", "20", "20");
            List<String> list_categories = new List<String>();
            list_categories.Add("Fragile");
            list_categories.Add("Cooled");
            ExternalOrderRequestDto externalOrderRequestDto = new ExternalOrderRequestDto("Tripoli", "Tunis", "2022-01-01", dimensions, "40", list_categories);
            return externalOrderRequestDto;
        }
    }
}