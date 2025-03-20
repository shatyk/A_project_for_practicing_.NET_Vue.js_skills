using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using vfdacha;

namespace Backend.Controllers;

[Route("api/grpc-testing")]
[ApiController]
public class GrpcTestingController : ControllerBase
{
    private readonly IGrpcTestingService _grpcTestingService;
    
    public GrpcTestingController(IGrpcTestingService grpcTestingService)
    {
        _grpcTestingService = grpcTestingService;
    }
    
    [HttpGet("unary/{request}")]
    public async Task<bool> UnaryAsync([FromRoute] bool request)
    {
        return await _grpcTestingService.UnaryAsync(request);
    }
    
    [HttpGet("server-streaming/{request}")]
    public async Task<string> ServerStreamingAsync([FromRoute] bool request)
    {
        return await _grpcTestingService.ServerStreamingAsync(request);
    }
    
    [HttpGet("client-streaming/{request}")]
    public async Task<bool> ClientStreamingAsync([FromRoute] bool request)
    {
        return await _grpcTestingService.ClientStreamingAsync(request);
    }
}