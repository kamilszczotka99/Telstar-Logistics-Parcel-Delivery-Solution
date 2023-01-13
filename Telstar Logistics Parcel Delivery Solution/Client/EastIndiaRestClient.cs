using Models;
using Newtonsoft.Json;
using RestSharp;

public class EastIndiaRestClient
{
    private readonly RestClient client;


    public EastIndiaRestClient()
    {
        client = new RestClient("https://wa-eit-dk2.azurewebsites.net");
    }

    public ExternalOrderResponseDto Post(ExternalOrderRequestDto body)
    {
        ExternalOrderResponseDto responseDto = null;
        var request = new RestRequest("/route/information", Method.Post);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJjb21wYW55IjoiVGVsc3RhciJ9.K7Xjj6FEUqtLzRDUcU-5zsnjjW1S-pGw-ngbbbOaocI");
        request.AddJsonBody(body);
        RestResponse response = client.Execute(request);
        try
        {
            responseDto = JsonConvert.DeserializeObject<ExternalOrderResponseDto>(response.Content);
            // use the responseDto object
        }
        /*catch (JsonSerializationException ex)
        {
            // handle the exception here
            Console.WriteLine("Error deserializing JSON content: " + ex.Message);
        }*/
        catch (System.Net.WebException ex)
        {
            Console.WriteLine("East India is not available: " + ex.Message);
        }
        return responseDto;
    }
}

