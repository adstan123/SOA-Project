using Microsoft.EntityFrameworkCore;
using SOA.DataApi.Entities;

namespace SOA.DataApi.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<SavedSearch> SavedSearches { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}