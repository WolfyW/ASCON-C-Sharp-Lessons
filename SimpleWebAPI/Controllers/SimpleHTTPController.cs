using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace SimpleWebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SimpleHTTPController : ControllerBase
{
    private readonly DataManager _manager;
    private readonly ILogger<SimpleHTTPController> _logger;
    public SimpleHTTPController(ILogger<SimpleHTTPController> logger, DataManager manager)
    {
        _logger = logger;
        _manager = manager;
    }

    [HttpGet]
    [Route(nameof(GetSomeValueJson))]
    public string GetSomeValueJson([FromQuery] int id)
    {
        _logger.LogInformation($"{DateTime.Now} http call: {nameof(GetSomeValueJson)} id: {id}");
        var data = _manager.GetData(id);
        return JsonSerializer.Serialize(data);
    }

    [HttpGet]
    [Route(nameof(GetSomeValueModel))]
    public Data GetSomeValueModel([FromQuery] int id)
    {
        _logger.LogInformation($"{DateTime.Now} http call: {nameof(GetSomeValueModel)} id: {id}");

        return _manager.GetData(id);
    }

    [HttpGet]
    [Route(nameof(SumValue))]
    public int SumValue([FromQuery] int x, [FromQuery] int y)
    {
        _logger.LogInformation($"{DateTime.Now} http call: {nameof(SumValue)} x: {x}, y {y}");

        return x + y;
    }


    [HttpPost]
    [Route(nameof(PostSomeValueJson))]
    public int PostSomeValueJson([FromBody] string data)
    {
        _logger.LogInformation($"{DateTime.Now} http call: {nameof(PostSomeValueJson)}");

        var info = JsonSerializer.Deserialize<Data>(data);
        var added = _manager.AddData(info);

        return added.Id;
    }


    [HttpPost]
    [Route(nameof(PostSomeValueModel))]
    public int PostSomeValueModel([FromBody] Data data)
    {
        _logger.LogInformation($"{DateTime.Now} http call: {nameof(PostSomeValueModel)}");

        var x = _manager.AddData(data);

        return x.Id;
    }
}
