using Backend.Constants;
using Backend.Interfaces;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Xml.Linq;

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

        public async Task<int> GetLanguageIdAsync(string name) 
        {
            int languageId;
            IEnumerable<Language>? languages;
            if (!_memoryCache.TryGetValue(LanguageConstants.LanguageCacheKey, out languages))
            {               
                languages = _appDbContext.Languages.AsNoTracking().ToList();
                if (languages.Any())
                {
                    _memoryCache.Set(LanguageConstants.LanguageCacheKey, languages, 
                            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));                                      
                } 
                else
                {
                    throw new HttpRequestException("Langugage table empty, WTF?!", null, HttpStatusCode.NotFound);
                }           
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

            return await Task.FromResult(languageId);        
        }

        public async Task<string> GetLanguageNameAsync(int id)
        {
            string languageName;
            IEnumerable<Language>? languages;
            if (!_memoryCache.TryGetValue(LanguageConstants.LanguageCacheKey, out languages))
            {
                languages = _appDbContext.Languages.AsNoTracking().ToList();
                if (languages.Any())
                {
                    _memoryCache.Set(LanguageConstants.LanguageCacheKey, languages,
                            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                }
                else
                {
                    throw new HttpRequestException("Langugage table empty, WTF?!", null, HttpStatusCode.NotFound);
                }
            }

            Language? resultLanguage = languages.FirstOrDefault(l => l.Id == id);
            if (resultLanguage == null)
            {
                throw new HttpRequestException("Cannot find language by Id. Check what you send, partner!", null, HttpStatusCode.NotFound);
            }
            else
            {
                languageName = resultLanguage.Name;
            }

            return await Task.FromResult(languageName);
        }

        public async Task<IEnumerable<Language>> GetAllLanguagesAsync()
        {
            IEnumerable<Language>? languages;
            if (!_memoryCache.TryGetValue(LanguageConstants.LanguageCacheKey, out languages))
            {
                languages = _appDbContext.Languages.AsNoTracking().ToList();
                if (languages.Any())
                {
                    _memoryCache.Set(LanguageConstants.LanguageCacheKey, languages,
                            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                } else
                {
                    throw new HttpRequestException("Langugage table empty, WTF?!", null, HttpStatusCode.NotFound);
                }
            }       

            return await Task.FromResult(languages);
        }
    }
}
