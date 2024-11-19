namespace ConsoleApp1;
using RestSharp;
using System;

internal class HTTP_NuGet_Test
{
    private readonly RestClient _client;
    public HTTP_NuGet_Test()
    {
        var options = new RestClientOptions("https://localhost:7216/api/SimpleHTTP/");
        _client = new RestClient(options);
    }

    public void SumValue(int x, int y)
    {
        var request = new RestRequest($"SumValue")
            .AddQueryParameter("x", x)
            .AddQueryParameter("y", y);

        var reponse = _client.Get<int>(request);
        Console.WriteLine(reponse);
    }

    public int PostValue(string name, string description)
    {
        var body = new DataDto()
        {
            Description = description,
            Name = name,
        };
        var request = new RestRequest($"PostSomeValueModel")
            .AddBody(body);

        var reponse = _client.Post<int>(request);

        return reponse;
    }

    public DataDto GetDataValue(int id)
    {
        var request = new RestRequest($"GetSomeValueModel").AddQueryParameter("id", id);

        return _client.Get<DataDto>(request);
    }
}
