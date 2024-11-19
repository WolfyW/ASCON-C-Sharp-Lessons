namespace ConsoleApp1;

using SimpleGrpcService;
using static SimpleGrpcService.TestService;

class Grpc_test
{
    private readonly ConnectionManager _manager;
    private TestServiceClient _client;
    public Grpc_test(ConnectionManager connectionManager)
    {
        _manager = connectionManager;
        _client = new TestServiceClient(connectionManager.Channel);
    }

    public int SumValue(int x, int y)
    {
        var result = _client.SumValue(new GetSumRequest()
        {
            X = x,
            Y = y
        });

        return result.Result;
    }

    public int PostData(string name, string description)
    {
        var data = new Data()
        {
            Description = description,
            Name = name,
        };
        var response = _client.PostSomeValueModel(new PostModelRequest()
        {
            DataModel = data
        });

        return response.Id;
    }

    public Data GetData(int id)
    {
        var response = _client.GetSomeValueModel(new GetRequest()
        {
            Id = id
        });

        return response.Data;
    }
}
