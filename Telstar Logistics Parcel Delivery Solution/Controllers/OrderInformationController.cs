using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;


namespace Telstar_Logistics_Parcel_Delivery_Solution.Controllers
{
    [ApiController]
    public class OrderInformationController : Controller
    {
        private const double maxWeight = 40.0;

        /*[HttpPost(Name = "/integration/india")]
        public ActionResult PostIndia([FromBody] ExternalOrderRequestDto externalOrder)
        {
            var client = new EastIndiaRestClient();
            Task<ExternalOrderResponseDto> response = client.PostOrderInformationAsync(createRequestDto());
            ExternalOrderResponseDto orderResponse = JsonConvert.DeserializeObject<ExternalOrderResponseDto>(response);

            return 
        }*/

        [HttpPost]
        [Route("/information/order")]
        public ActionResult Post([FromBody] ExternalOrderRequestDto externalOrder)
        {
            if (!validate(externalOrder))
            {
                return BadRequest(new ErrorResponseDto("This went bad, sorry"));
            }

            return Ok(createResponse());

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


            /*List<string> destination_list = new List<string> { "Dest_1", "Dest_2" };

            if (!IsInList(externalOrderDto.start_destination, destination_list)) { return false; }
            if (!IsInList(externalOrderDto.stop_destination, destination_list)) { return false; }*/

            return true;
        }

        public static bool IsNumber(string input)
        {
            int result1;
            double result2;
            return int.TryParse(input, out result1) || double.TryParse(input, out result2);
        }

        public static bool IsInList(string input, List<string> list)
        {
            return list.Contains(input);
        }

        private ExternalOrderResponseDto createResponse()
        {
            ExternalOrderResponseDto response = new ExternalOrderResponseDto("4", "4");
            return response;
        }

        private ExternalOrderRequestDto createRequestDto()
        {
            DimensionsDto dimensions = new DimensionsDto("20", "20", "20");
            ExternalOrderRequestDto externalOrderRequestDto = new ExternalOrderRequestDto("Tripoli", "Tunis", "2022-01-01", dimensions, "40", "Standard");
            return externalOrderRequestDto;
        }
    }
}