namespace Backend.Interfaces;

public interface IGrpcTestingService
{
    Task<bool> UnaryAsync(bool request);
    Task<string> ServerStreamingAsync(bool request);
    Task<bool> ClientStreamingAsync(bool request);
}