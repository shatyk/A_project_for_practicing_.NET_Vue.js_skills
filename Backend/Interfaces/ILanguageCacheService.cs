namespace Backend.Interfaces
{
    public interface ILanguageCacheService
    {
        Task<int> GetLanguageId(string name);
    }
}
