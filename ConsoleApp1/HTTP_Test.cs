namespace ConsoleApp1;
using System;
using System.Net.Http.Headers;
using System.Text.Json;

internal class HTTP_Test
{
    private HttpClient _client;
    public HTTP_Test()
    {
        _client = new HttpClient();
    }

    public int Sum(int x, int y)
    {
        HttpRequestMessage handler = new HttpRequestMessage();
        handler.Method = HttpMethod.Get;        
        handler.RequestUri = new Uri($"https://localhost:7216/api/SimpleHTTP/SumValue?x={x}&y={y}");
        var response = _client.Send(handler);
        var sw = response.Content.ReadAsStringAsync().Result;
        return int.Parse(sw);
    }

    public int PostData(string name, string description)
    {
        HttpRequestMessage handler = new HttpRequestMessage();
        handler.Method = HttpMethod.Post;
        handler.RequestUri = new Uri($"https://localhost:7216/api/SimpleHTTP/PostSomeValueModel");

        var str = JsonSerializer.Serialize(new DataDto()
        {
            Description = description,
            Name = name,
        });

        MediaTypeHeaderValue mediaType = new MediaTypeHeaderValue("application/json");
        handler.Content = new StringContent(str, mediaType);

        var response = _client.Send(handler);
        var sw = response.Content.ReadAsStringAsync().Result;
        
        return int.Parse(sw);
    }

    public DataDto GetDataJson(int id)
    {
        HttpRequestMessage handler = new HttpRequestMessage();
        handler.Method = HttpMethod.Get;
        handler.RequestUri = new Uri($"https://localhost:7216/api/SimpleHTTP/GetSomeValueJson?id={id}");
        var response = _client.Send(handler);
        var sw = response.Content.ReadAsStringAsync().Result;

        var options = new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;

        var data = JsonSerializer.Deserialize<DataDto>(sw, options);

        return data;
    }

    public DataDto GetDataModel(int id)
    {
        HttpRequestMessage handler = new HttpRequestMessage();
        handler.Method = HttpMethod.Get;
        handler.RequestUri = new Uri($"https://localhost:7216/api/SimpleHTTP/GetSomeValueModel?id={id}");
        var response = _client.Send(handler);
        var sw = response.Content.ReadAsStringAsync().Result;

        var options = new JsonSerializerOptions();
        options.PropertyNameCaseInsensitive = true;
        var data = JsonSerializer.Deserialize<DataDto>(sw, options);

        return data;
    }
}
