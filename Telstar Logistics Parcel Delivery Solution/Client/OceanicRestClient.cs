using Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class OceanicRestClient
{
    private readonly RestClient client;


    public OceanicRestClient()
    {
        client = new RestClient("https://wa-tl-dk2.azurewebsites.net");
    }

    public ExternalOrderResponseDto Post(ExternalOrderRequestDto body)
    {
        ExternalOrderResponseDto responseDto = null;
        var request = new RestRequest("/information/order", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddJsonBody(body);
        RestResponse response = client.Execute(request);
        try
        {
            responseDto = JsonConvert.DeserializeObject<ExternalOrderResponseDto>(response.Content);
            // use the responseDto object
        }
        catch (JsonSerializationException ex)
        {
            // handle the exception here
            Console.WriteLine("Error deserializing JSON content: " + ex.Message);
        }
        catch (System.Net.WebException ex)
        {
            Console.WriteLine("Oceanic is not available: " + ex.Message);
        }
        return responseDto;
    }
}

