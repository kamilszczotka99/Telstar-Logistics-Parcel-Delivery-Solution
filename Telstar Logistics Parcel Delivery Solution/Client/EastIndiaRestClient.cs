using Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;

public class EastIndiaRestClient
{
    readonly string url = "http://localhost:9080/order/information";
    private readonly HttpClient _client;

    public EastIndiaRestClient()
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

