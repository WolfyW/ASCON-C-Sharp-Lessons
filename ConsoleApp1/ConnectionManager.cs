namespace ConsoleApp1;

using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using System;
using System.Security.Cryptography.X509Certificates;

public class ConnectionManager : IDisposable
{
    private readonly TimeSpan InitialReconnectBackoff = TimeSpan.FromSeconds(1);
    private readonly TimeSpan MaxReconnectBackoff = TimeSpan.FromSeconds(40);
    private readonly int? MaxReceiveMessageSize = null;
    private readonly int? MaxSendMessageSize = null;
    private readonly int? MaxRetryAttempts = 1;
    private readonly bool UnsafeUseInsecureChannelCallCredentials = false;
    private readonly long? MaxRetryBufferPerCallSize = null;
    private readonly long? MaxRetryBufferSize = null;

    private Uri ServerLocation;
    private GrpcChannel _channel;

    public ConnectionManager(string serverName, int port)
    {
        InitChannel(serverName, port);
    }

    public string State { get; set; }

    public void InitChannel(string serverName, int port)
    {
        ServerLocation = new UriBuilder(Uri.UriSchemeHttps, serverName, port).Uri;
        _channel = GetChannel(ServerLocation);
    }

    public GrpcChannel Channel
    {
        get
        {
            return _channel ??= GetChannel(ServerLocation);
        }
    }

    public void Dispose()
    {
        if (_channel != null)
        {
            _channel.Dispose();
            State = null;
            _channel = null;
        }
    }

    private GrpcChannel GetChannel(Uri ServerLocation)
    {
        if (ServerLocation is null)
            throw new("Host для создания grpc канала не определен. Внутренняя ошибка.");

        GrpcChannel channel = _channel;

        if (!channel?.Target?.Equals(ServerLocation?.Authority) ?? false)
        {
            channel?.Dispose();
        }
        if (channel is null)
        {
            if (Environment.OSVersion?.Version?.Major >= 10 is true)
                channel = GetGrpcHttp2Channel(ServerLocation, (cert) =>
                {
                    return Task.FromResult(true);
                });
            else
                channel = GetGrpcWebChannel(ServerLocation, (cert) =>
                {
                    return Task.FromResult(true);
                });
        }
        return channel;
    }

    private GrpcChannel GetGrpcHttp2Channel(Uri serverLocation, Func<X509Certificate2, Task<bool>> doValidate)
    {
        var clientHandler = new SocketsHttpHandler()
        {
            EnableMultipleHttp2Connections = true,
            SslOptions = new()
            {
                RemoteCertificateValidationCallback = (httpResponce, serverCert, x509chin, errors) => doValidate(new(serverCert)).Result
            }
        };

        var channel = GrpcChannel.ForAddress(serverLocation, new()
        {
            HttpHandler = clientHandler,
            UnsafeUseInsecureChannelCallCredentials = UnsafeUseInsecureChannelCallCredentials,
            MaxReceiveMessageSize = MaxReceiveMessageSize,
            MaxSendMessageSize = MaxSendMessageSize,
            MaxRetryAttempts = MaxRetryAttempts,
            InitialReconnectBackoff = InitialReconnectBackoff,
            MaxReconnectBackoff = MaxReconnectBackoff,
            MaxRetryBufferPerCallSize = MaxRetryBufferPerCallSize,
            MaxRetryBufferSize = MaxRetryBufferSize
        });

        return channel;
    }

    private GrpcChannel GetGrpcWebChannel(Uri ServerLocation, Func<X509Certificate2, Task<bool>> doValidate)
    {
        var clientHandler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = (httpResponce, serverCert, x509chin, errors) => doValidate(serverCert).Result
        };
        return GrpcChannel.ForAddress(ServerLocation, new GrpcChannelOptions
        {
            HttpHandler = new GrpcWebHandler(clientHandler),
            UnsafeUseInsecureChannelCallCredentials = UnsafeUseInsecureChannelCallCredentials,
            MaxReceiveMessageSize = MaxReceiveMessageSize,
            MaxSendMessageSize = MaxSendMessageSize,
            MaxRetryAttempts = MaxRetryAttempts,
            InitialReconnectBackoff = InitialReconnectBackoff,
            MaxReconnectBackoff = MaxReconnectBackoff,
            MaxRetryBufferPerCallSize = MaxRetryBufferPerCallSize,
            MaxRetryBufferSize = MaxRetryBufferSize
        });
    }

}