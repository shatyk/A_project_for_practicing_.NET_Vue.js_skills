using GrpcClient;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("api/grpc-testing")]
[ApiController]
public class GrpcTestingController : ControllerBase
{
    private readonly Greeter.GreeterClient _greeterClient; 
        
    public GrpcTestingController(Greeter.GreeterClient greeterClient)
    {
        _greeterClient = greeterClient;
    }
    
    [HttpGet]
    public async Task<string> GetGreeter()
    {
        return (await _greeterClient.SayHelloAsync(
            new HelloRequest { Name = "GreeterClient" })).Message;
    }
}