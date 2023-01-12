using Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

public class OceanicRestClient
{
    readonly string url = "http://localhost:9080/order/information";
    private readonly HttpClient _client;

    public OceanicRestClient()
    {
        _client = new HttpClient();
    }
    public async Task<ExternalOrderResponseDto> PostOrderInformationAsync(ExternalOrderRequestDto externalOrderRequestDto)
    {
        var json = JsonConvert.SerializeObject(externalOrderRequestDto);
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await _client.PostAsync("http://localhost:9080/order/information", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        var orderResponse = JsonConvert.DeserializeObject<ExternalOrderResponseDto>(responseContent);
        return orderResponse;
    }
}

