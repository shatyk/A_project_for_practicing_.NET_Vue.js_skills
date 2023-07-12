using Backend.Interfaces;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace Backend.Services
{
    public class LanguageCacheService : ILanguageCacheService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMemoryCache _memoryCache;

        public LanguageCacheService(AppDbContext appDbContext, IMemoryCache memoryCache)
        {
            _appDbContext = appDbContext;
            _memoryCache = memoryCache;
        }

        public async Task<int> GetLanguageId(string name) 
        {
            int languageId;
            if (!_memoryCache.TryGetValue(name, out languageId))
            {
                IEnumerable<Language> languages = _appDbContext.Languages.AsNoTracking();
                if (languages != null)
                {
                    foreach (Language language in languages)
                    {
                        _memoryCache.Remove(language.Name);
                        _memoryCache.Set(language.Name, language.Id, 
                            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                    }
                    Language? resultLanguage = languages.FirstOrDefault(l => l.Name == name);
                    if (resultLanguage == null)
                    {
                        throw new HttpRequestException("Cannot find language by name. Check what you send, partner!", null, HttpStatusCode.NotFound);
                    }
                    else
                    {
                        languageId = resultLanguage.Id;
                    }
                } else
                {
                    throw new HttpRequestException("Langugage table empty, WTF?!", null, HttpStatusCode.NotFound);
                }               
            }
            return await Task.FromResult(languageId);        
        }
    }
}
