using Database.Models;

namespace Backend.Interfaces
{
    public interface ILanguageCacheService
    {
        Task<int> GetLanguageIdAsync(string name);
        Task<string> GetLanguageNameAsync(int id);
        Task<IEnumerable<Language>> GetAllLanguagesAsync();
    }
}
