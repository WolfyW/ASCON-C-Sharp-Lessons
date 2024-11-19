namespace SimpleWebAPI.Service;

using Grpc.Core;
using SimpleGrpcService;
using SimpleWebAPI.Controllers;
using System.Text.Json;
using System.Threading.Tasks;

public class TestGrpcService : TestService.TestServiceBase
{
    private readonly DataManager _dataManager;
    private readonly ILogger<TestGrpcService> _logger;

    public TestGrpcService(ILogger<TestGrpcService> logger, DataManager dataManager)
    {
        _logger = logger;
        _dataManager = dataManager;
    }

    public override Task<GetJsonResponse> GetSomeValueJson(GetRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"{DateTime.Now} grpc call: {nameof(GetSomeValueJson)} id: {request.Id}");

        var d = _dataManager.GetData(request.Id);

        return Task.FromResult(new GetJsonResponse()
        {
            Data = JsonSerializer.Serialize(d)
        });
    }

    public override Task<GetModelResponse> GetSomeValueModel(GetRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"{DateTime.Now} grpc call: {nameof(GetSomeValueModel)} id: {request.Id}");

        var d = _dataManager.GetData(request.Id);

        return Task.FromResult<GetModelResponse>(new GetModelResponse()
        {
            Data = new SimpleGrpcService.Data()
            {
                Description = d.Description,
                Name = d.Name,
                Id = d.Id,
            }
        });

    }

    public override Task<PostResponse> PostSomeValueJson(PostJsonRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"{DateTime.Now} grpc call: {nameof(PostSomeValueJson)}");

        var data = _dataManager.AddData(request.DataJson);
        return Task.FromResult(new PostResponse()
        {
            Id = data.Id
        });
    }

    public override Task<PostResponse> PostSomeValueModel(PostModelRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"{DateTime.Now} grpc call: {nameof(PostSomeValueModel)}");

        var data = new Controllers.Data()
        {
            Description = request.DataModel.Description,
            Name = request.DataModel.Name,
        };

        data = _dataManager.AddData(data);
        return Task.FromResult(new PostResponse()
        {
            Id = data.Id
        });
    }


    public override Task<GetSumResponse> SumValue(GetSumRequest request, ServerCallContext context)
    {
        _logger.LogInformation($"{DateTime.Now} grpc call: {nameof(SumValue)} x: {request.X} y: {request.Y}");

        return Task.FromResult(new GetSumResponse()
        {
            Result = request.X + request.Y,
        });
    }
}
