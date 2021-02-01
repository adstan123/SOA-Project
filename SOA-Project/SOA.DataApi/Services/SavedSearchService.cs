using Microsoft.EntityFrameworkCore;
using SOA.DataApi.Entities;
using SOA.DataApi.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SOA.DataApi.Services
{
    public interface ISavedSearchService
    {
        public Task<SavedSearch> SaveSearch(SavedSearch search);
        public Task<List<SavedSearch>> GetSavedSearchesByUsername(string username);
    }

    public class SavedSearchService : ISavedSearchService
    {
        private DataContext _context;

        public SavedSearchService(DataContext context)
        {
            _context = context;
        }

        public Task<List<SavedSearch>> GetSavedSearchesByUsername(string username)
        {
            return _context.SavedSearches.Where(s => s.Username == username).ToListAsync();
        }

        public async Task<SavedSearch> SaveSearch(SavedSearch search)
        {
            await _context.AddAsync(search);
            await _context.SaveChangesAsync();
            return search;
        }
    }
}
