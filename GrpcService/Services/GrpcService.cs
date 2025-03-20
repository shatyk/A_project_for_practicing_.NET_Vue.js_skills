using Grpc.Core;
using vfdacha;

namespace GrpcService.Services;

public class GrpcService : vfdacha.GrpcService.GrpcServiceBase
{
    public GrpcService()
    {
    }

    public override Task<GrpcResponse> Unary(GrpcRequest request, ServerCallContext context)
    {
        return Task.FromResult(new GrpcResponse
        {
            Value = !request.Value
        });
    }
    
    public override async Task ServerStream(GrpcRequest request,
        IServerStreamWriter<GrpcResponse> responseStream, ServerCallContext context)
    {
        bool val = request.Value;
        for (var i = 0; i < 5 && !context.CancellationToken.IsCancellationRequested; i++)
        {
            val = !val;
            await responseStream.WriteAsync(new GrpcResponse {Value = val});
            await Task.Delay(TimeSpan.FromSeconds(3));
        }
    }
    
    public override async Task<GrpcResponse> ClientStream(IAsyncStreamReader<GrpcRequest> requestStream, ServerCallContext context)
    {
        bool res = false;
        await foreach (var message in requestStream.ReadAllAsync())
        {
            Console.WriteLine(message.Value);
            res = message.Value;
        }
        return new GrpcResponse {Value = res};
    }
}