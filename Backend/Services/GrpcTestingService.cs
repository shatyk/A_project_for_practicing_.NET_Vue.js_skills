using Backend.Interfaces;
using Grpc.Core;
using vfdacha;

namespace Backend.Services;

public class GrpcTestingService : IGrpcTestingService
{
    private readonly vfdacha.GrpcService.GrpcServiceClient _grpcServiceClient;
    
    public GrpcTestingService(vfdacha.GrpcService.GrpcServiceClient grpcServiceClient)
    {
        _grpcServiceClient = grpcServiceClient;
    }
    
    public async Task<bool> UnaryAsync(bool request)
    {
        return (await _grpcServiceClient.UnaryAsync(new GrpcRequest { Value = request })).Value;
    }

    public async Task<string> ServerStreamingAsync(bool request)
    {
        using var call = _grpcServiceClient.ServerStream(new GrpcRequest { Value = request });
        string result = "";
        
        while (await call.ResponseStream.MoveNext())
        {
            Console.WriteLine(call.ResponseStream.Current.Value);
            result += call.ResponseStream.Current.Value + " ";
        }
        
        return result;
    }

    public async Task<bool> ClientStreamingAsync(bool request)
    {
        using var call = _grpcServiceClient.ClientStream();
        
        bool val = request;
        for (var i = 0; i < 5; i++)
        {
            val = !val;
            await call.RequestStream.WriteAsync(new GrpcRequest { Value = val });
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
        
        await call.RequestStream.CompleteAsync();
        return (await call).Value;
    }
}