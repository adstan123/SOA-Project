using Microsoft.EntityFrameworkCore;
using SOA.SecurityApi.Entities;

namespace SOA.SecurityApi.Helpers
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    }
}